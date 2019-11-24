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
    public class EventController : ApiController
    {
        GetDataFromDatabase db;
        public EventController()
        {
            db = new GetDataFromDatabase();
        }

        [HttpGet]
        public HttpResponseMessage test()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "It Works");   
        }

        [HttpPost]
        public HttpResponseMessage AddCommittee(Committee model)
        {
            try
            {
                if (model == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Data");

                string json = JsonConvert.SerializeObject(model);
                string folderPath = System.Web.Configuration.WebConfigurationManager.AppSettings["folderPath"];
                string fileName = folderPath + typeof(Committee).Name + "_" + DateTime.Now.ToString("yyyy-MM-dd HH mm ss") + ".json";

                //write string to file
                System.IO.File.WriteAllText(fileName, json);

                string replacedJson = json.Replace("'", "|");
                model = JsonConvert.DeserializeObject<Committee>(replacedJson);


               int cId= db.InsertCommittee(model);
               if (cId > 0 &&  model.Events!=null &&  model.Events.Count > 0)
               {
                   foreach (Event currentEvent in model.Events)
                   {
                       currentEvent.CommiteeId = cId;
                       bool isInserted= db.InsertEvent(currentEvent);
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
