using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITM.DAO
{
    public class InventoryBills
    {
        public int Id { get; set; }
        public int InventoryId { get; set; }
        public int DepartmentId { get; set; }
        public string BillNo { get; set; }
        public string DepartmentName { get; set; }
        public string InventoryName { get; set; }
        public string VendorName { get; set; }
        public int ReceivedQuantity { get; set; }
        public int ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public DateTime BillDate { get; set; }
        public string PurchasedPrice { get; set; }
    }
}
