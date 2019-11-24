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
    public class InventoryIssuesDAO
    {
        Logger logger = new Logger();

        public string INSERT_INVENTORY_ISSUES = "INSERT INTO inventoryissues (InventorytId, DepartmentId, TeacherName, IssueQuantity, IssuerId, IssuerName, IssueDate, LastUpdateDate) " +
                                                                                 "VALUES ({0},{1},'{2}',{3},{4},'{5}','{6}','{7}')";
        
        public string UPDATE_INVENTORY_STOCK = "UPDATE  inventorydetails set Quantity = Quantity - {0} WHERE ID = {1}";

        string Select_INVENTORY_ISSUES = "SELECT * FROM inventoryissues where IssueDate>='{0}' and IssueDate<='{1}' order by DepartmentId ";
           
        #region Insert
        public void CreateInventoryIssue(int departmentId, int inventoryId, string teacherName, int issueQuantity, int issuerId, string issuerName, DateTime issueDate, string cnxnString, string logPath)
        {
            try
            {
                string result = string.Format(INSERT_INVENTORY_ISSUES,
                    inventoryId,
                    departmentId,
                    teacherName,
                    issueQuantity,
                    issuerId,
                    ParameterFormater.FormatParameter(issuerName), 
                    issueDate.ToString("yyyy/MM/dd hh:mm:ss.fff"),
                    DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff"));

                Database db = new Database();
                db.Insert(result, cnxnString, logPath);

                string updateResult = string.Format(UPDATE_INVENTORY_STOCK, issueQuantity, inventoryId);
                db.Update(updateResult, cnxnString, logPath);
            }
            catch (Exception ex)
            {
                logger.Error("InventoryDetailsDAO", "CreateInventory", ex.Message, ex, logPath);
                throw ex;
            }
        }


        public List<InventoryIssues> GetIssueList(DateTime issueStartDate, DateTime issueEndDate, string cnxnString, string logPath)
        {
            try
            {
                string result = string.Format(Select_INVENTORY_ISSUES, issueStartDate.ToString("yyyy-MM-ddTHH:mm:ss"), issueEndDate.ToString("yyyy-MM-ddTHH:mm:ss"));
                Database db = new Database();
                DbDataReader reader = db.Select(result, cnxnString, logPath);

                List<InventoryIssues> inventorylist = new List<InventoryIssues>();

                if (reader.HasRows)
                {
                    List<DepartmentDetails> deptList = new DepartmentDetailsDAO().GetDepartmentList(cnxnString, logPath);
                    if (deptList == null) deptList = new List<DepartmentDetails>();

                    List<InventoryDetails> inventoryList = new InventoryDetailsDAO().GetInventoryList(cnxnString, logPath);
                    if (inventoryList == null) inventoryList = new List<InventoryDetails>();

                    while (reader.Read())
                    {
                        InventoryIssues inventoryIssuesdetails = new InventoryIssues();
                        inventoryIssuesdetails.Id = Convert.ToInt32(reader["Id"]);

                        inventoryIssuesdetails.DepartmentId = Convert.ToInt32(reader["DepartmentId"]);
                        int departmentid = Convert.ToInt32(reader["DepartmentId"]);
                        if (deptList.Find(dl => dl.DepartmentId == departmentid) != null)
                            inventoryIssuesdetails.DepartmentName = deptList.Find(dl => dl.DepartmentId == departmentid).DepartmentName;

                        int InventorytId = Convert.ToInt32(reader["InventorytId"]);
                        inventoryIssuesdetails.InventoryId = InventorytId;
                        if (inventoryList.Find(dl => dl.Id == InventorytId) != null)
                            inventoryIssuesdetails.InventoryName = inventoryList.Find(dl => dl.Id == InventorytId).Name;

                        inventoryIssuesdetails.TeacherName = reader["TeacherName"].ToString();
                        inventoryIssuesdetails.IssuerId = Convert.ToInt32(reader["IssuerId"].ToString());
                        inventoryIssuesdetails.IssueQuantity = Convert.ToInt32(reader["IssueQuantity"].ToString());
                        inventoryIssuesdetails.IssueDate = Convert.ToDateTime(reader["IssueDate"].ToString());
                        inventoryIssuesdetails.IssuerName = reader["IssuerName"].ToString();
                        inventorylist.Add(inventoryIssuesdetails);
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
        #endregion
       
    }
}
