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
    public class DataController : ApiController
    {
        ExamDbOperations db = null;

        public DataController()
        {
            db = new ExamDbOperations();
        }

        [HttpPost]
        public HttpResponseMessage GenerateExamCsv(string course, string specialization, string sem, string examType, string year)
        {
            try
            {
                if (string.IsNullOrEmpty(course) || string.IsNullOrEmpty(specialization) || string.IsNullOrEmpty(sem) || string.IsNullOrEmpty(examType) || string.IsNullOrEmpty(year))
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Invalid Data" });

                HttpResponseMessage result = null;
                //string currentSem = int.Parse(sem) % 2 == 0 ? "Even" : "Odd";
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    foreach (string currentFile in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[currentFile];
                        string courseName = string.Empty;
                        string subCourseName = string.Empty;
                        string filePath = string.Empty;
                        string csvFilePath = string.Empty;
                        filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/" + course.ToLower() + "/" + specialization.ToLower() + "/sem" + sem.ToLower() + ".json");
                        List<CourseDetails> courseList = JsonConvert.DeserializeObject<List<CourseDetails>>(System.IO.File.ReadAllText(filePath));

                        string path = System.Web.Configuration.WebConfigurationManager.AppSettings["CsvFolderPath"];
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        csvFilePath = path + Path.GetFileName(postedFile.FileName);
                        if (File.Exists(csvFilePath))
                        {
                            string archivePath = path + "/" + Path.GetFileNameWithoutExtension(postedFile.FileName) + "_" + DateTime.Now.ToString("yyyy-MM-dd HH mm ss") + Path.GetExtension(postedFile.FileName);
                            System.IO.File.Move(csvFilePath, archivePath);
                        }
                        postedFile.SaveAs(csvFilePath);

                        //Execute a loop over the rows.
                        //List<StudentDetails> students = System.IO.File.ReadAllLines(csvFilePath)
                        //                      .Skip(1)
                        //                      .Select(v => StudentDetails.FromCsv(v))
                        //                      .ToList();
                        StreamReader csv = new StreamReader(postedFile.InputStream);
                        string[] headers = csv.ReadLine().Split(',');
                        DataTable dtCSV = new DataTable();
                        foreach (string header in headers)
                        {
                            dtCSV.Columns.Add(header);
                        }
                        while (!csv.EndOfStream)
                        {
                            string[] rows = csv.ReadLine().Split(',');
                            DataRow dr = dtCSV.NewRow();
                            for (int i = 0; i < headers.Length; i++)
                            {
                                dr[i] = rows[i];
                            }
                            dtCSV.Rows.Add(dr);
                        }
                        if (dtCSV != null)
                        {
                            DataTable table = dtCSV.Copy();
                            DataColumn Col = table.Columns.Add("SeatNumber", System.Type.GetType("System.String"));
                            Col.SetOrdinal(0);
                            table.Columns.Add("ExamType", System.Type.GetType("System.String"));
                            table.Columns.Add("Year", System.Type.GetType("System.String"));
                            table.Columns.Add("Semester", System.Type.GetType("System.String"));

                            if (table != null & table.Rows.Count > 0)
                            {
                                foreach (DataRow row in table.Rows)
                                {
                                    courseName = row["Course"].ToString();
                                    subCourseName = row["Specialisation"].ToString();
                                    row["ExamType"] = examType;
                                    row["Year"] = year;
                                    row["Semester"] = sem;

                                    CourseDetails details = courseList.FirstOrDefault(a => (a.CourseShortName.ToLower().Replace(" ", string.Empty)) == (courseName.ToLower().Replace(" ", string.Empty)) && (a.SpecialisationCode.ToLower().Replace(" ", string.Empty)) == (subCourseName.ToLower().Replace(" ", string.Empty)));
                                    if (details == null)
                                        return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "No Course and specialisation matching found" });
                                    List<PaperDetail> paper = details.PaperDetails;
                                    if (paper != null && paper.Count > 0)
                                    {
                                        int current = 1;
                                        for (int i = 0; i < paper.Count; i++)
                                        {
                                            if (paper[i].IsElective.ToLower().Equals("yes"))
                                            {
                                                string paperTitle = paper[i].PaperTitle.ToLower();
                                                string g1 = Convert.ToString(row["GeneralElective1"]) == null ? "" : Convert.ToString(row["GeneralElective1"]);
                                                string g2 = Convert.ToString(row["GeneralElective2"]) == null ? "" : Convert.ToString(row["GeneralElective2"]);
                                                //string g3 = Convert.ToString(row["GeneralElective3"]) == null ? "" : Convert.ToString(row["GeneralElective3"]);
                                                //string g4 = Convert.ToString(row["GeneralElective4"]) == null ? "" : Convert.ToString(row["GeneralElective4"]);

                                                //if (!(paperTitle.Equals(g1.ToLower()) ||
                                                //    paperTitle.Equals(g2.ToLower()) ||
                                                //    paperTitle.Equals(g3.ToLower()) ||
                                                //    paperTitle.Equals(g4.ToLower())))
                                                //{
                                                //    continue;
                                                //}

                                                if (!(paperTitle.Equals(g1.ToLower()) ||
                                                    paperTitle.Equals(g2.ToLower())
                                                    ))
                                                {
                                                    continue;
                                                }
                                            }
                                            string clmpaperAppered = "Paper" + current + "Appeared";
                                            if (!table.Columns.Contains(clmpaperAppered))
                                                table.Columns.Add(clmpaperAppered, typeof(String));

                                            string clmCode = "Code" + current;
                                            if (!table.Columns.Contains(clmCode))
                                                table.Columns.Add(clmCode, typeof(String));

                                            string clmInternalMakrs = "InternalC" + current;
                                            if (!table.Columns.Contains(clmInternalMakrs))
                                                table.Columns.Add(clmInternalMakrs, typeof(String));

                                            string clmInternalMakrsEx = "ExternalSection1C" + current;
                                            if (!table.Columns.Contains(clmInternalMakrsEx))
                                                table.Columns.Add(clmInternalMakrsEx, typeof(String));

                                            string clmExternalMarks = "ExternalSection2C" + current;
                                            if (!table.Columns.Contains(clmExternalMarks))
                                                table.Columns.Add(clmExternalMarks, typeof(String));

                                            string clmExternalMarksEx = "ExternalTotalC" + current;
                                            if (!table.Columns.Contains(clmExternalMarksEx))
                                                table.Columns.Add(clmExternalMarksEx, typeof(String));

                                            string clmGraceMarks = "GraceC" + current;
                                            if (!table.Columns.Contains(clmGraceMarks))
                                                table.Columns.Add(clmGraceMarks, typeof(String));

                                            string clmTotalMarks = "PracticalMarksC" + current;
                                            if (!table.Columns.Contains(clmTotalMarks))
                                                table.Columns.Add(clmTotalMarks, typeof(String));

                                            row["Code" + current] = paper[i].Code;
                                            if (examType.ToLower().Equals("regular"))
                                            {
                                                row["Paper" + current + "Appeared"] = "*";
                                            }

                                            string clmAttempt = "Attempt" + current;
                                            if (!table.Columns.Contains(clmAttempt))
                                                table.Columns.Add(clmAttempt, typeof(String));

                                            string clmRemarks = "Remarks" + current;
                                            if (!table.Columns.Contains(clmRemarks))
                                                table.Columns.Add(clmRemarks, typeof(String));

                                            current = current + 1;
                                        }
                                    }
                                }

                                string newFileName = courseName.Replace(" ", string.Empty) + "_Sem" + sem + "_" + year + "_" + examType + ".csv";
                                string fullFilePath = path + "\\" + newFileName;
                                if (File.Exists(fullFilePath))
                                {
                                    string archivePath = path + "/" + Path.GetFileNameWithoutExtension(newFileName) + "_" + DateTime.Now.ToString("yyyy-MM-dd HH mm ss") + Path.GetExtension(newFileName);
                                    System.IO.File.Move(fullFilePath, archivePath);
                                }
                                FileHelper.ExportDataTableToCSV(table, fullFilePath);
                            }

                        }
                    }
                    result = Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = true, SuccessMessage = "File Generated Successfully" });
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
        public HttpResponseMessage SaveATKTStudent(StudentATKTDetails sDetails)
        {
            try
            {
                HttpResponseMessage result = null;
                if (sDetails == null || string.IsNullOrEmpty(sDetails.StudentDetails.Course) || string.IsNullOrEmpty(sDetails.StudentDetails.Specialisation) || string.IsNullOrEmpty(sDetails.Year) || string.IsNullOrEmpty(sDetails.Semester))
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Invalid Data" });

                #region Create New File or Archive existing file
                string newFileName = "BSc-" + sDetails.StudentDetails.Stream + "_sem" + sDetails.Semester + "_" + sDetails.Year + "_ATKT_Odd.csv";
                string path = System.Web.Configuration.WebConfigurationManager.AppSettings["CsvFolderPath"];
                string fullFilePath = path + "\\" + newFileName;

                DataTable dtCSV;

                if (!File.Exists(fullFilePath))
                {
                    GenerateATKTFile(sDetails);
                    //string archivePath = path + "/" + Path.GetFileNameWithoutExtension(newFileName) + "_" + DateTime.Now.ToString("yyyy-MM-dd HH mm ss") + Path.GetExtension(newFileName);
                    //System.IO.File.Move(fullFilePath, archivePath);
                }

                dtCSV = ReadATKTFile(fullFilePath);
                #endregion

                #region Add Record in Existing File
                DataRow dr = dtCSV.NewRow();

                dr["College_Registration_No_"] = sDetails.StudentDetails.CollegeRegistrationNumber;

                dr["LastName"] = sDetails.StudentDetails.LastName;
                dr["FirstName"] = sDetails.StudentDetails.FirstName;
                dr["FatherName"] = sDetails.StudentDetails.FatherName;
                dr["MotherName"] = sDetails.StudentDetails.MotherName;
                dr["PRN"] = sDetails.StudentDetails.Prn + "'";
                dr["CurrentAddress"] = string.Empty;
                dr["EmailAddress"] = sDetails.StudentDetails.EmailAddress;
                dr["MobileNumber"] = sDetails.StudentDetails.MobileNumber;
                dr["ExamType"] = "ATKT";
                dr["Semester"] = sDetails.Semester;
                dr["Year"] = sDetails.Year;
                int index = 1;

                foreach (var paper in sDetails.AtktMarksDetailList)
                {
                    dr["Code" + index] = paper.PaperCode;
                    dr["InternalC" + index] = paper.Internalmarksobtained;
                    index++;
                }

                dtCSV.Rows.Add(dr);

                FileHelper.ExportDataTableToCSV(dtCSV, fullFilePath);

                #endregion
                return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = true, SuccessMessage = "ATKT form submitted succesfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable ReadATKTFile(string strFilePath)
        {
            try
            {
                StreamReader sr = new StreamReader(strFilePath);
                string[] headers = sr.ReadLine().Split(',');
                DataTable dt = new DataTable();
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }
                while (!sr.EndOfStream)
                {
                    string[] rows = Regex.Split(sr.ReadLine(), ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i];
                    }
                    dt.Rows.Add(dr);
                }

                sr.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool GenerateATKTFile(StudentATKTDetails sDetails)
        {
            DataTable dtCSV = new DataTable();
            DataRow dr = dtCSV.NewRow();

            if (dtCSV != null)
            {
                DataTable table = dtCSV.Copy();
                DataColumn Col = table.Columns.Add("SeatNumber", System.Type.GetType("System.String"));
                Col.SetOrdinal(0);
                table.Columns.Add("SerialNumber", System.Type.GetType("System.String"));
                table.Columns.Add("RollNumber", System.Type.GetType("System.String"));
                table.Columns.Add("College_Registration_No_", System.Type.GetType("System.String"));
                table.Columns.Add("LastName", System.Type.GetType("System.String"));
                table.Columns.Add("FirstName", System.Type.GetType("System.String"));
                table.Columns.Add("FatherName", System.Type.GetType("System.String"));
                table.Columns.Add("MotherName", System.Type.GetType("System.String"));
                table.Columns.Add("PRN", System.Type.GetType("System.String"));
                table.Columns.Add("ExamType", System.Type.GetType("System.String"));
                table.Columns.Add("Year", System.Type.GetType("System.String"));
                table.Columns.Add("Semester", System.Type.GetType("System.String"));
                table.Columns.Add("CurrentAddress", System.Type.GetType("System.String"));
                table.Columns.Add("MobileNumber", System.Type.GetType("System.String"));
                table.Columns.Add("EmailAddress", System.Type.GetType("System.String"));

                List<AtktMarksDetail> paper = sDetails.AtktMarksDetailList;
                if (paper != null && paper.Count > 0)
                {
                    int current = 1;
                    for (int i = 0; i < 6; i++)
                    {
                        string clmpaperAppered = "Paper" + current + "Appeared";
                        if (!table.Columns.Contains(clmpaperAppered))
                            table.Columns.Add(clmpaperAppered, typeof(String));

                        string clmCode = "Code" + current;
                        if (!table.Columns.Contains(clmCode))
                            table.Columns.Add(clmCode, typeof(String));

                        string clmInternalMakrs = "InternalC" + current;
                        if (!table.Columns.Contains(clmInternalMakrs))
                            table.Columns.Add(clmInternalMakrs, typeof(String));

                        string clmInternalMakrsEx = "ExternalSection1C" + current;
                        if (!table.Columns.Contains(clmInternalMakrsEx))
                            table.Columns.Add(clmInternalMakrsEx, typeof(String));

                        string clmExternalMarks = "ExternalSection2C" + current;
                        if (!table.Columns.Contains(clmExternalMarks))
                            table.Columns.Add(clmExternalMarks, typeof(String));

                        string clmExternalMarksEx = "ExternalTotalC" + current;
                        if (!table.Columns.Contains(clmExternalMarksEx))
                            table.Columns.Add(clmExternalMarksEx, typeof(String));

                        string clmGraceMarks = "GraceC" + current;
                        if (!table.Columns.Contains(clmGraceMarks))
                            table.Columns.Add(clmGraceMarks, typeof(String));

                        string clmTotalMarks = "PracticalMarksC" + current;
                        if (!table.Columns.Contains(clmTotalMarks))
                            table.Columns.Add(clmTotalMarks, typeof(String));

                        //row["Code" + current] = paper[i].PaperCode;
                        string clmAttempt = "Attempt" + current;
                        if (!table.Columns.Contains(clmAttempt))
                            table.Columns.Add(clmAttempt, typeof(String));

                        string clmRemarks = "Remarks" + current;
                        if (!table.Columns.Contains(clmRemarks))
                            table.Columns.Add(clmRemarks, typeof(String));

                        current = current + 1;
                    }
                }


                string newFileName = "BSc-" + sDetails.StudentDetails.Stream + "_sem" + sDetails.Semester + "_" + sDetails.Year + "_ATKT_Odd.csv";
                string path = System.Web.Configuration.WebConfigurationManager.AppSettings["CsvFolderPath"];
                string fullFilePath = path + "\\" + newFileName;
                if (File.Exists(fullFilePath))
                {
                    string archivePath = path + "/" + Path.GetFileNameWithoutExtension(newFileName) + "_" + DateTime.Now.ToString("yyyy-MM-dd HH mm ss") + Path.GetExtension(newFileName);
                    System.IO.File.Move(fullFilePath, archivePath);
                }
                FileHelper.ExportDataTableToCSV(table, fullFilePath);
            }
            return true;
        }

        [HttpPost]
        public HttpResponseMessage SaveMarks([FromBody]dynamic model, [FromUri] string fileName)
        {
            string BaseFolderPath = System.Web.Hosting.HostingEnvironment.MapPath("~/data/SVT/");
            string archivePath = BaseFolderPath + Path.GetFileNameWithoutExtension(fileName) + "_" + DateTime.Now.ToString("yyyy-MM-dd HH mm ss") + Path.GetExtension(fileName);
            string csvFilePath = string.Empty;
            HttpResponseMessage result = null;
            try
            {
                string jsonFilePath = string.Empty;
                //System.Web.Configuration.WebConfigurationManager.AppSettings["CsvFolderPath"];
                string jsonString = Convert.ToString(model);
                //jsonString = jsonString + "#@$#$2";
                jsonFilePath = string.Format(BaseFolderPath + Path.GetFileNameWithoutExtension(fileName) + ".json");
                File.WriteAllText(jsonFilePath, jsonString);
                using (var r = new ChoJSONReader(jsonFilePath))
                {
                    csvFilePath = BaseFolderPath + Path.GetFileName(fileName);
                    if (File.Exists(csvFilePath))
                    {
                        System.IO.File.Move(csvFilePath, archivePath);
                    }
                    using (var w = new ChoCSVWriter(csvFilePath).WithFirstLineHeader())
                    {
                        w.Write(r);
                    }
                }
                File.Delete(jsonFilePath);
                result = Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = true, SuccessMessage = "Marks Saved Successfully" });
                return result;
            }
            catch (Exception ex)
            {
                if (File.Exists(csvFilePath))
                {
                    File.Delete(csvFilePath);
                }

                if (File.Exists(archivePath))
                {
                    System.IO.File.Move(archivePath, csvFilePath);
                }

                LogException("DataController", "SaveMarks", ex, new List<string>() { fileName }, model);
                result = Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, SuccessMessage = "Fail to save marks" });
            }
            return result;
        }

        public void LogException(string ControllerName, string MethodName, Exception ex, List<string> Data, dynamic model)
        {
            string BaseFolderPath = System.Web.Hosting.HostingEnvironment.MapPath("~/data/SVT/");
            string csvFilePath = BaseFolderPath + "Log" + DateTime.Now.Ticks + ".txt";
            string text = "ControllerName : " + ControllerName + " " + "MethodName : " + MethodName + System.Environment.NewLine;
            text = text + "Exception: " + ex.Message + System.Environment.NewLine;
            foreach (string d in Data)
            {
                text = text + "Data Values: " + d + System.Environment.NewLine;
            }
            text = text + Convert.ToString(model) + System.Environment.NewLine;
            File.WriteAllText(csvFilePath, text);
        }

        [HttpPost]
        public HttpResponseMessage SaveJsonFiles([FromBody]dynamic model, [FromUri] string fileName)
        {
            try
            {
                string csvFilePath = string.Empty;
                string jsonFilePath = string.Empty;
                HttpResponseMessage result = null;
                string BaseFolderPath = System.Web.Hosting.HostingEnvironment.MapPath("~/data/SVT/");
                //System.Web.Configuration.WebConfigurationManager.AppSettings["CsvFolderPath"];
                string jsonString = model;
                jsonFilePath = string.Format(BaseFolderPath + Path.GetFileNameWithoutExtension(fileName) + ".json");
                File.WriteAllText(jsonFilePath, jsonString);
                result = Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = true, SuccessMessage = "Marks Saved Successfully" });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet]
        public HttpResponseMessage GetAggregateMarksheetStudentDetails(string collegeRegistrationNumber)
        {
            CommonMarksheetResponse details = db.GetStudentDetailsByCollegeRegistrationNumber(collegeRegistrationNumber);
            return Request.CreateResponse(HttpStatusCode.OK, details);
        }

        [HttpGet]
        public HttpResponseMessage UpdateAggregateMarksheetStudentDetails(string firstName, string lastName, string fatherName, string motherName, string prn, string collegeRegistrationNumber)
        {
            return Request.CreateResponse(HttpStatusCode.OK, db.UpdateStudentDetails(lastName, firstName, fatherName, motherName, collegeRegistrationNumber, prn));
        }

        [HttpGet]
        public HttpResponseMessage GetAggregateMarksheetMarksDetails(int studentId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, db.GetPaperDetails(studentId));
        }

        [HttpGet]
        public HttpResponseMessage UpdateAggregateMarksheetMarks(int rowid, string studentId, string internalMarks, string externalMarks, string practicalMarks, string grade, string attempt, string paperCode)
        {
            db.UpdateMarkDetails(rowid.ToString(), studentId, paperCode, internalMarks, externalMarks, practicalMarks, grade, attempt);
            return Request.CreateResponse(HttpStatusCode.OK, "Success");
        }

        [HttpGet]
        public HttpResponseMessage TransferStudentFromHonorsToRegular(string studentId)
        {
            db.UpdateCourseFromHonorsToRegular(studentId);
            return Request.CreateResponse(HttpStatusCode.OK, "Success");
        }

        [HttpGet]
        public HttpResponseMessage SaveCoreSubjects(string type, string semester, string year)
        {
            DataTable dtCSV = new DataTable();

            string fileContent = string.Empty;
            string currentYear = DateTime.Now.Year.ToString();
            string originalFileName = "BSc-" + type + "_sem" + semester + "_" + currentYear + "_Regular.csv";
            string newFileName = "BSc-" + type + "_sem" + semester + "_" + currentYear + "_Regular_" + DateTime.Now.ToString("yyyy-MM-dd hh mm ss") + ".csv";
            string line = string.Empty;
            int lineCounter = 0;
            string[] headers = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["DataFilePath"]);
                string urlParams = string.Format("?course=bsc&type={0}&sem={1}&year={2}", type, semester, year);
                HttpResponseMessage response = client.GetAsync(urlParams).Result;

                if (response.IsSuccessStatusCode)
                {
                    fileContent = response.Content.ReadAsStringAsync().Result;
                    byte[] bytes = Encoding.ASCII.GetBytes(fileContent);

                    if (File.Exists(ConfigurationManager.AppSettings["CsvFolderPath"] + "\\" + originalFileName))
                    {
                        File.Move(ConfigurationManager.AppSettings["CsvFolderPath"] + "\\" + originalFileName, ConfigurationManager.AppSettings["CsvFolderPath"] + "\\" + newFileName);
                    }

                    File.WriteAllBytes(ConfigurationManager.AppSettings["CsvFolderPath"] + "\\" + originalFileName, bytes);
                }
            }

            string jsonPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/BSc/" + type + "/sem" + semester + ".json");
            List<CourseDetails> courseList = JsonConvert.DeserializeObject<List<CourseDetails>>(File.ReadAllText(jsonPath));

            StreamReader file = new StreamReader(ConfigurationManager.AppSettings["CsvFolderPath"] + "\\" + originalFileName);

            while ((line = file.ReadLine()) != null)
            {
                if (lineCounter == 0)
                {
                    headers = line.Split(',');
                    foreach (string header in headers)
                    {
                        dtCSV.Columns.Add(header);
                    }
                }
                else
                {
                    string[] rows = line.Split(',');
                    DataRow dr = dtCSV.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i];
                    }

                    dtCSV.Rows.Add(dr);
                }

                lineCounter++;
            }

            file.Close();

            foreach (DataRow row in dtCSV.Rows)
            {
                CourseDetails course = courseList.Find(p => p.SpecialisationCode == row["Specialisation"].ToString());
                int paperId = 1;
                foreach(PaperDetail paper in course.PaperDetails)
                {
                    if(paper.IsElective.ToLower() == "false" || paper.IsElective.ToLower() == "no")
                    {
                        row["Code" + paperId] = paper.paperCode;
                        paperId++;
                    }
                }
            }

            ToCSV(dtCSV, ConfigurationManager.AppSettings["CsvFolderPath"] + "\\" + originalFileName);

            return Request.CreateResponse(HttpStatusCode.OK, "Success");
        }

        public void ToCSV(DataTable dtDataTable, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false);
            //headers  
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                sw.Write(dtDataTable.Columns[i]);
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }
    }
}
