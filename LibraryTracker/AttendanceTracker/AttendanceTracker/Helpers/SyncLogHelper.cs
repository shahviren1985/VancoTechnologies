using System;
using AttendanceTracker.Models.Local;

namespace AttendanceTracker.Helpers
{
    public static class SyncLogHelper
    {
        public static void AddSyncLog(string syncType)
        {
            if (!Config.SyncPushLogging && syncType == "PUSH") { return; }

            using (var localContext = new LocalContext())
            {
                localContext
                    .Synchronizations
                    .Add(new Synchronization()
                    {
                        SyncedAt = DateTime.Now,
                        SyncType = syncType
                    });

                localContext.SaveChanges();
            }
        }
    }
}
