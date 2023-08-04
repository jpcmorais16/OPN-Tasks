using OPN.Domain.Tasks;

namespace OPN.Domain.Repositories;

public interface ITasksRepository
{
    Task<OPNProductHandlingTask> GetByIdAsync(int id);
}
