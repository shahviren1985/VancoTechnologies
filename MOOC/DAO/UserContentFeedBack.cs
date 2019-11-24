using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITM.Courses.DAO
{
    public class UserContentFeedBack
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ChapterDetailsId { get; set; }
        public string Feedback { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
