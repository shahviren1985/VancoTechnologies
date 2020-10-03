using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using AcademicCalendar.Models;
using Day = AcademicCalendar.Models.Day;

namespace AcademicCalendar.Forms.CommonTimeslotMaster
{
    public partial class FrmCommonTimeslotDetails : Form
    {
        CalContext ctx;

        private CommonTimeslot updatedCommonTimeslot;

        public FrmCommonTimeslotDetails(CommonTimeslot c)
        {
            InitializeComponent();

            ctx = new CalContext();

            ctx.Days.Load();

            chckdLBDays.Items.AddRange(ctx.Days.OrderBy(d => d.OrderOnWeek).ToArray());

            if (c != null)
            {
                updatedCommonTimeslot = c;

                txtBoxName.Text = c.Name;
                dTPFrom.Value = DateTime.Today + c.From;
                dTPTo.Value = DateTime.Today + c.To;

                for (var i = 0; i < chckdLBDays.Items.Count; i++)
                {
                    if (c.Days.Contains((Day)chckdLBDays.Items[i]))
                    {
                        chckdLBDays.SetItemChecked(i, true);
                    }
                }
            }
        }

        async void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBoxName.Text))
            {
                MessageBox.Show("The Name field can not be empty.");
                return;
            }

            var newFromValue = new TimeSpan(dTPFrom.Value.TimeOfDay.Hours, dTPFrom.Value.TimeOfDay.Minutes, 0);
            var newToValue = new TimeSpan(dTPTo.Value.TimeOfDay.Hours, dTPTo.Value.TimeOfDay.Minutes, 0);

            if (newFromValue >= newToValue)
            {
                MessageBox.Show("The 'from' value has to be lower than 'to' value!");
                return;
            }

            if (updatedCommonTimeslot == null)
            {
                var commonTimeslot = ctx.CommonTimeslots.Where(c => c.Name == txtBoxName.Text).FirstOrDefault();
                if (commonTimeslot != null)
                {
                    MessageBox.Show("The common timeslot already exists.");
                    return;
                }

                commonTimeslot = new CommonTimeslot()
                {
                    Name = txtBoxName.Text,
                    From = newFromValue,
                    To = newToValue
                };

                foreach (var i in chckdLBDays.CheckedItems)
                {
                    var day = (Day)i;
                    commonTimeslot.Days.Add(ctx.Days.Find(day.Id));
                }

                ctx.CommonTimeslots.Add(commonTimeslot);
            }
            else
            {
                var commonTimeslot = ctx.CommonTimeslots.Where(c => c.Name == txtBoxName.Text).FirstOrDefault();
                if (commonTimeslot != null && commonTimeslot.Id != updatedCommonTimeslot.Id)
                {
                    MessageBox.Show("The common timeslot doesn't exist.");
                    return;
                }
                else
                {
                    commonTimeslot = await ctx.CommonTimeslots.FindAsync(updatedCommonTimeslot.Id);
                }

                commonTimeslot.Name = txtBoxName.Text;
                commonTimeslot.From = newFromValue;
                commonTimeslot.To = newToValue;

                commonTimeslot.Days.Clear();

                foreach (var i in chckdLBDays.CheckedItems)
                {
                    var day = (Day)i;
                    commonTimeslot.Days.Add(ctx.Days.Find(day.Id));
                }
            }

            await ctx.SaveChangesAsync();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
