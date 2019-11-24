using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AA.DAO
{
    public class GroupsDetails
    {
        public int Id { get; set; }
        public string  GroupName { get; set; }
        public string OwnerUserId { get; set; }
        public DateTime DateCreated { get; set; }
        public string Users { get; set; }
    }
}
