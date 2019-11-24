using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITM.DAO
{
    public class InventoryDetails
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Specification { get; set; }
        public int Quantity { get; set; }
        public int QuantityRecommended { get; set; }
        public string Manufacturer { get; set; }
        public string Vendor { get; set; }
        public string Price { get; set; }
        public string ModelNo { get; set; }
        public string Id_Quantity { get { return Id + "|" + Quantity; } }
        public string Location { get; set; }
        public string Remarks { get; set; }
    }
}
