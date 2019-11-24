using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AA.DAO
{
    public class DocumentReadStatus
    {
        public int Id { get; set; }
        public int DocumentId { get; set; }
        public string DateRead { get; set; }
        public string ReadBy { get; set; }
    }
}
