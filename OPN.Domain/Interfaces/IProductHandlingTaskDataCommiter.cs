﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPN.Domain.Interfaces
{
    public interface IProductHandlingTaskDataCommiter
    {
        void Commit(string goal, string userIDN, DateTime creationTime, string institutionName, string productName);
    }
}
