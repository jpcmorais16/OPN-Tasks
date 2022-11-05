using OPN.Domain.Interfaces;
using OPN.Domain.Login;
using OPN.Services.Interfaces;
using OPN.Services.Interfaces.DataInterfaces;
using OPN.Services.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPN.Services
{
    public class LoginService: ILoginService
    {
        private readonly IUserDataFetcher _fetcher;
        private readonly IUserDataCommiter _commiter;

        public LoginService(IUserDataFetcher fetcher, IUserDataCommiter commiter)
        {
            _fetcher = fetcher;
            _commiter = commiter;
        }

        public LoggedUser Login(LoginRequest request)
        {
            var user = _fetcher.FetchUser(request.IDN);

            if(user == null)
            {
                var id = _fetcher.FetchNumberOfUsers() + 1; 
                user = _commiter.RegisterNewUser(request.IDN, id.ToString());
            }
            return user;
        }

    }
}
