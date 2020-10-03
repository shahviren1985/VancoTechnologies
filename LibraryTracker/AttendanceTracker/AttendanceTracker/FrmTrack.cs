using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AttendanceTracker.Helpers;
using AttendanceTracker.Models.Local;

namespace AttendanceTracker
{
    public partial class FrmTrack : Form
    {
        Timer timer = new Timer();

        public FrmTrack(StudentDetail student, LibraryLog libraryLog)
        {
            InitializeComponent();

            ShowStudentDetails(student, libraryLog);

            timer.Interval = Config.StudentDetailsAutoCloseSec * 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void ShowStudentDetails(StudentDetail student, LibraryLog libraryLog)
        {
            lblCheckState.Text = libraryLog.OutTime == null ? "Checked In" : "Checked Out";
            lblCheckState.ForeColor = libraryLog.OutTime == null ? Color.Green : Color.Red;
            lblCheckState.Visible = true;

            lblCrn.Text = student.CRN;
            lblFirstName.Text = student.FirstName;
            lblLastName.Text = student.LastName;
            lblCourseName.Text = student.CourseName;
            lblSpecialization.Text = student.Specialization;
            lblLogDate.Text = libraryLog.Date.ToString();
            lblInTime.Text = libraryLog.InTime.ToString();
            lblOutTime.Text = libraryLog.OutTime?.ToString() ?? "-";
            lblLibraryCardNo.Text = student.LibraryCardNo;

            ShowPicture(PictureHelper.GetLocalPicPath(student.AdmissionYear, student.PhotoPath));
        }

        private void ShowPicture(string pictureName)
        {
            if (string.IsNullOrWhiteSpace(pictureName))
            {
                pictureName = Path.Combine(Config.LocalPhotoFolder, "default.jpg");
            }

            try
            {
                picBoxUser.Image = Image.FromFile(pictureName);
                picBoxUser.Visible = true;
            }
            catch (Exception)
            {
                if (string.Equals(pictureName, Path.Combine(Config.LocalPhotoFolder, "default.jpg"), StringComparison.OrdinalIgnoreCase))
                {
                    picBoxUser.Image = null;
                }
                else
                {
                    // Fallbacking to the default picture.
                    ShowPicture(null);
                }
            }
        }

        private void CloseForm()
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void FrmTrack_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                CloseForm();
            }
        }

        private void FrmTrack_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (timer != null)
            {
                timer.Stop();
                timer.Dispose();
                timer = null;
            }
        }
    }
}
