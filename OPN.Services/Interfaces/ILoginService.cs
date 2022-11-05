using OPN.Domain.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPN.Services.Interfaces
{
    public interface ILoginService
    {
        public LoggedUser Login(string IDN);
    }
}
