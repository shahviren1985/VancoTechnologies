using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using AcademicCalendar.Models;

namespace AcademicCalendar.Forms.Mappers
{
    public partial class FrmSubjectTimeslotMapDetails : Form
    {
        private Timeslot updatedTimeslot;

        public FrmSubjectTimeslotMapDetails(Timeslot t)
        {
            InitializeComponent();

            InitDays();
            InitSubjects();

            if (t != null)
            {
                updatedTimeslot = t;

                cmbBoxSubject.SelectedItem = t.Subject;
                cmbBoxDays.SelectedItem = t.Day;
                dTPFrom.Value = DateTime.Today + t.From;
                dTPTo.Value = DateTime.Today + t.To;
            }
        }

        public void InitDays()
        {
            using (var ctx = new CalContext())
            {
                cmbBoxDays.DataSource = ctx.Days.OrderBy(t => t.OrderOnWeek).ToArray();
            }
        }

        public void InitSubjects()
        {
            using (var ctx = new CalContext())
            {
                cmbBoxSubject.DataSource = ctx.Subjects
                    .Include("Term.Year")
                    .OrderBy(t => t.SubjectName)
                    .Select(s => new SubjectElement()
                    {
                        Id = s.Id,
                        Name = s.SubjectName,
                        Term = s.Term.Name,
                        Year = s.Term.Year.Code
                    })
                    .ToArray();
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbBoxSubject.SelectedIndex == -1)
            {
                MessageBox.Show("A subject must be selected.");
                return;
            }

            if (cmbBoxDays.SelectedIndex == -1)
            {
                MessageBox.Show("A day must be selected.");
                return;
            }

            var newFromValue = new TimeSpan(dTPFrom.Value.TimeOfDay.Hours, dTPFrom.Value.TimeOfDay.Minutes, 0);
            var newToValue = new TimeSpan(dTPTo.Value.TimeOfDay.Hours, dTPTo.Value.TimeOfDay.Minutes, 0);

            if (newFromValue >= newToValue)
            {
                MessageBox.Show("The 'from' value has to be lower than 'to' value!");
                return;
            }

            var dayId = (cmbBoxDays.SelectedItem as Models.Day).Id;

            using (var ctx = new CalContext())
            {
                var selectedDay = ctx.Days
                    .Include("CommonTimeslots")
                    .Where(d => d.Id == dayId)
                    .FirstOrDefault();

                if (selectedDay == null)
                {
                    MessageBox.Show("The selected day can not be found in the database.");
                    return;
                }

                foreach (var ct in selectedDay.CommonTimeslots)
                {
                    if ((newFromValue <= ct.From && newToValue > ct.From) ||
                        (newFromValue >= ct.From && newFromValue < ct.To))
                    {
                        MessageBox.Show($"The timeslot overlaps with the next common timeslot: {ct.Name} ({ct.From.ToString()} - {ct.To.ToString()})");
                        return;
                    }
                }


                if (updatedTimeslot == null)
                {
                    var timeslot = new Timeslot()
                    {
                        From = newFromValue,
                        To = newToValue,
                        DayId = (cmbBoxDays.SelectedItem as Models.Day).Id,
                        SubjectId = (cmbBoxSubject.SelectedItem as SubjectElement).Id
                    };

                    ctx.Timeslots.Add(timeslot);
                }
                else
                {
                    var timeslot = await ctx.Timeslots.FindAsync(updatedTimeslot.Id);
                    if (timeslot == null)
                    {
                        MessageBox.Show("The subject doesn't exist.");
                        return;
                    }

                    timeslot.From = newFromValue;
                    timeslot.To = newToValue;
                    timeslot.DayId = (cmbBoxDays.SelectedItem as Models.Day).Id;
                    timeslot.SubjectId = (cmbBoxSubject.SelectedItem as SubjectElement).Id;
                }

                await ctx.SaveChangesAsync();
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private class SubjectElement
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string Year { get; set; }

            public string Term { get; set; }

            public override string ToString()
            {
                return $"{Year}-{Term}-{Name}";
            }
        }
    }
}
