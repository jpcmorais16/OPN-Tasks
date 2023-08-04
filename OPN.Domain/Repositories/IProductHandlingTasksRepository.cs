using OPN.Domain.Tasks;

namespace OPN.Domain.Repositories;

public interface IProductHandlingTasksRepository: ITasksRepository
{
    Task RegisterTaskAsync(OPNProductHandlingTask task);
    Task<OPNProductHandlingTask?> GetCurrentTask(string idn);
    Task<List<OPNProductHandlingTask>> GetUserCompletedTasks(string idn);
    Task<List<OPNProductHandlingTask>> GetAllCompletedTasks();
}
