using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITM.Courses.DAO
{
    [Serializable]
    public class StudentQueries
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CollegeName { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Query { get; set; }
        public int QueryStatus { get; set; }
        public DateTime DatePosted { get; set; }
        public string UserName { get; set; }
        public string DisplayDate { get; set; }
        public string Email { get; set; }
    }
}
