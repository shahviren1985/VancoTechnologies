using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using TT3.Models;

namespace TT3.Helpers
{
    public class Repository
    {
        private readonly string strpath = System.Web.Hosting.HostingEnvironment.MapPath("~/Data/ProfessorMaster.json");
        private readonly string strpathRoomJson = System.Web.Hosting.HostingEnvironment.MapPath("~/Data/Room.json");
        private readonly string strpathExamJson = System.Web.Hosting.HostingEnvironment.MapPath("~/Data/Exam.json");
        private readonly string strCommonPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Data/");
        public List<Professor> GetListOfProfessor(string GroupName)
        {
            List<Professor> liprof = new List<Professor>();
            try
            {
                string path = System.Web.Hosting.HostingEnvironment.MapPath("~/Data/" + GroupName + "");
                if (!Directory.Exists(path))
                {
                    return liprof;
                }
                if (!File.Exists(path + "/ProfessorMaster.json"))
                {
                    return liprof;
                }
                using (StreamReader r = new StreamReader(path+ "/ProfessorMaster.json"))
                {
                    string json = r.ReadToEnd();
                    liprof = JsonConvert.DeserializeObject<List<Professor>>(json);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return liprof;
        }
        public string GetListOfRoom(string GroupName)
        {
            string json = "";
            try
            {
                string path = System.Web.Hosting.HostingEnvironment.MapPath("~/Data/" + GroupName + "");
                if (!Directory.Exists(path))
                {
                    return "";
                }
                if (!File.Exists(path + "/Room.json"))
                {
                    return "";
                }
                using (StreamReader r = new StreamReader(path+ "/Room.json"))
                {
                    json = r.ReadToEnd();       
                }
            }
            catch (Exception)
            {
                throw;
            }
            return json;
        }
        public bool SaveRoomList(string strList, string GroupName)
        {
            try
            {
                string path = System.Web.Hosting.HostingEnvironment.MapPath("~/Data/"+GroupName+"");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                if (!File.Exists(path+ "/Room.json"))
                {
                    File.Create(path + "/Room.json").Dispose();
                }
                System.IO.File.WriteAllText(path + "/Room.json", JsonPrettify(strList));
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public bool SaveProfessorList(string strList, string GroupName)
        {
            try
            {
                string path = System.Web.Hosting.HostingEnvironment.MapPath("~/Data/" + GroupName + "");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                if (!File.Exists(path + "/ProfessorMaster.json"))
                {
                    File.Create(path + "/ProfessorMaster.json").Dispose();
                }
                System.IO.File.WriteAllText(path + "/ProfessorMaster.json", JsonPrettify(strList));
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }

        public string GetAllExamList(string GroupName)
        {
            string liprof = "";
            try
            {
                string path = System.Web.Hosting.HostingEnvironment.MapPath("~/Data/" + GroupName + "");
                if (!Directory.Exists(path))
                {
                    return "";
                }
                if (!File.Exists(path + "/Exam.json"))
                {
                    return "";
                }
                using (StreamReader r = new StreamReader(path + "/Exam.json"))
                {
                    liprof = r.ReadToEnd();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return liprof;
        }
        public bool SaveExamList(string strList, string GroupName)
        {
            try
            {
                string path = System.Web.Hosting.HostingEnvironment.MapPath("~/Data/" + GroupName + "");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                if (!File.Exists(path + "/Exam.json"))
                {
                    File.Create(path + "/Exam.json").Dispose();
                }
                System.IO.File.WriteAllText(path + "/Exam.json", JsonPrettify(strList));
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public string GetAllCommonList(string Filename, string GroupName)
        {
            string liprof = "";
            try
            {
                string path = System.Web.Hosting.HostingEnvironment.MapPath("~/Data/" + GroupName + "");
                if (!Directory.Exists(path))
                {
                    return "";
                }
                if (!File.Exists(path +"/"+ Filename))
                {
                    return "";
                }
                using (StreamReader r = new StreamReader(path + "/" + Filename))
                {
                    liprof = r.ReadToEnd();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return liprof;
        }
        public static string JsonPrettify(string json)
        {
            using (var stringReader = new StringReader(json))
            using (var stringWriter = new StringWriter())
            {
                var jsonReader = new JsonTextReader(stringReader);
                var jsonWriter = new JsonTextWriter(stringWriter) { Formatting = Formatting.Indented };
                jsonWriter.WriteToken(jsonReader);
                return stringWriter.ToString();
            }
        }
        public bool SaveCommonFile(string strList, string filename, string GroupName)
        {
            try
            {
                string path = System.Web.Hosting.HostingEnvironment.MapPath("~/Data/" + GroupName + "");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                if (!File.Exists(path + "/"+ filename))
                {
                    File.Create(path + "/" + filename).Dispose();
                }
                System.IO.File.WriteAllText(path + "/" + filename, JsonPrettify(strList));
                System.IO.File.WriteAllText(strCommonPath + filename, JsonPrettify(System.Uri.UnescapeDataString(strList)));
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public User CheckLoginUser(User user)
        {
            List<User> liUser = new List<User>();
            if (user == null || user.Username == null || user.Password == null) {
                return null;
            }
            try
            {
                using (StreamReader r = new StreamReader(strCommonPath+"User.json"))
                {
                    string json = r.ReadToEnd();
                    liUser = JsonConvert.DeserializeObject<List<User>>(json);
                }
                if (liUser.Count > 0) {
                    foreach (User a in liUser) {
                        if (a.Password == user.Password && a.Username.ToLower() == user.Username.ToLower()) {
                            return a;
                        }
                    }
                }
                
            }
            catch (Exception)
            {
                throw;
            }
            return null;
        }
    }
}