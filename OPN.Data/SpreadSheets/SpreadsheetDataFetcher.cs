using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Newtonsoft.Json;
using OPN.Data.SpreadSheets;
using OPN.Data.SpreadSheets.Interfaces;
using OPN.Domain;
using OPN.Domain.Login;
using OPN.Domain.Tasks;
using OPN.Services.Interfaces.DataInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPN.Data.GoogleSheets
{
    public class SpreadsheetDataFetcher : IProductHandlingTaskDataFetcher, IUserDataFetcher
    {
        private ISpreadsheetConnection _connection;
        List<Product>? productsList;
        private string _spreadsheetId;
        private List<LoggedUser> _users = new List<LoggedUser>();

        public SpreadsheetDataFetcher(ISpreadsheetConnection connection, string spreadsheetId)
        {
            _connection = connection;
            _spreadsheetId = spreadsheetId;
        }

        public int FetchNumberOfUsers()
        {
            string page = "Usuários";
            var columns = new List<string> { "IDN" };

            var columnsDic = _connection.GetColumnsFromSpreadsheet(_spreadsheetId, page, columns);

            return columnsDic["IDN"].Count;

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
            var columns = new List<string> { "Itens", "Lar", "Recanto", "Quantidade"};

            var columnsDic = _connection.GetColumnsFromSpreadsheet(_spreadsheetId, page, columns);

            var result = new List<Product>();

            for(int i=0; i < columnsDic["Itens"].Count; i++)
            {
                var proportionsDic = new Dictionary<string, int>
                {
                    { "Lar", Convert.ToInt32(columnsDic["Lar"][i]) },
                    { "Recanto", Convert.ToInt32(columnsDic["Recanto"][i]) },
                };
                result.Add(new Product(i, columnsDic["Itens"][i], proportionsDic, Convert.ToInt32(columnsDic["Quantidade"][i])));
            }

            return result;
             
        }

        public LoggedUser FetchUser(string idn)
        {
            foreach(LoggedUser user in _users)
            {
                if (user.IDN.Equals(idn))
                {
                    return user;
                }
            }
            string page = "Usuários";
            var columns = new List<string> { "IDN", "Task", "Id da Task" };

            var columnsDic = _connection.GetColumnsFromSpreadsheet(_spreadsheetId, page, columns);

            for(int i = 0; i < columnsDic["IDN"].Count; i++)
            {
                if (columnsDic["IDN"][i].Equals(idn))
                {
                    LoggedUser user = new LoggedUser
                    {
                        IDN = idn,
                        ID = (i + 1),
                        _userDataCommiter = new SpreadsheetDataCommiter(_connection, _spreadsheetId)
                    };
                    try
                    {
                        user.TaskGoal = columnsDic["Task"][i];
                        user.TaskId = Convert.ToInt32(columnsDic["Id da Task"][i]);
                    }
                    catch
                    {
                        _users.Add(user);
                        return user;
                    }
                    _users.Add(user);
                    return user;                      
                }
            } 
            return null;
        }
    }
}
