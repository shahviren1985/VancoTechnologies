using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AttendanceTracker.Helpers;
using AttendanceTracker.Models.Local;

namespace AttendanceTracker
{
    public partial class FrmMain : Form
    {
        string currentReportRange = "";

        string currentReportType = "";

        PicDownloaderEngine picDownloaderEngine;

        SyncPushEngine pushEngine;

        RemoteCourseDetailsCache remoteCourseDetailsCache;

        public FrmMain()
        {
            InitializeComponent();

            this.Width = 1200;
            this.Height = 600;

            Config.Init();

            if (Config.EnableLibraryCardUpdate)
            {
                InitUpdateLibraryCardMode();
            }
            else
            {
                InitNormalMode();
            }
        }

        #region Normal mode

        #region Initialization of normal mode

        private void InitNormalMode()
        {
            pnlReport.Location = new Point(218, 12);

            bool atLeastOneSyncExists = false;

            try
            {
                using (var localContext = new LocalContext())
                {
                    atLeastOneSyncExists = localContext.Synchronizations.Where(s => s.SyncType == "PULL").FirstOrDefault() != null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Checking of sync history in local database failed!{Environment.NewLine}{ex.Message}");
            }

            if (!atLeastOneSyncExists)
            {
                try
                {
                    MessageBox.Show("First run, student data will be pulled from the remote database. This process can take a while. A MessageBox will be shown at the end of the process.");

                    var puller = new SyncPullEngine();
                    puller.PullData();

                    MessageBox.Show("Student data pull has been successfully finished!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Pulling of remote data to the local database failed!{Environment.NewLine}{ex.Message}");
                }
            }

            pushEngine = new SyncPushEngine();

            try
            {
                using (var localContext = new LocalContext())
                {
                    var lastPullId = localContext.Synchronizations.Where(s => s.SyncType == "PULL").OrderByDescending(s => s.Id).First().Id;
                    var lastPiclId = localContext.Synchronizations.Where(s => s.SyncType == "PIC").OrderByDescending(s => s.Id).FirstOrDefault()?.Id;

                    if (lastPiclId == null || lastPiclId < lastPullId)
                    {
                        picDownloaderEngine = new PicDownloaderEngine();
                        picDownloaderEngine.Start(10);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Checking of sync history in local database failed!{Environment.NewLine}{ex.Message}");
            }

            RefreshLibraryEntryDataGridViewData().Wait();
        }

        #endregion

        #region Menu button clicks

        private void btnTrack_Click(object sender, EventArgs e)
        {
            dGVReport.DataSource = null;

            currentReportRange = "";
            currentReportType = "";

            btnTrack.BackColor = SystemColors.ActiveCaption;
            btnSync.BackColor = SystemColors.Control;
            btnReport.BackColor = SystemColors.Control;

            btnStudentDataPull.Visible = false;

            pnlMain.Visible = true;
            pnlReport.Visible = false;
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            dGVReport.DataSource = null;

            currentReportRange = "";
            currentReportType = "";

            btnTrack.BackColor = SystemColors.Control;
            btnSync.BackColor = SystemColors.ActiveCaption;
            btnReport.BackColor = SystemColors.Control;

            btnStudentDataPull.Visible = true;

            pnlMain.Visible = true;
            pnlReport.Visible = false;
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            rBToday.Checked = true;

            currentReportRange = "";
            currentReportType = "";

            btnTrack.BackColor = SystemColors.Control;
            btnSync.BackColor = SystemColors.Control;
            btnReport.BackColor = SystemColors.ActiveCaption;

            pnlMain.Visible = false;
            pnlReport.Visible = true;
        }

        #endregion

        private async void txtBoxLibraryCardNo_TextChanged(object sender, EventArgs e)
        {
            if (txtBoxLibraryCardNo.Text.Length < 10) { return; }

            using (var localContext = new LocalContext())
            {
                var student = localContext.StudentDetails.Where(s => s.LibraryCardNo == txtBoxLibraryCardNo.Text).FirstOrDefault();

                if (student == null)
                {
                    Task.Delay(100).Wait();
                    txtBoxLibraryCardNo.Clear();
                    return;
                }

                var libraryLog = localContext.LibraryLogs.Where(l => l.OutTime == null).FirstOrDefault();
                if (libraryLog == null)
                {
                    var dt = DateTime.Now;
                    libraryLog = new LibraryLog()
                    {
                        CRN = student.CRN,
                        IdCardNumber = student.LibraryCardNo,
                        Date = dt,
                        InTime = dt,
                    };

                    localContext.LibraryLogs.Add(libraryLog);
                }
                else
                {
                    libraryLog.OutTime = DateTime.Now;
                }

                localContext.SaveChanges();

                using (var frm = new FrmTrack(student, libraryLog))
                {
                    frm.ShowDialog();
                }

                txtBoxLibraryCardNo.Clear();

                await RefreshLibraryEntryDataGridViewData();
            }
        }

        private void txtBoxLibraryCardNo_Leave(object sender, EventArgs e)
        {
            // The focus always has to be on card no field because the RFID reader can only write to it in that case. (If we would not like to handle serial port communication)
            txtBoxLibraryCardNo.Focus();
        }

        private async void btnStudentDataPull_Click(object sender, EventArgs e)
        {
            btnStudentDataPull.Enabled = false;

            MessageBox.Show("Student data pull can take a while. A MessageBox will be shown at the end of the process.");

            await Task.Run(() =>
            {
                try
                {
                    var puller = new SyncPullEngine();
                    puller.PullData();

                    MessageBox.Show("Student data pull has been successfully finished!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Pulling of remote data to the local database failed!{Environment.NewLine}{ex.Message}");
                }
            });

            if (picDownloaderEngine == null)
            {
                picDownloaderEngine = new PicDownloaderEngine();
            }

            picDownloaderEngine.Start(1);

            btnStudentDataPull.Enabled = true;
        }

        private async Task RefreshLibraryEntryDataGridViewData()
        {
            try
            {
                dGVLibraryEntry.DataSource = await ReportHelper.GetLibraryLogsAsync(DateTime.Today, DateTime.Today);

                foreach (DataGridViewColumn c in dGVLibraryEntry.Columns)
                {
                    switch (c.Name.ToLower())
                    {
                        case "id":
                            c.Width = 100;
                            break;
                        case "idcardnumber":
                            c.Visible = false;
                            break;
                        case "crn":
                            c.Width = 160;
                            break;
                        case "date":
                            c.Width = 215;
                            break;
                        case "intime":
                            c.Width = 215;
                            break;
                        case "outtime":
                            c.Width = 215;
                            break;
                        case "pushed":
                            c.Visible = false;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Refresh of Entry Log table failed!{Environment.NewLine}{ex.Message}");
            }
        }

        private async void btnGetLibraryEntry_Click(object sender, EventArgs e)
        {
            btnGetLibraryEntry.Enabled = false;
            btnHoursSpent.Enabled = false;
            btnExport.Enabled = false;

            if (rBToday.Checked)
            {
                dGVReport.DataSource = await ReportHelper.GetReportDataObjectLibraryEntryAsync(DateTime.Today, DateTime.Today);
                currentReportRange = "today";
            }
            else if (rBWeekly.Checked)
            {
                var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
                var diff = DateTime.Today.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
                if (diff < 0)
                {
                    diff += 7;
                }

                dGVReport.DataSource = await ReportHelper.GetReportDataObjectLibraryEntryAsync(DateTime.Today.AddDays(-diff), DateTime.Today);
                currentReportRange = "week";
            }
            else if (rBMonthly.Checked)
            {
                dGVReport.DataSource = await ReportHelper.GetReportDataObjectLibraryEntryAsync(new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1), DateTime.Today);
                currentReportRange = "month";
            }
            else
            {
                dGVReport.DataSource = await ReportHelper.GetReportDataObjectLibraryEntryAsync(new DateTime(DateTime.Today.Year, 1, 1), DateTime.Today);
                currentReportRange = "year";
            }

            foreach (DataGridViewColumn c in dGVReport.Columns)
            {
                switch (c.Name.ToLower())
                {
                    case "id":
                        c.Width = 85;
                        break;
                    case "name":
                        c.Width = 280;
                        break;
                    case "crn":
                        c.Width = 140;
                        break;
                    case "in":
                        c.Width = 200;
                        break;
                    case "out":
                        c.Width = 200;
                        break;
                }
            }

            currentReportType = "libraryentry";

            btnGetLibraryEntry.Enabled = true;
            btnHoursSpent.Enabled = true;
            btnExport.Enabled = true;
        }

        private async void btnHoursSpent_Click(object sender, EventArgs e)
        {
            btnGetLibraryEntry.Enabled = false;
            btnHoursSpent.Enabled = false;
            btnExport.Enabled = false;

            if (rBToday.Checked)
            {
                dGVReport.DataSource = await ReportHelper.GetReportDataObjectHoursSpentAsync(DateTime.Today, DateTime.Today);
                currentReportRange = "today";
            }
            else if (rBWeekly.Checked)
            {
                var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
                var diff = DateTime.Today.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
                if (diff < 0)
                {
                    diff += 7;
                }

                dGVReport.DataSource = await ReportHelper.GetReportDataObjectHoursSpentAsync(DateTime.Today.AddDays(-diff), DateTime.Today);
                currentReportRange = "week";
            }
            else if (rBMonthly.Checked)
            {
                dGVReport.DataSource = await ReportHelper.GetReportDataObjectHoursSpentAsync(new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1), DateTime.Today);
                currentReportRange = "month";
            }
            else
            {
                dGVReport.DataSource = await ReportHelper.GetReportDataObjectHoursSpentAsync(new DateTime(DateTime.Today.Year, 1, 1), DateTime.Today);
                currentReportRange = "year";
            }

            foreach (DataGridViewColumn c in dGVReport.Columns)
            {
                switch (c.Name.ToLower())
                {
                    case "crn":
                        c.Width = 140;
                        break;
                    case "name":
                        c.Width = 280;
                        break;
                    case "specialization":
                        c.Width = 250;
                        break;
                    case "month":
                        c.Width = 80;
                        break;
                    case "hoursspent":
                        c.Width = 155;
                        break;
                }
            }

            currentReportType = "hoursspent";

            btnGetLibraryEntry.Enabled = true;
            btnHoursSpent.Enabled = true;
            btnExport.Enabled = true;
        }

        private async void btnExport_Click(object sender, EventArgs e)
        {
            if (dGVReport.Rows.Count == 0)
            {
                MessageBox.Show("Nothing to export...");
                return;
            }

            int type = -1;

            if (dGVReport.Rows[0].DataBoundItem is ReportDataObjectLibraryEntry)
            {
                type = 1;
            }
            else if (dGVReport.Rows[0].DataBoundItem is ReportDataObjectHoursSpent)
            {
                type = 2;
            }
            else
            {
                MessageBox.Show("Unknown report type.");
                return;
            }


            string fileName = $"{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}_{currentReportType}_{currentReportRange}.csv";

            using (var sw = new StreamWriter(fileName))
            {
                if (type == 1)
                {
                    await sw.WriteLineAsync(ReportDataObjectLibraryEntry.GetCsvHeader());
                }
                else
                {
                    await sw.WriteLineAsync(ReportDataObjectHoursSpent.GetCsvHeader());
                }

                foreach (DataGridViewRow r in dGVReport.Rows)
                {
                    if (type == 1)
                    {
                        await sw.WriteLineAsync(((ReportDataObjectLibraryEntry)r.DataBoundItem).ToCsvLine());
                    }
                    else
                    {
                        await sw.WriteLineAsync(((ReportDataObjectHoursSpent)r.DataBoundItem).ToCsvLine());
                    }
                }

                sw.Flush();
            }

            MessageBox.Show("Successfully exported!");
        }

        #endregion

        #region Library Card Update mode

        #region Initialization of Library Card Update mode

        private void InitUpdateLibraryCardMode()
        {
            btnTrack.Visible = false;
            btnSync.Visible = false;
            btnReport.Visible = false;
            lblLibraryCardNoText.Visible = false;
            txtBoxLibraryCardNo.Visible = false;
            dGVLibraryEntry.Visible = false;
            pnlUpdateLibraryCard.Location = new Point(12, 12);
            pnlUpdateLibraryCard.Visible = true;

            try
            {
                cBCourse.DataSource = LibraryCardUpdateHelper.GetCourseList();
                cBSpecialization.DataSource = LibraryCardUpdateHelper.GetSpecializationList();
                cBType.DataSource = LibraryCardUpdateHelper.GetTypeList();

                remoteCourseDetailsCache = new RemoteCourseDetailsCache();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Loading data from remote database failed!{Environment.NewLine}{ex.Message}");
            }
        }

        #endregion

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dGVUpdateLibraryCard.DataSource = null;

            var ids = LibraryCardUpdateHelper.GetCourseIdsForFilters(cBCourse.SelectedItem.ToString(), cBSpecialization.SelectedItem.ToString(), cBType.SelectedItem.ToString());
            dGVUpdateLibraryCard.DataSource = LibraryCardUpdateHelper.GetRemoteStudentsByCourseIds(ids);

            foreach (DataGridViewColumn c in dGVUpdateLibraryCard.Columns)
            {
                switch (c.Name.ToLower())
                {
                    case "userid":
                        c.Width = 110;
                        break;
                    case "librarycardno":
                        c.Width = 270;
                        break;
                    case "rollnumber":
                        c.Width = 170;
                        break;
                    case "firstname":
                        c.Width = 270;
                        break;
                    case "lastname":
                        c.Width = 270;
                        break;
                    default:
                        c.Visible = false;
                        break;
                }
            }
        }

        private void dGVUpdateLibraryCard_SelectionChanged(object sender, EventArgs e)
        {
            ShowSelectedStudentData();
        }

        private void ShowSelectedStudentData()
        {
            if (dGVUpdateLibraryCard.SelectedRows.Count != 1)
            {
                pnlUpdate.Visible = false;
                return;
            }

            var element = (Models.Remote.StudentDetail)dGVUpdateLibraryCard.SelectedRows[0].DataBoundItem;

            lblFirstName.Text = element.FirstName;
            lblLastName.Text = element.LastName;
            lblRollNumber.Text = element.RollNumber?.ToString() ?? "-";
            lblCrn.Text = element.Id.ToString();
            txtBoxLibraryCardNoVal.Text = element.LibraryCardNo;
            ShowPicture(PictureHelper.GetLocalPicPath(element.AdmissionYear, element.PhotoPath));

            var course = remoteCourseDetailsCache.GetCourseById(element.CourseId);

            if (course != null)
            {
                lblCourse.Text = course.Year;
                lblSpecialization.Text = course.Specialization;
                lblType.Text = course.CourseName;
            }
            else
            {
                lblCourse.Text = "-";
                lblSpecialization.Text = "-";
                lblType.Text = "-";
            }

            pnlUpdate.Visible = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ((Models.Remote.StudentDetail)dGVUpdateLibraryCard.SelectedRows[0].DataBoundItem).LibraryCardNo = txtBoxLibraryCardNoVal.Text;

                LibraryCardUpdateHelper.UpdateRemote((Models.Remote.StudentDetail)dGVUpdateLibraryCard.SelectedRows[0].DataBoundItem);

                LibraryCardUpdateHelper.UpdateLocal((Models.Remote.StudentDetail)dGVUpdateLibraryCard.SelectedRows[0].DataBoundItem);

                dGVUpdateLibraryCard.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Update of student failed!{Environment.NewLine}{ex.Message}");
            }
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

        #endregion

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (pushEngine != null)
            {
                pushEngine.Stop();
                pushEngine.Dispose();
                pushEngine = null;
            }

            if (picDownloaderEngine != null)
            {
                picDownloaderEngine.Stop();
                picDownloaderEngine.Dispose();
                picDownloaderEngine = null;
            }
        }
    }
}
