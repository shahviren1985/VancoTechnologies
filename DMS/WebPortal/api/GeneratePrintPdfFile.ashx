<%@ WebHandler Language="C#" Class="GeneratePrintPdfFile" %>
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

public class GeneratePrintPdfFile : IHttpHandler, IRequiresSessionState
{
    DocumentDetailsDAO dao = new DocumentDetailsDAO();
    PdfWriter writer = null;
    JavaScriptSerializer jss = new JavaScriptSerializer();

    public void ProcessRequest(HttpContext context)
    {
        string docId = context.Request.QueryString["docid"];        
        string logPath = context.Session["LogFilePath"].ToString();        
        string cnxnString = context.Session["ConnectionString"].ToString();
        string userName = context.Session["UserName"].ToString();
        string configFilePath = context.Server.MapPath(context.Session["ReleaseFilePath"].ToString());

        string college = context.Session["college"].ToString(); ;

        //context.Response.Write(docId +" <br/>");
        //context.Response.Write(logPath + " <br/>");
        //context.Response.Write(cnxnString + " <br/>");
        //context.Response.Write(userName + " <br/>");
        //context.Response.Write(configFilePath + " <br/>");
        //context.Response.Write(college + " <br/>");

        List<PdfPrintOptions> configs = new List<PdfPrintOptions>();
        var document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4);

        //context.Response.Write("Errro after : line 42  <br/>");
        
        DocumentDetails doc = dao.GetDocumentDetails(int.Parse(docId), cnxnString, logPath);

        //context.Response.Write(jss.Serialize(doc) + Environment.NewLine);

        //context.Response.Write("Errro after : line 46 <br/>");
        
        bool isMarathi = false;

        if (File.Exists(context.Server.MapPath("~/Directories/" + college + "/outward/Print_" + doc.UniqueName + ".pdf")))
        {
            // File already exists, redirect user to existing file
            //context.Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["BASE_URL"] + "/Directories/outward/Print_" + doc.UniqueName + ".pdf");
            File.Exists(context.Server.MapPath("~/Directories/" + college + "/outward/Print_" + doc.UniqueName + ".pdf"));
        }

        //context.Response.Write("Errro after : if<br/>");

        {
            //PrintHeaderLessDocument
            try
            {
                PdfPrintContent ppc = new PdfPrintContent();
                ppc.Address = HttpUtility.UrlDecode(doc.Address);
                //context.Response.Write("Errro after : Address : " + ppc.Address + " <br/>");
                
                ppc.Content = context.Server.UrlDecode(doc.Content);
                //context.Response.Write("Errro after : Content : " + ppc.Content + " <br/>");
                
                ppc.Date = Util.Utilities.GetCurrentIndianDate().ToString("dd/MM/yyyy");
                //context.Response.Write("Errro after : Date: " + ppc.Date + " <br/>");
                
                ppc.RefNumber = ppc.GetReferenceNumber(doc.SerialNumber, logPath, configFilePath);
                //context.Response.Write("Errro after : RefNumber : " + ppc.RefNumber + " <br/>");
                
                //context.Response.Write("Errro after : assigning values in pdfPrintcontent <br/>");
                   
                ppc.Subject = HttpUtility.UrlDecode(jss.Deserialize<MessageHeader>(doc.MessageHeader).Subject);
                //context.Response.Write
                //context.Response.Write("Errro after : deserialize Subject <br/>");
                
                string fileName = "Print_" + doc.UniqueName + ".pdf";
                string filePath = "~/Directories/" + college + "/outward/";

                filePath = Path.Combine(filePath, fileName);

                new XmlConfiguration().GetPdfDocumentContent(ref configs, logPath, configFilePath);

                //context.Response.Write("Errro after : 79" + Environment.NewLine);

                /*generat docx file*/
                if (!File.Exists(context.Server.MapPath("~/Directories/" + college + "/outward/Print_" + doc.UniqueName + ".docx")))
                {
                    GenerateWordDocument(context, filePath.Replace(".pdf", ".docx"), ppc, college);
                }

                //context.Response.Write("Errro after : if" + Environment.NewLine);
                /*generate pdf file*/

                PDFCommonPrint pcp = new PDFCommonPrint(userName);
                //pcp.PrintHeaderLessDocument(ref writer, ref document, configs, ppc, 100, logPath);


                if (doc.IsScannedMarathi.ToLower() == "true")
                {
                    isMarathi = true;
                    string sourceFilePath = context.Server.MapPath("~/marathi-docs/" + college + "/" + doc.Author + "/" + doc.ScanMarathiPath);
                    string newFilePath = context.Server.MapPath("~/Directories/" + college + "/outward/" + fileName);
                    
                    try
                    {
                        if (File.Exists(newFilePath)) File.Delete(newFilePath);
                    }
                    catch (Exception)
                    {

                    }

                    pcp.PrintHeaderLessMarathiDocuemt(sourceFilePath, newFilePath, configs, ppc, 100, logPath);
                }
                else
                {
                    writer = PdfWriter.GetInstance(document, new FileStream(context.Server.MapPath(filePath), FileMode.Create));
                    document.Open();

                    pcp.PrintHeaderLessDocument(ref writer, ref document, configs, ppc, 100, logPath);
                }

                context.Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["BASE_URL"] + "/Directories/" + college + "/outward/Print_" + doc.UniqueName + ".pdf");
            }
            catch (Exception ex)
            {
                context.Response.Write(ex.Message);
            }
            finally
            {
                document.Close();
            }
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

            if (!Directory.Exists(context.Server.MapPath("~/docs/")))
            {
                Directory.CreateDirectory(context.Server.MapPath("~/docs/"));
            }

            if (File.Exists(context.Server.MapPath("~/docs/" + college + "/empty-doc.docx")))
                  File.Copy(context.Server.MapPath("~/docs/" + college + "/empty-doc.docx"), context.Server.MapPath(filePath));


            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(context.Server.MapPath(filePath), true))
            {
                MainDocumentPart mainPart = wordDoc.MainDocumentPart;
                int altChunkIdCounter = 1;
                int blockLevelCounter = 1;

                string mainhtml = "<html><body><div style='float: left'><span>Ref.:</span><span>" + ppc.RefNumber + "</span></div><div style='float: right'><span>Date.:</span><span>" + ppc.Date + "</span></div><div>To,</div><div>" + ppc.Address + "</div><div>Sub.: " + ppc.Subject + "</div><div>" + ppc.Content + "</div><div style='float: right'>Yours faithfully,<br/><br/><br/>Dr. (Ms.) Harshada Rathod<br/>Principal<br/>MANIBEN NANAVATI WOMEN'S COLLEGE,<br/>Mumbai - 400 056</div></body></html>";
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