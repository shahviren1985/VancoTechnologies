using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuarterlyReports.Models
{
    public class FeedbackQuestions
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("question")]
        public string Question { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("optionValues")]
        public IList<string> OptionValues { get; set; }

        [JsonProperty("placeHolder")]
        public string PlaceHolder { get; set; }

    }
    public class ReportCard
    {
        public int TSMaxScore { get; set; }
        public int SKMaxScore { get; set; }
        public int PIMaxScore { get; set; }

        public double TSScoreObtain { get; set; }
        public double SKScoreObtain { get; set; }
        public double PIScoreObtain { get; set; }

        public double TSPerformance { get; set; }
        public double SKPerformance { get; set; }
        public double PIPerformance { get; set; }

        public string TeacherCode { get; set; }
        public string TeacherName { get; set; }

        public string AcedemicYear { get; set; }
        public double GrandMean { get; set; }
        public double GrandPercentage { get; set; }
        public double GrandMaxScore { get; set; }

    }
}