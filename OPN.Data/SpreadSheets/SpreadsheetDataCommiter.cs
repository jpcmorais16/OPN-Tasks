using OPN.Data.SpreadSheets.Interfaces;
using OPN.Domain.Interfaces;
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
        private readonly string _spreadsheetId = "16x8We-oqLJOZdm_seunG283Ki5AOKb0UN_CZnnP_Nsw";
        public SpreadsheetDataCommiter(ISpreadsheetConnection connection)
        {
            _connection = connection;
        }

        public void Commit(string goal, int id, string userIDN, DateTime creationTime, string institutionName, string productName, int productId)
        {
            
            string page = "Tasks";

            List<string> values = new List<string>
            {
                goal, productName, productId.ToString(), institutionName, creationTime.ToString(), id.ToString(), userIDN
            };

            _connection.AppendRowToSpreadsheet(_spreadsheetId, page, values);
        }

        public void RegisterNewUser(string idn)
        {
            string page = "Usuários";

            List<string> values = new List<string> { idn };

            _connection.AppendRowToSpreadsheet(_spreadsheetId, page, values);
        }
    }
}
