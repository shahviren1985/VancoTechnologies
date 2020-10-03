using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Windows.Forms;
using AcademicCalendar.Models;

namespace AcademicCalendar.Forms.Mappers
{
    public partial class FrmSubjectTimeslotMapList : Form
    {
        CalContext ctx;

        public FrmSubjectTimeslotMapList()
        {
            InitializeComponent();

            LoadData().Wait();
        }

        private void InitHeader()
        {
            foreach (DataGridViewColumn c in dGVTimeslots.Columns)
            {
                switch (c.Name.ToLower())
                {
                    case "year":
                        c.Width = 80;
                        break;
                    case "term":
                        c.Width = 80;
                        break;
                    case "subject":
                        c.Width = 240;
                        break;
                    case "day":
                        c.Width = 80;
                        break;
                    case "timeslot":
                        c.Width = 120;
                        break;
                    default:
                        c.Visible = false;
                        break;
                }
            }
        }

        private async Task LoadData()
        {
            dGVTimeslots.DataSource = null;

            if (ctx != null)
            {
                ctx.Dispose();
                ctx = null;
            }

            ctx = new CalContext();

            await ctx.Days.LoadAsync();
            await ctx.Terms.LoadAsync();
            await ctx.Years.LoadAsync();

            var dataSource = new List<TimeslotDisplayHelper>();

            foreach (var t in await ctx.Timeslots.Include("Subject").ToListAsync())
            {
                dataSource.Add(new TimeslotDisplayHelper(t));
            }

            dGVTimeslots.DataSource = dataSource;

            InitHeader();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmSubjectTimeslotMapDetails(null))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    await LoadData();
                }
            }
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (dGVTimeslots.SelectedRows.Count < 1)
            {
                MessageBox.Show("Row must be selected to edit.");
                return;
            }

            var editedId = (dGVTimeslots.SelectedRows[0].DataBoundItem as TimeslotDisplayHelper).Id;

            using (var frm = new FrmSubjectTimeslotMapDetails(await ctx.Timeslots.FindAsync(editedId)))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    await LoadData();
                }
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dGVTimeslots.SelectedRows.Count < 1)
            {
                MessageBox.Show("Row must be selected to delete.");
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            var selectedItem = (TimeslotDisplayHelper)(dGVTimeslots.SelectedRows[0].DataBoundItem);
            var timeslot = await ctx.Timeslots.FindAsync(selectedItem.Id);

            ctx.Timeslots.Remove(timeslot);

            await ctx.SaveChangesAsync();

            await LoadData();
        }

        class TimeslotDisplayHelper
        {
            public TimeslotDisplayHelper(Timeslot timeslot)
            {
                Id = timeslot.Id;
                Day = timeslot.Day.Name;
                Subject = timeslot.Subject.SubjectName;
                Term = timeslot.Subject.Term.ToString();
                Timeslot = timeslot.From.ToString() + " - " + timeslot.To.ToString();
                Year = timeslot.Subject.Term.Year.ToString();
            }

            public int Id { get; set; }

            public string Year { get; set; }

            public string Term { get; set; }

            public string Subject { get; set; }

            public string Day { get; set; }

            public string Timeslot { get; set; }
        }
    }
}
