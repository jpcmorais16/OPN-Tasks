using OPN.Domain.Login;

namespace OPN.Domain.Repositories;

public interface IUserRepository
{
    Task UpdateUserAsync(LoggedUser user);
    Task<LoggedUser> GetByIdnAsync(string loggedUserIDN);
}
