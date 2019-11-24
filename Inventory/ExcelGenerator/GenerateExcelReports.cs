using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using System.Data;
using ITM.LogManager;
using System.Text.RegularExpressions;

namespace ITM.ExcelGenerator
{
    public class Item
    {
        public Item()
        {
            listOfCellReference = new List<string>();
        }

        public string Name { get; set; }

        public List<string> listOfCellReference { get; set; }

    }

    public class GenerateExcelReports2
    {
        Logger logger = new Logger();

        public void Create(string fileName, DataTable objects, string sheetName, string logPath, List<int> size = null)
        {
            try
            {
                logger.Debug("ExcelFacade.cs", "Create", "Getting passed parameter", logPath);
                //Open the copied template workbook. 
                // SpreadsheetDocument myWorkbook = SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook);
                logger.Debug("ExcelFacade.cs", "Create", "Creating SpreadsheetDocument name myworkbook", logPath);
                using (SpreadsheetDocument myWorkbook = SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook))
                {
                    logger.Debug("ExcelFacade.cs", "Create", "Creating workbook part and worksheet part", logPath);
                    WorkbookPart workbookPart = myWorkbook.AddWorkbookPart();
                    WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();

                    // Create Styles and Insert into Workbook
                    logger.Debug("ExcelFacade.cs", "Create", "Creating workbook style part", logPath);
                    WorkbookStylesPart stylesPart = myWorkbook.WorkbookPart.AddNewPart<WorkbookStylesPart>();
                    logger.Debug("ExcelFacade.cs", "Create", "Creating styles from customStylesheet", logPath);
                    Stylesheet styles = new CustomStylesheet();
                    logger.Debug("ExcelFacade.cs", "Create", "Calling Save function", logPath);
                    styles.Save(stylesPart);

                    logger.Debug("ExcelFacade.cs", "Create", "Getting Part Id from worksheet part", logPath);
                    string relId = workbookPart.GetIdOfPart(worksheetPart);

                    logger.Debug("ExcelFacade.cs", "Create", "Creating workbook", logPath);
                    Workbook workbook = new Workbook();
                    logger.Debug("ExcelFacade.cs", "Create", "Getting file versioin", logPath);
                    FileVersion fileVersion = new FileVersion { ApplicationName = "Microsoft Office Excel" };

                    logger.Debug("ExcelFacade.cs", "Create", "Creating worksheet", logPath);
                    Worksheet worksheet = new Worksheet();

                    int columnCount;

                    logger.Debug("ExcelFacade.cs", "Create", "Creating sheet data", logPath);
                    SheetData sheetData = CreateSheetData(objects, stylesPart, out columnCount);

                    logger.Debug("ExcelFacade.cs", "Create", "Getting column count", logPath);
                    int numCols = columnCount;//headerNames.Count;
                    int width = 50;//headerNames.Max(h => h.Length) + 5;


                    logger.Debug("ExcelFacade.cs", "Create", "Creating columns", logPath);
                    Columns columns = new Columns();

                    for (int col = 0; col < numCols; col++)
                    {
                        if (size != null)
                            width = size[col];
                        Column c = new ExcelFacade().CreateColumnData((UInt32)col + 1, (UInt32)numCols + 1, width);

                        columns.Append(c);
                    }

                    logger.Debug("ExcelFacade.cs", "Create", "Appending columns into the worksheet", logPath);
                    worksheet.Append(columns);


                    logger.Debug("ExcelFacade.cs", "Create", "Creating sheets", logPath);
                    Sheets sheets = new Sheets();
                    Sheet sheet = new Sheet { Name = sheetName, SheetId = 1, Id = relId };
                    logger.Debug("ExcelFacade.cs", "Create", "Appending sheet into the sheets", logPath);
                    sheets.Append(sheet);
                    logger.Debug("ExcelFacade.cs", "Create", "Appending fileVersion and sheets into the workbook", logPath);
                    workbook.Append(fileVersion);
                    workbook.Append(sheets);

                    logger.Debug("ExcelFacade.cs", "Create", "Appending sheetData into the worksheet", logPath);
                    worksheet.Append(sheetData);
                    worksheetPart.Worksheet = worksheet;

                    logger.Debug("ExcelFacade.cs", "Create", "successfully appended merged cells into the another merge cell", logPath);
                    logger.Debug("ExcelFacade.cs", "Create", "Calling save function to save worksheetpart.worksheet", logPath);
                    worksheetPart.Worksheet.Save();

                    List<Row> rows2 = sheetData.Descendants<Row>().ToList();
                    List<Item> items = new List<Item>();
                    for (int k = 2; k < rows2.Count(); k++)
                    {
                        Row r1 = rows2[k - 1];
                        Row r2 = rows2[k];
                        for (int m = 0; m < rows2[k].Descendants<Cell>().Count(); m++)
                        {
                            string ss1 = r2.Descendants<Cell>().ElementAt(m).InnerText;
                            string ss2 = r1.Descendants<Cell>().ElementAt(m).InnerText;

                            if (string.IsNullOrEmpty(ss1) || string.IsNullOrEmpty(ss2))
                                continue;


                            if (ss1.Equals(ss2))
                            {
                                string cn1 = GetColumnName(r1.Descendants<Cell>().ElementAt(m).CellReference);
                                string cn2 = GetColumnName(r2.Descendants<Cell>().ElementAt(m).CellReference);
                                List<string> cname = new List<string>() { "B" };
                                if (cname.Contains(cn1))
                                {
                                    if (items.Exists(a => a.Name.Equals(ss1)))
                                    {
                                        Item it = items.FirstOrDefault(a => a.Name.Equals(ss1));
                                        int index = items.IndexOf(it);
                                        string ceref1 = r1.Descendants<Cell>().ElementAt(m).CellReference;
                                        string ceref2 = r2.Descendants<Cell>().ElementAt(m).CellReference;

                                        if ((!it.listOfCellReference.Contains(ceref1)))
                                        {
                                            it.listOfCellReference.Add(ceref1);
                                        }
                                        if ((!it.listOfCellReference.Contains(ceref2)))
                                        {
                                            it.listOfCellReference.Add(ceref2);
                                        }
                                        items[index] = it;
                                    }
                                    else
                                    {
                                        Item it = new Item();
                                        it.Name = ss1;
                                        string ceref1 = r1.Descendants<Cell>().ElementAt(m).CellReference;
                                        string ceref2 = r2.Descendants<Cell>().ElementAt(m).CellReference;

                                        if ((!it.listOfCellReference.Contains(ceref1)))
                                        {
                                            it.listOfCellReference.Add(ceref1);
                                        }
                                        if ((!it.listOfCellReference.Contains(ceref2)))
                                        {
                                            it.listOfCellReference.Add(ceref2);
                                        }
                                        items.Add(it);
                                    }
                                }
                            }




                        }
                    }

                    foreach (Item item in items)
                    {
                        string mergeCelValue = string.Join(":", item.listOfCellReference);

                        MergeCells mergeCells;
                        if (worksheet.Elements<MergeCells>().Count() > 0)
                        {
                            mergeCells = worksheet.Elements<MergeCells>().First();
                        }
                        else
                        {
                            mergeCells = new MergeCells();

                            // Insert a MergeCells object into the specified position.
                            if (worksheet.Elements<CustomSheetView>().Count() > 0)
                            {
                                worksheet.InsertAfter(mergeCells, worksheet.Elements<CustomSheetView>().First());
                            }
                            else if (worksheet.Elements<DataConsolidate>().Count() > 0)
                            {
                                worksheet.InsertAfter(mergeCells, worksheet.Elements<DataConsolidate>().First());
                            }
                            else if (worksheet.Elements<SortState>().Count() > 0)
                            {
                                worksheet.InsertAfter(mergeCells, worksheet.Elements<SortState>().First());
                            }
                            else if (worksheet.Elements<AutoFilter>().Count() > 0)
                            {
                                worksheet.InsertAfter(mergeCells, worksheet.Elements<AutoFilter>().First());
                            }
                            else if (worksheet.Elements<Scenarios>().Count() > 0)
                            {
                                worksheet.InsertAfter(mergeCells, worksheet.Elements<Scenarios>().First());
                            }
                            else if (worksheet.Elements<ProtectedRanges>().Count() > 0)
                            {
                                worksheet.InsertAfter(mergeCells, worksheet.Elements<ProtectedRanges>().First());
                            }
                            else if (worksheet.Elements<SheetProtection>().Count() > 0)
                            {
                                worksheet.InsertAfter(mergeCells, worksheet.Elements<SheetProtection>().First());
                            }
                            else if (worksheet.Elements<SheetCalculationProperties>().Count() > 0)
                            {
                                worksheet.InsertAfter(mergeCells, worksheet.Elements<SheetCalculationProperties>().First());
                            }
                            else
                            {
                                worksheet.InsertAfter(mergeCells, worksheet.Elements<SheetData>().First());
                            }
                        }
                        string bFirst = item.listOfCellReference.FirstOrDefault();
                        string bLast = item.listOfCellReference.LastOrDefault();
                        MergeCell mergeCell = new MergeCell() { Reference = new StringValue(bFirst + ":" + bLast) };
                        mergeCells.Append(mergeCell);

                        string aFirst = bFirst.Replace('B', 'A');
                        string aLast = bLast.Replace('B', 'A');
                        MergeCell amergeCell = new MergeCell() { Reference = new StringValue(aFirst + ":" + aLast) };
                        mergeCells.Append(amergeCell);

                        string mFirst = bFirst.Replace('B','M');
                        string mLast = bLast.Replace('B', 'M');
                        MergeCell mergeCell2 = new MergeCell() { Reference = new StringValue(mFirst + ":" + mLast) };
                        mergeCells.Append(mergeCell2);

                        string tFirst = bFirst.Replace('B', 'T');
                        string tLast = bLast.Replace('B', 'T');
                        MergeCell mergeCell3 = new MergeCell() { Reference = new StringValue(tFirst + ":" + tLast) };
                        mergeCells.Append(mergeCell3);

                        string uFirst = bFirst.Replace('B', 'U');
                        string uLast = bLast.Replace('B', 'U');
                        MergeCell mergeCell4 = new MergeCell() { Reference = new StringValue(uFirst + ":" + uLast) };
                        mergeCells.Append(mergeCell4);

                        worksheet.Save();

                    }
                  
                    logger.Debug("ExcelFacade.cs", "Create", "assigning created workbook into the myworkbook.workbookPart.workbook", logPath);
                    myWorkbook.WorkbookPart.Workbook = workbook;
                    logger.Debug("ExcelFacade.cs", "Create", "Calling save function to save myWorkbook.WorkbookPart.Workbook", logPath);
                    myWorkbook.WorkbookPart.Workbook.Save();
                    logger.Debug("ExcelFacade.cs", "Create", "successfully saved", logPath);
                    logger.Debug("ExcelFacade.cs", "Create", "closing myWorkbook", logPath);
                    myWorkbook.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error("ExcelFacade.cs", "Create", "occurd by :" + ex.Message, logPath);
                throw ex;
            }
        }
        private static string GetColumnName(string cellReference)
        {
            if (ColumnNameRegex.IsMatch(cellReference))
                return ColumnNameRegex.Match(cellReference).Value;

            throw new ArgumentOutOfRangeException(cellReference);
        }

