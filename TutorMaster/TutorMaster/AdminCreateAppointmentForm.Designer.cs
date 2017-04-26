namespace TutorMaster
{
    partial class AdminCreateAppointmentForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminCreateAppointmentForm));
            this.cbxClasses = new System.Windows.Forms.ComboBox();
            this.cbxStudents = new System.Windows.Forms.ComboBox();
            this.lvTimeMatches = new System.Windows.Forms.ListView();
            this.cbWeekly = new System.Windows.Forms.CheckBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbxHour = new System.Windows.Forms.ComboBox();
            this.cbxMinutes = new System.Windows.Forms.ComboBox();
            this.tbxLocation = new System.Windows.Forms.TextBox();
            this.lblClasses = new System.Windows.Forms.Label();
            this.lblPartner = new System.Windows.Forms.Label();
            this.lblHours = new System.Windows.Forms.Label();
            this.lblMinutes = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnFindMatches = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxClasses
            // 
            this.cbxClasses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxClasses.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbxClasses.FormattingEnabled = true;
            this.cbxClasses.Location = new System.Drawing.Point(56, 61);
            this.cbxClasses.Name = "cbxClasses";
            this.cbxClasses.Size = new System.Drawing.Size(167, 21);
            this.cbxClasses.TabIndex = 0;
            this.cbxClasses.SelectedIndexChanged += new System.EventHandler(this.cbxClasses_SelectedIndexChanged);
            // 
            // cbxStudents
            // 
            this.cbxStudents.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxStudents.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbxStudents.FormattingEnabled = true;
            this.cbxStudents.Location = new System.Drawing.Point(241, 61);
            this.cbxStudents.Name = "cbxStudents";
            this.cbxStudents.Size = new System.Drawing.Size(167, 21);
            this.cbxStudents.TabIndex = 1;
            this.cbxStudents.SelectedIndexChanged += new System.EventHandler(this.cbxStudents_SelectedIndexChanged);
            // 
            // lvTimeMatches
            // 
            this.lvTimeMatches.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.lvTimeMatches.Location = new System.Drawing.Point(56, 210);
            this.lvTimeMatches.Name = "lvTimeMatches";
            this.lvTimeMatches.Size = new System.Drawing.Size(352, 194);
            this.lvTimeMatches.TabIndex = 2;
            this.lvTimeMatches.UseCompatibleStateImageBehavior = false;
            this.lvTimeMatches.View = System.Windows.Forms.View.Details;
            this.lvTimeMatches.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvTimeMatches_ItemCheck);
            this.lvTimeMatches.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvTimeMatches_ItemChecked);
            // 
            // cbWeekly
            // 
            this.cbWeekly.AutoSize = true;
            this.cbWeekly.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbWeekly.Location = new System.Drawing.Point(241, 171);
            this.cbWeekly.Name = "cbWeekly";
            this.cbWeekly.Size = new System.Drawing.Size(60, 17);
            this.cbWeekly.TabIndex = 3;
            this.cbWeekly.Text = "Weekly";
            this.cbWeekly.UseVisualStyleBackColor = true;
            this.cbWeekly.CheckedChanged += new System.EventHandler(this.cbWeekly_CheckedChanged);
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(222)))), ((int)(((byte)(229)))));
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSubmit.Location = new System.Drawing.Point(76, 410);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(151, 23);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "Set Appointment";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(222)))), ((int)(((byte)(229)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Location = new System.Drawing.Point(233, 410);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(151, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cbxHour
            // 
            this.cbxHour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxHour.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbxHour.FormattingEnabled = true;
            this.cbxHour.Items.AddRange(new object[] {
            "",
            "0",
            "1",
            "2",
            "3"});
            this.cbxHour.Location = new System.Drawing.Point(56, 116);
            this.cbxHour.Name = "cbxHour";
            this.cbxHour.Size = new System.Drawing.Size(167, 21);
            this.cbxHour.TabIndex = 6;
            // 
            // cbxMinutes
            // 
            this.cbxMinutes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMinutes.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbxMinutes.FormattingEnabled = true;
            this.cbxMinutes.Items.AddRange(new object[] {
            "",
            "00",
            "15",
            "30",
            "45"});
            this.cbxMinutes.Location = new System.Drawing.Point(241, 116);
            this.cbxMinutes.Name = "cbxMinutes";
            this.cbxMinutes.Size = new System.Drawing.Size(167, 21);
            this.cbxMinutes.TabIndex = 7;
            // 
            // tbxLocation
            // 
            this.tbxLocation.Location = new System.Drawing.Point(56, 170);
            this.tbxLocation.Name = "tbxLocation";
            this.tbxLocation.Size = new System.Drawing.Size(167, 20);
            this.tbxLocation.TabIndex = 8;
            // 
            // lblClasses
            // 
            this.lblClasses.AutoSize = true;
            this.lblClasses.Location = new System.Drawing.Point(53, 45);
            this.lblClasses.Name = "lblClasses";
            this.lblClasses.Size = new System.Drawing.Size(32, 13);
            this.lblClasses.TabIndex = 9;
            this.lblClasses.Text = "Class";
            // 
            // lblPartner
            // 
            this.lblPartner.AutoSize = true;
            this.lblPartner.Location = new System.Drawing.Point(238, 45);
            this.lblPartner.Name = "lblPartner";
            this.lblPartner.Size = new System.Drawing.Size(41, 13);
            this.lblPartner.TabIndex = 10;
            this.lblPartner.Text = "Partner";
            // 
            // lblHours
            // 
            this.lblHours.AutoSize = true;
            this.lblHours.Location = new System.Drawing.Point(53, 100);
            this.lblHours.Name = "lblHours";
            this.lblHours.Size = new System.Drawing.Size(85, 13);
            this.lblHours.TabIndex = 11;
            this.lblHours.Text = "Hours of session";
            // 
            // lblMinutes
            // 
            this.lblMinutes.AutoSize = true;
            this.lblMinutes.Location = new System.Drawing.Point(238, 100);
            this.lblMinutes.Name = "lblMinutes";
            this.lblMinutes.Size = new System.Drawing.Size(94, 13);
            this.lblMinutes.TabIndex = 12;
            this.lblMinutes.Text = "Minutes of session";
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(53, 154);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(98, 13);
            this.lblLocation.TabIndex = 13;
            this.lblLocation.Text = "Location of session";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TutorMaster.Properties.Resources.WatermarkR2;
            this.pictureBox1.Location = new System.Drawing.Point(375, 395);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(92, 98);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            // 
            // btnFindMatches
            // 
            this.btnFindMatches.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(222)))), ((int)(((byte)(229)))));
            this.btnFindMatches.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFindMatches.Location = new System.Drawing.Point(307, 168);
            this.btnFindMatches.Name = "btnFindMatches";
            this.btnFindMatches.Size = new System.Drawing.Size(101, 23);
            this.btnFindMatches.TabIndex = 21;
            this.btnFindMatches.Text = "Find Matches";
            this.btnFindMatches.UseVisualStyleBackColor = false;
            this.btnFindMatches.Click += new System.EventHandler(this.btnSetAppointment_Click);
            // 
            // AdminCreateAppointmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(232)))), ((int)(((byte)(239)))));
            this.ClientSize = new System.Drawing.Size(466, 492);
            this.Controls.Add(this.btnFindMatches);
            this.Controls.Add(this.lblLocation);
            this.Controls.Add(this.lblMinutes);
            this.Controls.Add(this.lblHours);
            this.Controls.Add(this.lblPartner);
            this.Controls.Add(this.lblClasses);
            this.Controls.Add(this.tbxLocation);
            this.Controls.Add(this.cbxMinutes);
            this.Controls.Add(this.cbxHour);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.cbWeekly);
            this.Controls.Add(this.lvTimeMatches);
            this.Controls.Add(this.cbxStudents);
            this.Controls.Add(this.cbxClasses);
            this.Controls.Add(this.pictureBox1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(71)))), ((int)(((byte)(76)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AdminCreateAppointmentForm";
            this.Text = "TutorMaster";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AdminCreateAppointmentForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxClasses;
        private System.Windows.Forms.ComboBox cbxStudents;
        private System.Windows.Forms.ListView lvTimeMatches;
        private System.Windows.Forms.CheckBox cbWeekly;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cbxHour;
        private System.Windows.Forms.ComboBox cbxMinutes;
        private System.Windows.Forms.TextBox tbxLocation;
        private System.Windows.Forms.Label lblClasses;
        private System.Windows.Forms.Label lblPartner;
        private System.Windows.Forms.Label lblHours;
        private System.Windows.Forms.Label lblMinutes;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnFindMatches;

    }
}