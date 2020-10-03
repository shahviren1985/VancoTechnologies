namespace AcademicCalendar.Forms.Mappers
{
    partial class FrmSubjectTimeslotMapDetails
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
            this.cmbBoxSubject = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbBoxDays = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dTPFrom = new System.Windows.Forms.DateTimePicker();
            this.dTPTo = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmbBoxSubject
            // 
            this.cmbBoxSubject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxSubject.FormattingEnabled = true;
            this.cmbBoxSubject.Location = new System.Drawing.Point(87, 34);
            this.cmbBoxSubject.Name = "cmbBoxSubject";
            this.cmbBoxSubject.Size = new System.Drawing.Size(336, 21);
            this.cmbBoxSubject.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Subject";
            // 
            // cmbBoxDays
            // 
            this.cmbBoxDays.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxDays.FormattingEnabled = true;
            this.cmbBoxDays.Location = new System.Drawing.Point(87, 83);
            this.cmbBoxDays.Name = "cmbBoxDays";
            this.cmbBoxDays.Size = new System.Drawing.Size(147, 21);
            this.cmbBoxDays.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Day";
            // 
            // dTPFrom
            // 
            this.dTPFrom.CustomFormat = "HH:mm";
            this.dTPFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dTPFrom.Location = new System.Drawing.Point(87, 134);
            this.dTPFrom.Name = "dTPFrom";
            this.dTPFrom.ShowUpDown = true;
            this.dTPFrom.Size = new System.Drawing.Size(95, 20);
            this.dTPFrom.TabIndex = 10;
            // 
            // dTPTo
            // 
            this.dTPTo.CustomFormat = "HH:mm";
            this.dTPTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dTPTo.Location = new System.Drawing.Point(252, 134);
            this.dTPTo.Name = "dTPTo";
            this.dTPTo.ShowUpDown = true;
            this.dTPTo.Size = new System.Drawing.Size(95, 20);
            this.dTPTo.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "From";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(226, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "To";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(41, 179);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // FrmSubjectTimeslotMapDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 229);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dTPTo);
            this.Controls.Add(this.dTPFrom);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbBoxDays);
            this.Controls.Add(this.cmbBoxSubject);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSubjectTimeslotMapDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Subject Timeslot Details";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbBoxSubject;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbBoxDays;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dTPFrom;
        private System.Windows.Forms.DateTimePicker dTPTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSave;
    }
}