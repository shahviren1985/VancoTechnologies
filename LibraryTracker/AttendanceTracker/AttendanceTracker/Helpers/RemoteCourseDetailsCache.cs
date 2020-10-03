using System.Linq;
using AttendanceTracker.Models.Remote;

namespace AttendanceTracker.Helpers
{
    class RemoteCourseDetailsCache
    {
        CourseDetail[] courses;

        public RemoteCourseDetailsCache()
        {
            using (var ctx = new RemoteContext())
            {
                courses = ctx.CourseDetails.ToArray();
            }
        }

        public CourseDetail GetCourseById(int id)
        {
            return courses.Where(c => c.Id == id).FirstOrDefault();
        }
    }
}
