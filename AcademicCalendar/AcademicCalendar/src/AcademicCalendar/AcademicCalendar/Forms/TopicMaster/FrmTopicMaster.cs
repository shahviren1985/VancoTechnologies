using System;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using AcademicCalendar.Helpers;
using AcademicCalendar.Models;

namespace AcademicCalendar.Forms.TopicMaster
{
    public partial class FrmTopicMaster : Form
    {
        BindingList<TopicHelper> topicRows;

        Faculty[] faculties;

        public FrmTopicMaster()
        {
            InitializeComponent();

            using (var ctx = new CalContext())
            {
                cmbBoxYear.DataSource = ctx.Years.Include("Terms").ToList();
            }
        }

        private void SetDGVDataSource(int selectedIndex = -1)
        {
            topicRows = new BindingList<TopicHelper>(topicRows.OrderBy(t => t.LectureNo).ToList());
            dGVTopics.DataSource = topicRows;

            if (selectedIndex > -1)
            {
                dGVTopics.Rows[selectedIndex].Selected = true;
            }

            dGVTopics.Refresh();

            foreach (DataGridViewColumn c in dGVTopics.Columns)
            {
                switch (c.Name.ToLower())
                {
                    case "lectureno":
                        c.Width = 105;
                        break;
                    case "name":
                        c.Width = 310;
                        break;
                    case "facultyname":
                        c.Width = 310;
                        break;
                    case "facultyid":
                        c.Visible = false;
                        break;
                }
            }
        }

        private void cmbBoxYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (topicRows != null)
            {
                topicRows.Clear();
            }

            topicRows = null;

            dGVTopics.DataSource = null;

            cmbBoxSubject.SelectedIndex = -1;
            cmbBoxTerm.SelectedIndex = -1;

            cmbBoxSubject.DataSource = null;
            cmbBoxTerm.DataSource = null;

            if (cmbBoxYear.SelectedIndex == -1)
            {
                return;
            }

            cmbBoxTerm.DataSource = (cmbBoxYear.SelectedItem as Year).Terms;
        }

        private void cmbBoxTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (topicRows != null)
            {
                topicRows.Clear();
            }

            topicRows = null;

            dGVTopics.DataSource = null;

            cmbBoxSubject.SelectedIndex = -1;

            if (cmbBoxTerm.SelectedIndex == -1)
            {
                return;
            }

            var term = cmbBoxTerm.SelectedItem as Term;

