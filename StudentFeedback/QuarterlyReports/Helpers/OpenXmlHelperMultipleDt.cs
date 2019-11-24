using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;


namespace QuarterlyReports.Helpers
{
    public class OpenXmlHelperMultipleDt
    {
        public bool ExportDataSet(System.Data.DataTable[] tables, string excelFileName, Dictionary<string, string> dic, string collegeCode = null)
        {
            try
            {
                using (var workbook = SpreadsheetDocument.Create(excelFileName, DocumentFormat.OpenXml.SpreadsheetDocumentType.Workbook))
                {
                    var workbookPart = workbook.AddWorkbookPart();

                    workbook.WorkbookPart.Workbook = new DocumentFormat.OpenXml.Spreadsheet.Workbook();

                    workbook.WorkbookPart.Workbook.Sheets = new DocumentFormat.OpenXml.Spreadsheet.Sheets();

                    var sheetPart = workbook.WorkbookPart.AddNewPart<WorksheetPart>();

                    //WorkbookStylesPart stylesPart = workbook.WorkbookPart.AddNewPart<WorkbookStylesPart>();
                    //Stylesheet styles = new CustomStylesheet();
                    //styles.Save(stylesPart);

                    WorkbookStylesPart stylePart = workbookPart.AddNewPart<WorkbookStylesPart>();
                    stylePart.Stylesheet = GenerateStylesheet();
                    stylePart.Stylesheet.Save();

                    var sheetData = new DocumentFormat.OpenXml.Spreadsheet.SheetData();
                    //sheetPart.Worksheet = new DocumentFormat.OpenXml.Spreadsheet.Worksheet(sheetData);

                    DocumentFormat.OpenXml.Spreadsheet.Sheets sheets = workbook.WorkbookPart.Workbook.GetFirstChild<DocumentFormat.OpenXml.Spreadsheet.Sheets>();
                    string relationshipId = workbook.WorkbookPart.GetIdOfPart(sheetPart);

                    uint sheetId = 1;
                    if (sheets.Elements<DocumentFormat.OpenXml.Spreadsheet.Sheet>().Count() > 0)
                    {
                        sheetId =
                            sheets.Elements<DocumentFormat.OpenXml.Spreadsheet.Sheet>().Select(s => s.SheetId.Value).Max() + 1;
                    }

                    DocumentFormat.OpenXml.Spreadsheet.Sheet sheet = new DocumentFormat.OpenXml.Spreadsheet.Sheet() { Id = relationshipId, SheetId = sheetId, Name = "Teacher Code" };
                    sheets.Append(sheet);

                    DocumentFormat.OpenXml.Spreadsheet.Row newRow1 = new DocumentFormat.OpenXml.Spreadsheet.Row();
                    DocumentFormat.OpenXml.Spreadsheet.Cell cell1 = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                    cell1.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;

                    string collegeName = ConfigurationManager.AppSettings[collegeCode];

                    if (string.IsNullOrEmpty(collegeName))
                        collegeName = "MANIBEN NANAVATI WOMEN'S COLLEGE";

                    cell1.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(collegeName);
                    newRow1.AppendChild(cell1);
                    sheetData.AppendChild(newRow1);

                    DocumentFormat.OpenXml.Spreadsheet.Row newRow2 = new DocumentFormat.OpenXml.Spreadsheet.Row();
                    DocumentFormat.OpenXml.Spreadsheet.Cell cell2 = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                    cell2.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;

                    if (dic.ContainsKey("teacherCode"))
                        cell2.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("TEACHING EFFECTIVENESS SCALE");
                    if (dic.ContainsKey("adminName"))
                        cell2.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("ADMINISTRATION EFFECTIVENESS SCALE");

                    newRow2.AppendChild(cell2);
                    sheetData.AppendChild(newRow2);

                    if (dic["acedemicYear"] == "undefined")
                        dic["acedemicYear"] = "2018-2019";

                    DocumentFormat.OpenXml.Spreadsheet.Row newRow3 = new DocumentFormat.OpenXml.Spreadsheet.Row();
                    DocumentFormat.OpenXml.Spreadsheet.Cell cell3 = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                    cell3.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                    cell3.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("YEAR " + dic["acedemicYear"]);
                    newRow3.AppendChild(cell3);
                    sheetData.AppendChild(newRow3);

                    DocumentFormat.OpenXml.Spreadsheet.Row newRow4 = new DocumentFormat.OpenXml.Spreadsheet.Row();
                    DocumentFormat.OpenXml.Spreadsheet.Cell cell4 = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                    cell4.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;

                    if (dic.ContainsKey("teacherCode"))
                        cell4.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("TEACHER'S CODE : " + dic["teacherCode"]);
                    else if (dic.ContainsKey("adminName"))
                        cell4.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("ADMINISTRATOR'S NAME : " + dic["adminName"]);

                    newRow4.AppendChild(cell4);
                    sheetData.AppendChild(newRow4);

                    DocumentFormat.OpenXml.Spreadsheet.Row newRow5 = new DocumentFormat.OpenXml.Spreadsheet.Row();
                    DocumentFormat.OpenXml.Spreadsheet.Cell cell5 = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                    cell5.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                    cell5.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("");
                    newRow5.AppendChild(cell5);
                    sheetData.AppendChild(newRow5);


                    foreach (DataTable table in tables)
                    {
                        DocumentFormat.OpenXml.Spreadsheet.Row headerRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
                        // Construct column names 
                        List<String> columns = new List<string>();
                        foreach (System.Data.DataColumn column in table.Columns)
                        {
                            columns.Add(column.ColumnName);


                            if (!column.ColumnName.Equals("ENUMDESCRIPTION"))
                            {
                                DocumentFormat.OpenXml.Spreadsheet.Cell cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                                cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                                cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(column.ColumnName);
                                headerRow.AppendChild(cell);
                                AddBold(workbook, cell, HorizontalAlignmentValues.Left);
                            }

                        }


                        // Add the row values to the excel sheet 
                        sheetData.AppendChild(headerRow);

                        foreach (System.Data.DataRow dsrow in table.Rows)
                        {
                            DocumentFormat.OpenXml.Spreadsheet.Row newRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
                            foreach (String col in columns)
                            {
                                DocumentFormat.OpenXml.Spreadsheet.Cell cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();

                                string value = Convert.ToString(dsrow[col]);
                                string replacedValue = string.Empty;
                                if (!string.IsNullOrEmpty(value))
                                {
                                    replacedValue = value.Replace("|", "'");
                                }

                                if (IsNumeric(replacedValue))
                                {
                                    cell.DataType = CellValues.Number;
                                }
                                else
                                {
                                    cell.DataType = CellValues.String;

                                }

                                cell.CellValue = new CellValue(replacedValue);
                                newRow.AppendChild(cell);
                            }

                            sheetData.AppendChild(newRow);
                        }

                    }
                    Columns columns2 = AutoSize(sheetData);
                    sheetPart.Worksheet = new Worksheet();
                    sheetPart.Worksheet.Append(columns2);
                    sheetPart.Worksheet.Append(sheetData);

                    MergeCells mergeCells;
                    if (sheetPart.Worksheet.Elements<MergeCells>().Count() > 0)
                    {
                        mergeCells = sheet.Elements<MergeCells>().First();
                    }
                    else
                    {
                        mergeCells = new MergeCells();

                        // Insert a MergeCells object into the specified position.
                        if (sheetPart.Worksheet.Elements<CustomSheetView>().Count() > 0)
                        {
                            sheetPart.Worksheet.InsertAfter(mergeCells, sheetPart.Worksheet.Elements<CustomSheetView>().First());
                        }
                        else if (sheet.Elements<DataConsolidate>().Count() > 0)
                        {
                            sheetPart.Worksheet.InsertAfter(mergeCells, sheetPart.Worksheet.Elements<DataConsolidate>().First());
                        }
                        else if (sheet.Elements<SortState>().Count() > 0)
                        {
                            sheetPart.Worksheet.InsertAfter(mergeCells, sheetPart.Worksheet.Elements<SortState>().First());
                        }
                        else if (sheet.Elements<AutoFilter>().Count() > 0)
                        {
                            sheetPart.Worksheet.InsertAfter(mergeCells, sheetPart.Worksheet.Elements<AutoFilter>().First());
                        }
                        else if (sheet.Elements<Scenarios>().Count() > 0)
                        {
                            sheetPart.Worksheet.InsertAfter(mergeCells, sheetPart.Worksheet.Elements<Scenarios>().First());
                        }
                        else if (sheet.Elements<ProtectedRanges>().Count() > 0)
                        {
                            sheetPart.Worksheet.InsertAfter(mergeCells, sheetPart.Worksheet.Elements<ProtectedRanges>().First());
                        }
                        else if (sheet.Elements<SheetProtection>().Count() > 0)
                        {
                            sheetPart.Worksheet.InsertAfter(mergeCells, sheetPart.Worksheet.Elements<SheetProtection>().First());
                        }
                        else if (sheet.Elements<SheetCalculationProperties>().Count() > 0)
                        {
                            sheetPart.Worksheet.InsertAfter(mergeCells, sheetPart.Worksheet.Elements<SheetCalculationProperties>().First());
                        }
                        else
                        {
                            sheetPart.Worksheet.InsertAfter(mergeCells, sheetPart.Worksheet.Elements<SheetData>().First());
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
                    sheetPart.Worksheet.Save();

                    AddBold(workbook, cell1, HorizontalAlignmentValues.Center);
                    AddBold(workbook, cell2, HorizontalAlignmentValues.Center);
                    AddBold(workbook, cell3, HorizontalAlignmentValues.Center);
                    AddBold(workbook, cell4, HorizontalAlignmentValues.Left);

                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        void AddBold(SpreadsheetDocument document, Cell c, HorizontalAlignmentValues alignment)
        {
            Fonts fs = AddFont(document.WorkbookPart.WorkbookStylesPart.Stylesheet.Fonts);
            AddCellFormat(document.WorkbookPart.WorkbookStylesPart.Stylesheet.CellFormats, document.WorkbookPart.WorkbookStylesPart.Stylesheet.Fonts, alignment);
            c.StyleIndex = (UInt32)(document.WorkbookPart.WorkbookStylesPart.Stylesheet.CellFormats.Elements<CellFormat>().Count() - 1);
        }

        private static bool IsNumeric(string value)
        {
            double output = 0;
            if (Double.TryParse(value, out output))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static Fonts AddFont(Fonts fs)
        {
            Font font2 = new Font();
            Bold bold1 = new Bold();
            FontSize fontSize2 = new FontSize() { Val = 11D };
            Color color2 = new Color() { Theme = (UInt32Value)1U };
            FontName fontName2 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering2 = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme2 = new FontScheme() { Val = FontSchemeValues.Minor };

            font2.Append(bold1);
            font2.Append(fontSize2);
            font2.Append(color2);
            font2.Append(fontName2);
            font2.Append(fontFamilyNumbering2);
            font2.Append(fontScheme2);

            fs.Append(font2);
            return fs;
        }
        static void AddCellFormat(CellFormats cf, Fonts fs, HorizontalAlignmentValues alignment)
        {
            CellFormat cellFormat2 = new CellFormat() { Alignment = new Alignment() { Horizontal = alignment }, NumberFormatId = 0, FontId = (UInt32)(fs.Elements<Font>().Count() - 1), FillId = 0, BorderId = 0, FormatId = 0, ApplyFill = true };
            cf.Append(cellFormat2);
        }

        private Columns AutoSize(SheetData sheetData)
        {
            var maxColWidth = GetMaxCharacterWidth(sheetData);

            Columns columns = new Columns();
            //this is the width of my font - yours may be different
            double maxWidth = 3;
            foreach (var item in maxColWidth)
            {
                //width = Truncate([{Number of Characters} * {Maximum Digit Width} + {5 pixel padding}]/{Maximum Digit Width}*256)/256
                double width = Math.Truncate((item.Value * maxWidth + 5) / maxWidth * 256) / 256;

                //pixels=Truncate(((256 * {width} + Truncate(128/{Maximum Digit Width}))/256)*{Maximum Digit Width})
                double pixels = Math.Truncate(((256 * width + Math.Truncate(128 / maxWidth)) / 256) * maxWidth);

                //character width=Truncate(({pixels}-5)/{Maximum Digit Width} * 100+0.5)/100
                double charWidth = Math.Truncate((pixels - 5) / maxWidth * 100 + 0.5) / 100;

                Column col = new Column() { BestFit = false, Min = (UInt32)(item.Key + 1), Max = (UInt32)(item.Key + 1), CustomWidth = true, Width = (DoubleValue)width };
                columns.Append(col);
            }

            return columns;
        }

        private Dictionary<int, int> GetMaxCharacterWidth(SheetData sheetData)
        {
            //iterate over all cells getting a max char value for each column
            Dictionary<int, int> maxColWidth = new Dictionary<int, int>();
            var rows = sheetData.Elements<Row>();
            UInt32[] numberStyles = new UInt32[] { 5, 6, 7, 8 }; //styles that will add extra chars
            UInt32[] boldStyles = new UInt32[] { 1, 2, 3, 4, 6, 7, 8 }; //styles that will bold
            foreach (var r in rows)
            {
                var cells = r.Elements<Cell>().ToArray();

                //using cell index as my column
                for (int i = 0; i < cells.Length; i++)
                {
                    var cell = cells[i];
                    var cellValue = cell.CellValue == null ? string.Empty : cell.CellValue.InnerText;
                    var cellTextLength = cellValue.Length;

                    if (cell.StyleIndex != null && numberStyles.Contains(cell.StyleIndex))
                    {
                        int thousandCount = (int)Math.Truncate((double)cellTextLength / 4);

                        //add 3 for '.00' 
                        cellTextLength += (3 + thousandCount);
                    }

                    if (cell.StyleIndex != null && boldStyles.Contains(cell.StyleIndex))
                    {
                        //add an extra char for bold - not 100% acurate but good enough for what i need.
                        cellTextLength += 1;
                    }

                    if (maxColWidth.ContainsKey(i))
                    {
                        var current = maxColWidth[i];
                        if (cellTextLength > current)
                        {
                            maxColWidth[i] = cellTextLength;
                        }
                    }
                    else
                    {
                        maxColWidth.Add(i, cellTextLength);
                    }
                }
            }

            return maxColWidth;
        }

        private Stylesheet GenerateStylesheet()
        {
            Stylesheet styleSheet = null;

            Fonts fonts = new Fonts(
                new Font( // Index 0 - default
                    new FontSize() { Val = 10 },
                    new FontName() { Val = "Calibri" }
                ),
                new Font( // Index 1 - header
                    new FontSize() { Val = 20 },
                    new Bold(),
                    new Color() { Rgb = "FFFFFF" },
                    new FontName() { Val = "Calibri" }
                ));

            Fills fills = new Fills(
                    new Fill(new PatternFill() { PatternType = PatternValues.None }), // Index 0 - default
                    new Fill(new PatternFill() { PatternType = PatternValues.Gray125 }), // Index 1 - default
                    new Fill(new PatternFill(new ForegroundColor { Rgb = new HexBinaryValue() { Value = "66666666" } }) { PatternType = PatternValues.Solid }) // Index 2 - header
                );
            Borders borders = new Borders(
                    new Border(), // index 0 default
                    new Border( // index 1 black border
                        new LeftBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new RightBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new TopBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new BottomBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                        new DiagonalBorder())
                );

            CellFormats cellFormats = new CellFormats(
                    new CellFormat(), // default
                    new CellFormat { FontId = 0, FillId = 0, BorderId = 1, ApplyBorder = true }, // body
                    new CellFormat { FontId = 1, FillId = 2, BorderId = 1, ApplyFill = true } // header
                );

            styleSheet = new Stylesheet(fonts, fills, borders, cellFormats);

            return styleSheet;
        }
    }
}