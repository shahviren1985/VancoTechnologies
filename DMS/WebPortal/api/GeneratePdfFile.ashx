<%@ WebHandler Language="C#" Class="GeneratePdfFile" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Web.SessionState;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.IO;
using AA.DAO;
using System.Text;
using System.Web.Script.Serialization;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO.Packaging;

public class GeneratePdfFile : IHttpHandler, IRequiresSessionState
{
    DocumentDetailsDAO dao = new DocumentDetailsDAO();
    PdfWriter writer = null;
    JavaScriptSerializer jss = new JavaScriptSerializer();

    public void ProcessRequest(HttpContext context)
    {

        List<PdfPrintOptions> configs = new List<PdfPrintOptions>();
        var document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4);
        bool isMarathi = false;
        try
        {
            string filename = context.Request.QueryString["name"];
            string docId = context.Request.QueryString["docid"];
            string logPath = context.Request.QueryString["log"];
            string cnxnString = context.Request.QueryString["con"];
            string user = context.Request.QueryString["user"];

            string college = context.Request.QueryString["college"];
            string configFilePath = context.Request.QueryString["configPath"];

            long ticks = DateTime.Now.Ticks;
            var styles = new StyleSheet();
            DocumentDetails doc = dao.GetDocumentDetails(int.Parse(docId), cnxnString, logPath);

            PdfPrintContent ppc = new PdfPrintContent();
            ppc.Address = HttpUtility.UrlDecode(doc.Address);
            ppc.Content = context.Server.UrlDecode(doc.Content);
            ppc.Date = Util.Utilities.GetCurrentIndianDate().ToString("dd/MM/yyyy");
            ppc.RefNumber = ppc.GetReferenceNumber(doc.SerialNumber, logPath, configFilePath);
            ppc.Subject = HttpUtility.UrlDecode(jss.Deserialize<MessageHeader>(doc.MessageHeader).Subject);

            //string filePath = "~/Directories/outward/" + filename;
            //string filePath = "~/Directories/outward/";

            string filePath = "~/Directories/" + college + "/outward/";

            if (!Directory.Exists(context.Server.MapPath(filePath)))
            {
                Directory.CreateDirectory(context.Server.MapPath(filePath));
            }

            filePath = Path.Combine(filePath, filename);

            new XmlConfiguration().GetPdfDocumentContent(ref configs, logPath, configFilePath);
            // generate docx file for marathi fonts
            GenerateWordDocument(context, filePath.Replace(".pdf", ".docx"), ppc, college);

            //generate pdf file            
            PDFCommonPrint pcp = new PDFCommonPrint(user);

            if (!string.IsNullOrEmpty(doc.IsScannedMarathi) && doc.IsScannedMarathi.ToLower() == "true")
            {
                isMarathi = true;
                string sourceFilePath = context.Server.MapPath("~/marathi-docs/" + college + "/" + doc.Author + "/" + doc.ScanMarathiPath);
                string newFilePath = context.Server.MapPath("~/Directories/" + college + "/outward/" + filename);
                try
                {
                    if (File.Exists(newFilePath)) File.Delete(newFilePath);
                }
                catch (Exception)
                {

                }

                //pcp.PrintMarathi(sourceFilePath, newFilePath, configs, ppc, 100, logPath);
            }
            else
            {
                writer = PdfWriter.GetInstance(document, new FileStream(context.Server.MapPath(filePath), FileMode.Create));
                document.Open();

                pcp.Print(ref writer, ref document, configs, ppc, 100, logPath);
            }
            //send response 
            context.Response.Write(filename);
        }
        catch (Exception ex)
        {

        }
        finally
        {
            if (!isMarathi)
                document.Close();
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    public void GenerateWordDocument(HttpContext context, string filePath, PdfPrintContent ppc, string college)
    {
        try
        {
            if (File.Exists(context.Server.MapPath(filePath)))
            {
                File.Delete(context.Server.MapPath(filePath));
            }

            if (!Directory.Exists(context.Server.MapPath("~/docs/" + college + "/")))
            {
                Directory.CreateDirectory(context.Server.MapPath("~/docs/" + college + "/"));
            }

            if (File.Exists(context.Server.MapPath("~/docs/" + college + "/doc.docx")))
                File.Copy(context.Server.MapPath("~/docs/" + college + "/doc.docx"), context.Server.MapPath(filePath));


            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(context.Server.MapPath(filePath), true))
            {
                MainDocumentPart mainPart = wordDoc.MainDocumentPart;
                int altChunkIdCounter = 1;
                int blockLevelCounter = 1;

                string mainhtml = "<html><body><div style='float: left'><span>Ref.: </span><span>" + ppc.RefNumber + "</span></div><div style='float: right'><span>Date.: </span><span>" + ppc.Date + "</span></div><div>To,</div><div>" + ppc.Address + "</div><div>Sub.: " + ppc.Subject + "</div><div>" + ppc.Content + "</div></body></html>";
                string altChunkId = String.Format("AltChunkId{0}", altChunkIdCounter++);

                //Import data as html content using Altchunk
                AlternativeFormatImportPart chunk = mainPart.AddAlternativeFormatImportPart(AlternativeFormatImportPartType.Html, altChunkId);

                using (Stream chunkStream = chunk.GetStream(FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter stringWriter = new StreamWriter(chunkStream, Encoding.UTF8))
                    {
                        stringWriter.Write(mainhtml);
                    }
                }

                AltChunk altChunk = new AltChunk();
                altChunk.Id = altChunkId;

                mainPart.Document.Body.InsertAt(altChunk, blockLevelCounter++);
                mainPart.Document.Save();

                mainPart = null;
                altChunk = null;
            }

        }
        catch (Exception ex)
        {
            context.Response.Write(ex.Message.ToString());
        }
        finally
        {

        }
    }
}