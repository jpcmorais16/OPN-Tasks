using OPN.Domain.Login;

namespace OPN.Domain.Repositories;

public interface IUserRepository
{
    Task<LoggedUser?> GetByIdn(string idn);
    Task<LoggedUser> CreateUser(string idn, string userName);
    Task<List<LoggedUser>> GetRanking();
    Task<LoggedUser?> Login(string requestIdn);
    Task Reset();
}
