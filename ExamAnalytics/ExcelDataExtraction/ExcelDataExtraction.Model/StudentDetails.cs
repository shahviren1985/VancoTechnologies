using Newtonsoft.Json;
using System;

namespace ExcelDataExtraction.Model
{
    [Serializable]
    public class StudentDetails : Entity
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Seat No")]
        public string SeatNumber { get; set; }
        [JsonProperty("Center")]
        public string Center { get; set; }
        [JsonProperty("PRN")]
        public string PRN { get; set; }
        [JsonProperty("Medium")]
        public string Medium { get; set; }
        [JsonProperty("College Code")]
        public string CollegeCode { get; set; }
        [JsonProperty("College Name")]
        public string CollegeName { get; set; }
        [JsonProperty("Course Name")]
        public string CourseName { get; set; }
        [JsonProperty("Semester")]
        public int Semester { get; set; }
        [JsonProperty("Month")]
        public string ExamMonth { get; set; }
        [JsonProperty("Year")]
        public int ExamYear { get; set; }
        [JsonProperty("Result Date")]
        public DateTime ResultDate { get; set; }
        [JsonProperty("Exam Type")]
        public string ExamType { get; set; }

        public DateTime DateCreated { get; set; }
        protected StudentDetails()
        {
            DateCreated = DateTime.Now;
        }
    }
}
