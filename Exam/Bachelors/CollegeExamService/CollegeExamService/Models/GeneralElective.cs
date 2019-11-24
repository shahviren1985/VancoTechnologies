using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeExamService.Models
{
    public class GeneralElective
    {
        public string Specialization { get; set; }
        public string specialisationCode { get; set; }
        public List<ElectiveSubject> ElectiveSubject { get; set; }
    }

    public class Subject
    {
        public string Title { get; set; }
        public string Code { get; set; }
    }

    public class ElectiveSubject
    {
        public string Title { get; set; }
        public List<Subject> Subject { get; set; }
    }
}