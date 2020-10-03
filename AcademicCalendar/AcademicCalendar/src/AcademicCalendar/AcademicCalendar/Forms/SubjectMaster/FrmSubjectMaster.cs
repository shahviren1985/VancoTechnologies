using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AcademicCalendar.Models;

namespace AcademicCalendar.Forms.SubjectMaster
{
    public partial class FrmSubjectMaster : Form
    {
        CalContext ctx;

        public FrmSubjectMaster()
        {
            InitializeComponent();

            ctx = new CalContext();
            cmbBoxYear.DataSource = ctx.Years.Include("Terms").ToList();

            LoadData().Wait();
        }

        private void InitHeader()
        {
            foreach (DataGridViewColumn c in dGVSubjects.Columns)
            {
                switch (c.Name.ToLower())
                {
                    case "id":
                        c.Width = 100;
                        break;
                    case "year":
                        c.Width = 100;
                        break;
                    case "term":
                        c.Width = 100;
                        break;
                    case "subjectname":
                        c.Width = 300;
                        break;
                    case "faculties":
                        c.Visible = false;
                        break;
                    default:
                        c.Visible = false;
                        break;
                }
            }
        }

        private async Task LoadData()
        {
            dGVSubjects.DataSource = null;

            if (ctx != null)
            {
                ctx.Dispose();
                ctx = null;
            }

            ctx = new CalContext();

            if (chBoxFilterActive.Checked)
            {
                if (cmbBoxTerm.SelectedIndex != -1)
                {
                    dGVSubjects.DataSource = ctx.Terms
                        .Include("Subjects")
                        .Include("Year")
                        .Where(t => t.Id == ((Term)cmbBoxTerm.SelectedItem).Id)
                        .FirstOrDefault()?.Subjects;
                }
                else if (cmbBoxYear.SelectedIndex != -1)
                {
                    var terms = await ctx.Terms
                        .Include("Subjects")
                        .Include("Year")
                        .Where(t => t.YearId == ((Year)cmbBoxYear.SelectedItem).Id)
                        .ToListAsync();

                    var dataSource = new List<Subject>();

                    foreach (var t in terms)
                    {
                        dataSource.AddRange(t.Subjects);
                    }

                    dGVSubjects.DataSource = dataSource;
                }
            }
            else
            {
                dGVSubjects.DataSource = await ctx.Subjects
                .Include("Term.Year")
                .ToListAsync();
            }

            InitHeader();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmSubjectDetails(null))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    await LoadData();
                }
            }
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (dGVSubjects.SelectedRows.Count < 1)
            {
                MessageBox.Show("Row must be selected to edit.");
                return;
            }

            using (var frm = new FrmSubjectDetails(dGVSubjects.SelectedRows[0].DataBoundItem as Subject))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    await LoadData();
                }
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dGVSubjects.SelectedRows.Count < 1)
            {
                MessageBox.Show("Row must be selected to delete.");
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            var selectedItem = (Subject)(dGVSubjects.SelectedRows[0].DataBoundItem);
            var subject = await ctx.Subjects.FindAsync(selectedItem.Id);

            ctx.Subjects.Remove(subject);

            await ctx.SaveChangesAsync();

            await LoadData();
        }

        private async void btnShowResult_Click(object sender, EventArgs e)
        {
            await LoadData();
        }

        private void cmbBoxYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            dGVSubjects.DataSource = null;

            if (cmbBoxYear.SelectedIndex != -1)
            {
                cmbBoxTerm.DataSource = null;
                cmbBoxTerm.DataSource = ((Year)cmbBoxYear.SelectedItem).Terms;
            }

            cmbBoxTerm.SelectedIndex = -1;
        }

        private async void chBoxFilterActive_CheckedChanged(object sender, EventArgs e)
        {
            btnShowResult.Enabled = chBoxFilterActive.Checked;
            cmbBoxYear.Enabled = chBoxFilterActive.Checked;
            cmbBoxTerm.Enabled = chBoxFilterActive.Checked;

            await LoadData();
        }
    }
}
