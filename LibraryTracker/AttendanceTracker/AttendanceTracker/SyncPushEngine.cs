using System;
using System.Linq;
using System.Timers;
using AttendanceTracker.Helpers;
using AttendanceTracker.Models.Local;
using AttendanceTracker.Models.Remote;

namespace AttendanceTracker
{
    class SyncPushEngine : IDisposable
    {
        Timer pushTimer;

        public SyncPushEngine()
        {
            // We wait 5 seconds before start sync during program start.
            pushTimer = new Timer(5000);
            pushTimer.AutoReset = false;
            pushTimer.Elapsed += PushTimer_Elapsed;
            pushTimer.Start();
        }

        public void Stop()
        {
            if (pushTimer != null)
            {
                pushTimer.Stop();
                pushTimer.Dispose();
                pushTimer = null;
            }
        }

        private void PushTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            PushData();
            pushTimer.Interval = Config.SyncPushIntervalMinutes * 60 * 1000;
            pushTimer.Start();
        }

        private void PushData()
        {
            try
            {
                using (var localContext = new LocalContext())
                {
                    var logsToPush = localContext.LibraryLogs
                        .Where(l => !l.Pushed)
                        .ToArray();

                    foreach (var l in logsToPush)
                    {
                        if (l.OutTime == null && l.InTime < DateTime.Today)
                        {
                            l.OutTime = l.InTime.AddMinutes(Config.AutoCheckoutMinutesToAdd);
                        }

                        if (l.OutTime != null)
                        {
                            try
                            {
                                AddLibraryLogToRemote(l);
                                l.Pushed = true;
                                localContext.SaveChanges();
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                }

                SyncLogHelper.AddSyncLog("PUSH");
            }
            catch (Exception)
            {
            }
        }

        private void AddLibraryLogToRemote(Models.Local.LibraryLog localLog)
        {
            using (var remoteContext = new RemoteContext())
            {
                var remoteLog = remoteContext.LibraryLogs.Where(l => l.IdInAttendanceTracker == localLog.Id).FirstOrDefault();

                if (remoteLog == null)
                {
                    remoteLog = new Models.Remote.LibraryLog()
                    {
                        CRN = localLog.CRN,
                        Date = localLog.Date,
                        IdCardNumber = localLog.IdCardNumber,
                        IdInAttendanceTracker = localLog.Id,
                        InTime = localLog.InTime,
                        OutTime = localLog.OutTime
                    };

                    remoteContext.LibraryLogs.Add(remoteLog);
                    remoteContext.SaveChanges();
                }
            }
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
        // ~SyncPushEngine()
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
