namespace AttendanceTracker
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblLibraryCardNoText = new System.Windows.Forms.Label();
            this.txtBoxLibraryCardNo = new System.Windows.Forms.TextBox();
            this.btnTrack = new System.Windows.Forms.Button();
            this.btnSync = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnStudentDataPull = new System.Windows.Forms.Button();
            this.dGVLibraryEntry = new System.Windows.Forms.DataGridView();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlReport = new System.Windows.Forms.Panel();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnHoursSpent = new System.Windows.Forms.Button();
            this.btnGetLibraryEntry = new System.Windows.Forms.Button();
            this.rBYearly = new System.Windows.Forms.RadioButton();
            this.rBMonthly = new System.Windows.Forms.RadioButton();
            this.rBWeekly = new System.Windows.Forms.RadioButton();
            this.rBToday = new System.Windows.Forms.RadioButton();
            this.lblDateRangeText = new System.Windows.Forms.Label();
            this.dGVReport = new System.Windows.Forms.DataGridView();
            this.pnlUpdateLibraryCard = new System.Windows.Forms.Panel();
            this.pnlUpdate = new System.Windows.Forms.Panel();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.lblType = new System.Windows.Forms.Label();
            this.lblCourse = new System.Windows.Forms.Label();
            this.lblRollNumber = new System.Windows.Forms.Label();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.lblLastName = new System.Windows.Forms.Label();
            this.lblCrn = new System.Windows.Forms.Label();
            this.lblSpecialization = new System.Windows.Forms.Label();
            this.txtBoxLibraryCardNoVal = new System.Windows.Forms.TextBox();
            this.lblLibraryCardNoTextUpd = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSpecializationText = new System.Windows.Forms.Label();
            this.lblCourseText = new System.Windows.Forms.Label();
            this.lblCrnText = new System.Windows.Forms.Label();
            this.lblRollNumberText = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblLastNameText = new System.Windows.Forms.Label();
            this.picBoxUser = new System.Windows.Forms.PictureBox();
            this.lblFirstNameText = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cBType = new System.Windows.Forms.ComboBox();
            this.lblTypeFilterText = new System.Windows.Forms.Label();
            this.cBSpecialization = new System.Windows.Forms.ComboBox();
            this.lblSpecializationFilterText = new System.Windows.Forms.Label();
            this.cBCourse = new System.Windows.Forms.ComboBox();
            this.lblCourseFilterText = new System.Windows.Forms.Label();
            this.dGVUpdateLibraryCard = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dGVLibraryEntry)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.pnlReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGVReport)).BeginInit();
            this.pnlUpdateLibraryCard.SuspendLayout();
            this.pnlUpdate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGVUpdateLibraryCard)).BeginInit();
            this.SuspendLayout();
            // 
            // lblLibraryCardNoText
            // 
            this.lblLibraryCardNoText.AutoSize = true;
            this.lblLibraryCardNoText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLibraryCardNoText.Location = new System.Drawing.Point(409, 28);
            this.lblLibraryCardNoText.Name = "lblLibraryCardNoText";
            this.lblLibraryCardNoText.Size = new System.Drawing.Size(122, 20);
            this.lblLibraryCardNoText.TabIndex = 1;
            this.lblLibraryCardNoText.Text = "Library Card No:";
            // 
            // txtBoxLibraryCardNo
            // 
            this.txtBoxLibraryCardNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxLibraryCardNo.Location = new System.Drawing.Point(537, 25);
            this.txtBoxLibraryCardNo.Name = "txtBoxLibraryCardNo";
            this.txtBoxLibraryCardNo.Size = new System.Drawing.Size(355, 26);
            this.txtBoxLibraryCardNo.TabIndex = 0;
            this.txtBoxLibraryCardNo.TextChanged += new System.EventHandler(this.txtBoxLibraryCardNo_TextChanged);
            this.txtBoxLibraryCardNo.Leave += new System.EventHandler(this.txtBoxLibraryCardNo_Leave);
            // 
            // btnTrack
            // 
            this.btnTrack.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnTrack.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrack.Location = new System.Drawing.Point(12, 12);
            this.btnTrack.Name = "btnTrack";
            this.btnTrack.Size = new System.Drawing.Size(200, 70);
            this.btnTrack.TabIndex = 1;
            this.btnTrack.Text = "Track";
            this.btnTrack.UseVisualStyleBackColor = false;
            this.btnTrack.Click += new System.EventHandler(this.btnTrack_Click);
            // 
            // btnSync
            // 
            this.btnSync.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSync.Location = new System.Drawing.Point(12, 88);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(200, 70);
            this.btnSync.TabIndex = 2;
            this.btnSync.Text = "Sync";
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // btnReport
            // 
            this.btnReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.Location = new System.Drawing.Point(12, 164);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(200, 70);
            this.btnReport.TabIndex = 3;
            this.btnReport.Text = "Report";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnStudentDataPull
            // 
            this.btnStudentDataPull.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStudentDataPull.Location = new System.Drawing.Point(777, 3);
            this.btnStudentDataPull.Name = "btnStudentDataPull";
            this.btnStudentDataPull.Size = new System.Drawing.Size(162, 34);
            this.btnStudentDataPull.TabIndex = 2;
            this.btnStudentDataPull.Text = "Student Data Pull";
            this.btnStudentDataPull.UseVisualStyleBackColor = true;
            this.btnStudentDataPull.Visible = false;
            this.btnStudentDataPull.Click += new System.EventHandler(this.btnStudentDataPull_Click);
            // 
            // dGVLibraryEntry
            // 
            this.dGVLibraryEntry.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGVLibraryEntry.Location = new System.Drawing.Point(3, 43);
            this.dGVLibraryEntry.Name = "dGVLibraryEntry";
            this.dGVLibraryEntry.ReadOnly = true;
            this.dGVLibraryEntry.Size = new System.Drawing.Size(948, 446);
            this.dGVLibraryEntry.TabIndex = 0;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.btnStudentDataPull);
            this.pnlMain.Controls.Add(this.dGVLibraryEntry);
            this.pnlMain.Location = new System.Drawing.Point(218, 57);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(954, 492);
            this.pnlMain.TabIndex = 10;
            // 
            // pnlReport
            // 
            this.pnlReport.Controls.Add(this.btnExport);
            this.pnlReport.Controls.Add(this.btnHoursSpent);
            this.pnlReport.Controls.Add(this.btnGetLibraryEntry);
            this.pnlReport.Controls.Add(this.rBYearly);
            this.pnlReport.Controls.Add(this.rBMonthly);
            this.pnlReport.Controls.Add(this.rBWeekly);
            this.pnlReport.Controls.Add(this.rBToday);
            this.pnlReport.Controls.Add(this.lblDateRangeText);
            this.pnlReport.Controls.Add(this.dGVReport);
            this.pnlReport.Location = new System.Drawing.Point(218, 555);
            this.pnlReport.Name = "pnlReport";
            this.pnlReport.Size = new System.Drawing.Size(954, 537);
            this.pnlReport.TabIndex = 11;
            this.pnlReport.Visible = false;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(840, 14);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(111, 23);
            this.btnExport.TabIndex = 8;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnHoursSpent
            // 
            this.btnHoursSpent.Location = new System.Drawing.Point(689, 14);
            this.btnHoursSpent.Name = "btnHoursSpent";
            this.btnHoursSpent.Size = new System.Drawing.Size(111, 23);
            this.btnHoursSpent.TabIndex = 7;
            this.btnHoursSpent.Text = "Hours Spent";
            this.btnHoursSpent.UseVisualStyleBackColor = true;
            this.btnHoursSpent.Click += new System.EventHandler(this.btnHoursSpent_Click);
            // 
            // btnGetLibraryEntry
            // 
            this.btnGetLibraryEntry.Location = new System.Drawing.Point(533, 14);
            this.btnGetLibraryEntry.Name = "btnGetLibraryEntry";
            this.btnGetLibraryEntry.Size = new System.Drawing.Size(111, 23);
            this.btnGetLibraryEntry.TabIndex = 6;
            this.btnGetLibraryEntry.Text = "Library Entry";
            this.btnGetLibraryEntry.UseVisualStyleBackColor = true;
            this.btnGetLibraryEntry.Click += new System.EventHandler(this.btnGetLibraryEntry_Click);
            // 
            // rBYearly
            // 
            this.rBYearly.AutoSize = true;
            this.rBYearly.Location = new System.Drawing.Point(408, 17);
            this.rBYearly.Name = "rBYearly";
            this.rBYearly.Size = new System.Drawing.Size(54, 17);
            this.rBYearly.TabIndex = 5;
            this.rBYearly.TabStop = true;
            this.rBYearly.Text = "Yearly";
            this.rBYearly.UseVisualStyleBackColor = true;
            // 
            // rBMonthly
            // 
            this.rBMonthly.AutoSize = true;
            this.rBMonthly.Location = new System.Drawing.Point(299, 17);
            this.rBMonthly.Name = "rBMonthly";
            this.rBMonthly.Size = new System.Drawing.Size(62, 17);
            this.rBMonthly.TabIndex = 4;
            this.rBMonthly.TabStop = true;
            this.rBMonthly.Text = "Monthly";
            this.rBMonthly.UseVisualStyleBackColor = true;
            // 
            // rBWeekly
            // 
            this.rBWeekly.AutoSize = true;
            this.rBWeekly.Location = new System.Drawing.Point(195, 17);
            this.rBWeekly.Name = "rBWeekly";
            this.rBWeekly.Size = new System.Drawing.Size(61, 17);
            this.rBWeekly.TabIndex = 3;
            this.rBWeekly.TabStop = true;
            this.rBWeekly.Text = "Weekly";
            this.rBWeekly.UseVisualStyleBackColor = true;
            // 
            // rBToday
            // 
            this.rBToday.AutoSize = true;
            this.rBToday.Location = new System.Drawing.Point(98, 17);
            this.rBToday.Name = "rBToday";
            this.rBToday.Size = new System.Drawing.Size(55, 17);
            this.rBToday.TabIndex = 2;
            this.rBToday.TabStop = true;
            this.rBToday.Text = "Today";
            this.rBToday.UseVisualStyleBackColor = true;
            // 
            // lblDateRangeText
            // 
            this.lblDateRangeText.AutoSize = true;
            this.lblDateRangeText.Location = new System.Drawing.Point(24, 19);
            this.lblDateRangeText.Name = "lblDateRangeText";
            this.lblDateRangeText.Size = new System.Drawing.Size(68, 13);
            this.lblDateRangeText.TabIndex = 1;
            this.lblDateRangeText.Text = "Date Range:";
            // 
            // dGVReport
            // 
            this.dGVReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGVReport.Location = new System.Drawing.Point(3, 43);
            this.dGVReport.Name = "dGVReport";
            this.dGVReport.ReadOnly = true;
            this.dGVReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dGVReport.Size = new System.Drawing.Size(948, 491);
            this.dGVReport.TabIndex = 0;
            // 
            // pnlUpdateLibraryCard
            // 
            this.pnlUpdateLibraryCard.Controls.Add(this.pnlUpdate);
            this.pnlUpdateLibraryCard.Controls.Add(this.btnSearch);
            this.pnlUpdateLibraryCard.Controls.Add(this.cBType);
            this.pnlUpdateLibraryCard.Controls.Add(this.lblTypeFilterText);
            this.pnlUpdateLibraryCard.Controls.Add(this.cBSpecialization);
            this.pnlUpdateLibraryCard.Controls.Add(this.lblSpecializationFilterText);
            this.pnlUpdateLibraryCard.Controls.Add(this.cBCourse);
            this.pnlUpdateLibraryCard.Controls.Add(this.lblCourseFilterText);
            this.pnlUpdateLibraryCard.Controls.Add(this.dGVUpdateLibraryCard);
            this.pnlUpdateLibraryCard.Location = new System.Drawing.Point(12, 1098);
            this.pnlUpdateLibraryCard.Name = "pnlUpdateLibraryCard";
            this.pnlUpdateLibraryCard.Size = new System.Drawing.Size(1160, 537);
            this.pnlUpdateLibraryCard.TabIndex = 9;
            // 
            // pnlUpdate
            // 
            this.pnlUpdate.Controls.Add(this.btnUpdate);
            this.pnlUpdate.Controls.Add(this.lblType);
            this.pnlUpdate.Controls.Add(this.lblCourse);
            this.pnlUpdate.Controls.Add(this.lblRollNumber);
            this.pnlUpdate.Controls.Add(this.lblFirstName);
            this.pnlUpdate.Controls.Add(this.lblLastName);
            this.pnlUpdate.Controls.Add(this.lblCrn);
            this.pnlUpdate.Controls.Add(this.lblSpecialization);
            this.pnlUpdate.Controls.Add(this.txtBoxLibraryCardNoVal);
            this.pnlUpdate.Controls.Add(this.lblLibraryCardNoTextUpd);
            this.pnlUpdate.Controls.Add(this.label2);
            this.pnlUpdate.Controls.Add(this.lblSpecializationText);
            this.pnlUpdate.Controls.Add(this.lblCourseText);
            this.pnlUpdate.Controls.Add(this.lblCrnText);
            this.pnlUpdate.Controls.Add(this.lblRollNumberText);
            this.pnlUpdate.Controls.Add(this.btnClose);
            this.pnlUpdate.Controls.Add(this.lblLastNameText);
            this.pnlUpdate.Controls.Add(this.picBoxUser);
            this.pnlUpdate.Controls.Add(this.lblFirstNameText);
            this.pnlUpdate.Location = new System.Drawing.Point(3, 306);
            this.pnlUpdate.Name = "pnlUpdate";
            this.pnlUpdate.Size = new System.Drawing.Size(1154, 185);
            this.pnlUpdate.TabIndex = 56;
            this.pnlUpdate.Visible = false;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(600, 144);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(288, 23);
            this.btnUpdate.TabIndex = 62;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(279, 107);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(10, 13);
            this.lblType.TabIndex = 61;
            this.lblType.Text = "-";
            // 
            // lblCourse
            // 
            this.lblCourse.AutoSize = true;
            this.lblCourse.Location = new System.Drawing.Point(279, 79);
            this.lblCourse.Name = "lblCourse";
            this.lblCourse.Size = new System.Drawing.Size(10, 13);
            this.lblCourse.TabIndex = 60;
            this.lblCourse.Text = "-";
            // 
            // lblRollNumber
            // 
            this.lblRollNumber.AutoSize = true;
            this.lblRollNumber.Location = new System.Drawing.Point(279, 51);
            this.lblRollNumber.Name = "lblRollNumber";
            this.lblRollNumber.Size = new System.Drawing.Size(10, 13);
            this.lblRollNumber.TabIndex = 59;
            this.lblRollNumber.Text = "-";
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Location = new System.Drawing.Point(279, 23);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(10, 13);
            this.lblFirstName.TabIndex = 58;
            this.lblFirstName.Text = "-";
            // 
            // lblLastName
            // 
            this.lblLastName.AutoSize = true;
            this.lblLastName.Location = new System.Drawing.Point(678, 23);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(10, 13);
            this.lblLastName.TabIndex = 57;
            this.lblLastName.Text = "-";
            // 
            // lblCrn
            // 
            this.lblCrn.AutoSize = true;
            this.lblCrn.Location = new System.Drawing.Point(678, 51);
            this.lblCrn.Name = "lblCrn";
            this.lblCrn.Size = new System.Drawing.Size(10, 13);
            this.lblCrn.TabIndex = 56;
            this.lblCrn.Text = "-";
            // 
            // lblSpecialization
            // 
            this.lblSpecialization.AutoSize = true;
            this.lblSpecialization.Location = new System.Drawing.Point(678, 79);
            this.lblSpecialization.Name = "lblSpecialization";
            this.lblSpecialization.Size = new System.Drawing.Size(10, 13);
            this.lblSpecialization.TabIndex = 55;
            this.lblSpecialization.Text = "-";
            // 
            // txtBoxLibraryCardNoVal
            // 
            this.txtBoxLibraryCardNoVal.Location = new System.Drawing.Point(681, 104);
            this.txtBoxLibraryCardNoVal.Name = "txtBoxLibraryCardNoVal";
            this.txtBoxLibraryCardNoVal.Size = new System.Drawing.Size(207, 20);
            this.txtBoxLibraryCardNoVal.TabIndex = 54;
            // 
            // lblLibraryCardNoTextUpd
            // 
            this.lblLibraryCardNoTextUpd.AutoSize = true;
            this.lblLibraryCardNoTextUpd.Location = new System.Drawing.Point(597, 107);
            this.lblLibraryCardNoTextUpd.Name = "lblLibraryCardNoTextUpd";
            this.lblLibraryCardNoTextUpd.Size = new System.Drawing.Size(66, 13);
            this.lblLibraryCardNoTextUpd.TabIndex = 53;
            this.lblLibraryCardNoTextUpd.Text = "Library Card:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(205, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 52;
            this.label2.Text = "Type:";
            // 
            // lblSpecializationText
            // 
            this.lblSpecializationText.AutoSize = true;
            this.lblSpecializationText.Location = new System.Drawing.Point(597, 79);
            this.lblSpecializationText.Name = "lblSpecializationText";
            this.lblSpecializationText.Size = new System.Drawing.Size(75, 13);
            this.lblSpecializationText.TabIndex = 51;
            this.lblSpecializationText.Text = "Specialization:";
            // 
            // lblCourseText
            // 
            this.lblCourseText.AutoSize = true;
            this.lblCourseText.Location = new System.Drawing.Point(205, 79);
            this.lblCourseText.Name = "lblCourseText";
            this.lblCourseText.Size = new System.Drawing.Size(43, 13);
            this.lblCourseText.TabIndex = 50;
            this.lblCourseText.Text = "Course:";
            // 
            // lblCrnText
            // 
            this.lblCrnText.AutoSize = true;
            this.lblCrnText.Location = new System.Drawing.Point(597, 51);
            this.lblCrnText.Name = "lblCrnText";
            this.lblCrnText.Size = new System.Drawing.Size(33, 13);
            this.lblCrnText.TabIndex = 49;
            this.lblCrnText.Text = "CRN:";
            // 
            // lblRollNumberText
            // 
            this.lblRollNumberText.AutoSize = true;
            this.lblRollNumberText.Location = new System.Drawing.Point(205, 51);
            this.lblRollNumberText.Name = "lblRollNumberText";
            this.lblRollNumberText.Size = new System.Drawing.Size(68, 13);
            this.lblRollNumberText.TabIndex = 48;
            this.lblRollNumberText.Text = "Roll Number:";
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(1110, 13);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(32, 32);
            this.btnClose.TabIndex = 47;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // lblLastNameText
            // 
            this.lblLastNameText.AutoSize = true;
            this.lblLastNameText.Location = new System.Drawing.Point(597, 23);
            this.lblLastNameText.Name = "lblLastNameText";
            this.lblLastNameText.Size = new System.Drawing.Size(61, 13);
            this.lblLastNameText.TabIndex = 46;
            this.lblLastNameText.Text = "Last Name:";
            // 
            // picBoxUser
            // 
            this.picBoxUser.Location = new System.Drawing.Point(12, 13);
            this.picBoxUser.Name = "picBoxUser";
            this.picBoxUser.Size = new System.Drawing.Size(120, 160);
            this.picBoxUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxUser.TabIndex = 44;
            this.picBoxUser.TabStop = false;
            this.picBoxUser.Visible = false;
            // 
            // lblFirstNameText
            // 
            this.lblFirstNameText.AutoSize = true;
            this.lblFirstNameText.Location = new System.Drawing.Point(205, 23);
            this.lblFirstNameText.Name = "lblFirstNameText";
            this.lblFirstNameText.Size = new System.Drawing.Size(60, 13);
            this.lblFirstNameText.TabIndex = 45;
            this.lblFirstNameText.Text = "First Name:";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(1010, 26);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(121, 23);
            this.btnSearch.TabIndex = 55;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cBType
            // 
            this.cBType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBType.FormattingEnabled = true;
            this.cBType.Location = new System.Drawing.Point(790, 28);
            this.cBType.Name = "cBType";
            this.cBType.Size = new System.Drawing.Size(171, 21);
            this.cBType.TabIndex = 54;
            // 
            // lblTypeFilterText
            // 
            this.lblTypeFilterText.AutoSize = true;
            this.lblTypeFilterText.Location = new System.Drawing.Point(750, 31);
            this.lblTypeFilterText.Name = "lblTypeFilterText";
            this.lblTypeFilterText.Size = new System.Drawing.Size(34, 13);
            this.lblTypeFilterText.TabIndex = 53;
            this.lblTypeFilterText.Text = "Type:";
            // 
            // cBSpecialization
            // 
            this.cBSpecialization.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBSpecialization.FormattingEnabled = true;
            this.cBSpecialization.Location = new System.Drawing.Point(425, 28);
            this.cBSpecialization.Name = "cBSpecialization";
            this.cBSpecialization.Size = new System.Drawing.Size(171, 21);
            this.cBSpecialization.TabIndex = 52;
            // 
            // lblSpecializationFilterText
            // 
            this.lblSpecializationFilterText.AutoSize = true;
            this.lblSpecializationFilterText.Location = new System.Drawing.Point(344, 31);
            this.lblSpecializationFilterText.Name = "lblSpecializationFilterText";
            this.lblSpecializationFilterText.Size = new System.Drawing.Size(75, 13);
            this.lblSpecializationFilterText.TabIndex = 51;
            this.lblSpecializationFilterText.Text = "Specialization:";
            // 
            // cBCourse
            // 
            this.cBCourse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBCourse.FormattingEnabled = true;
            this.cBCourse.Location = new System.Drawing.Point(79, 28);
            this.cBCourse.Name = "cBCourse";
            this.cBCourse.Size = new System.Drawing.Size(171, 21);
            this.cBCourse.TabIndex = 50;
            // 
            // lblCourseFilterText
            // 
            this.lblCourseFilterText.AutoSize = true;
            this.lblCourseFilterText.Location = new System.Drawing.Point(30, 31);
            this.lblCourseFilterText.Name = "lblCourseFilterText";
            this.lblCourseFilterText.Size = new System.Drawing.Size(43, 13);
            this.lblCourseFilterText.TabIndex = 49;
            this.lblCourseFilterText.Text = "Course:";
            // 
            // dGVUpdateLibraryCard
            // 
            this.dGVUpdateLibraryCard.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGVUpdateLibraryCard.Location = new System.Drawing.Point(3, 72);
            this.dGVUpdateLibraryCard.Name = "dGVUpdateLibraryCard";
            this.dGVUpdateLibraryCard.ReadOnly = true;
            this.dGVUpdateLibraryCard.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dGVUpdateLibraryCard.Size = new System.Drawing.Size(1154, 216);
            this.dGVUpdateLibraryCard.TabIndex = 48;
            this.dGVUpdateLibraryCard.SelectionChanged += new System.EventHandler(this.dGVUpdateLibraryCard_SelectionChanged);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 1661);
            this.Controls.Add(this.pnlUpdateLibraryCard);
            this.Controls.Add(this.pnlReport);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.txtBoxLibraryCardNo);
            this.Controls.Add(this.lblLibraryCardNoText);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.btnSync);
            this.Controls.Add(this.btnTrack);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Attendance Tracker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dGVLibraryEntry)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlReport.ResumeLayout(false);
            this.pnlReport.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGVReport)).EndInit();
            this.pnlUpdateLibraryCard.ResumeLayout(false);
            this.pnlUpdateLibraryCard.PerformLayout();
            this.pnlUpdate.ResumeLayout(false);
            this.pnlUpdate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGVUpdateLibraryCard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnTrack;
        private System.Windows.Forms.Button btnSync;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.TextBox txtBoxLibraryCardNo;
        private System.Windows.Forms.Label lblLibraryCardNoText;
        private System.Windows.Forms.DataGridView dGVLibraryEntry;
        private System.Windows.Forms.Button btnStudentDataPull;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlReport;
        private System.Windows.Forms.DataGridView dGVReport;
        private System.Windows.Forms.RadioButton rBToday;
        private System.Windows.Forms.Label lblDateRangeText;
        private System.Windows.Forms.RadioButton rBWeekly;
        private System.Windows.Forms.RadioButton rBMonthly;
        private System.Windows.Forms.RadioButton rBYearly;
        private System.Windows.Forms.Button btnGetLibraryEntry;
        private System.Windows.Forms.Button btnHoursSpent;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Panel pnlUpdateLibraryCard;
        private System.Windows.Forms.Panel pnlUpdate;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblCourse;
        private System.Windows.Forms.Label lblRollNumber;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.Label lblCrn;
        private System.Windows.Forms.Label lblSpecialization;
        private System.Windows.Forms.TextBox txtBoxLibraryCardNoVal;
        private System.Windows.Forms.Label lblLibraryCardNoTextUpd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSpecializationText;
        private System.Windows.Forms.Label lblCourseText;
        private System.Windows.Forms.Label lblCrnText;
        private System.Windows.Forms.Label lblRollNumberText;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblLastNameText;
        private System.Windows.Forms.PictureBox picBoxUser;
        private System.Windows.Forms.Label lblFirstNameText;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cBType;
        private System.Windows.Forms.Label lblTypeFilterText;
        private System.Windows.Forms.ComboBox cBSpecialization;
        private System.Windows.Forms.Label lblSpecializationFilterText;
        private System.Windows.Forms.ComboBox cBCourse;
        private System.Windows.Forms.Label lblCourseFilterText;
        private System.Windows.Forms.DataGridView dGVUpdateLibraryCard;
    }
}

