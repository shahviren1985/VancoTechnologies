using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using AcademicCalendar.Models;

namespace AcademicCalendar.Forms.SubjectMaster
{
    public partial class FrmSubjectDetails : Form
    {
        Subject updatedSubject;

        public FrmSubjectDetails(Subject s)
        {
            InitializeComponent();

            using (var ctx = new CalContext())
            {
                cmbBoxYear.DataSource = ctx.Years.Include("Terms").ToList();
            }

            if (s != null)
            {
                updatedSubject = s;

                cmbBoxYear.SelectedItem = updatedSubject.Term.Year;
                cmbBoxTerm.SelectedItem = updatedSubject.Term;
                txtBoxSubject.Text = updatedSubject.SubjectName;
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBoxSubject.Text))
            {
                MessageBox.Show("The Subject field can not be empty.");
                return;
            }

            if (cmbBoxYear.SelectedIndex == -1 || cmbBoxTerm.SelectedIndex == -1)
            {
                MessageBox.Show("Please select year and term before save!");
                return;
            }

            using (var ctx = new CalContext())
            {
                if (updatedSubject == null)
                {
                    var subject = ctx.Subjects.Where(s => s.SubjectName == txtBoxSubject.Text).FirstOrDefault();
                    if (subject != null)
                    {
                        MessageBox.Show("The subject already exists.");
                        return;
                    }

                    subject = new Subject()
                    {
                        SubjectName = txtBoxSubject.Text,
                        TermId = (cmbBoxTerm.SelectedValue as Term).Id
                    };

                    ctx.Subjects.Add(subject);
                }
                else
                {
                    var subject = ctx.Subjects.Where(s => s.SubjectName == txtBoxSubject.Text).FirstOrDefault();
                    if (subject != null && subject.Id != updatedSubject.Id)
                    {
                        MessageBox.Show("The subject already exists.");
                        return;
                    }
                    else
                    {
                        subject = await ctx.Subjects.FindAsync(updatedSubject.Id);
                    }

                    subject.SubjectName = txtBoxSubject.Text;
                    subject.TermId = (cmbBoxTerm.SelectedValue as Term).Id;
                }

                await ctx.SaveChangesAsync();
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cmbBoxYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBoxYear.SelectedIndex != -1)
            {
                cmbBoxTerm.DataSource = null;
                cmbBoxTerm.DataSource = ((Year)cmbBoxYear.SelectedItem).Terms;
            }
        }
    }
}
