using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using System.Reflection;
using System.Data;
using System.Windows.Forms;

namespace AdmissionForm.API.Helper
{
    public class xmlHeaderHelper
    {

        public static bool BuildWorkbook(string filename, DataTable dt, string Reportname)
        {
            try
            {
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }
                using (SpreadsheetDocument xl = SpreadsheetDocument.Create(filename, SpreadsheetDocumentType.Workbook))
                {
                    WorkbookPart wbp = xl.AddWorkbookPart();
                    WorksheetPart wsp = wbp.AddNewPart<WorksheetPart>();
                    Workbook wb = new Workbook();
                    FileVersion fv = new FileVersion();
                    fv.ApplicationName = "Microsoft Office Excel";
                    Worksheet ws = new Worksheet();


                    SheetData sd = new SheetData();

                    WorkbookStylesPart wbsp = wbp.AddNewPart<WorkbookStylesPart>();
                    wbsp.Stylesheet = CreateStylesheet();
                    wbsp.Stylesheet.Save();

                    string sImagePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/logo.png");
                    DrawingsPart dp = wsp.AddNewPart<DrawingsPart>();
                    ImagePart imgp = dp.AddImagePart(ImagePartType.Png, wsp.GetIdOfPart(dp));
                    using (FileStream fs = new FileStream(sImagePath, FileMode.Open))
                    {
                        imgp.FeedData(fs);
                    }

                    NonVisualDrawingProperties nvdp = new NonVisualDrawingProperties();
                    nvdp.Id = 1025;
                    nvdp.Name = "Picture 1";
                    nvdp.Description = "polymathlogo";
                    DocumentFormat.OpenXml.Drawing.PictureLocks picLocks = new DocumentFormat.OpenXml.Drawing.PictureLocks();
                    picLocks.NoChangeAspect = true;
                    picLocks.NoChangeArrowheads = true;
                    NonVisualPictureDrawingProperties nvpdp = new NonVisualPictureDrawingProperties();
                    nvpdp.PictureLocks = picLocks;
                    NonVisualPictureProperties nvpp = new NonVisualPictureProperties();
                    nvpp.NonVisualDrawingProperties = nvdp;
                    nvpp.NonVisualPictureDrawingProperties = nvpdp;

                    DocumentFormat.OpenXml.Drawing.Stretch stretch = new DocumentFormat.OpenXml.Drawing.Stretch();
                    stretch.FillRectangle = new DocumentFormat.OpenXml.Drawing.FillRectangle();

                    BlipFill blipFill = new BlipFill();
                    DocumentFormat.OpenXml.Drawing.Blip blip = new DocumentFormat.OpenXml.Drawing.Blip();
                    blip.Embed = dp.GetIdOfPart(imgp);
                    blip.CompressionState = DocumentFormat.OpenXml.Drawing.BlipCompressionValues.Print;
                    blipFill.Blip = blip;
                    blipFill.SourceRectangle = new DocumentFormat.OpenXml.Drawing.SourceRectangle();
                    blipFill.Append(stretch);

                    DocumentFormat.OpenXml.Drawing.Transform2D t2d = new DocumentFormat.OpenXml.Drawing.Transform2D();
                    DocumentFormat.OpenXml.Drawing.Offset offset = new DocumentFormat.OpenXml.Drawing.Offset();
                    offset.X = 2;
                    offset.Y = 2;
                    t2d.Offset = offset;
                    Bitmap bm = new Bitmap(sImagePath);
                    //http://en.wikipedia.org/wiki/English_Metric_Unit#DrawingML
                    //http://stackoverflow.com/questions/1341930/pixel-to-centimeter
                    //http://stackoverflow.com/questions/139655/how-to-convert-pixels-to-points-px-to-pt-in-net-c
                    DocumentFormat.OpenXml.Drawing.Extents extents = new DocumentFormat.OpenXml.Drawing.Extents();
                    extents.Cx = (long)bm.Width * (long)((float)910000 / bm.HorizontalResolution);
                    //extents.Cx = (long)bm.Width * (long)((float)914400 / bm.HorizontalResolution);
                    extents.Cy = (long)bm.Height * (long)((float)910000 / bm.VerticalResolution);
                    bm.Dispose();
                    t2d.Extents = extents;
                    ShapeProperties sp = new ShapeProperties();
                    sp.BlackWhiteMode = DocumentFormat.OpenXml.Drawing.BlackWhiteModeValues.Auto;
                    sp.Transform2D = t2d;
                    DocumentFormat.OpenXml.Drawing.PresetGeometry prstGeom = new DocumentFormat.OpenXml.Drawing.PresetGeometry();
                    prstGeom.Preset = DocumentFormat.OpenXml.Drawing.ShapeTypeValues.Rectangle;
                    prstGeom.AdjustValueList = new DocumentFormat.OpenXml.Drawing.AdjustValueList();
                    sp.Append(prstGeom);
                    sp.Append(new DocumentFormat.OpenXml.Drawing.NoFill());

                    DocumentFormat.OpenXml.Drawing.Spreadsheet.Picture picture = new DocumentFormat.OpenXml.Drawing.Spreadsheet.Picture();
                    picture.NonVisualPictureProperties = nvpp;
                    picture.BlipFill = blipFill;
                    picture.ShapeProperties = sp;

                    Position pos = new Position();
                    pos.X = 3;
                    pos.Y = 3;
                    Extent ext = new Extent();
                    ext.Cx = extents.Cx;
                    ext.Cy = extents.Cy;
                    AbsoluteAnchor anchor = new AbsoluteAnchor();
                    anchor.Position = pos;
                    anchor.Extent = ext;
                    anchor.Append(picture);
                    anchor.Append(new ClientData());
                    WorksheetDrawing wsd = new WorksheetDrawing();
                    wsd.Append(anchor);
                    Drawing drawing = new Drawing();
                    drawing.Id = dp.GetIdOfPart(imgp);

                    wsd.Save(dp);

                    UInt32 index;
                    Random rand = new Random();

                    //sd.Append(CreateHeader(1, 9, "H", "Sir Vithaldas Thackersey College of Home Science (Autonomous)"));
                    //sd.Append(CreateHeader(2, 9, "J", "SNDT Women's University, Mumbai"));
                    //sd.Append(CreateHeader(3, 9, "K", "General Report"));

                    sd.Append(CreateColumnHeader(2, dt));
                    int CountofRow = 0;
                    for (index = 3; index < (dt.Rows.Count + 3); ++index)
                    {
                        DataRow rowdata = dt.Rows[CountofRow];
                        sd.Append(CreateContent(index, rowdata));
                        CountofRow = CountofRow + 1;
                    }

                    Columns columns2 = AutoSize(sd);
                    ws.Append(columns2);
                    ws.Append(sd);

                    HeaderFooter headerFooter = new HeaderFooter();
                    headerFooter = AddHeader(wsp, sImagePath, Reportname);
                    ws.Append(headerFooter);

                    //ws.Append(drawing);
                    wsp.Worksheet = ws;
                    wsp.Worksheet.Save();

                    Sheets sheets = new Sheets();
                    Sheet sheet = new Sheet();
                    sheet.Name = "Report";
                    sheet.SheetId = 1;
                    sheet.Id = wbp.GetIdOfPart(wsp);
                    sheets.Append(sheet);
                    wb.Append(fv);
                    wb.Append(sheets);

                    xl.WorkbookPart.Workbook = wb;
                    xl.WorkbookPart.Workbook.Save();
                    xl.Close();
                }
                return true;
            }
            catch (Exception e)
            {
                throw e;

            }
        }

