﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPN.ExternalConnections.GoogleSheets
{
    public static class SpreadsheetRangeHelper
    {
        public static string GetReadRequestRange(int firstRow, int lastRow, int firstColumn, int lastColumn, string page)
        {
            char charFirstColumn = (char)(firstColumn + 'A' - 1);
            char charLastColumn = (char)(lastColumn + 'A' - 1);

            return page + "!" + charFirstColumn.ToString() + firstRow.ToString() + ":" + charLastColumn.ToString() + lastRow.ToString();
                
        }

        public static string GetAppendRequestRange(int firstColumn, int lastColumn, string page)
        {
            char charFirstColumn = (char)(firstColumn + 'A' - 1);
            char charLastColumn = (char)(lastColumn + 'A' - 1);

            return page + "!" + charFirstColumn.ToString() + ":" + charLastColumn.ToString();
        }

        public static string GetColumnUpdateRequestRange(int row, int column, string page)
        {
            char charColumn = (char)(column + 'A' - 1);

            return page + "!" + charColumn + row.ToString() + ":" + charColumn.ToString() + row.ToString();
        }
    }
}
