using System.Collections.Generic;
using System.Linq;
using AttendanceTracker.Helpers;
using AttendanceTracker.Models.Local;
using AttendanceTracker.Models.Remote;

namespace AttendanceTracker
{
    /// <summary>
    /// Handles the staff and student data syncing from the remote server.
    /// </summary>
    class SyncPullEngine
    {
        Dictionary<int, Models.Remote.CourseDetail> courseDetails = new Dictionary<int, Models.Remote.CourseDetail>();

        public SyncPullEngine()
        {
            using (var remoteContext = new RemoteContext())
            {
                var details = remoteContext.CourseDetails.ToArray();

                foreach (var detail in details)
                {
                    // Id column is NULLABLE in the remote course_details table.
                    // I think it will be never NULL but if it does then it is not valid for us because we can not join to any students so we skip it.
                    if (detail.Id != null)
                    {
                        courseDetails.Add(detail.Id ?? -1, detail);
                    }
                }
            }
        }

        public void PullData()
        {
            SyncCourseDetails();
            SyncStaffDetails();
            SyncStudentDetails();
            SyncLogHelper.AddSyncLog("PULL");
        }

        #region Courses

        private void SyncCourseDetails()
        {
            using (var localContext = new LocalContext())
            {
                foreach (var remoteRecord in courseDetails)
                {
                    var localRecord = localContext.CourseDetails.Where(c => c.Id == remoteRecord.Key).FirstOrDefault();

                    if (localRecord == null)
                    {
                        localRecord = new Models.Local.CourseDetail()
                        {
                            CourseName = remoteRecord.Value.CourseName,
                            CourseType = remoteRecord.Value.CourseType,
                            Id = remoteRecord.Key,
                            ShortForm = remoteRecord.Value.ShortForm,
                            Specialization = remoteRecord.Value.Specialization,
                            Status = remoteRecord.Value.Status,
                            Year = remoteRecord.Value.Year
                        };

                        localContext.CourseDetails.Add(localRecord);
                    }
                    else
                    {
                        localRecord.CourseName = remoteRecord.Value.CourseName;
                        localRecord.CourseType = remoteRecord.Value.CourseType;
                        localRecord.ShortForm = remoteRecord.Value.ShortForm;
                        localRecord.Specialization = remoteRecord.Value.Specialization;
                        localRecord.Status = remoteRecord.Value.Status;
                        localRecord.Year = remoteRecord.Value.Year;
                    }

                    localContext.SaveChanges();
                }
            }
        }

        #endregion

        #region Staff

        private void SyncStaffDetails()
        {
            bool finished = false;

            int page = 0;

            while (!finished)
            {
                var sd = GetRemoteStaffDetailsBatch(page);
                UpdateLocalStaffDetails(sd);
                page++;

                if (sd.Count() == 0)
                {
                    finished = true;
                }
            }
        }

        private IEnumerable<Models.Remote.StaffDetail> GetRemoteStaffDetailsBatch(int page)
        {
            using (var remoteContext = new RemoteContext())
            {
                var result = remoteContext.StaffDetails
                    .OrderBy(s => s.Id)
                    .Skip(page * Config.SyncBatchSize)
                    .Take(Config.SyncBatchSize)
                    .ToArray();

                return result;
            }
        }

        private void UpdateLocalStaffDetails(IEnumerable<Models.Remote.StaffDetail> remoteStaffDetails)
        {
            if (remoteStaffDetails == null || remoteStaffDetails.Count() == 0) { return; }

            using (var localContext = new LocalContext())
            {
                foreach (var nsd in remoteStaffDetails)
                {
                    var localStaffDetail = localContext.StaffDetails.Where(sd => sd.CRN == nsd.Id).FirstOrDefault();

                    if (localStaffDetail == null)
                    {
                        localStaffDetail = new Models.Local.StaffDetail()
                        {
                            CRN = nsd.Id,
                            FirstName = nsd.FirstName,
                            LastName = nsd.LastName,
                            EmployeeCode = nsd.EmployeeCode,
                            Department = nsd.Department
                        };

                        localContext.StaffDetails.Add(localStaffDetail);
                    }
                    else
                    {
                        localStaffDetail.FirstName = nsd.FirstName;
                        localStaffDetail.LastName = nsd.LastName;
                        localStaffDetail.EmployeeCode = nsd.EmployeeCode;
                        localStaffDetail.Department = nsd.Department;
                    }

                    localContext.SaveChanges();
                }
            }
        }

