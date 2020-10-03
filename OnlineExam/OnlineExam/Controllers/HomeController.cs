using Newtonsoft.Json;
using OnlineExam.Helpers;
using OnlineExam.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc;

namespace OnlineExam.Controllers
{
    public class HomeController : Controller
    {
        private static Dictionary<string, List<QuestionMaster>> questionMaster = new Dictionary<string, List<QuestionMaster>>();
        DatabaseOperations db = new DatabaseOperations();

        public HomeController()
        {
            if (questionMaster == null || questionMaster.Count == 0)
            {
                string[] filePaths = Directory.GetFiles(System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/"), "*.json", SearchOption.TopDirectoryOnly);

                foreach (string filePath in filePaths)
                {
                    string allText = System.IO.File.ReadAllText(filePath);
                    FileInfo file = new FileInfo(filePath);

                    if (!questionMaster.ContainsKey(file.Name.Replace(file.Extension, "")))
                    {
                        questionMaster.Add(file.Name.Replace(file.Extension, ""), JsonConvert.DeserializeObject<List<QuestionMaster>>(allText));
                    }
                }
            }
        }


        [HttpGet]
        public JsonResult ValidateUser(string examid)
        {
            var response = db.IsValidExam(examid);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult StartExam(string examid, string subjectCode)
        {
            string time = ConfigurationManager.AppSettings[subjectCode];
            db.StartExam(examid);
            var response = new ExamValidation { IsExamExpired = false, IsExamValid = true, TimeRemaining = int.Parse(time) };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult UpdateTime(string examId)
        {
            db.UpdateExamTime(examId);
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SubmitAnswer(Answer answer)
        {
            if (!Directory.Exists(HostingEnvironment.MapPath(@"~/App_Data/Answers/" + answer.subjectCode + "/" + answer.examid)))
            {
                Directory.CreateDirectory(HostingEnvironment.MapPath(@"~/App_Data/Answers/" + answer.subjectCode + "/" + answer.examid));
            }

            answer.answer = answer.answer.Trim();

            string path = HostingEnvironment.MapPath(@"~/App_Data/Answers/" + answer.subjectCode + "/" + answer.examid + "/" + answer.qid + ".json");
            System.IO.File.WriteAllText(path, JsonConvert.SerializeObject(answer));

            // Database call - submit the answer
            if (questionMaster != null)
            {
                var question = questionMaster[answer.subjectCode].Where(q => q.id == answer.qid).FirstOrDefault();
                answer.correctAnswer = question.correctAnswer;
            }

            db.SaveAnswer(answer);
            return Json(new { Success = true });
        }

        [HttpGet]
        public JsonResult FinishExam(string examId, string subjectCode, bool isDB)
        {
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/Answers/" + subjectCode + "/" + examId + "/Questions.json");
            string allText = string.Empty;
            if (System.IO.File.Exists(filePath))
            {
                allText = System.IO.File.ReadAllText(filePath);
            }

            List<Questions> response = JsonConvert.DeserializeObject<List<Questions>>(allText);

            int counter = 1;
            int userCorrectResponse = 0;

            foreach (Questions question in response)
            {
                question.correctResponse = questionMaster[subjectCode].Find(q => q.id == question.id).correctAnswer;
                var answer = new Answer
                {
                    answer = question.answer.Trim(),
                    category = question.category,
                    correctAnswer = question.correctResponse,
                    examid = examId,
                    qid = question.id,
                    status = "Submitted",
                    serial = counter
                };

                if (answer.answer != null && question.correctResponse != null)
                {
                    answer.answer = answer.answer.Trim();
                    question.correctResponse = question.correctResponse.Trim();
                }

                if (question.correctResponse == answer.answer)
                {
                    userCorrectResponse++;
                }

                if (isDB)
                {
                    db.SaveAnswer(answer);
                }

                counter++;
            }

            db.FinishExam(examId);

            return Json(new { Success = true, CorrectAnswers = userCorrectResponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveAllAnswers(List<Questions> answers, string examId, string subjectCode)
        {
            string path = HostingEnvironment.MapPath(@"~/App_Data/Answers/" + subjectCode + "/" + examId + "/Questions.json");

            foreach (var answer in answers)
            {
                if (answer.answer != null)
                    answer.answer = answer.answer.Trim();
            }

            System.IO.File.WriteAllText(path, JsonConvert.SerializeObject(answers));
            return Json(new { Success = true });
        }

        [HttpGet]
        public JsonResult GetQuestions(string examId, string subjectCode)
        {
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/Answers/" + subjectCode + "/" + examId + "/Questions.json");
            string allText = string.Empty;
            if (System.IO.File.Exists(filePath))
            {
                allText = System.IO.File.ReadAllText(filePath);
            }
            else
            {
                // Create new file by selecting right questions - 100 from each category
                Directory.CreateDirectory(System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/Answers/" + subjectCode + "/" + examId));
                List<KeyValuePair<int, int>> categoryTotalQuestions = null;

                switch (subjectCode)
                {
                    case "ASNFS":
                        categoryTotalQuestions = new List<KeyValuePair<int, int>>() {
                                                    new KeyValuePair<int,int>(1,20),
                                                    new KeyValuePair<int,int>(2,10),
                                                    new KeyValuePair<int,int>(3,15),
                                                    new KeyValuePair<int,int>(4,15),
                                                    new KeyValuePair<int,int>(5,20),
                                                    new KeyValuePair<int,int>(6,10),
                                                    new KeyValuePair<int,int>(7,10)
                                                };
                        break;
                    case "FCIV13":
                        categoryTotalQuestions = new List<KeyValuePair<int, int>>() {
                                                    new KeyValuePair<int,int>(1,30)
                                                };
                        break;
                    case "FCIV14":
                        categoryTotalQuestions = new List<KeyValuePair<int, int>>() {
                                                    new KeyValuePair<int,int>(1,10),
                                                    new KeyValuePair<int,int>(2,5)
                                                };
                        break;

                    case "FCIV16":
                        categoryTotalQuestions = new List<KeyValuePair<int, int>>() {
                                                    new KeyValuePair<int,int>(1,30)
                                                };
                        break;
                    case "FCVI22":
                        categoryTotalQuestions = new List<KeyValuePair<int, int>>() {
                                                    new KeyValuePair<int,int>(1,10),
                                                    new KeyValuePair<int,int>(2,10)
                                                }; 
                        break;
                    case "FCVI27":
                        categoryTotalQuestions = new List<KeyValuePair<int, int>>() {
                                                    new KeyValuePair<int,int>(1,40)
                                                };
                        break;
                    case "FG101":
                        categoryTotalQuestions = new List<KeyValuePair<int, int>>() {
                                                    new KeyValuePair<int,int>(1,30)
                                                };
                        break;
                    case "Demo":
                        categoryTotalQuestions = new List<KeyValuePair<int, int>>() {
                                                    new KeyValuePair<int,int>(1,5)
                                                };
                        break;
                    case "SN101":
                        categoryTotalQuestions = new List<KeyValuePair<int, int>>() {
                                                    new KeyValuePair<int,int>(1,50)
                                                };
                        break;
                }


                List<Questions> finalQuestions = new List<Questions>();
                // Loop through each category - mentioned in the excel file
                for (int i = 1; i <= 7; i++)
                {
                    var categoryQuestions = questionMaster[subjectCode].FindAll(c => c.category == i.ToString());
                    int totalQuestionsCount = categoryTotalQuestions.Where(q => q.Key == i).FirstOrDefault().Value;
                    var qList = categoryQuestions.OrderBy(arg => Guid.NewGuid()).Take(totalQuestionsCount).ToList();

                    foreach (var q in qList)
                    {
                        finalQuestions.Add(new Questions()
                        {
                            id = q.id,
                            category = q.category,
                            optionValues = q.optionValues,
                            question = q.question,
                            type = q.type,
                            answer = string.Empty
                        });
                    }
                }

                string path = HostingEnvironment.MapPath(@"~/App_Data/Answers/" + subjectCode + "/" + examId + "/Questions.json");
                System.IO.File.WriteAllText(path, JsonConvert.SerializeObject(finalQuestions));
                allText = System.IO.File.ReadAllText(path);
            }

            List<Questions> response = JsonConvert.DeserializeObject<List<Questions>>(allText);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetStudentList(string subjectCode)
        {
            var students = db.GetStudents(subjectCode);
            List<StudentDetails> studentList = new List<StudentDetails>();

            foreach (DataRow row in students.Tables[0].Rows)
            {
                studentList.Add(new StudentDetails
                {
                    FirstName = row["firstname"].ToString(),
                    LastName = row["lastname"].ToString(),
                    Email = row["email"].ToString(),
                    Mobile = row["mobile"].ToString(),
                    Membership = row["membership"].ToString(),
                    CertificatePdfLink = row["certificatepdflink"].ToString(),
                    Status = row["status"].ToString(),
                    ExamId = row["examid"].ToString(),
                    StartTime = row["starttime"].ToString(),
                    CorrectAnswers = row["correctanswers"].ToString()
                });
            }

            return Json(studentList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetStudentExamResponse(string examId, string subjectCode)
        {
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/Answers/" + subjectCode + "/" + examId + "/Questions.json");
            string allText = string.Empty;
            if (System.IO.File.Exists(filePath))
            {
                allText = System.IO.File.ReadAllText(filePath);
            }

            List<Questions> response = JsonConvert.DeserializeObject<List<Questions>>(allText);

            foreach (Questions question in response)
            {
                question.correctResponse = questionMaster[subjectCode].Find(q => q.id == question.id).correctAnswer;
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CreateUser(StudentDetails student)
        {
            if (student.Membership == null)
                student.Membership = "Silver";

            db.CreateUser(student);
            return Json(new { Success = true });
        }
    }
}