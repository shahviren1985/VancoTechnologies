using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITM.Courses.DAO
{
    public class StudentQueriesResponse
    {
        public int Id { get; set; }
        public int QueryId { get; set; }
        public int ResponsedBy { get; set; }
        public DateTime ResponsedDate { get; set; }
        public string Comments { get; set; }
    }
}