            using (var ctx = new CalContext())
            {
                cmbBoxSubject.DataSource = ctx.Subjects.Include("Term.Year").Where(s => s.TermId == term.Id).ToList();
            }
        }

        private async void btnShow_Click(object sender, EventArgs e)
        {
            if (cmbBoxSubject.SelectedIndex == -1)
            {
                MessageBox.Show("Subject must be selected.");
                return;
            }

            if (topicRows != null)
            {
                topicRows.Clear();
            }

            topicRows = null;

            topicRows = new BindingList<TopicHelper>();

            var selectedSubject = cmbBoxSubject.SelectedItem as Subject;

            using (var ctx = new CalContext())
            {
                var facultySubjects = await ctx.FacultySubjects
                    .Where(fs => fs.SubjectId == selectedSubject.Id)
                    .Include("Faculty")
                    .Include("Topics")
                    .ToListAsync();

                faculties = facultySubjects.Select(el => el.Faculty).ToArray();

                foreach (var facultySubject in facultySubjects)
                {
                    foreach (var topic in facultySubject.Topics)
                    {
                        topicRows.Add(new TopicHelper()
                        {
                            FacultyId = facultySubject.FacultyId,
                            FacultyName = facultySubject.Faculty.Name,
                            LectureNo = topic.LectureNo,
                            Name = topic.Name
                        });
                    }
                }

                SetDGVDataSource();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (topicRows == null)
            {
                MessageBox.Show("It is necessary to load current topics before adding a new one.");
                return;
            }

            var topic = new TopicHelper();

            using (var frm = new FrmTopicDetails(topic, faculties))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    topic.LectureNo = topicRows.Count == 0 ? 1 : topicRows.Max(r => r.LectureNo) + 1;
                    topicRows.Add(topic);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dGVTopics.SelectedRows.Count < 1)
            {
                MessageBox.Show("There is no line selected.");
                return;
            }

            var item = dGVTopics.SelectedRows[0].DataBoundItem as TopicHelper;

            using (var frm = new FrmTopicDetails(item, faculties))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    dGVTopics.Refresh();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dGVTopics.SelectedRows.Count < 1)
            {
                MessageBox.Show("There is no line selected.");
                return;
            }

            var item = dGVTopics.SelectedRows[0].DataBoundItem as TopicHelper;

            foreach (DataGridViewRow i in dGVTopics.Rows)
            {
                var curr = i.DataBoundItem as TopicHelper;

                if (curr.LectureNo > item.LectureNo)
                {
                    curr.LectureNo -= 1;
                    break;
                }
            }

            topicRows.Remove(item);

            SetDGVDataSource();
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            if (dGVTopics.SelectedRows.Count < 1)
            {
                MessageBox.Show("There is no line selected.");
                return;
            }

            var selectedIndex = dGVTopics.SelectedRows[0].Index;

            var item = dGVTopics.SelectedRows[0].DataBoundItem as TopicHelper;

            if (item.LectureNo < topicRows.Max(r => r.LectureNo))
            {
                foreach (DataGridViewRow i in dGVTopics.Rows)
                {
                    var curr = i.DataBoundItem as TopicHelper;

                    if (curr.LectureNo == item.LectureNo + 1)
                    {
                        curr.LectureNo -= 1;
                        item.LectureNo += 1;
                        break;
                    }
                }

                SetDGVDataSource(selectedIndex + 1);
            }
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            if (dGVTopics.SelectedRows.Count < 1)
            {
                MessageBox.Show("There is no line selected.");
                return;
            }

            var selectedIndex = dGVTopics.SelectedRows[0].Index;

            var item = dGVTopics.SelectedRows[0].DataBoundItem as TopicHelper;

            if (item.LectureNo > 1)
            {
                foreach (DataGridViewRow i in dGVTopics.Rows)
                {
                    var curr = i.DataBoundItem as TopicHelper;

                    if (curr.LectureNo == item.LectureNo - 1)
                    {
                        curr.LectureNo += 1;
                        item.LectureNo -= 1;
                        break;
                    }
                }

                SetDGVDataSource(selectedIndex - 1);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = false;

            if (topicRows != null)
            {
                var selectedSubject = cmbBoxSubject.SelectedItem as Subject;

                using (var ctx = new CalContext())
                {
                    var facultySubjects = ctx.FacultySubjects
                        .Where(fs => fs.SubjectId == selectedSubject.Id)
                        .ToList();

                    var facultySubjectIds = facultySubjects
                        .Select(fs => fs.Id)
                        .ToList();

                    var topicsToDelete = ctx.Topics.Where(t => facultySubjectIds.Contains(t.FacultySubjectId)).ToArray();

                    ctx.Topics.RemoveRange(topicsToDelete);

                    ctx.SaveChanges();

                    foreach (var topicRow in topicRows)
                    {
                        var newTopic = new Topic()
                        {
                            FacultySubject = facultySubjects.Where(fs => fs.FacultyId == topicRow.FacultyId && fs.SubjectId == selectedSubject.Id).First(),
                            LectureNo = topicRow.LectureNo,
                            Name = topicRow.Name
                        };

                        ctx.Topics.Add(newTopic);
                    }

                    ctx.SaveChanges();
                }
            }

            this.Close();
        }

        private void cmbBoxSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (topicRows != null)
            {
                topicRows.Clear();
            }

            topicRows = null;

            dGVTopics.DataSource = null;
        }
    }
}
