using Microsoft.EntityFrameworkCore;
using OPN.Domain.Login;
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

    public async Task<OPNProductHandlingTask?> GetCurrentTask(string idn)
    {
        return await _context.ProductHandlingTasks.FirstOrDefaultAsync(t => t.UserIDN == idn && t.Status == ETaskStatus.InExecution);
    }

    public async Task<List<OPNProductHandlingTask>> GetUserCompletedTasks(string idn)
    {
        return await _context.ProductHandlingTasks.Where(t => t.UserIDN == idn && t.Status == ETaskStatus.Completed).ToListAsync();
    }

    public async Task<List<OPNProductHandlingTask>> GetAllCompletedTasks()
    {
        return await _context.ProductHandlingTasks.Where(p => p.Status == ETaskStatus.Completed)
            .ToListAsync();
    }
    public async Task<OPNProductHandlingTask> GetByIdAsync(int id)
    {
        return await _context.ProductHandlingTasks.FirstAsync(t => t.Id == id);
    }
}