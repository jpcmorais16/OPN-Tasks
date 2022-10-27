﻿using OPN.Domain;
using OPN.Domain.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPN.Services.Interfaces.DataInterfaces
{
    public interface ITaskDataHandler
    {
        public Task<List<Product>> GetRemainingProducts();
        public Task RegisterTask(OPNTask task);
    }
}
