﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPN.Domain.Interfaces
{
    public interface IUserDataCommiter
    {
        void RegisterNewUser(string iDN);
    }
}