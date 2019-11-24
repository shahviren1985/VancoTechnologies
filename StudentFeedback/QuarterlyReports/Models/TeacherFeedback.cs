using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuarterlyReports.Models
{
    public class TeacherFeedback
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("userType")]
        public string UserType { get; set; }

        [JsonProperty("teacherCode")]
        public string TeacherCode { get; set; }

        [JsonProperty("subjectCode")]
        public string SubjectCode { get; set; }

        [JsonProperty("collegeCode")]
        public string CollegeCode { get; set; }

        [JsonProperty("a1")]
        public string A1 { get; set; }

        [JsonProperty("a2")]
        public string A2 { get; set; }

        [JsonProperty("a3")]
        public string A3 { get; set; }

        [JsonProperty("a4")]
        public string A4 { get; set; }

        [JsonProperty("a5")]
        public string A5 { get; set; }

        [JsonProperty("a6")]
        public string A6 { get; set; }

        [JsonProperty("a7")]
        public string A7 { get; set; }

        [JsonProperty("a8")]
        public string A8 { get; set; }

        [JsonProperty("a9")]
        public string A9 { get; set; }

        [JsonProperty("a10")]
        public string A10 { get; set; }

        [JsonProperty("a11")]
        public string A11 { get; set; }

        [JsonProperty("a12")]
        public string A12 { get; set; }

        [JsonProperty("a13")]
        public string A13 { get; set; }

        [JsonProperty("a14")]
        public string A14 { get; set; }

        [JsonProperty("a15")]
        public string A15 { get; set; }

        [JsonProperty("a16")]
        public string A16 { get; set; }

        [JsonProperty("a17")]
        public string A17 { get; set; }

        [JsonProperty("a18")]
        public string A18 { get; set; }

        [JsonProperty("a19")]
        public string A19 { get; set; }

        [JsonProperty("a20")]
        public string A20 { get; set; }

        [JsonProperty("a21")]
        public List<string> A21 { get; set; }

        [JsonIgnore]
        public string A_21 { get; set; }

        [JsonProperty("a22")]
        public List<string> A22 { get; set; }

        [JsonIgnore]
        public string A_22 { get; set; }

        [JsonProperty("a23")]
        public string A23 { get; set; }

        [JsonProperty("a24")]
        public string A24 { get; set; }

        [JsonProperty("a25")]
        public string A25 { get; set; }

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }
         
        [JsonProperty("ipAddress")]
        public string IPAddress { get; set; }
       [JsonProperty("fwdIPAddresses")]
        public string FwdIPAddresses { get; set; }
    }
    public class AcademicAdministrator
    {
        public string Id { get; set; }
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("userType")]
        public string UserType { get; set; }

        [JsonProperty("teacherCode")]
        public string TeacherCode { get; set; }

        [JsonProperty("subjectCode")]
        public string SubjectCode { get; set; }

        [JsonProperty("collegeCode")]
        public string CollegeCode { get; set; }

        [JsonProperty("a1")]
        public string A1 { get; set; }

        [JsonProperty("a2")]
        public string A2 { get; set; }

        [JsonProperty("a3")]
        public string A3 { get; set; }

        [JsonProperty("a4")]
        public string A4 { get; set; }

        [JsonProperty("a5")]
        public string A5 { get; set; }

        [JsonProperty("a6")]
        public string A6 { get; set; }

        [JsonProperty("a7")]
        public string A7 { get; set; }

        [JsonProperty("a8")]
        public string A8 { get; set; }

        [JsonProperty("a9")]
        public string A9 { get; set; }

        [JsonProperty("a10")]
        public string A10 { get; set; }

        [JsonProperty("a11")]
        public string A11 { get; set; }

        [JsonProperty("a12")]
        public string A12 { get; set; }

        [JsonProperty("a13")]
        public string A13 { get; set; }

        [JsonProperty("a14")]
        public string A14 { get; set; }

        [JsonProperty("a15")]
        public string A15 { get; set; }

        [JsonProperty("a16")]
        public string A16 { get; set; }

        [JsonProperty("a17")]
        public string A17 { get; set; }

        [JsonProperty("a18")]
        public string A18 { get; set; }

        [JsonProperty("a19")]
        public string A19 { get; set; }

        [JsonProperty("a20")]
        public string A20 { get; set; }

        [JsonProperty("a21")]
        public string A21 { get; set; }

        [JsonProperty("a22")]
        public string A22 { get; set; }

        [JsonProperty("a23")]
        public string A23 { get; set; }

        [JsonProperty("a24")]
        public string A24 { get; set; }

        [JsonProperty("a25")]
        public string A25 { get; set; }
        [JsonProperty("a26")]
        public string A26 { get; set; }
        [JsonProperty("a27")]
        public string A27 { get; set; }
        [JsonProperty("a28")]
        public string A28 { get; set; }
        [JsonProperty("a29")]
        public string A29 { get; set; }
        [JsonProperty("a30")]
        public string A30 { get; set; }
        [JsonProperty("a31")]
        public string A31 { get; set; }
        [JsonProperty("a32")]
        public string A32 { get; set; }
        [JsonProperty("a33")]
        public string A33 { get; set; }
        [JsonProperty("a34")]
        public string A34 { get; set; }
        [JsonProperty("a35")]
        public string A35 { get; set; }

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }
        
        [JsonProperty("ipAddress")]
        public string IPAddress { get; set; }
        
        [JsonProperty("fwdIPAddress")]
        public string FwdIPAddresses { get; set; }
    }
}