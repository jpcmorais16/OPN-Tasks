using Microsoft.EntityFrameworkCore;
using OPN.Domain.Login;
using OPN.Domain.Repositories;

namespace OPN.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationContext _context;
    public UserRepository(ApplicationContext context)
    {
        _context = context;
    }
    public async Task<LoggedUser> CreateUser(string idn, string userName)
    {
        var user = new LoggedUser() { Idn = idn, Name = userName };

        await _context.LoggedUsers.AddAsync(user);

        return user;
    }

    public async Task<List<LoggedUser>> GetRanking()
    {
        return await _context.LoggedUsers.OrderByDescending(p => p.CompletedTasks).ToListAsync();
    }

    public async Task<LoggedUser?> Login(string idn)
    {
        return await _context.LoggedUsers.FirstOrDefaultAsync(p => p.Idn == idn);
    }

    public async Task<LoggedUser> GetByIdn(string idn)
    {
        var user = await _context.LoggedUsers.Include(p => p.Task)
            .FirstOrDefaultAsync(u => u.Idn == idn);
        
        if (user == null)
            throw new Exception("Usuário não encontrado");
        
        return user;
    }
}
