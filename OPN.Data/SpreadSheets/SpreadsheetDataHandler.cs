using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Newtonsoft.Json;
using OPN.Data.SpreadSheets.Interfaces;
using OPN.Domain;
using OPN.Domain.Tasks;
using OPN.Services.Interfaces.DataInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPN.Data.GoogleSheets
{
    public class SpreadsheetDataHandler : ITaskDataHandler
    {
        private ISpreadsheetConnection _connection;
        public SpreadsheetDataHandler(ISpreadsheetConnection connection)
        {
            _connection = connection;
        }

        public Task<List<OPNProductHandlingTask>> GetProductHandlingTasks()
        {
            throw new NotImplementedException();
        }

        public List<Product> GetUnfinishedProducts()
        {
            string spreadsheetId = "16x8We-oqLJOZdm_seunG283Ki5AOKb0UN_CZnnP_Nsw";
            string page = "Proporções";
            var columns = new List<string> { "Itens", "Lar", "Recanto", "IsFinished"};

            var columnsDic = _connection.GetColumnsFromSpreadsheet(spreadsheetId, page, columns);

            var result = new List<Product>();

            for(int i=0; i < columnsDic["Itens"].Count; i++)
            {
                var proportionsDic = new Dictionary<string, int>
                {
                    { "Lar", Convert.ToInt32(columnsDic["Lar"][i]) },
                    { "Recanto", Convert.ToInt32(columnsDic["Recanto"][i]) }
                };

                

                result.Add(new Product(i, columnsDic["Itens"][i], proportionsDic)); //id only for testing
            }

            return result;
             
        }


        public Task RegisterTask(OPNTask task)
        {
            throw new NotImplementedException();
        }
    }
}
