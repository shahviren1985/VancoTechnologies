using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuarterlyReports.Models
{
    public class FeedbackStatus 
    {
        public string TeacherCode { get; set; }

        public string SubjectCode { get; set; }
    }

     public class LoginResponse
    {
         public LoginResponse()
         {
             IsEmployeeFormSubmitted = "false";
             IsParentFormSubmitted = "false";
             IsAluminiFormSubmitted = "false";
             IsTeacherFormSubmitted = "false";
         }

        public bool Success { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("mobileNumber")]
        public string MobileNumber { get; set; }

        [JsonProperty("collegeCode")]
        public string CollegeCode { get; set; }

        [JsonProperty("course")]
        public string Course { get; set; }

        [JsonProperty("subCourse")]
        public string SubCourse { get; set; }

        [JsonProperty("isActive")]
        public string IsActive { get; set; }

        [JsonProperty("isFinalYearStudent")]
        public string IsFinalYearStudent { get; set; }

        [JsonProperty("currentSemester")]
        public string CurrentSemester { get; set; }

        [JsonProperty("feedbackStatus")]
        public List<FeedbackStatus> FeedbackStatus { get; set; }

        [JsonProperty("roleType")]
        public string RoleType { get; set; }

        [JsonProperty("acedemicYear")]
        public string AcedemicYear { get; set; }

        [JsonProperty("submissionId")]
        public long SubmissionId { get; set; }

        [JsonProperty("isExistFormSubmitted")]
        public string IsExistFormSubmitted { get; set; }

        [JsonProperty("isEmployeeFormSubmitted")]
        public string IsEmployeeFormSubmitted { get; set; }

        [JsonProperty("isParentFormSubmitted")]
        public string IsParentFormSubmitted { get; set; }

        [JsonProperty("isAluminiFormSubmitted")]
        public string IsAluminiFormSubmitted { get; set; }

        [JsonProperty("isTeacherFormSubmitted")]
        public string IsTeacherFormSubmitted { get; set; }

        [JsonProperty("dateCreated")]
        public DateTime DateCreated { get; set; }

        public static LoginResponse FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            LoginResponse response = new LoginResponse();
            response.UserId = values[0];
            response.FirstName = values[1];
            response.LastName = values[2];
            response.MobileNumber = values[3];
            response.CollegeCode = values[4];
            response.Course = values[5];
            response.SubCourse = values[6];
            response.IsActive = values[7] == null ? "false" : values[7].ToLower();
            response.IsFinalYearStudent = values[8] == null ? "false" : values[7].ToLower();
            response.CurrentSemester = values[9];
            
            response.RoleType = values[10] == null ? "Customer" : values[10];
            response.AcedemicYear = values[11];
            return response;
        }
    }
}