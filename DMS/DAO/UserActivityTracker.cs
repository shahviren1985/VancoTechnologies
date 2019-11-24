using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AA.DAO
{
    public class UserActivityTracker
    {
        public int Id { get; set; }
        public string Activity { get; set; }
        public string ActivityOn { get; set; }
        public int ObjectId { get; set; }
        public DateTime ActivityDate { get; set; }
        public string UserId { get; set; }
        public int SendedReminder { get; set; }
    }
}
