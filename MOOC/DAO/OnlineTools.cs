using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITM.Courses.DAO
{
    public class OnlineTools
    {
        public int Id { get; set; }
        public int RelatedCourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string LogoImageURL { get; set; }
        public string LogoImageName { get; set; }
        public string ToolURL { get; set; }
        public DateTime ToolDisplayDate { get; set; }
        public string ToolDisplayDateNormal { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
