using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITM.Courses.DAO
{
    public class UserActivityTraker
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Activity { get; set; }
        public DateTime DateCreated { get; set; }

        public UserActivityObject UserActivity { get; set; }
    }

    public class UserActivityObject
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string ActivityType { get; set; }
        public string Datetime { get; set; }
    }
}
