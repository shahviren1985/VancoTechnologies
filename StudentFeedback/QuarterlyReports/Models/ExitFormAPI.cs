using QuarterlyReports.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuarterlyReports.Models
{
    public class ExitFormAPI
    {
    }
   

    public class ExitFormObject
    {
        public List<AluminiInnerQuestion> exitform { get; set; }
        public List<AluminiInnerQuestion> a2 { get; set; }
        public List<AluminiInnerQuestion> a9 { get; set; }
    }
}