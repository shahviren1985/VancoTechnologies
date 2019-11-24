using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeExamService.Models
{
    public class PaperDetail
    {
        [JsonProperty("paperTitle")]
        public string PaperTitle { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("isElective")]
        public string IsElective { get; set; }

        [JsonProperty("order")]
        public string Order { get; set; }
        [JsonProperty("paperCode")]
        public string paperCode { get; set; }
        [JsonProperty("isGrade")]
        public string isGrade { get; set; }
        [JsonProperty("paperType")]
        public string paperType { get; set; }
        [JsonProperty("theoryInternalMax")]
        public string theoryInternalMax { get; set; }
        [JsonProperty("theoryInternalPassing")]
        public string theoryInternalPassing { get; set; }
        [JsonProperty("theoryExternalSection1Max")]
        public string theoryExternalSection1Max { get; set; }
        [JsonProperty("theoryExternalSection2Max")]
        public string theoryExternalSection2Max { get; set; }
        [JsonProperty("theoryExternalPassing")]
        public string theoryExternalPassing { get; set; }
        [JsonProperty("practicalMaxMarks")]
        public string practicalMaxMarks { get; set; }
        [JsonProperty("credits")]
        public string credits { get; set; }
        [JsonProperty("maxGrace")]
        public string maxGrace { get; set; }
    }

    public class CourseDetails
    {
        [JsonProperty("courseName")]
        public string CourseName { get; set; }

        [JsonProperty("courseShortName")]
        public string CourseShortName { get; set; }

        [JsonProperty("marksheetName")]
        public string MarksheetName { get; set; }

        [JsonProperty("specialisation")]
        public string Specialisation { get; set; }

        [JsonProperty("specialisationCode")]
        public string SpecialisationCode { get; set; }

        [JsonProperty("paperDetails")]
        public List<PaperDetail> PaperDetails { get; set; }
        [JsonProperty("isContinousAssessment")]
        public string isContinousAssessment { get; set; }
    }
}