using OPN.Domain.Tasks;

namespace OPN.Domain.Repositories;

public interface IProductHandlingTasksRepository: ITasksRepository
{
    Task RegisterTaskAsync(OPNProductHandlingTask task);
}
