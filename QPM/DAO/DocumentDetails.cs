using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITM.DAO
{
    public class DocumentDetails
    {
        public string UserId { get; set; }
        public int Id { get; set; }
        public string Course { get; set; }
        public string SubCourse { get; set; }
        public string Subject { get; set; }
        public string Paper { get; set; }
        public string DocumentFirst { get; set; }
        public string DocumentSecond { get; set; }
        public string IsDeleteRequested { get; set; }
        public string IsUsed { get; set; }
        public string Date { get; set; }
        public string UsedStatus{ get; set; }

    }
}
