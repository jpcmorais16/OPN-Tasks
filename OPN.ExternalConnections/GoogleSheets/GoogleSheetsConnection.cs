using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OPN.Data.SpreadSheets.Interfaces;
using OPN.ExternalConnections.GoogleSheets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPN.ExternalConnections.GoogleSheets
{
    public class GoogleSheetsConnection: ISpreadsheetConnection
    {
        private SheetsService _sheetsService1;
        private SheetsService _sheetsService2;
        private Tuple<int, int> _baseRange;

        public GoogleSheetsConnection(IConfiguration configuration)
        {        
            GoogleSheetsServiceAccountCredentials credentials = configuration.GetSection("Google").Get<GoogleSheetsServiceAccountCredentials>();

            var xCred = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(credentials.client_email)
            {
                Scopes = new[] {
                SheetsService.Scope.Spreadsheets
            }
            }.FromPrivateKey(credentials.private_key));

            _sheetsService1 = new SheetsService(
                new BaseClientService.Initializer()
                {
                    HttpClientInitializer = xCred,
                }
            );

            _baseRange = new Tuple<int, int>(200, 15);
        }

        public GoogleSheetsConnection(string credPath, int baseNumberOfRows, int baseNumberOfColumns)
        {
            var path = File.ReadAllText(credPath);
            GoogleSheetsServiceAccountCredentials credentials = JsonConvert.DeserializeObject<GoogleSheetsServiceAccountCredentials>(path);


            var xCred = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(credentials.client_email)
            {
                Scopes = new[] {
                SheetsService.Scope.Spreadsheets
            }
            }.FromPrivateKey(credentials.private_key));

            _sheetsService1 = new SheetsService(
                new BaseClientService.Initializer()
                {
                    HttpClientInitializer = xCred,
                }
            );

            _baseRange = new Tuple<int, int>(baseNumberOfRows, baseNumberOfColumns);

        }

        public Dictionary<string, List<string>> GetColumnsFromSpreadsheet(string spreadsheetId, string page, List<string> columns)
        {

            var response = MakeGetRequest(spreadsheetId, page);

            List<string> columnsFromSpreadsheet = response.Values[0].Select(o => o.ToString()).ToList();

            Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();
            
            foreach(var c in columns)
            {
                result.Add(c, new List<string>());
            }

            
            foreach (List<object> row in response.Values.Skip(1))
            {
                List<string> data = new List<string>();

                for(int i = 0; i < row.Count; i++)
                {
                    if (result.Keys.Contains(columnsFromSpreadsheet[i]))
                        result[columnsFromSpreadsheet[i]].Add(row[i].ToString());

                }

            }

            return result;
        }

        public void AppendRowToSpreadsheet(string spreadsheetId, string page, List<string> valuesToAppend)
        {

            List<object> list = new List<object>();

            foreach(string value in valuesToAppend)
            {
                list.Add(value);
            }

            var range = SpreadsheetRangeHelper.GetAppendRequestRange(1, valuesToAppend.Count, page);
            var valuerange = new ValueRange();
            valuerange.Values = new List<IList<object>>() { list };


            MakeAppendRequest(spreadsheetId, valuerange, range);

        }

        public void UpdateSingleCell(string spreadsheetId, string page, int column, int row, string value)
        {
            List<object> list = new List<object> { value };

            var valueRange = new ValueRange();
            valueRange.Values = new List<IList<object>>() { list };

            var range = SpreadsheetRangeHelper.GetColumnUpdateRequestRange(row, column, page);
            MakeUpdateRequest(spreadsheetId, valueRange, range);
        }

        private ValueRange MakeGetRequest(string spreadsheetId, string page)
        {
            var request = _sheetsService1.Spreadsheets.Values.Get(spreadsheetId,
                SpreadsheetRangeHelper.GetReadRequestRange(1, _baseRange.Item1, 1, _baseRange.Item2, page));

            ValueRange? response;

            try
            {
                response = request.Execute();
            }

            catch
            {
                Thread.Sleep(60000);
                response = request.Execute();
            }

            return response;
        }

        private void MakeAppendRequest(string spreadsheetId, ValueRange valuerange, string range)
        {
            var appendRequest = _sheetsService1.Spreadsheets.Values.Append(valuerange, spreadsheetId, range);
            appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;

            try
            {      
                 appendRequest.Execute();
            }
            catch
            {
                Thread.Sleep(60000);
                appendRequest.Execute();

            }
        }

        private void MakeUpdateRequest(string spreadsheetId, ValueRange valuerange, string range)
        {
            var updateRequest = _sheetsService1.Spreadsheets.Values.Update(valuerange, spreadsheetId, range);
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;

            try
            {
                updateRequest.Execute();
            }
            catch
            {
                Thread.Sleep(60000);
                updateRequest.Execute();
            }
            
        }
    }
}
