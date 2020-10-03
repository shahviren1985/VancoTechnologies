using System.Configuration;

namespace AttendanceTracker
{
    static class Config
    {
        public static int AutoCheckoutMinutesToAdd { get; private set; }

        public static bool EnableLibraryCardUpdate { get; private set; }

        public static string LocalPhotoFolder { get; private set; }

        public static string DownloadPhotoFolder { get; private set; }

        public static string RemotePhotoBaseUrl { get; private set; }

        public static string RemotePhotoHardcodedPart { get; private set; }

        public static int StudentDetailsAutoCloseSec { get; private set; }

        public static int SyncBatchSize { get; private set; }

        public static bool SyncPushLogging { get; private set; }

        public static int SyncPushIntervalMinutes { get; private set; }

        public static void Init()
        {
            var appSettings = ConfigurationManager.AppSettings;

            try
            {
                EnableLibraryCardUpdate = bool.Parse(appSettings["EnableLibraryCardUpdate"].ToString());
            }
            catch
            {
                EnableLibraryCardUpdate = true;
            }

            LocalPhotoFolder = appSettings["LocalPhotoFolder"] ?? "Pictures";
            RemotePhotoBaseUrl = appSettings["RemotePhotoBaseUrl"];
            DownloadPhotoFolder = appSettings["DownloadPhotoFolder"];
            RemotePhotoHardcodedPart = appSettings["RemotePhotoHardcodedPart"];

            try
            {
                AutoCheckoutMinutesToAdd = int.Parse(appSettings["AutoCheckoutMinutesToAdd"].ToString());
            }
            catch
            {
                AutoCheckoutMinutesToAdd = 60;
            }

            try
            {
                StudentDetailsAutoCloseSec = int.Parse(appSettings["StudentDetailsAutoCloseSec"].ToString());
            }
            catch
            {
                StudentDetailsAutoCloseSec = 500;
            }

            try
            {
                SyncBatchSize = int.Parse(appSettings["SyncBatchSize"].ToString());
            }
            catch
            {
                SyncBatchSize = 500;
            }

            try
            {
                SyncPushLogging = bool.Parse(appSettings["SyncPushLogging"].ToString());
            }
            catch
            {
                SyncPushLogging = true;
            }

            try
            {
                SyncPushIntervalMinutes = int.Parse(appSettings["SyncPushIntervalMinutes"].ToString());
            }
            catch
            {
                SyncPushIntervalMinutes = 500;
            }
        }
    }
}
