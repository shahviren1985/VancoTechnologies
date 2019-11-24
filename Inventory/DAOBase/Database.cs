using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Collections;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using ITM.LogManager;

namespace ITM.DAOBase
{
    public class Database
    {
        Logger logger = new Logger();

        private string database;

        private DbConnection GetConnection(string logPath)
        {
            try
            {
                if (string.IsNullOrEmpty(database))
                {
                    database = "mysql";
                }

                switch (database)
                {
                    case "mysql":
                        return new MySqlConnection();
                    case "sqlserver":
                        return new SqlConnection();
                    default:
                        return new MySqlConnection();
                }

            }
            catch (Exception ex)
            {
                logger.Error("Database","GetConnection", "Database Operation Error", ex, logPath);
                throw new Exception("11061");
            }

        }

        public DbDataReader Select(string cmdText, string cnxnString, string logPath)
        {
            DbConnection con = GetConnection(logPath);

            DbDataReader result = null;

            if (string.IsNullOrEmpty(cnxnString))
            {
                return null;
            }

            if (!string.IsNullOrEmpty(cmdText))
            {
                try
                {
                    con.ConnectionString = cnxnString;
                    con.Open();

                    DbCommand cmd = con.CreateCommand();
                    cmd.CommandText = cmdText;
                    result = cmd.ExecuteReader();
                }
                catch (Exception ex)
                {
                    logger.Error("Database", "Select", "Database Operation Error", ex, logPath);
                    throw ex;
                }
            }

            return result;
        }

        public void Insert(string cmdText, string cnxnString, string logPath)
        {
            using (DbConnection con = GetConnection(logPath))
            {
                DbDataReader result = null;

                if (!string.IsNullOrEmpty(cnxnString) && !string.IsNullOrEmpty(cmdText))
                {
                    try
                    {
                        con.ConnectionString = cnxnString;
                        con.Open();

                        DbCommand cmd = con.CreateCommand();
                        cmd.CommandText = cmdText;
                        int r = cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        logger.Error("Database", "Insert", "Database Operation Error", ex, logPath);
                        throw new Exception("11063");
                    }
                    finally
                    {
                        if (result != null && !result.IsClosed)
                        {
                            result.Close();
                        }
                    }
                }
            }
        }

        public void Delete(string cmdText, string cnxnString, string logPath)
        {
            using (DbConnection con = GetConnection(logPath))
            {

                if (!string.IsNullOrEmpty(cnxnString) && !string.IsNullOrEmpty(cmdText))
                {
                    try
                    {
                        con.ConnectionString = cnxnString;
                        con.Open();

                        DbCommand cmd = con.CreateCommand();
                        cmd.CommandText = cmdText;
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        logger.Error("Database", "Delete", "Database Operation Error", ex, logPath);
                        throw new Exception("11064");
                    }
                    finally
                    {
                        //con.Close();
                    }
                }
            }
        }

        public void Update(string cmdText, string cnxnString, string logPath)
        {
            using (DbConnection con = GetConnection(logPath))
            {
                DbDataReader result = null;

                if (!string.IsNullOrEmpty(cnxnString) && !string.IsNullOrEmpty(cmdText))
                {
                    try
                    {
                        con.ConnectionString = cnxnString;
                        con.Open();

                        DbCommand cmd = con.CreateCommand();
                        cmd.CommandText = cmdText;
                        result = cmd.ExecuteReader();
                    }
                    catch (Exception ex)
                    {
                        logger.Error("Database", "Update", "Database Operation Error", ex, logPath);
                        throw new Exception("11065");
                    }
                    finally
                    {
                        if (result != null && !result.IsClosed)
                        {
                            result.Close();
                        }
                    }
                }
            }
        }

        public void KillSleepingConnections(string cnxnString, string logPath)
        {
            //Manages processlist.
            string strSQL = "show processlist";
            ArrayList m_ProcessesToKill = new ArrayList();

            MySqlConnection myConn = new MySqlConnection();
            MySqlDataReader result = null;
            MySqlCommand cmd = null;
            myConn.ConnectionString = cnxnString;

            try
            {
                myConn.Open();
                cmd = myConn.CreateCommand();
                cmd.CommandText = strSQL;
                result = cmd.ExecuteReader();

                while (result.Read())
                {
                    // Find all processes sleeping with a timeout value higher than our threshold.
                    int iPID = Convert.ToInt32(result["Id"].ToString());
                    string strState = result["Command"].ToString();
                    string db = result["DB"].ToString();

                    if (strState == "Sleep" && iPID > 0)
                    {
                        // This connection is sitting around doing nothing. Kill it.
                        m_ProcessesToKill.Add(iPID);
                    }
                }

                result.Close();

                foreach (int aPID in m_ProcessesToKill)
                {
                    strSQL = "kill " + aPID;
                    cmd.CommandText = strSQL;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Database", "KillSleepingConnections", " Error occurred while killing sleeping connections", ex, logPath);
                //throw new Exception("11500");

            }
            finally
            {
                if (myConn != null && myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }

            return;
        }

        public static void KillConnections(string cnxnString, string logPath)
        {
            //Manages processlist.
            string strSQL = "show processlist";
            ArrayList m_ProcessesToKill = new ArrayList();

            MySqlConnection myConn = new MySqlConnection();
            MySqlDataReader result = null;
            MySqlCommand cmd = null;
            myConn.ConnectionString = cnxnString;

            try
            {
                myConn.Open();
                cmd = myConn.CreateCommand();
                cmd.CommandText = strSQL;
                result = cmd.ExecuteReader();

                while (result.Read())
                {
                    // Find all processes sleeping with a timeout value higher than our threshold.
                    int iPID = Convert.ToInt32(result["Id"].ToString());
                    string strState = result["Command"].ToString();
                    string db = result["DB"].ToString();

                    if (strState == "Sleep" && string.Equals("exam", db) && iPID > 0)
                    {
                        // This connection is sitting around doing nothing. Kill it.
                        m_ProcessesToKill.Add(iPID);
                    }
                }

                result.Close();

                foreach (int aPID in m_ProcessesToKill)
                {
                    strSQL = "kill " + aPID;
                    cmd.CommandText = strSQL;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                //logger.Error("Database", "KillSleepingConnections", " Error occurred while killing sleeping connections", ex, logPath);
                //throw new Exception("11500");

            }
            finally
            {
                if (myConn != null && myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }

            return;
        }
    }
}
