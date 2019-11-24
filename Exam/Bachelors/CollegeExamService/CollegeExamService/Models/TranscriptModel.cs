using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeExamService.Models
{
    public class TranscriptModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PassingYear { get; set; }
        public string CollegeRegNo { get; set; }
        public DateTime DateOfRecieved { get; set; }
        public int Status { get; set; }
    }
}