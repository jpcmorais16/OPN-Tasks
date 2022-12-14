using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPN.Data.SpreadSheets.Interfaces
{
    public interface ISpreadsheetConnection
    {
        public Dictionary<string, List<string>> GetColumnsFromSpreadsheet(string spreadsheetId, string page, List<string> columns );
        public void AppendRowToSpreadsheet(string spreadsheetId, string page, List<string> valuesToAppend);
        public void UpdateSingleCell(string spreadsheetId, string page, int column, int row, string value);
    }
}
