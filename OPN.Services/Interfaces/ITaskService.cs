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
        Task CompleteTask(string idn);
        Task CancelTask(string idn);
        Task Reset();
        Task<OPNProductHandlingTask> GetTaskToUser(string idn);
    }
}
