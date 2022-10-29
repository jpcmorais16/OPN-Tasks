using OPN.Data.GoogleSheets;
using OPN.Data.SpreadSheets;
using OPN.ExternalConnections.GoogleSheets;
using OPN.Services;
using OPN.Services.Requests;

var connection = new GoogleSheetsConnection(@"C:\Users\Trilogo\Desktop\credentials\credentials.json", 99, 5);

var x = new TaskService(new SpreadsheetDataFetcher(connection),
                                  new SpreadsheetDataCommiter(connection));



while (true)
{
    Console.WriteLine(x.CreateRandomProductHandlingTask(new TaskRequest()).Goal);
    Thread.Sleep(1000);
}