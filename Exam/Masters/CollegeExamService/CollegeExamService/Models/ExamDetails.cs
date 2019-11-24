using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeExamService.Models
{
    public class ExamDetails
    {
        [JsonProperty("paperTitle")]
        public string PaperTitle { get; set; }

        [JsonProperty("paperCode")]
        public string PaperCode { get; set; }

        [JsonProperty("isGrade")]
        public string IsGrade { get; set; }

        [JsonProperty("paperType")]
        public string PaperType { get; set; }

        [JsonProperty("theoryInternalMax")]
        public string TheoryInternalMax { get; set; }

        [JsonProperty("theoryInternalPassing")]
        public string TheoryInternalPassing { get; set; }

        [JsonProperty("theoryExternalSection1Max")]
        public string TheoryExternalSection1Max { get; set; }

        [JsonProperty("theoryExternalSection2Max")]
        public string TheoryExternalSection2Max { get; set; }

        [JsonProperty("theoryExternalPassing")]
        public string TheoryExternalPassing { get; set; }

        [JsonProperty("practicalMaxMarks")]
        public string PracticalMaxMarks { get; set; }

        [JsonProperty("credits")]
        public string Credits { get; set; }

        [JsonProperty("maxGrace")]
        public string MaxGrace { get; set; }
    }
}