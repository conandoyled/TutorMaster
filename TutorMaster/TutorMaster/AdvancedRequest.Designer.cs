namespace TutorMaster
{
    partial class AdvancedRequest
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
            this.combTutorNameLeft = new System.Windows.Forms.ComboBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblTutorName = new System.Windows.Forms.Label();
            this.lblClassesAvailable = new System.Windows.Forms.Label();
            this.lblAvailableTimes = new System.Windows.Forms.Label();
            this.lblChoice = new System.Windows.Forms.Label();
            this.btnByTutor = new System.Windows.Forms.Button();
            this.btnByClass = new System.Windows.Forms.Button();
            this.lblClasses = new System.Windows.Forms.Label();
            this.combClassBoxRight = new System.Windows.Forms.ComboBox();
            this.combTutorNameRight = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSendRequest = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnManualTime = new System.Windows.Forms.Button();
            this.lblHowLong = new System.Windows.Forms.Label();
            this.combMeetingLength = new System.Windows.Forms.ComboBox();
            this.cbxWeekly = new System.Windows.Forms.CheckBox();
            this.combClassBoxLeft = new System.Windows.Forms.ComboBox();
            this.combTutorAvailability = new System.Windows.Forms.ComboBox();
            this.btnFindMatches = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.dayStartDateTime = new System.Windows.Forms.DateTimePicker();
            this.combStartMinute = new System.Windows.Forms.ComboBox();
            this.combStartAmPm = new System.Windows.Forms.ComboBox();
            this.combStartHour = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // combTutorNameLeft
            // 
            this.combTutorNameLeft.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combTutorNameLeft.FormattingEnabled = true;
            this.combTutorNameLeft.Location = new System.Drawing.Point(63, 69);
            this.combTutorNameLeft.Name = "combTutorNameLeft";
            this.combTutorNameLeft.Size = new System.Drawing.Size(173, 21);
            this.combTutorNameLeft.Sorted = true;
            this.combTutorNameLeft.TabIndex = 3;
            this.combTutorNameLeft.SelectedIndexChanged += new System.EventHandler(this.combTutorNameLeft_SelectedIndexChanged);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(99, 450);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(101, 23);
            this.btnExit.TabIndex = 15;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblTutorName
            // 
            this.lblTutorName.AutoSize = true;
            this.lblTutorName.Location = new System.Drawing.Point(118, 55);
            this.lblTutorName.Name = "lblTutorName";
            this.lblTutorName.Size = new System.Drawing.Size(63, 13);
            this.lblTutorName.TabIndex = 16;
            this.lblTutorName.Text = "Tutor Name";
            // 
            // lblClassesAvailable
            // 
            this.lblClassesAvailable.AutoSize = true;
            this.lblClassesAvailable.Location = new System.Drawing.Point(105, 167);
            this.lblClassesAvailable.Name = "lblClassesAvailable";
            this.lblClassesAvailable.Size = new System.Drawing.Size(89, 13);
            this.lblClassesAvailable.TabIndex = 17;
            this.lblClassesAvailable.Text = "Classes Available";
            // 
            // lblAvailableTimes
            // 
            this.lblAvailableTimes.AutoSize = true;
            this.lblAvailableTimes.Location = new System.Drawing.Point(109, 240);
            this.lblAvailableTimes.Name = "lblAvailableTimes";
            this.lblAvailableTimes.Size = new System.Drawing.Size(81, 13);
            this.lblAvailableTimes.TabIndex = 18;
            this.lblAvailableTimes.Text = "Available Times";
            // 
            // lblChoice
            // 
            this.lblChoice.AutoSize = true;
            this.lblChoice.Location = new System.Drawing.Point(80, 9);
            this.lblChoice.Name = "lblChoice";
            this.lblChoice.Size = new System.Drawing.Size(138, 13);
            this.lblChoice.TabIndex = 19;
            this.lblChoice.Text = "Search by tutor or by class?";
            // 
            // btnByTutor
            // 
            this.btnByTutor.Location = new System.Drawing.Point(74, 25);
            this.btnByTutor.Name = "btnByTutor";
            this.btnByTutor.Size = new System.Drawing.Size(75, 23);
            this.btnByTutor.TabIndex = 20;
            this.btnByTutor.Text = "Tutor";
            this.btnByTutor.UseVisualStyleBackColor = true;
            this.btnByTutor.Click += new System.EventHandler(this.btnByTutor_Click);
            // 
            // btnByClass
            // 
            this.btnByClass.Location = new System.Drawing.Point(155, 25);
            this.btnByClass.Name = "btnByClass";
            this.btnByClass.Size = new System.Drawing.Size(75, 23);
            this.btnByClass.TabIndex = 21;
            this.btnByClass.Text = "Class";
            this.btnByClass.UseVisualStyleBackColor = true;
            this.btnByClass.Click += new System.EventHandler(this.btnByClass_Click);
            // 
            // lblClasses
            // 
            this.lblClasses.AutoSize = true;
            this.lblClasses.Location = new System.Drawing.Point(128, 55);
            this.lblClasses.Name = "lblClasses";
            this.lblClasses.Size = new System.Drawing.Size(43, 13);
            this.lblClasses.TabIndex = 22;
            this.lblClasses.Text = "Classes";
            // 
            // combClassBoxRight
            // 
            this.combClassBoxRight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combClassBoxRight.FormattingEnabled = true;
            this.combClassBoxRight.Location = new System.Drawing.Point(62, 69);
            this.combClassBoxRight.Name = "combClassBoxRight";
            this.combClassBoxRight.Size = new System.Drawing.Size(175, 21);
            this.combClassBoxRight.TabIndex = 23;
            this.combClassBoxRight.SelectedIndexChanged += new System.EventHandler(this.combClassBoxRight_SelectedIndexChanged);
            // 
            // combTutorNameRight
            // 
            this.combTutorNameRight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combTutorNameRight.FormattingEnabled = true;
            this.combTutorNameRight.Location = new System.Drawing.Point(62, 187);
            this.combTutorNameRight.Name = "combTutorNameRight";
            this.combTutorNameRight.Size = new System.Drawing.Size(175, 21);
            this.combTutorNameRight.TabIndex = 24;
            this.combTutorNameRight.SelectedIndexChanged += new System.EventHandler(this.combTutorNameRight_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(108, 167);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Available Tutors";
            // 
            // btnSendRequest
            // 
            this.btnSendRequest.Location = new System.Drawing.Point(99, 401);
            this.btnSendRequest.Margin = new System.Windows.Forms.Padding(2);
            this.btnSendRequest.Name = "btnSendRequest";
            this.btnSendRequest.Size = new System.Drawing.Size(101, 32);
            this.btnSendRequest.TabIndex = 26;
            this.btnSendRequest.Text = "Send Request";
            this.btnSendRequest.UseVisualStyleBackColor = true;
            this.btnSendRequest.Click += new System.EventHandler(this.btnSendRequest_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 302);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(183, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Do none of these times work for you?";
            // 
            // btnManualTime
            // 
            this.btnManualTime.Location = new System.Drawing.Point(92, 317);
            this.btnManualTime.Margin = new System.Windows.Forms.Padding(2);
            this.btnManualTime.Name = "btnManualTime";
            this.btnManualTime.Size = new System.Drawing.Size(115, 25);
            this.btnManualTime.TabIndex = 28;
            this.btnManualTime.Text = "Enter My Own Time";
            this.btnManualTime.UseVisualStyleBackColor = true;
            this.btnManualTime.Click += new System.EventHandler(this.btnManualTime_Click);
            // 
            // lblHowLong
            // 
            this.lblHowLong.AutoSize = true;
            this.lblHowLong.Location = new System.Drawing.Point(71, 93);
            this.lblHowLong.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHowLong.Name = "lblHowLong";
            this.lblHowLong.Size = new System.Drawing.Size(157, 13);
            this.lblHowLong.TabIndex = 29;
            this.lblHowLong.Text = "How long do you want to meet?";
            // 
            // combMeetingLength
            // 
            this.combMeetingLength.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combMeetingLength.FormattingEnabled = true;
            this.combMeetingLength.Items.AddRange(new object[] {
            "15",
            "30",
            "45",
            "60",
            "75",
            "90",
            "105",
            "120",
            "135",
            "150",
            "165",
            "180"});
            this.combMeetingLength.Location = new System.Drawing.Point(62, 124);
            this.combMeetingLength.Margin = new System.Windows.Forms.Padding(2);
            this.combMeetingLength.Name = "combMeetingLength";
            this.combMeetingLength.Size = new System.Drawing.Size(175, 21);
            this.combMeetingLength.TabIndex = 30;
            this.combMeetingLength.SelectedIndexChanged += new System.EventHandler(this.combMeetingLength_SelectedIndexChanged);
            // 
            // cbxWeekly
            // 
            this.cbxWeekly.AutoSize = true;
            this.cbxWeekly.Location = new System.Drawing.Point(68, 149);
            this.cbxWeekly.Margin = new System.Windows.Forms.Padding(2);
            this.cbxWeekly.Name = "cbxWeekly";
            this.cbxWeekly.Size = new System.Drawing.Size(162, 17);
            this.cbxWeekly.TabIndex = 31;
            this.cbxWeekly.Text = "This is a weekly appointment";
            this.cbxWeekly.UseVisualStyleBackColor = true;
            // 
            // combClassBoxLeft
            // 
            this.combClassBoxLeft.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combClassBoxLeft.FormattingEnabled = true;
            this.combClassBoxLeft.Location = new System.Drawing.Point(62, 187);
            this.combClassBoxLeft.Name = "combClassBoxLeft";
            this.combClassBoxLeft.Size = new System.Drawing.Size(175, 21);
            this.combClassBoxLeft.TabIndex = 32;
            this.combClassBoxLeft.SelectedIndexChanged += new System.EventHandler(this.combClassBoxLeft_SelectedIndexChanged);
            // 
            // combTutorAvailability
            // 
            this.combTutorAvailability.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combTutorAvailability.FormattingEnabled = true;
            this.combTutorAvailability.Location = new System.Drawing.Point(25, 267);
            this.combTutorAvailability.Name = "combTutorAvailability";
            this.combTutorAvailability.Size = new System.Drawing.Size(248, 21);
            this.combTutorAvailability.TabIndex = 33;
            // 
            // btnFindMatches
            // 
            this.btnFindMatches.Location = new System.Drawing.Point(107, 214);
            this.btnFindMatches.Name = "btnFindMatches";
            this.btnFindMatches.Size = new System.Drawing.Size(84, 23);
            this.btnFindMatches.TabIndex = 34;
            this.btnFindMatches.Text = "Find Matches";
            this.btnFindMatches.UseVisualStyleBackColor = true;
            this.btnFindMatches.Click += new System.EventHandler(this.btnFindMatches_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(118, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 35;
            this.label3.Text = "(In Minutes)";
            // 
            // dayStartDateTime
            // 
            this.dayStartDateTime.Location = new System.Drawing.Point(50, 349);
            this.dayStartDateTime.MaxDate = new System.DateTime(2017, 4, 30, 0, 0, 0, 0);
            this.dayStartDateTime.MinDate = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
            this.dayStartDateTime.Name = "dayStartDateTime";
            this.dayStartDateTime.Size = new System.Drawing.Size(200, 20);
            this.dayStartDateTime.TabIndex = 40;
            this.dayStartDateTime.Value = new System.DateTime(2017, 3, 23, 0, 0, 0, 0);
            // 
            // combStartMinute
            // 
            this.combStartMinute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combStartMinute.FormattingEnabled = true;
            this.combStartMinute.Items.AddRange(new object[] {
            "00",
            "15",
            "30",
            "45"});
            this.combStartMinute.Location = new System.Drawing.Point(122, 375);
            this.combStartMinute.Name = "combStartMinute";
            this.combStartMinute.Size = new System.Drawing.Size(49, 21);
            this.combStartMinute.TabIndex = 37;
            // 
            // combStartAmPm
            // 
            this.combStartAmPm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combStartAmPm.FormattingEnabled = true;
            this.combStartAmPm.Items.AddRange(new object[] {
            "AM",
            "PM"});
            this.combStartAmPm.Location = new System.Drawing.Point(177, 375);
            this.combStartAmPm.Name = "combStartAmPm";
            this.combStartAmPm.Size = new System.Drawing.Size(50, 21);
            this.combStartAmPm.TabIndex = 38;
            // 
            // combStartHour
            // 
            this.combStartHour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.combStartHour.Location = new System.Drawing.Point(72, 375);
            this.combStartHour.Name = "combStartHour";
            this.combStartHour.Size = new System.Drawing.Size(44, 21);
            this.combStartHour.TabIndex = 36;
            // 
            // AdvancedRequest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 476);
            this.Controls.Add(this.dayStartDateTime);
            this.Controls.Add(this.combStartMinute);
            this.Controls.Add(this.combStartAmPm);
            this.Controls.Add(this.combStartHour);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnFindMatches);
            this.Controls.Add(this.combTutorAvailability);
            this.Controls.Add(this.combClassBoxLeft);
            this.Controls.Add(this.cbxWeekly);
            this.Controls.Add(this.combMeetingLength);
            this.Controls.Add(this.lblHowLong);
            this.Controls.Add(this.btnManualTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSendRequest);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.combTutorNameRight);
            this.Controls.Add(this.combClassBoxRight);
            this.Controls.Add(this.lblClasses);
            this.Controls.Add(this.btnByClass);
            this.Controls.Add(this.btnByTutor);
            this.Controls.Add(this.lblChoice);
            this.Controls.Add(this.lblAvailableTimes);
            this.Controls.Add(this.lblClassesAvailable);
            this.Controls.Add(this.lblTutorName);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.combTutorNameLeft);
            this.Name = "AdvancedRequest";
            this.Text = "How many minutes ";
            this.Load += new System.EventHandler(this.AdvancedRequest_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox combTutorNameLeft;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblTutorName;
        private System.Windows.Forms.Label lblClassesAvailable;
        private System.Windows.Forms.Label lblAvailableTimes;
        private System.Windows.Forms.Label lblChoice;
        private System.Windows.Forms.Button btnByTutor;
        private System.Windows.Forms.Button btnByClass;
        private System.Windows.Forms.Label lblClasses;
        private System.Windows.Forms.ComboBox combClassBoxRight;
        private System.Windows.Forms.ComboBox combTutorNameRight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSendRequest;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnManualTime;
        private System.Windows.Forms.Label lblHowLong;
        private System.Windows.Forms.ComboBox combMeetingLength;
        private System.Windows.Forms.CheckBox cbxWeekly;
        private System.Windows.Forms.ComboBox combClassBoxLeft;
        private System.Windows.Forms.ComboBox combTutorAvailability;
        private System.Windows.Forms.Button btnFindMatches;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dayStartDateTime;
        private System.Windows.Forms.ComboBox combStartMinute;
        private System.Windows.Forms.ComboBox combStartAmPm;
        private System.Windows.Forms.ComboBox combStartHour;
    }
}