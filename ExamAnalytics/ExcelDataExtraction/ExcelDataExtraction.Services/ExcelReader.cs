using Spire.Xls;
using Spire.Xls.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExcelDataExtraction.Services
{
    public class ExcelReader : IDisposable
    {
        public Workbook Workbook { get; }
        public ExcelReader(string filePath)
        {
            Workbook = new Workbook();
            Workbook.LoadFromFile(filePath);
        }
        public List<IWorksheet> GetWorksheets(int startIndex, int lastIndex)
        {
            return Workbook.Worksheets.Where(x => x.Index >= startIndex && x.Index <= lastIndex).ToList();
        }
        /// <summary>
        /// Get data from string in excel file by number of column and row
        /// </summary>
        /// <param name="worksheetNumber"></param>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        public string GetDataStringByColumnAndRowIndexes(int worksheetNumber, int rowIndex, int columnIndex)
        {
            return Workbook.Worksheets[worksheetNumber].Rows[rowIndex].Columns[columnIndex].Text.Trim();
        }
        public void Dispose() => Workbook.Dispose();
    }
}
