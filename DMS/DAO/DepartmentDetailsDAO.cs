using AA.DAOBase;
using AA.LogManager;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace AA.DAO
{
    public class DepartmentDetailsDAO
    {
        Logger logger = new Logger();

        public string SELECT_DEPARTMENT_DETAILS = "SELECT Id, Name,Enabled,Remarks FROM department";
        public string SELECT_ACTIVE_DEPARTMENT_DETAILS = "SELECT Id, Name,Enabled,Remarks FROM department WHERE Enabled<>'0'";
        
        public DepartmentDetailsDAO()
        {
            this.logger = new Logger();
        }


        public List<DepartmentDetails> GetDepartmentDetails(string cxnString, string logPath)
        {
            List<DepartmentDetails> departments = new List<DepartmentDetails>();

            try
            {
                logger.Debug("DepartmentDetailsDAO", "GetDepartmentDetails", " Getting departments", logPath);

                Database db = new Database();
                DbDataReader reader = db.Select(SELECT_DEPARTMENT_DETAILS, cxnString, logPath);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DepartmentDetails dept = new DepartmentDetails();

                        logger.Debug("DepartmentDetailsDAO", "GetDepartmentDetails", " Getting single department", logPath);
                        dept.Id = int.Parse(reader["Id"].ToString());
                        dept.DepartmentName = reader["Name"].ToString();
                        dept.IsEnabled = (reader["Enabled"].ToString() == "1") || (reader["Enabled"].ToString() == "49") ? true : false;
                        dept.Remarks = reader["Remarks"].ToString();

                        departments.Add(dept);
                    }

                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }
                }


            }
            catch (Exception ex)
            {
                logger.Error("DepartmentDetailsDAO", "GetDepartmentDetails", " Error occurred while Authenticating User", ex, logPath);
                throw ex;
            }

            return departments;
        }

        public List<DepartmentDetails> GetActiveDepartmentDetails(string cxnString, string logPath)
        {
            List<DepartmentDetails> list = new List<DepartmentDetails>();
            try
            {
                this.logger.Debug("DepartmentDetailsDAO", "GetActiveDepartmentDetails", " Getting departments", logPath);
                DbDataReader reader = new Database().Select(this.SELECT_ACTIVE_DEPARTMENT_DETAILS, cxnString, logPath);
                if (!reader.HasRows)
                {
                    return list;
                }
                while (reader.Read())
                {
                    DepartmentDetails item = new DepartmentDetails();
                    this.logger.Debug("DepartmentDetailsDAO", "GetActiveDepartmentDetails", " Getting single department", logPath);
                    item.Id = int.Parse(reader["Id"].ToString());
                    item.DepartmentName = reader["Name"].ToString();
                    item.IsEnabled = (reader["Enabled"].ToString() == "1") || (reader["Enabled"].ToString() == "49");
                    item.Remarks = reader["Remarks"].ToString();
                    list.Add(item);
                }
                if (!reader.IsClosed)
                {
                    reader.Close();
                }
            }
            catch (Exception exception)
            {
                this.logger.Error("DepartmentDetailsDAO", "GetActiveDepartmentDetails", " Error occurred while Authenticating User", exception, logPath);
                throw exception;
            }
            return list;
        }


    }
}
