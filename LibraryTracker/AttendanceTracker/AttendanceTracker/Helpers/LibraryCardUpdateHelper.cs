using System.Linq;
using AttendanceTracker.Models.Local;
using AttendanceTracker.Models.Remote;

namespace AttendanceTracker.Helpers
{
    public static class LibraryCardUpdateHelper
    {
        public static string[] GetCourseList()
        {
            using (var remoteContext = new RemoteContext())
            {
                return remoteContext.CourseDetails.OrderBy(c => c.Year).Select(c => c.Year).Distinct().ToArray();
            }
        }

        public static string[] GetSpecializationList()
        {
            using (var remoteContext = new RemoteContext())
            {
                return remoteContext.CourseDetails.OrderBy(c => c.Specialization).Select(c => c.Specialization).Distinct().ToArray();
            }
        }

        public static string[] GetTypeList()
        {
            using (var remoteContext = new RemoteContext())
            {
                var elements = remoteContext.CourseDetails.Select(c => c.CourseName).Distinct().ToArray();

                for (int i = 0; i < elements.Length; i++)
                {
                    if (string.IsNullOrWhiteSpace(elements[i]))
                    {
                        elements[i] = "Regular";
                    }
                }

                return elements;
            }
        }

        public static int?[] GetCourseIdsForFilters(string course, string specialization, string type)
        {
            if (type == "Regular")
            {
                type = "";
            }

            using (var remoteContext = new RemoteContext())
            {
                return remoteContext.CourseDetails
                    .Where(c => c.Year == course && c.Specialization == specialization && c.CourseName == type)
                    .Select(c => c.Id).Distinct()
                    .ToArray();
            }
        }

        public static Models.Remote.StudentDetail[] GetRemoteStudentsByCourseIds(int?[] courseIds)
        {
            using (var remoteContext = new RemoteContext())
            {
                return remoteContext.StudentDetails
                    .Where(s => courseIds.Contains(s.CourseId))
                    .ToArray();
            }
        }

        public static void UpdateRemote(Models.Remote.StudentDetail student)
        {
            using (var remoteContext = new RemoteContext())
            {
                var stud = remoteContext.StudentDetails.Where(s => s.UserId == student.UserId).First();
                stud.LibraryCardNo = student.LibraryCardNo;
                remoteContext.SaveChanges();
            }
        }

        public static void UpdateLocal(Models.Remote.StudentDetail student)
        {
            using (var localContext = new LocalContext())
            {
                var stud = localContext.StudentDetails.Where(s => s.CRN == student.UserId).First();
                stud.LibraryCardNo = student.LibraryCardNo;
                localContext.SaveChanges();
            }
        }
    }
}
