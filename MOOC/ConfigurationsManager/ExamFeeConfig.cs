using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITM.Courses.ConfigurationsManager
{
    public class ExamFeeConfig
    {
        public bool IsHeaderRequired { get; set; }
        public string LogoPath { get; set; }
        public string LogoWidth { get; set; }
        public string LogoHeight { get; set; }
        public string CollegeName { get; set; }
        public string TagLine { get; set; }
        public string Address { get; set; }
        public string ReceiptTitle { get; set; }
        public List<string> ReceiptContent { get; set; }
    }
}