        private static DefinedName getDefinedName(string Name, string Text, string Comment)
        {
            DefinedName dn = new DefinedName();
            dn.Text = Text;
            dn.Name = Name;
            dn.Comment = Comment;
            dn.LocalSheetId = (UInt32Value)1U;
            dn.Xlm = true; // since its a special schema name
            return dn;
        }

        private static HeaderFooter AddHeader(WorksheetPart wsp, string imgPath, string Reportname)
        {

            var headerFooter = new HeaderFooter();

            var oddHeader = new OddHeader();

            var oddFooter = new OddFooter();

            //string sImagePath = "C:\\MyWork\\logo.png";
            //Image image = Image.FromFile(sImagePath);


            /*************** Header Section **********************/

            // Want to insert image into right side of the Header

            oddHeader.Text = "&C&\"Verdana,Bold\"&10Sir Vithaldas Thackersey College of Home Science (Autonomous) \n SNDT Women's University, Mumbai \n " + Reportname;

            //oddHeader.Text = "&R&G";

            //var imageRelationshipId = GetImageRelationshipId(wsp, imgPath);

            //LegacyDrawingHeaderFooter legacyDrawingHeaderFooter = new LegacyDrawingHeaderFooter { Id = imageRelationshipId };

            /*************** End Header Section **********************/

            /*************** Footer Section **********************/

            var stringBuilder = new StringBuilder();

            //stringBuilder.Append("&L&\"Times New Roman,Regular\"&7© Avex Aviation Experts AG ");

            //stringBuilder.Append("\r\r\n&L&\"Times New Roman,Regular\"&7Ringstrasse 26 – CH-8317 Tagelswangen / Switzerland");

            //stringBuilder.Append("\r\n&L&\"Times New Roman,Regular\"&7Phone: +41 44 830 51 08 – qperfect@avex.ch – www.avex.ch");

            //stringBuilder.Append("\r\n&L&\"Times New Roman,Regular\"&7Phone: +41 44 830 51 08 – qperfect@avex.ch - www");

            stringBuilder.Append("&R&\"Times New Roman,Regular\"&P of &N");

            oddFooter.Text = stringBuilder.ToString();

            /*************** End Footer Section **********************/

            headerFooter.Append(oddHeader);

            headerFooter.Append(oddFooter);


            return headerFooter;

        }

