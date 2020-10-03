using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AttendanceTracker.Models.Local;

namespace AttendanceTracker.Helpers
{
    public static class ReportHelper
    {
        public static async Task<LibraryLog[]> GetLibraryLogsAsync(DateTime fromDate, DateTime toDate)
        {
            toDate = toDate.AddDays(1);

            using (var ctx = new LocalContext())
            {
                return await ctx.LibraryLogs
                        .Where(l => l.Date > fromDate && l.Date < toDate)
                        .OrderByDescending(l => l.Id)
                        .ToArrayAsync();
            }
        }

        public static async Task<ReportDataObjectLibraryEntry[]> GetReportDataObjectLibraryEntryAsync(DateTime fromDate, DateTime toDate)
        {
            toDate = toDate.AddDays(1);

            using (var ctx = new LocalContext())
            {
                return await (from l in ctx.LibraryLogs
                              join s in ctx.StudentDetails
                              on l.CRN equals s.CRN
                              where l.Date > fromDate && l.Date < toDate
                              orderby l.Id descending
                              select new ReportDataObjectLibraryEntry()
                              {
                                  CRN = l.CRN,
                                  Id = l.Id,
                                  In = l.InTime,
                                  Name = s.FirstName + " " + s.LastName,
                                  Out = l.OutTime
                              })
                              .ToArrayAsync();
            }
        }

        public static async Task<List<ReportDataObjectHoursSpent>> GetReportDataObjectHoursSpentAsync(DateTime fromDate, DateTime toDate)
        {
            List<ReportDataObjectHoursSpent> result = new List<ReportDataObjectHoursSpent>();

            toDate = toDate.AddDays(1);

            using (var ctx = new LocalContext())
            {
                var sumDict = new Dictionary<string, double>();

                var crns = await ctx.LibraryLogs
                        .Where(l => l.Date > fromDate && l.Date < toDate && l.OutTime != null)
                        .Select(l => l.CRN)
                        .Distinct()
                        .ToArrayAsync();

                var students = await ctx.StudentDetails
                    .Where(s => crns.Contains(s.CRN))
                    .OrderBy(s => s.FirstName)
                    .Select(s => new {
                        CRN = s.CRN,
                        Name = s.FirstName + " " + s.LastName,
                        Specialization = s.Specialization
                    })
                    .ToArrayAsync();

                var logs = await ctx.LibraryLogs
                        .Where(l => l.Date > fromDate && l.Date < toDate && l.OutTime != null)
                        .ToArrayAsync();

                foreach (var l in logs)
                {
                    if (sumDict.ContainsKey(l.CRN))
                    {
                        sumDict[l.CRN] += ((l.OutTime ?? DateTime.Now) - l.InTime).TotalHours;
                    }
                    else
                    {
                        sumDict.Add(l.CRN, ((l.OutTime ?? DateTime.Now) - l.InTime).TotalHours);
                    }
                }

                foreach (var s in students)
                {
                    result.Add(new ReportDataObjectHoursSpent()
                    {
                        CRN = s.CRN,
                        HoursSpent = sumDict[s.CRN],
                        Month = DateTime.Now.Month.ToString(),
                        Name = s.Name,
                        Specialization = s.Specialization
                    });
                }

                return result;
            }
        }
    }
}
