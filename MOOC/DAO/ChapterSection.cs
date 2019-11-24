using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITM.Courses.DAO
{
    public class ChapterSection
    {
        public int Id { get; set; }
        public int ChapterId { get; set; }
        public string Title { get; set; }
        public string PageName { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public int Time { get; set; }
        public bool IsDB { get; set; }
        public string PageContent { get; set; }
        public DateTime DateCreated { get; set; }

        public int MigratedSectionId { get; set; }
        public string OrignalName { get; set; }
    }
}
