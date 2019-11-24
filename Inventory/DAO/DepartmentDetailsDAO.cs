using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;

using ITM.DAOBase;
using ITM.LogManager;
using ITM.Utilities;

namespace ITM.DAO
{
    public class DepartmentDetailsDAO
    {
        Logger logger = new Logger();

        public string SELECT_DEPARTMENT_MASTER = "SELECT DepartmentId, DepartmentName,ShortForm,History FROM department order by DepartmentName";
        public string INSERT_DEPARTMENT_MASTER = "INSERT INTO department (DepartmentName,ShortForm,History) VALUES ('{0}','{1}','{2}')";
        public string UPDATE_DEPARTMENT_MASTER = "UPDATE  department set DepartmentName = '{1}',ShortForm = '{2}',History = '{3}' WHERE DepartmentId ={0}";
        public string DELETE_DEPARTMENT_MASTER = "DELETE FROM department WHERE DepartmentId ={0}";
        public string GET_DEPARTMENT_BY_ID = "SELECT DepartmentName FROM department WHERE DepartmentId ={0}";
        public string SELECT_DEPARTMENT_BY_ID = "SELECT DepartmentId, DepartmentName,ShortForm,History FROM department WHERE DepartmentId={0}";

        #region Insert
        public DepartmentDetails CreateDepartmentDetails(string DepartmentName, string ShortForm, string History, string cnxnString, string logPath)
        {
            try
            {
                string result = string.Format(INSERT_DEPARTMENT_MASTER, ParameterFormater.FormatParameter(DepartmentName), ParameterFormater.FormatParameter(ShortForm), ParameterFormater.FormatParameter(History));
                Database db = new Database();
                db.Insert(result, cnxnString, logPath);
                DepartmentDetails dept = new DepartmentDetails();
                dept.DepartmentName = DepartmentName;
                dept.ShortForm = ShortForm;
                dept.History = History;
                logger.Debug("DepartmentDetailsDAO", "CreateCourse", "Department Details Saved", logPath);
                return dept;
            }
            catch (Exception ex)
            {
                logger.Error("DepartmentDetailsDAO", "CreateCourse", " Error occurred while Add Department", ex, logPath);
                throw new Exception("11276");
            }
        }
        #endregion

        #region Select
        public List<DepartmentDetails> GetDepartmentList(string cnxnString, string logPath)
        {
            try
            {
                logger.Debug("DepartmentDetailsDAO", "GetDepartmentList", "Selecting Department Details from database", logPath);

                Database db = new Database();
                DbDataReader reader = db.Select(SELECT_DEPARTMENT_MASTER, cnxnString, logPath);
                if (reader.HasRows)
                {
                    List<DepartmentDetails> deptlist = new List<DepartmentDetails>();
                    while (reader.Read())
                    {
                        DepartmentDetails dept = new DepartmentDetails();
                        dept.DepartmentId = Convert.ToInt32(reader["DepartmentId"]);
                        dept.DepartmentName = reader["DepartmentName"].ToString();
                        dept.ShortForm = reader["ShortForm"].ToString();
                        dept.History = reader["History"].ToString();
                        deptlist.Add(dept);
                    }
                    if (reader != null)
                    {
                        reader.Close();
                    }

                    logger.Debug("DepartmentDetailsDAO", "GetDepartmentList", " returning Department Details from database", logPath);
                    return deptlist;
                }
            }
            catch (Exception ex)
            {
                logger.Error("DepartmentDetailsDAO", "GetDepartmentList", " Error occurred while Get Department Details", ex, logPath);
                throw new Exception("11277");
            }

            return null;
        }

        public string GetDepartmentName(int Id, string cnxnString, string logPath)
        {
            string departmentName = string.Empty;

            try
            {
                string result = string.Format(GET_DEPARTMENT_BY_ID, Id);
                Database db = new Database();
                logger.Debug("DepartmentDetailsDAO", "GetDepartmentName", "Reading Department Master Details ", logPath);

                DbDataReader reader = db.Select(result, cnxnString, logPath);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        departmentName = reader["departmentName"].ToString();
                    }
                }

                logger.Debug("DepartmentDetailsDAO", "GetDepartmentName", "Returning Department Name", logPath);

                return departmentName;
            }
            catch (Exception ex)
            {
                logger.Error("DepartmentDetailsDAO", "GetDepartmentName", " Error occurred while getting Department Name", ex, logPath);
                throw new Exception("11278");

            }
        }
        #endregion

        public bool RemoveDepartment(int deptId, string cnxnString, string logPath)
        {
            try
            {
                string result = string.Format(DELETE_DEPARTMENT_MASTER, deptId);
                Database db = new Database();
                logger.Debug("DepartmentDetailsDAO", "RemoveDepartment", "Saving Changes in Department Master Details", logPath);
                db.Delete(result, cnxnString, logPath);
                logger.Debug("DepartmentDetailsDAO", "RemoveDepartment", "Saved Changes in Department Master Details", logPath);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error("DepartmentDetailsDAO", "RemoveDepartment", " Error occurred while Delete Department Master", ex, logPath);
                throw new Exception("11279");
            }
        }

        public DepartmentDetails GetDepartmentMasterById(int id, string cnxnString, string logPath)
        {
            try
            {
                logger.Debug("DepartmentDetailsDAO", "GetDepartmentMasterById", "Selecting Department Details from database", logPath);
                string result = string.Format(SELECT_DEPARTMENT_BY_ID, id);
                Database db = new Database();
                DbDataReader reader = db.Select(result, cnxnString, logPath);
                if (reader.HasRows)
                {
                    DepartmentDetails dept = new DepartmentDetails();
                    while (reader.Read())
                    {

                        dept.DepartmentId = Convert.ToInt32(reader["DepartmentId"]);
                        dept.DepartmentName = reader["DepartmentName"].ToString();
                        dept.ShortForm = reader["ShortForm"].ToString();
                        dept.History = reader["History"].ToString();

                    }
                    if (reader != null)
                    {
                        reader.Close();
                    }

                    logger.Debug("DepartmentDetailsDAO", "GetDepartmentMasterById", " returning Department Details from database", logPath);
                    return dept;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return null;
        }

        public bool ChangeDepartmentDetails(DepartmentDetails departmentDetails, string cnxnString, string logPath)
        {
            try
            {
                if (departmentDetails != null)
                {
                    logger.Debug("DepartmentDetailsDAO", "ChangeDepartmentDetails", "Updating Department Master Details", logPath);
                    string result = string.Format(UPDATE_DEPARTMENT_MASTER, departmentDetails.DepartmentId, ParameterFormater.FormatParameter(departmentDetails.DepartmentName), ParameterFormater.FormatParameter(departmentDetails.ShortForm), ParameterFormater.FormatParameter(departmentDetails.History));
                    Database db = new Database();
                    logger.Debug("DepartmentDetailsDAO", "ChangeDepartmentDetails", "Saving Department Master Details Saved", logPath);
                    db.Update(result, cnxnString, logPath);

                    logger.Debug("DepartmentDetailsDAO", "ChangeDepartmentDetails", "Department Master Details Saved", logPath);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                logger.Error("DepartmentDetailsDAO", "ChangeDepartmentDetails", " Error occurred while Update Department Master", ex, logPath);
                throw new Exception("11280");
            }
        }
    }
}
