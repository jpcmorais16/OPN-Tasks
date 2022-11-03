using OPN.Domain.Login;

namespace OPN.Services.Interfaces.DataInterfaces
{
    public interface IUserDataFetcher
    {
        LoggedUser FetchUser(string iDN);
    }
}