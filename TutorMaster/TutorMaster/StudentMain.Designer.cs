namespace TutorMaster
{
    partial class StudentMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StudentMain));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabAvailability = new System.Windows.Forms.TabPage();
            this.dateTime = new System.Windows.Forms.DateTimePicker();
            this.cbxWeekly = new System.Windows.Forms.CheckBox();
            this.combEndDay = new System.Windows.Forms.ComboBox();
            this.combStartDay = new System.Windows.Forms.ComboBox();
            this.dayTabs = new System.Windows.Forms.TabControl();
            this.tabSunday = new System.Windows.Forms.TabPage();
            this.lvSunday = new System.Windows.Forms.ListView();
            this.tabMonday = new System.Windows.Forms.TabPage();
            this.lvMonday = new System.Windows.Forms.ListView();
            this.tabTuesday = new System.Windows.Forms.TabPage();
            this.lvTuesday = new System.Windows.Forms.ListView();
            this.tabWednesday = new System.Windows.Forms.TabPage();
            this.lvWednesday = new System.Windows.Forms.ListView();
            this.tabThursday = new System.Windows.Forms.TabPage();
            this.lvThursday = new System.Windows.Forms.ListView();
            this.tabFriday = new System.Windows.Forms.TabPage();
            this.lvFriday = new System.Windows.Forms.ListView();
            this.tabSaturday = new System.Windows.Forms.TabPage();
            this.lvSaturday = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddOpenBlock = new System.Windows.Forms.Button();
            this.combEndAmPm = new System.Windows.Forms.ComboBox();
            this.combEndMinute = new System.Windows.Forms.ComboBox();
            this.combEndHour = new System.Windows.Forms.ComboBox();
            this.combStartAmPm = new System.Windows.Forms.ComboBox();
            this.combStartMinute = new System.Windows.Forms.ComboBox();
            this.combStartHour = new System.Windows.Forms.ComboBox();
            this.tabAppointments = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabAccepted = new System.Windows.Forms.TabPage();
            this.lbxAccepted = new System.Windows.Forms.ListBox();
            this.tabPendingTutor = new System.Windows.Forms.TabPage();
            this.cbxPendingTutor = new System.Windows.Forms.CheckedListBox();
            this.tabPendingTutee = new System.Windows.Forms.TabPage();
            this.cbxPendingTutee = new System.Windows.Forms.CheckedListBox();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnRequest = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabAvailability.SuspendLayout();
            this.dayTabs.SuspendLayout();
            this.tabSunday.SuspendLayout();
            this.tabMonday.SuspendLayout();
            this.tabTuesday.SuspendLayout();
            this.tabWednesday.SuspendLayout();
            this.tabThursday.SuspendLayout();
            this.tabFriday.SuspendLayout();
            this.tabSaturday.SuspendLayout();
            this.tabAppointments.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabAccepted.SuspendLayout();
            this.tabPendingTutor.SuspendLayout();
            this.tabPendingTutee.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabAvailability);
            this.tabControl1.Controls.Add(this.tabAppointments);
            this.tabControl1.Location = new System.Drawing.Point(13, 69);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(837, 332);
            this.tabControl1.TabIndex = 0;
            // 
            // tabAvailability
            // 
            this.tabAvailability.BackColor = System.Drawing.Color.Transparent;
            this.tabAvailability.Controls.Add(this.dateTime);
            this.tabAvailability.Controls.Add(this.cbxWeekly);
            this.tabAvailability.Controls.Add(this.combEndDay);
            this.tabAvailability.Controls.Add(this.combStartDay);
            this.tabAvailability.Controls.Add(this.dayTabs);
            this.tabAvailability.Controls.Add(this.label2);
            this.tabAvailability.Controls.Add(this.label1);
            this.tabAvailability.Controls.Add(this.btnAddOpenBlock);
            this.tabAvailability.Controls.Add(this.combEndAmPm);
            this.tabAvailability.Controls.Add(this.combEndMinute);
            this.tabAvailability.Controls.Add(this.combEndHour);
            this.tabAvailability.Controls.Add(this.combStartAmPm);
            this.tabAvailability.Controls.Add(this.combStartMinute);
            this.tabAvailability.Controls.Add(this.combStartHour);
            this.tabAvailability.Location = new System.Drawing.Point(4, 22);
            this.tabAvailability.Name = "tabAvailability";
            this.tabAvailability.Padding = new System.Windows.Forms.Padding(3);
            this.tabAvailability.Size = new System.Drawing.Size(829, 306);
            this.tabAvailability.TabIndex = 0;
            this.tabAvailability.Text = "Availability";
            // 
            // dateTime
            // 
            this.dateTime.Location = new System.Drawing.Point(391, 35);
            this.dateTime.Name = "dateTime";
            this.dateTime.Size = new System.Drawing.Size(200, 20);
            this.dateTime.TabIndex = 13;
            // 
            // cbxWeekly
            // 
            this.cbxWeekly.AutoSize = true;
            this.cbxWeekly.Location = new System.Drawing.Point(385, 157);
            this.cbxWeekly.Name = "cbxWeekly";
            this.cbxWeekly.Size = new System.Drawing.Size(62, 17);
            this.cbxWeekly.TabIndex = 12;
            this.cbxWeekly.Text = "Weekly";
            this.cbxWeekly.UseVisualStyleBackColor = true;
            // 
            // combEndDay
            // 
            this.combEndDay.FormattingEnabled = true;
            this.combEndDay.Items.AddRange(new object[] {
            "Sunday",
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday",
            "Saturday"});
            this.combEndDay.Location = new System.Drawing.Point(385, 116);
            this.combEndDay.Name = "combEndDay";
            this.combEndDay.Size = new System.Drawing.Size(101, 21);
            this.combEndDay.TabIndex = 11;
            // 
            // combStartDay
            // 
            this.combStartDay.FormattingEnabled = true;
            this.combStartDay.Items.AddRange(new object[] {
            "Sunday",
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday",
            "Saturday"});
            this.combStartDay.Location = new System.Drawing.Point(385, 89);
            this.combStartDay.Name = "combStartDay";
            this.combStartDay.Size = new System.Drawing.Size(101, 21);
            this.combStartDay.TabIndex = 10;
            // 
            // dayTabs
            // 
            this.dayTabs.Controls.Add(this.tabSunday);
            this.dayTabs.Controls.Add(this.tabMonday);
            this.dayTabs.Controls.Add(this.tabTuesday);
            this.dayTabs.Controls.Add(this.tabWednesday);
            this.dayTabs.Controls.Add(this.tabThursday);
            this.dayTabs.Controls.Add(this.tabFriday);
            this.dayTabs.Controls.Add(this.tabSaturday);
            this.dayTabs.Location = new System.Drawing.Point(3, 13);
            this.dayTabs.Name = "dayTabs";
            this.dayTabs.SelectedIndex = 0;
            this.dayTabs.Size = new System.Drawing.Size(376, 287);
            this.dayTabs.TabIndex = 9;
            // 
            // tabSunday
            // 
            this.tabSunday.Controls.Add(this.lvSunday);
            this.tabSunday.Location = new System.Drawing.Point(4, 22);
            this.tabSunday.Name = "tabSunday";
            this.tabSunday.Padding = new System.Windows.Forms.Padding(3);
            this.tabSunday.Size = new System.Drawing.Size(368, 261);
            this.tabSunday.TabIndex = 0;
            this.tabSunday.Text = "Sunday";
            this.tabSunday.UseVisualStyleBackColor = true;
            // 
            // lvSunday
            // 
            this.lvSunday.Location = new System.Drawing.Point(7, 7);
            this.lvSunday.Name = "lvSunday";
            this.lvSunday.Size = new System.Drawing.Size(355, 248);
            this.lvSunday.TabIndex = 0;
            this.lvSunday.UseCompatibleStateImageBehavior = false;
            this.lvSunday.View = System.Windows.Forms.View.Details;
            // 
            // tabMonday
            // 
            this.tabMonday.Controls.Add(this.lvMonday);
            this.tabMonday.Location = new System.Drawing.Point(4, 22);
            this.tabMonday.Name = "tabMonday";
            this.tabMonday.Padding = new System.Windows.Forms.Padding(3);
            this.tabMonday.Size = new System.Drawing.Size(368, 261);
            this.tabMonday.TabIndex = 1;
            this.tabMonday.Text = "Monday";
            this.tabMonday.UseVisualStyleBackColor = true;
            // 
            // lvMonday
            // 
            this.lvMonday.Location = new System.Drawing.Point(7, 6);
            this.lvMonday.Name = "lvMonday";
            this.lvMonday.Size = new System.Drawing.Size(355, 248);
            this.lvMonday.TabIndex = 1;
            this.lvMonday.UseCompatibleStateImageBehavior = false;
            this.lvMonday.View = System.Windows.Forms.View.Details;
            // 
            // tabTuesday
            // 
            this.tabTuesday.Controls.Add(this.lvTuesday);
            this.tabTuesday.Location = new System.Drawing.Point(4, 22);
            this.tabTuesday.Name = "tabTuesday";
            this.tabTuesday.Size = new System.Drawing.Size(368, 261);
            this.tabTuesday.TabIndex = 2;
            this.tabTuesday.Text = "Tuesday";
            this.tabTuesday.UseVisualStyleBackColor = true;
            // 
            // lvTuesday
            // 
            this.lvTuesday.Location = new System.Drawing.Point(7, 6);
            this.lvTuesday.Name = "lvTuesday";
            this.lvTuesday.Size = new System.Drawing.Size(355, 248);
            this.lvTuesday.TabIndex = 1;
            this.lvTuesday.UseCompatibleStateImageBehavior = false;
            this.lvTuesday.View = System.Windows.Forms.View.Details;
            // 
            // tabWednesday
            // 
            this.tabWednesday.Controls.Add(this.lvWednesday);
            this.tabWednesday.Location = new System.Drawing.Point(4, 22);
            this.tabWednesday.Name = "tabWednesday";
            this.tabWednesday.Size = new System.Drawing.Size(368, 261);
            this.tabWednesday.TabIndex = 3;
            this.tabWednesday.Text = "Wednesday";
            this.tabWednesday.UseVisualStyleBackColor = true;
            // 
            // lvWednesday
            // 
            this.lvWednesday.Location = new System.Drawing.Point(7, 6);
            this.lvWednesday.Name = "lvWednesday";
            this.lvWednesday.Size = new System.Drawing.Size(355, 248);
            this.lvWednesday.TabIndex = 1;
            this.lvWednesday.UseCompatibleStateImageBehavior = false;
            this.lvWednesday.View = System.Windows.Forms.View.Details;
            // 
            // tabThursday
            // 
            this.tabThursday.Controls.Add(this.lvThursday);
            this.tabThursday.Location = new System.Drawing.Point(4, 22);
            this.tabThursday.Name = "tabThursday";
            this.tabThursday.Size = new System.Drawing.Size(368, 261);
            this.tabThursday.TabIndex = 4;
            this.tabThursday.Text = "Thursday";
            this.tabThursday.UseVisualStyleBackColor = true;
            // 
            // lvThursday
            // 
            this.lvThursday.Location = new System.Drawing.Point(7, 6);
            this.lvThursday.Name = "lvThursday";
            this.lvThursday.Size = new System.Drawing.Size(355, 248);
            this.lvThursday.TabIndex = 1;
            this.lvThursday.UseCompatibleStateImageBehavior = false;
            this.lvThursday.View = System.Windows.Forms.View.Details;
            // 
            // tabFriday
            // 
            this.tabFriday.Controls.Add(this.lvFriday);
            this.tabFriday.Location = new System.Drawing.Point(4, 22);
            this.tabFriday.Name = "tabFriday";
            this.tabFriday.Size = new System.Drawing.Size(368, 261);
            this.tabFriday.TabIndex = 5;
            this.tabFriday.Text = "Friday";
            this.tabFriday.UseVisualStyleBackColor = true;
            // 
            // lvFriday
            // 
            this.lvFriday.Location = new System.Drawing.Point(7, 6);
            this.lvFriday.Name = "lvFriday";
            this.lvFriday.Size = new System.Drawing.Size(355, 248);
            this.lvFriday.TabIndex = 1;
            this.lvFriday.UseCompatibleStateImageBehavior = false;
            this.lvFriday.View = System.Windows.Forms.View.Details;
            // 
            // tabSaturday
            // 
            this.tabSaturday.Controls.Add(this.lvSaturday);
            this.tabSaturday.Location = new System.Drawing.Point(4, 22);
            this.tabSaturday.Name = "tabSaturday";
            this.tabSaturday.Size = new System.Drawing.Size(368, 261);
            this.tabSaturday.TabIndex = 6;
            this.tabSaturday.Text = "Saturday";
            this.tabSaturday.UseVisualStyleBackColor = true;
            // 
            // lvSaturday
            // 
            this.lvSaturday.Location = new System.Drawing.Point(7, 6);
            this.lvSaturday.Name = "lvSaturday";
            this.lvSaturday.Size = new System.Drawing.Size(355, 248);
            this.lvSaturday.TabIndex = 1;
            this.lvSaturday.UseCompatibleStateImageBehavior = false;
            this.lvSaturday.View = System.Windows.Forms.View.Details;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(654, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "End Time";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(654, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Start Time";
            // 
            // btnAddOpenBlock
            // 
            this.btnAddOpenBlock.Location = new System.Drawing.Point(542, 157);
            this.btnAddOpenBlock.Name = "btnAddOpenBlock";
            this.btnAddOpenBlock.Size = new System.Drawing.Size(105, 23);
            this.btnAddOpenBlock.TabIndex = 6;
            this.btnAddOpenBlock.Text = "Add Availability";
            this.btnAddOpenBlock.UseVisualStyleBackColor = true;
            this.btnAddOpenBlock.Click += new System.EventHandler(this.btnAddOpenBlock_Click);
            // 
            // combEndAmPm
            // 
            this.combEndAmPm.FormattingEnabled = true;
            this.combEndAmPm.Items.AddRange(new object[] {
            "AM",
            "PM"});
            this.combEndAmPm.Location = new System.Drawing.Point(597, 115);
            this.combEndAmPm.Name = "combEndAmPm";
            this.combEndAmPm.Size = new System.Drawing.Size(50, 21);
            this.combEndAmPm.TabIndex = 5;
            // 
            // combEndMinute
            // 
            this.combEndMinute.FormattingEnabled = true;
            this.combEndMinute.Items.AddRange(new object[] {
            "00",
            "15",
            "30",
            "45"});
            this.combEndMinute.Location = new System.Drawing.Point(542, 115);
            this.combEndMinute.Name = "combEndMinute";
            this.combEndMinute.Size = new System.Drawing.Size(49, 21);
            this.combEndMinute.TabIndex = 4;
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
            this.combEndHour.Location = new System.Drawing.Point(492, 115);
            this.combEndHour.Name = "combEndHour";
            this.combEndHour.Size = new System.Drawing.Size(44, 21);
            this.combEndHour.TabIndex = 3;
            // 
            // combStartAmPm
            // 
            this.combStartAmPm.FormattingEnabled = true;
            this.combStartAmPm.Items.AddRange(new object[] {
            "AM",
            "PM"});
            this.combStartAmPm.Location = new System.Drawing.Point(597, 88);
            this.combStartAmPm.Name = "combStartAmPm";
            this.combStartAmPm.Size = new System.Drawing.Size(50, 21);
            this.combStartAmPm.TabIndex = 2;
            // 
            // combStartMinute
            // 
            this.combStartMinute.FormattingEnabled = true;
            this.combStartMinute.Items.AddRange(new object[] {
            "00",
            "15",
            "30",
            "45"});
            this.combStartMinute.Location = new System.Drawing.Point(542, 89);
            this.combStartMinute.Name = "combStartMinute";
            this.combStartMinute.Size = new System.Drawing.Size(49, 21);
            this.combStartMinute.TabIndex = 1;
            // 
            // combStartHour
            // 
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
            this.combStartHour.Location = new System.Drawing.Point(492, 89);
            this.combStartHour.Name = "combStartHour";
            this.combStartHour.Size = new System.Drawing.Size(44, 21);
            this.combStartHour.TabIndex = 0;
            // 
            // tabAppointments
            // 
            this.tabAppointments.BackColor = System.Drawing.Color.Transparent;
            this.tabAppointments.Controls.Add(this.tabControl2);
            this.tabAppointments.Location = new System.Drawing.Point(4, 22);
            this.tabAppointments.Name = "tabAppointments";
            this.tabAppointments.Padding = new System.Windows.Forms.Padding(3);
            this.tabAppointments.Size = new System.Drawing.Size(829, 306);
            this.tabAppointments.TabIndex = 1;
            this.tabAppointments.Text = "Appointments";
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabAccepted);
            this.tabControl2.Controls.Add(this.tabPendingTutor);
            this.tabControl2.Controls.Add(this.tabPendingTutee);
            this.tabControl2.Location = new System.Drawing.Point(7, 19);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(452, 281);
            this.tabControl2.TabIndex = 0;
            // 
            // tabAccepted
            // 
            this.tabAccepted.Controls.Add(this.lbxAccepted);
            this.tabAccepted.Location = new System.Drawing.Point(4, 22);
            this.tabAccepted.Name = "tabAccepted";
            this.tabAccepted.Padding = new System.Windows.Forms.Padding(3);
            this.tabAccepted.Size = new System.Drawing.Size(444, 255);
            this.tabAccepted.TabIndex = 0;
            this.tabAccepted.Text = "Accepted";
            this.tabAccepted.UseVisualStyleBackColor = true;
            // 
            // lbxAccepted
            // 
            this.lbxAccepted.FormattingEnabled = true;
            this.lbxAccepted.Location = new System.Drawing.Point(6, 5);
            this.lbxAccepted.Name = "lbxAccepted";
            this.lbxAccepted.Size = new System.Drawing.Size(435, 238);
            this.lbxAccepted.TabIndex = 0;
            // 
            // tabPendingTutor
            // 
            this.tabPendingTutor.Controls.Add(this.cbxPendingTutor);
            this.tabPendingTutor.Location = new System.Drawing.Point(4, 22);
            this.tabPendingTutor.Name = "tabPendingTutor";
            this.tabPendingTutor.Padding = new System.Windows.Forms.Padding(3);
            this.tabPendingTutor.Size = new System.Drawing.Size(444, 255);
            this.tabPendingTutor.TabIndex = 1;
            this.tabPendingTutor.Text = "Pending Tutor";
            this.tabPendingTutor.UseVisualStyleBackColor = true;
            // 
            // cbxPendingTutor
            // 
            this.cbxPendingTutor.FormattingEnabled = true;
            this.cbxPendingTutor.Location = new System.Drawing.Point(3, 2);
            this.cbxPendingTutor.Name = "cbxPendingTutor";
            this.cbxPendingTutor.Size = new System.Drawing.Size(435, 244);
            this.cbxPendingTutor.TabIndex = 0;
            // 
            // tabPendingTutee
            // 
            this.tabPendingTutee.Controls.Add(this.cbxPendingTutee);
            this.tabPendingTutee.Location = new System.Drawing.Point(4, 22);
            this.tabPendingTutee.Name = "tabPendingTutee";
            this.tabPendingTutee.Size = new System.Drawing.Size(444, 255);
            this.tabPendingTutee.TabIndex = 2;
            this.tabPendingTutee.Text = "Pending Tutee";
            this.tabPendingTutee.UseVisualStyleBackColor = true;
            // 
            // cbxPendingTutee
            // 
            this.cbxPendingTutee.FormattingEnabled = true;
            this.cbxPendingTutee.Location = new System.Drawing.Point(3, 3);
            this.cbxPendingTutee.Name = "cbxPendingTutee";
            this.cbxPendingTutee.Size = new System.Drawing.Size(438, 244);
            this.cbxPendingTutee.TabIndex = 0;
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(991, 380);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(97, 23);
            this.btnLogout.TabIndex = 1;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnRequest
            // 
            this.btnRequest.Location = new System.Drawing.Point(991, 342);
            this.btnRequest.Name = "btnRequest";
            this.btnRequest.Size = new System.Drawing.Size(97, 23);
            this.btnRequest.TabIndex = 2;
            this.btnRequest.Text = "Make Request";
            this.btnRequest.UseVisualStyleBackColor = true;
            this.btnRequest.Click += new System.EventHandler(this.btnRequest_Click);
            // 
            // StudentMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1167, 464);
            this.Controls.Add(this.btnRequest);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StudentMain";
            this.Text = "StudentMain";
            this.tabControl1.ResumeLayout(false);
            this.tabAvailability.ResumeLayout(false);
            this.tabAvailability.PerformLayout();
            this.dayTabs.ResumeLayout(false);
            this.tabSunday.ResumeLayout(false);
            this.tabMonday.ResumeLayout(false);
            this.tabTuesday.ResumeLayout(false);
            this.tabWednesday.ResumeLayout(false);
            this.tabThursday.ResumeLayout(false);
            this.tabFriday.ResumeLayout(false);
            this.tabSaturday.ResumeLayout(false);
            this.tabAppointments.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabAccepted.ResumeLayout(false);
            this.tabPendingTutor.ResumeLayout(false);
            this.tabPendingTutee.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabAvailability;
        private System.Windows.Forms.TabPage tabAppointments;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabAccepted;
        private System.Windows.Forms.ListBox lbxAccepted;
        private System.Windows.Forms.TabPage tabPendingTutor;
        private System.Windows.Forms.CheckedListBox cbxPendingTutor;
        private System.Windows.Forms.TabPage tabPendingTutee;
        private System.Windows.Forms.CheckedListBox cbxPendingTutee;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnRequest;
        private System.Windows.Forms.Button btnAddOpenBlock;
        private System.Windows.Forms.ComboBox combEndAmPm;
        private System.Windows.Forms.ComboBox combEndMinute;
        private System.Windows.Forms.ComboBox combEndHour;
        private System.Windows.Forms.ComboBox combStartAmPm;
        private System.Windows.Forms.ComboBox combStartMinute;
        private System.Windows.Forms.ComboBox combStartHour;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox combEndDay;
        private System.Windows.Forms.ComboBox combStartDay;
        private System.Windows.Forms.TabControl dayTabs;
        private System.Windows.Forms.TabPage tabSunday;
        private System.Windows.Forms.TabPage tabMonday;
        private System.Windows.Forms.TabPage tabTuesday;
        private System.Windows.Forms.TabPage tabWednesday;
        private System.Windows.Forms.TabPage tabThursday;
        private System.Windows.Forms.TabPage tabFriday;
        private System.Windows.Forms.TabPage tabSaturday;
        private System.Windows.Forms.ListView lvSunday;
        private System.Windows.Forms.ListView lvMonday;
        private System.Windows.Forms.ListView lvTuesday;
        private System.Windows.Forms.ListView lvWednesday;
        private System.Windows.Forms.ListView lvThursday;
        private System.Windows.Forms.ListView lvFriday;
        private System.Windows.Forms.ListView lvSaturday;
        private System.Windows.Forms.CheckBox cbxWeekly;
        private System.Windows.Forms.DateTimePicker dateTime;
    }
}