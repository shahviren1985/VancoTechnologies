using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITM.Courses.ConfigurationsManager
{
    public class MarksheetConfig
    {
        public bool IsHeaderRequired { get; set; }
        public string LogoPath { get; set; }
        public int LogoWidth { get; set; }
        public int LogoHeight { get; set; }
        public string CollegeName { get; set; }
        public string TagLine { get; set; }
        public string Address { get; set; }
        public int PostHeader { get; set; }
        public int PostMarkSheet { get; set; }
        public int LineBreakHeight { get; set; }
        public int PrintMarkSheetsPerPage { get; set; }
        public bool DisplayURoNumber { get; set; }
        public int ResultYPosition { get; set; }
        public int ATKTYPosition { get; set; }
        public int ATKTCounterYPosition { get; set; }
        public int DateYPosition { get; set; }
        public int AllowedAtkt { get; set; }
    }
}
