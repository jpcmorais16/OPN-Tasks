using OPN.Data.GoogleSheets;
using OPN.Data.SpreadSheets;
using OPN.Data.SpreadSheets.Interfaces;
using OPN.ExternalConnections.GoogleSheets;
using OPN.Services.Interfaces.DataInterfaces;
namespace OPN.Api.Factories
{
    public static class CommiterFactory
    {
        private static readonly ISpreadsheetConnection _connection = new GoogleSheetsConnection(@"C:\Users\Trilogo\Desktop\credentials\credentials.json");
        private static readonly SpreadsheetDataCommiter _dataCommiter = new SpreadsheetDataCommiter(_connection, "16x8We-oqLJOZdm_seunG283Ki5AOKb0UN_CZnnP_Nsw");
        
        public static SpreadsheetDataCommiter GetSpreadsheetCommiter()
        {
            return _dataCommiter;
        }
    }
}
