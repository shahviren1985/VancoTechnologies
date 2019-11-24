using CollegeExamService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Http;

namespace CollegeExamService.Controllers
{
    public class UserController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Test()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Its Works");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public HttpResponseMessage Login(UserDetails user)
        {
            if (user == null || string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
                return Request.CreateResponse(HttpStatusCode.OK, new UserDetails() { IsSuccess = false, ErrorMessage = "Invalid Data" });
            try
            {
                string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/userlist.json");
                List<UserDetails> response = JsonConvert.DeserializeObject<List<UserDetails>>(System.IO.File.ReadAllText(filePath));
                UserDetails userDetails = response.FirstOrDefault(a => a.UserName.Equals(user.UserName) && a.Password.Equals(user.Password));
                if (userDetails == null)
                    return Request.CreateResponse(HttpStatusCode.OK, new UserDetails() { IsSuccess = false, ErrorMessage = "Invalid Credentials" });
                else
                {
                    userDetails.IsSuccess = true;
                    userDetails.SuccessMessage = "Success";
                    return Request.CreateResponse(HttpStatusCode.OK, userDetails);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public HttpResponseMessage StaffLogin(UserDetails user)
        {
            if (user == null || string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
                return Request.CreateResponse(HttpStatusCode.OK, new UserDetails() { IsSuccess = false, ErrorMessage = "Invalid Data" });
            try
            {
                string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/stafflist.json");
                List<UserDetails> response = JsonConvert.DeserializeObject<List<UserDetails>>(System.IO.File.ReadAllText(filePath));
                UserDetails userDetails = response.FirstOrDefault(a => a.UserName.Equals(user.UserName) && a.Password.Equals(user.Password));
                if (userDetails == null)
                    return Request.CreateResponse(HttpStatusCode.OK, new UserDetails() { IsSuccess = false, ErrorMessage = "Invalid Credentials" });
                else
                {
                    userDetails.IsSuccess = true;
                    userDetails.SuccessMessage = "Success";
                    return Request.CreateResponse(HttpStatusCode.OK, userDetails);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public HttpResponseMessage GetUserList()
        {
            try
            {
                int userId = 0;
                string value = Convert.ToString(HttpContext.Current.Request.RequestContext.RouteData.Values["id"]);
                if (!string.IsNullOrEmpty(value))
                    int.TryParse(value, out userId);

                string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/userlist.json");
                List<UserDetails> response = JsonConvert.DeserializeObject<List<UserDetails>>(System.IO.File.ReadAllText(filePath));
                if (userId > 0)
                {
                    UserDetails userDetails = response.FirstOrDefault(a => a.UserId == userId);
                    if (userDetails == null)
                        return Request.CreateResponse(HttpStatusCode.OK, new UserDetails() { IsSuccess = false, ErrorMessage = "User not found" });
                    else
                    {
                        userDetails.IsSuccess = true;
                        userDetails.SuccessMessage = "Success";
                        return Request.CreateResponse(HttpStatusCode.OK, userDetails);
                    }
                }
                else
                {
                    UserList list=new UserList();
                    list.IsSuccess=true;
                    list.SuccessMessage="Success";
                    list.Users=response;
                    return Request.CreateResponse(HttpStatusCode.OK, list);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        public HttpResponseMessage GetExamList(string course, string sem)
        {
            try
            {
                if (string.IsNullOrEmpty(course) || string.IsNullOrEmpty(sem))
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Invalid Data" });

                string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/" + course.ToLower() + "/");
                if (sem.Equals("1") || sem.Equals("2"))
                {
                    filePath = filePath + "examdetails_sem_1_2.json";
                }
                else if (sem.Equals("3") || sem.Equals("4"))
                {
                    filePath = filePath + "examdetails_sem_3_4.json";
                }
                else
                {
                    filePath = filePath + "examdetails_sem_5_6.json";
                }

                if (!File.Exists(filePath))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "File does not exists" });
                }
                else
                {
                    List<ExamDetails> response = JsonConvert.DeserializeObject<List<ExamDetails>>(System.IO.File.ReadAllText(filePath));
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public HttpResponseMessage GetPaperList(string course, string specialization, string sem)
        {
            if (string.IsNullOrEmpty(course) || string.IsNullOrEmpty(specialization) || string.IsNullOrEmpty(sem))
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Data");
            try
            {
                string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/" + course.ToLower() + "/" + specialization.ToLower() + "/sem" + sem.ToLower() + ".json");
                if (!File.Exists(filePath))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Invalid Data" });
                }
                else
                {
                    //List<CourseDetails> response = JsonConvert.DeserializeObject<List<CourseDetails>>(System.IO.File.ReadAllText(filePath));
                    var data=JsonConvert.DeserializeObject(System.IO.File.ReadAllText(filePath));
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        public HttpResponseMessage GetGEList(string course, string specialization, string sem)
        {
            if (string.IsNullOrEmpty(course) || string.IsNullOrEmpty(specialization) || string.IsNullOrEmpty(sem))
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Data");
            try
            {
                string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/" + course.ToLower() + "/" + specialization.ToLower() + "/sem" + sem.ToLower() + "GE.json");
                if (!File.Exists(filePath))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Invalid Data" });
                }
                else
                {
                    List<GeneralElective> response = JsonConvert.DeserializeObject<List<GeneralElective>>(System.IO.File.ReadAllText(filePath));
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public HttpResponseMessage UploadUsers()
        {
            try
            {
                string filePath = string.Empty;
                string userFilePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/userlist.json");
                List<UserDetails> userList = JsonConvert.DeserializeObject<List<UserDetails>>(System.IO.File.ReadAllText(userFilePath));
                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    foreach (string file in HttpContext.Current.Request.Files)
                    {
                        var postedFile = HttpContext.Current.Request.Files[file];
                        if (postedFile != null && postedFile.ContentLength > 0)
                        {
                            string path = System.Web.Configuration.WebConfigurationManager.AppSettings["CsvFolderPath"];
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            filePath = path + Path.GetFileName(postedFile.FileName);
                            postedFile.SaveAs(filePath);
                            //Execute a loop over the rows.
                            List<UserDetails> users = System.IO.File.ReadAllLines(filePath)
                                                  .Skip(1)
                                                  .Select(v => UserDetails.FromCsv(v))
                                                  .ToList();
                            if (users != null && users.Count > 0)
                            {
                                foreach (UserDetails response in users)
                                {
                                    if (response.UserId == 0)
                                    {
                                        int intIdt = userList.Max(u => u.UserId);
                                        response.UserId = intIdt + 1;
                                        userList.Add(response);
                                    }
                                    else
                                    {
                                        UserDetails details = userList.FirstOrDefault(a => a.UserId == response.UserId);
                                        var index = userList.IndexOf(details);
                                        if (index != -1)
                                        {
                                            response.Paper = details.Paper;
                                            userList[index] = response;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Invalid Data" });
                        }
                    }
                    string archivePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/userlist_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".json");
                    System.IO.File.Move(userFilePath, archivePath);
                    System.IO.File.WriteAllText(userFilePath, JsonConvert.SerializeObject(userList));
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = true, SuccessMessage = "Records inserted successfully" });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Invalid Data" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public HttpResponseMessage DownloadUsers()
        {
            string csvString = string.Empty;
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/userlist.json");
            List<User> response = JsonConvert.DeserializeObject<List<User>>(System.IO.File.ReadAllText(filePath));
            DataTable dt = GetDataTableFromObjects(response.ToArray());
            if (dt != null)
            {
                csvString = ExportDataTableToCSV(dt);
            }
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(csvString);
            writer.Flush();
            stream.Position = 0;
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("text/csv");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = "Users.csv" };
            return result;
        }

        [HttpPost]
        public HttpResponseMessage UpdatePaperDetails(UserDetails userDetails)
        {
            try
            {
                string filePath = string.Empty;
                string userFilePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/userlist.json");
                List<UserDetails> userList = JsonConvert.DeserializeObject<List<UserDetails>>(System.IO.File.ReadAllText(userFilePath));
                UserDetails details = userList.FirstOrDefault(a => a.UserId == userDetails.UserId);
                var index = userList.IndexOf(details);
                if (index != -1)
                {
                    details.Paper = userDetails.Paper;
                    userList[index] = details;
                }
                string archivePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/userlist_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".json");
                System.IO.File.Move(userFilePath, archivePath);
                System.IO.File.WriteAllText(userFilePath, JsonConvert.SerializeObject(userList, Formatting.Indented));
                return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = true, SuccessMessage = "Records inserted successfully" });

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static DataTable GetDataTableFromObjects(object[] objects)
        {

            if (objects != null && objects.Length > 0)
            {

                Type t = objects[0].GetType();

                DataTable dt = new DataTable(t.Name);

                foreach (PropertyInfo pi in t.GetProperties())
                {

                    dt.Columns.Add(new DataColumn(pi.Name));

                }

                foreach (var o in objects)
                {

                    DataRow dr = dt.NewRow();

                    foreach (DataColumn dc in dt.Columns)
                    {

                        dr[dc.ColumnName] = o.GetType().GetProperty(dc.ColumnName).GetValue(o, null);

                    }

                    dt.Rows.Add(dr);

                }

                return dt;

            }

            return null;

        }

        public string ExportDataTableToCSV(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                sb.Append(dt.Columns[i].ColumnName + ',');
            }
            sb.Append(Environment.NewLine);

            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    sb.Append(dt.Rows[j][k].ToString().Replace(",", " ") + ',');
                }
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }

    }
}
