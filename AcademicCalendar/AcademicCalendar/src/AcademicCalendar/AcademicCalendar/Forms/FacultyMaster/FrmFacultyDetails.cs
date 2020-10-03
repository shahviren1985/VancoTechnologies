using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AcademicCalendar.Models;

namespace AcademicCalendar.Forms.FacultyMaster
{
    public partial class FrmFacultyDetails : Form
    {
        CalContext ctx;

        private Faculty updatedFaculty;

        public FrmFacultyDetails(Faculty f)
        {
            InitializeComponent();

            if (f != null)
            {
                updatedFaculty = f;

                txtBoxName.Text = updatedFaculty.Name;
            }

            LoadSubjects().Wait();

            InitHeader();
        }

        private void InitHeader()
        {
            foreach (DataGridViewColumn c in dGVSubjects.Columns)
            {
                switch (c.Name.ToLower())
                {
                    case "id":
                        c.Visible = false;
                        c.ReadOnly = true;
                        break;
                    case "year":
                        c.Width = 100;
                        c.ReadOnly = true;
                        break;
                    case "term":
                        c.Width = 100;
                        c.ReadOnly = true;
                        break;
                    case "subjectname":
                        c.Width = 300;
                        c.ReadOnly = true;
                        break;
                    case "selected":
                        c.HeaderText = string.Empty;
                        break;
                    default:
                        c.Visible = false;
                        break;
                }
            }
        }

        private async Task LoadSubjects()
        {
            dGVSubjects.DataSource = null;

            if (ctx != null)
            {
                ctx.Dispose();
                ctx = null;
            }

            ctx = new CalContext();
            await ctx.Subjects.LoadAsync();
            await ctx.FacultySubjects.LoadAsync();
            await ctx.Terms.LoadAsync();
            await ctx.Years.LoadAsync();

            var dataSource = new List<SubjectMappingHelper>();

            foreach (var s in await ctx.Subjects.ToArrayAsync())
            {
                dataSource.Add(
                    new SubjectMappingHelper(
                        s,
                        updatedFaculty != null && ctx.FacultySubjects.Where(fst => fst.FacultyId == updatedFaculty.Id && fst.SubjectId == s.Id).FirstOrDefault() != null
                        )
                    );
            }

            dGVSubjects.DataSource = dataSource;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBoxName.Text))
            {
                MessageBox.Show("The Name field can not be empty.");
                return;
            }

            if (updatedFaculty == null)
            {
                var faculty = ctx.Faculties.Where(s => s.Name == txtBoxName.Text).FirstOrDefault();
                if (faculty != null)
                {
                    MessageBox.Show("The faculty already exists.");
                    return;
                }

                faculty = new Faculty()
                {
                    Name = txtBoxName.Text
                };

                foreach (DataGridViewRow s in dGVSubjects.Rows)
                {
                    var row = (SubjectMappingHelper)s.DataBoundItem;

                    if (row.Selected)
                    {
                        faculty.FacultySubjects.Add(new FacultySubject() { SubjectId = row.Id });
                    }
                }

                ctx.Faculties.Add(faculty);
            }
            else
            {
                var faculty = ctx.Faculties.Where(f => f.Name == txtBoxName.Text).FirstOrDefault();
                if (faculty != null && faculty.Id != updatedFaculty.Id)
                {
                    MessageBox.Show("The faculty doesn't exist.");
                    return;
                }
                else
                {
                    faculty = await ctx.Faculties.FindAsync(updatedFaculty.Id);
                }

                faculty.Name = txtBoxName.Text;

                foreach (DataGridViewRow s in dGVSubjects.Rows)
                {
                    var row = (SubjectMappingHelper)s.DataBoundItem;

                    if (row.Selected)
                    {
                        var mapObj = ctx.FacultySubjects.Where(fst => fst.FacultyId == updatedFaculty.Id && fst.SubjectId == row.Id).FirstOrDefault();

                        if (mapObj == null)
                        {
                            ctx.FacultySubjects.Add(new FacultySubject() { FacultyId = updatedFaculty.Id, SubjectId = row.Id });
                        }
                    }
                    else
                    {
                        var mapObj = ctx.FacultySubjects.Where(fst => fst.FacultyId == updatedFaculty.Id && fst.SubjectId == row.Id).FirstOrDefault();

                        if (mapObj != null)
                        {
                            ctx.FacultySubjects.Remove(mapObj);
                        }
                    }
                }
            }

            await ctx.SaveChangesAsync();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private class SubjectMappingHelper
        {
            public SubjectMappingHelper(Subject subject, bool selected)
            {
                Id = subject.Id;
                Year = subject.Term.Year.Code;
                Term = subject.Term.Name;
                SubjectName = subject.SubjectName;
                Selected = selected;
            }

            public int Id { get; set; }

            public string Year { get; set; }

            public string Term { get; set; }

            public string SubjectName { get; set; }

            public bool Selected { get; set; }
        }
    }
}