        private static string GetImageRelationshipId(WorksheetPart wsp, string imgPath)
        {

            DrawingsPart dp = wsp.AddNewPart<DrawingsPart>();

            ImagePart imgp = dp.AddImagePart(ImagePartType.Jpeg, wsp.GetIdOfPart(dp));

            using (FileStream fs = new FileStream(imgPath, FileMode.Open))
            {

                imgp.FeedData(fs);

            }

            return wsp.GetIdOfPart(dp);

        }

        private static Columns AutoSize(SheetData sheetData)
        {
            try
            {
                var maxColWidth = GetMaxCharacterWidth(sheetData);

                Columns columns = new Columns();
                //this is the width of my font - yours may be different
                double maxWidth = 7;
                foreach (var item in maxColWidth)
                {
                    //width = Truncate([{Number of Characters} * {Maximum Digit Width} + {5 pixel padding}]/{Maximum Digit Width}*256)/256
                    double width = Math.Truncate((item.Value * maxWidth + 5) / maxWidth * 256) / 256;

                    //pixels=Truncate(((256 * {width} + Truncate(128/{Maximum Digit Width}))/256)*{Maximum Digit Width})
                    double pixels = Math.Truncate(((256 * width + Math.Truncate(128 / maxWidth)) / 256) * maxWidth);

                    //character width=Truncate(({pixels}-5)/{Maximum Digit Width} * 100+0.5)/100
                    double charWidth = Math.Truncate((pixels - 5) / maxWidth * 100 + 0.5) / 100;

                    Column col = new Column() { BestFit = true, Min = (UInt32)(item.Key + 1), Max = (UInt32)(item.Key + 1), CustomWidth = true, Width = (DoubleValue)width };
                    columns.Append(col);
                }

                return columns;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        private static Dictionary<int, int> GetMaxCharacterWidth(SheetData sheetData)
        {
            try
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
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private static double GetWidth(string font, int fontSize, string text)
        {
            System.Drawing.Font stringFont = new System.Drawing.Font(font, fontSize);
            return GetWidth(stringFont, text);
        }

        private static double GetWidth(System.Drawing.Font stringFont, string text)
        {
            // This formula is based on this article plus a nudge ( + 0.2M )
            // http://msdn.microsoft.com/en-us/library/documentformat.openxml.spreadsheet.column.width.aspx
            // Truncate(((256 * Solve_For_This + Truncate(128 / 7)) / 256) * 7) = DeterminePixelsOfString

            Size textSize = TextRenderer.MeasureText(text, stringFont);
            double width = (double)(((textSize.Width / (double)7) * 256) - (128 / 7)) / 256;
            width = (double)decimal.Round((decimal)width + 0.2M, 2);

            return width;
        }


        private static Stylesheet CreateStylesheet()
        {
            Stylesheet ss = new Stylesheet();

            Fonts fts = new Fonts();
            DocumentFormat.OpenXml.Spreadsheet.Font ft = new DocumentFormat.OpenXml.Spreadsheet.Font();
            FontName ftn = new FontName();
            ftn.Val = StringValue.FromString("Calibri");
            FontSize ftsz = new FontSize();
            ftsz.Val = DoubleValue.FromDouble(11);
            ft.FontName = ftn;
            ft.FontSize = ftsz;
            fts.Append(ft);

            ft = new DocumentFormat.OpenXml.Spreadsheet.Font();
            ftn = new FontName();
            ftn.Val = StringValue.FromString("Calibri");
            ftsz = new FontSize();
            ftsz.Val = DoubleValue.FromDouble(16);
            ft.FontName = ftn;
            ft.FontSize = ftsz;
            fts.Append(ft);

            ft = new DocumentFormat.OpenXml.Spreadsheet.Font();
            ftn = new FontName();
            ftn.Val = StringValue.FromString("Calibri");
            ftsz = new FontSize();
            ftsz.Val = DoubleValue.FromDouble(18);
            ft.FontName = ftn;
            ft.FontSize = ftsz;
            fts.Append(ft);
            fts.Count = UInt32Value.FromUInt32((uint)fts.ChildElements.Count);

            Fills fills = new Fills();
            Fill fill;
            PatternFill patternFill;
            fill = new Fill();
            patternFill = new PatternFill();
            patternFill.PatternType = PatternValues.None;
            fill.PatternFill = patternFill;
            fills.Append(fill);

            fill = new Fill();
            patternFill = new PatternFill();
            patternFill.PatternType = PatternValues.Gray125;
            fill.PatternFill = patternFill;
            fills.Append(fill);

            fill = new Fill();
            patternFill = new PatternFill();
            patternFill.PatternType = PatternValues.Solid;
            patternFill.ForegroundColor = new ForegroundColor();
            patternFill.ForegroundColor.Rgb = HexBinaryValue.FromString("00ff9728");
            patternFill.BackgroundColor = new BackgroundColor();
            patternFill.BackgroundColor.Rgb = patternFill.ForegroundColor.Rgb;
            fill.PatternFill = patternFill;
            fills.Append(fill);

            fills.Count = UInt32Value.FromUInt32((uint)fills.ChildElements.Count);

            Borders borders = new Borders();
            Border border = new Border();
            border.LeftBorder = new LeftBorder();
            border.RightBorder = new RightBorder();
            border.TopBorder = new TopBorder();
            border.BottomBorder = new BottomBorder();
            border.DiagonalBorder = new DiagonalBorder();
            borders.Append(border);

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
            borders.Append(border);
            borders.Count = UInt32Value.FromUInt32((uint)borders.ChildElements.Count);

            CellStyleFormats csfs = new CellStyleFormats();
            CellFormat cf = new CellFormat();
            cf.NumberFormatId = 0;
            cf.FontId = 0;
            cf.FillId = 0;
            cf.BorderId = 0;
            csfs.Append(cf);
            csfs.Count = UInt32Value.FromUInt32((uint)csfs.ChildElements.Count);

            uint iExcelIndex = 164;


            NumberingFormats nfs = new NumberingFormats();
            CellFormats cfs = new CellFormats();

            cf = new CellFormat();
            cf.NumberFormatId = 0;
            cf.FontId = 0;
            cf.FillId = 0;
            cf.BorderId = 0;
            cf.FormatId = 0;
            cfs.Append(cf);

            NumberingFormat nfDateTime = new NumberingFormat();
            nfDateTime.NumberFormatId = UInt32Value.FromUInt32(iExcelIndex++);
            nfDateTime.FormatCode = StringValue.FromString("dd/mm/yyyy hh:mm:ss");
            nfs.Append(nfDateTime);

            NumberingFormat nf4decimal = new NumberingFormat();
            nf4decimal.NumberFormatId = UInt32Value.FromUInt32(iExcelIndex++);
            nf4decimal.FormatCode = StringValue.FromString("#,##0.0000");
            nfs.Append(nf4decimal);

            // #,##0.00 is also Excel style index 4
            NumberingFormat nf2decimal = new NumberingFormat();
            nf2decimal.NumberFormatId = UInt32Value.FromUInt32(iExcelIndex++);
            nf2decimal.FormatCode = StringValue.FromString("#,##0.00");
            nfs.Append(nf2decimal);

            // @ is also Excel style index 49
            NumberingFormat nfForcedText = new NumberingFormat();
            nfForcedText.NumberFormatId = UInt32Value.FromUInt32(iExcelIndex++);
            nfForcedText.FormatCode = StringValue.FromString("@");
            nfs.Append(nfForcedText);

            // index 1
            cf = new CellFormat();
            cf.NumberFormatId = nfDateTime.NumberFormatId;
            cf.FontId = 0;
            cf.FillId = 0;
            cf.BorderId = 0;
            cf.FormatId = 0;
            cf.ApplyNumberFormat = BooleanValue.FromBoolean(true);
            cfs.Append(cf);

            // index 2
            cf = new CellFormat();
            cf.NumberFormatId = nf4decimal.NumberFormatId;
            cf.FontId = 0;
            cf.FillId = 0;
            cf.BorderId = 0;
            cf.FormatId = 0;
            cf.ApplyNumberFormat = BooleanValue.FromBoolean(true);
            cfs.Append(cf);

            // index 3
            cf = new CellFormat();
            cf.NumberFormatId = nf2decimal.NumberFormatId;
            cf.FontId = 0;
            cf.FillId = 0;
            cf.BorderId = 0;
            cf.FormatId = 0;
            cf.ApplyNumberFormat = BooleanValue.FromBoolean(true);
            cfs.Append(cf);

            // index 4
            cf = new CellFormat();
            cf.NumberFormatId = nfForcedText.NumberFormatId;
            cf.FontId = 0;
            cf.FillId = 0;
            cf.BorderId = 0;
            cf.FormatId = 0;
            cf.ApplyNumberFormat = BooleanValue.FromBoolean(true);
            cfs.Append(cf);

            // index 5
            // Header text
            cf = new CellFormat();
            cf.NumberFormatId = nfForcedText.NumberFormatId;
            cf.FontId = 1;
            cf.FillId = 0;
            cf.BorderId = 0;
            cf.FormatId = 0;
            cf.ApplyNumberFormat = BooleanValue.FromBoolean(true);
            cfs.Append(cf);

            // index 6
            // column text
            cf = new CellFormat();
            cf.NumberFormatId = nfForcedText.NumberFormatId;
            cf.FontId = 0;
            cf.FillId = 0;
            cf.BorderId = 1;
            cf.FormatId = 0;
            cf.ApplyNumberFormat = BooleanValue.FromBoolean(true);
            cfs.Append(cf);

            // index 7
            // coloured 2 decimal text
            cf = new CellFormat();
            cf.NumberFormatId = nf2decimal.NumberFormatId;
            cf.FontId = 0;
            cf.FillId = 2;
            cf.BorderId = 0;
            cf.FormatId = 0;
            cf.ApplyNumberFormat = BooleanValue.FromBoolean(true);
            cfs.Append(cf);

            // index 8
            // coloured column text
            cf = new CellFormat();
            cf.NumberFormatId = nfForcedText.NumberFormatId;
            cf.FontId = 0;
            cf.FillId = 2;
            cf.BorderId = 1;
            cf.FormatId = 0;
            cf.ApplyNumberFormat = BooleanValue.FromBoolean(true);
            cfs.Append(cf);

            // index 9
            // Font size 16
            cf = new CellFormat();
            cf.NumberFormatId = nfForcedText.NumberFormatId;
            cf.FontId = 2;
            cf.FillId = 0;
            cf.BorderId = 0;
            cf.FormatId = 0;
            cf.ApplyNumberFormat = BooleanValue.FromBoolean(true);
            cfs.Append(cf);

            // index 10
            // Font size 18
            cf = new CellFormat();
            cf.NumberFormatId = nfForcedText.NumberFormatId;
            cf.FontId = 3;
            cf.FillId = 0;
            cf.BorderId = 0;
            cf.FormatId = 0;
            cf.ApplyNumberFormat = BooleanValue.FromBoolean(true);
            cfs.Append(cf);

            nfs.Count = UInt32Value.FromUInt32((uint)nfs.ChildElements.Count);
            cfs.Count = UInt32Value.FromUInt32((uint)cfs.ChildElements.Count);

            ss.Append(nfs);
            ss.Append(fts);
            ss.Append(fills);
            ss.Append(borders);
            ss.Append(csfs);
            ss.Append(cfs);

            CellStyles css = new CellStyles();
            CellStyle cs = new CellStyle();
            cs.Name = StringValue.FromString("Normal");
            cs.FormatId = 0;
            cs.BuiltinId = 0;
            css.Append(cs);
            css.Count = UInt32Value.FromUInt32((uint)css.ChildElements.Count);
            ss.Append(css);

            DifferentialFormats dfs = new DifferentialFormats();
            dfs.Count = 0;
            ss.Append(dfs);

            TableStyles tss = new TableStyles();
            tss.Count = 0;
            tss.DefaultTableStyle = StringValue.FromString("TableStyleMedium9");
            tss.DefaultPivotStyle = StringValue.FromString("PivotStyleLight16");
            ss.Append(tss);

            return ss;
        }

        private static Row CreateHeader(UInt32 index, UInt32Value styleindex, string cellref, string cellvalue)
        {
            Row r = new Row();
            r.RowIndex = index;
            Cell c = new Cell();
            c.DataType = CellValues.String;
            c.StyleIndex = styleindex;
            c.CellReference = cellref + index.ToString();
            c.CellValue = new CellValue(cellvalue);
            r.Append(c);
            return r;
        }

        private static Row CreateColumnHeader(UInt32 index, DataTable dt)
        {
            Row r = new Row();

            r.RowIndex = index;
            Cell c;
            char Alpha = 'A';
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                c = new Cell();
                c.DataType = CellValues.String;
                c.StyleIndex = 2;
                c.CellReference = Alpha + index.ToString();
                c.CellValue = new CellValue(dt.Columns[i].ColumnName.ToString());
                r.Append(c);
                Alpha++;
            }
            return r;
        }

        private static Row CreateContent(UInt32 index, DataRow rowdata)
        {
            Row r = new Row();
            r.RowIndex = index;

            char Alpha = 'A';
            foreach (var row in rowdata.ItemArray)
            {
                Cell c;
                c = new Cell();
                c.DataType = CellValues.String;
                c.StyleIndex = 2;
                c.CellReference = Alpha + index.ToString();
                c.CellValue = new CellValue(row.ToString());
                r.Append(c);
                Alpha++;
            }
            return r;
        }

        private static PageMargins SetPageMargin()
        {
            PageMargins pageMargins1 = new PageMargins();
            pageMargins1.Left = 0.1D;
            pageMargins1.Right = 0.1D;
            pageMargins1.Top = 0.1D;
            pageMargins1.Bottom = 0.1D;
            pageMargins1.Header = 0.3D;
            pageMargins1.Footer = 0.3D;
            return pageMargins1;
        }
        private static PageSetup SetPageSetup()
        {
            PageSetup pageSetup = new PageSetup();
            pageSetup.Orientation = OrientationValues.Landscape;
            pageSetup.FitToHeight = 2;
            pageSetup.HorizontalDpi = 200;
            pageSetup.VerticalDpi = 200;
            return pageSetup;
        }

        #region Not Used method

        #region Commented
        #region Properties

        private UInt32Value _numberStyleId;
        private UInt32Value _doubleStyleId;
        private UInt32Value _dateStyleId;

        #endregion

        #region Public Methods



        #endregion

        #region WorkBook Methods

        /// <summary>
        /// Creates a new font and appends it to the workbook's stylesheet
        /// </summary>
        /// <param name="styleSheet">The stylesheet for the current WorkBook</param>
        /// <param name="fontName">The font name.</param>
        /// <param name="fontSize">The font size.</param>
        /// <param name="isBold">Set to true for bold font.</param>
        /// <param name="foreColor">The font color.</param>
        /// <returns>The index of the font.</returns>
        private UInt32Value createFont(Stylesheet styleSheet, string fontName, Nullable<double> fontSize, bool isBold, System.Drawing.Color foreColor)
        {

            DocumentFormat.OpenXml.Spreadsheet.Font font = new DocumentFormat.OpenXml.Spreadsheet.Font();

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

            if (foreColor != null)
            {
                DocumentFormat.OpenXml.Spreadsheet.Color color = new DocumentFormat.OpenXml.Spreadsheet.Color()
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
            }
            styleSheet.Fonts.Append(font);
            UInt32Value result = styleSheet.Fonts.Count;
            styleSheet.Fonts.Count++;
            return result;
        }

        /// <summary>
        /// Creates a new Fill object and appends it to the WorkBook's stylesheet.
        /// </summary>
        /// <param name="styleSheet">The stylesheet for the current WorkBook.</param>
        /// <param name="fillColor">The background color for the fill.</param>
        /// <returns></returns>
        private UInt32Value createFill(Stylesheet styleSheet, System.Drawing.Color fillColor)
        {
            Fill fill = new Fill(
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
                    })
                {
                    PatternType = PatternValues.Solid
                }
            );
            styleSheet.Fills.Append(fill);

            UInt32Value result = styleSheet.Fills.Count;
            styleSheet.Fills.Count++;
            return result;
        }

