using Newtonsoft.Json;

namespace ExcelDataExtraction.Model
{
    public class ResultSummary : Entity
    {
        [JsonProperty("Semester")]
        public int Semester { get; set; }
        [JsonProperty("PRN")]
        public virtual string PRN { get; set; }
        [JsonProperty("Total Credits")]
        public double TotalCredits { get; set; }
        [JsonProperty("Total EGP")]
        public double TotalEGP { get; set; }
        [JsonProperty("SGPA")]
        public double SGPA { get; set; }
        [JsonProperty("Grade")]
        public string Grade { get; set; }
        [JsonProperty("Grand Total Obtained")]
        public int GrandTotalObtained { get; set; }
        [JsonProperty("Grand Total Marks")]
        public int GrandTotalMarks { get; set; }
        [JsonProperty("Percentage")]
        public double Percentage { get; set; }
    }
}
