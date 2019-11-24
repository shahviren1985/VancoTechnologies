using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITM.Courses.DAO
{
    [Serializable]
    public class UserDetails
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastLogin { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string EmailAddress { get; set; }
        public string UserType { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public bool IsEnable { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsCertified { get; set; }
        public string VersionRegister { get; set; }
        public string MobileNo { get; set; }
        public string RollNumber { get; set; }
        public bool IsAuthenticated { get; set; }
        public decimal Percentage { get; set; }
        public bool  IsNewUser { get; set; }
        public string LastActivityDate { get; set; }
        public int BatchYear { get; set; }
        public string Course { get; set; }
        public string SubCourse { get; set; }
        public string StudentFullName { get; set; }
    }
}