        private UInt32Value createCellFormat(Stylesheet styleSheet, UInt32Value fontIndex, UInt32Value fillIndex, UInt32Value numberFormatId)
        {
            CellFormat cellFormat = new CellFormat();

            if (fontIndex != null)
                cellFormat.FontId = fontIndex;

            if (fillIndex != null)
                cellFormat.FillId = fillIndex;

            if (numberFormatId != null)
            {
                cellFormat.NumberFormatId = numberFormatId;
                cellFormat.ApplyNumberFormat = BooleanValue.FromBoolean(true);
            }

            styleSheet.CellFormats.Append(cellFormat);

            UInt32Value result = styleSheet.CellFormats.Count;
            styleSheet.CellFormats.Count++;
            return result;
        }

        /// <summary>
        /// Gets the Excel column name based on a supplied index number.
        /// </summary>
        /// <returns>1 = A, 2 = B... 27 = AA, etc.</returns>
        private string getColumnName(int columnIndex)
        {
            int dividend = columnIndex;
            string columnName = String.Empty;
            int modifier;

            while (dividend > 0)
            {
                modifier = (dividend - 1) % 26;
                columnName =
                    Convert.ToChar(65 + modifier).ToString() + columnName;
                dividend = (int)((dividend - modifier) / 26);
            }

            return columnName;
        }

