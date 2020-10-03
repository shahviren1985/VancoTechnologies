using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace CSVDataProcessor
{
    public partial class Service1 : ServiceBase
    {
        System.Timers.Timer timerFTP = new System.Timers.Timer();
        public string LastProcessedDate = ConfigurationManager.AppSettings["LastProcessedDate"];
        public string PostFileAPI = ConfigurationManager.AppSettings["PostFileAPI"];
        public string SourcePath = ConfigurationManager.AppSettings["SourcePath"];
        public string DataPath = ConfigurationManager.AppSettings["DataPath"];

        public string FailedPath = Path.Combine(ConfigurationManager.AppSettings["SourcePath"], "Failed");
        public string ProcessedPath = Path.Combine(ConfigurationManager.AppSettings["SourcePath"], "Processed");
        public string ProcessingPath = Path.Combine(ConfigurationManager.AppSettings["SourcePath"], "Processing");


        Thread t = null;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                EventLogs.LogFileCreate();
                EventLogs.ErrorWrite("service start...");

                t = new Thread(() => StartThread("*.csv"));
                t.Start();
            }
            catch (Exception ex)
            {

            }
        }

        private void StartThread(string filter)
        {
            while (true)
            {
                try
                {
                    var myFiles = Directory.GetFiles(SourcePath, filter, SearchOption.TopDirectoryOnly);
                    int files_count = myFiles.Count();
                    if (files_count > 0)
                    {
                        EventLogs.ErrorWrite(files_count + " files found at " + SourcePath);
                    }
                    foreach (var item in myFiles)
                    {
                        FileSystemEventArgs arg = new FileSystemEventArgs(WatcherChangeTypes.All, SourcePath, Path.GetFileName(item));
                        WatchDir(null, arg);
                    }
                }
                catch (Exception ex)
                {
                    EventLogs.ErrorWrite("Error on Picking File From Watch Folder : " + ex.ToString());
                }
                System.Threading.Thread.Sleep(1000);
            }
        }

        private void WatchDir(object sender, FileSystemEventArgs e)
        {
            if (!IsFileLocked(new FileInfo(e.FullPath)))
            {
                FileParsing fParser = new FileParsing();
                EventLogs.ErrorWrite("File Name is : " + e.FullPath);
                fParser.ProcessCSV(e.FullPath);
            }
            else
            {
                while (true)
                {
                    EventLogs.ErrorWrite("file is locked trying again: " + e.Name);
                    System.Threading.Thread.Sleep(1000);
                    if (!IsFileLocked(new FileInfo(e.FullPath)))
                    {
                        FileParsing fParser = new FileParsing();
                        fParser.ProcessCSV(e.FullPath);
                        break;
                    }
                }
            }
        }

        private bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }

        public static void StopService(string serviceName, int timeoutMilliseconds)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
            }
            catch
            {
                
            }
        }

        protected override void OnStop()
        {

            if (t != null)
                t.Abort();
            EventLogs.ErrorWrite("service stopped...");
        }
    }
}
