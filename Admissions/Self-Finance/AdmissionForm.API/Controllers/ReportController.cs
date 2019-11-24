using AdmissionForm.Business.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using AdmissionForm.API.Helper;

namespace AdmissionForm.API.Controllers
{
    public class ReportController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GenerateGeneralReport()
        {
            FileStream fileStream = null;
            try
            {
                string Reportname = "General Report";
                string folderPath = HttpContext.Current.Server.MapPath(String.Format("~/data/Pdf/"));
                string fileName = string.Empty;
                string fullPath = string.Empty;
                DataTable dt = null;
                HttpResponseMessage response = new HttpResponseMessage();
                ServiceContext serviceRef = new ServiceContext();
                bool isTrue = false;
                fileName = "General_Report_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                fullPath = folderPath + fileName;
                dt = serviceRef.GenerateGeneralReport();
                if (dt != null)
                {
                    isTrue = xmlHeaderHelper.BuildWorkbook(fullPath, dt, Reportname);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Datatable is empty");
                }
                if (isTrue)
                {
                    //ValidateExcelDocument(fullPath);
                    fileStream = File.Open(fullPath, FileMode.Open);
                    response.StatusCode = HttpStatusCode.OK;
                    response.Content = new StreamContent(fileStream);
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentDisposition.FileName = fileName;
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/ms-excel");
                    response.Content.Headers.ContentLength = fileStream.Length;
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Issue with the excel file generation");
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}