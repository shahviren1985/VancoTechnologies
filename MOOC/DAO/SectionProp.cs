using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITM.Courses.DAO;
using ITM.Courses.DAOBase;
using ITM.Courses.LogManager;
using ITM.Courses.Utilities;
using System.Configuration;


namespace ITM.Courses.DAO
{
    public class SectionProp
    {
        public string SectionName { get; set; }        
        public string SectionFileName { get; set; }
        public string FilePath { get; set; }
        public int Id { get; set; }
    }
}
