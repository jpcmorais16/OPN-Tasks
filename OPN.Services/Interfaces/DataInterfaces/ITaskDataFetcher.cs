using OPN.Domain;
using OPN.Domain.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPN.Services.Interfaces.DataInterfaces
{
    public interface ITaskDataFetcher
    {
        public List<Product> FetchProducts();
        public List<OPNProductHandlingTask> FetchProductHandlingTasks();
    }
}
