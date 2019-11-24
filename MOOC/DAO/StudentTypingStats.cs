using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ITM.Courses.DAO
{
    [Serializable]
    public class StudentTypingStats
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Level { get; set; }
        public int TimeSpanInSeconds { get; set; }
        public string TimeSpanInNormal { get; set; }
        public int Accuracy { get; set; }
        public int GrossWPM { get; set; }
        public int NetWPM { get; set; }
        public DateTime DateCreated { get; set; }

        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public string Course { get; set; }
        public string MotherName { get; set; }
    }
}
