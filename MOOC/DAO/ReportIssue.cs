using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITM.Courses.DAO
{
    [Serializable]
    public class ReportIssue
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string  Title { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string ExpectedContent { get; set; }
        public string UserName { get; set; }

        public int ChapterId { get; set; }
        public string ChapterName { get; set; }
        public int SectionId { get; set; }
        public string SectionName { get; set; }
        
        public DateTime DatePosted { get; set; }        
    }
}
