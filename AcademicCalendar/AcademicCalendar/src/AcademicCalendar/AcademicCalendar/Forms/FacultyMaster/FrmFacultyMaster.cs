using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Windows.Forms;
using AcademicCalendar.Models;

namespace AcademicCalendar.Forms.FacultyMaster
{
    public partial class FrmFacultyMaster : Form
    {
        CalContext ctx;

        public FrmFacultyMaster()
        {
            InitializeComponent();

            LoadData().Wait();
        }

        private void InitHeader()
        {
            foreach (DataGridViewColumn c in dGVFaculties.Columns)
            {
                switch (c.Name.ToLower())
                {
                    case "id":
                        c.Width = 75;
                        break;
                    case "faculty":
                        c.Width = 250;
                        break;
                    case "subjectid":
                        c.Visible = false;
                        break;
                    case "subject":
                        c.Width = 250;
                        break;
                }
            }
        }

        private async Task LoadData()
        {
            dGVFaculties.DataSource = null;

            if (ctx != null)
            {
                ctx.Dispose();
                ctx = null;
            }

            ctx = new CalContext();

            await ctx.Subjects.LoadAsync();
            await ctx.FacultySubjects.LoadAsync();
            await ctx.Years.LoadAsync();
            await ctx.Terms.LoadAsync();

            var dataSource = new List<FacultyDisplayHelper>();

            foreach (var f in await ctx.Faculties.ToListAsync())
            {
                if (f.FacultySubjects.Count > 0)
                {
                    foreach (var s in f.FacultySubjects)
                    {
                        dataSource.Add(new FacultyDisplayHelper(f, s.Subject));
                    }
                }
                else
                {
                    dataSource.Add(new FacultyDisplayHelper(f, null));
                }
            }

            dGVFaculties.DataSource = dataSource;

            InitHeader();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmFacultyDetails(null))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    await LoadData();
                }
            }
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (dGVFaculties.SelectedRows.Count < 1)
            {
                MessageBox.Show("Row must be selected to edit.");
                return;
            }

            var idToEdit = ((FacultyDisplayHelper)dGVFaculties.SelectedRows[0].DataBoundItem).Id;

            using (var frm = new FrmFacultyDetails(await ctx.Faculties.FindAsync(idToEdit)))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    await LoadData();
                }
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dGVFaculties.SelectedRows.Count < 1)
            {
                MessageBox.Show("Row must be selected to delete.");
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            var selectedItem = (Faculty)(dGVFaculties.SelectedRows[0].DataBoundItem);
            var faculty = await ctx.Faculties.FindAsync(selectedItem.Id);

            ctx.Faculties.Remove(faculty);

            await ctx.SaveChangesAsync();

            await LoadData();
        }

        class FacultyDisplayHelper
        {
            public FacultyDisplayHelper(Faculty faculty, Subject subject)
            {
                Id = faculty.Id;
                Faculty = faculty.ToString();

                if (subject == null)
                {
                    Subject = "-";
                }
                else
                {
                    Subject = $"{subject.Term.Year.Code}-{subject.Term}-{subject.SubjectName}";
                }
            }

            public int Id { get; set; }

            public string Faculty { get; set; }

            public string Subject { get; set; }
        }
    }
}
