using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITM.Courses.DAO
{
    public class ApplicationLogoHeader
    {
        public int Id { get; set; }
        public string CollegeName { get; set; }
        public string LogoImageName { get; set; }
        public string LogoImagePath { get; set; }
        public string LogoText { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
