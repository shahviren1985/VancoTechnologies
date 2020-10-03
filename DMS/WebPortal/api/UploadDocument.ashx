<%@ WebHandler Language="C#" Class="UploadDocument" %>

using System;
using System.Web;
using System.IO;

public class UploadDocument : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        string admissionYear = context.Request.QueryString["admissionYear"].ToString();
        string collegeRegistrationNumber = context.Request.QueryString["crn"].ToString();
        string documentType = context.Request.QueryString["docType"].ToString();
        long ticks = DateTime.Now.Ticks;
        string fileName = context.Request.Headers["X-File-Name"];
        
        if (string.IsNullOrEmpty(fileName))
        {
            fileName = context.Request.QueryString["fileName"].ToString();
        }
        
        string fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
        
        System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();

        // Document Types - 
        // 1. Marksheet, Passing-Certificate, Eligibility-Certificate, 
        // 2. Caste-Certificate, Ration-Card, Transfer-Certificate
        // 3. Aadhar-Card, Pan-Card, Voter-Card
        // 4. Migration-Certificate, Name-Change-Certificate
        
        // HSC Marksheet
        // SSC Marksheet
        // Passing Certificate
        // Eligibility Certificate
        
        // Caste Certificate 
        // Ration Card
        // Transfer Certificate
        
        // 

        if (string.IsNullOrEmpty(collegeRegistrationNumber) || string.IsNullOrEmpty(documentType) || string.IsNullOrEmpty(admissionYear))
        {
            context.Response.Write("Error: Please provide College Registration Number/Document Type/Admission Year.");
            return;
        }

        try
        {
            if (!Directory.Exists(context.Server.MapPath("~/docs/SVT/Students/" + admissionYear + "/" + collegeRegistrationNumber + "/" + documentType)))
            {
                Directory.CreateDirectory(context.Server.MapPath("~/docs/SVT/Students/" + admissionYear + "/" + collegeRegistrationNumber + "/" + documentType));
            }

            string path = context.Server.MapPath("~/docs/SVT/Students/" + admissionYear + "/" + collegeRegistrationNumber + "/" + documentType + "/" + fileNameWithoutExt + "_" + ticks + ".pdf");

            Stream inputStream = context.Request.InputStream;
            FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate);
            Util.Utilities.CopyFile(inputStream, fileStream);

            fileStream.Close();
            
            string message = "File (" + documentType + ") uploaded successfully.";
            context.Response.Write(jss.Serialize(message));
        }
        catch (Exception ex)
        {
            context.Response.Write(jss.Serialize(ex.Message));
        }
         
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}