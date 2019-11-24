using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;

namespace ITM.ExcelGenerator
{
    public class TextCell : Cell
    {
        public TextCell(string header, string text, int index, Stylesheet styleSheet, System.Drawing.Color fillColour)
        {
            
            this.DataType = CellValues.InlineString;
            this.CellReference = header + index;
            //Add text to the text cell.
            this.InlineString = new InlineString { Text = new Text { Text = text } };

            UInt32Value fillId = CreateFill(styleSheet, fillColour);
            UInt32Value formatId = CreateCellFormat(styleSheet, fillId);
            this.StyleIndex = formatId;
        }

        private static UInt32Value CreateCellFormat(Stylesheet styleSheet, UInt32Value fillIndex)
        {
            CellFormat cellFormat = new CellFormat();

            if (fillIndex != null)
                cellFormat.FillId = fillIndex;

            //allignment
            Alignment a = new Alignment();
            a.WrapText = true;
            a.Vertical = VerticalAlignmentValues.Center;

            // add allignment
            cellFormat.Alignment = a;
            cellFormat.ApplyAlignment = true;

            //boreder
            Borders borders = new Borders();
            Border border = new Border();
            
            border = new Border();
            border.LeftBorder = new LeftBorder();
            border.LeftBorder.Style = BorderStyleValues.Medium;
            border.RightBorder = new RightBorder();
            border.RightBorder.Style = BorderStyleValues.Medium;
            border.TopBorder = new TopBorder();
            border.TopBorder.Style = BorderStyleValues.Medium;
            border.BottomBorder = new BottomBorder();
            border.BottomBorder.Style = BorderStyleValues.Medium;
            border.DiagonalBorder = new DiagonalBorder();

            borders.Append(border);
            borders.Count = UInt32Value.FromUInt32((uint)borders.ChildElements.Count);
            //

            cellFormat.BorderId = borders.Count;
            //cellFormat.BorderId = UInt32Value.FromUInt32((uint)border.Count());

            styleSheet.CellFormats.Append(cellFormat);

            UInt32Value result = styleSheet.CellFormats.Count;
            styleSheet.CellFormats.Count++;
            return result;
        }

        private static UInt32Value CreateFill(Stylesheet styleSheet, System.Drawing.Color fillColor)
        {
            PatternFill patternFill =
                new PatternFill(
                    new ForegroundColor()
                    {
                        Rgb = new HexBinaryValue()
                        {
                            Value =
                            System.Drawing.ColorTranslator.ToHtml(
                                System.Drawing.Color.FromArgb(
                                    fillColor.A,
                                    fillColor.R,
                                    fillColor.G,
                                    fillColor.B)).Replace("#", "")
                        }
                    });
            
            patternFill.PatternType = PatternValues.Solid;

            Fill fill = new Fill(patternFill);

            styleSheet.Fills.Append(fill);

            UInt32Value result = styleSheet.Fills.Count;
            styleSheet.Fills.Count++;
            return result;
        }

        private static UInt32Value CreateFont(Stylesheet styleSheet, string fontName, double? fontSize, bool isBold, System.Drawing.Color foreColor)
        {

            Font font = new Font();

            if (!string.IsNullOrEmpty(fontName))
            {
                FontName name = new FontName()
                {
                    Val = fontName
                };
                font.Append(name);
            }

            if (fontSize.HasValue)
            {
                FontSize size = new FontSize()
                {
                    Val = fontSize.Value
                };
                font.Append(size);
            }

            if (isBold == true)
            {
                Bold bold = new Bold();
                font.Append(bold);
            }


            Color color = new Color()
            {
                Rgb = new HexBinaryValue()
                {
                    Value =
                        System.Drawing.ColorTranslator.ToHtml(
                            System.Drawing.Color.FromArgb(
                                foreColor.A,
                                foreColor.R,
                                foreColor.G,
                                foreColor.B)).Replace("#", "")
                }
            };
            font.Append(color);

            styleSheet.Fonts.Append(font);
            UInt32Value result = styleSheet.Fonts.Count;
            styleSheet.Fonts.Count++;
            return result;
        }
    }
}
