using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITM.DAO
{
    public class InventoryIssues
    {
        public int Id { get; set; }
        public int InventoryId { get; set; }
        public int DepartmentId { get; set; }
        public int TeacherId { get; set; }
        public string DepartmentName { get; set; }
        public string TeacherName { get; set; }
        public string InventoryName { get; set; }
        public int IssueQuantity { get; set; }
        public int IssuerId { get; set; }
        public string IssuerName { get; set; }
        public DateTime IssueDate { get; set; }
    }
}
