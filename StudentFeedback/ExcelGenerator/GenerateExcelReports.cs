using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using System.Data;
using System.Text.RegularExpressions;

namespace ITM.ExcelGenerator
{
    public class GenerateExcelReports
    {

        public void Create(string fileName, System.Data.DataTable[] tables, string sheetName, List<int> size = null)
        {
            try
            {
                using (SpreadsheetDocument myWorkbook = SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook))
                {
                    WorkbookPart workbookPart = myWorkbook.AddWorkbookPart();
                    WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();

                    // Create Styles and Insert into Workbook
                    WorkbookStylesPart stylesPart = myWorkbook.WorkbookPart.AddNewPart<WorkbookStylesPart>();
                    Stylesheet styles = new CustomStylesheet();
                    styles.Save(stylesPart);

                    string relId = workbookPart.GetIdOfPart(worksheetPart);

                    Workbook workbook = new Workbook();
                    FileVersion fileVersion = new FileVersion { ApplicationName = "Microsoft Office Excel" };

                    Worksheet worksheet = new Worksheet();

                    int columnCount;

                    SheetData sheetData = CreateSheetData(tables, stylesPart, out columnCount);

                    int numCols = columnCount;//headerNames.Count;
                    int width = 50;//headerNames.Max(h => h.Length) + 5;


                    Columns columns = new Columns();

                    for (int col = 0; col < numCols; col++)
                    {
                        if (size != null)
                            width = size[col];
                        Column c = new ExcelFacade().CreateColumnData((UInt32)col + 1, (UInt32)numCols + 1, width);

                        columns.Append(c);
                    }

                    worksheet.Append(columns);


                    Sheets sheets = new Sheets();
                    Sheet sheet = new Sheet { Name = sheetName, SheetId = 1, Id = relId };
                    sheets.Append(sheet);
                    workbook.Append(fileVersion);
                    workbook.Append(sheets);

                    worksheet.Append(sheetData);
                    worksheetPart.Worksheet = worksheet;

                    worksheetPart.Worksheet.Save();

                    MergeCells mergeCells;
                    if (worksheet.Elements<MergeCells>().Count() > 0)
                    {
                        mergeCells = sheet.Elements<MergeCells>().First();
                    }
                    else
                    {
                        mergeCells = new MergeCells();

                        // Insert a MergeCells object into the specified position.
                        if (worksheet.Elements<CustomSheetView>().Count() > 0)
                        {
                            worksheet.InsertAfter(mergeCells, worksheet.Elements<CustomSheetView>().First());
                        }
                        else if (sheet.Elements<DataConsolidate>().Count() > 0)
                        {
                            worksheet.InsertAfter(mergeCells, worksheet.Elements<DataConsolidate>().First());
                        }
                        else if (sheet.Elements<SortState>().Count() > 0)
                        {
                            worksheet.InsertAfter(mergeCells, worksheet.Elements<SortState>().First());
                        }
                        else if (sheet.Elements<AutoFilter>().Count() > 0)
                        {
                            worksheet.InsertAfter(mergeCells, worksheet.Elements<AutoFilter>().First());
                        }
                        else if (sheet.Elements<Scenarios>().Count() > 0)
                        {
                            worksheet.InsertAfter(mergeCells, worksheet.Elements<Scenarios>().First());
                        }
                        else if (sheet.Elements<ProtectedRanges>().Count() > 0)
                        {
                            worksheet.InsertAfter(mergeCells, worksheet.Elements<ProtectedRanges>().First());
                        }
                        else if (sheet.Elements<SheetProtection>().Count() > 0)
                        {
                            worksheet.InsertAfter(mergeCells, worksheet.Elements<SheetProtection>().First());
                        }
                        else if (sheet.Elements<SheetCalculationProperties>().Count() > 0)
                        {
                            worksheet.InsertAfter(mergeCells, worksheet.Elements<SheetCalculationProperties>().First());
                        }
                        else
                        {
                            worksheet.InsertAfter(mergeCells, worksheet.Elements<SheetData>().First());
                        }
                    }
                    MergeCell mergeCell = new MergeCell() { Reference = new StringValue("A1:B1") };
                    mergeCells.Append(mergeCell);
                    MergeCell mergeCell2 = new MergeCell() { Reference = new StringValue("A2:B2") };
                    mergeCells.Append(mergeCell2);
                    MergeCell mergeCell3 = new MergeCell() { Reference = new StringValue("A3:B3") };
                    mergeCells.Append(mergeCell3);
                    MergeCell mergeCell4 = new MergeCell() { Reference = new StringValue("A4:B4") };
                    mergeCells.Append(mergeCell4);
                    worksheet.Save();

                    myWorkbook.WorkbookPart.Workbook = workbook;
                    myWorkbook.WorkbookPart.Workbook.Save();
                    myWorkbook.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private SheetData CreateSheetData(DataTable[] objects, WorkbookStylesPart stylesPart, out int totalColumn)
        {
            SheetData sheetData = new SheetData();

            DocumentFormat.OpenXml.Spreadsheet.Row newRow1 = new DocumentFormat.OpenXml.Spreadsheet.Row();
            DocumentFormat.OpenXml.Spreadsheet.Cell cell1 = new DocumentFormat.OpenXml.Spreadsheet.Cell();
            cell1.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
            cell1.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("MANIBEN NANAVATI WOMEN'S COLLEGE");
            newRow1.AppendChild(cell1);
            sheetData.AppendChild(newRow1);

            DocumentFormat.OpenXml.Spreadsheet.Row newRow2 = new DocumentFormat.OpenXml.Spreadsheet.Row();
            DocumentFormat.OpenXml.Spreadsheet.Cell cell2 = new DocumentFormat.OpenXml.Spreadsheet.Cell();
            cell2.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
            cell2.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("TEACHING EFFECTIVENESS SCALE");
            newRow2.AppendChild(cell2);
            sheetData.AppendChild(newRow2);

            DocumentFormat.OpenXml.Spreadsheet.Row newRow3 = new DocumentFormat.OpenXml.Spreadsheet.Row();
            DocumentFormat.OpenXml.Spreadsheet.Cell cell3 = new DocumentFormat.OpenXml.Spreadsheet.Cell();
            cell3.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
            cell3.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("YEAR 2016-17");
            newRow3.AppendChild(cell3);
            sheetData.AppendChild(newRow3);

            DocumentFormat.OpenXml.Spreadsheet.Row newRow4 = new DocumentFormat.OpenXml.Spreadsheet.Row();
            DocumentFormat.OpenXml.Spreadsheet.Cell cell4 = new DocumentFormat.OpenXml.Spreadsheet.Cell();
            cell4.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
            cell4.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("TEACHER'S CODE : BF 2");
            newRow4.AppendChild(cell4);
            sheetData.AppendChild(newRow4);

            DocumentFormat.OpenXml.Spreadsheet.Row newRow5 = new DocumentFormat.OpenXml.Spreadsheet.Row();
            DocumentFormat.OpenXml.Spreadsheet.Cell cell5 = new DocumentFormat.OpenXml.Spreadsheet.Cell();
            cell5.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
            cell5.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("");
            newRow5.AppendChild(cell5);
            sheetData.AppendChild(newRow5);


            totalColumn = 0;
            try
            {
                if (objects != null)
                {
                    foreach (DataTable table in objects)
                    {
                        int columnCount = table.Columns.Count;

                        totalColumn = columnCount;

                        /* Get A to Z Alfabets */
                        var az = new List<Char>(Enumerable.Range('A', 'Z' - 'A' + 1).Select(i => (Char)i).ToArray());

                        /* Get Range of colums like A, AC(excel columns) */
                        List<string> headers = new ExcelFacade().GetRange(az, columnCount);
                        int numCols = columnCount;

                        int numRows = table.Rows.Count;

                        /* Create row object */
                        Row header = new Row();
                        int index = 1;
                        header.RowIndex = (uint)index;

                        /* Set Height to row */
                        header.Height = 20;
                        header.CustomHeight = true;

                        /* Code for user */
                        int row = 0;
                        foreach (DataColumn sHeader in table.Columns)
                        {
                            if (!sHeader.ColumnName.Equals("ENUMDESCRIPTION"))
                            {
                                HeaderCell cc = new HeaderCell(headers[row].ToString(), sHeader.ColumnName, index, stylesPart.Stylesheet, System.Drawing.Color.SkyBlue, 12, true, false);
                                header.Append(cc);
                                row++;
                            }
                        }
                        sheetData.Append(header);
                        // new data
                        foreach (DataRow oUserMaster in table.Rows)
                        {
                            index++;
                            var r1 = new Row { RowIndex = (uint)index };
                            row = 0;
                            foreach (DataColumn dc in table.Columns)
                            {
                                // write user frist-name into file                            
                                long val;
                                double valDouble;

                                Cell cell;

                                if (long.TryParse(oUserMaster[dc.ColumnName].ToString(), out val))
                                {
                                    cell = new NumberCell(headers[row].ToString(), oUserMaster[dc.ColumnName].ToString(), index, stylesPart.Stylesheet);
                                }
                                else if (double.TryParse(oUserMaster[dc.ColumnName].ToString(), out valDouble))
                                {
                                    cell = new NumberCell(headers[row].ToString(), oUserMaster[dc.ColumnName].ToString(), index, stylesPart.Stylesheet);
                                }
                                else
                                {
                                    cell = new TextCell(headers[row].ToString(), oUserMaster[dc.ColumnName].ToString(), index, stylesPart.Stylesheet, System.Drawing.Color.White);
                                }

                                r1.Append(cell);
                                row++;
                            }

                            sheetData.Append(r1);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return sheetData;
        }
    }
}
