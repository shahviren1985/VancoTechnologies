using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITM.DAO
{
    public class UserDetails
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string Password { get; set; }
        public DateTime LastLogin { get; set; }
        public bool IsActive { get; set; }
        public bool IsFaculty { get; set; }
        public int FacultyId { get; set; }
        public string History { get; set; }
    }
}
