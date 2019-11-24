using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Common;
using System.Data;

using ITM.DAOBase;
using ITM.Utilities;
using ITM.LogManager;
using ITM.DAO;


namespace ITM.DAO
{
    public class InventoryBillsDAO
    {
        Logger logger = new Logger();

        public string INSERT_INVENTORY_BILLS = "INSERT INTO inventorybills (DepartmentId, InventorytId, BillNo, Vendor, ReceivedQuantity,  BillDate, LastUpdateDate,PurchasedPrice) " +
                                                                                 "VALUES ({0},{1},'{2}','{3}',{4},'{5}','{6}','{7}')";

        public string UPDATE_INVENTORY_STOCK = "UPDATE  inventorydetails set Quantity = Quantity + {0} WHERE ID = {1}";

        public string Q_INVENTORY_BILLS = "select * from inventorybills where DepartmentId='{0}' order by BillNo ";

        public string Q_INVENTORY_BILLS_All = "select * from inventorybills  order by BillNo ";

        #region Insert
        public void CreateInventoryBill(string vendorName, string billNo,int departmentId, int inventoryId, int receivedQuantity, DateTime receiveDate,string purchasedPrice, string cnxnString, string logPath)
        {
            try
            {
                string result = string.Format(INSERT_INVENTORY_BILLS,
                                                departmentId,
                                                inventoryId,
                                                ParameterFormater.FormatParameter(billNo) ,
                                                ParameterFormater.FormatParameter(vendorName),
                                                receivedQuantity,
                                                receiveDate.ToString("yyyy/MM/dd hh:mm:ss.fff"),
                                                DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff"),
                                                purchasedPrice);

                Database db = new Database();
                db.Insert(result, cnxnString, logPath);

                string updateResult = string.Format(UPDATE_INVENTORY_STOCK, receivedQuantity, inventoryId);
                db.Update(updateResult, cnxnString, logPath);
            }
            catch (Exception ex)
            {
                logger.Error("InventoryBillsDAO", "CreateInventory", ex.Message, ex, logPath);
                throw ex;
            }
        }
        #endregion


        public List<InventoryBills> GetInventoryBills(string deptId,string cnxnString, string logPath)
        {
            try
            {
                string result = string.Empty;
                if (!string.IsNullOrEmpty(deptId))
                {
                    result = string.Format(Q_INVENTORY_BILLS, deptId);
                }
                else
                    result = Q_INVENTORY_BILLS_All;

                Database db = new Database();
                DbDataReader reader = db.Select(result, cnxnString, logPath);

                List<InventoryBills> inventorylist = new List<InventoryBills>();

                if (reader.HasRows)
                {
                    List<DepartmentDetails> deptList = new DepartmentDetailsDAO().GetDepartmentList(cnxnString, logPath);
                    if (deptList == null) deptList = new List<DepartmentDetails>();

                    List<InventoryDetails> inventoryList = new InventoryDetailsDAO().GetInventoryList(cnxnString, logPath);
                    if (inventoryList == null) inventoryList = new List<InventoryDetails>();

                    while (reader.Read())
                    {
                        InventoryBills inventoryBilldetails = new InventoryBills();
                        inventoryBilldetails.Id = Convert.ToInt32(reader["Id"]);

                        inventoryBilldetails.DepartmentId = Convert.ToInt32(reader["DepartmentId"]);
                        int departmentid = Convert.ToInt32(reader["DepartmentId"]);
                        if (deptList.Find(dl => dl.DepartmentId == departmentid) != null)
                            inventoryBilldetails.DepartmentName = deptList.Find(dl => dl.DepartmentId == departmentid).DepartmentName;

                        int InventorytId = Convert.ToInt32(reader["InventorytId"]);
                        inventoryBilldetails.InventoryId = InventorytId;
                        if (inventoryList.Find(dl => dl.Id == InventorytId) != null)
                            inventoryBilldetails.InventoryName = inventoryList.Find(dl => dl.Id == InventorytId).Name;

                        inventoryBilldetails.BillNo = reader["BillNo"].ToString();
                        inventoryBilldetails.VendorName = reader["Vendor"].ToString();
                        inventoryBilldetails.ReceivedQuantity = Convert.ToInt32(reader["ReceivedQuantity"].ToString());
                        inventoryBilldetails.BillDate = Convert.ToDateTime(reader["BillDate"].ToString());
                        inventoryBilldetails.PurchasedPrice = reader["PurchasedPrice"].ToString();
                        inventorylist.Add(inventoryBilldetails);
                    }

                    if (reader != null)
                    {
                        reader.Close();
                    }
                }

                return inventorylist;
            }
            catch (Exception ex)
            {
                logger.Error("InventoryDetailsDAO", "GetInventoryList", ex.Message, ex, logPath);
                throw ex;
            }

        }

       
    }
}
