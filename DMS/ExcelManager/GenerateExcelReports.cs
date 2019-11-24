using AA.LogManager;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AA.ExcelManager
{
    public class GenerateExcelReports
    {
        private Logger logger = new Logger();

        public void Create(string fileName, DataTable objects, string sheetName, string logPath)
        {
            try
            {
                this.logger.Debug("ExcelFacade.cs", "Create", "Getting passed parameter", logPath);
                this.logger.Debug("ExcelFacade.cs", "Create", "Creating SpreadsheetDocument name myworkbook", logPath);
                using (SpreadsheetDocument document = SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook))
                {
                    int num;
                    this.logger.Debug("ExcelFacade.cs", "Create", "Creating workbook part and worksheet part", logPath);
                    WorkbookPart part = document.AddWorkbookPart();
                    WorksheetPart part2 = part.AddNewPart<WorksheetPart>();
                    this.logger.Debug("ExcelFacade.cs", "Create", "Creating workbook style part", logPath);
                    WorkbookStylesPart openXmlPart = document.WorkbookPart.AddNewPart<WorkbookStylesPart>();
                    this.logger.Debug("ExcelFacade.cs", "Create", "Creating styles from customStylesheet", logPath);
                    Stylesheet stylesheet = new CustomStylesheet();
                    this.logger.Debug("ExcelFacade.cs", "Create", "Calling Save function", logPath);
                    stylesheet.Save(openXmlPart);
                    this.logger.Debug("ExcelFacade.cs", "Create", "Getting Part Id from worksheet part", logPath);
                    string idOfPart = part.GetIdOfPart(part2);
                    this.logger.Debug("ExcelFacade.cs", "Create", "Creating workbook", logPath);
                    Workbook workbook = new Workbook();
                    this.logger.Debug("ExcelFacade.cs", "Create", "Getting file versioin", logPath);
                    FileVersion version = new FileVersion
                    {
                        ApplicationName = "Microsoft Office Excel"
                    };
                    this.logger.Debug("ExcelFacade.cs", "Create", "Creating worksheet", logPath);
                    Worksheet worksheet = new Worksheet();
                    this.logger.Debug("ExcelFacade.cs", "Create", "Creating sheet data", logPath);
                    SheetData data = this.CreateSheetData(objects, openXmlPart, out num);
                    this.logger.Debug("ExcelFacade.cs", "Create", "Getting column count", logPath);
                    int num2 = num;
                    int num3 = 50;
                    this.logger.Debug("ExcelFacade.cs", "Create", "Creating columns", logPath);
                    Columns columns = new Columns();
                    for (int i = 0; i < num2; i++)
                    {
                        Column column = new ExcelFacade().CreateColumnData((uint)(i + 1), (uint)(num2 + 1), (double)num3);
                        columns.Append(new OpenXmlElement[] { column });
                    }
                    this.logger.Debug("ExcelFacade.cs", "Create", "Appending columns into the worksheet", logPath);
                    worksheet.Append(new OpenXmlElement[] { columns });
                    this.logger.Debug("ExcelFacade.cs", "Create", "Creating sheets", logPath);
                    Sheets sheets = new Sheets();
                    Sheet sheet = new Sheet
                    {
                        Name = sheetName,
                        SheetId = 1,
                        Id = idOfPart
                    };
                    this.logger.Debug("ExcelFacade.cs", "Create", "Appending sheet into the sheets", logPath);
                    sheets.Append(new OpenXmlElement[] { sheet });
                    this.logger.Debug("ExcelFacade.cs", "Create", "Appending fileVersion and sheets into the workbook", logPath);
                    workbook.Append(new OpenXmlElement[] { version });
                    workbook.Append(new OpenXmlElement[] { sheets });
                    this.logger.Debug("ExcelFacade.cs", "Create", "Appending sheetData into the worksheet", logPath);
                    worksheet.Append(new OpenXmlElement[] { data });
                    part2.Worksheet = worksheet;
                    this.logger.Debug("ExcelFacade.cs", "Create", "successfully appended merged cells into the another merge cell", logPath);
                    this.logger.Debug("ExcelFacade.cs", "Create", "Calling save function to save worksheetpart.worksheet", logPath);
                    part2.Worksheet.Save();
                    this.logger.Debug("ExcelFacade.cs", "Create", "assigning created workbook into the myworkbook.workbookPart.workbook", logPath);
                    document.WorkbookPart.Workbook = workbook;
                    this.logger.Debug("ExcelFacade.cs", "Create", "Calling save function to save myWorkbook.WorkbookPart.Workbook", logPath);
                    document.WorkbookPart.Workbook.Save();
                    this.logger.Debug("ExcelFacade.cs", "Create", "successfully saved", logPath);
                    this.logger.Debug("ExcelFacade.cs", "Create", "closing myWorkbook", logPath);
                    document.Close();
                }
            }
            catch (Exception exception)
            {
                this.logger.Error("ExcelFacade.cs", "Create", "occurd by :" + exception.Message, logPath);
                throw exception;
            }
        }

        private SheetData CreateSheetData(DataTable objects, WorkbookStylesPart stylesPart, out int totalColumn)
        {
            SheetData data = new SheetData();
            totalColumn = 0;
            try
            {
                if (objects == null)
                {
                    return data;
                }
                int count = objects.Columns.Count;
                totalColumn = count;
                List<char> chr = new List<char>((from i in Enumerable.Range(0x41, 0x1a) select (char)i).ToArray<char>());
                List<string> range = new ExcelFacade().GetRange(chr, count);
                int num1 = objects.Rows.Count;
                Row row = new Row();
                int index = 1;
                row.RowIndex = Convert.ToUInt32(index);
                row.Height = 20.0;
                row.CustomHeight = true;
                int num3 = 0;
                foreach (DataColumn column in objects.Columns)
                {
                    HeaderCell cell = new HeaderCell(range[num3].ToString(), column.ColumnName, index, stylesPart.Stylesheet, System.Drawing.Color.SkyBlue, 12.0, true, false);
                    row.Append(new OpenXmlElement[] { cell });
                    num3++;
                }
                data.Append(new OpenXmlElement[] { row });
                foreach (DataRow row2 in objects.Rows)
                {
                    index++;
                    Row row3 = new Row
                    {
                        RowIndex = Convert.ToUInt32(index)
                    };
                    num3 = 0;
                    foreach (DataColumn column2 in objects.Columns)
                    {
                        TextCell cell2 = new TextCell(range[num3].ToString(), row2[column2.ColumnName].ToString(), index, stylesPart.Stylesheet, System.Drawing.Color.White);
                        row3.Append(new OpenXmlElement[] { cell2 });
                        num3++;
                    }
                    data.Append(new OpenXmlElement[] { row3 });
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return data;
        }
    }
}
