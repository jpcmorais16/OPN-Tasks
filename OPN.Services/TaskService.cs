using OPN.Domain;
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
        private readonly ITaskDataHandler _handler;
        public TaskService(ITaskDataHandler handler)
        {
            _handler = handler;
        }
        public Task CompleteTask(string UserIDN)
        {
            //register on db that task is completed
            throw new NotImplementedException();
        }

        public async Task<OPNProductHandlingTask> CreateRandomProductHandlingTask(TaskRequest request)
        {
            //get a random unhandled product

            var products = await _handler.GetRemainingProducts();
            Random random = new Random();
            Product taskProduct = products[random.Next(0, products.Count)];

            //create task

            var institutionWithProportion = taskProduct.GetRandomInstitutionWithProportion();

            var task = new OPNProductHandlingTask(
                                            request.LoggedUserIDN,
                                            taskProduct,
                                            institutionWithProportion
                                        );

            //register task on db

            await _handler.RegisterTask(task);

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
