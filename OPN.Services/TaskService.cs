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
        private readonly ITaskDataFetcher _fetcher;
        private readonly IProductHandlingTaskDataCommiter _commiter;
        public TaskService(ITaskDataFetcher fetcher, IProductHandlingTaskDataCommiter commiter)
        {
            _fetcher = fetcher;
            _commiter = commiter;
        }
        public Task CompleteTask(string UserIDN)
        {
            //register on db that task is completed
            throw new NotImplementedException();
        }

        public OPNProductHandlingTask CreateRandomProductHandlingTask(TaskRequest request)
        {
            //get a random unfinished product

            var products = _fetcher.FetchProducts();
            //var tasks = await _handler.GetProductHandlingTasks(); 
            Random random = new Random();
            Product taskProduct = products[random.Next(0, products.Count)];

            //create task

            //not optimal
            var institutionWithProportion = taskProduct.GetRandomInstitutionWithProportion(s => true);
                
            //    instName => 

            //        !(tasks.Any(

            //                    t =>

            //                    t.Product.Name.Equals(taskProduct.Name) 

            //                                     &&

            //                     t.InstitutionName.Equals(instName)
                                 
            //        )

            //    )
                     
            //);

            var task = new OPNProductHandlingTask(
                                            request.LoggedUserIDN,
                                            taskProduct,
                                            institutionWithProportion,
                                            _commiter
                                        );

            //register task on db

            task.Register();

            //return task to the logged user
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
