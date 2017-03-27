namespace TutorMaster
{
    partial class RequestForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RequestForm));
            this.combEndAmPm = new System.Windows.Forms.ComboBox();
            this.combEndMinute = new System.Windows.Forms.ComboBox();
            this.combEndHour = new System.Windows.Forms.ComboBox();
            this.combStartAmPm = new System.Windows.Forms.ComboBox();
            this.combStartMinute = new System.Windows.Forms.ComboBox();
            this.combStartHour = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCourseName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnRequest = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.cbxWeekly = new System.Windows.Forms.CheckBox();
            this.dayStartDateTime = new System.Windows.Forms.DateTimePicker();
            this.dayEndDateTime = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // combEndAmPm
            // 
            this.combEndAmPm.FormattingEnabled = true;
            this.combEndAmPm.Items.AddRange(new object[] {
            "AM",
            "PM"});
            this.combEndAmPm.Location = new System.Drawing.Point(309, 90);
            this.combEndAmPm.Name = "combEndAmPm";
            this.combEndAmPm.Size = new System.Drawing.Size(61, 21);
            this.combEndAmPm.TabIndex = 11;
            // 
            // combEndMinute
            // 
            this.combEndMinute.FormattingEnabled = true;
            this.combEndMinute.Items.AddRange(new object[] {
            "00",
            "15",
            "30",
            "45"});
            this.combEndMinute.Location = new System.Drawing.Point(254, 90);
            this.combEndMinute.Name = "combEndMinute";
            this.combEndMinute.Size = new System.Drawing.Size(49, 21);
            this.combEndMinute.TabIndex = 10;
            // 
            // combEndHour
            // 
            this.combEndHour.FormattingEnabled = true;
            this.combEndHour.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12"});
            this.combEndHour.Location = new System.Drawing.Point(204, 91);
            this.combEndHour.Name = "combEndHour";
            this.combEndHour.Size = new System.Drawing.Size(44, 21);
            this.combEndHour.TabIndex = 9;
            // 
            // combStartAmPm
            // 
            this.combStartAmPm.FormattingEnabled = true;
            this.combStartAmPm.Items.AddRange(new object[] {
            "AM",
            "PM"});
            this.combStartAmPm.Location = new System.Drawing.Point(309, 65);
            this.combStartAmPm.Name = "combStartAmPm";
            this.combStartAmPm.Size = new System.Drawing.Size(61, 21);
            this.combStartAmPm.TabIndex = 8;
            // 
            // combStartMinute
            // 
            this.combStartMinute.FormattingEnabled = true;
            this.combStartMinute.Items.AddRange(new object[] {
            "00",
            "15",
            "30",
            "45"});
            this.combStartMinute.Location = new System.Drawing.Point(254, 65);
            this.combStartMinute.Name = "combStartMinute";
            this.combStartMinute.Size = new System.Drawing.Size(49, 21);
            this.combStartMinute.TabIndex = 7;
            // 
            // combStartHour
            // 
            this.combStartHour.BackColor = System.Drawing.SystemColors.Window;
            this.combStartHour.FormattingEnabled = true;
            this.combStartHour.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12"});
            this.combStartHour.Location = new System.Drawing.Point(204, 64);
            this.combStartHour.Name = "combStartHour";
            this.combStartHour.Size = new System.Drawing.Size(44, 21);
            this.combStartHour.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(376, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "End Time";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(376, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Start Time";
            // 
            // txtCourseName
            // 
            this.txtCourseName.Location = new System.Drawing.Point(114, 8);
            this.txtCourseName.Name = "txtCourseName";
            this.txtCourseName.Size = new System.Drawing.Size(100, 20);
            this.txtCourseName.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(220, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Course Name";
            // 
            // btnRequest
            // 
            this.btnRequest.Location = new System.Drawing.Point(13, 153);
            this.btnRequest.Name = "btnRequest";
            this.btnRequest.Size = new System.Drawing.Size(75, 23);
            this.btnRequest.TabIndex = 16;
            this.btnRequest.Text = "Request";
            this.btnRequest.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(94, 153);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(74, 23);
            this.btnExit.TabIndex = 17;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // cbxWeekly
            // 
            this.cbxWeekly.AutoSize = true;
            this.cbxWeekly.Location = new System.Drawing.Point(13, 118);
            this.cbxWeekly.Name = "cbxWeekly";
            this.cbxWeekly.Size = new System.Drawing.Size(165, 17);
            this.cbxWeekly.TabIndex = 18;
            this.cbxWeekly.Text = "Is this a weekly appointment?";
            this.cbxWeekly.UseVisualStyleBackColor = true;
            // 
            // dayStartDateTime
            // 
            this.dayStartDateTime.Location = new System.Drawing.Point(-2, 65);
            this.dayStartDateTime.MaxDate = new System.DateTime(2017, 4, 30, 0, 0, 0, 0);
            this.dayStartDateTime.MinDate = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
            this.dayStartDateTime.Name = "dayStartDateTime";
            this.dayStartDateTime.Size = new System.Drawing.Size(200, 20);
            this.dayStartDateTime.TabIndex = 27;
            this.dayStartDateTime.Value = new System.DateTime(2017, 3, 23, 0, 0, 0, 0);
            // 
            // dayEndDateTime
            // 
            this.dayEndDateTime.Location = new System.Drawing.Point(-2, 91);
            this.dayEndDateTime.MaxDate = new System.DateTime(2017, 4, 30, 0, 0, 0, 0);
            this.dayEndDateTime.MinDate = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
            this.dayEndDateTime.Name = "dayEndDateTime";
            this.dayEndDateTime.Size = new System.Drawing.Size(200, 20);
            this.dayEndDateTime.TabIndex = 28;
            this.dayEndDateTime.Value = new System.DateTime(2017, 3, 23, 0, 0, 0, 0);
            // 
            // RequestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 198);
            this.Controls.Add(this.dayEndDateTime);
            this.Controls.Add(this.dayStartDateTime);
            this.Controls.Add(this.cbxWeekly);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnRequest);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCourseName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.combEndAmPm);
            this.Controls.Add(this.combEndMinute);
            this.Controls.Add(this.combEndHour);
            this.Controls.Add(this.combStartAmPm);
            this.Controls.Add(this.combStartMinute);
            this.Controls.Add(this.combStartHour);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RequestForm";
            this.Text = "RequestForm";
            this.Load += new System.EventHandler(this.RequestForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox combEndAmPm;
        private System.Windows.Forms.ComboBox combEndMinute;
        private System.Windows.Forms.ComboBox combEndHour;
        private System.Windows.Forms.ComboBox combStartAmPm;
        private System.Windows.Forms.ComboBox combStartMinute;
        private System.Windows.Forms.ComboBox combStartHour;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCourseName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRequest;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.CheckBox cbxWeekly;
        private System.Windows.Forms.DateTimePicker dayStartDateTime;
        private System.Windows.Forms.DateTimePicker dayEndDateTime;
    }
}