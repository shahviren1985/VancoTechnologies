using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Common;
using System.Diagnostics;
using System.Xml;
using ITM.InventoryXmlConfiguration;

namespace ITM.LogManager
{
    public class Logger
    {
        InventoryConfiguration configuration = new InventoryConfiguration();

        private FileStream fileStream;
        private StreamWriter streamWriter;

        private object lockObject = new object();

        private string LogPath;
        private string LogFileName;
        private string LogLevel;
        private string LogDestination;
        private int LogFileSize;

        public Logger()
        {
            InitializeStaticMembers();
        }

        public void SetLogPath(string logPath, string fileName)
        {
            try
            {
                LogPath = logPath;
                LogFileName = fileName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InitializeStaticMembers()
        {
            try
            {
                LoggerDetails details = configuration.GetLoggerDetails("");

                if (details != null)
                {
                    LogPath = details.LogFilePath;
                    LogFileName = details.LogFileName;
                    LogLevel = details.LogLevel;
                    LogDestination = details.LogType;
                    LogFileSize = int.Parse(details.LogFileSizeMB) * 1024 * 1024;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Error(string className, string methodName, string errorMsg, string logPath)
        {
            //LogPath = logPath;
            if (!string.IsNullOrEmpty(logPath))
                LogPath = logPath;

            try
            {
                WriteLog(className, "E", methodName, errorMsg, null, null);
            }
            catch (Exception ex)
            {
                //EventLog.WriteEntry("LogManager_Logger_Error", ex.Message, EventLogEntryType.Error);
            }
        }

        public void Error(string className, string methodName, string errorMsg, Exception ex, string logPath)
        {
            //LogPath = logPath;
            if (!string.IsNullOrEmpty(logPath))
                LogPath = logPath;

            try
            {
                WriteLog(className, "E", methodName, errorMsg, ex, null);
            }
            catch (Exception)
            {
                //EventLog.WriteEntry("LogManager_Logger_Error with Exception arguments", ex.Message, EventLogEntryType.Error);
                //throw;
            }
        }

        public void Error(string className, string methodName, Exception ex, string logPath)
        {
            //LogPath = logPath;
            if (!string.IsNullOrEmpty(logPath))
                LogPath = logPath;

            try
            {
                WriteLog(className, "E", methodName, null, ex, null);
            }
            catch (Exception)
            {
                //EventLog.WriteEntry("LogManager_Logger_Error with Exception arguments", ex.Message, EventLogEntryType.Error);
                //throw;
            }
        }

        public void Error(string className, string methodName, string errorMsg, Hashtable args, string logPath)
        {
            //LogPath = logPath;
            if (!string.IsNullOrEmpty(logPath))
                LogPath = logPath;

            try
            {
                WriteLog(className, "E", methodName, errorMsg, null, args);
            }
            catch (Exception ex)
            {
                //EventLog.WriteEntry("LogManager_Logger_Error with Hashtable arguments", ex.Message, EventLogEntryType.Error);
                //throw;
            }
        }

        public void Debug(string className, string methodName, string message, Hashtable args, string logPath)
        {
            //LogPath = logPath;
            if (!string.IsNullOrEmpty(logPath))
                LogPath = logPath;

            try
            {
                WriteLog(className, "D", methodName, message, null, args);
            }
            catch (Exception ex)
            {
                //EventLog.WriteEntry("LogManager_Logger_Debug with Hashtable arguments", ex.Message, EventLogEntryType.Error);
                //throw;
            }
        }

        public void Debug(string className, string methodName, string message, string logPath)
        {
            if (!string.IsNullOrEmpty(logPath))
                LogPath = logPath;

            try
            {
                WriteLog(className, "D", methodName, message, null, null);
            }
            catch (Exception ex)
            {
                //EventLog.WriteEntry("LogManager_Logger_Debug", ex.Message, EventLogEntryType.Error);
                //throw;
            }
        }

        public void Info(string className, string methodName, string message, string logPath)
        {
            //LogPath = logPath;
            if (!string.IsNullOrEmpty(logPath))
                LogPath = logPath;

            try
            {
                WriteLog(className, "I", methodName, message, null, null);
            }
            catch (Exception ex)
            {
                //EventLog.WriteEntry("LogManager_Logger_Info", ex.Message, EventLogEntryType.Error);
                //throw;
            }
        }

        private void WriteLog(string className, string mode, string methodName, string message, Exception exc, Hashtable args)
        {
            try
            {
                lock (lockObject)
                {
                    string arguments = string.Empty;
                    string writeText = string.Empty;

                    if (exc != null)
                    {
                        if (exc.InnerException != null)
                        {
                            arguments += "ExceptionMessage: ";
                            arguments += exc.Message;
                            arguments += " || InnerException: ";
                            arguments += exc.InnerException.Message;
                            arguments += " || StackTrace:";
                            arguments += exc.StackTrace;
                        }
                        else
                        {
                            arguments += exc.Message;
                            arguments += " || StackTrace:";
                            arguments += exc.StackTrace;
                        }

                        if (!string.IsNullOrEmpty(message))
                        {
                            writeText = System.DateTime.Now.ToString("dd/MMM/yyyy HH:mm:ss") + " || " + mode + " || " + className + " || " + methodName + " || "
                                + message + " || " + arguments;
                        }
                        else
                        {
                            writeText = System.DateTime.Now.ToString("dd/MMM/yyyy HH:mm:ss") + " || " + mode + " || " + className + " || " + methodName + " || "
                                + exc.Message + " || " + arguments;
                        }
                    }
                    else if (args != null)
                    {
                        IDictionaryEnumerator num = args.GetEnumerator();
                        arguments += "arguments: ";
                        while (num.MoveNext())
                        {
                            arguments += num.Key + " : ";
                            arguments += num.Value + ", ";
                        }

                        writeText = System.DateTime.Now.ToString("dd/MMM/yyyy HH:mm:ss") + " || " + mode + " || " + className + " || " + methodName + " || "
                                + message + " || " + arguments;
                    }
                    else
                    {
                        writeText = System.DateTime.Now.ToString("dd/MMM/yyyy HH:mm:ss") + " || " + mode + " || " + className + " || " + methodName + " || "
                                            + message;
                    }

                    if (LogDestination == "file")
                    {
                        OpenFile();
                        streamWriter.WriteLine(writeText);
                        CloseFile();
                    }
                    else if (LogDestination == "db")
                    {
                        string dbType = ConfigurationManager.AppSettings["DatabaseType"];

                        string _ConnectionString = ConfigurationManager.ConnectionStrings[dbType].ConnectionString;

                        DbConnection con = GetConnection(dbType);
                        con.ConnectionString = _ConnectionString;
                        con.Open();
                        DbCommand cmd = con.CreateCommand();
                        cmd.CommandText = string.Format("INSERT INTO [Logger] ([CreateDateTime] ,[Mode],[ClassName],[MethodName],[Message],[ExceptionDetails]"
                                + ",[ArgumentsDetails]) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", DateTime.Now.ToString("dd/MMM/yyyy HH:mm:ss"), mode, className, methodName, message, arguments, arguments);
                        try
                        {
                            int r = cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            EventLog.WriteEntry("LogManager_Logger_Write (insert into Database)", ex.Message, EventLogEntryType.Error);
                            //throw;
                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //EventLog.WriteEntry("LogManager_Logger_Write (write into file)", ex.Message, EventLogEntryType.Error);
            }
        }

        private DbConnection GetConnection(string dbType)
        {
            try
            {
                if (string.IsNullOrEmpty(dbType))
                {
                    dbType = "sqlserver";
                }

                switch (dbType)
                {
                    case "mysql":
                        //return new MySqlConnection();
                        break;
                    case "sqlserver":
                        return new SqlConnection();
                    default:
                        return new SqlConnection();
                }
            }
            catch (Exception ex)
            {
                //EventLog.WriteEntry("LogManager_Logger_GetConnection type", ex.Message, EventLogEntryType.Error);
            }
            return null;
        }

        /// <summary>
        /// Open file
        /// </summary>
        private void OpenFile()
        {
            try
            {
                string strPath = null;
                //InitializeStaticMembers();
                if (string.IsNullOrEmpty(LogPath) || string.IsNullOrEmpty(LogFileName) || string.IsNullOrEmpty(LogFileSize.ToString()) || string.IsNullOrEmpty(LogDestination)
                    || string.IsNullOrEmpty(LogLevel))
                {
                    InitializeStaticMembers();
                }


                //check if directory exists or not
                if (Directory.Exists(LogPath))
                {
                }
                else
                {
                    // if not exist then create directory
                    Directory.CreateDirectory(LogPath);
                }

                strPath = LogPath + "\\" + LogFileName;
                //check if file exist or not
                if (System.IO.File.Exists(strPath))
                {
                    // if exist then get file infomartion
                    FileInfo file = new FileInfo(strPath);
                    // check file length if it is greater than our specific length then renamed existing file and create new file
                    if (file.Length < LogFileSize)
                    {
                        fileStream = new FileStream(strPath, FileMode.Append, FileAccess.Write);
                    }
                    else
                    {
                        string reNameFile = "\\" + Path.GetFileNameWithoutExtension(file.Name) + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString()
                            + DateTime.Now.Day.ToString() + DateTime.Now.ToString("HHmmss") + System.IO.Path.GetExtension(file.Name);
                        File.Copy(strPath, LogPath + reNameFile);
                        File.Delete(strPath);

                        fileStream = new FileStream(strPath, FileMode.Create, FileAccess.Write);
                    }
                }
                else
                {
                    fileStream = new FileStream(strPath, FileMode.Create, FileAccess.Write);
                }

                streamWriter = new StreamWriter(fileStream);
            }
            catch (Exception ex)
            {
                //EventLog.WriteEntry("LogManager_Logger_OpenFile", ex.Message, EventLogEntryType.Error);
            }
        }

        /// <summary>
        /// Close open file
        /// </summary>
        private void CloseFile()
        {
            try
            {
                streamWriter.Close();
                fileStream.Close();
            }
            catch (Exception ex)
            {
                //EventLog.WriteEntry("LogManager_Logger_CloseFile", ex.Message, EventLogEntryType.Error);
            }
        }
    }
}
