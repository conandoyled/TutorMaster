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
            this.startDateTime = new System.Windows.Forms.DateTimePicker();
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
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabAccepted = new System.Windows.Forms.TabPage();
            this.tabPendingTutor = new System.Windows.Forms.TabPage();
            this.tabPendingTutee = new System.Windows.Forms.TabPage();
            this.btnLogout = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lvAccepted = new System.Windows.Forms.ListView();
            this.lvPendingTutor = new System.Windows.Forms.ListView();
            this.lvPendingTutee = new System.Windows.Forms.ListView();
            this.dayTabs.SuspendLayout();
            this.tabSunday.SuspendLayout();
            this.tabMonday.SuspendLayout();
            this.tabTuesday.SuspendLayout();
            this.tabWednesday.SuspendLayout();
            this.tabThursday.SuspendLayout();
            this.tabFriday.SuspendLayout();
            this.tabSaturday.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabAccepted.SuspendLayout();
            this.tabPendingTutor.SuspendLayout();
            this.tabPendingTutee.SuspendLayout();
            this.SuspendLayout();
            // 
            // startDateTime
            // 
            this.startDateTime.Location = new System.Drawing.Point(851, 428);
            this.startDateTime.Name = "startDateTime";
            this.startDateTime.Size = new System.Drawing.Size(200, 20);
            this.startDateTime.TabIndex = 13;
            // 
            // cbxWeekly
            // 
            this.cbxWeekly.AutoSize = true;
            this.cbxWeekly.Location = new System.Drawing.Point(845, 556);
            this.cbxWeekly.Name = "cbxWeekly";
            this.cbxWeekly.Size = new System.Drawing.Size(62, 17);
            this.cbxWeekly.TabIndex = 12;
            this.cbxWeekly.Text = "Weekly";
            this.cbxWeekly.UseVisualStyleBackColor = true;
            // 
            // combEndDay
            // 
            this.combEndDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combEndDay.FormattingEnabled = true;
            this.combEndDay.Items.AddRange(new object[] {
            "Sunday",
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday",
            "Saturday"});
            this.combEndDay.Location = new System.Drawing.Point(845, 509);
            this.combEndDay.Name = "combEndDay";
            this.combEndDay.Size = new System.Drawing.Size(101, 21);
            this.combEndDay.TabIndex = 11;
            // 
            // combStartDay
            // 
            this.combStartDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combStartDay.FormattingEnabled = true;
            this.combStartDay.Items.AddRange(new object[] {
            "Sunday",
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday",
            "Saturday"});
            this.combStartDay.Location = new System.Drawing.Point(845, 482);
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
            this.dayTabs.Location = new System.Drawing.Point(142, 450);
            this.dayTabs.Name = "dayTabs";
            this.dayTabs.SelectedIndex = 0;
            this.dayTabs.Size = new System.Drawing.Size(685, 287);
            this.dayTabs.TabIndex = 9;
            // 
            // tabSunday
            // 
            this.tabSunday.Controls.Add(this.lvSunday);
            this.tabSunday.Location = new System.Drawing.Point(4, 22);
            this.tabSunday.Name = "tabSunday";
            this.tabSunday.Padding = new System.Windows.Forms.Padding(3);
            this.tabSunday.Size = new System.Drawing.Size(677, 261);
            this.tabSunday.TabIndex = 0;
            this.tabSunday.Text = "Sunday";
            this.tabSunday.UseVisualStyleBackColor = true;
            // 
            // lvSunday
            // 
            this.lvSunday.Location = new System.Drawing.Point(7, 7);
            this.lvSunday.Name = "lvSunday";
            this.lvSunday.Size = new System.Drawing.Size(664, 248);
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
            this.tabMonday.Size = new System.Drawing.Size(677, 261);
            this.tabMonday.TabIndex = 1;
            this.tabMonday.Text = "Monday";
            this.tabMonday.UseVisualStyleBackColor = true;
            // 
            // lvMonday
            // 
            this.lvMonday.Location = new System.Drawing.Point(7, 6);
            this.lvMonday.Name = "lvMonday";
            this.lvMonday.Size = new System.Drawing.Size(664, 248);
            this.lvMonday.TabIndex = 1;
            this.lvMonday.UseCompatibleStateImageBehavior = false;
            this.lvMonday.View = System.Windows.Forms.View.Details;
            // 
            // tabTuesday
            // 
            this.tabTuesday.Controls.Add(this.lvTuesday);
            this.tabTuesday.Location = new System.Drawing.Point(4, 22);
            this.tabTuesday.Name = "tabTuesday";
            this.tabTuesday.Size = new System.Drawing.Size(677, 261);
            this.tabTuesday.TabIndex = 2;
            this.tabTuesday.Text = "Tuesday";
            this.tabTuesday.UseVisualStyleBackColor = true;
            // 
            // lvTuesday
            // 
            this.lvTuesday.Location = new System.Drawing.Point(7, 6);
            this.lvTuesday.Name = "lvTuesday";
            this.lvTuesday.Size = new System.Drawing.Size(658, 248);
            this.lvTuesday.TabIndex = 1;
            this.lvTuesday.UseCompatibleStateImageBehavior = false;
            this.lvTuesday.View = System.Windows.Forms.View.Details;
            // 
            // tabWednesday
            // 
            this.tabWednesday.Controls.Add(this.lvWednesday);
            this.tabWednesday.Location = new System.Drawing.Point(4, 22);
            this.tabWednesday.Name = "tabWednesday";
            this.tabWednesday.Size = new System.Drawing.Size(677, 261);
            this.tabWednesday.TabIndex = 3;
            this.tabWednesday.Text = "Wednesday";
            this.tabWednesday.UseVisualStyleBackColor = true;
            // 
            // lvWednesday
            // 
            this.lvWednesday.Location = new System.Drawing.Point(7, 6);
            this.lvWednesday.Name = "lvWednesday";
            this.lvWednesday.Size = new System.Drawing.Size(659, 248);
            this.lvWednesday.TabIndex = 1;
            this.lvWednesday.UseCompatibleStateImageBehavior = false;
            this.lvWednesday.View = System.Windows.Forms.View.Details;
            // 
            // tabThursday
            // 
            this.tabThursday.Controls.Add(this.lvThursday);
            this.tabThursday.Location = new System.Drawing.Point(4, 22);
            this.tabThursday.Name = "tabThursday";
            this.tabThursday.Size = new System.Drawing.Size(677, 261);
            this.tabThursday.TabIndex = 4;
            this.tabThursday.Text = "Thursday";
            this.tabThursday.UseVisualStyleBackColor = true;
            // 
            // lvThursday
            // 
            this.lvThursday.Location = new System.Drawing.Point(7, 6);
            this.lvThursday.Name = "lvThursday";
            this.lvThursday.Size = new System.Drawing.Size(655, 248);
            this.lvThursday.TabIndex = 1;
            this.lvThursday.UseCompatibleStateImageBehavior = false;
            this.lvThursday.View = System.Windows.Forms.View.Details;
            // 
            // tabFriday
            // 
            this.tabFriday.Controls.Add(this.lvFriday);
            this.tabFriday.Location = new System.Drawing.Point(4, 22);
            this.tabFriday.Name = "tabFriday";
            this.tabFriday.Size = new System.Drawing.Size(677, 261);
            this.tabFriday.TabIndex = 5;
            this.tabFriday.Text = "Friday";
            this.tabFriday.UseVisualStyleBackColor = true;
            // 
            // lvFriday
            // 
            this.lvFriday.Location = new System.Drawing.Point(7, 6);
            this.lvFriday.Name = "lvFriday";
            this.lvFriday.Size = new System.Drawing.Size(658, 248);
            this.lvFriday.TabIndex = 1;
            this.lvFriday.UseCompatibleStateImageBehavior = false;
            this.lvFriday.View = System.Windows.Forms.View.Details;
            // 
            // tabSaturday
            // 
            this.tabSaturday.Controls.Add(this.lvSaturday);
            this.tabSaturday.Location = new System.Drawing.Point(4, 22);
            this.tabSaturday.Name = "tabSaturday";
            this.tabSaturday.Size = new System.Drawing.Size(677, 261);
            this.tabSaturday.TabIndex = 6;
            this.tabSaturday.Text = "Saturday";
            this.tabSaturday.UseVisualStyleBackColor = true;
            // 
            // lvSaturday
            // 
            this.lvSaturday.Location = new System.Drawing.Point(7, 6);
            this.lvSaturday.Name = "lvSaturday";
            this.lvSaturday.Size = new System.Drawing.Size(657, 248);
            this.lvSaturday.TabIndex = 1;
            this.lvSaturday.UseCompatibleStateImageBehavior = false;
            this.lvSaturday.View = System.Windows.Forms.View.Details;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1114, 515);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "End Time";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1114, 488);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Start Time";
            // 
            // btnAddOpenBlock
            // 
            this.btnAddOpenBlock.Location = new System.Drawing.Point(1002, 552);
            this.btnAddOpenBlock.Name = "btnAddOpenBlock";
            this.btnAddOpenBlock.Size = new System.Drawing.Size(105, 23);
            this.btnAddOpenBlock.TabIndex = 6;
            this.btnAddOpenBlock.Text = "Add Availability";
            this.btnAddOpenBlock.UseVisualStyleBackColor = true;
            this.btnAddOpenBlock.Click += new System.EventHandler(this.btnAddOpenBlock_Click);
            // 
            // combEndAmPm
            // 
            this.combEndAmPm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combEndAmPm.FormattingEnabled = true;
            this.combEndAmPm.Items.AddRange(new object[] {
            "AM",
            "PM"});
            this.combEndAmPm.Location = new System.Drawing.Point(1057, 508);
            this.combEndAmPm.Name = "combEndAmPm";
            this.combEndAmPm.Size = new System.Drawing.Size(50, 21);
            this.combEndAmPm.TabIndex = 5;
            // 
            // combEndMinute
            // 
            this.combEndMinute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combEndMinute.FormattingEnabled = true;
            this.combEndMinute.Items.AddRange(new object[] {
            "00",
            "15",
            "30",
            "45"});
            this.combEndMinute.Location = new System.Drawing.Point(1002, 508);
            this.combEndMinute.Name = "combEndMinute";
            this.combEndMinute.Size = new System.Drawing.Size(49, 21);
            this.combEndMinute.TabIndex = 4;
            // 
            // combEndHour
            // 
            this.combEndHour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.combEndHour.Location = new System.Drawing.Point(952, 508);
            this.combEndHour.Name = "combEndHour";
            this.combEndHour.Size = new System.Drawing.Size(44, 21);
            this.combEndHour.TabIndex = 3;
            // 
            // combStartAmPm
            // 
            this.combStartAmPm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combStartAmPm.FormattingEnabled = true;
            this.combStartAmPm.Items.AddRange(new object[] {
            "AM",
            "PM"});
            this.combStartAmPm.Location = new System.Drawing.Point(1057, 481);
            this.combStartAmPm.Name = "combStartAmPm";
            this.combStartAmPm.Size = new System.Drawing.Size(50, 21);
            this.combStartAmPm.TabIndex = 2;
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
            this.combStartMinute.Location = new System.Drawing.Point(1002, 482);
            this.combStartMinute.Name = "combStartMinute";
            this.combStartMinute.Size = new System.Drawing.Size(49, 21);
            this.combStartMinute.TabIndex = 1;
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
            this.combStartHour.Location = new System.Drawing.Point(952, 482);
            this.combStartHour.Name = "combStartHour";
            this.combStartHour.Size = new System.Drawing.Size(44, 21);
            this.combStartHour.TabIndex = 0;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabAccepted);
            this.tabControl2.Controls.Add(this.tabPendingTutor);
            this.tabControl2.Controls.Add(this.tabPendingTutee);
            this.tabControl2.Location = new System.Drawing.Point(146, 83);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(677, 281);
            this.tabControl2.TabIndex = 0;
            // 
            // tabAccepted
            // 
            this.tabAccepted.Controls.Add(this.lvAccepted);
            this.tabAccepted.Location = new System.Drawing.Point(4, 22);
            this.tabAccepted.Name = "tabAccepted";
            this.tabAccepted.Padding = new System.Windows.Forms.Padding(3);
            this.tabAccepted.Size = new System.Drawing.Size(669, 255);
            this.tabAccepted.TabIndex = 0;
            this.tabAccepted.Text = "Accepted";
            this.tabAccepted.UseVisualStyleBackColor = true;
            // 
            // tabPendingTutor
            // 
            this.tabPendingTutor.Controls.Add(this.lvPendingTutor);
            this.tabPendingTutor.Location = new System.Drawing.Point(4, 22);
            this.tabPendingTutor.Name = "tabPendingTutor";
            this.tabPendingTutor.Padding = new System.Windows.Forms.Padding(3);
            this.tabPendingTutor.Size = new System.Drawing.Size(444, 255);
            this.tabPendingTutor.TabIndex = 1;
            this.tabPendingTutor.Text = "Pending Tutor";
            this.tabPendingTutor.UseVisualStyleBackColor = true;
            // 
            // tabPendingTutee
            // 
            this.tabPendingTutee.Controls.Add(this.lvPendingTutee);
            this.tabPendingTutee.Location = new System.Drawing.Point(4, 22);
            this.tabPendingTutee.Name = "tabPendingTutee";
            this.tabPendingTutee.Size = new System.Drawing.Size(444, 255);
            this.tabPendingTutee.TabIndex = 2;
            this.tabPendingTutee.Text = "Pending Tutee";
            this.tabPendingTutee.UseVisualStyleBackColor = true;
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(1010, 626);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(97, 23);
            this.btnLogout.TabIndex = 1;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1117, 589);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Make Request";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // lvAccepted
            // 
            this.lvAccepted.Location = new System.Drawing.Point(6, 6);
            this.lvAccepted.Name = "lvAccepted";
            this.lvAccepted.Size = new System.Drawing.Size(657, 246);
            this.lvAccepted.TabIndex = 14;
            this.lvAccepted.UseCompatibleStateImageBehavior = false;
            this.lvAccepted.View = System.Windows.Forms.View.Details;
            // 
            // lvPendingTutor
            // 
            this.lvPendingTutor.Location = new System.Drawing.Point(6, 4);
            this.lvPendingTutor.Name = "lvPendingTutor";
            this.lvPendingTutor.Size = new System.Drawing.Size(432, 246);
            this.lvPendingTutor.TabIndex = 15;
            this.lvPendingTutor.UseCompatibleStateImageBehavior = false;
            this.lvPendingTutor.View = System.Windows.Forms.View.Details;
            // 
            // lvPendingTutee
            // 
            this.lvPendingTutee.Location = new System.Drawing.Point(6, 4);
            this.lvPendingTutee.Name = "lvPendingTutee";
            this.lvPendingTutee.Size = new System.Drawing.Size(432, 246);
            this.lvPendingTutee.TabIndex = 15;
            this.lvPendingTutee.UseCompatibleStateImageBehavior = false;
            this.lvPendingTutee.View = System.Windows.Forms.View.Details;
            // 
            // StudentMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1754, 770);
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.startDateTime);
            this.Controls.Add(this.dayTabs);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.combStartMinute);
            this.Controls.Add(this.combEndMinute);
            this.Controls.Add(this.combStartHour);
            this.Controls.Add(this.cbxWeekly);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.combEndAmPm);
            this.Controls.Add(this.combStartAmPm);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.combStartDay);
            this.Controls.Add(this.combEndHour);
            this.Controls.Add(this.btnAddOpenBlock);
            this.Controls.Add(this.combEndDay);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StudentMain";
            this.Text = "StudentMain";
            this.dayTabs.ResumeLayout(false);
            this.tabSunday.ResumeLayout(false);
            this.tabMonday.ResumeLayout(false);
            this.tabTuesday.ResumeLayout(false);
            this.tabWednesday.ResumeLayout(false);
            this.tabThursday.ResumeLayout(false);
            this.tabFriday.ResumeLayout(false);
            this.tabSaturday.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabAccepted.ResumeLayout(false);
            this.tabPendingTutor.ResumeLayout(false);
            this.tabPendingTutee.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabAccepted;
        private System.Windows.Forms.TabPage tabPendingTutor;
        private System.Windows.Forms.TabPage tabPendingTutee;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button button1;
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
        private System.Windows.Forms.DateTimePicker startDateTime;
        private System.Windows.Forms.ListView lvAccepted;
        private System.Windows.Forms.ListView lvPendingTutor;
        private System.Windows.Forms.ListView lvPendingTutee;
    }
}