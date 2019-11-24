using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuarterlyReports.Models
{
    public class TeacherDetails
    {

        [JsonProperty("teacherCode")]
        public string TeacherCode { get; set; }

        [JsonProperty("teacherName")]
        public string TeacherName { get; set; }

        [JsonProperty("subjectCode")]
        public string SubjectCode { get; set; }

        [JsonProperty("subjectName")]
        public string SubjectName { get; set; }

        [JsonProperty("course")]
        public string Course { get; set; }

        [JsonProperty("subCourse")]
        public string SubCourse { get; set; }

        [JsonProperty("semester")]
        public string Semester { get; set; }
    }
}