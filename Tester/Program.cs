using OPN.Data.GoogleSheets;
using OPN.Data.SpreadSheets;
using OPN.ExternalConnections.GoogleSheets;
using OPN.Services;
using OPN.Services.Requests;

var connection = new GoogleSheetsConnection(@"C:\Users\Trilogo\Desktop\credentials\credentials.json", 99, 5);

var x = new TaskService(new SpreadsheetDataFetcher(connection, "16x8We-oqLJOZdm_seunG283Ki5AOKb0UN_CZnnP_Nsw"),
                                  new SpreadsheetDataCommiter(connection));



while (true)
{
    //Console.WriteLine(x.CreateRandomProductHandlingTask(new TaskRequest()).Goal);
    x.CreateRandomProductHandlingTask(new TaskRequest());
    Thread.Sleep(2000);
}