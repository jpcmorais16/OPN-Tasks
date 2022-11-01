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
    public class SpreadsheetDataFetcher : ITaskDataFetcher
    {
        private ISpreadsheetConnection _connection;
        List<Product>? productsList;
        private string _spreadsheetId;
        public SpreadsheetDataFetcher(ISpreadsheetConnection connection, string spreadsheetId)
        {
            _connection = connection;
            _spreadsheetId = spreadsheetId;
        }

        public List<OPNProductHandlingTask> FetchProductHandlingTasks()
        {
            var page = "Tasks";
            var columns = new List<string> { "Id", "Produto", "Instituição", "Id do Produto" };

            var columnsDic = _connection.GetColumnsFromSpreadsheet(_spreadsheetId, page, columns);

            var result = new List<OPNProductHandlingTask>();

            if (columnsDic["Id"].Count == 0)
                return result;

            for(int i = 0; i < columnsDic["Id"].Count; i++)
            {
                var task = new OPNProductHandlingTask
                {
                    Id = Convert.ToInt32(columnsDic["Id"][i]),
                    Product = new Product
                    {
                        Name = columnsDic["Produto"][i],
                        Id = Convert.ToInt32(columnsDic["Id do Produto"][i])
                    },
                    InstitutionName = columnsDic["Instituição"][i]

                };
                result.Add(task);
            }

            return result;
        }

        public List<Product> FetchProducts()
        {
            if (productsList != null)
                return productsList;

            string page = "Proporções";
            var columns = new List<string> { "Itens", "Lar", "Recanto"};

            var columnsDic = _connection.GetColumnsFromSpreadsheet(_spreadsheetId, page, columns);

            var result = new List<Product>();

            for(int i=0; i < columnsDic["Itens"].Count; i++)
            {
                var proportionsDic = new Dictionary<string, int>
                {
                    { "Lar", Convert.ToInt32(columnsDic["Lar"][i]) },
                    { "Recanto", Convert.ToInt32(columnsDic["Recanto"][i]) }
                };

                

                result.Add(new Product(i, columnsDic["Itens"][i], proportionsDic));
            }

            return result;
             
        }

    }
}
