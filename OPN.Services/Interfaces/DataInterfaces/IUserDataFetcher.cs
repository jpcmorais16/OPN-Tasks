using OPN.Domain.Login;

namespace OPN.Services.Interfaces.DataInterfaces
{
    public interface IUserDataFetcher
    {
        LoggedUser FetchUser(string iDN);
        public int FetchNumberOfUsers();
        List<LoggedUser> FetchUsers();
    }
}