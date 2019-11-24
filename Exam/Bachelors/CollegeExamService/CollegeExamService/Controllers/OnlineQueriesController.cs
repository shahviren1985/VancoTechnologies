using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Hosting;
using System.Web.Http;
using CollegeExamService.Models;
using CollegeExamService.Helpers;

namespace CollegeExamService.Controllers
{
    public class OnlineQueriesController : ApiController
    {
        string FilePath = HostingEnvironment.ApplicationPhysicalPath;
        [HttpPost]
        public HttpResponseMessage CreateRequest(TranscriptModel ts)
        {
            try
            {
                LiteOperation lt = new LiteOperation();
                if (lt.AddTranscriptRequest(ts) > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, ts);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "No record inserted! Please contact Administrator");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}
