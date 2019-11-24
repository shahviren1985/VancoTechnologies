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
    public class PeakPerformersController : ApiController
    {
          GetDataFromDatabase db;
          public PeakPerformersController()
        {
            db = new GetDataFromDatabase();
        }


        public HttpResponseMessage AddPeakPerformers(PeakPerformers model)
        {
            try
            {
                if (model == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Data");

                string json = JsonConvert.SerializeObject(model);
                string folderPath = System.Web.Configuration.WebConfigurationManager.AppSettings["folderPath"];
                string fileName = folderPath + typeof(PeakPerformers).Name + "_" + DateTime.Now.ToString("yyyy-MM-dd HH mm ss") + ".json";

                //write string to file
                System.IO.File.WriteAllText(fileName, json);
                string replacedJson = json.Replace("'", "|");
                model = JsonConvert.DeserializeObject<PeakPerformers>(replacedJson);


                if (model.WinnersAtZonalLevel != null)
                {
                    model.WinnersAtZonalLevel.PeakPerformerLevel = "WinnersAtZonalLevel";
                    int cId = db.InsertPeakPerformers(model.WinnersAtZonalLevel);
                    if (cId > 0 && model.WinnersAtZonalLevel.Activities != null && model.WinnersAtZonalLevel.Activities.Count > 0)
                    {
                        foreach (PerformerActivity performerActivity in model.WinnersAtZonalLevel.Activities)
                        {
                            performerActivity.PerformerId = cId;
                            //performerActivity.ActivityType = "WinnersAtZonalLevel";
                            bool isInserted = db.InsertPerformerActivities(performerActivity);
                            if (!isInserted)
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Data for WinnerATZonalLevel Activity");
                        }
                    }
                }
                if (model.WinnersAtRegionalLevel != null)
                {
                    model.WinnersAtRegionalLevel.PeakPerformerLevel = "WinnersAtRegionalLevel";
                    int cId = db.InsertPeakPerformers(model.WinnersAtRegionalLevel);
                    if (cId > 0 && model.WinnersAtRegionalLevel.Activities != null && model.WinnersAtRegionalLevel.Activities.Count > 0)
                    {
                        foreach (PerformerActivity performerActivity in model.WinnersAtRegionalLevel.Activities)
                        {
                            performerActivity.PerformerId = cId;
                            //performerActivity.ActivityType = "WinnersAtRegionalLevel";
                            bool isInserted = db.InsertPerformerActivities(performerActivity);
                            if (!isInserted)
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Data for WinnerATZonalLevel Activity");
                        }
                    }
                }
                if (model.ParticipationAtZonalLevel != null)
                {
                    model.ParticipationAtZonalLevel.PeakPerformerLevel = "ParticipationAtZonalLevel";
                    int cId = db.InsertPeakPerformers(model.ParticipationAtZonalLevel);
                    if (cId > 0 && model.ParticipationAtZonalLevel.Activities != null && model.ParticipationAtZonalLevel.Activities.Count > 0)
                    {
                        foreach (PerformerActivity performerActivity in model.ParticipationAtZonalLevel.Activities)
                        {
                            performerActivity.PerformerId = cId;
                            //performerActivity.ActivityType = "ParticipationAtZonalLevel";
                            bool isInserted = db.InsertPerformerActivities(performerActivity);
                            if (!isInserted)
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Data for WinnerATZonalLevel Activity");
                        }
                    }
                }
                if (model.ParticipationAtRegionalLevel != null)
                {
                    model.ParticipationAtRegionalLevel.PeakPerformerLevel = "ParticipationAtRegionalLevel";
                    int cId = db.InsertPeakPerformers(model.ParticipationAtRegionalLevel);
                    if (cId > 0 && model.ParticipationAtRegionalLevel.Activities != null && model.ParticipationAtRegionalLevel.Activities.Count > 0)
                    {
                        foreach (PerformerActivity performerActivity in model.ParticipationAtRegionalLevel.Activities)
                        {
                            performerActivity.PerformerId = cId;
                            //performerActivity.ActivityType = "ParticipationAtRegionalLevel";
                            bool isInserted = db.InsertPerformerActivities(performerActivity);
                            if (!isInserted)
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Data for WinnerATZonalLevel Activity");
                        }
                    }
                }
                if (model.WinnersAtCollageLevel != null)
                {
                    model.WinnersAtCollageLevel.PeakPerformerLevel = "WinnersAtCollageLevel";
                    int cId = db.InsertPeakPerformers(model.WinnersAtCollageLevel);
                    if (cId > 0 && model.WinnersAtCollageLevel.Activities != null && model.WinnersAtCollageLevel.Activities.Count > 0)
                    {
                        foreach (PerformerActivity performerActivity in model.WinnersAtCollageLevel.Activities)
                        {
                            performerActivity.PerformerId = cId;
                            //performerActivity.ActivityType = "WinnersAtCollageLevel";
                            bool isInserted = db.InsertPerformerActivities(performerActivity);
                            if (!isInserted)
                                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Data for WinnerATZonalLevel Activity");
                        }
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
