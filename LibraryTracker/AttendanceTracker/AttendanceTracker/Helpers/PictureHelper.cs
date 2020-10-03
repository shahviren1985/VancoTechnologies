using System.IO;

namespace AttendanceTracker.Helpers
{
    public static class PictureHelper
    {
        public static string GetPicRemoteUri(string admissionYear, string photoPath)
        {
            string result = Config.RemotePhotoBaseUrl.TrimEnd().TrimEnd(new char[] { '/' });
            result += $"/{admissionYear}";

            if (!string.IsNullOrWhiteSpace(Config.RemotePhotoHardcodedPart))
            {
                result += $"/{Config.RemotePhotoHardcodedPart}";
            }

            result += $"/{photoPath}";

            return result;
        }

        public static string GetLocalPicPath(string admissionYear, string photoPath)
        {
            string targetDir = Path.Combine(Config.DownloadPhotoFolder, admissionYear);

            if (!Directory.Exists(targetDir))
            {
                Directory.CreateDirectory(targetDir);
            }

            return Path.Combine(targetDir, photoPath);
        }
    }
}
