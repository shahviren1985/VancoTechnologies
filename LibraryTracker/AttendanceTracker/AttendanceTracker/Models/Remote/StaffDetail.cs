﻿using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceTracker.Models.Remote
{
    [Table("staff_details")]
    public class StaffDetail
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("firstname")]
        public string FirstName { get; set; }

        [Column("lastname")]
        public string LastName { get; set; }

        [Column("employee_code")]
        public string EmployeeCode { get; set; }

        [Column("department")]
        public string Department { get; set; }
    }
}
