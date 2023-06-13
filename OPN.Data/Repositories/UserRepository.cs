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
        var user = new LoggedUser() { IDN = idn, UserName = userName };

        await _context.LoggedUsers.AddAsync(user);

        return user;
    }

    public async Task<LoggedUser?> GetByIdn(string loggedUserIDN)
    {
        //return await _context.LoggedUsers.FirstOrDefaultAsync(u => u.IDN == loggedUserIDN)!;

        var users = await _context.LoggedUsers.ToListAsync();
        return users.FirstOrDefault(u => u.IDN == loggedUserIDN);
    }

    public void UpdateUser(LoggedUser user)
    {
        _context.LoggedUsers.Update(user);
    }
}
