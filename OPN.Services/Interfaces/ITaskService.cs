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
        public List<OPNProductHandlingTask> GetUserCompletedTasks(string userIDN);
        public List<OPNProductHandlingTask> GetAllCompletedTasks();
        public int GetUserNumberOfCompletedTasks(string userIDN);
        public int GetNumberOfCompletedTasks();
        public void CompleteTask(string userIDN);

    }
}
