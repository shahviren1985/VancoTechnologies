namespace AcademicCalendar.Forms.CommonTimeslotMaster
{
    partial class FrmCommonTimeslotDetails
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
            this.dTPFrom = new System.Windows.Forms.DateTimePicker();
            this.dTPTo = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBoxName = new System.Windows.Forms.TextBox();
            this.chckdLBDays = new System.Windows.Forms.CheckedListBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dTPFrom
            // 
            this.dTPFrom.CustomFormat = "HH:mm";
            this.dTPFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dTPFrom.Location = new System.Drawing.Point(67, 36);
            this.dTPFrom.Name = "dTPFrom";
            this.dTPFrom.ShowUpDown = true;
            this.dTPFrom.Size = new System.Drawing.Size(91, 20);
            this.dTPFrom.TabIndex = 0;
            // 
            // dTPTo
            // 
            this.dTPTo.CustomFormat = "HH:mm";
            this.dTPTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dTPTo.Location = new System.Drawing.Point(200, 36);
            this.dTPTo.Name = "dTPTo";
            this.dTPTo.ShowUpDown = true;
            this.dTPTo.Size = new System.Drawing.Size(111, 20);
            this.dTPTo.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "From";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(178, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "to";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Name";
            // 
            // txtBoxName
            // 
            this.txtBoxName.Location = new System.Drawing.Point(72, 95);
            this.txtBoxName.MaxLength = 255;
            this.txtBoxName.Name = "txtBoxName";
            this.txtBoxName.Size = new System.Drawing.Size(239, 20);
            this.txtBoxName.TabIndex = 5;
            // 
            // chckdLBDays
            // 
            this.chckdLBDays.FormattingEnabled = true;
            this.chckdLBDays.Location = new System.Drawing.Point(348, 12);
            this.chckdLBDays.Name = "chckdLBDays";
            this.chckdLBDays.Size = new System.Drawing.Size(169, 124);
            this.chckdLBDays.TabIndex = 6;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(34, 143);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // FrmCommonTimeslotDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 188);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chckdLBDays);
            this.Controls.Add(this.txtBoxName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dTPTo);
            this.Controls.Add(this.dTPFrom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCommonTimeslotDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Common Timeslot Details";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dTPFrom;
        private System.Windows.Forms.DateTimePicker dTPTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBoxName;
        private System.Windows.Forms.CheckedListBox chckdLBDays;
        private System.Windows.Forms.Button btnSave;
    }
}