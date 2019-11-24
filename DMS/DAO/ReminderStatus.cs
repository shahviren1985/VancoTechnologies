using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class ReminderStatus
    {
        public int Id { get; set; }
        public int DocumentId { get; set; }
        public string ReminderSentTo { get; set; }
        public string EmailAddresses { get; set; }
        public string DateSent { get; set; }
    }
}
