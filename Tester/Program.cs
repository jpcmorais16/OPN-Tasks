using OPN.Data.GoogleSheets;
using OPN.Data.SpreadSheets;
using OPN.Domain.Login;
using OPN.ExternalConnections.GoogleSheets;
using OPN.Services;
using OPN.Services.Interfaces.DataInterfaces;
using OPN.Services.Requests;

var connection = new GoogleSheetsConnection(@"C:\Users\Trilogo\Desktop\credentials\credentials.json", 200, 6);

var fetcher = new SpreadsheetDataFetcher(connection, "16x8We-oqLJOZdm_seunG283Ki5AOKb0UN_CZnnP_Nsw");

var x = new TaskService(fetcher,
                                  new SpreadsheetDataCommiter(connection, "16x8We-oqLJOZdm_seunG283Ki5AOKb0UN_CZnnP_Nsw"), new SpreadsheetDataFetcher(connection, "16x8We-oqLJOZdm_seunG283Ki5AOKb0UN_CZnnP_Nsw"), new SpreadsheetDataCommiter(connection, "16x8We-oqLJOZdm_seunG283Ki5AOKb0UN_CZnnP_Nsw"));
var y = new LoginService(fetcher,
                                  new SpreadsheetDataCommiter(connection, "16x8We-oqLJOZdm_seunG283Ki5AOKb0UN_CZnnP_Nsw"));



//connection.UpdateColumnAtRow("16x8We-oqLJOZdm_seunG283Ki5AOKb0UN_CZnnP_Nsw", "Usuários", 1, 2, "jonas" );



while (true)
{
    //Console.WriteLine(x.CreateRandomProductHandlingTask(new TaskRequest()).Goal);
    string idn = new Random().Next(0, 2000).ToString();
    var user = y.Login(idn);
    x.CreateRandomProductHandlingTask(new TaskRequest { LoggedUserIDN = idn});
    user = fetcher.FetchUser(idn);
    user.CompleteTask();

    Thread.Sleep(2000);
}