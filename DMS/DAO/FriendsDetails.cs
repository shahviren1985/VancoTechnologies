using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AA.DAO
{
    public class FriendsDetails
    {
        public int Id { get; set; }
        public string OwnerUserId { get; set; }
        public DateTime DateCreated { get; set; }
        public string Users { get; set; }
        public List<FriendUsers> UserList { get; set; }
    }

    public class FriendUsers
    {
        public string UserId { get; set; }
    }
}
