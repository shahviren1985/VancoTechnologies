using AcademicCalendar.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AcademicCalendar.Forms.CommonTimeslotMaster
{
    public partial class FrmCommonTimeslotMaster : Form
    {
        CalContext ctx;

        public FrmCommonTimeslotMaster()
        {
            InitializeComponent();

            LoadData().Wait();

            InitHeader();
        }

        private void InitHeader()
        {
            foreach (DataGridViewColumn c in dGVCommonTimeslots.Columns)
            {
                switch (c.Name.ToLower())
                {
                    case "id":
                        c.Width = 75;
                        break;
                    case "days":
                        c.Width = 270;
                        break;
                    case "timeslot":
                        c.Width = 270;
                        break;
                    case "name":
                        c.Width = 270;
                        break;
                }
            }
        }

        private async Task LoadData()
        {
            dGVCommonTimeslots.DataSource = null;

            if (ctx != null)
            {
                ctx.Dispose();
                ctx = null;
            }

            ctx = new CalContext();

            await ctx.Days.LoadAsync();

            var dataSource = new List<CommonTimeslotDisplayHelper>();

            foreach (var c in await ctx.CommonTimeslots.ToListAsync())
            {
                dataSource.Add(new CommonTimeslotDisplayHelper(c));
            }

            dGVCommonTimeslots.DataSource = dataSource;

            InitHeader();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmCommonTimeslotDetails(null))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    await LoadData();
                }
            }
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (dGVCommonTimeslots.SelectedRows.Count < 1)
            {
                MessageBox.Show("Row must be selected to edit.");
                return;
            }

            var idToEdit = ((CommonTimeslotDisplayHelper)dGVCommonTimeslots.SelectedRows[0].DataBoundItem).Id;

            using (var frm = new FrmCommonTimeslotDetails(await ctx.CommonTimeslots.FindAsync(idToEdit)))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    await LoadData();
                }
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dGVCommonTimeslots.SelectedRows.Count < 1)
            {
                MessageBox.Show("Row must be selected to delete.");
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            var selectedItem = (CommonTimeslotDisplayHelper)(dGVCommonTimeslots.SelectedRows[0].DataBoundItem);
            var commonTimeslotDisplayHelper = await ctx.CommonTimeslots.FindAsync(selectedItem.Id);

            ctx.CommonTimeslots.Remove(commonTimeslotDisplayHelper);

            await ctx.SaveChangesAsync();

            await LoadData();
        }

        class CommonTimeslotDisplayHelper
        {
            public CommonTimeslotDisplayHelper(CommonTimeslot commonTimeslot)
            {
                Id = commonTimeslot.Id;
                Days = string.Join(", ", commonTimeslot.Days.Select(d => d.Name.Substring(0,3)).ToArray());
                Timeslot = commonTimeslot.From.ToString() + " - " + commonTimeslot.To.ToString();
                Name = commonTimeslot.Name;
            }

            public int Id { get; set; }

            public string Days { get; set; }

            public string Timeslot { get; set; }

            public string Name { get; set; }
        }
    }
}
