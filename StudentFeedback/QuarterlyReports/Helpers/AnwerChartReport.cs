using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuarterlyReports.Helpers
{
    public class AnwerChartReport
    {
        public string Title { get; set; }

        public Dictionary<string, int> DicAnwers { get; set; }
    }
}