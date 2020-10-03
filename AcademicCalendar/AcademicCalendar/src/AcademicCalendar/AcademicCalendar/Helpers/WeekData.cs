using AcademicCalendar.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;

namespace AcademicCalendar.Helpers
{
    class WeekData
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int WeekNo { get; set; }

        public List<CommonTimeslot> CommonTimeslots { get; set; } = new List<CommonTimeslot>();

        public List<Timeslot> Timeslots { get; set; } = new List<Timeslot>();

        public string[] HeaderTexts { get; set; } = new string[8];

        public Dictionary<string, string[]> Rows { get; set; } = new Dictionary<string, string[]>();

        public WeekData(int weekNo, DateTime startDate, List<CommonTimeslot> commonTimeslots, List<Timeslot> timeslots, Dictionary<int, Queue<TopicData>> topics, List<Holiday> holidays)
        {
            WeekNo = weekNo;
            StartDate = startDate;
            EndDate = StartDate.AddDays(6);

            CommonTimeslots = commonTimeslots;
            Timeslots = timeslots.OrderBy(t => t.Day.OrderOnWeek).ToList();

            HeaderTexts[0] = "Time/Day";

            string[] holidayData = new string[7];

            var skippedDays = new List<int>();

            // Adding column headers (day names)
            for (int i = 0; i < 7; i++)
            {
                var dayDate = StartDate.AddDays(i);
                var dayNoInTerm = (dayDate - Config.StartDate).TotalDays + 1;

                if (dayNoInTerm < 1)
                {
                    skippedDays.Add(i + 1);
                }

                foreach (var h in holidays)
                {
                    if (h.Date == dayDate)
                    {
                        holidayData[i] = h.Name;
                        break;
                    }
                    else
                    {
                        holidayData[i] = null;
                    }
                }

                HeaderTexts[i + 1] = dayNoInTerm > 0 ? $"Day {dayNoInTerm}, {dayDate.DayOfWeek}{Environment.NewLine}{dayDate.ToString("yyyy-MM-dd")}" : string.Empty;
            }

            // Adding common timeslot data
            foreach (var c in CommonTimeslots)
            {
                if (!Rows.ContainsKey(c.From + " to" + c.To))
                {
                    Rows[c.From + " to" + c.To] = new string[8];
                    Rows[c.From + " to" + c.To][0] = c.From + " to" + c.To;
                }

                foreach (var d in c.Days)
                {
                    if (!skippedDays.Contains(d.OrderOnWeek + 1))
                    {
                        if (holidayData[d.OrderOnWeek] != null)
                        {
                            // In this case it is a holiday
                            Rows[c.From + " to" + c.To][d.OrderOnWeek + 1] = holidayData[d.OrderOnWeek];
                        }
                        else
                        {
                            Rows[c.From + " to" + c.To][d.OrderOnWeek + 1] = c.Name;
                        }
                    }
                }
            }

            // Adding timeslot data
            foreach (var t in Timeslots)
            {
                if (!Rows.ContainsKey(t.From + " to" + t.To))
                {
                    Rows[t.From + " to" + t.To] = new string[8];
                    Rows[t.From + " to" + t.To][0] = t.From + " to" + t.To;
                }

                if (!skippedDays.Contains(t.Day.OrderOnWeek + 1))
                {
                    if (holidayData[t.Day.OrderOnWeek] != null)
                    {
                        // In this case it is a holiday
                        Rows[t.From + " to" + t.To][t.Day.OrderOnWeek + 1] = holidayData[t.Day.OrderOnWeek];
                    }
                    else
                    {
                        if (topics.ContainsKey(t.SubjectId) && topics[t.SubjectId].Count > 0)
                        {
                            Rows[t.From + " to" + t.To][t.Day.OrderOnWeek + 1] = $"{topics[t.SubjectId].Dequeue()}";
                        }
                    }
                }
            }
        }

        public override string ToString()
        {
            return $"Week {WeekNo} ({StartDate.ToString("yyyy-MM-dd")} to {EndDate.ToString("yyyy-MM-dd")})";
        }

        public string ToCalendarHeaderString()
        {
            return $"From {StartDate.ToString("yyyy-MM-dd")} to {EndDate.ToString("yyyy-MM-dd")}";
        }
    }
}
