using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITM.Courses.DAO
{
    public class UserLogins
    {
        public int CollegeId { get; set; }
        public string CollegeName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CnxnString { get; set; }
        public string PDFFolderPath { get; set; }
        public string ReleaseFilePath { get; set; }
        public string LogFilePath { get; set; }
        public string UserType { get; set; }
        public string ApplicationType { get; set; }
        public string RedirectURL { get; set; }
        public bool IsAuthenticated { get; set; }
        public bool IsStatusTrackerRequired  { get; set; }
    }
}
