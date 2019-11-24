using Newtonsoft.Json;
using QuarterlyReports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QuarterlyReports.Controllers
{
    public class NaacRequirementsController : ApiController
    {
           GetDataFromDatabase db;
           public NaacRequirementsController()
        {
            db = new GetDataFromDatabase();
        }
        [HttpPost]
           public HttpResponseMessage AddNaacRequirements(NaacRequirements model)
           {
               try
               {
                   if (model == null)
                       return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Data");

                   string json = JsonConvert.SerializeObject(model);
                   string folderPath = System.Web.Configuration.WebConfigurationManager.AppSettings["folderPath"];
                   string fileName = folderPath + typeof(NaacRequirements).Name + "_" + DateTime.Now.ToString("yyyy-MM-dd HH mm ss") + ".json";

                   //write string to file
                   System.IO.File.WriteAllText(fileName, json);
                   string replacedJson = json.Replace("'", "|");
                   model = JsonConvert.DeserializeObject<NaacRequirements>(replacedJson);

                   int cId = db.InsertNaacRequirements(model);
                   if (cId > 0 && model.EditedBookDetails != null && model.EditedBookDetails.Count > 0)
                   {
                       foreach (EditedBookDetail editedBookDetail in model.EditedBookDetails)
                       {
                           editedBookDetail.NaacRequirementsId = cId;
                           bool isInserted = db.InsertEditedBookDetail(editedBookDetail);
                           if (!isInserted)
                               return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Data for EditedBookDetail");
                       }
                   }
                   if (cId > 0 && model.ExtensionOutReachProgramDetails != null && model.ExtensionOutReachProgramDetails.Count > 0)
                   {
                       foreach (ExtensionOutReachProgramDetail extensionOutReachProgramDetail in model.ExtensionOutReachProgramDetails)
                       {
                           extensionOutReachProgramDetail.NaacRequirementsId = cId;
                           bool isInserted = db.InsertExtensionOutReachProgramDetail(extensionOutReachProgramDetail);
                           if (!isInserted)
                               return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Data for ExtensionOutReachProgramDetail");
                       }
                   }
                   if (cId > 0 && model.ActivityDetails != null && model.ActivityDetails.Count > 0)
                   {
                       foreach (ActivityDetail activityDetail in model.ActivityDetails)
                       {
                           activityDetail.NaacRequirementsId = cId;
                           bool isInserted = db.InsertActivityDetail(activityDetail);
                           if (!isInserted)
                               return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Data for ActivityDetail");
                       }
                   }
                   if (cId > 0 && model.LinkageDetails != null && model.LinkageDetails.Count > 0)
                   {
                       foreach (LinkageDetail linkageDetail in model.LinkageDetails)
                       {
                           linkageDetail.NaacRequirementsId = cId;
                           bool isInserted = db.InsertLinkageDetail(linkageDetail);
                           if (!isInserted)
                               return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Data for LinkageDetail");
                       }
                   }
                   if (cId > 0 && model.MouDetails != null && model.MouDetails.Count > 0)
                   {
                       foreach (MouDetail mouDetail in model.MouDetails)
                       {
                           mouDetail.NaacRequirementsId = cId;
                           bool isInserted = db.InsertMouDetail(mouDetail);
                           if (!isInserted)
                               return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Data for MouDetail");
                       }
                   }
                   if (cId > 0 && model.DevelopmentSchemeDetails != null && model.DevelopmentSchemeDetails.Count > 0)
                   {
                       foreach (DevelopmentSchemeDetail developmentSchemeDetail in model.DevelopmentSchemeDetails)
                       {
                           developmentSchemeDetail.NaacRequirementsId = cId;
                           bool isInserted = db.InsertDevelopmentSchemeDetail(developmentSchemeDetail);
                           if (!isInserted)
                               return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Data for DevelopmentSchemeDetail");
                       }
                   }
                   return Request.CreateResponse(HttpStatusCode.OK, "Record inserted successfully");
               }
               catch (Exception ex)
               {
                   throw ex;
               }
           }

    }
}
