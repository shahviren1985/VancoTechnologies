using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITM.Courses.DAO
{
    public class ChapterDetails
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }    
        public string PageName { get; set; }
        public string Link { get; set; }
        public int Time { get; set; }
        public bool IsDB { get; set; }        
        public DateTime DateCreated { get; set; }
        public string Language { get; set; }
        public string ContentVersion { get; set; }
        public string CourseName { get; set; }

        public int MigratedChapterId { get; set; }
        public string OrignalName { get; set; }
    }
}