        private Row createContentRow(DataRow dataRow, int rowIndex)
        {
            Row row = new Row

            {
                RowIndex = (UInt32)rowIndex
            };

            Nullable<uint> styleIndex = null;
            double doubleValue;
            int intValue;
            DateTime dateValue;

            for (int i = 0; i < dataRow.Table.Columns.Count; i++)
            {
                Cell dataCell;

                //check the data type of the cell content to apply basic formatting
                if (DateTime.TryParse(dataRow[i].ToString(), out dateValue))
                {
                    styleIndex = _dateStyleId;
                    //the ToOADate method addresses how Excel stores Date values...
                    dataCell = createValueCell(i + 1, rowIndex, dateValue.ToOADate().ToString(), styleIndex);
                }
                else if (int.TryParse(dataRow[i].ToString(), out intValue))
                {
                    styleIndex = _numberStyleId;
                    dataCell = createValueCell(i + 1, rowIndex, intValue, styleIndex);
                }
                else if (Double.TryParse(dataRow[i].ToString(), out doubleValue))
                {
                    styleIndex = _doubleStyleId;
                    dataCell = createValueCell(i + 1, rowIndex, doubleValue, styleIndex);
                }
                else
                {
                    //assume the value is string, use the InlineString value type...
                    dataCell = createTextCell(i + 1, rowIndex, dataRow[i], null);
                }


                row.AppendChild(dataCell);
                styleIndex = null;
            }
            return row;
        }

