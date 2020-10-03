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
using ITM.Courses.LogManager;

namespace ITM.Courses.DAOBase
{
    public class Database
    {
        Logger logger = new Logger();

        private string database;
        private MySqlConnection conn = null;
        private object lockObject = new object();

        //private DbConnection GetConnection(string logPath)
        private MySqlConnection GetConnection(string cnxnString, string logPath)
        {
            try
            {
                if (conn == null)
                {
                    lock (lockObject)
                    {
                        conn = new MySqlConnection(cnxnString);
                    }
                }

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                return conn;

            }
            catch (Exception ex)
            {
                logger.Error("Database", "GetConnection", "Database Operation Error", ex, logPath);
                throw ex;
            }

        }

        public DbDataReader Select(string cmdText, string logPath, MySqlConnection con)
        {
            DbDataReader result = null;

            if (string.IsNullOrEmpty(cmdText) || con == null || con.State == ConnectionState.Closed)
            {
                throw new Exception("Invalid arguments. Either connection is closed/null/command text is missing");
            }

            try
            {
                //logger.Debug("Database", "Select", "cmdtext: " + cmdText, logPath);

                using (DbCommand cmd = con.CreateCommand())
                {
                    cmd.CommandTimeout = 300;
                    cmd.CommandText = cmdText;
                    result = cmd.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Database", "Select", "Database Operation Error, CMDText: " + cmdText, ex, logPath);
                throw ex;
            }

            return result;
        }

        public DataSet SelectDataSet(string cmdText, string logPath, MySqlConnection con)
        {
            DataSet ds = new DataSet();

            if (!string.IsNullOrEmpty(cmdText) && con != null && con.State == ConnectionState.Open)
            {
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand(cmdText, con))
                    {
                        cmd.CommandTimeout = 300;
                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.Error("Database", "SelectDataSet", "Database Operation Error, CMDText: " + cmdText, ex, logPath);
                    throw ex;
                }
            }

            return ds;
        }

        public int Insert(string cmdText, string logPath, MySqlConnection cn)
        {
            try
            {
                if (cn == null || cn.State == ConnectionState.Closed)
                {
                    throw new Exception("Invalid Connection String/Connection Status. Error occurred while inserting new record");
                }

                MySqlCommand cmd = cn.CreateCommand();
                cmd.CommandTimeout = 300;
                cmd.CommandText = cmdText;
                cmd.ExecuteNonQuery();
                cmd.Parameters.Add(new MySqlParameter("newId", cmd.LastInsertedId));
                return Convert.ToInt32(cmd.Parameters["newId"].Value);
            }
            catch (Exception ex)
            {
                logger.Error("Database", "Insert", "Database Operation Error", ex, logPath);
                throw ex;
            }
        }

        public void Delete(string cmdText, string logPath, MySqlConnection con)
        {
            if (con != null && con.State == ConnectionState.Open && !string.IsNullOrEmpty(cmdText))
            {
                try
                {
                    DbCommand cmd = con.CreateCommand();
                    cmd.CommandText = cmdText;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    logger.Error("Database", "Delete", "Database Operation Error", ex, logPath);
                    throw ex;
                }
            }
        }

        public void Update(string cmdText, string logPath, MySqlConnection con)
        {
            DbDataReader result = null;

            if (con != null && !string.IsNullOrEmpty(cmdText))
            {
                try
                {
                    DbCommand cmd = con.CreateCommand();
                    cmd.CommandTimeout = 300;
                    cmd.CommandText = cmdText;
                    result = cmd.ExecuteReader();
                }
                catch (Exception ex)
                {
                    logger.Error("Database", "Update", "Database Operation Error", ex, logPath);
                    throw ex;
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

        private bool IsAvailable(MySqlConnection connection, string logPath)
        {
            var result = false;

            try
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    result = connection.Ping();
                }
            }
            catch (Exception e)
            {
                logger.Error("Database", "IsAvailable", "Ping exception: " + e.Message, e, logPath);
            }

            return result && connection.State == System.Data.ConnectionState.Open;
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

            #region Previos code to kill the connections
            /*MySqlConnection.ClearAllPools();
            try
            {
                myConn.Open();

                try
                {
                    MySqlCommand cmdtemp = new MySqlCommand("set net_write_timeout=99999; set net_read_timeout=99999", myConn); // Setting tiimeout on mysqlServer
                    cmdtemp.ExecuteNonQuery();
                }
                catch (Exception)
                {

                }

                cmd = myConn.CreateCommand();
                cmd.CommandTimeout = 5000000;
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
            }*/
            #endregion

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

                cmd.Dispose();
            }
            catch (Exception ex)
            {
                logger.Error("Database", "KillSleepingConnections", " Error occurred while killing sleeping connections", ex, logPath);
                //throw new Exception("11500");

            }
            //finally
            //{
            //    if (myConn != null && myConn.State == ConnectionState.Open)
            //    {
            //        myConn.Close();
            //    }
            //}

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

                    //if (strState == "Sleep" && string.Equals("exam", db) && iPID > 0)
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

                cmd.Dispose();
            }
            catch (Exception ex)
            {
            } 

            return;
        }
    }
}