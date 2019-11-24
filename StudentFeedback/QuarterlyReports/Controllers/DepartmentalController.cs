using Newtonsoft.Json;
using QuarterlyReports.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QuarterlyReports.Controllers
{
    public class DepartmentalController : ApiController
    {
        GetDataFromDatabase db;
        public DepartmentalController()
        {
            db = new GetDataFromDatabase();
        }


        public HttpResponseMessage AddDepartmental(Departmental model)
        {
            try
            {
                if (model == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Data");

                string json = JsonConvert.SerializeObject(model);
                string folderPath = System.Web.Configuration.WebConfigurationManager.AppSettings["folderPath"];
                string fileName = folderPath + typeof(Departmental).Name + "_" + DateTime.Now.ToString("yyyy-MM-dd HH mm ss") + ".json";
                //write string to file
                System.IO.File.WriteAllText(fileName, json);
                string replacedJson = json.Replace("'", "|");
                model = JsonConvert.DeserializeObject<Departmental>(replacedJson);

                int cId = db.InsertDepartmental(model);
                if (cId > 0 && model.GuestLectureTalk != null && model.GuestLectureTalk.Count > 0)
                {
                    foreach (GuestLectureTalk currentGuestLectureTalk in model.GuestLectureTalk)
                    {
                        currentGuestLectureTalk.DeptId = cId;
                        bool isInserted = db.InsertGuestLectureTalk(currentGuestLectureTalk);
                        if (!isInserted)
                            return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Data for GuestLectureTalk");

                    }
                }
                if (cId > 0 && model.Visits != null && model.Visits.Count > 0)
                {
                    foreach (Visit currentVisits in model.Visits)
                    {
                        currentVisits.DeptId = cId;
                        bool isInserted = db.InsertVisits(currentVisits);
                        if (!isInserted)
                            return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Data for Visit");

                    }
                }
                if (cId > 0 && model.Seminars != null && model.Seminars.Count > 0)
                {
                    foreach (Seminar currentSeminars in model.Seminars)
                    {
                        currentSeminars.DeptId = cId;
                        bool isInserted = db.InsertSeminar(currentSeminars);
                        if (!isInserted)
                            return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Data for Seminar");

                    }
                }
                if (cId > 0 && model.Activities != null && model.Activities.Count > 0)
                {
                    foreach (Activity currentActivities in model.Activities)
                    {
                        currentActivities.DeptId = cId;
                        bool isInserted = db.InsertActivities(currentActivities);
                        if (!isInserted)
                            return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Data for Activity");

                    }
                }
                if (cId > 0 && model.Collaborations != null && model.Collaborations.Count > 0)
                {
                    foreach (Collaboration currentCollaborations in model.Collaborations)
                    {
                        currentCollaborations.DeptId = cId;
                        bool isInserted = db.InsertCollaborations(currentCollaborations);
                        if (!isInserted)
                            return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Data for Collaboration");

                    }
                }
                if (cId > 0 && model.InHouseCollaborations != null && model.InHouseCollaborations.Count > 0)
                {
                    foreach (InHouseCollaboration currentInHouseCollaborations in model.InHouseCollaborations)
                    {
                        currentInHouseCollaborations.DeptId = cId;
                        bool isInserted = db.InsertInHouseCollaborations(currentInHouseCollaborations);
                        if (!isInserted)
                            return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Data for InHouseCollaboration");

                    }
                }
                if (cId > 0 && model.DepartmentalSeminar != null && model.DepartmentalSeminar.Count > 0)
                {
                    foreach (DepartmentalSeminar currentDepartmentalSeminar in model.DepartmentalSeminar)
                    {
                        currentDepartmentalSeminar.DeptId = cId;
                        bool isInserted = db.InsertDepartmentalSeminar(currentDepartmentalSeminar);
                        if (!isInserted)
                            return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Data for DepartmentalSeminar");

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
