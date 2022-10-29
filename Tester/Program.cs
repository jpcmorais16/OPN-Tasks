using OPN.Data.GoogleSheets;
using OPN.ExternalConnections.GoogleSheets;
using OPN.Services;
using OPN.Services.Requests;

var x = new TaskService(new SpreadsheetDataHandler(new GoogleSheetsConnection(@"C:\Users\Trilogo\Desktop\credentials\credentials.json", 99, 5)));



var a = x.CreateRandomProductHandlingTask(new TaskRequest());


while (true)
{
    Console.WriteLine(x.CreateRandomProductHandlingTask(new TaskRequest()).Goal);
    Thread.Sleep(1000);
}