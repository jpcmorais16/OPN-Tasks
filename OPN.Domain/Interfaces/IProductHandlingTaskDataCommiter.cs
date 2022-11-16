using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPN.Domain.Interfaces
{
    public interface IProductHandlingTaskDataCommiter
    {
        void CommitTask(string goal, int id, string userIDN, string creationTime, string institutionName, string productName, int productId, int quantity, int proportion, string conclusionTime);
        void UpdateTaskIDN(int id, string loggedUserIDN);
    }
}
