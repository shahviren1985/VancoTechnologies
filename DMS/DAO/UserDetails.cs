using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AA.DAO
{
    public class UserDetails
    {
        

        // Properties
        public DateTime DateCreated { get;  set; }
        public string Departments { get;  set; }
        public string Email { get;  set; }
        public string FirstName { get;  set; }
        public int Id { get;  set; }
        public bool IsActive { get;  set; }
        public DateTime LastLogin { get;  set; }
        public string LastName { get;  set; }
        public string MobileNo { get;  set; }
        public string Password { get;  set; }
        public int RoleId { get;  set; }
        public string RoleName { get;  set; }
        public string UserName { get;  set; }
        public string VerificationId { get;  set; }
    }


}
