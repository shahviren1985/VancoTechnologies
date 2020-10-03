using ExcelDataExtraction.Model;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ExcelDataExtraction.Services
{
    public static class StudentDataSerializer
    {
        /// <summary>
        /// Combine common data about student with each course name 
        /// </summary>
        /// <param name="studentData">Common data about student</param>
        /// <param name="programNames">List of program names</param>
        /// <param name="semester">Number of semester</param>
        /// <returns>The list then can be added to table</returns>
        public static List<StudentDetails> SerializeStudentDetailsData(Dictionary<string, string> studentData, List<string> programNames, int semester)
        {
            List<StudentDetails> studentDetails = new List<StudentDetails>();
            studentData.Add("Semester", semester.ToString());
            foreach (var program in programNames)
            {
                studentData.Add("Course Name", program);
                var jsonString = JsonConvert.SerializeObject(studentData);
                studentDetails.Add(JsonConvert.DeserializeObject<StudentDetails>(jsonString));
                studentData.Remove("Course Name");
            }
            studentData.Remove("Semester");
            return studentDetails;
        }
    }
}
