using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuarterlyReports.Models
{
    public class SubjectTeacherMapper
    {
        [JsonProperty("SubjectCode")]
        public string SubjectCode { get; set; }

        [JsonProperty("TeacherName")]
        public string TeacherName { get; set; }
    }

    public class ExcelTableMapper
    {
        [JsonProperty("course")]
        public string CourseName { get; set; }
        [JsonProperty("excel")]
        public string ExcelColumn { get; set; }

        [JsonProperty("table")]
        public string TableName { get; set; }
        [JsonProperty("column")]
        public string DatabaseColumnName { get; set; }
    }

    public class StaffExcelColumnMapper
    {
        [JsonProperty("Type1")]
        public List<string> PeerOwnDepartment { get; set; }
        [JsonProperty("Type2")]
        public List<string> PeerOtherDepartment { get; set; }

        [JsonProperty("Type3")]
        public List<string> PeerAnyDepartment { get; set; }
        [JsonProperty("Type4")]
        public List<string> Library { get; set; }
        [JsonProperty("Type5")]
        public List<string> CurriculumEvaluation { get; set; }
        [JsonProperty("Type6")]
        public List<string> JobSatisfactionEmployeeRelation { get; set; }
        [JsonProperty("Type7")]
        public List<string> JobSatisfactionWorkPlace { get; set; }
        [JsonProperty("Type8")]
        public List<string> JobSatisfactionTechnologyResource { get; set; }
        [JsonProperty("Type9")]
        public List<string> AcademicAdmin { get; set; }
        [JsonProperty("Type10")]
        public List<string> PeerReviewOwnDepartmentNames { get; set; }
        [JsonProperty("Type11")]
        public List<string> PeerReviewOtherDepartmentNames { get; set; }
        [JsonProperty("Type12")]
        public List<string> PeerReviewAnyDepartmentNames { get; set; }
    }
}