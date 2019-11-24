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
using System.Configuration;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;


namespace ITM.DAO
{
    public class CurrentStock
    {
        public CurrentStock()
        {
            listOfStock = new List<Stock>();
        }

        public string Name { get; set; }

        public List<Stock> listOfStock { get; set; }

    }
    public class Stock
    {
        public int InventoryId { get; set; }

        public int DepartmentId { get; set; }

        public int Quantity { get; set; }
    }



    public class InventoryDetailsDAO
    {
        Logger logger = new Logger();

        public string SELECT_INVENTORY_DETAILS = "SELECT Id, DepartmentId, Category, Name, Specification, Quantity, QuantityRecommended, Manufacturer, Vendor,Price,ModelNo,Remarks,LocationDetails FROM inventorydetails";
        public string INSERT_INVENTORY_DETAILS = "INSERT INTO inventorydetails (DepartmentId, Category, Name, Specification, Quantity, QuantityRecommended, Manufacturer, Vendor, Price, ModelNo, Remarks, LocationDetails, LastUpdateDate) " +
                                                                                 "VALUES ({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}', '{10}', '{11}', '{12}')";

        public string UPDATE_INVENTORY_DETAILS = "UPDATE  inventorydetails set DepartmentId={1}, Name = '{2}',Specification = '{3}',Quantity='{4}',Vendor='{5}',Price={6}, ModelNo='{7}',Remarks='{8}', Category='{9}', QuantityRecommended='{10}', Manufacturer='{11}', LocationDetails='{12}', LastUpdateDate='{12}'" +
                                                                                 "WHERE Id ={0}";
        public string DELETE_INVENTORY_DETAILS = "DELETE FROM inventorydetails WHERE Id ={0}";
        public string GET_INVENTORY_DETAILS_BY_ID = "SELECT Id, DepartmentId, Category, Name,Specification,Quantity, QuantityRecommended, Manufacturer, Vendor,Price,ModelNo,Remarks,LocationDetails FROM inventorydetails WHERE Id ={0}";
        public string Q_GetInventoryByCategory = "Select * from inventorydetails where DepartmentId='{0}' and Category='{1}'";
        public string Q_GetInventoryByDepartment = "Select * from inventorydetails where DepartmentId='{0}'";
        public string Q_GetInventoryByCategory_All = "Select Name,Manufacturer,Specification,sum(Quantity) Quantity,sum(price) Price,id,departmentid,vendor,remarks,QuantityRecommended,LocationDetails,ModelNo,Category from inventorydetails where Category='{0}' group by name";

        public string Q_GetInvetoryForPurchase = "SELECT Id, Category, DepartmentId, Name, Specification, Manufacturer, ROUND(QuantityRecommended - Quantity) as Quantity, ROUND((Price / Quantity),2) as PricePerUnit,"
                                                     + " ROUND(((QuantityRecommended - Quantity) * (Price / Quantity)),2) as Total FROM inventorydetails where DepartmentId='{0}' and Category='{1}'";

        public string Q_GetInvetoryForPurchase_All = "SELECT Id, Category, DepartmentId, Name, Specification, Manufacturer, ROUND(QuantityRecommended - Quantity) as Quantity, ROUND((Price / Quantity),2) as PricePerUnit,"
                                                    + " ROUND(((QuantityRecommended - Quantity) * (Price / Quantity)),2) as Total FROM inventorydetails where Category='{0}'";

        public string Q_CURRENTSTOCK_ITEM_WISE = "select d.id,d.Name,d.Vendor,b.BillNo,b.BillDate,b.DepartmentId,"
                 + "b.ReceivedQuantity  from inventorydetails d inner join inventorybills b on d.id=b.InventorytId "
                 + " where BillDate>='{0}' and BillDate<='{1}' order by d.Name,b.BillDate;";

        public string getIssueDetails = "select Id,InventorytId,DepartmentId,IssueQuantity from inventoryissues "
            + " where InventorytId in ({0}) ";



