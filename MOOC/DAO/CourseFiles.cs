using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITM.Courses.DAO
{
    public class CourseFiles
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Caption { get; set; }
        public string ContentType { get; set; }
        public int ContentSize { get; set; }
        public string Thumbnail { get; set; }
        public string AttachLink { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
    }
}
