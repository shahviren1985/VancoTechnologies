using System;
using System.Linq;
using System.Windows.Forms;
using AcademicCalendar.Models;

namespace AcademicCalendar.Forms.HolidayMaster
{
    public partial class FrmHolidayDetails : Form
    {
        private Holiday updatedHoliday;

        public FrmHolidayDetails(Holiday h)
        {
            InitializeComponent();

            if (h != null)
            {
                updatedHoliday = h;

                dTPDate.Value = h.Date;
                txtBoxName.Text = h.Name;
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBoxName.Text))
            {
                MessageBox.Show("The Name field can not be empty.");
                return;
            }

            using (var ctx = new CalContext())
            {
                if (updatedHoliday == null)
                {
                    var rows = ctx.Holidays.Where(h => h.Name == txtBoxName.Text).ToArray();
                    if (rows.Length > 0)
                    {
                        foreach (var h in rows)
                        {
                            if (h.Date.Equals(dTPDate.Value.Date))
                            {
                                MessageBox.Show("The holiday already exists.");
                                return;
                            }
                        }
                    }

                    var holiday = new Holiday()
                    {
                        Date = dTPDate.Value,
                        Name = txtBoxName.Text
                    };

                    ctx.Holidays.Add(holiday);
                }
                else
                {
                    var rows = ctx.Holidays.Where(h => h.Name == txtBoxName.Text).ToArray();
                    if (rows.Length > 0)
                    {
                        foreach (var h in rows)
                        {
                            if (h.Date.Equals(dTPDate.Value.Date) && h.Id != updatedHoliday.Id)
                            {
                                MessageBox.Show("Holiday with same data already exists.");
                                return;
                            }
                        }
                    }

                    var holiday = await ctx.Holidays.FindAsync(updatedHoliday.Id);

                    holiday.Date = dTPDate.Value;
                    holiday.Name = txtBoxName.Text;
                }

                await ctx.SaveChangesAsync();
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
