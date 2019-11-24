using AA.LogManager;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using iTextSharp.text.html.simpleparser;
public class PDFCommonPrint
{
    Logger logger = null;
    public string LoggedInUser { get; set; }

    public PDFCommonPrint(string loggedInUser)
    {
        LoggedInUser = loggedInUser;
        logger = new Logger(loggedInUser);
    }

    private static int LOGO_IMAGE_SCALE = 10;
    private static int LOGO_POSITION_X = 50;
    private static int LOGO_POSITION_Y = 350;
    private static int COLLEGE_NAME_FONT_SIZE = 10;
    private static int FONT_SIZE = 10;

    #region PDFCommon Printing Code For Printing New Leaving Certificate.
    public void Print(ref PdfWriter writer, ref Document document, List<PdfPrintOptions> configs, PdfPrintContent ppc, int y, string logPath)
    {
        try
        {
            int yOffset = 0;
            HTMLWorker hw = new HTMLWorker(document);
            StyleSheet styles = new iTextSharp.text.html.simpleparser.StyleSheet();
            for (int counter = 0; counter < configs.Count; counter++)
            {
                switch (configs[counter].Id)
                {
                    case "25":
                        configs[counter].Text = ppc.RefNumber;
                        break;
                    case "26":
                        configs[counter].Text = ppc.Date;
                        break;
                    case "27":
                        configs[counter].Text = ppc.Address;
                        break;
                    case "28":
                        configs[counter].Text = ppc.Subject;
                        break;
                }

                switch (configs[counter].LineType.ToLower())
                {
                    case "text":
                        if (counter == 23)
                        {
                            //hw.Parse(new StringReader(content));
                            List<IElement> elements = HTMLWorker.ParseToList(new StringReader(ppc.Content), styles);
                            ColumnText textCT = new ColumnText(writer.DirectContent);

                            foreach (IElement element in elements)
                            {
                                int chunkCounter = 0;
                                foreach (var chunk in element.Chunks)
                                {
                                    if (chunkCounter == 0)
                                        textCT.AddElement(Chunk.NEWLINE);
                                    chunk.Font = new Font(Font.FontFamily.HELVETICA, configs[counter].FontSize, configs[counter].Bold ? Font.BOLD : Font.NORMAL);
                                    chunkCounter++;
                                }

                                textCT.AddElement(element);
                            }

                            textCT.SetSimpleColumn(configs[counter].LLX, configs[counter].LLY + yOffset, configs[counter].URX, configs[counter].URY + yOffset);
                            textCT.Go();
                        }
                        else
                        {
                            Phrase text = new Phrase(configs[counter].Text, new Font(Font.FontFamily.HELVETICA, configs[counter].FontSize, configs[counter].Bold ? Font.BOLD : Font.NORMAL));
                            ColumnText textCT = new ColumnText(writer.DirectContent);
                            textCT.AddText(text);
                            textCT.SetSimpleColumn(configs[counter].LLX, configs[counter].LLY + yOffset, configs[counter].URX, configs[counter].URY + yOffset);
                            textCT.Go();
                        }
                        break;
                    case "image":
                        using (Stream logoStream = new FileStream(HttpContext.Current.Server.MapPath(configs[counter].ImagePath), FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            Image image = Image.GetInstance(logoStream);
                            image.ScalePercent(configs[counter].Scale);
                            image.SetAbsolutePosition(configs[counter].LLX, configs[counter].LLY + yOffset);
                            document.Add(image);
                        }
                        break;
                    case "line":
                        PdfContentByte cb1 = writer.DirectContent;
                        cb1.SetLineWidth(1.0f);
                        cb1.MoveTo(configs[counter].LLX, document.Top - configs[counter].LLY + yOffset);
                        cb1.LineTo(configs[counter].URX, document.Top - configs[counter].URY + yOffset);
                        cb1.Stroke();
                        break;
                }
            }

        }
        catch (Exception ex)
        {
            logger.Error("PDFCommonPrint.cs", "PrintHeaderNoRotate", "Error Occured while Printing Header with no Rotate", ex, logPath);
            throw;
        }
    }

    public void PrintHeaderLessMarathiDocuemt(string sourceFileName, string newFileName, List<PdfPrintOptions> configs, PdfPrintContent ppc, int y, string logPath)
    {
        try
        {
            using (Stream stream = new FileStream(sourceFileName, FileMode.Open))
            {
                using (Stream stream2 = new FileStream(newFileName, FileMode.Create, FileAccess.ReadWrite))
                {
                    PdfReader reader = new PdfReader(stream);
                    PdfStamper stamper = new PdfStamper(reader, stream2);
                    PdfContentByte overContent = stamper.GetOverContent(1);
                    int num2 = 0;
                    new HTMLWorker(overContent.PdfDocument);
                    new StyleSheet();
                    for (int i = 14; i < configs.Count; i++)
                    {
                        string str = configs[i].LineType.ToLower();
                        if (str == null)
                        {
                            continue;
                        }
                        if (str == "text")
                        {
                            int num4 = Convert.ToInt32(configs[i].Id);
                            switch (num4)
                            {
                                case 20:
                                case 0x15:
                                case 0x16:
                                case 0x17:
                                case 0x18:
                                    {
                                        continue;
                                    }
                            }
                            if ((num4 > 14) && (num4 < 20))
                            {
                                if (num4 == 15)
                                {
                                    configs[i].LLY -= 40;
                                    configs[i].URY -= 40;
                                }
                                else
                                {
                                    configs[i].LLY -= 30;
                                    configs[i].URY -= 30;
                                }
                            }
                            else
                            {
                                configs[i].LLY -= 50;
                                configs[i].URY -= 50;
                            }
                            Phrase phrase = new Phrase(configs[i].Text, new Font(iTextSharp.text.Font.FontFamily.HELVETICA, (float)configs[i].FontSize, configs[i].Bold ? 1 : 0));
                            ColumnText text = new ColumnText(overContent);
                            text.AddText(phrase);
                            text.SetSimpleColumn((float)configs[i].LLX, (float)(configs[i].LLY + num2), (float)configs[i].URX, (float)(configs[i].URY + num2));
                            text.Go();
                            continue;
                        }
                        if (str != "image")
                        {
                            if (str == "line")
                            {
                                goto Label_02DF;
                            }
                            continue;
                        }
                        using (Stream stream3 = new FileStream(HttpContext.Current.Server.MapPath(configs[i].ImagePath), FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            configs[i].LLY -= 30;
                            Image instance = Image.GetInstance(stream3);
                            instance.ScalePercent((float)configs[i].Scale);
                            instance.SetAbsolutePosition((float)configs[i].LLX, (float)(configs[i].LLY + num2));
                            overContent.AddImage(instance);
                            continue;
                        }
                    Label_02DF:
                        configs[i].URY += 50;
                        configs[i].LLY += 50;
                        PdfContentByte num5 = overContent;
                        num5.SetLineWidth(1f);
                        num5.MoveTo((float)configs[i].LLX, (overContent.PdfDocument.Top - configs[i].LLY) + num2);
                        num5.LineTo((float)configs[i].URX, (overContent.PdfDocument.Top - configs[i].URY) + num2);
                        num5.Stroke();
                    }
                    stamper.Close();
                    reader.Close();
                }
            }
        }
        catch (Exception exception)
        {
            this.logger.Error("PDFCommonPrint.cs", "PrintHeaderNoRotate", "Error Occured while Printing Header with no Rotate", exception, logPath);
        }
    }

    public void PrintMarathi(string sourceFileName, string newFileName, List<PdfPrintOptions> configs, PdfPrintContent ppc, int y, string logPath)
    {
        try
        {
            using (Stream stream = new FileStream(sourceFileName, FileMode.Open))
            {
                using (Stream stream2 = new FileStream(newFileName, FileMode.Create, FileAccess.ReadWrite))
                {
                    PdfReader reader = new PdfReader(stream);
                    PdfStamper stamper = new PdfStamper(reader, stream2);
                    PdfContentByte overContent = stamper.GetOverContent(1);
                    int num2 = 0;
                    new HTMLWorker(overContent.PdfDocument);
                    new StyleSheet();
                    using (Stream stream3 = new FileStream(HttpContext.Current.Server.MapPath("~/static/images/logo1.gif"), FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        Image instance = Image.GetInstance(stream3);
                        instance.ScalePercent(70f);
                        instance.SetAbsolutePosition(40f, 710f);
                        overContent.AddImage(instance);
                    }
                    for (int i = 0; i < configs.Count; i++)
                    {
                        string str = configs[i].LineType.ToLower();
                        if (str == null)
                        {
                            continue;
                        }
                        if (str == "text")
                        {
                            int num4 = Convert.ToInt32(configs[i].Id);
                            switch (num4)
                            {
                                case 20:
                                case 0x15:
                                case 0x16:
                                case 0x17:
                                case 0x18:
                                    {
                                        continue;
                                    }
                            }
                            if ((num4 > 14) && (num4 < 20))
                            {
                                if (num4 == 15)
                                {
                                    configs[i].LLY -= 40;
                                    configs[i].URY -= 40;
                                }
                                else
                                {
                                    configs[i].LLY -= 30;
                                    configs[i].URY -= 30;
                                }
                            }
                            else
                            {
                                configs[i].LLY -= 50;
                                configs[i].URY -= 50;
                            }
                            Phrase phrase = new Phrase(configs[i].Text, new Font(iTextSharp.text.Font.FontFamily.HELVETICA, (float)configs[i].FontSize, configs[i].Bold ? 1 : 0));
                            ColumnText text = new ColumnText(overContent);
                            text.AddText(phrase);
                            text.SetSimpleColumn((float)configs[i].LLX, (float)(configs[i].LLY + num2), (float)configs[i].URX, (float)(configs[i].URY + num2));
                            text.Go();
                            continue;
                        }
                        if (str != "image")
                        {
                            if (str == "line")
                            {
                                goto Label_0339;
                            }
                            continue;
                        }
                        using (Stream stream4 = new FileStream(HttpContext.Current.Server.MapPath(configs[i].ImagePath), FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            configs[i].LLY -= 30;
                            Image image2 = Image.GetInstance(stream4);
                            image2.ScalePercent((float)configs[i].Scale);
                            image2.SetAbsolutePosition((float)configs[i].LLX, (float)(configs[i].LLY + num2));
                            overContent.AddImage(image2);
                            continue;
                        }
                    Label_0339:
                        configs[i].URY += 50;
                        configs[i].LLY += 50;
                        PdfContentByte num5 = overContent;
                        num5.SetLineWidth(1f);
                        num5.MoveTo((float)configs[i].LLX, (overContent.PdfDocument.Top - configs[i].LLY) + num2);
                        num5.LineTo((float)configs[i].URX, (overContent.PdfDocument.Top - configs[i].URY) + num2);
                        num5.Stroke();
                    }
                    stamper.Close();
                    reader.Close();
                }
            }
        }
        catch (Exception exception)
        {
            this.logger.Error("PDFCommonPrint.cs", "PrintHeaderNoRotate", "Error Occured while Printing Header with no Rotate", exception, logPath);
        }
    }
    public void PrintHeaderLessDocument(ref PdfWriter writer, ref Document document, List<PdfPrintOptions> configs, PdfPrintContent ppc, int y, string logPath)
    {
        try
        {
            int num = 0;
            new HTMLWorker(document);
            StyleSheet sheet = new StyleSheet();
            for (int i = 14; i < configs.Count; i++)
            {
                PdfContentByte num4;
                string str2;
                string id = configs[i].Id;
                if (id != null)
                {
                    if (id != "25")
                    {
                        if (id == "26")
                        {
                            goto Label_007B;
                        }
                        if (id == "27")
                        {
                            goto Label_0090;
                        }
                        if (id == "28")
                        {
                            goto Label_00A5;
                        }
                    }
                    else
                    {
                        configs[i].Text = ppc.RefNumber;
                    }
                }
                goto Label_00B8;
            Label_007B:
                configs[i].Text = ppc.Date;
                goto Label_00B8;
            Label_0090:
                configs[i].Text = ppc.Address;
                goto Label_00B8;
            Label_00A5:
                configs[i].Text = ppc.Subject;
            Label_00B8:
                if ((str2 = configs[i].LineType.ToLower()) != null)
                {
                    switch (str2)
                    {
                        case "text":
                            if (i == 0x17)
                            {
                                List<IElement> list = HTMLWorker.ParseToList(new StringReader(ppc.Content), sheet);
                                ColumnText text = new ColumnText(writer.DirectContent);
                                foreach (IElement element in list)
                                {
                                    int num3 = 0;
                                    foreach (Chunk chunk in element.Chunks)
                                    {
                                        if (num3 == 0)
                                        {
                                            text.AddElement(Chunk.NEWLINE);
                                        }
                                        chunk.Font = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, (float)configs[i].FontSize, configs[i].Bold ? 1 : 0);
                                        num3++;
                                    }
                                    text.AddElement(element);
                                }
                                text.SetSimpleColumn((float)configs[i].LLX, (float)(configs[i].LLY + num), (float)configs[i].URX, (float)(configs[i].URY + num));
                                text.Go();
                            }
                            else
                            {
                                Phrase phrase = new Phrase(configs[i].Text, new Font(iTextSharp.text.Font.FontFamily.HELVETICA, (float)configs[i].FontSize, configs[i].Bold ? 1 : 0));
                                ColumnText text2 = new ColumnText(writer.DirectContent);
                                text2.AddText(phrase);
                                text2.SetSimpleColumn((float)configs[i].LLX, (float)(configs[i].LLY + num), (float)configs[i].URX, (float)(configs[i].URY + num));
                                text2.Go();
                            }
                            break;

                        case "image":
                            {
                                using (Stream stream = new FileStream(HttpContext.Current.Server.MapPath(configs[i].ImagePath), FileMode.Open, FileAccess.Read, FileShare.Read))
                                {
                                    Image instance = Image.GetInstance(stream);
                                    instance.ScalePercent((float)configs[i].Scale);
                                    instance.SetAbsolutePosition((float)configs[i].LLX, (float)(configs[i].LLY + num));
                                    document.Add(instance);
                                    break;
                                }
                            }
                        case "line":
                            goto Label_0346;
                    }
                }
                continue;
            Label_0346:
                num4 = writer.DirectContent;
                num4.SetLineWidth(1f);
                num4.MoveTo((float)configs[i].LLX, (document.Top - configs[i].LLY) + num);
                num4.LineTo((float)configs[i].URX, (document.Top - configs[i].URY) + num);
                num4.Stroke();
            }
        }
        catch (Exception exception)
        {
            this.logger.Error("PDFCommonPrint.cs", "PrintHeaderNoRotate", "Error Occured while Printing Header with no Rotate", exception, logPath);
            throw;
        }
    }


    #endregion
}