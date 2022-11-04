using OPN.Domain.Interfaces;
using OPN.Domain.Login;
using OPN.Services.Interfaces.DataInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPN.Services
{
    public class LoginService
    {
        private readonly IUserDataFetcher _fetcher;
        private readonly IUserDataCommiter _commiter;

        public LoginService(IUserDataFetcher fetcher, IUserDataCommiter commiter)
        {
            _fetcher = fetcher;
            _commiter = commiter;
        }

        public LoggedUser Login(string IDN)
        {
            var user = _fetcher.FetchUser(IDN);

            if(user == null)
            {
                var id = _fetcher.FetchNumberOfUsers() + 1; 
                user = _commiter.RegisterNewUser(IDN, id.ToString());
            }
            return user;
        }

    }
}
