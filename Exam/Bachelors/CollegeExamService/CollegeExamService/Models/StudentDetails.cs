using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeExamService.Models
{
    public class StudentDetails
    {
        public string Id { get; set; }
        public string Crn { get; set; }
        public string RollNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }

        public string Course { get; set; }
        public int CourseId { get; set; }
        public string Specialisation { get; set; }
        public int SpecialisationId { get; set; }

        public string PRN { get; set; }
        public string StudentPhotoPath { get; set; }
       
        public string GeneralElective1 { get; set; }
        public string GeneralElective2 { get; set; }
        public string GeneralElective3 { get; set; }
        public string GeneralElective4 { get; set; }

        public static StudentDetails FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            StudentDetails response = new StudentDetails();
            response.RollNumber = values[0];
            response.FirstName = values[1];
            response.LastName = values[2];
            response.FatherName = values[3];
            response.MotherName = values[4];
            response.Course = values[5];
            response.CourseId = values[6]==null ? 0 : Convert.ToInt32(values[6]);
            response.Specialisation = values[7];
            response.SpecialisationId = values[8] == null ? 0 : Convert.ToInt32(values[8]);
            response.PRN = values[9];
            response.StudentPhotoPath = values[10];
            response.GeneralElective1 = values[11];
            response.GeneralElective2 = values[12];
            response.GeneralElective3 = values[13];
            response.GeneralElective4 = values[14];

            return response;
        }

    }
}