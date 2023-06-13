using OPN.Domain.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPN.Domain.Login;
public class LoggedUser
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string IDN { get; set; }
    public OPNTask? Task { get; set; }
    public int TaskId { get; set; }
    public int CompletedTasks { get; set; }
    public int CancelledTasks { get; set; }

    public void AddTask(OPNTask task)
    {
        if (Task != null)
            throw new Exception("Este usuário já possui uma task ativa!");

        Task = task;
    }

    public void CompleteTask()
    {
        if (Task == null)
            throw new Exception("Este usuário não possui uma task ativa!");

        CompletedTasks += 1;

        Task = null;
    }

    public void CancelTask()
    {
        if (Task == null)
            throw new Exception("Este usuário não possui uma task ativa!");

        CancelledTasks += 1;

        Task = null;
    }
}
