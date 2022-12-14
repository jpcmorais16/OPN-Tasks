using OPN.Data.SpreadSheets.Interfaces;
using OPN.Domain.Interfaces;
using OPN.Domain.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPN.Data.SpreadSheets
{
    public class SpreadsheetDataCommiter : IProductHandlingTaskDataCommiter, IUserDataCommiter
    {
        ISpreadsheetConnection _connection;
        private readonly string _spreadsheetId;
        public SpreadsheetDataCommiter(ISpreadsheetConnection connection, string spreadsheetId)
        {
            _connection = connection;
            _spreadsheetId = spreadsheetId;
        }

        public void CommitTask(string goal, int id, string userIDN, string creationTime, string institutionName, string productName, int productId, int quantity, int proportion, string cancellationTime)
        {
            
            string page = "Tasks";

            List<string> values = new List<string>
            {
                goal, productName, productId.ToString(), institutionName, creationTime, id.ToString(), userIDN, "nulo", quantity.ToString(), proportion.ToString(), cancellationTime
            };

            _connection.AppendRowToSpreadsheet(_spreadsheetId, page, values);
        }

        public LoggedUser RegisterNewUser(string idn, string id)
        {
            string page = "Usuários";

            List<string> values = new List<string> { idn, id, "nulo", "nulo", "0" };

            _connection.AppendRowToSpreadsheet(_spreadsheetId, page, values);

            return new LoggedUser
            {
                ID = Convert.ToInt32(id),
                IDN = idn
            };
        }

        public void AddTaskToUser(int userId, string taskGoal, int taskId)
        {
            var page = "Usuários";
            _connection.UpdateSingleCell(_spreadsheetId,  page, 3, Convert.ToInt32(userId) + 1, taskGoal);
            _connection.UpdateSingleCell(_spreadsheetId, page, 4, Convert.ToInt32(userId) + 1, taskId.ToString());
        }

        public void CompleteTaskFromUser(int userId, int? taskId, int completedTasksFromUser)
        {
            string page1 = "Usuários";
            _connection.UpdateSingleCell(_spreadsheetId, page1, 3, Convert.ToInt32(userId) + 1, "nulo");
            _connection.UpdateSingleCell(_spreadsheetId, page1, 4, Convert.ToInt32(userId) + 1, "nulo");
            _connection.UpdateSingleCell(_spreadsheetId, page1, 5, Convert.ToInt32(userId) + 1, (completedTasksFromUser + 1).ToString()); 

            string page2 = "Tasks";
            _connection.UpdateSingleCell(_spreadsheetId, page2, 8, Convert.ToInt32(taskId) + 1, DateTime.Now.ToLocalTime().ToString());
        }

        public void CancelTaskFromUser(int userId, int? taskId)
        {
            string page1 = "Usuários";
            _connection.UpdateSingleCell(_spreadsheetId, page1, 3, Convert.ToInt32(userId) + 1, "nulo");
            _connection.UpdateSingleCell(_spreadsheetId, page1, 4, Convert.ToInt32(userId) + 1, "nulo");

            string page2 = "Tasks";
            _connection.UpdateSingleCell(_spreadsheetId, page2,7, Convert.ToInt32(taskId) + 1, "nulo");
            _connection.UpdateSingleCell(_spreadsheetId, page2, 11, Convert.ToInt32(taskId) + 1, DateTime.UtcNow.ToString());
        }

        public void UpdateTaskIDN(int taskId, string loggedUserIDN)
        {
            string page = "Tasks";
            _connection.UpdateSingleCell(_spreadsheetId, page, 7, Convert.ToInt32(taskId) + 1, loggedUserIDN);
            _connection.UpdateSingleCell(_spreadsheetId, page, 11, Convert.ToInt32(taskId) + 1, DateTime.UtcNow.ToString());
        }
    }
}