        #region Insert
        public InventoryDetails CreateInventory(int departmentId, string category, string name, string specification, int quantity, int recQuantity, string manufacturer, string vendor, int price, string modelNo, string location, string remarks, string cnxnString, string logPath)
        {
            try
            {
                string result = string.Format(INSERT_INVENTORY_DETAILS,
                                                departmentId,
                                                category,
                                                ParameterFormater.FormatParameter(name),
                                                ParameterFormater.FormatParameter(specification),
                                                quantity,
                                                recQuantity,
                                                manufacturer,
                                                ParameterFormater.FormatParameter(vendor),
                                                price,
                                                ParameterFormater.FormatParameter(modelNo),
                                                ParameterFormater.FormatParameter(remarks),
                                                location,
                                                DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff"));

                Database db = new Database();
                db.Insert(result, cnxnString, logPath);

                InventoryDetails inventory = new InventoryDetails();
                inventory.DepartmentId = departmentId;
                inventory.Name = name;
                inventory.Specification = specification;
                inventory.Quantity = quantity;
                inventory.Vendor = vendor;
                inventory.Price = price.ToString();
                inventory.ModelNo = modelNo;
                inventory.Remarks = remarks;
                inventory.Category = category;
                inventory.QuantityRecommended = recQuantity;
                inventory.Manufacturer = manufacturer;
                inventory.Location = location;

                return inventory;
            }
            catch (Exception ex)
            {
                logger.Error("InventoryDetailsDAO", "CreateInventory", ex.Message, ex, logPath);
                throw ex;
            }
        }
        #endregion
        public List<InventoryDetails> GetInventoryList(string cnxnString, string logPath)
        {
            try
            {
                Database db = new Database();
                DbDataReader reader = db.Select(SELECT_INVENTORY_DETAILS, cnxnString, logPath);

                if (reader.HasRows)
                {
                    List<DepartmentDetails> deptList = new DepartmentDetailsDAO().GetDepartmentList(cnxnString, logPath);
                    if (deptList == null) deptList = new List<DepartmentDetails>();

                    List<InventoryDetails> inventorylist = new List<InventoryDetails>();
                    while (reader.Read())
                    {
                        InventoryDetails inventorydetails = new InventoryDetails();
                        inventorydetails.Id = Convert.ToInt32(reader["Id"]);
                        inventorydetails.DepartmentId = Convert.ToInt32(reader["DepartmentId"]);
                        int departmentid = Convert.ToInt32(reader["DepartmentId"]);

                        if (deptList.Find(dl => dl.DepartmentId == departmentid) != null)
                            inventorydetails.DepartmentName = deptList.Find(dl => dl.DepartmentId == departmentid).DepartmentName;

                        inventorydetails.Name = reader["Name"].ToString();
                        inventorydetails.Specification = reader["Specification"].ToString();
                        inventorydetails.Quantity = Convert.ToInt32(reader["Quantity"]);
                        inventorydetails.Vendor = reader["Vendor"].ToString();
                        inventorydetails.Price = reader["Price"].ToString();
                        inventorydetails.ModelNo = reader["ModelNo"].ToString();
                        inventorydetails.Remarks = reader["Remarks"].ToString();
                        inventorydetails.Category = reader["Category"].ToString();
                        inventorydetails.QuantityRecommended = Convert.ToInt32(reader["QuantityRecommended"].ToString());
                        inventorydetails.Manufacturer = reader["Manufacturer"].ToString();
                        inventorydetails.Location = reader["LocationDetails"].ToString();
                        inventorylist.Add(inventorydetails);
                    }
                    if (reader != null)
                    {
                        reader.Close();
                    }


                    return inventorylist;
                }
            }
            catch (Exception ex)
            {
                logger.Error("InventoryDetailsDAO", "GetInventoryList", ex.Message, ex, logPath);
                throw ex;
            }

            return null;
        }

        public DataTable GetCurrentStockReport(DateTime startDate, DateTime endDate, string cnxnString, string logPath)
        {
            try
            {
                List<CurrentStock> listOfCurrentStock = new List<CurrentStock>();

                Database db = new Database();
                string qry = string.Format(Q_CURRENTSTOCK_ITEM_WISE, startDate.ToString("yyyy-MM-ddTHH:mm:ss"), endDate.ToString("yyyy-MM-ddTHH:mm:ss"));
                DbDataReader reader = db.Select(qry, cnxnString, logPath);
                DataTable newdt = new DataTable("CurrentStockReport");
                newdt.Columns.Add("Sr No.");
                newdt.Columns.Add("Item Name");
                newdt.Columns.Add("Vendor Name");
                newdt.Columns.Add("Bill No.");
                newdt.Columns.Add("Bill Date");

                if (reader.HasRows)
                {
                    List<DepartmentDetails> deptList = new DepartmentDetailsDAO().GetDepartmentList(cnxnString, logPath);
                    if (deptList == null) deptList = new List<DepartmentDetails>();

                    int count = 0;
                    while (reader.Read())
                    {

                        DataRow r = newdt.NewRow();
                        int inventoryID = Convert.ToInt32(reader["id"]);

                        string name = reader["Name"].ToString(); ;

                        r["Item Name"] = name;
                        int departmentid = Convert.ToInt32(reader["DepartmentId"]);
                        string DepartmentName = string.Empty;

                        if (deptList.Find(dl => dl.DepartmentId == departmentid) != null)
                            DepartmentName = deptList.Find(dl => dl.DepartmentId == departmentid).DepartmentName;

                        r["Vendor Name"] = reader["Vendor"].ToString();
                        r["Bill No."] = reader["BillNo"].ToString();
                        r["Bill Date"] = Convert.ToDateTime(reader["BillDate"]).ToString("dd-MM-yyyy");

                        if (!newdt.Columns.Contains(DepartmentName))
                            newdt.Columns.Add(DepartmentName, typeof(String));

                        int qty = Convert.ToInt32(reader["ReceivedQuantity"]);

                        r[DepartmentName] = qty;

                        bool isExist = listOfCurrentStock.Exists(a => a.Name.Equals(name));
                        if (isExist)
                        {
                            CurrentStock s = listOfCurrentStock.FirstOrDefault(a => a.Name.Equals(name));
                            int index = listOfCurrentStock.IndexOf(s);
                            s.listOfStock.Add(new Stock() { InventoryId = inventoryID, DepartmentId = departmentid, Quantity = qty });
                            listOfCurrentStock[index] = s;
                        }
                        else
                        {
                            CurrentStock s = new CurrentStock();
                            s.Name = name;
                            s.listOfStock.Add(new Stock() { InventoryId = inventoryID, DepartmentId = departmentid, Quantity = qty });
                            listOfCurrentStock.Add(s);
                            ++count;
                            r["Sr No."] = count;
                        }

                        newdt.Rows.Add(r);
                    }
                    if (reader != null)
                    {
                        reader.Close();
                        db.KillSleepingConnections(cnxnString, logPath);
                    }

                    DataTable sid = new DataTable("sid");
                    sid.Columns.Add("Sr No.");

                    sid.Columns.Add("Total Stock");
                
                    sid.Columns.Add("Total Stock Issued");

                    sid.Columns.Add("Actual Stock");

                    //foreach (DepartmentDetails d in deptList)
                    //{
                    //    if (!sid.Columns.Contains(d.DepartmentName))
                    //    {
                    //        sid.Columns.Add(d.DepartmentName, typeof(String));
                    //    }
                    //    if (!sid.Columns.Contains(d.DepartmentName + " "))
                    //    {
                    //        sid.Columns.Add(d.DepartmentName + " ", typeof(String));
                    //    }
                    //}
                
                    int c = 0;
                    DataTable dt2 = new DataTable();

                    using (MySqlConnection sqlConnection = new MySqlConnection(cnxnString))
                    {
                        sqlConnection.Open();
                        string query = string.Format(getIssueDetails, string.Join(",", listOfCurrentStock.SelectMany(a => a.listOfStock.Select(p => p.InventoryId).Distinct().ToArray())));
                        MySqlCommand sqlCommand = new MySqlCommand(query, sqlConnection);
                        sqlCommand.CommandType = CommandType.Text;
                        dt2.Locale = System.Globalization.CultureInfo.InvariantCulture;
                        MySqlDataAdapter sqlAdapter = new MySqlDataAdapter(sqlCommand);
                        sqlAdapter.Fill(dt2);
                    }
                    foreach (CurrentStock cs in listOfCurrentStock)
                    {
                        ++c;
                        DataRow r = sid.NewRow();
                        int total = 0;
                        int totalStockIssued = 0;
                        if (cs.listOfStock.Count > 0)
                        {
                            foreach (Stock s in cs.listOfStock)
                            {
                                DataRow result = dt2.Select(string.Format("InventorytId={0} AND DepartmentId={1}", s.InventoryId, s.DepartmentId)).FirstOrDefault();
                                if (result != null)
                                {
                                    int qty = Convert.ToInt32(result["IssueQuantity"]);
                                    int DepId = Convert.ToInt32(result["DepartmentId"]);
                                    string DepartmentName = string.Empty;

                                    if (deptList.Find(dl => dl.DepartmentId == DepId) != null)
                                        DepartmentName = deptList.Find(dl => dl.DepartmentId == DepId).DepartmentName;

                                    if (!sid.Columns.Contains(DepartmentName + " "))
                                    {
                                        sid.Columns.Add(DepartmentName + " ", typeof(String));
                                    }
                                    r[DepartmentName + " "] = qty;
                                    totalStockIssued = totalStockIssued + qty;
                                }
                                total = total + s.Quantity;
                            }
                        }
                        r["Total Stock Issued"] = totalStockIssued;
                        r["Actual Stock"] = total - totalStockIssued;
                        r["Total Stock"] = total;
                        r["Sr No."] = c;
                        sid.Rows.Add(r);
                    }
                    sid.Columns["Total Stock Issued"].SetOrdinal(sid.Columns.Count-1);
                    sid.Columns["Actual Stock"].SetOrdinal(sid.Columns.Count-1);
                    sid.AcceptChanges();

                    var inum = sid.AsEnumerable();
                    //newdt.Merge(sid,true,MissingSchemaAction.Add);
                    foreach (DataRow row in newdt.Rows)
                    {
                        string srno = row["Sr No."].ToString();
                        DataRow dr = inum.FirstOrDefault(d => d.Field<string>("Sr No.").Equals(srno));
                        if (dr != null)
                        {
                            foreach (DataColumn dc in sid.Columns)
                            {
                                if (!newdt.Columns.Contains(dc.ColumnName))
                                    newdt.Columns.Add(dc.ColumnName, typeof(String));

                                row[dc.ColumnName] = dr[dc];
                                newdt.AcceptChanges();
                            }

                        }
                    }
                    
                    return newdt;
                }
            }
            catch (Exception ex)
            {
                logger.Error("InventoryDetailsDAO", "GetInventoryList", ex.Message, ex, logPath);
                throw ex;
            }
            return null;
        }

        public List<InventoryDetails> GetInventoryList(string deptId, string category, string cnxnString, string logPath)
        {
            try
            {
                string result = string.Empty;
                if (!string.IsNullOrEmpty(deptId))
                {
                    result = string.Format(Q_GetInventoryByCategory, deptId, category);
                }
                else
                    result = string.Format(Q_GetInventoryByCategory_All, category);

                Database db = new Database();
                DbDataReader reader = db.Select(result, cnxnString, logPath);

                List<InventoryDetails> inventorylist = new List<InventoryDetails>();

                if (reader.HasRows)
                {
                    List<DepartmentDetails> deptList = new DepartmentDetailsDAO().GetDepartmentList(cnxnString, logPath);
                    if (deptList == null) deptList = new List<DepartmentDetails>();

                    while (reader.Read())
                    {
                        InventoryDetails inventorydetails = new InventoryDetails();
                        inventorydetails.Id = Convert.ToInt32(reader["Id"]);
                        inventorydetails.DepartmentId = Convert.ToInt32(reader["DepartmentId"]);
                        int departmentid = Convert.ToInt32(reader["DepartmentId"]);

                        if (deptList.Find(dl => dl.DepartmentId == departmentid) != null)
                            inventorydetails.DepartmentName = deptList.Find(dl => dl.DepartmentId == departmentid).DepartmentName;

                        inventorydetails.Name = reader["Name"].ToString();
                        inventorydetails.Specification = reader["Specification"].ToString();
                        inventorydetails.Quantity = Convert.ToInt32(reader["Quantity"]);
                        inventorydetails.Vendor = reader["Vendor"].ToString();
                        inventorydetails.Price = reader["Price"].ToString();
                        inventorydetails.ModelNo = reader["ModelNo"].ToString();
                        inventorydetails.Remarks = reader["Remarks"].ToString();
                        inventorydetails.Category = reader["Category"].ToString();
                        inventorydetails.QuantityRecommended = Convert.ToInt32(reader["QuantityRecommended"].ToString());
                        inventorydetails.Manufacturer = reader["Manufacturer"].ToString();
                        inventorydetails.Location = reader["LocationDetails"].ToString();
                        inventorylist.Add(inventorydetails);
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

        public List<InventoryDetails> GetInventoryList(string deptId, string cnxnString, string logPath)
        {
            try
            {
                string result = string.Empty;

                result = string.Format(Q_GetInventoryByDepartment, deptId);

                Database db = new Database();
                DbDataReader reader = db.Select(result, cnxnString, logPath);

                List<InventoryDetails> inventorylist = new List<InventoryDetails>();

                if (reader.HasRows)
                {
                    List<DepartmentDetails> deptList = new DepartmentDetailsDAO().GetDepartmentList(cnxnString, logPath);
                    if (deptList == null) deptList = new List<DepartmentDetails>();

                    while (reader.Read())
                    {
                        InventoryDetails inventorydetails = new InventoryDetails();
                        inventorydetails.Id = Convert.ToInt32(reader["Id"]);
                        inventorydetails.DepartmentId = Convert.ToInt32(reader["DepartmentId"]);
                        int departmentid = Convert.ToInt32(reader["DepartmentId"]);

                        if (deptList.Find(dl => dl.DepartmentId == departmentid) != null)
                            inventorydetails.DepartmentName = deptList.Find(dl => dl.DepartmentId == departmentid).DepartmentName;

                        inventorydetails.Name = reader["Name"].ToString();
                        inventorydetails.Specification = reader["Specification"].ToString();
                        inventorydetails.Quantity = Convert.ToInt32(reader["Quantity"]);
                        inventorydetails.Vendor = reader["Vendor"].ToString();
                        inventorydetails.Price = reader["Price"].ToString();
                        inventorydetails.ModelNo = reader["ModelNo"].ToString();
                        inventorydetails.Remarks = reader["Remarks"].ToString();
                        inventorydetails.Category = reader["Category"].ToString();
                        inventorydetails.QuantityRecommended = Convert.ToInt32(reader["QuantityRecommended"].ToString());
                        inventorydetails.Manufacturer = reader["Manufacturer"].ToString();
                        inventorydetails.Location = reader["LocationDetails"].ToString();
                        inventorylist.Add(inventorydetails);
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

        public List<InventoryDetails> GetInventoryListForPurchase(string deptId, string category, string cnxnString, string logPath)
        {
            try
            {

                string result = string.Empty;
                if (!string.IsNullOrEmpty(deptId))
                {
                    result = string.Format(Q_GetInvetoryForPurchase, deptId, category);
                }
                else
                    result = string.Format(Q_GetInvetoryForPurchase_All, category);
                //string result = string.Format(Q_GetInvetoryForPurchase, deptId, category);
                Database db = new Database();
                DbDataReader reader = db.Select(result, cnxnString, logPath);

                List<InventoryDetails> inventorylist = new List<InventoryDetails>();

                if (reader.HasRows)
                {
                    List<DepartmentDetails> deptList = new DepartmentDetailsDAO().GetDepartmentList(cnxnString, logPath);
                    if (deptList == null) deptList = new List<DepartmentDetails>();

                    while (reader.Read())
                    {
                        InventoryDetails inventorydetails = new InventoryDetails();
                        inventorydetails.Id = Convert.ToInt32(reader["Id"]);
                        inventorydetails.DepartmentId = Convert.ToInt32(reader["DepartmentId"]);
                        int departmentid = Convert.ToInt32(reader["DepartmentId"]);

                        if (deptList.Find(dl => dl.DepartmentId == departmentid) != null)
                            inventorydetails.DepartmentName = deptList.Find(dl => dl.DepartmentId == departmentid).DepartmentName;

                        inventorydetails.Name = reader["Name"].ToString();
                        inventorydetails.Specification = reader["Specification"].ToString();

                        inventorydetails.Manufacturer = reader["Manufacturer"].ToString();
                        inventorydetails.ModelNo = reader["PricePerUnit"].ToString();
                        inventorydetails.Price = reader["Total"].ToString();

                        inventorydetails.Category = reader["Category"].ToString();
                        // virtual columns
                        inventorydetails.QuantityRecommended = Convert.ToInt32(reader["Quantity"].ToString());

                        if (inventorydetails.QuantityRecommended < 0)
                        {
                            continue;
                        }

                        inventorylist.Add(inventorydetails);

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
                logger.Error("InventoryDetailsDAO", "GetInventoryListForPurchase", ex.Message, ex, logPath);
                throw ex;
            }

        }

        public InventoryDetails GetInventoryDetailById(int id, string cnxnString, string logPath)
        {
            try
            {
                string result = string.Format(GET_INVENTORY_DETAILS_BY_ID, id);
                Database db = new Database();
                DbDataReader reader = db.Select(result, cnxnString, logPath);
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        InventoryDetails inventorydetails = new InventoryDetails();
                        inventorydetails.Id = Convert.ToInt32(reader["Id"]);
                        inventorydetails.DepartmentId = Convert.ToInt32(reader["DepartmentId"]);
                        int departmentid = Convert.ToInt32(reader["DepartmentId"]);
                        inventorydetails.DepartmentName = new DepartmentDetailsDAO().GetDepartmentName(departmentid, cnxnString, logPath);
                        inventorydetails.Name = reader["Name"].ToString();
                        inventorydetails.Specification = reader["Specification"].ToString();
                        inventorydetails.Quantity = Convert.ToInt32(reader["Quantity"]);
                        inventorydetails.Vendor = reader["Vendor"].ToString();
                        inventorydetails.Price = reader["Price"].ToString();
                        inventorydetails.ModelNo = reader["ModelNo"].ToString();
                        inventorydetails.Remarks = reader["Remarks"].ToString();
                        inventorydetails.Category = reader["Category"].ToString();
                        inventorydetails.QuantityRecommended = Convert.ToInt32(reader["QuantityRecommended"].ToString());
                        inventorydetails.Manufacturer = reader["Manufacturer"].ToString();
                        inventorydetails.Location = reader["LocationDetails"].ToString();
                        return inventorydetails;
                    }
                    if (reader != null)
                    {
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("InventoryDetailsDAO", "GetInventoryDetailById", ex.Message, ex, logPath);
                throw ex;
            }

            return null;
        }
        public bool RemoveInventoryDetails(int id, string cnxnString, string logPath)
        {
            try
            {
                string result = string.Format(DELETE_INVENTORY_DETAILS, id);
                Database db = new Database();

                db.Delete(result, cnxnString, logPath);

                return true;
            }
            catch (Exception ex)
            {
                logger.Error("InventoryDetailsDAO", "RemoveInventoryDetails", ex.Message, ex, logPath);
                throw ex;
            }
        }

        public bool ChangeInventoryDetails(InventoryDetails inventoryDetails, string cnxnString, string logPath)
        {
            try
            {
                if (inventoryDetails != null)
                {
                    string result = string.Format(UPDATE_INVENTORY_DETAILS, inventoryDetails.Id, inventoryDetails.DepartmentId, ParameterFormater.FormatParameter(inventoryDetails.Name),
                        ParameterFormater.FormatParameter(inventoryDetails.Specification), inventoryDetails.Quantity, ParameterFormater.FormatParameter(inventoryDetails.Vendor),
                        inventoryDetails.Price, ParameterFormater.FormatParameter(inventoryDetails.ModelNo), ParameterFormater.FormatParameter(inventoryDetails.Remarks),
                        inventoryDetails.Category, inventoryDetails.QuantityRecommended, inventoryDetails.Manufacturer, inventoryDetails.Location, DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff"));

                    Database db = new Database();

                    db.Update(result, cnxnString, logPath);

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw new Exception("");
            }
        }
    }
}
