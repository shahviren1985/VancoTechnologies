using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Timers;
using AttendanceTracker.Helpers;
using AttendanceTracker.Models.Local;

namespace AttendanceTracker
{
    class PicDownloaderEngine : IDisposable
    {
        bool stopped = true;
        bool needToStop = false;

        Timer downloadTimer;

        public void Start(int delaySec)
        {
            Stop();
            needToStop = false;

            downloadTimer = new Timer(delaySec * 1000);
            downloadTimer.AutoReset = false;
            downloadTimer.Elapsed += DownloadTimer_Elapsed; ;
            downloadTimer.Start();
        }

        public void Stop()
        {
            needToStop = true;

            while (!stopped)
            {
                Task.Delay(100).Wait();
            }

            Task.Delay(100).Wait();

            if (downloadTimer != null)
            {
                downloadTimer.Stop();
                downloadTimer.Dispose();
                downloadTimer = null;
            }
        }

        private void DownloadTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            DownloadPitures();
        }

        private void DownloadPitures()
        {
            try
            {
                FileToDownload[] files = null;

                using (var localContext = new LocalContext())
                {
                    files = localContext.StudentDetails
                        .Where(s => s.PhotoPath != null)
                        .Select(s => new FileToDownload() { AdmissionYear = s.AdmissionYear, PhotoPath = s.PhotoPath })
                        .ToArray();
                }

                foreach (var f in files)
                {
                    if (needToStop)
                    {
                        stopped = true;
                        return;
                    }

                    try
                    {
                        DownloadFile(f);
                    }
                    catch (Exception e)
                    {
                        // We just hide the error is occurs
                    }
                }

                SyncLogHelper.AddSyncLog("PIC");
            }
            catch (Exception)
            {
            }
            finally
            {
                stopped = true;
            }
        }

        private void DownloadFile(FileToDownload file)
        {
            Uri picRemoteUrl = new Uri(PictureHelper.GetPicRemoteUri(file.AdmissionYear, file.PhotoPath));
            string downloadedFilePath = PictureHelper.GetLocalPicPath(file.AdmissionYear, file.PhotoPath);

            if (!File.Exists(downloadedFilePath))
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(picRemoteUrl, downloadedFilePath);
                }
            }
        }

        private class FileToDownload
        {
            public string AdmissionYear { get; set; }

            public string PhotoPath { get; set; }
        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Stop();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~PicDownloaderEngine()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion
    }
}