        /// <summary>
        /// Creates a new Cell object with the InlineString data type.
        /// </summary>
        private Cell createTextCell(int columnIndex, int rowIndex, object cellValue, Nullable<uint> styleIndex)
        {
            Cell cell = new Cell();

            cell.DataType = CellValues.InlineString;
            cell.CellReference = getColumnName(columnIndex) + rowIndex;

            //apply the cell style if supplied
            if (styleIndex.HasValue)
                cell.StyleIndex = styleIndex.Value;

            InlineString inlineString = new InlineString();
            Text t = new Text();

            t.Text = cellValue.ToString();
            inlineString.AppendChild(t);
            cell.AppendChild(inlineString);

            return cell;
        }

        /// <summary>
        /// Creates a new Cell object.
        /// </summary>
        private Cell createValueCell(int columnIndex, int rowIndex, object cellValue, Nullable<uint> styleIndex)
        {
            Cell cell = new Cell();
            cell.CellReference = getColumnName(columnIndex) + rowIndex;
            CellValue value = new CellValue();
            value.Text = cellValue.ToString();

            //apply the cell style if supplied
            if (styleIndex.HasValue)
                cell.StyleIndex = styleIndex.Value;

            cell.AppendChild(value);

            return cell;
        }

        #endregion
        #endregion

