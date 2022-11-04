using OPN.Domain;
using OPN.Domain.Interfaces;
using OPN.Domain.Login;
using OPN.Domain.Tasks;
using OPN.Services.Interfaces;
using OPN.Services.Interfaces.DataInterfaces;
using OPN.Services.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPN.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskDataFetcher _taskFetcher;
        private readonly IProductHandlingTaskDataCommiter _taskCommiter;
        private readonly IUserDataFetcher _userDataFetcher;
        private readonly IUserDataCommiter _userDataCommiter;
        public TaskService(ITaskDataFetcher taskFetcher, IProductHandlingTaskDataCommiter taskCommiter, IUserDataFetcher userDataFetcher, IUserDataCommiter userDataCommiter)
        {
            _taskFetcher = taskFetcher;
            _taskCommiter = taskCommiter;
            _userDataFetcher = userDataFetcher;
            _userDataCommiter = userDataCommiter;
        }
        public void CompleteTask(string UserIDN)
        {
            var user = _userDataFetcher.FetchUser(UserIDN);
            user.CompleteTask();
        }

        public OPNProductHandlingTask CreateRandomProductHandlingTask(TaskRequest request)
        {
            //get a random product

            var products = _taskFetcher.FetchProducts();
            var tasks = _taskFetcher.FetchProductHandlingTasks();
            var user = _userDataFetcher.FetchUser(request.LoggedUserIDN);
            user._userDataCommiter = _userDataCommiter;
            var institutions = products.First().Institutions;//fazer o filtro
            
            
            //this method of filtering is not optimal and should be eventually replaced

            var remainingProducts = products.Where(p => p.Institutions.Any(i =>
            !(tasks.Any(t => t.Product.Name.Equals(p.Name) && t.InstitutionName.Equals(i))))).ToList();//O(n^3)

            if (remainingProducts.Count == 0)
                throw new Exception("Não há mais tasks");

            Random random = new Random();

            Product taskProduct = remainingProducts[random.Next(0, remainingProducts.Count)];

            var availableInstitutions = taskProduct.Institutions.Where(i =>
            !(tasks.Any(t => t.Product.Name.Equals(taskProduct.Name) && t.InstitutionName.Equals(i)))).ToList();

            var institutionName = availableInstitutions[random.Next(0, availableInstitutions.Count)];

            var institutionProportion = taskProduct.GetInstitutionProportion(institutionName);

            var task = new OPNProductHandlingTask(tasks.Count + 1,
                                            request.LoggedUserIDN,
                                            taskProduct,
                                            institutionName,
                                            institutionProportion,
                                            _taskCommiter
                                        );

            //register task on db

            task.Register();

            //return task to the logged user
            user.AddTask(task);


            return task;
        }

        public Task<List<OPNProductHandlingTask>> GetAllCompletedTasks()
        {
            throw new NotImplementedException();
        }

        public Task<List<OPNProductHandlingTask>> GetLoggedUserCompletedTasks()
        {
            throw new NotImplementedException();
        }

        public Task<int> GetLoggedUserNumberOfCompletedTasks()
        {
            throw new NotImplementedException();
        }

        public Task<int> GetNumberOfCompletedTasks()
        {
            throw new NotImplementedException();
        }
    }
}
