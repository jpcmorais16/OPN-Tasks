using OPN.Data.SpreadSheets.Interfaces;
using OPN.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPN.Data.SpreadSheets
{
    public class SpreadsheetDataCommiter : IProductHandlingTaskDataCommiter
    {
        ISpreadsheetConnection _connection;
        public SpreadsheetDataCommiter(ISpreadsheetConnection connection)
        {
            _connection = connection;
        }

        public void Commit(string goal, int id, string userIDN, DateTime creationTime, string institutionName, string productName, int productId)
        {
            string spreadsheetId = "16x8We-oqLJOZdm_seunG283Ki5AOKb0UN_CZnnP_Nsw";
            string page = "Tasks";

            List<string> values = new List<string>
            {
                goal, productName, productId.ToString(), institutionName, creationTime.ToString(), id.ToString(), userIDN
            };

            _connection.AppendRowToSpreadsheet(spreadsheetId, page, values);
        }
    }
}
