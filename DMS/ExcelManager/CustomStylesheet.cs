namespace AA.ExcelManager
{
    using DocumentFormat.OpenXml;
    using DocumentFormat.OpenXml.Spreadsheet;
    using System;

    public class CustomStylesheet : Stylesheet
    {
        public CustomStylesheet()
        {
            Fonts fonts = new Fonts();
            Font font = new Font();
            FontName name = new FontName();
            name.Val = StringValue.FromString("Calibri");
            FontSize size = new FontSize();
            size.Val = DoubleValue.FromDouble(11.0);
            font.FontName = name;
            font.FontSize = size;
            fonts.Append(new OpenXmlElement[] { font });
            font = new Font();
            name = new FontName();
            name.Val = StringValue.FromString("Palatino Linotype");
            size = new FontSize();
            size.Val = DoubleValue.FromDouble(18.0);
            font.FontName = name;
            font.FontSize = size;
            fonts.Append(new OpenXmlElement[] { font });
            fonts.Count = UInt32Value.FromUInt32((uint)fonts.ChildElements.Count);
            Fills fills = new Fills();
            Fill fill = new Fill();
            PatternFill fill2 = new PatternFill();
            fill2.PatternType = PatternValues.None;
            fill.PatternFill = fill2;
            fills.Append(new OpenXmlElement[] { fill });
            fill = new Fill();
            fill2 = new PatternFill();
            fill2.PatternType = PatternValues.Gray125;
            fill.PatternFill = fill2;
            fills.Append(new OpenXmlElement[] { fill });
            fill = new Fill();
            fill2 = new PatternFill();
            fill2.PatternType = PatternValues.Solid;
            fill2.ForegroundColor = new ForegroundColor();
            fill2.ForegroundColor.Rgb = HexBinaryValue.FromString("00ff9728");
            fill2.BackgroundColor = new BackgroundColor();
            fill2.BackgroundColor.Rgb = fill2.ForegroundColor.Rgb;
            fill.PatternFill = fill2;
            fills.Append(new OpenXmlElement[] { fill });
            fills.Count = UInt32Value.FromUInt32((uint)fills.ChildElements.Count);
            Borders borders = new Borders();
            Border border = new Border();
            border.LeftBorder = new LeftBorder();
            border.RightBorder = new RightBorder();
            border.TopBorder = new TopBorder();
            border.BottomBorder = new BottomBorder();
            border.DiagonalBorder = new DiagonalBorder();
            borders.Append(new OpenXmlElement[] { border });
            border = new Border();
            border.LeftBorder = new LeftBorder();
            border.LeftBorder.Style = BorderStyleValues.Thin;
            border.RightBorder = new RightBorder();
            border.RightBorder.Style = BorderStyleValues.Thin;
            border.TopBorder = new TopBorder();
            border.TopBorder.Style = BorderStyleValues.Thin;
            border.BottomBorder = new BottomBorder();
            border.BottomBorder.Style = BorderStyleValues.Thin;
            border.DiagonalBorder = new DiagonalBorder();
            borders.Append(new OpenXmlElement[] { border });
            border = new Border();
            border.LeftBorder = new LeftBorder();
            border.RightBorder = new RightBorder();
            border.TopBorder = new TopBorder();
            border.TopBorder.Style = BorderStyleValues.Thin;
            border.BottomBorder = new BottomBorder();
            border.BottomBorder.Style = BorderStyleValues.Thin;
            border.DiagonalBorder = new DiagonalBorder();
            borders.Append(new OpenXmlElement[] { border });
            borders.Count = UInt32Value.FromUInt32((uint)borders.ChildElements.Count);
            CellStyleFormats formats = new CellStyleFormats();
            CellFormat format = new CellFormat();
            format.NumberFormatId = 0;
            format.FontId = 0;
            format.FillId = 0;
            format.BorderId = 0;
            formats.Append(new OpenXmlElement[] { format });
            formats.Count = UInt32Value.FromUInt32((uint)formats.ChildElements.Count);
            uint num = 0xa4;
            NumberingFormats formats2 = new NumberingFormats();
            CellFormats formats3 = new CellFormats();
            format = new CellFormat();
            format.NumberFormatId = 0;
            format.FontId = 0;
            format.FillId = 0;
            format.BorderId = 0;
            format.FormatId = 0;
            formats3.Append(new OpenXmlElement[] { format });
            NumberingFormat format2 = new NumberingFormat();
            format2.NumberFormatId = UInt32Value.FromUInt32(num++);
            format2.FormatCode = StringValue.FromString("dd/mm/yyyy hh:mm:ss");
            formats2.Append(new OpenXmlElement[] { format2 });
            NumberingFormat format3 = new NumberingFormat();
            format3.NumberFormatId = UInt32Value.FromUInt32(num++);
            format3.FormatCode = StringValue.FromString("#,##0.0000");
            formats2.Append(new OpenXmlElement[] { format3 });
            NumberingFormat format4 = new NumberingFormat();
            format4.NumberFormatId = UInt32Value.FromUInt32(num++);
            format4.FormatCode = StringValue.FromString("#,##0.00");
            formats2.Append(new OpenXmlElement[] { format4 });
            NumberingFormat format5 = new NumberingFormat();
            format5.NumberFormatId = UInt32Value.FromUInt32(num++);
            format5.FormatCode = StringValue.FromString("@");
            formats2.Append(new OpenXmlElement[] { format5 });
            format = new CellFormat();
            format.NumberFormatId = 14;
            format.FontId = 0;
            format.FillId = 0;
            format.BorderId = 0;
            format.FormatId = 0;
            format.ApplyNumberFormat = BooleanValue.FromBoolean(true);
            formats3.Append(new OpenXmlElement[] { format });
            format = new CellFormat
            {
                NumberFormatId = 4,
                FontId = 0,
                FillId = 0,
                BorderId = 0,
                FormatId = 0,
                ApplyNumberFormat = BooleanValue.FromBoolean(true)
            };
            formats3.Append(new OpenXmlElement[] { format });
            format = new CellFormat
            {
                NumberFormatId = format2.NumberFormatId,
                FontId = 0,
                FillId = 0,
                BorderId = 0,
                FormatId = 0,
                ApplyNumberFormat = BooleanValue.FromBoolean(true)
            };
            formats3.Append(new OpenXmlElement[] { format });
            format = new CellFormat
            {
                NumberFormatId = format3.NumberFormatId,
                FontId = 0,
                FillId = 0,
                BorderId = 0,
                FormatId = 0,
                ApplyNumberFormat = BooleanValue.FromBoolean(true)
            };
            formats3.Append(new OpenXmlElement[] { format });
            format = new CellFormat
            {
                NumberFormatId = format4.NumberFormatId,
                FontId = 0,
                FillId = 0,
                BorderId = 0,
                FormatId = 0,
                ApplyNumberFormat = BooleanValue.FromBoolean(true)
            };
            formats3.Append(new OpenXmlElement[] { format });
            format = new CellFormat
            {
                NumberFormatId = format5.NumberFormatId,
                FontId = 0,
                FillId = 0,
                BorderId = 0,
                FormatId = 0,
                ApplyNumberFormat = BooleanValue.FromBoolean(true)
            };
            formats3.Append(new OpenXmlElement[] { format });
            format = new CellFormat
            {
                NumberFormatId = format5.NumberFormatId,
                FontId = 1,
                FillId = 0,
                BorderId = 0,
                FormatId = 0,
                ApplyNumberFormat = BooleanValue.FromBoolean(true)
            };
            formats3.Append(new OpenXmlElement[] { format });
            format = new CellFormat
            {
                NumberFormatId = format5.NumberFormatId,
                FontId = 0,
                FillId = 0,
                BorderId = 1,
                FormatId = 0,
                ApplyNumberFormat = BooleanValue.FromBoolean(true)
            };
            formats3.Append(new OpenXmlElement[] { format });
            format = new CellFormat
            {
                NumberFormatId = format4.NumberFormatId,
                FontId = 0,
                FillId = 2,
                BorderId = 2,
                FormatId = 0,
                ApplyNumberFormat = BooleanValue.FromBoolean(true)
            };
            formats3.Append(new OpenXmlElement[] { format });
            format = new CellFormat
            {
                NumberFormatId = format5.NumberFormatId,
                FontId = 0,
                FillId = 2,
                BorderId = 2,
                FormatId = 0,
                ApplyNumberFormat = BooleanValue.FromBoolean(true)
            };
            formats3.Append(new OpenXmlElement[] { format });
            formats2.Count = UInt32Value.FromUInt32((uint)formats2.ChildElements.Count);
            formats3.Count = UInt32Value.FromUInt32((uint)formats3.ChildElements.Count);
            base.Append(new OpenXmlElement[] { formats2 });
            base.Append(new OpenXmlElement[] { fonts });
            base.Append(new OpenXmlElement[] { fills });
            base.Append(new OpenXmlElement[] { borders });
            base.Append(new OpenXmlElement[] { formats });
            base.Append(new OpenXmlElement[] { formats3 });
            CellStyles styles = new CellStyles();
            CellStyle style = new CellStyle
            {
                Name = StringValue.FromString("Normal"),
                FormatId = 0,
                BuiltinId = 0
            };
            styles.Append(new OpenXmlElement[] { style });
            styles.Count = UInt32Value.FromUInt32((uint)styles.ChildElements.Count);
            base.Append(new OpenXmlElement[] { styles });
            DifferentialFormats formats4 = new DifferentialFormats
            {
                Count = 0
            };
            base.Append(new OpenXmlElement[] { formats4 });
            TableStyles styles2 = new TableStyles
            {
                Count = 0,
                DefaultTableStyle = StringValue.FromString("TableStyleMedium9"),
                DefaultPivotStyle = StringValue.FromString("PivotStyleLight16")
            };
            base.Append(new OpenXmlElement[] { styles2 });
        }
    }
}
