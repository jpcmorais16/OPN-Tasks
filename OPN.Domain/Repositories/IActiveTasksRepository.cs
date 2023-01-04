using OPN.Domain.Tasks;

namespace OPN.Domain.Repositories;

public interface IActiveTasksRepository
{
    Task RegisterTaskAsync(OPNTask task);
    Task<OPNTask> GetByIdAsync();
    Task DeleteAsync(OPNTask task);
}