        #endregion

        #region Student

        private void SyncStudentDetails()
        {
            bool finished = false;

            int page = 0;

            while (!finished)
            {
                var sd = GetRemoteStudentDetailsBatch(page);
                UpdateLocalStudentDetails(sd);
                page++;

                if (sd.Count() == 0)
                {
                    finished = true;
                }
            }
        }

        private IEnumerable<Models.Remote.StudentDetail> GetRemoteStudentDetailsBatch(int page)
        {
            using (var remoteContext = new RemoteContext())
            {
                return remoteContext.StudentDetails
                    .OrderBy(s => s.Id)
                    .Skip(page * Config.SyncBatchSize)
                    .Take(Config.SyncBatchSize).ToArray();
            }
        }

        private void UpdateLocalStudentDetails(IEnumerable<Models.Remote.StudentDetail> remoteStudentDetails)
        {
            if (remoteStudentDetails == null || remoteStudentDetails.Count() == 0) { return; }

            using (var localContext = new LocalContext())
            {
                foreach (var nsd in remoteStudentDetails)
                {
                    var localStudentDetail = localContext.StudentDetails.Where(sd => sd.CRN == nsd.UserId).FirstOrDefault();

                    var courseDetailsYear = "";
                    var courseDetailsCourseName = "";

                    if (courseDetails.ContainsKey(nsd.CourseId))
                    {
                        courseDetailsYear = courseDetails[nsd.CourseId].Year;
                        courseDetailsCourseName = courseDetails[nsd.CourseId].CourseName;
                    }

                    if (localStudentDetail == null)
                    {
                        localStudentDetail = new Models.Local.StudentDetail()
                        {
                            CRN = nsd.UserId,
                            FirstName = nsd.FirstName,
                            LastName = nsd.LastName,
                            CourseDetailsYear = courseDetailsYear,
                            CourseDetailsCourseName = courseDetailsCourseName,
                            CourseName = nsd.CourseName,
                            Specialization = nsd.Specialization,
                            RollNumber = nsd.RollNumber,
                            Division = nsd.Division,
                            PrnNumber = nsd.PrnNumber,
                            PhotoPath = nsd.PhotoPath,
                            LibraryCardNo = nsd.LibraryCardNo,
                            AdmissionYear = nsd.AdmissionYear
                        };

                        localContext.StudentDetails.Add(localStudentDetail);
                    }
                    else
                    {
                        localStudentDetail.FirstName = nsd.FirstName;
                        localStudentDetail.LastName = nsd.LastName;
                        localStudentDetail.CourseDetailsYear = courseDetailsYear;
                        localStudentDetail.CourseDetailsCourseName = courseDetailsCourseName;
                        localStudentDetail.CourseName = nsd.CourseName;
                        localStudentDetail.Specialization = nsd.Specialization;
                        localStudentDetail.RollNumber = nsd.RollNumber;
                        localStudentDetail.Division = nsd.Division;
                        localStudentDetail.PrnNumber = nsd.PrnNumber;
                        localStudentDetail.PhotoPath = nsd.PhotoPath;
                        localStudentDetail.LibraryCardNo = nsd.LibraryCardNo;
                        localStudentDetail.AdmissionYear = nsd.AdmissionYear;
                    }

                    localContext.SaveChanges();
                }
            }
        }

        #endregion
    }
}
