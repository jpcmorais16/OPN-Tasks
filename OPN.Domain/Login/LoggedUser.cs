using OPN.Domain.Tasks;

namespace OPN.Domain.Login;
public class LoggedUser
{
    public string Name { get; set; }
    public string Idn { get; set; }
    public OPNProductHandlingTask? Task { get; set; }
    public int TaskId { get; set; }
    public int CompletedTasks { get; set; }
    public int CancelledTasks { get; set; }

    public void AddTask(OPNProductHandlingTask task)
    {
        if (Task != null)
            throw new Exception("Este usuário já possui uma task ativa!");

        Task = task;
    }

    public OPNProductHandlingTask CompleteTask()
    {
        if (Task == null)
            throw new Exception("Este usuário não possui uma task ativa!");

        CompletedTasks += 1;
        
        var task = Task;
        
        Task = null;
        
        task.ConclusionTime = DateTime.Now;

        return task;
    }

    public OPNProductHandlingTask CancelTask()
    {
        if (Task == null)
            throw new Exception("Este usuário não possui uma task ativa!");
        
        CancelledTasks += 1;

        var task = Task;

        task.Status = ETaskStatus.Cancelled;

        Task = null;

        task.CancelTime = DateTime.UtcNow;

        return task;
    }
}