        public static bool CreateExcelDocument<T>(List<T> list, string xlsxFilePath)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(ListToDataTable(list));

            return CreateExcelDocument(ds, xlsxFilePath);
        }


        public static DataTable ListToDataTable<T>(List<T> list)
        {
            DataTable dt = new DataTable();

            foreach (PropertyInfo info in typeof(T).GetProperties())
            {
                dt.Columns.Add(new DataColumn(info.Name, info.PropertyType));
            }
            foreach (T t in list)
            {
                DataRow row = dt.NewRow();
                foreach (PropertyInfo info in typeof(T).GetProperties())
                {
                    row[info.Name] = info.GetValue(t, null);
                }
                dt.Rows.Add(row);
            }
            return dt;
        }

        public static bool CreateExcelDocument(DataTable dt, string xlsxFilePath)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);

            return CreateExcelDocument(ds, xlsxFilePath);
        }

        public static bool CreateExcelDocument(DataSet ds, string excelFilename)
        {
            try
            {
                //using (SpreadsheetDocument document = SpreadsheetDocument.Create(excelFilename, SpreadsheetDocumentType.Workbook))
                //{
                //    CreateParts(ds, document);
                //}

                DocumentFormat.OpenXml.Packaging.SpreadsheetDocument document = DocumentFormat.OpenXml.Packaging.SpreadsheetDocument.Create(excelFilename, SpreadsheetDocumentType.Workbook);
                CreateParts(ds, document);

                //Trace.WriteLine("Successfully created: " + excelFilename);
                return true;
            }
            catch (Exception ex)
            {
                //Trace.WriteLine("Failed, exception thrown: " + ex.Message);
                return false;
            }
        }

        private static void CreateParts(DataSet ds, SpreadsheetDocument document)
        {
            WorkbookPart workbookPart = document.AddWorkbookPart();
            Workbook workbook = new Workbook();
            workbookPart.Workbook = workbook;

            //  If we don't add a "WorkbookStylesPart", OLEDB will refuse to connect to this .xlsx file !
            WorkbookStylesPart workbookStylesPart = workbookPart.AddNewPart<WorkbookStylesPart>("rIdStyles");
            Stylesheet stylesheet = new Stylesheet();
            workbookStylesPart.Stylesheet = stylesheet;

            Sheets sheets = new Sheets();

            //  Loop through each of the DataTables in our DataSet, and create a new Excel Worksheet for each.
            uint worksheetNumber = 1;
            foreach (DataTable dt in ds.Tables)
            {
                //  For each worksheet you want to create
                string workSheetID = "rId" + worksheetNumber.ToString();
                string worksheetName = dt.TableName;

                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>(workSheetID);
                WriteDataTableToExcelWorksheet(dt, worksheetPart);

                Sheet sheet = new Sheet() { Name = worksheetName, SheetId = (UInt32Value)worksheetNumber, Id = workSheetID };
                sheets.Append(sheet);

                worksheetNumber++;
            }

            workbook.Append(sheets);
        }

        private static void WriteDataTableToExcelWorksheet(DataTable dt, WorksheetPart worksheetPart1)
        {
            Worksheet worksheet = new Worksheet();
            SheetViews sheetViews = new SheetViews();

            SheetView sheetView = new SheetView() { TabSelected = true, WorkbookViewId = (UInt32Value)0U };
            sheetViews.Append(sheetView);

            SheetData sheetData1 = new SheetData();
            string cellValue = "";

            //  Create a Header Row in our Excel file, containing one header for each Column of data in our DataTable.
            //
            //  We'll also create an array, showing which type each column of data is (Text or Numeric), so when we come to write the actual
            //  cells of data, we'll know if to write Text values or Numeric cell values.
            int numberOfColumns = dt.Columns.Count;
            bool[] IsNumericColumn = new bool[numberOfColumns];

            string[] excelColumnNames = new string[numberOfColumns];
            for (int n = 0; n < numberOfColumns; n++)
                excelColumnNames[n] = GetExcelColumnName(n);

            //
            //  Create the Header row in our Excel Worksheet
            //
            int rowIndex = 1;
            Row row1 = new Row() { RowIndex = (UInt32Value)1U };
            for (int colInx = 0; colInx < numberOfColumns; colInx++)
            {
                DataColumn col = dt.Columns[colInx];

                AppendTextCell(excelColumnNames[colInx] + "1", col.ColumnName, row1);
                IsNumericColumn[colInx] = (col.DataType.FullName == "System.Decimal");     //  eg "System.String" or "System.Decimal"
            }
            sheetData1.Append(row1);


            //
            //  Now, step through each row of data in our DataTable...
            //
            foreach (DataRow dr in dt.Rows)
            {
                // ...create a new row, and append a set of this row's data to it.
                ++rowIndex;
                Row newExcelRow = new Row() { RowIndex = (UInt32Value)(uint)rowIndex };

                for (int colInx = 0; colInx < numberOfColumns; colInx++)
                {
                    cellValue = dr.ItemArray[colInx].ToString();

                    // Create cell with data
                    if (IsNumericColumn[colInx] && !string.IsNullOrEmpty(cellValue))
                        AppendNumericCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, newExcelRow);
                    else
                        AppendTextCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, newExcelRow);
                }
                sheetData1.Append(newExcelRow);
            }

            worksheet.Append(sheetViews);
            worksheet.Append(sheetData1);

            worksheetPart1.Worksheet = worksheet;
        }

        private static void AppendTextCell(string cellReference, string cellStringValue, Row excelRow)
        {
            //  Add a new Excel Cell to our Row 
            Cell cell = new Cell() { CellReference = cellReference, DataType = CellValues.String };
            CellValue cellValue = new CellValue();
            cellValue.Text = cellStringValue;
            cell.Append(cellValue);
            excelRow.Append(cell);
        }

        private static void AppendNumericCell(string cellReference, string cellStringValue, Row excelRow)
        {
            //  Add a new Excel Cell to our Row 
            Cell cell = new Cell() { CellReference = cellReference };
            CellValue cellValue = new CellValue();
            cellValue.Text = cellStringValue;
            cell.Append(cellValue);
            excelRow.Append(cell);
        }

        private static string GetExcelColumnName(int columnIndex)
        {
            //  Convert a zero-based column index into an Excel column reference (A, B, C.. Y, Y, AA, AB, AC... AY, AZ, B1, B2..)
            //  Each Excel cell we write must have the cell name stored with it.
            //
            if (columnIndex < 26)
                return ((char)('A' + columnIndex)).ToString();

            char firstChar = (char)('A' + (columnIndex / 26) - 1);
            char secondChar = (char)('A' + (columnIndex % 26));

            return string.Format("{0}{1}", firstChar, secondChar);
        }
        #endregion
    }
}