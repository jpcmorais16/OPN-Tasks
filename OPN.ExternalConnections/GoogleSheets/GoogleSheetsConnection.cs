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
        private SheetsService _sheetsService3;
        private List<SheetsService> _sheetsServiceList = new List<SheetsService>();
        private Tuple<int, int> _baseRange;

        public GoogleSheetsConnection(IConfiguration configuration)
        {        
            GoogleSheetsServiceAccountCredentials credentials1 = configuration.GetSection("acc1").Get<GoogleSheetsServiceAccountCredentials>();
            GoogleSheetsServiceAccountCredentials credentials2 = configuration.GetSection("acc2").Get<GoogleSheetsServiceAccountCredentials>();
            GoogleSheetsServiceAccountCredentials credentials3 = configuration.GetSection("acc3").Get<GoogleSheetsServiceAccountCredentials>();

            var xCred1 = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(credentials1.client_email)
            {
                Scopes = new[] {
                SheetsService.Scope.Spreadsheets
            }
            }.FromPrivateKey(credentials1.private_key));

            var xCred2 = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(credentials2.client_email)
            {
                Scopes = new[] {
                SheetsService.Scope.Spreadsheets
            }
            }.FromPrivateKey(credentials1.private_key));

           

            var xCred3 = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(credentials3.client_email)
            {
                Scopes = new[] {
                SheetsService.Scope.Spreadsheets
            }
            }.FromPrivateKey(credentials1.private_key));


            _sheetsService1 = new SheetsService(
                new BaseClientService.Initializer()
                {
                    HttpClientInitializer = xCred1,
                }
            );
            _sheetsService2 = new SheetsService(
                new BaseClientService.Initializer()
                {
                    HttpClientInitializer = xCred2,
                }
            );
            _sheetsService3 = new SheetsService(
                new BaseClientService.Initializer()
                {
                    HttpClientInitializer = xCred3,
                }
            );

            _sheetsServiceList.Add(_sheetsService1);
            _sheetsServiceList.Add(_sheetsService2);
            _sheetsServiceList.Add(_sheetsService3);


            _baseRange = new Tuple<int, int>(200, 15);
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
            //var request1 = _sheetsService1.Spreadsheets.Values.Get(spreadsheetId,
            //    SpreadsheetRangeHelper.GetReadRequestRange(1, _baseRange.Item1, 1, _baseRange.Item2, page));
            //var request2 = _sheetsService2.Spreadsheets.Values.Get(spreadsheetId,
            //    SpreadsheetRangeHelper.GetReadRequestRange(1, _baseRange.Item1, 1, _baseRange.Item2, page));


            //try
            //{
            //    response = request1.Execute();
            //}

            //catch
            //{

            //    response = request2.Execute();
            //}

            ValueRange? response = null;

            foreach(var service in _sheetsServiceList)
            {
                try
                {
                    response = service.Spreadsheets.Values.Get(spreadsheetId,
                    SpreadsheetRangeHelper.GetReadRequestRange(1, _baseRange.Item1, 1, _baseRange.Item2, page)).Execute();

                    break;
                }
                catch
                {
                    continue;
                }
            }

            if (response == null)
                throw new Exception("Muitas requests!");

            return response; 
        }

        private void MakeAppendRequest(string spreadsheetId, ValueRange valuerange, string range)
        {
            //var appendRequest = _sheetsService1.Spreadsheets.Values.Append(valuerange, spreadsheetId, range);
            //appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;

            //try
            //{      
            //     appendRequest.Execute();
            //}
            //catch
            //{
            //    Thread.Sleep(60000);
            //    appendRequest.Execute();

            //}
            AppendValuesResponse? response = null;

            foreach (var service in _sheetsServiceList)
            {
                try
                {
                    var appendRequest = service.Spreadsheets.Values.Append(valuerange, spreadsheetId, range);
                    appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;

                    response = appendRequest.Execute();

                    break;
                }
                catch
                {
                    continue;
                }
            }

            if (response == null)
                throw new Exception("Muitas requests!");
        }

        private void MakeUpdateRequest(string spreadsheetId, ValueRange valuerange, string range)
        {
            //var updateRequest = _sheetsService1.Spreadsheets.Values.Update(valuerange, spreadsheetId, range);
            //updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;

            //try
            //{
            //    updateRequest.Execute();
            //}
            //catch
            //{
            //    Thread.Sleep(60000);
            //    updateRequest.Execute();
            //}

            UpdateValuesResponse? response = null;

            foreach (var service in _sheetsServiceList)
            {
                try
                {
                    var updateRequest = service.Spreadsheets.Values.Update(valuerange, spreadsheetId, range);
                    updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;

                    response = updateRequest.Execute();

                    break;
                }
                catch
                {
                    continue;
                }
            }

            if (response == null)
                throw new Exception("Muitas requests!");

        }
    }
}
