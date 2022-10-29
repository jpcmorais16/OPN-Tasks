using OPN.Domain.Login;
using OPN.Domain.Tasks;
using OPN.Services.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPN.Services.Interfaces
{
    public interface ITaskService
    {
        public OPNProductHandlingTask CreateRandomProductHandlingTask(TaskRequest request);
        public Task CompleteTask(string UserIDN);
        public Task<List<OPNProductHandlingTask>> GetLoggedUserCompletedTasks();
        public Task<List<OPNProductHandlingTask>> GetAllCompletedTasks();
        public Task<int> GetLoggedUserNumberOfCompletedTasks();
        public Task<int> GetNumberOfCompletedTasks();

    }
}
