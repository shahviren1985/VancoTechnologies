using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuarterlyReports.Models
{
    public class User : BaseClass
    {
        [JsonProperty("userId")]
        public long UserId { get; set; }

        [JsonProperty("firsName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("mobileNumber")]
        public string MobileNumber { get; set; }

        [JsonProperty("userType")]
        public string UserType { get; set; }

        [JsonProperty("collegeId")]
        public string CollegeId { get; set; }

        [JsonProperty("course")]
        public string Course { get; set; }

        [JsonProperty("collegeCode")]
        public string CollegeCode { get; set; }

        [JsonProperty("subCourse")]
        public string SubCourse { get; set; }

        [JsonProperty("isActive")]
        public string IsActive { get; set; }

        [JsonProperty("isFinalYearStudent")]
        public string IsFinalYearStudent { get; set; }

        [JsonProperty("currentSemester")]
        public string CurrentSemester { get; set; }

        [JsonProperty("acedemicYear")]
        public string AcedemicYear { get; set; }

        [JsonProperty("submissionId")]
        public long SubmissionId { get; set; }

        [JsonProperty("teacherCodes")]
        public List<string> TeacherCodes { get; set; }

        [JsonProperty("subjectCodes")]
        public List<string> SubjectCodes { get; set; }

        [JsonProperty("teacherNames")]
        public List<string> TeacherNames { get; set; }

        [JsonProperty("subjectNames")]
        public List<string> SubjectNames { get; set; }

        public User()
        {
            TeacherCodes = new List<string>();
            SubjectCodes = new List<string>();
            TeacherNames = new List<string>();
            SubjectNames = new List<string>();

        }

        public static User FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            User response = new User();
            if (string.IsNullOrEmpty(values[0]))
                response.UserId = 0;
            else
                response.UserId = int.Parse(values[0]);
            response.FirstName = values[1];
            response.LastName = values[2];
            response.MobileNumber = values[3];
            string value4 = values[4];
            if (string.IsNullOrEmpty(value4))
                response.UserType = "Customer";
            else
                response.UserType = value4;
            response.CollegeId = values[5];
            return response;
        }

    }
}