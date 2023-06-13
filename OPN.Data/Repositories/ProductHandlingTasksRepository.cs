using Microsoft.EntityFrameworkCore;
using OPN.Domain.Repositories;
using OPN.Domain.Tasks;

namespace OPN.Data.Repositories;

public class ProductHandlingTasksRepository: IProductHandlingTasksRepository
{
    private readonly ApplicationContext _context;
    
    public ProductHandlingTasksRepository(ApplicationContext context)
    {
        _context = context;
    }
    public async Task RegisterTaskAsync(OPNProductHandlingTask task)
    {
        await _context.ProductHandlingTasks.AddAsync(task);
    }

    public async Task<OPNTask> GetByIdAsync(int id)
    {
        return await _context.ProductHandlingTasks.FirstAsync(t => t.Id == id);
    }

    public Task DeleteAsync(OPNTask task)
    {
        throw new NotImplementedException();
    }
}