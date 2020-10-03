using ChoETL;
using CollegeExamService.Helpers;
using CollegeExamService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;

namespace CollegeExamService.Controllers
{
    public class AttendanceController : ApiController
    {
        ExamDbOperations db = null;

        public AttendanceController()
        {
            db = new ExamDbOperations();
        }

        [HttpPost]
        public HttpResponseMessage UploadAttendanceCsv()
        {
            try
            {
                HttpResponseMessage result = null;

                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    foreach (string currentFile in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[currentFile];
                        string courseName = string.Empty;
                        string subCourseName = string.Empty;
                        string csvFilePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/data/attendance/processing/");
                        string fileName = Path.GetFileNameWithoutExtension(postedFile.FileName) + "_" + DateTime.Now.ToString("yyyy-MM-dd HH mm ss") + Path.GetExtension(postedFile.FileName);

                        if (!Directory.Exists(csvFilePath))
                        {
                            Directory.CreateDirectory(csvFilePath);
                        }

                        postedFile.SaveAs(csvFilePath + fileName);

                        StreamReader csv = new StreamReader(postedFile.InputStream);

                        DataTable dtCSV = new DataTable();
                        dtCSV.Columns.Add("id");
                        dtCSV.Columns.Add("misempcode");
                        dtCSV.Columns.Add("bioempcode");
                        dtCSV.Columns.Add("punchindate");
                        dtCSV.Columns.Add("punchintime");
                        dtCSV.Columns.Add("punchoutdate");
                        dtCSV.Columns.Add("punchouttime");
                        dtCSV.Columns.Add("ipaddress");

                        while (!csv.EndOfStream)
                        {
                            string[] rows = csv.ReadLine().Split(',');


                            int count = dtCSV.AsEnumerable().Where(x => x["bioempcode"].ToString() == rows[0] && x["punchindate"].ToString() == rows[1]).ToList().Count;

                            if (count > 0)
                            {
                                DataRow dr = dtCSV.AsEnumerable().Where(x => x["bioempcode"].ToString() == rows[0] && x["punchindate"].ToString() == rows[1]).ToList().FirstOrDefault();
                                dr["punchoutdate"] = rows[1];
                                dr["punchouttime"] = rows[2];
                            }
                            else
                            {
                                DataRow dr = dtCSV.NewRow();
                                dr["bioempcode"] = rows[0];
                                dr["punchindate"] = rows[1];
                                dr["punchintime"] = rows[2];
                                dr["ipaddress"] = rows[4];
                                dtCSV.Rows.Add(dr);
                            }
                        }

                        if (dtCSV != null)
                        {
                            Attendance attendance;
                            StringBuilder query = new StringBuilder();
                            foreach (DataRow dr in dtCSV.Rows)
                            {
                                attendance = new Attendance();
                                attendance.BioEmpCode = dr["bioempcode"].ToString().Trim();

                                string inDate = "0001-01-01";
                                string outDate = "0001-01-01";
                                if (!string.IsNullOrEmpty(dr["punchindate"].ToString()))
                                {
                                    inDate = dr["punchindate"].ToString().Split("/")[2] + "-" + dr["punchindate"].ToString().Split("/")[1] + "-" + dr["punchindate"].ToString().Split("/")[0];
                                }

                                if (!string.IsNullOrEmpty(dr["punchoutdate"].ToString()))
                                {
                                    outDate = dr["punchoutdate"].ToString().Split("/")[2] + "-" + dr["punchoutdate"].ToString().Split("/")[1] + "-" + dr["punchoutdate"].ToString().Split("/")[0];
                                }

                                attendance.PunchInTime = DateTime.Parse(inDate + " " + dr["punchintime"].ToString());
                                attendance.PunchOutTime = DateTime.Parse(outDate + " " + dr["punchouttime"].ToString());
                                attendance.IPAddress = dr["ipaddress"].ToString();
                                db.InsertAttendanceRecord(db.BuildAttendanceInsertQuery(attendance));
                            }


                        }
                    }

                    result = Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = true, SuccessMessage = "File imported Successfully" });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Please upload file" });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public HttpResponseMessage SaveStudentElectives(Electives elective)
        {

            ExamDbOperations db = new ExamDbOperations();

            if (db.IsElectiveExist(elective.CollegeRegistartionNumber, elective.Semester, elective.Year))
            {
                db.ExcuteUpdateQuery(db.GetUpdateElectiveQuery(elective));
            }
            else
            {
                db.InsertElective(db.GetInsertElectiveQuery(elective));
            }

            return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = true, SuccessMessage = "Electives saved successfully" });
        }

        [HttpPost]
        public HttpResponseMessage SaveApprovedElectives(List<Electives> electives)
        {
            ExamDbOperations db = new ExamDbOperations();

            foreach (Electives elective in electives)
            {
                db.ExcuteUpdateQuery(db.GetUpdateApprovedElectiveQuery(elective));
            }

            return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = true, SuccessMessage = "Electives approved successfully" });
        }

        [HttpGet]
        public HttpResponseMessage GetStudentElectives(string program, string specialisation, string semester, string year)
        {
            ExamDbOperations db = new ExamDbOperations();
            return Request.CreateResponse(HttpStatusCode.OK, db.GetStudentElectives(program,specialisation, semester, year));
        }

        [HttpGet]
        public HttpResponseMessage GetStudentElective(string crn, string semester)
        {
            ExamDbOperations db = new ExamDbOperations();
            return Request.CreateResponse(HttpStatusCode.OK, db.GetStudentElective(crn, semester));
        }

        [HttpGet]
        public HttpResponseMessage GetAttendanceSummary(string bioEmpCode, int month, int year)
        {
            ExamDbOperations db = new ExamDbOperations();
            DataTable attendance = db.GetAttendanceSummary(bioEmpCode, month, year);
            return Request.CreateResponse(HttpStatusCode.OK, attendance);
        }

        public class Electives
        {
            public string CollegeRegistartionNumber { get; set; }
            public int RollNumber { get; set; }
            public string StudentName { get; set; }
            public string Year { get; set; }
            public string Program { get; set; }
            public string Department { get; set; }
            public string Specialisation { get; set; }
            public string Semester { get; set; }
            public string Elective1 { get; set; }
            public string Elective2 { get; set; }
            public string Elective3 { get; set; }
            public string Elective4 { get; set; }
            public string Elective5 { get; set; }
            public string Elective6 { get; set; }
            public string Elective7 { get; set; }
            public string ApprovedElective1 { get; set; }
            public string ApprovedElective2 { get; set; }
            public string ApprovedElective3 { get; set; }
            public string ApprovedElective4 { get; set; }
            public string ApprovedElective5 { get; set; }
            public string ApprovedElective6 { get; set; }
            public string ApprovedElective7 { get; set; }
            public string ApprovedBy { get; set; }
        }

        public class Attendance
        {
            public int Id { get; set; }
            public string MisEmpCode { get; set; }
            public string BioEmpCode { get; set; }
            public DateTime PunchInTime { get; set; }
            public DateTime PunchOutTime { get; set; }
            public string IPAddress { get; set; }
        }
    }
}
