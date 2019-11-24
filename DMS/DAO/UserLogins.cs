using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AA.DAO
{
    public class UserLogins
    {
        public string ApplicationType { get; set; }

        public int CollegeId { get; set; }

        public string CollegeName { get; set; }

        public string ConnectionString { get; set; }

        public string ExcelReportURL { get; set; }

        public bool IsAuthenticated { get; set; }

        public string LastLogin { get; set; }

        public string LogFilePath { get; set; }

        public string Password { get; set; }

        public string PDFFolderPath { get; set; }

        public string RedirectUrl { get; set; }

        public string ReleaseFilePath { get; set; }

        public string RoleType { get; set; }

        public string UserName { get; set; }
    }
}
