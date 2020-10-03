using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AcademicCalendar.Models;

namespace AcademicCalendar.Forms.HolidayMaster
{
    public partial class FrmHolidayMaster : Form
    {
        CalContext ctx;

        public FrmHolidayMaster()
        {
            InitializeComponent();

            LoadData().Wait();

            InitHeader();
        }

        private void InitHeader()
        {
            foreach (DataGridViewColumn c in dGVHolidays.Columns)
            {
                switch (c.Name.ToLower())
                {
                    case "id":
                        c.Visible = false;
                        break;
                    case "date":
                        c.Width = 160;
                        break;
                    case "name":
                        c.Width = 240;
                        break;
                }
            }
        }

        private async Task LoadData()
        {
            dGVHolidays.DataSource = null;

            if (ctx != null)
            {
                ctx.Dispose();
                ctx = null;
            }

            ctx = new CalContext();

            dGVHolidays.DataSource = await ctx.Holidays.OrderBy(h => h.Date).ToListAsync();

            InitHeader();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmHolidayDetails(null))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    await LoadData();
                }
            }
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (dGVHolidays.SelectedRows.Count < 1)
            {
                MessageBox.Show("Row must be selected to edit.");
                return;
            }

            using (var frm = new FrmHolidayDetails(dGVHolidays.SelectedRows[0].DataBoundItem as Holiday))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    await LoadData();
                }
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dGVHolidays.SelectedRows.Count < 1)
            {
                MessageBox.Show("Row must be selected to delete.");
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            var selectedItem = (Holiday)(dGVHolidays.SelectedRows[0].DataBoundItem);
            var holiday = await ctx.Holidays.FindAsync(selectedItem.Id);

            ctx.Holidays.Remove(holiday);

            await ctx.SaveChangesAsync();

            await LoadData();
        }
    }
}
