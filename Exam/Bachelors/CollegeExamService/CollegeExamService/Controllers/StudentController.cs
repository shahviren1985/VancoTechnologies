using CollegeExamService.Helpers;
using CollegeExamService.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CollegeExamService.Controllers
{
    public class StudentController : ApiController
    {
        ExamDbOperations db = null;

        [HttpPost]
        public HttpResponseMessage SubmitTranscriptForms(string fName, string LName, string PNR, string AdYear)
        {
            if (string.IsNullOrWhiteSpace(fName))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "First Name is required");
            }
            if (string.IsNullOrWhiteSpace(LName))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Last Name is required");
            }
            if (string.IsNullOrWhiteSpace(PNR))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "PNR is required");
            }
            if (string.IsNullOrWhiteSpace(AdYear))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Admission year is required");
            }
            db = new ExamDbOperations();
            int RequestNo = db.InsertStudentTranscript(fName, LName, PNR, AdYear);
            if (RequestNo > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, string.Format("Request recieved successfully, Your Request No is <b>#{0}</b>", RequestNo));
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, string.Format("Error! Please try again after some time..."));
            }
        }

        [HttpGet]
        public HttpResponseMessage CompleteTranscriptRequest(string TranscriptId)
        {
            if (string.IsNullOrWhiteSpace(TranscriptId))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "TranscriptId is required");
            }
            db = new ExamDbOperations();
            int RequestNo = db.CompleteTranscriptData(Convert.ToInt32(TranscriptId), 1);
            return Request.CreateResponse(HttpStatusCode.OK, string.Format("Completed successfully"));

        }

        [HttpGet]
        public HttpResponseMessage GetTranscriptData(int status)
        {
            db = new ExamDbOperations();
            return Request.CreateResponse(HttpStatusCode.OK, db.GetTranscriptRequest(status).Tables[0]);
        }

        [HttpPost]
        public HttpResponseMessage SubmitOnlineQuery(QueryModel model)
        {
            string path = System.Web.Configuration.WebConfigurationManager.AppSettings["CsvFolderPath"];
            File.WriteAllText(path + "\\Query\\" + DateTime.Now.ToString("yyyy-MM-dd HH mm ss") + ".json", JsonConvert.SerializeObject(model));
            return Request.CreateResponse(HttpStatusCode.OK, "Your Request received, we will get back to you soon.");
        }

        [HttpGet]
        public HttpResponseMessage GetSummaryGraphData(string ssc, string hsc, string grad, string crn)
        {
            ExamDbOperations db = new ExamDbOperations();
            string path = System.Web.Configuration.WebConfigurationManager.AppSettings["CsvFolderPath"];
            StudentProgress progress = new StudentProgress();

            CommonMarksheetResponse cm = db.GetStudentDetailsByCollegeRegistrationNumber(crn);
            if (cm == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "College Registration Number not matching with any student");
            }
            else
            {
                var ExamTable = db.GetStudentExamHestory(cm.Id);
                var ExamDetails = db.GetStudentExamDetailsById(Convert.ToInt32(cm.Id));
                progress.sscpercent = ssc;
                progress.hscpercent = hsc;
                for (int i = 1; i < 7; i++)
                {
                    switch (i)
                    {
                        case 1:
                            progress.sem1percent = db.GetSemesterPercent(cm.Id, i).ToString();
                            break;
                        case 2:
                            progress.sem2percent = db.GetSemesterPercent(cm.Id, i).ToString();
                            break;
                        case 3:
                            progress.sem3percent = db.GetSemesterPercent(cm.Id, i).ToString();
                            break;
                        case 4:
                            progress.sem4percent = db.GetSemesterPercent(cm.Id, i).ToString();
                            break;
                        case 5:
                            progress.sem5percent = db.GetSemesterPercent(cm.Id, i).ToString();
                            break;
                        case 6:
                            progress.sem6percent = db.GetSemesterPercent(cm.Id, i).ToString();
                            break;
                    }
                }

                return Request.CreateResponse(HttpStatusCode.OK, progress);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetSemesterGraphData(string semester, string crn)
        {
            string path = System.Web.Configuration.WebConfigurationManager.AppSettings["CsvFolderPath"];
            SemesterPapers paper = new SemesterPapers();
            paper.Papers = new List<PaperDetails>();
            ExamDbOperations db = new ExamDbOperations();
            CommonMarksheetResponse cm = db.GetStudentDetailsByCollegeRegistrationNumber(crn);
            if (cm == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "College Registration Number not matching with any student");
            }
            else
            {
                DataTable table = db.GetSemesterData(cm.Id, int.Parse(semester));
                foreach (DataRow row in table.Rows)
                {
                    PaperDetails paperDetails = new PaperDetails();
                    paperDetails.PaperCode = row["papercode"].ToString();
                    paperDetails.Papertitle = row["papertitle"].ToString();
                    paperDetails.PaperType = row["papertype"].ToString();
                    paperDetails.InternalMarks = row["internalmarksobtained"].ToString();
                    paperDetails.ExternalMarks = row["externaltotalmarks"].ToString();
                    paperDetails.PracticalMarks = row["practicalmarksobtained"].ToString();
                    paperDetails.TotalMarksObtained = row["TotalObtained"].ToString();
                    paperDetails.TotalMarks = row["TotalMarks"].ToString();
                    paperDetails.Credit = row["credit"].ToString();
                    paperDetails.WeightedMarks =  (float.Parse(paperDetails.TotalMarksObtained) * float.Parse(paperDetails.Credit)).ToString();
                    paperDetails.Grade = row["gp"].ToString();

                    paper.Papers.Add(paperDetails);
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, paper);

        }
    }

    public class QueryModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string RequestDate { get; set; }
        public string PaymentAmount { get; set; }
        public string PaymentDate { get; set; }
        public string TransactionNumber { get; set; }
        public string QueryType { get; set; }
        public string Quantity { get; set; }
        public string QueryStatus { get; set; }
        public string AdmissionYear { get; set; }
        public string Specialisation { get; set; }
        public string Course { get; set; }
        public string CollegeRegistrationNumber { get; set; }
        public bool CurrentStudent { get; set; }
    }

    public class StudentProgress
    {
        public string sscpercent { get; set; }
        public string hscpercent { get; set; }
        public string sem1percent { get; set; }
        public string sem2percent { get; set; }
        public string sem3percent { get; set; }
        public string sem4percent { get; set; }
        public string sem5percent { get; set; }
        public string sem6percent { get; set; }
    }

    public class SemesterPapers
    {
        public List<PaperDetails> Papers { get; set; }
    }

    public class PaperDetails
    {
        public string PaperType { get; set; }
        public string PaperCode { get; set; }
        public string Papertitle { get; set; }
        public string InternalMarks { get; set; }
        public string ExternalMarks { get; set; }
        public string PracticalMarks { get; set; }
        public string TotalMarks { get; set; }
        public string TotalMarksObtained { get; set; }
        public string Grade { get; set; }
        public string WeightedMarks { get; set; }
        public string Percentage { get; set; }
        public string Credit { get; set; }
    }
}