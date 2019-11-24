using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;


namespace AA.ExcelManager
{

    public class ExcelFacade
    {
        private static UInt32Value CreateCellFormat(Stylesheet styleSheet, UInt32Value fontIndex, UInt32Value fillIndex, UInt32Value numberFormatId)
        {
            UInt32Value value3;
            CellFormat format = new CellFormat();
            try
            {
                if (fontIndex != null)
                {
                    format.FontId = fontIndex;
                }
                if (fillIndex != null)
                {
                    format.FillId = fillIndex;
                }
                if (numberFormatId != null)
                {
                    format.NumberFormatId = numberFormatId;
                    format.ApplyNumberFormat = BooleanValue.FromBoolean(true);
                }
                styleSheet.CellFormats.Append(new OpenXmlElement[] { format });
                UInt32Value count = styleSheet.CellFormats.Count;
                CellFormats cellFormats = styleSheet.CellFormats;
                cellFormats.Count += 1;
                value3 = count;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return value3;
        }

        public Column CreateColumnData(uint startColumnIndex, uint endColumnIndex, double columnWidth)
        {
            return new Column
            {
                Min = startColumnIndex,
                Max = endColumnIndex,
                Width = columnWidth,
                CustomWidth = true
            };
        }

        private Cell CreateDateCell(string header, string text, int index, Stylesheet styles)
        {
            Cell cell = new Cell();
            try
            {
                cell.DataType = CellValues.Date;
                cell.CellReference = header + index;
                UInt32Value fontIndex = this.CreateFont(styles, "Arial", 11.0, false, System.Drawing.Color.Black);
                UInt32Value fillIndex = this.CreateFill(styles, System.Drawing.Color.White);
                UInt32Value value4 = CreateCellFormat(styles, fontIndex, fillIndex, 14);
                cell.StyleIndex = value4;
                CellValue value5 = new CellValue
                {
                    Text = text
                };
                cell.CellValue = value5;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return cell;
        }

        private Cell CreateDecimalCell(string header, string text, int index, Stylesheet styles)
        {
            Cell cell = new Cell();
            try
            {
                cell.DataType = CellValues.Number;
                cell.CellReference = header + index;
                UInt32Value fontIndex = this.CreateFont(styles, "Arial", 11.0, false, System.Drawing.Color.Black);
                UInt32Value fillIndex = this.CreateFill(styles, System.Drawing.Color.White);
                UInt32Value value4 = CreateCellFormat(styles, fontIndex, fillIndex, 0xab);
                cell.StyleIndex = value4;
                CellValue newChild = new CellValue
                {
                    Text = text
                };
                cell.AppendChild<CellValue>(newChild);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return cell;
        }

        private UInt32Value CreateFill(Stylesheet styleSheet, System.Drawing.Color fillColor)
        {
            UInt32Value value4;
            try
            {
                OpenXmlElement[] childElements = new OpenXmlElement[1];
                ForegroundColor color = new ForegroundColor();
                HexBinaryValue value3 = new HexBinaryValue
                {
                    Value = ColorTranslator.ToHtml(System.Drawing.Color.FromArgb(fillColor.A, fillColor.R, fillColor.G, fillColor.B)).Replace("#", "")
                };
                color.Rgb = value3;
                childElements[0] = color;
                PatternFill fill = new PatternFill(childElements)
                {
                    PatternType = (fillColor == System.Drawing.Color.White) ? PatternValues.None : PatternValues.LightDown
                };
                Fill fill2 = new Fill(new OpenXmlElement[] { fill });
                styleSheet.Fills.Append(new OpenXmlElement[] { fill2 });
                UInt32Value count = styleSheet.Fills.Count;
                Fills fills = styleSheet.Fills;
                fills.Count += 1;
                value4 = count;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return value4;
        }

        private Cell CreateFomulaCell(string header, string formula, int index, Stylesheet styles)
        {
            Cell cell = new Cell();
            try
            {
                cell.DataType = CellValues.Number;
                cell.CellReference = header + index;
                UInt32Value fontIndex = this.CreateFont(styles, "Arial", 11.0, false, System.Drawing.Color.Black);
                UInt32Value fillIndex = this.CreateFill(styles, System.Drawing.Color.White);
                UInt32Value value4 = CreateCellFormat(styles, fontIndex, fillIndex, 0xab);
                cell.StyleIndex = value4;
                CellFormula formula2 = new CellFormula
                {
                    CalculateCell = true,
                    Text = formula
                };
                cell.Append(new OpenXmlElement[] { formula2 });
                CellValue newChild = new CellValue();
                cell.AppendChild<CellValue>(newChild);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return cell;
        }

        private UInt32Value CreateFont(Stylesheet styleSheet, string fontName, double? fontSize, bool isBold, System.Drawing.Color foreColor)
        {
            DocumentFormat.OpenXml.Spreadsheet.Font font = new DocumentFormat.OpenXml.Spreadsheet.Font();
            if (!string.IsNullOrEmpty(fontName))
            {
                FontName name = new FontName
                {
                    Val = fontName
                };
                font.Append(new OpenXmlElement[] { name });
            }
            if (fontSize.HasValue)
            {
                FontSize size = new FontSize
                {
                    Val = fontSize.Value
                };
                font.Append(new OpenXmlElement[] { size });
            }
            if (isBold)
            {
                Bold bold = new Bold();
                font.Append(new OpenXmlElement[] { bold });
            }
            DocumentFormat.OpenXml.Spreadsheet.Color color2 = new DocumentFormat.OpenXml.Spreadsheet.Color();
            HexBinaryValue value3 = new HexBinaryValue
            {
                Value = ColorTranslator.ToHtml(System.Drawing.Color.FromArgb(foreColor.A, foreColor.R, foreColor.G, foreColor.B)).Replace("#", "")
            };
            color2.Rgb = value3;
            DocumentFormat.OpenXml.Spreadsheet.Color color = color2;
            font.Append(new OpenXmlElement[] { color });
            styleSheet.Fonts.Append(new OpenXmlElement[] { font });
            UInt32Value count = styleSheet.Fonts.Count;
            Fonts fonts = styleSheet.Fonts;
            fonts.Count += 1;
            return count;
        }

        private Cell CreateHeaderCell(string header, string text, int index, Stylesheet styles)
        {
            Cell cell = new Cell();
            try
            {
                cell.DataType = CellValues.InlineString;
                cell.CellReference = header + index;
                Console.WriteLine(header + index);
                UInt32Value fontIndex = this.CreateFont(styles, "Arial", 12.0, true, System.Drawing.Color.Black);
                UInt32Value fillIndex = this.CreateFill(styles, System.Drawing.Color.ForestGreen);
                UInt32Value value4 = CreateCellFormat(styles, fontIndex, fillIndex, 0);
                cell.StyleIndex = value4;
                InlineString newChild = new InlineString();
                Text text2 = new Text
                {
                    Text = text
                };
                newChild.AppendChild<Text>(text2);
                cell.AppendChild<InlineString>(newChild);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return cell;
        }

        private Cell CreateIntegerCell(string header, string text, int index)
        {
            Cell cell2;
            try
            {
                Cell cell = new Cell
                {
                    DataType = CellValues.Number,
                    CellReference = header + index
                };
                CellValue newChild = new CellValue
                {
                    Text = text
                };
                cell.AppendChild<CellValue>(newChild);
                cell2 = cell;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return cell2;
        }

        private Stylesheet CreateStylesheet()
        {
            Stylesheet stylesheet = new Stylesheet();
            Fonts fonts = new Fonts();
            FontName name = new FontName
            {
                Val = "Arial"
            };
            FontSize size = new FontSize
            {
                Val = 11.0
            };
            try
            {
                DocumentFormat.OpenXml.Spreadsheet.Font font = new DocumentFormat.OpenXml.Spreadsheet.Font
                {
                    FontName = name,
                    FontSize = size
                };
                fonts.Append(new OpenXmlElement[] { font });
                fonts.Count = Convert.ToUInt32(fonts.ChildElements.Count);
                Fills fills = new Fills();
                Fill fill = new Fill();
                PatternFill fill2 = new PatternFill
                {
                    PatternType = PatternValues.None
                };
                fill.PatternFill = fill2;
                fills.Append(new OpenXmlElement[] { fill });
                fill = new Fill();
                fill2 = new PatternFill
                {
                    PatternType = PatternValues.Gray125
                };
                fill.PatternFill = fill2;
                fills.Append(new OpenXmlElement[] { fill });
                fills.Count = Convert.ToUInt32(fills.ChildElements.Count);
                Borders borders = new Borders();
                Border border = new Border
                {
                    LeftBorder = new LeftBorder(),
                    RightBorder = new RightBorder(),
                    TopBorder = new TopBorder(),
                    BottomBorder = new BottomBorder(),
                    DiagonalBorder = new DiagonalBorder()
                };
                borders.Append(new OpenXmlElement[] { border });
                borders.Count = Convert.ToUInt32(borders.ChildElements.Count);
                CellStyleFormats formats = new CellStyleFormats();
                CellFormat format = new CellFormat
                {
                    NumberFormatId = 0,
                    FontId = 0,
                    FillId = 0,
                    BorderId = 0
                };
                formats.Append(new OpenXmlElement[] { format });
                formats.Count = Convert.ToUInt32(formats.ChildElements.Count);
                uint num = 0xa4;
                NumberingFormats formats2 = new NumberingFormats();
                CellFormats formats3 = new CellFormats();
                format = new CellFormat
                {
                    NumberFormatId = 0,
                    FontId = 0,
                    FillId = 0,
                    BorderId = 0,
                    FormatId = 0
                };
                formats3.Append(new OpenXmlElement[] { format });
                NumberingFormat format2 = new NumberingFormat
                {
                    NumberFormatId = num,
                    FormatCode = "dd/mm/yyyy hh:mm:ss"
                };
                formats2.Append(new OpenXmlElement[] { format2 });
                format = new CellFormat
                {
                    NumberFormatId = format2.NumberFormatId,
                    FontId = 0,
                    FillId = 0,
                    BorderId = 0,
                    FormatId = 0,
                    ApplyNumberFormat = true
                };
                formats3.Append(new OpenXmlElement[] { format });
                num = 0xa5;
                formats2 = new NumberingFormats();
                formats3 = new CellFormats();
                format = new CellFormat
                {
                    NumberFormatId = 0,
                    FontId = 0,
                    FillId = 0,
                    BorderId = 0,
                    FormatId = 0
                };
                formats3.Append(new OpenXmlElement[] { format });
                format2 = new NumberingFormat
                {
                    NumberFormatId = num,
                    FormatCode = "MMM yyyy"
                };
                formats2.Append(new OpenXmlElement[] { format2 });
                format = new CellFormat
                {
                    NumberFormatId = format2.NumberFormatId,
                    FontId = 0,
                    FillId = 0,
                    BorderId = 0,
                    FormatId = 0,
                    ApplyNumberFormat = true
                };
                formats3.Append(new OpenXmlElement[] { format });
                num = 170;
                format2 = new NumberingFormat
                {
                    NumberFormatId = num,
                    FormatCode = "#,##0.0000"
                };
                formats2.Append(new OpenXmlElement[] { format2 });
                format = new CellFormat
                {
                    NumberFormatId = format2.NumberFormatId,
                    FontId = 0,
                    FillId = 0,
                    BorderId = 0,
                    FormatId = 0,
                    ApplyNumberFormat = true
                };
                formats3.Append(new OpenXmlElement[] { format });
                num = 0xab;
                format2 = new NumberingFormat
                {
                    NumberFormatId = num,
                    FormatCode = "#,##0.00"
                };
                formats2.Append(new OpenXmlElement[] { format2 });
                format = new CellFormat
                {
                    NumberFormatId = format2.NumberFormatId,
                    FontId = 0,
                    FillId = 0,
                    BorderId = 0,
                    FormatId = 0,
                    ApplyNumberFormat = true
                };
                formats3.Append(new OpenXmlElement[] { format });
                num = 0xac;
                format2 = new NumberingFormat
                {
                    NumberFormatId = num,
                    FormatCode = "@"
                };
                formats2.Append(new OpenXmlElement[] { format2 });
                format = new CellFormat
                {
                    NumberFormatId = format2.NumberFormatId,
                    FontId = 0,
                    FillId = 0,
                    BorderId = 0,
                    FormatId = 0,
                    ApplyNumberFormat = true
                };
                formats3.Append(new OpenXmlElement[] { format });
                formats2.Count = Convert.ToUInt32(formats2.ChildElements.Count);
                formats3.Count = Convert.ToUInt32(formats3.ChildElements.Count);
                stylesheet.Append(new OpenXmlElement[] { formats2 });
                stylesheet.Append(new OpenXmlElement[] { fonts });
                stylesheet.Append(new OpenXmlElement[] { fills });
                stylesheet.Append(new OpenXmlElement[] { borders });
                stylesheet.Append(new OpenXmlElement[] { formats });
                stylesheet.Append(new OpenXmlElement[] { formats3 });
                CellStyles styles = new CellStyles();
                CellStyle style = new CellStyle
                {
                    Name = "Normal",
                    FormatId = 0,
                    BuiltinId = 0
                };
                styles.Append(new OpenXmlElement[] { style });
                styles.Count = Convert.ToUInt32(styles.ChildElements.Count);
                stylesheet.Append(new OpenXmlElement[] { styles });
                DifferentialFormats formats4 = new DifferentialFormats
                {
                    Count = 0
                };
                stylesheet.Append(new OpenXmlElement[] { formats4 });
                TableStyles styles2 = new TableStyles
                {
                    Count = 0,
                    DefaultTableStyle = "TableStyleMedium9",
                    DefaultPivotStyle = "PivotStyleLight16"
                };
                stylesheet.Append(new OpenXmlElement[] { styles2 });
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return stylesheet;
        }

        private Cell CreateTextCell(string header, string text, int index)
        {
            Cell cell = new Cell();
            try
            {
                cell.DataType = CellValues.InlineString;
                cell.CellReference = header + index;
                InlineString newChild = new InlineString();
                Text text2 = new Text
                {
                    Text = text
                };
                newChild.AppendChild<Text>(text2);
                cell.AppendChild<InlineString>(newChild);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return cell;
        }

        private int GetExcelSerialDate(DateTime input)
        {
            int day = input.Day;
            int month = input.Month;
            int year = input.Year;
            if (((day == 0x1d) && (month == 2)) && (year == 0x76c))
            {
                return 60;
            }
            long num4 = ((((((0x5b5 * ((year + 0x12c0) + ((month - 14) / 12))) / 4) + ((0x16f * ((month - 2) - (12 * ((month - 14) / 12)))) / 12)) - ((3 * (((year + 0x1324) + ((month - 14) / 12)) / 100)) / 4)) + day) - 0x24d9ab) - 0x7d4b;
            if (num4 < 60L)
            {
                num4 -= 1L;
            }
            return (int)num4;
        }

        private List<string> GetPropertyInfo<T>()
        {
            return (from propertyInfo in typeof(T).GetProperties() select propertyInfo.Name).ToList<string>();
        }

        public List<string> GetRange(List<char> chr, int count)
        {
            List<string> list2;
            try
            {
                List<string> list = new List<string>();
                int num = count / 0x1a;
                int num2 = 0;
                if (num > 0)
                {
                    for (int i = 0; i <= num; i++)
                    {
                        for (int j = 0; j < chr.Count; j++)
                        {
                            if (num2 == count)
                            {
                                return list;
                            }
                            if (i > 0)
                            {
                                list.Add(chr[i - 1].ToString() + chr[j].ToString());
                            }
                            else
                            {
                                list.Add(chr[j].ToString());
                            }
                            num2++;
                        }
                    }
                }
                else
                {
                    for (int k = 0; k < count; k++)
                    {
                        list.Add(chr[k].ToString());
                    }
                }
                list2 = list;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return list2;
        }
    }
}
