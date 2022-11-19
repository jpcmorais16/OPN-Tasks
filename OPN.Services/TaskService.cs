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
        private readonly IProductHandlingTaskDataFetcher _taskFetcher;
        private readonly IProductHandlingTaskDataCommiter _taskCommiter;
        private readonly IUserDataFetcher _userDataFetcher;
        private readonly IUserDataCommiter _userDataCommiter;
        public TaskService(IProductHandlingTaskDataFetcher taskFetcher, IProductHandlingTaskDataCommiter taskCommiter, IUserDataFetcher userDataFetcher, IUserDataCommiter userDataCommiter)
        {
            _taskFetcher = taskFetcher;
            _taskCommiter = taskCommiter;
            _userDataFetcher = userDataFetcher;
            _userDataCommiter = userDataCommiter;
        }
        public void CompleteTask(string UserIDN)
        {
            var user = _userDataFetcher.FetchUser(UserIDN);

            if (user == null)
                throw new Exception("Esse IDN não fez login!");

            user.CompleteTask();
        }

        public OPNProductHandlingTask CreateRandomProductHandlingTask(TaskRequest request)//TODO:move this method into the user
        {
            //get a random product

            var products = _taskFetcher.FetchProducts();
            var tasks = _taskFetcher.FetchProductHandlingTasks();
            var user = _userDataFetcher.FetchUser(request.LoggedUserIDN);

            if (user == null)
                throw new Exception("Este IDN não fez login!");

            if (user.TaskGoal is not null)
                return tasks.FirstOrDefault(t => t.ConclusionTime == null && t.UserIDN.Equals(user.IDN))!;       

            user._userDataCommiter = _userDataCommiter;
            var institutions = products.First().Institutions;//fazer o filtro
            
            
            //this method of filtering is not optimal and should be eventually replaced

            var remainingProducts = products.Where(p => p.Institutions.Any(i =>
            !(tasks.Any(t => t.Product.Name.Equals(p.Name) && t.InstitutionName.Equals(i))))).ToList();//O(n^3)

            if (remainingProducts.Count == 0) {
                var cancelledTask = tasks.FirstOrDefault(t => t.UserIDN.Equals("nulo"));
                if (cancelledTask != null)  throw new Exception("Não há mais tasks!"); 

                if (cancelledTask != null)
                {
                    cancelledTask._commiter = _taskCommiter;
                    cancelledTask.UpdateIDN(request.LoggedUserIDN);
                    user.AddTask(cancelledTask);

                    return cancelledTask;
                }
            }
                

            Random random = new Random();

            Product taskProduct = remainingProducts[random.Next(0, remainingProducts.Count)];

            var availableInstitutions = taskProduct.Institutions.Where(i =>
            !(tasks.Any(t => t.Product.Name.Equals(taskProduct.Name) && t.InstitutionName.Equals(i))) && taskProduct.GetInstitutionProportion(i) != 0).ToList();

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

        public void CancelTask(string IDN)
        {
            var user = _userDataFetcher.FetchUser(IDN);

            if(user == null)
                throw new Exception("Este IDN não fez login!");

            user.CancelTask();
        }

        public List<OPNProductHandlingTask> GetAllCompletedTasks()
        {
            var tasks = _taskFetcher.FetchProductHandlingTasks();

            return tasks.Where(t => t.ConclusionTime != null).ToList();
        }

        public List<OPNProductHandlingTask> GetUserCompletedTasks(string userIDN)
        {
            var tasks = _taskFetcher.FetchProductHandlingTasks();

            return tasks.Where(t => t.ConclusionTime != null && t.UserIDN.Equals(userIDN)).ToList();
        }

        public int GetUserNumberOfCompletedTasks(string userIDN)
        {
            //var tasks = _taskFetcher.FetchProductHandlingTasks();

            //return tasks.Where(t => t.ConclusionTime != null && t.UserIDN.Equals(userIDN)).ToList().Count;

            var user = _userDataFetcher.FetchUser(userIDN);

            return user.GetNumberOfCompletedTasks();
        }

        public int GetNumberOfCompletedTasks()
        {
            var tasks = _taskFetcher.FetchProductHandlingTasks();

            return tasks.Where(t => t.ConclusionTime != null).ToList().Count;
        }

        public List<LoggedUser> GetRanking()
        {
            var users = _userDataFetcher.FetchUsers();

            return users.OrderByDescending(u => u.CompletedTasks).ToList();
        }
    }
}
