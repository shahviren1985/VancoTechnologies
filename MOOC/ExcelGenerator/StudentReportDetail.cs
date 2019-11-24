using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using System.Data;
using ITM.Courses.LogManager;

namespace ITM.Courses.ExcelGenerator
{
    public class StudentReportDetail
    {
        Logger logger = null;
        public string LoggedInUser { get; set; }

        public void Create(string fileName, DataTable objects, string sheetName, string logPath)
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
                        foreach (DataColumn dc in objects.Columns)
                        {


                            // write user frist-name into file
                            TextCell tId = new TextCell(dc.ColumnName.ToString(), oUserMaster[dc.ColumnName].ToString()/*oUserMaster["Id"].ToString()*/, index, stylesPart.Stylesheet, System.Drawing.Color.White);
                            r1.Append(tId);

                        }

                        sheetData.Append(r1);
                        //index++;
                        //var r1 = new Row { RowIndex = (uint)index };

                        //// write user frist-name into file
                        //TextCell tId = new TextCell(headers[0].ToString(), dc.c/*oUserMaster["Id"].ToString()*/, index, stylesPart.Stylesheet, System.Drawing.Color.White);
                        //r1.Append(tId);

                        //TextCell tRollNumber = new TextCell(headers[1].ToString(), "", index, stylesPart.Stylesheet, System.Drawing.Color.White);
                        //r1.Append(tRollNumber);

                        //TextCell tCourseId = new TextCell(headers[2].ToString(), "", index, stylesPart.Stylesheet, System.Drawing.Color.White);
                        //r1.Append(tCourseId);

                        //TextCell tSubCourseId = new TextCell(headers[3].ToString(), "", index, stylesPart.Stylesheet, System.Drawing.Color.White);
                        //r1.Append(tSubCourseId);

                        //TextCell tFirstName = new TextCell(headers[4].ToString(), "", index, stylesPart.Stylesheet, System.Drawing.Color.White);
                        //r1.Append(tFirstName);

                        //TextCell tLastName = new TextCell(headers[5].ToString(), "", index, stylesPart.Stylesheet, System.Drawing.Color.White);
                        //r1.Append(tLastName);

                        //TextCell tFatherName = new TextCell(headers[6].ToString(), "", index, stylesPart.Stylesheet, System.Drawing.Color.White);
                        //r1.Append(tFatherName);

                        //TextCell tMotherName = new TextCell(headers[7].ToString(), "", index, stylesPart.Stylesheet, System.Drawing.Color.White);
                        //r1.Append(tMotherName);

                        //TextCell tPreRegNumber = new TextCell(headers[8].ToString(), "", index, stylesPart.Stylesheet, System.Drawing.Color.White);
                        //r1.Append(tPreRegNumber);

                        //TextCell tBatchYear = new TextCell(headers[9].ToString(), "", index, stylesPart.Stylesheet, System.Drawing.Color.White);
                        //r1.Append(tBatchYear);

                        //TextCell tStudentId = new TextCell(headers[10].ToString(), "", index, stylesPart.Stylesheet, System.Drawing.Color.White);
                        //r1.Append(tStudentId);

                        //TextCell tCD = new TextCell(headers[9].ToString(), oUserMaster.CreatedDate.ToString("dd-MMM-yyyy"), index, stylesPart.Stylesheet, System.Drawing.Color.White);
                        //r1.Append(tCD);

                        //TextCell tComplete = new TextCell(headers[10].ToString(), oUserMaster.Complete, index, stylesPart.Stylesheet, System.Drawing.Color.White);
                        //r1.Append(tComplete);

                        //sheetData.Append(r1);
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
