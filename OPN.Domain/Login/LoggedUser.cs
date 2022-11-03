using OPN.Domain.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPN.Domain.Login
{
    public class LoggedUser
    {
        public string IDN { get; set; }
        public string TaskGoal { get; set; }
        public int? TaskId { get; set; }

    }
}
