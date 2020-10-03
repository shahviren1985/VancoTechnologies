using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AcademicCalendar.Forms.CommonTimeslotMaster;
using AcademicCalendar.Forms.FacultyMaster;
using AcademicCalendar.Forms.HolidayMaster;
using AcademicCalendar.Forms.Mappers;
using AcademicCalendar.Forms.SubjectMaster;
using AcademicCalendar.Forms.TopicMaster;
using AcademicCalendar.Helpers;
using AcademicCalendar.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace AcademicCalendar
{
    public partial class FrmMain : Form
    {
        const string HtmlTemplate = @"
  <table style=""width:100%; border:1px solid black;"">
    <thead>
      <tr>
        <th colspan=""8"" style=""text-align: center;"">{HEADERTEXT}</th>
      </tr>
    </thead>
    <tbody>
      {TABLEROWS}
    </tbody>
  </table>";

        public FrmMain()
        {
            InitializeComponent();

            Config.Init();

            using (var ctx = new CalContext())
            {
                cmbBoxYear.DataSource = ctx.Years.Include("Terms").ToList();
            }

            cmbBoxYear.SelectedIndex = -1;
            cmbBoxTerm.SelectedIndex = -1;

            cmbBoxTerm.SelectedIndexChanged += cmbBoxTerm_SelectedIndexChanged;

            dGVCalendar.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        private void FrmMain_SizeChanged(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;

            dGVCalendar.Width = Width - (int)(3.5 * dGVCalendar.Location.X);
            dGVCalendar.Height = Height - 2 * dGVCalendar.Location.Y;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void facultyMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmFacultyMaster())
            {
                frm.ShowDialog();
            }
        }

        private void subjectMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmSubjectMaster())
            {
                frm.ShowDialog();
            }
        }

        private void holidayMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmHolidayMaster())
            {
                frm.ShowDialog();
            }
        }

        private void commonTimeslotMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmCommonTimeslotMaster())
            {
                frm.ShowDialog();
            }
        }

        private void subjectTimeslotMapperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmSubjectTimeslotMapList())
            {
                frm.ShowDialog();
            }
        }

        private void topicMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmTopicMaster())
            {
                frm.ShowDialog();
            }
        }

        private void cmbBoxYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            dGVCalendar.Rows.Clear();
            dGVCalendar.Columns.Clear();
            dGVCalendar.DataSource = null;

            cmbBoxWeeks.SelectedIndex = -1;
            cmbBoxWeeks.Items.Clear();

            cmbBoxTerm.SelectedIndex = -1;
            cmbBoxTerm.DataSource = null;

            if (cmbBoxYear.SelectedIndex == -1)
            {
                return;
            }

            cmbBoxTerm.DataSource = (cmbBoxYear.SelectedItem as Year).Terms;
        }

        private void cmbBoxWeeks_SelectedIndexChanged(object sender, EventArgs e)
        {
            dGVCalendar.Rows.Clear();
            dGVCalendar.Columns.Clear();
            dGVCalendar.DataSource = null;

            if (cmbBoxYear.SelectedIndex == -1 || cmbBoxWeeks.SelectedIndex == -1 || cmbBoxWeeks.SelectedIndex == -1)
            {
                return;
            }

            var weekData = cmbBoxWeeks.SelectedItem as WeekData;

            if (weekData == null)
            {
                return;
            }

            var colWidth = (dGVCalendar.Width / 8) - 5;

            for (int i = 0; i < 8; i++)
            {
                dGVCalendar.Columns.Add(i.ToString(), weekData.HeaderTexts[i]);
                dGVCalendar.Columns[i].Width = colWidth;
            }

            foreach (var row in weekData.Rows.OrderBy(r => r.Key))
            {
                dGVCalendar.Rows.Add(row.Value);
            }
        }

        private void cmbBoxTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbBoxWeeks.SelectedIndex = -1;
            cmbBoxWeeks.Items.Clear();

            if (cmbBoxTerm.SelectedIndex == -1)
            {
                return;
            }

            var termId = (cmbBoxTerm.SelectedItem as Term).Id;

            using (var ctx = new CalContext())
            {
                var commonTimeslots = ctx.CommonTimeslots.ToList();

                var subjects = ctx.Subjects
                    .Include("FacultySubjects")
                    .Include("FacultySubjects.Faculty")
                    .Include("FacultySubjects.Topics")
                    .Include("Timeslots")
                    .Include("Timeslots.Day")
                    .Where(s => s.TermId == termId)
                    .ToList();

                var date = Config.StartDate;
                var monday = date.AddDays(-(int)date.DayOfWeek + (int)DayOfWeek.Monday);

                List<Timeslot> timeslots = new List<Timeslot>();
                List<Holiday> holidays = ctx.Holidays.ToList();

                var topics = new Dictionary<int, Queue<TopicData>>();

                foreach (var s in subjects)
                {
                    foreach (var t in s.Timeslots)
                    {
                        timeslots.Add(t);
                    }

                    foreach (var fs in s.FacultySubjects)
                    {
                        var q = fs.Topics.OrderBy(top => top.LectureNo)
                            .Select(top => new TopicData() { Faculty = top.FacultySubject.Faculty.Name, Subject = top.FacultySubject.Subject.SubjectName, Topic = top.Name })
                            .ToList();

                        if (!topics.ContainsKey(fs.SubjectId))
                        {
                            topics.Add(fs.SubjectId, new Queue<TopicData>(q));
                        }
                    }
                }

                for (int i = 0; i < 52; i++)
                {
                    var week = new WeekData(i + 1, monday, commonTimeslots, timeslots, topics, holidays);
                    cmbBoxWeeks.Items.Add(week);
                    monday = monday.AddDays(7);
                }
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (cmbBoxWeeks.SelectedIndex == 0)
                return;

            cmbBoxWeeks.SelectedIndex--;
        }

        private void btnNextWeek_Click(object sender, EventArgs e)
        {
            if (cmbBoxWeeks.SelectedIndex == cmbBoxWeeks.Items.Count - 1)
                return;

            cmbBoxWeeks.SelectedIndex++;
        }

        private void btnPdfExport_Click(object sender, EventArgs e)
        {
            Byte[] bytes;

            using (var ms = new MemoryStream())
            {
                using (var doc = new Document(PageSize.A4, 7f, 5f, 5f, 0f))
                {
                    using (var writer = PdfWriter.GetInstance(doc, ms))
                    {
                        doc.Open();

                        var calendarHtml = GetCalendarHtml();

                        using (var htmlWorker = new iTextSharp.text.html.simpleparser.HTMLWorker(doc))
                        {
                            using (var sr = new StringReader(calendarHtml))
                            {
                                htmlWorker.Parse(sr);
                            }
                        }

                        doc.Close();
                    }
                }

                bytes = ms.ToArray();
            }

            File.WriteAllBytes("calendar.pdf", bytes);
        }

        private string GetCalendarHtml()
        {
            var resultHtml = HtmlTemplate.Replace("{HEADERTEXT}", $"Academic Calendar For {cmbBoxTerm.SelectedItem.ToString()} {cmbBoxYear.SelectedItem.ToString()}, {((WeekData)(cmbBoxWeeks.SelectedItem)).ToCalendarHeaderString()}");

            foreach (DataGridViewRow r in dGVCalendar.Rows)
            {

            }

            return resultHtml;
        }
    }
}
