using OPN.Domain.Login;

namespace OPN.Domain.Repositories;

public interface IUserRepository
{
    void UpdateUser(LoggedUser user);
    Task<LoggedUser?> GetByIdn(string loggedUserIDN);
    Task<LoggedUser> CreateUser(string iDN, string userName);
}
