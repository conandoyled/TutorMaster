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
            this.weekStartDateTime = new System.Windows.Forms.DateTimePicker();
            this.dayTabs = new System.Windows.Forms.TabControl();
            this.tabSunday = new System.Windows.Forms.TabPage();
            this.lblSunday = new System.Windows.Forms.Label();
            this.lvSunday = new System.Windows.Forms.ListView();
            this.tabMonday = new System.Windows.Forms.TabPage();
            this.lblMonday = new System.Windows.Forms.Label();
            this.lvMonday = new System.Windows.Forms.ListView();
            this.tabTuesday = new System.Windows.Forms.TabPage();
            this.lblTuesday = new System.Windows.Forms.Label();
            this.lvTuesday = new System.Windows.Forms.ListView();
            this.tabWednesday = new System.Windows.Forms.TabPage();
            this.lblWednesday = new System.Windows.Forms.Label();
            this.lvWednesday = new System.Windows.Forms.ListView();
            this.tabThursday = new System.Windows.Forms.TabPage();
            this.lblThursday = new System.Windows.Forms.Label();
            this.lvThursday = new System.Windows.Forms.ListView();
            this.tabFriday = new System.Windows.Forms.TabPage();
            this.lblFriday = new System.Windows.Forms.Label();
            this.lvFriday = new System.Windows.Forms.ListView();
            this.tabSaturday = new System.Windows.Forms.TabPage();
            this.lblSaturday = new System.Windows.Forms.Label();
            this.lvSaturday = new System.Windows.Forms.ListView();
            this.btnAddOpenBlock = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnMakeRequest = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabSchedule = new System.Windows.Forms.TabPage();
            this.btnRemoveAvail = new System.Windows.Forms.Button();
            this.tabAppointments = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabAccepted = new System.Windows.Forms.TabPage();
            this.btnDeselect1 = new System.Windows.Forms.Button();
            this.btnCancelFinalized = new System.Windows.Forms.Button();
            this.lvFinalized = new System.Windows.Forms.ListView();
            this.tabPendingTutor = new System.Windows.Forms.TabPage();
            this.btnDeselect2 = new System.Windows.Forms.Button();
            this.btnRejectTutor = new System.Windows.Forms.Button();
            this.btnAcceptAddLoc = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lvPendingTutor = new System.Windows.Forms.ListView();
            this.lvTutor = new System.Windows.Forms.ListView();
            this.tabPendingTutee = new System.Windows.Forms.TabPage();
            this.btnDeselect3 = new System.Windows.Forms.Button();
            this.btnRejectTutee = new System.Windows.Forms.Button();
            this.btnFinalize = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.lvTutee = new System.Windows.Forms.ListView();
            this.label6 = new System.Windows.Forms.Label();
            this.lvPendingTutee = new System.Windows.Forms.ListView();
            this.label10 = new System.Windows.Forms.Label();
            this.btnAdvanceRequest = new System.Windows.Forms.Button();
            this.btnChangePassword = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblOpen = new System.Windows.Forms.Label();
            this.lblTutor = new System.Windows.Forms.Label();
            this.lblTutee = new System.Windows.Forms.Label();
            this.lblAttention = new System.Windows.Forms.Label();
            this.lblWaiting = new System.Windows.Forms.Label();
            this.dayTabs.SuspendLayout();
            this.tabSunday.SuspendLayout();
            this.tabMonday.SuspendLayout();
            this.tabTuesday.SuspendLayout();
            this.tabWednesday.SuspendLayout();
            this.tabThursday.SuspendLayout();
            this.tabFriday.SuspendLayout();
            this.tabSaturday.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabSchedule.SuspendLayout();
            this.tabAppointments.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabAccepted.SuspendLayout();
            this.tabPendingTutor.SuspendLayout();
            this.tabPendingTutee.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // weekStartDateTime
            // 
            this.weekStartDateTime.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.weekStartDateTime.Location = new System.Drawing.Point(733, 59);
            this.weekStartDateTime.MaxDate = new System.DateTime(2017, 4, 30, 0, 0, 0, 0);
            this.weekStartDateTime.MinDate = new System.DateTime(2017, 1, 1, 0, 0, 0, 0);
            this.weekStartDateTime.Name = "weekStartDateTime";
            this.weekStartDateTime.Size = new System.Drawing.Size(200, 20);
            this.weekStartDateTime.TabIndex = 13;
            this.weekStartDateTime.Value = new System.DateTime(2017, 3, 23, 0, 0, 0, 0);
            this.weekStartDateTime.CloseUp += new System.EventHandler(this.weekStartDateTime_ValueChanged);
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
            this.dayTabs.Location = new System.Drawing.Point(3, 6);
            this.dayTabs.Name = "dayTabs";
            this.dayTabs.SelectedIndex = 0;
            this.dayTabs.Size = new System.Drawing.Size(685, 352);
            this.dayTabs.TabIndex = 9;
            this.dayTabs.Selected += new System.Windows.Forms.TabControlEventHandler(this.dayTabs_Selected);
            // 
            // tabSunday
            // 
            this.tabSunday.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.tabSunday.Controls.Add(this.lblSunday);
            this.tabSunday.Controls.Add(this.lvSunday);
            this.tabSunday.Location = new System.Drawing.Point(4, 22);
            this.tabSunday.Name = "tabSunday";
            this.tabSunday.Padding = new System.Windows.Forms.Padding(3);
            this.tabSunday.Size = new System.Drawing.Size(677, 326);
            this.tabSunday.TabIndex = 0;
            this.tabSunday.Text = "Sunday";
            // 
            // lblSunday
            // 
            this.lblSunday.AutoSize = true;
            this.lblSunday.Location = new System.Drawing.Point(6, 3);
            this.lblSunday.Name = "lblSunday";
            this.lblSunday.Size = new System.Drawing.Size(35, 13);
            this.lblSunday.TabIndex = 14;
            this.lblSunday.Text = "label3";
            // 
            // lvSunday
            // 
            this.lvSunday.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.lvSunday.Location = new System.Drawing.Point(0, 19);
            this.lvSunday.Name = "lvSunday";
            this.lvSunday.Size = new System.Drawing.Size(677, 307);
            this.lvSunday.TabIndex = 0;
            this.lvSunday.UseCompatibleStateImageBehavior = false;
            this.lvSunday.View = System.Windows.Forms.View.Details;
            this.lvSunday.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvSunday_ItemChecked);
            // 
            // tabMonday
            // 
            this.tabMonday.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.tabMonday.Controls.Add(this.lblMonday);
            this.tabMonday.Controls.Add(this.lvMonday);
            this.tabMonday.Location = new System.Drawing.Point(4, 22);
            this.tabMonday.Name = "tabMonday";
            this.tabMonday.Padding = new System.Windows.Forms.Padding(3);
            this.tabMonday.Size = new System.Drawing.Size(677, 326);
            this.tabMonday.TabIndex = 1;
            this.tabMonday.Text = "Monday";
            // 
            // lblMonday
            // 
            this.lblMonday.AutoSize = true;
            this.lblMonday.Location = new System.Drawing.Point(6, 3);
            this.lblMonday.Name = "lblMonday";
            this.lblMonday.Size = new System.Drawing.Size(35, 13);
            this.lblMonday.TabIndex = 2;
            this.lblMonday.Text = "label4";
            // 
            // lvMonday
            // 
            this.lvMonday.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.lvMonday.Location = new System.Drawing.Point(0, 19);
            this.lvMonday.Name = "lvMonday";
            this.lvMonday.Size = new System.Drawing.Size(677, 307);
            this.lvMonday.TabIndex = 1;
            this.lvMonday.UseCompatibleStateImageBehavior = false;
            this.lvMonday.View = System.Windows.Forms.View.Details;
            this.lvMonday.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvMonday_ItemChecked);
            // 
            // tabTuesday
            // 
            this.tabTuesday.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.tabTuesday.Controls.Add(this.lblTuesday);
            this.tabTuesday.Controls.Add(this.lvTuesday);
            this.tabTuesday.Location = new System.Drawing.Point(4, 22);
            this.tabTuesday.Name = "tabTuesday";
            this.tabTuesday.Size = new System.Drawing.Size(677, 326);
            this.tabTuesday.TabIndex = 2;
            this.tabTuesday.Text = "Tuesday";
            // 
            // lblTuesday
            // 
            this.lblTuesday.AutoSize = true;
            this.lblTuesday.Location = new System.Drawing.Point(6, 3);
            this.lblTuesday.Name = "lblTuesday";
            this.lblTuesday.Size = new System.Drawing.Size(35, 13);
            this.lblTuesday.TabIndex = 2;
            this.lblTuesday.Text = "label5";
            // 
            // lvTuesday
            // 
            this.lvTuesday.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.lvTuesday.Location = new System.Drawing.Point(0, 19);
            this.lvTuesday.Name = "lvTuesday";
            this.lvTuesday.Size = new System.Drawing.Size(677, 307);
            this.lvTuesday.TabIndex = 1;
            this.lvTuesday.UseCompatibleStateImageBehavior = false;
            this.lvTuesday.View = System.Windows.Forms.View.Details;
            this.lvTuesday.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvTuesday_ItemChecked);
            // 
            // tabWednesday
            // 
            this.tabWednesday.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.tabWednesday.Controls.Add(this.lblWednesday);
            this.tabWednesday.Controls.Add(this.lvWednesday);
            this.tabWednesday.Location = new System.Drawing.Point(4, 22);
            this.tabWednesday.Name = "tabWednesday";
            this.tabWednesday.Size = new System.Drawing.Size(677, 326);
            this.tabWednesday.TabIndex = 3;
            this.tabWednesday.Text = "Wednesday";
            // 
            // lblWednesday
            // 
            this.lblWednesday.AutoSize = true;
            this.lblWednesday.Location = new System.Drawing.Point(6, 3);
            this.lblWednesday.Name = "lblWednesday";
            this.lblWednesday.Size = new System.Drawing.Size(35, 13);
            this.lblWednesday.TabIndex = 2;
            this.lblWednesday.Text = "label6";
            // 
            // lvWednesday
            // 
            this.lvWednesday.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.lvWednesday.Location = new System.Drawing.Point(0, 19);
            this.lvWednesday.Name = "lvWednesday";
            this.lvWednesday.Size = new System.Drawing.Size(677, 307);
            this.lvWednesday.TabIndex = 1;
            this.lvWednesday.UseCompatibleStateImageBehavior = false;
            this.lvWednesday.View = System.Windows.Forms.View.Details;
            this.lvWednesday.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvWednesday_ItemChecked);
            // 
            // tabThursday
            // 
            this.tabThursday.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.tabThursday.Controls.Add(this.lblThursday);
            this.tabThursday.Controls.Add(this.lvThursday);
            this.tabThursday.Location = new System.Drawing.Point(4, 22);
            this.tabThursday.Name = "tabThursday";
            this.tabThursday.Size = new System.Drawing.Size(677, 326);
            this.tabThursday.TabIndex = 4;
            this.tabThursday.Text = "Thursday";
            // 
            // lblThursday
            // 
            this.lblThursday.AutoSize = true;
            this.lblThursday.Location = new System.Drawing.Point(6, 3);
            this.lblThursday.Name = "lblThursday";
            this.lblThursday.Size = new System.Drawing.Size(35, 13);
            this.lblThursday.TabIndex = 2;
            this.lblThursday.Text = "label7";
            // 
            // lvThursday
            // 
            this.lvThursday.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.lvThursday.Location = new System.Drawing.Point(0, 19);
            this.lvThursday.Name = "lvThursday";
            this.lvThursday.Size = new System.Drawing.Size(677, 307);
            this.lvThursday.TabIndex = 1;
            this.lvThursday.UseCompatibleStateImageBehavior = false;
            this.lvThursday.View = System.Windows.Forms.View.Details;
            this.lvThursday.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvThursday_ItemChecked);
            // 
            // tabFriday
            // 
            this.tabFriday.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.tabFriday.Controls.Add(this.lblFriday);
            this.tabFriday.Controls.Add(this.lvFriday);
            this.tabFriday.Location = new System.Drawing.Point(4, 22);
            this.tabFriday.Name = "tabFriday";
            this.tabFriday.Size = new System.Drawing.Size(677, 326);
            this.tabFriday.TabIndex = 5;
            this.tabFriday.Text = "Friday";
            // 
            // lblFriday
            // 
            this.lblFriday.AutoSize = true;
            this.lblFriday.Location = new System.Drawing.Point(6, 3);
            this.lblFriday.Name = "lblFriday";
            this.lblFriday.Size = new System.Drawing.Size(35, 13);
            this.lblFriday.TabIndex = 2;
            this.lblFriday.Text = "label8";
            // 
            // lvFriday
            // 
            this.lvFriday.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.lvFriday.Location = new System.Drawing.Point(0, 19);
            this.lvFriday.Name = "lvFriday";
            this.lvFriday.Size = new System.Drawing.Size(677, 307);
            this.lvFriday.TabIndex = 1;
            this.lvFriday.UseCompatibleStateImageBehavior = false;
            this.lvFriday.View = System.Windows.Forms.View.Details;
            this.lvFriday.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvFriday_ItemChecked);
            // 
            // tabSaturday
            // 
            this.tabSaturday.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.tabSaturday.Controls.Add(this.lblSaturday);
            this.tabSaturday.Controls.Add(this.lvSaturday);
            this.tabSaturday.Location = new System.Drawing.Point(4, 22);
            this.tabSaturday.Name = "tabSaturday";
            this.tabSaturday.Size = new System.Drawing.Size(677, 326);
            this.tabSaturday.TabIndex = 6;
            this.tabSaturday.Text = "Saturday";
            // 
            // lblSaturday
            // 
            this.lblSaturday.AutoSize = true;
            this.lblSaturday.Location = new System.Drawing.Point(6, 3);
            this.lblSaturday.Name = "lblSaturday";
            this.lblSaturday.Size = new System.Drawing.Size(35, 13);
            this.lblSaturday.TabIndex = 2;
            this.lblSaturday.Text = "label9";
            // 
            // lvSaturday
            // 
            this.lvSaturday.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.lvSaturday.Location = new System.Drawing.Point(0, 19);
            this.lvSaturday.Name = "lvSaturday";
            this.lvSaturday.Size = new System.Drawing.Size(677, 307);
            this.lvSaturday.TabIndex = 1;
            this.lvSaturday.UseCompatibleStateImageBehavior = false;
            this.lvSaturday.View = System.Windows.Forms.View.Details;
            this.lvSaturday.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvSaturday_ItemChecked);
            // 
            // btnAddOpenBlock
            // 
            this.btnAddOpenBlock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.btnAddOpenBlock.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddOpenBlock.Location = new System.Drawing.Point(7, 364);
            this.btnAddOpenBlock.Name = "btnAddOpenBlock";
            this.btnAddOpenBlock.Size = new System.Drawing.Size(132, 23);
            this.btnAddOpenBlock.TabIndex = 6;
            this.btnAddOpenBlock.Text = "Add Availability";
            this.btnAddOpenBlock.UseVisualStyleBackColor = false;
            this.btnAddOpenBlock.Click += new System.EventHandler(this.btnAddOpenBlock_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(222)))), ((int)(((byte)(229)))));
            this.btnLogout.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(167)))), ((int)(((byte)(175)))));
            this.btnLogout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(167)))), ((int)(((byte)(175)))));
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLogout.Location = new System.Drawing.Point(736, 85);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(197, 23);
            this.btnLogout.TabIndex = 1;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnMakeRequest
            // 
            this.btnMakeRequest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(222)))), ((int)(((byte)(229)))));
            this.btnMakeRequest.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(71)))), ((int)(((byte)(76)))));
            this.btnMakeRequest.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMakeRequest.Location = new System.Drawing.Point(736, 114);
            this.btnMakeRequest.Name = "btnMakeRequest";
            this.btnMakeRequest.Size = new System.Drawing.Size(197, 23);
            this.btnMakeRequest.TabIndex = 2;
            this.btnMakeRequest.Text = "Make Tutor Request";
            this.btnMakeRequest.UseVisualStyleBackColor = false;
            this.btnMakeRequest.Click += new System.EventHandler(this.btnMakeRequest_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabSchedule);
            this.tabControl1.Controls.Add(this.tabAppointments);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(715, 419);
            this.tabControl1.TabIndex = 14;
            // 
            // tabSchedule
            // 
            this.tabSchedule.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.tabSchedule.Controls.Add(this.dayTabs);
            this.tabSchedule.Controls.Add(this.btnAddOpenBlock);
            this.tabSchedule.Controls.Add(this.btnRemoveAvail);
            this.tabSchedule.Location = new System.Drawing.Point(4, 22);
            this.tabSchedule.Name = "tabSchedule";
            this.tabSchedule.Padding = new System.Windows.Forms.Padding(3);
            this.tabSchedule.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tabSchedule.Size = new System.Drawing.Size(707, 393);
            this.tabSchedule.TabIndex = 1;
            this.tabSchedule.Text = "Schedule";
            // 
            // btnRemoveAvail
            // 
            this.btnRemoveAvail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(204)))));
            this.btnRemoveAvail.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRemoveAvail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(66)))), ((int)(((byte)(76)))));
            this.btnRemoveAvail.Location = new System.Drawing.Point(145, 364);
            this.btnRemoveAvail.Name = "btnRemoveAvail";
            this.btnRemoveAvail.Size = new System.Drawing.Size(132, 23);
            this.btnRemoveAvail.TabIndex = 16;
            this.btnRemoveAvail.Text = "Remove Availability";
            this.btnRemoveAvail.UseVisualStyleBackColor = false;
            this.btnRemoveAvail.Click += new System.EventHandler(this.btnRemoveAvail_Click);
            // 
            // tabAppointments
            // 
            this.tabAppointments.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.tabAppointments.Controls.Add(this.tabControl2);
            this.tabAppointments.Location = new System.Drawing.Point(4, 22);
            this.tabAppointments.Name = "tabAppointments";
            this.tabAppointments.Size = new System.Drawing.Size(707, 393);
            this.tabAppointments.TabIndex = 2;
            this.tabAppointments.Text = "Appointments";
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabAccepted);
            this.tabControl2.Controls.Add(this.tabPendingTutor);
            this.tabControl2.Controls.Add(this.tabPendingTutee);
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(707, 393);
            this.tabControl2.TabIndex = 0;
            this.tabControl2.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.btnDeselect1_Click);
            this.tabControl2.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl2_Selected);
            // 
            // tabAccepted
            // 
            this.tabAccepted.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.tabAccepted.Controls.Add(this.btnDeselect1);
            this.tabAccepted.Controls.Add(this.btnCancelFinalized);
            this.tabAccepted.Controls.Add(this.lvFinalized);
            this.tabAccepted.Location = new System.Drawing.Point(4, 22);
            this.tabAccepted.Name = "tabAccepted";
            this.tabAccepted.Padding = new System.Windows.Forms.Padding(3);
            this.tabAccepted.Size = new System.Drawing.Size(699, 367);
            this.tabAccepted.TabIndex = 0;
            this.tabAccepted.Text = "Finalized Appointments";
            // 
            // btnDeselect1
            // 
            this.btnDeselect1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.btnDeselect1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDeselect1.Location = new System.Drawing.Point(592, 336);
            this.btnDeselect1.Name = "btnDeselect1";
            this.btnDeselect1.Size = new System.Drawing.Size(101, 23);
            this.btnDeselect1.TabIndex = 18;
            this.btnDeselect1.Text = "Deselect All";
            this.btnDeselect1.UseVisualStyleBackColor = false;
            this.btnDeselect1.Click += new System.EventHandler(this.btnDeselect1_Click);
            // 
            // btnCancelFinalized
            // 
            this.btnCancelFinalized.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.btnCancelFinalized.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancelFinalized.Location = new System.Drawing.Point(6, 336);
            this.btnCancelFinalized.Name = "btnCancelFinalized";
            this.btnCancelFinalized.Size = new System.Drawing.Size(177, 23);
            this.btnCancelFinalized.TabIndex = 17;
            this.btnCancelFinalized.Text = "Cancel Appointment(s)";
            this.btnCancelFinalized.UseVisualStyleBackColor = false;
            this.btnCancelFinalized.Click += new System.EventHandler(this.btnCancelFinalized_Click);
            // 
            // lvFinalized
            // 
            this.lvFinalized.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.lvFinalized.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvFinalized.Location = new System.Drawing.Point(0, 0);
            this.lvFinalized.Name = "lvFinalized";
            this.lvFinalized.Size = new System.Drawing.Size(699, 330);
            this.lvFinalized.TabIndex = 16;
            this.lvFinalized.UseCompatibleStateImageBehavior = false;
            this.lvFinalized.View = System.Windows.Forms.View.Details;
            this.lvFinalized.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lvFinalized_ColumnWidthChanging);
            this.lvFinalized.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvFinalized_ItemChecked);
            // 
            // tabPendingTutor
            // 
            this.tabPendingTutor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.tabPendingTutor.Controls.Add(this.btnDeselect2);
            this.tabPendingTutor.Controls.Add(this.btnRejectTutor);
            this.tabPendingTutor.Controls.Add(this.btnAcceptAddLoc);
            this.tabPendingTutor.Controls.Add(this.label4);
            this.tabPendingTutor.Controls.Add(this.label3);
            this.tabPendingTutor.Controls.Add(this.lvPendingTutor);
            this.tabPendingTutor.Controls.Add(this.lvTutor);
            this.tabPendingTutor.Location = new System.Drawing.Point(4, 22);
            this.tabPendingTutor.Name = "tabPendingTutor";
            this.tabPendingTutor.Padding = new System.Windows.Forms.Padding(3);
            this.tabPendingTutor.Size = new System.Drawing.Size(699, 367);
            this.tabPendingTutor.TabIndex = 1;
            this.tabPendingTutor.Text = "Pending Tutoring Appointments";
            // 
            // btnDeselect2
            // 
            this.btnDeselect2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.btnDeselect2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDeselect2.Location = new System.Drawing.Point(592, 336);
            this.btnDeselect2.Name = "btnDeselect2";
            this.btnDeselect2.Size = new System.Drawing.Size(101, 23);
            this.btnDeselect2.TabIndex = 23;
            this.btnDeselect2.Text = "Deselect All";
            this.btnDeselect2.UseVisualStyleBackColor = false;
            this.btnDeselect2.Click += new System.EventHandler(this.btnDeselect1_Click);
            // 
            // btnRejectTutor
            // 
            this.btnRejectTutor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.btnRejectTutor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRejectTutor.Location = new System.Drawing.Point(189, 336);
            this.btnRejectTutor.Name = "btnRejectTutor";
            this.btnRejectTutor.Size = new System.Drawing.Size(177, 23);
            this.btnRejectTutor.TabIndex = 22;
            this.btnRejectTutor.Text = "Reject/Delete Appointment(s)";
            this.btnRejectTutor.UseVisualStyleBackColor = false;
            this.btnRejectTutor.Click += new System.EventHandler(this.btnRejectTutor_Click);
            // 
            // btnAcceptAddLoc
            // 
            this.btnAcceptAddLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.btnAcceptAddLoc.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAcceptAddLoc.Location = new System.Drawing.Point(6, 336);
            this.btnAcceptAddLoc.Name = "btnAcceptAddLoc";
            this.btnAcceptAddLoc.Size = new System.Drawing.Size(177, 23);
            this.btnAcceptAddLoc.TabIndex = 21;
            this.btnAcceptAddLoc.Text = "Accept/Add Location";
            this.btnAcceptAddLoc.UseVisualStyleBackColor = false;
            this.btnAcceptAddLoc.Click += new System.EventHandler(this.btnAcceptAddLoc_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(254, 15);
            this.label4.TabIndex = 20;
            this.label4.Text = "Appointments waiting on the Tutee\'s Approval";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(338, 15);
            this.label3.TabIndex = 19;
            this.label3.Text = "Appointments waiting for your approval and location proposal";
            // 
            // lvPendingTutor
            // 
            this.lvPendingTutor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.lvPendingTutor.Location = new System.Drawing.Point(0, 24);
            this.lvPendingTutor.Name = "lvPendingTutor";
            this.lvPendingTutor.Size = new System.Drawing.Size(699, 140);
            this.lvPendingTutor.TabIndex = 18;
            this.lvPendingTutor.UseCompatibleStateImageBehavior = false;
            this.lvPendingTutor.View = System.Windows.Forms.View.Details;
            this.lvPendingTutor.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lvPendingTutor_ColumnWidthChanging);
            this.lvPendingTutor.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvPendingTutor_ItemChecked);
            // 
            // lvTutor
            // 
            this.lvTutor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.lvTutor.Location = new System.Drawing.Point(0, 190);
            this.lvTutor.Name = "lvTutor";
            this.lvTutor.Size = new System.Drawing.Size(699, 140);
            this.lvTutor.TabIndex = 17;
            this.lvTutor.UseCompatibleStateImageBehavior = false;
            this.lvTutor.View = System.Windows.Forms.View.Details;
            this.lvTutor.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lvTutor_ColumnWidthChanging);
            this.lvTutor.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvTutor_ItemChecked_2);
            // 
            // tabPendingTutee
            // 
            this.tabPendingTutee.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.tabPendingTutee.Controls.Add(this.btnDeselect3);
            this.tabPendingTutee.Controls.Add(this.btnRejectTutee);
            this.tabPendingTutee.Controls.Add(this.btnFinalize);
            this.tabPendingTutee.Controls.Add(this.label5);
            this.tabPendingTutee.Controls.Add(this.lvTutee);
            this.tabPendingTutee.Controls.Add(this.label6);
            this.tabPendingTutee.Controls.Add(this.lvPendingTutee);
            this.tabPendingTutee.Location = new System.Drawing.Point(4, 22);
            this.tabPendingTutee.Name = "tabPendingTutee";
            this.tabPendingTutee.Size = new System.Drawing.Size(699, 367);
            this.tabPendingTutee.TabIndex = 2;
            this.tabPendingTutee.Text = "Pending Tutee Appointments";
            // 
            // btnDeselect3
            // 
            this.btnDeselect3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.btnDeselect3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDeselect3.Location = new System.Drawing.Point(592, 336);
            this.btnDeselect3.Name = "btnDeselect3";
            this.btnDeselect3.Size = new System.Drawing.Size(101, 23);
            this.btnDeselect3.TabIndex = 26;
            this.btnDeselect3.Text = "Deselect All";
            this.btnDeselect3.UseVisualStyleBackColor = false;
            this.btnDeselect3.Click += new System.EventHandler(this.btnDeselect1_Click);
            // 
            // btnRejectTutee
            // 
            this.btnRejectTutee.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.btnRejectTutee.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRejectTutee.Location = new System.Drawing.Point(189, 336);
            this.btnRejectTutee.Name = "btnRejectTutee";
            this.btnRejectTutee.Size = new System.Drawing.Size(177, 23);
            this.btnRejectTutee.TabIndex = 25;
            this.btnRejectTutee.Text = "Reject/Delete Appointment(s)";
            this.btnRejectTutee.UseVisualStyleBackColor = false;
            this.btnRejectTutee.Click += new System.EventHandler(this.btnRejectTutee_Click);
            // 
            // btnFinalize
            // 
            this.btnFinalize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.btnFinalize.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFinalize.Location = new System.Drawing.Point(6, 336);
            this.btnFinalize.Name = "btnFinalize";
            this.btnFinalize.Size = new System.Drawing.Size(177, 23);
            this.btnFinalize.TabIndex = 24;
            this.btnFinalize.Text = "Accept Appointment(s)";
            this.btnFinalize.UseVisualStyleBackColor = false;
            this.btnFinalize.Click += new System.EventHandler(this.btnFinalize_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(218, 15);
            this.label5.TabIndex = 23;
            this.label5.Text = "Appointments waiting on tutor approval";
            // 
            // lvTutee
            // 
            this.lvTutee.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.lvTutee.Location = new System.Drawing.Point(0, 24);
            this.lvTutee.Name = "lvTutee";
            this.lvTutee.Size = new System.Drawing.Size(699, 140);
            this.lvTutee.TabIndex = 22;
            this.lvTutee.UseCompatibleStateImageBehavior = false;
            this.lvTutee.View = System.Windows.Forms.View.Details;
            this.lvTutee.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lvTutee_ColumnWidthChanging);
            this.lvTutee.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvTutee_ItemChecked);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 170);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(217, 15);
            this.label6.TabIndex = 21;
            this.label6.Text = "Appointments waiting for your approval";
            // 
            // lvPendingTutee
            // 
            this.lvPendingTutee.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.lvPendingTutee.Location = new System.Drawing.Point(0, 188);
            this.lvPendingTutee.Name = "lvPendingTutee";
            this.lvPendingTutee.Size = new System.Drawing.Size(699, 140);
            this.lvPendingTutee.TabIndex = 18;
            this.lvPendingTutee.UseCompatibleStateImageBehavior = false;
            this.lvPendingTutee.View = System.Windows.Forms.View.Details;
            this.lvPendingTutee.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lvPendingTutee_ColumnWidthChanging);
            this.lvPendingTutee.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvPendingTutee_ItemChecked);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(733, 40);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(190, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "Date to load schedule a week out from";
            // 
            // btnAdvanceRequest
            // 
            this.btnAdvanceRequest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(222)))), ((int)(((byte)(229)))));
            this.btnAdvanceRequest.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdvanceRequest.Location = new System.Drawing.Point(736, 143);
            this.btnAdvanceRequest.Name = "btnAdvanceRequest";
            this.btnAdvanceRequest.Size = new System.Drawing.Size(200, 23);
            this.btnAdvanceRequest.TabIndex = 17;
            this.btnAdvanceRequest.Text = "Make Advanced Tutor Request";
            this.btnAdvanceRequest.UseVisualStyleBackColor = false;
            this.btnAdvanceRequest.Click += new System.EventHandler(this.btnAdvanceRequest_Click);
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(222)))), ((int)(((byte)(229)))));
            this.btnChangePassword.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnChangePassword.Location = new System.Drawing.Point(736, 197);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(200, 23);
            this.btnChangePassword.TabIndex = 18;
            this.btnChangePassword.Text = "Change Password";
            this.btnChangePassword.UseVisualStyleBackColor = false;
            this.btnChangePassword.Click += new System.EventHandler(this.btnChangePassword_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TutorMaster.Properties.Resources.WatermarkR2;
            this.pictureBox1.Location = new System.Drawing.Point(834, 326);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(119, 119);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(733, 224);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 15);
            this.label1.TabIndex = 25;
            this.label1.Text = "Appointment colors:";
            // 
            // lblOpen
            // 
            this.lblOpen.AutoSize = true;
            this.lblOpen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(104)))), ((int)(((byte)(10)))));
            this.lblOpen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.lblOpen.Location = new System.Drawing.Point(733, 246);
            this.lblOpen.Name = "lblOpen";
            this.lblOpen.Padding = new System.Windows.Forms.Padding(40, 4, 40, 4);
            this.lblOpen.Size = new System.Drawing.Size(113, 21);
            this.lblOpen.TabIndex = 26;
            this.lblOpen.Text = "Open";
            // 
            // lblTutor
            // 
            this.lblTutor.AutoSize = true;
            this.lblTutor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(120)))), ((int)(((byte)(122)))));
            this.lblTutor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.lblTutor.Location = new System.Drawing.Point(733, 273);
            this.lblTutor.Name = "lblTutor";
            this.lblTutor.Padding = new System.Windows.Forms.Padding(34, 4, 33, 4);
            this.lblTutor.Size = new System.Drawing.Size(113, 21);
            this.lblTutor.TabIndex = 27;
            this.lblTutor.Text = "Tutoring";
            // 
            // lblTutee
            // 
            this.lblTutee.AutoSize = true;
            this.lblTutee.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(76)))), ((int)(((byte)(114)))));
            this.lblTutee.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.lblTutee.Location = new System.Drawing.Point(733, 301);
            this.lblTutee.Name = "lblTutee";
            this.lblTutee.Padding = new System.Windows.Forms.Padding(19, 4, 20, 4);
            this.lblTutee.Size = new System.Drawing.Size(113, 21);
            this.lblTutee.TabIndex = 28;
            this.lblTutee.Text = "Being Tutored";
            // 
            // lblAttention
            // 
            this.lblAttention.AutoSize = true;
            this.lblAttention.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(6)))), ((int)(((byte)(6)))));
            this.lblAttention.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.lblAttention.Location = new System.Drawing.Point(733, 329);
            this.lblAttention.Name = "lblAttention";
            this.lblAttention.Padding = new System.Windows.Forms.Padding(15, 4, 15, 4);
            this.lblAttention.Size = new System.Drawing.Size(113, 21);
            this.lblAttention.TabIndex = 29;
            this.lblAttention.Text = "Needs Attention";
            // 
            // lblWaiting
            // 
            this.lblWaiting.AutoSize = true;
            this.lblWaiting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(13)))), ((int)(((byte)(137)))));
            this.lblWaiting.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.lblWaiting.Location = new System.Drawing.Point(733, 357);
            this.lblWaiting.Name = "lblWaiting";
            this.lblWaiting.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.lblWaiting.Size = new System.Drawing.Size(114, 21);
            this.lblWaiting.TabIndex = 30;
            this.lblWaiting.Text = "Waiting For Partner";
            // 
            // StudentMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(232)))), ((int)(((byte)(239)))));
            this.ClientSize = new System.Drawing.Size(948, 441);
            this.Controls.Add(this.lblWaiting);
            this.Controls.Add(this.lblAttention);
            this.Controls.Add(this.lblTutee);
            this.Controls.Add(this.lblTutor);
            this.Controls.Add(this.lblOpen);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnChangePassword);
            this.Controls.Add(this.btnAdvanceRequest);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.weekStartDateTime);
            this.Controls.Add(this.btnMakeRequest);
            this.Controls.Add(this.btnLogout);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(71)))), ((int)(((byte)(76)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StudentMain";
            this.Text = "StudentMain";
            this.Load += new System.EventHandler(this.StudentMain_Load);
            this.dayTabs.ResumeLayout(false);
            this.tabSunday.ResumeLayout(false);
            this.tabSunday.PerformLayout();
            this.tabMonday.ResumeLayout(false);
            this.tabMonday.PerformLayout();
            this.tabTuesday.ResumeLayout(false);
            this.tabTuesday.PerformLayout();
            this.tabWednesday.ResumeLayout(false);
            this.tabWednesday.PerformLayout();
            this.tabThursday.ResumeLayout(false);
            this.tabThursday.PerformLayout();
            this.tabFriday.ResumeLayout(false);
            this.tabFriday.PerformLayout();
            this.tabSaturday.ResumeLayout(false);
            this.tabSaturday.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabSchedule.ResumeLayout(false);
            this.tabAppointments.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabAccepted.ResumeLayout(false);
            this.tabPendingTutor.ResumeLayout(false);
            this.tabPendingTutor.PerformLayout();
            this.tabPendingTutee.ResumeLayout(false);
            this.tabPendingTutee.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnMakeRequest;
        private System.Windows.Forms.Button btnAddOpenBlock;
        private System.Windows.Forms.TabControl dayTabs;
        private System.Windows.Forms.TabPage tabSunday;
        private System.Windows.Forms.TabPage tabMonday;
        private System.Windows.Forms.TabPage tabTuesday;
        private System.Windows.Forms.TabPage tabWednesday;
        private System.Windows.Forms.TabPage tabThursday;
        private System.Windows.Forms.TabPage tabFriday;
        private System.Windows.Forms.TabPage tabSaturday;
        private System.Windows.Forms.ListView lvSunday;
        private System.Windows.Forms.ListView lvTuesday;
        private System.Windows.Forms.ListView lvWednesday;
        private System.Windows.Forms.ListView lvThursday;
        private System.Windows.Forms.ListView lvFriday;
        private System.Windows.Forms.ListView lvSaturday;
        private System.Windows.Forms.DateTimePicker weekStartDateTime;
        private System.Windows.Forms.Label lblSunday;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabSchedule;
        private System.Windows.Forms.TabPage tabAppointments;
        private System.Windows.Forms.TabPage tabPendingTutee;
        private System.Windows.Forms.TabPage tabPendingTutor;
        private System.Windows.Forms.TabPage tabAccepted;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.Label lblMonday;
        private System.Windows.Forms.Label lblTuesday;
        private System.Windows.Forms.Label lblWednesday;
        private System.Windows.Forms.Label lblThursday;
        private System.Windows.Forms.Label lblFriday;
        private System.Windows.Forms.Label lblSaturday;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ListView lvFinalized;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView lvPendingTutor;
        private System.Windows.Forms.ListView lvTutor;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListView lvPendingTutee;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListView lvTutee;
        private System.Windows.Forms.Button btnCancelFinalized;
        private System.Windows.Forms.Button btnDeselect1;
        private System.Windows.Forms.Button btnDeselect2;
        private System.Windows.Forms.Button btnRejectTutor;
        private System.Windows.Forms.Button btnAcceptAddLoc;
        private System.Windows.Forms.Button btnDeselect3;
        private System.Windows.Forms.Button btnRejectTutee;
        private System.Windows.Forms.Button btnFinalize;
        private System.Windows.Forms.Button btnRemoveAvail;
        private System.Windows.Forms.Button btnAdvanceRequest;
        private System.Windows.Forms.Button btnChangePassword;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ListView lvMonday;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblOpen;
        private System.Windows.Forms.Label lblTutor;
        private System.Windows.Forms.Label lblTutee;
        private System.Windows.Forms.Label lblAttention;
        private System.Windows.Forms.Label lblWaiting;
    }
}