        private static readonly Regex ColumnNameRegex = new Regex("[A-Za-z]+");


        static string GetCellValue1(WorkbookPart wb, Cell c)
        {
            string value = c.InnerText;
            if (c.DataType != null)
            {
                switch (c.DataType.Value)
                {
                    case CellValues.SharedString:
                        var stringTable = wb.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();
                        if (stringTable != null)
                        {
                            value =
                                stringTable.SharedStringTable
                                .ElementAt(int.Parse(value)).InnerText;
                        }
                        break;

                    case CellValues.Boolean:
                        switch (value)
                        {
                            case "0":
                                value = "FALSE";
                                break;
                            default:
                                value = "TRUE";
                                break;
                        }
                        break;
                    default:
                        value = "";
                        break;
                }
            }
            return value;
        }
        private static string GetCellValue(SpreadsheetDocument document, Cell cell)
        {
            SharedStringTablePart stringTablePart = document.WorkbookPart.SharedStringTablePart;
            string value = cell.CellValue.InnerXml;

            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText;
            }
            else
            {
                return value;
            }
        }

        private SheetData CreateSheetData(DataTable objects, WorkbookStylesPart stylesPart, out int totalColumn)
        {
            SheetData sheetData = new SheetData();

            totalColumn = 0;
            try
            {
                if (objects != null)
                {
                    int columnCount = objects.Columns.Count;

                    totalColumn = columnCount;

                    /* Get A to Z Alfabets */
                    var az = new List<Char>(Enumerable.Range('A', 'Z' - 'A' + 1).Select(i => (Char)i).ToArray());

                    /* Get Range of colums like A, AC(excel columns) */
                    List<string> headers = new ExcelFacade().GetRange(az, columnCount);
                    int numCols = columnCount;

                    int numRows = objects.Rows.Count;

                    /* Create row object */
                    Row header = new Row();
                    int index = 1;
                    header.RowIndex = (uint)index;

                    /* Set Height to row */
                    header.Height = 20;
                    header.CustomHeight = true;

                    /* Code for user */
                    int row = 0;
                    foreach (DataColumn sHeader in objects.Columns)
                    {
                        HeaderCell cc = new HeaderCell(headers[row].ToString(), sHeader.ColumnName, index, stylesPart.Stylesheet, System.Drawing.Color.SkyBlue, 12, true, false);
                        header.Append(cc);
                        row++;
                    }
                    sheetData.Append(header);
                    // new data
                    foreach (DataRow oUserMaster in objects.Rows)
                    {
                        index++;
                        var r1 = new Row { RowIndex = (uint)index };
                        row = 0;
                        foreach (DataColumn dc in objects.Columns)
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
            catch (Exception ex)
            {
                throw ex;
            }

            return sheetData;
        }
    }

    public class GenerateExcelReports
    {
        Logger logger = new Logger();

        public void Create(string fileName, DataTable objects, string sheetName, string logPath, List<int> size = null)
        {
            try
            {
                logger.Debug("ExcelFacade.cs", "Create", "Getting passed parameter", logPath);
                //Open the copied template workbook. 
                // SpreadsheetDocument myWorkbook = SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook);
                logger.Debug("ExcelFacade.cs", "Create", "Creating SpreadsheetDocument name myworkbook", logPath);
                using (SpreadsheetDocument myWorkbook = SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook))
                {
                    logger.Debug("ExcelFacade.cs", "Create", "Creating workbook part and worksheet part", logPath);
                    WorkbookPart workbookPart = myWorkbook.AddWorkbookPart();
                    WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();

                    // Create Styles and Insert into Workbook
                    logger.Debug("ExcelFacade.cs", "Create", "Creating workbook style part", logPath);
                    WorkbookStylesPart stylesPart = myWorkbook.WorkbookPart.AddNewPart<WorkbookStylesPart>();
                    logger.Debug("ExcelFacade.cs", "Create", "Creating styles from customStylesheet", logPath);
                    Stylesheet styles = new CustomStylesheet();
                    logger.Debug("ExcelFacade.cs", "Create", "Calling Save function", logPath);
                    styles.Save(stylesPart);

                    logger.Debug("ExcelFacade.cs", "Create", "Getting Part Id from worksheet part", logPath);
                    string relId = workbookPart.GetIdOfPart(worksheetPart);

                    logger.Debug("ExcelFacade.cs", "Create", "Creating workbook", logPath);
                    Workbook workbook = new Workbook();
                    logger.Debug("ExcelFacade.cs", "Create", "Getting file versioin", logPath);
                    FileVersion fileVersion = new FileVersion { ApplicationName = "Microsoft Office Excel" };

                    logger.Debug("ExcelFacade.cs", "Create", "Creating worksheet", logPath);
                    Worksheet worksheet = new Worksheet();

                    int columnCount;

                    logger.Debug("ExcelFacade.cs", "Create", "Creating sheet data", logPath);
                    SheetData sheetData = CreateSheetData(objects, stylesPart, out columnCount);

                    logger.Debug("ExcelFacade.cs", "Create", "Getting column count", logPath);
                    int numCols = columnCount;//headerNames.Count;
                    int width = 50;//headerNames.Max(h => h.Length) + 5;


                    logger.Debug("ExcelFacade.cs", "Create", "Creating columns", logPath);
                    Columns columns = new Columns();

                    for (int col = 0; col < numCols; col++)
                    {
                        if (size != null)
                            width = size[col];
                        Column c = new ExcelFacade().CreateColumnData((UInt32)col + 1, (UInt32)numCols + 1, width);

                        columns.Append(c);
                    }

                    logger.Debug("ExcelFacade.cs", "Create", "Appending columns into the worksheet", logPath);
                    worksheet.Append(columns);


                    logger.Debug("ExcelFacade.cs", "Create", "Creating sheets", logPath);
                    Sheets sheets = new Sheets();
                    Sheet sheet = new Sheet { Name = sheetName, SheetId = 1, Id = relId };
                    logger.Debug("ExcelFacade.cs", "Create", "Appending sheet into the sheets", logPath);
                    sheets.Append(sheet);
                    logger.Debug("ExcelFacade.cs", "Create", "Appending fileVersion and sheets into the workbook", logPath);
                    workbook.Append(fileVersion);
                    workbook.Append(sheets);

                    logger.Debug("ExcelFacade.cs", "Create", "Appending sheetData into the worksheet", logPath);
                    worksheet.Append(sheetData);
                    worksheetPart.Worksheet = worksheet;

                    logger.Debug("ExcelFacade.cs", "Create", "successfully appended merged cells into the another merge cell", logPath);
                    logger.Debug("ExcelFacade.cs", "Create", "Calling save function to save worksheetpart.worksheet", logPath);
                    worksheetPart.Worksheet.Save();

                    logger.Debug("ExcelFacade.cs", "Create", "assigning created workbook into the myworkbook.workbookPart.workbook", logPath);
                    myWorkbook.WorkbookPart.Workbook = workbook;
                    logger.Debug("ExcelFacade.cs", "Create", "Calling save function to save myWorkbook.WorkbookPart.Workbook", logPath);
                    myWorkbook.WorkbookPart.Workbook.Save();
                    logger.Debug("ExcelFacade.cs", "Create", "successfully saved", logPath);
                    logger.Debug("ExcelFacade.cs", "Create", "closing myWorkbook", logPath);
                    myWorkbook.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error("ExcelFacade.cs", "Create", "occurd by :" + ex.Message, logPath);
                throw ex;
            }
        }

        private SheetData CreateSheetData(DataTable objects, WorkbookStylesPart stylesPart, out int totalColumn)
        {
            SheetData sheetData = new SheetData();

            totalColumn = 0;
            try
            {
                if (objects != null)
                {
                    int columnCount = objects.Columns.Count;

                    totalColumn = columnCount;

                    /* Get A to Z Alfabets */
                    var az = new List<Char>(Enumerable.Range('A', 'Z' - 'A' + 1).Select(i => (Char)i).ToArray());

                    /* Get Range of colums like A, AC(excel columns) */
                    List<string> headers = new ExcelFacade().GetRange(az, columnCount);
                    int numCols = columnCount;

                    int numRows = objects.Rows.Count;

                    /* Create row object */
                    Row header = new Row();
                    int index = 1;
                    header.RowIndex = (uint)index;

                    /* Set Height to row */
                    header.Height = 20;
                    header.CustomHeight = true;

                    /* Code for user */
                    int row = 0;
                    foreach (DataColumn sHeader in objects.Columns)
                    {
                        HeaderCell cc = new HeaderCell(headers[row].ToString(), sHeader.ColumnName, index, stylesPart.Stylesheet, System.Drawing.Color.SkyBlue, 12, true, false);
                        header.Append(cc);
                        row++;
                    }
                    sheetData.Append(header);
                    // new data
                    foreach (DataRow oUserMaster in objects.Rows)
                    {
                        index++;
                        var r1 = new Row { RowIndex = (uint)index };
                        row = 0;
                        foreach (DataColumn dc in objects.Columns)
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
            catch (Exception ex)
            {
                throw ex;
            }

            return sheetData;
        }
    }


}
