using Microsoft.EntityFrameworkCore;
using OPN.Domain.Login;
using OPN.Domain.Repositories;
using OPN.Domain.Tasks;

namespace OPN.Data.Repositories;

public class ProductHandlingTasksRepository: IProductHandlingTasksRepository
{
    private readonly ApplicationContext _context;
    private int? _totalTasks;
    
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
        return await _context.ProductHandlingTasks
            .Include(t => t.Product)
            .Include(t => t.Institution)
            .FirstOrDefaultAsync(t => t.UserIdn == idn && t.Status == ETaskStatus.InExecution);
    }

    public async Task<List<OPNProductHandlingTask>> GetUserCompletedTasks(string idn)
    {
        return await _context.ProductHandlingTasks.Where(t => t.UserIdn == idn && t.Status == ETaskStatus.Completed).ToListAsync();
    }

    public async Task<List<OPNProductHandlingTask>> GetAllCompletedTasks()
    {
        return await _context.ProductHandlingTasks.Where(p => p.Status == ETaskStatus.Completed)
            .ToListAsync();
    }

    public async Task<List<OPNProductHandlingTask>> GetActiveTasks()
    {
        return await _context.ProductHandlingTasks
            .Where(t => t.Status == ETaskStatus.InExecution)
            .ToListAsync();
    }
    
    public async Task<List<OPNProductHandlingTask>> GetWaitingTasks()
    {
        return await _context.ProductHandlingTasks
            .Where(t => t.Status == ETaskStatus.Waiting)
            .ToListAsync();
    }

    public async Task Reset()
    {
        var itemsToRemove = await _context.ProductHandlingTasks.ToListAsync();
        _context.ProductHandlingTasks.RemoveRange(itemsToRemove);
    }

    public async Task<OPNProductHandlingTask> GetRandomTask()
    {
        _totalTasks ??= await _context.ProductHandlingTasks.CountAsync();

        var index = new Random().Next(1, _totalTasks.Value);

        var result = await _context.ProductHandlingTasks.FirstOrDefaultAsync(t => t.Status == ETaskStatus.Waiting && t.Id == index);

        if (result is null)
            throw new Exception("Todas as tarefas já foram concluídas!");

        return result;
    }

    public async Task RegisterRangeAsync(List<OPNProductHandlingTask> tasks)
    {
        await _context.ProductHandlingTasks.AddRangeAsync(tasks);
    }

    public async Task<OPNProductHandlingTask> GetByIdAsync(int id)
    {
        return await _context.ProductHandlingTasks.FirstAsync(t => t.Id == id);
    }
}