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
            this.lvTutorAvailability = new System.Windows.Forms.ListView();
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
            this.SuspendLayout();
            // 
            // combTutorNameLeft
            // 
            this.combTutorNameLeft.FormattingEnabled = true;
            this.combTutorNameLeft.Location = new System.Drawing.Point(24, 116);
            this.combTutorNameLeft.Name = "combTutorNameLeft";
            this.combTutorNameLeft.Size = new System.Drawing.Size(93, 21);
            this.combTutorNameLeft.Sorted = true;
            this.combTutorNameLeft.TabIndex = 3;
            this.combTutorNameLeft.SelectedIndexChanged += new System.EventHandler(this.combTutorName_SelectedIndexChanged);
            // 
            // lvTutorAvailability
            // 
            this.lvTutorAvailability.Location = new System.Drawing.Point(207, 361);
            this.lvTutorAvailability.Name = "lvTutorAvailability";
            this.lvTutorAvailability.Size = new System.Drawing.Size(203, 125);
            this.lvTutorAvailability.TabIndex = 5;
            this.lvTutorAvailability.UseCompatibleStateImageBehavior = false;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(664, 12);
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
            this.lblTutorName.Location = new System.Drawing.Point(21, 87);
            this.lblTutorName.Name = "lblTutorName";
            this.lblTutorName.Size = new System.Drawing.Size(63, 13);
            this.lblTutorName.TabIndex = 16;
            this.lblTutorName.Text = "Tutor Name";
            // 
            // lblClassesAvailable
            // 
            this.lblClassesAvailable.AutoSize = true;
            this.lblClassesAvailable.Location = new System.Drawing.Point(21, 154);
            this.lblClassesAvailable.Name = "lblClassesAvailable";
            this.lblClassesAvailable.Size = new System.Drawing.Size(89, 13);
            this.lblClassesAvailable.TabIndex = 17;
            this.lblClassesAvailable.Text = "Classes Available";
            // 
            // lblAvailableTimes
            // 
            this.lblAvailableTimes.AutoSize = true;
            this.lblAvailableTimes.Location = new System.Drawing.Point(260, 331);
            this.lblAvailableTimes.Name = "lblAvailableTimes";
            this.lblAvailableTimes.Size = new System.Drawing.Size(81, 13);
            this.lblAvailableTimes.TabIndex = 18;
            this.lblAvailableTimes.Text = "Available Times";
            // 
            // lblChoice
            // 
            this.lblChoice.AutoSize = true;
            this.lblChoice.Location = new System.Drawing.Point(264, 22);
            this.lblChoice.Name = "lblChoice";
            this.lblChoice.Size = new System.Drawing.Size(138, 13);
            this.lblChoice.TabIndex = 19;
            this.lblChoice.Text = "Search by tutor or by class?";
            // 
            // btnByTutor
            // 
            this.btnByTutor.Location = new System.Drawing.Point(228, 38);
            this.btnByTutor.Name = "btnByTutor";
            this.btnByTutor.Size = new System.Drawing.Size(75, 23);
            this.btnByTutor.TabIndex = 20;
            this.btnByTutor.Text = "Tutor";
            this.btnByTutor.UseVisualStyleBackColor = true;
            this.btnByTutor.Click += new System.EventHandler(this.btnByTutor_Click);
            // 
            // btnByClass
            // 
            this.btnByClass.Location = new System.Drawing.Point(377, 38);
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
            this.lblClasses.Location = new System.Drawing.Point(445, 106);
            this.lblClasses.Name = "lblClasses";
            this.lblClasses.Size = new System.Drawing.Size(43, 13);
            this.lblClasses.TabIndex = 22;
            this.lblClasses.Text = "Classes";
            // 
            // combClassBoxRight
            // 
            this.combClassBoxRight.FormattingEnabled = true;
            this.combClassBoxRight.Location = new System.Drawing.Point(448, 131);
            this.combClassBoxRight.Name = "combClassBoxRight";
            this.combClassBoxRight.Size = new System.Drawing.Size(121, 21);
            this.combClassBoxRight.TabIndex = 23;
            this.combClassBoxRight.SelectedIndexChanged += new System.EventHandler(this.combClassBox_SelectedIndexChanged);
            // 
            // combTutorNameRight
            // 
            this.combTutorNameRight.FormattingEnabled = true;
            this.combTutorNameRight.Location = new System.Drawing.Point(448, 185);
            this.combTutorNameRight.Name = "combTutorNameRight";
            this.combTutorNameRight.Size = new System.Drawing.Size(121, 21);
            this.combTutorNameRight.TabIndex = 24;
            this.combTutorNameRight.SelectedIndexChanged += new System.EventHandler(this.combTutorName2_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(445, 168);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Available Tutors";
            // 
            // btnSendRequest
            // 
            this.btnSendRequest.Location = new System.Drawing.Point(267, 490);
            this.btnSendRequest.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSendRequest.Name = "btnSendRequest";
            this.btnSendRequest.Size = new System.Drawing.Size(88, 32);
            this.btnSendRequest.TabIndex = 26;
            this.btnSendRequest.Text = "Send Request";
            this.btnSendRequest.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(527, 296);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(183, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Do none of these times work for you?";
            // 
            // btnManualTime
            // 
            this.btnManualTime.Location = new System.Drawing.Point(561, 320);
            this.btnManualTime.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnManualTime.Name = "btnManualTime";
            this.btnManualTime.Size = new System.Drawing.Size(115, 25);
            this.btnManualTime.TabIndex = 28;
            this.btnManualTime.Text = "Enter My Own Time";
            this.btnManualTime.UseVisualStyleBackColor = true;
            // 
            // lblHowLong
            // 
            this.lblHowLong.AutoSize = true;
            this.lblHowLong.Location = new System.Drawing.Point(225, 87);
            this.lblHowLong.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHowLong.Name = "lblHowLong";
            this.lblHowLong.Size = new System.Drawing.Size(157, 13);
            this.lblHowLong.TabIndex = 29;
            this.lblHowLong.Text = "How long do you want to meet?";
            // 
            // combMeetingLength
            // 
            this.combMeetingLength.FormattingEnabled = true;
            this.combMeetingLength.Items.AddRange(new object[] {
            "15 minutes",
            "30 minutes",
            "45 minutes",
            "1 hour",
            "1 hour and 15 minutes",
            "1 hour and 30 minutes",
            "1 hour and 45 minutes",
            "2 hours",
            "2 hours and 15 minutes",
            "2 hours and 30 minutes",
            "2 hours and 45 minutes",
            "3 hours"});
            this.combMeetingLength.Location = new System.Drawing.Point(258, 125);
            this.combMeetingLength.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.combMeetingLength.Name = "combMeetingLength";
            this.combMeetingLength.Size = new System.Drawing.Size(82, 21);
            this.combMeetingLength.TabIndex = 30;
            // 
            // cbxWeekly
            // 
            this.cbxWeekly.AutoSize = true;
            this.cbxWeekly.Location = new System.Drawing.Point(237, 205);
            this.cbxWeekly.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbxWeekly.Name = "cbxWeekly";
            this.cbxWeekly.Size = new System.Drawing.Size(162, 17);
            this.cbxWeekly.TabIndex = 31;
            this.cbxWeekly.Text = "This is a weekly appointment";
            this.cbxWeekly.UseVisualStyleBackColor = true;
            // 
            // combClassBoxLeft
            // 
            this.combClassBoxLeft.FormattingEnabled = true;
            this.combClassBoxLeft.Location = new System.Drawing.Point(24, 179);
            this.combClassBoxLeft.Name = "combClassBoxLeft";
            this.combClassBoxLeft.Size = new System.Drawing.Size(121, 21);
            this.combClassBoxLeft.TabIndex = 32;
            // 
            // AdvancedRequest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 542);
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
            this.Controls.Add(this.lvTutorAvailability);
            this.Controls.Add(this.combTutorNameLeft);
            this.Name = "AdvancedRequest";
            this.Text = "AdvancedRequest";
            this.Load += new System.EventHandler(this.AdvancedRequest_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox combTutorNameLeft;
        private System.Windows.Forms.ListView lvTutorAvailability;
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
    }
}