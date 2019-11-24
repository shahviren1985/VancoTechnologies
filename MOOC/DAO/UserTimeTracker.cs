using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITM.Courses.DAO
{
    public class UserTimeTracker
    {
        public int Id { get; set; }
        public int UserId  { get; set; }
        public int ChapterId { get; set; }
        public int SectionId { get; set; }
        public int TimeSpent { get; set; }
    }
}
