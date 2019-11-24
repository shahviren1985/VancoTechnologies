using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeExamService.Models
{

    public class ConvocationList
    {
        public int StudentId { get; set; }
        public string Crn { get; set; }
        public string StudentName { get; set; }
        public float TotalMarks { get; set; }
        public float TotalCredits { get; set; }
        public float WeightedPercentage { get; set; }
        public string Grade { get; set; }
        public string Specialisation { get; set; }
    }

    public class PaperModel
    {
        public PaperModel()
        {
            IsDuplicate = false;
        }
        [JsonProperty("StudentId")]
        public string StudentId { get; set; }

        [JsonProperty("ExamId")]
        public string ExamId { get; set; }

        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Credit")]
        public string Credit { get; set; }

        [JsonProperty("PaperCode")]
        public string PaperCode { get; set; }

        [JsonProperty("PaperTitle")]
        public string PaperTitle { get; set; }

        [JsonProperty("PaperType")]
        public string PaperType { get; set; }

        [JsonProperty("InternalPassingMarks")]
        public string InternalPassingMarks { get; set; }

        [JsonProperty("InternalTotalMarks")]
        public string InternalTotalMarks { get; set; }

        [JsonProperty("InternalMarksObtained")]
        public string InternalMarksObtained { get; set; }

        [JsonProperty("ExternalPassingMarks")]
        public string ExternalPassingMarks { get; set; }

        [JsonProperty("ExternalSection1Marks")]
        public string ExternalSection1Marks { get; set; }

        [JsonProperty("ExternalSection2Marks")]
        public string ExternalSection2Marks { get; set; }

        [JsonProperty("ExternalTotalMarks")]
        public string ExternalTotalMarks { get; set; }

        [JsonProperty("PracticalMarksObtained")]
        public string PracticalMarksObtained { get; set; }

        [JsonProperty("PracticalMaxMarks")]
        public string PracticalMaxMarks { get; set; }

        [JsonProperty("externalMaxMarks")]
        public string ExternalMaxMarks { get; set; }

        [JsonProperty("GraceMarks")]
        public string GraceMarks { get; set; }

        [JsonProperty("PaperResult")]
        public string PaperResult { get; set; }

        [JsonProperty("GP")]
        public string GP { get; set; }

        [JsonProperty("Grade")]
        public string Grade { get; set; }

        [JsonProperty("Attempt")]
        public string Attempt { get; set; }

        [JsonProperty("Remarks")]
        public string Remarks { get; set; }

        [JsonProperty("Semester")]
        public int Semester { get; set; }
        [JsonProperty("RetryCount")]
        public int RetryCount { get; set; }
        [JsonProperty("yearOfExam")]
        public int yearOfExam { get; set; }
        [JsonProperty("semester")]
        public string semester { get; set; }
        [JsonProperty("monthOfExam")]
        public string monthOfExam { get; set; }
        [JsonProperty("ResultStatus")]
        public string ResultStatus { get; set; }
        [JsonProperty("IsDuplicate")]
        public bool IsDuplicate { get; set; }
    }

    public class CommonMarksheetResponse
    {

        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("PRN")]
        public string PRN { get; set; }

        [JsonProperty("CollegeRegistrationNumber")]
        public string CollegeRegistrationNumber { get; set; }

        [JsonProperty("AdmissionYear")]
        public string AdmissionYear { get; set; }

        [JsonProperty("StudentFullName")]
        public string StudentFullName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("FatherName")]
        public string FatherName { get; set; }

        [JsonProperty("MotherName")]
        public string MotherName { get; set; }

        [JsonProperty("Stream")]
        public string Stream { get; set; }

        [JsonProperty("Course")]
        public string Course { get; set; }

        [JsonProperty("Specialisation")]
        public string Specialisation { get; set; }

        [JsonProperty("Papers")]
        public List<PaperModel> Papers { get; set; }
    }
}