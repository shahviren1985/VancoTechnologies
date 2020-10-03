using System;
using System.Windows.Forms;
using AcademicCalendar.Helpers;
using AcademicCalendar.Models;

namespace AcademicCalendar.Forms.TopicMaster
{
    public partial class FrmTopicDetails : Form
    {
        TopicHelper topic;

        public FrmTopicDetails(TopicHelper topicHelper, Faculty[] faculties)
        {
            InitializeComponent();

            topic = topicHelper;

            txtBoxTopic.Text = topic.Name;

            cmbBoxFaculties.DataSource = faculties;

            foreach (var f in faculties)
            {
                if (f.Id == topic.FacultyId)
                {
                    cmbBoxFaculties.SelectedItem = f;
                    break;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtBoxTopic.Text.Length < 1)
            {
                MessageBox.Show("The name field can not be empty.");
                return;
            }

            if (cmbBoxFaculties.SelectedIndex == -1)
            {
                MessageBox.Show("Faculty can not be empty.");
                return;
            }

            var selectedFaculty = (cmbBoxFaculties.SelectedItem as Faculty);
            topic.Name = txtBoxTopic.Text;
            topic.FacultyId = selectedFaculty.Id;
            topic.FacultyName = selectedFaculty.Name;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnDiscard_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
