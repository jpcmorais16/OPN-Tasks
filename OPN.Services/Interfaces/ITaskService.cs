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
        Task<OPNProductHandlingTask> CreateRandomProductHandlingTask(string idn);
        Task<List<OPNProductHandlingTask>> GetUserCompletedTasks(string idn);
        List<OPNProductHandlingTask> GetAllCompletedTasks();
        int GetUserNumberOfCompletedTasks(string idn);
        int GetNumberOfCompletedTasks();
        Task CompleteTask(string idn);
        Task CancelTask(string idn);
        List<LoggedUser> GetRanking();
        Task<OPNProductHandlingTask?> GetUserCurrentTask(string idn);
    }
}
