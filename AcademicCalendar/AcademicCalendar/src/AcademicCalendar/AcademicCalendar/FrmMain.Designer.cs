namespace AcademicCalendar
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.masterDataMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.facultyMasterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subjectMasterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.holidayMasterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commonTimeslotMasterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subjectTimeslotMapperToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.topicMasterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dGVCalendar = new System.Windows.Forms.DataGridView();
            this.btnNextWeek = new System.Windows.Forms.Button();
            this.cmbBoxWeeks = new System.Windows.Forms.ComboBox();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.cmbBoxYear = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbBoxTerm = new System.Windows.Forms.ComboBox();
            this.btnPdfExport = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGVCalendar)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.masterDataMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(984, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // masterDataMenuItem
            // 
            this.masterDataMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.facultyMasterToolStripMenuItem,
            this.subjectMasterToolStripMenuItem,
            this.holidayMasterToolStripMenuItem,
            this.commonTimeslotMasterToolStripMenuItem,
            this.subjectTimeslotMapperToolStripMenuItem,
            this.topicMasterToolStripMenuItem});
            this.masterDataMenuItem.Name = "masterDataMenuItem";
            this.masterDataMenuItem.Size = new System.Drawing.Size(82, 20);
            this.masterDataMenuItem.Text = "Master Data";
            // 
            // facultyMasterToolStripMenuItem
            // 
            this.facultyMasterToolStripMenuItem.Name = "facultyMasterToolStripMenuItem";
            this.facultyMasterToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.facultyMasterToolStripMenuItem.Text = "Faculty Master";
            this.facultyMasterToolStripMenuItem.Click += new System.EventHandler(this.facultyMasterToolStripMenuItem_Click);
            // 
            // subjectMasterToolStripMenuItem
            // 
            this.subjectMasterToolStripMenuItem.Name = "subjectMasterToolStripMenuItem";
            this.subjectMasterToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.subjectMasterToolStripMenuItem.Text = "Subject Master";
            this.subjectMasterToolStripMenuItem.Click += new System.EventHandler(this.subjectMasterToolStripMenuItem_Click);
            // 
            // holidayMasterToolStripMenuItem
            // 
            this.holidayMasterToolStripMenuItem.Name = "holidayMasterToolStripMenuItem";
            this.holidayMasterToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.holidayMasterToolStripMenuItem.Text = "Holiday Master";
            this.holidayMasterToolStripMenuItem.Click += new System.EventHandler(this.holidayMasterToolStripMenuItem_Click);
            // 
            // commonTimeslotMasterToolStripMenuItem
            // 
            this.commonTimeslotMasterToolStripMenuItem.Name = "commonTimeslotMasterToolStripMenuItem";
            this.commonTimeslotMasterToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.commonTimeslotMasterToolStripMenuItem.Text = "Common Timeslot Master";
            this.commonTimeslotMasterToolStripMenuItem.Click += new System.EventHandler(this.commonTimeslotMasterToolStripMenuItem_Click);
            // 
            // subjectTimeslotMapperToolStripMenuItem
            // 
            this.subjectTimeslotMapperToolStripMenuItem.Name = "subjectTimeslotMapperToolStripMenuItem";
            this.subjectTimeslotMapperToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.subjectTimeslotMapperToolStripMenuItem.Text = "Subject Timeslot Mapper";
            this.subjectTimeslotMapperToolStripMenuItem.Click += new System.EventHandler(this.subjectTimeslotMapperToolStripMenuItem_Click);
            // 
            // topicMasterToolStripMenuItem
            // 
            this.topicMasterToolStripMenuItem.Name = "topicMasterToolStripMenuItem";
            this.topicMasterToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.topicMasterToolStripMenuItem.Text = "Topic Master";
            this.topicMasterToolStripMenuItem.Click += new System.EventHandler(this.topicMasterToolStripMenuItem_Click);
            // 
            // dGVCalendar
            // 
            this.dGVCalendar.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dGVCalendar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGVCalendar.Location = new System.Drawing.Point(12, 83);
            this.dGVCalendar.Name = "dGVCalendar";
            this.dGVCalendar.ReadOnly = true;
            this.dGVCalendar.Size = new System.Drawing.Size(960, 366);
            this.dGVCalendar.TabIndex = 1;
            // 
            // btnNextWeek
            // 
            this.btnNextWeek.Location = new System.Drawing.Point(778, 42);
            this.btnNextWeek.Name = "btnNextWeek";
            this.btnNextWeek.Size = new System.Drawing.Size(75, 23);
            this.btnNextWeek.TabIndex = 2;
            this.btnNextWeek.Text = "Next";
            this.btnNextWeek.UseVisualStyleBackColor = true;
            this.btnNextWeek.Click += new System.EventHandler(this.btnNextWeek_Click);
            // 
            // cmbBoxWeeks
            // 
            this.cmbBoxWeeks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxWeeks.FormattingEnabled = true;
            this.cmbBoxWeeks.Location = new System.Drawing.Point(512, 44);
            this.cmbBoxWeeks.Name = "cmbBoxWeeks";
            this.cmbBoxWeeks.Size = new System.Drawing.Size(260, 21);
            this.cmbBoxWeeks.TabIndex = 3;
            this.cmbBoxWeeks.SelectedIndexChanged += new System.EventHandler(this.cmbBoxWeeks_SelectedIndexChanged);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Location = new System.Drawing.Point(431, 42);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(75, 23);
            this.btnPrevious.TabIndex = 4;
            this.btnPrevious.Text = "Previous";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // cmbBoxYear
            // 
            this.cmbBoxYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxYear.FormattingEnabled = true;
            this.cmbBoxYear.Location = new System.Drawing.Point(59, 44);
            this.cmbBoxYear.Name = "cmbBoxYear";
            this.cmbBoxYear.Size = new System.Drawing.Size(121, 21);
            this.cmbBoxYear.TabIndex = 5;
            this.cmbBoxYear.SelectedIndexChanged += new System.EventHandler(this.cmbBoxYear_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Year:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(217, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Term:";
            // 
            // cmbBoxTerm
            // 
            this.cmbBoxTerm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxTerm.FormattingEnabled = true;
            this.cmbBoxTerm.Location = new System.Drawing.Point(257, 44);
            this.cmbBoxTerm.Name = "cmbBoxTerm";
            this.cmbBoxTerm.Size = new System.Drawing.Size(121, 21);
            this.cmbBoxTerm.TabIndex = 8;
            // 
            // btnPdfExport
            // 
            this.btnPdfExport.Location = new System.Drawing.Point(877, 42);
            this.btnPdfExport.Name = "btnPdfExport";
            this.btnPdfExport.Size = new System.Drawing.Size(75, 23);
            this.btnPdfExport.TabIndex = 9;
            this.btnPdfExport.Text = "PDF Export";
            this.btnPdfExport.UseVisualStyleBackColor = true;
            this.btnPdfExport.Click += new System.EventHandler(this.btnPdfExport_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 461);
            this.Controls.Add(this.btnPdfExport);
            this.Controls.Add(this.cmbBoxTerm);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbBoxYear);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.cmbBoxWeeks);
            this.Controls.Add(this.btnNextWeek);
            this.Controls.Add(this.dGVCalendar);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Academic Calendar";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.SizeChanged += new System.EventHandler(this.FrmMain_SizeChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGVCalendar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem masterDataMenuItem;
        private System.Windows.Forms.ToolStripMenuItem facultyMasterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem subjectMasterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem holidayMasterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem commonTimeslotMasterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem subjectTimeslotMapperToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem topicMasterToolStripMenuItem;
        private System.Windows.Forms.DataGridView dGVCalendar;
        private System.Windows.Forms.Button btnNextWeek;
        private System.Windows.Forms.ComboBox cmbBoxWeeks;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.ComboBox cmbBoxYear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbBoxTerm;
        private System.Windows.Forms.Button btnPdfExport;
    }
}

