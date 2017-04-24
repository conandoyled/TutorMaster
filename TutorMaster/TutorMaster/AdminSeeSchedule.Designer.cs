namespace TutorMaster
{
    partial class AdminSeeSchedule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminSeeSchedule));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnRemoveAvailability = new System.Windows.Forms.Button();
            this.btnCreateAppointment = new System.Windows.Forms.Button();
            this.btnAddAvailability = new System.Windows.Forms.Button();
            this.lvOpen = new System.Windows.Forms.ListView();
            this.tabAccepted = new System.Windows.Forms.TabPage();
            this.btnEditFinalized = new System.Windows.Forms.Button();
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
            this.lblNameTitle = new System.Windows.Forms.Label();
            this.btnDone = new System.Windows.Forms.Button();
            this.lblRemove = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabAccepted.SuspendLayout();
            this.tabPendingTutor.SuspendLayout();
            this.tabPendingTutee.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabAccepted);
            this.tabControl1.Controls.Add(this.tabPendingTutor);
            this.tabControl1.Controls.Add(this.tabPendingTutee);
            this.tabControl1.Location = new System.Drawing.Point(44, 49);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(707, 393);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.tabPage1.Controls.Add(this.btnRemoveAvailability);
            this.tabPage1.Controls.Add(this.btnCreateAppointment);
            this.tabPage1.Controls.Add(this.btnAddAvailability);
            this.tabPage1.Controls.Add(this.lvOpen);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(699, 367);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "Open Availability";
            // 
            // btnRemoveAvailability
            // 
            this.btnRemoveAvailability.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.btnRemoveAvailability.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRemoveAvailability.Location = new System.Drawing.Point(323, 335);
            this.btnRemoveAvailability.Name = "btnRemoveAvailability";
            this.btnRemoveAvailability.Size = new System.Drawing.Size(144, 23);
            this.btnRemoveAvailability.TabIndex = 20;
            this.btnRemoveAvailability.Text = "Remove Availability";
            this.btnRemoveAvailability.UseVisualStyleBackColor = false;
            this.btnRemoveAvailability.Click += new System.EventHandler(this.btnRemoveAvailability_Click);
            this.btnRemoveAvailability.MouseLeave += new System.EventHandler(this.btnRemoveAvailability_MouseLeave);
            this.btnRemoveAvailability.MouseHover += new System.EventHandler(this.btnRemoveAvailability_MouseHover);
            // 
            // btnCreateAppointment
            // 
            this.btnCreateAppointment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.btnCreateAppointment.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCreateAppointment.Location = new System.Drawing.Point(164, 336);
            this.btnCreateAppointment.Name = "btnCreateAppointment";
            this.btnCreateAppointment.Size = new System.Drawing.Size(152, 23);
            this.btnCreateAppointment.TabIndex = 19;
            this.btnCreateAppointment.Text = "Create Appointment";
            this.btnCreateAppointment.UseVisualStyleBackColor = false;
            this.btnCreateAppointment.Click += new System.EventHandler(this.btnCreateAppointment_Click);
            // 
            // btnAddAvailability
            // 
            this.btnAddAvailability.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.btnAddAvailability.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddAvailability.Location = new System.Drawing.Point(6, 336);
            this.btnAddAvailability.Name = "btnAddAvailability";
            this.btnAddAvailability.Size = new System.Drawing.Size(152, 23);
            this.btnAddAvailability.TabIndex = 18;
            this.btnAddAvailability.Text = "Add Availability";
            this.btnAddAvailability.UseVisualStyleBackColor = false;
            this.btnAddAvailability.Click += new System.EventHandler(this.btnAddAvailability_Click);
            // 
            // lvOpen
            // 
            this.lvOpen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.lvOpen.Location = new System.Drawing.Point(0, 0);
            this.lvOpen.Name = "lvOpen";
            this.lvOpen.Size = new System.Drawing.Size(699, 330);
            this.lvOpen.TabIndex = 17;
            this.lvOpen.UseCompatibleStateImageBehavior = false;
            this.lvOpen.View = System.Windows.Forms.View.Details;
            this.lvOpen.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvOpen_ItemChecked);
            // 
            // tabAccepted
            // 
            this.tabAccepted.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.tabAccepted.Controls.Add(this.btnEditFinalized);
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
            // btnEditFinalized
            // 
            this.btnEditFinalized.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.btnEditFinalized.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEditFinalized.Location = new System.Drawing.Point(203, 336);
            this.btnEditFinalized.Name = "btnEditFinalized";
            this.btnEditFinalized.Size = new System.Drawing.Size(121, 23);
            this.btnEditFinalized.TabIndex = 24;
            this.btnEditFinalized.Text = "Edit Appointment";
            this.btnEditFinalized.UseVisualStyleBackColor = false;
            this.btnEditFinalized.Click += new System.EventHandler(this.btnEditFinalized_Click);
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
            this.lvFinalized.Location = new System.Drawing.Point(0, 0);
            this.lvFinalized.Name = "lvFinalized";
            this.lvFinalized.Size = new System.Drawing.Size(699, 330);
            this.lvFinalized.TabIndex = 16;
            this.lvFinalized.UseCompatibleStateImageBehavior = false;
            this.lvFinalized.View = System.Windows.Forms.View.Details;
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
            this.btnAcceptAddLoc.Text = "Finalize Appointment";
            this.btnAcceptAddLoc.UseVisualStyleBackColor = false;
            this.btnAcceptAddLoc.Click += new System.EventHandler(this.btnAcceptAddLoc_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(223, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Appointments waiting on the Tutee\'s Approval";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(293, 13);
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
            this.lvTutor.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvTutor_ItemChecked);
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
            // 
            // btnRejectTutee
            // 
            this.btnRejectTutee.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.btnRejectTutee.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRejectTutee.Location = new System.Drawing.Point(165, 336);
            this.btnRejectTutee.Name = "btnRejectTutee";
            this.btnRejectTutee.Size = new System.Drawing.Size(164, 23);
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
            this.btnFinalize.Size = new System.Drawing.Size(153, 23);
            this.btnFinalize.TabIndex = 24;
            this.btnFinalize.Text = "Finalize Appointment(s)";
            this.btnFinalize.UseVisualStyleBackColor = false;
            this.btnFinalize.Click += new System.EventHandler(this.btnFinalize_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(190, 13);
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
            this.lvTutee.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvTutee_ItemChecked);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 172);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(189, 13);
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
            this.lvPendingTutee.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvPendingTutee_ItemChecked);
            // 
            // lblNameTitle
            // 
            this.lblNameTitle.AutoSize = true;
            this.lblNameTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNameTitle.Location = new System.Drawing.Point(45, 21);
            this.lblNameTitle.Name = "lblNameTitle";
            this.lblNameTitle.Size = new System.Drawing.Size(229, 24);
            this.lblNameTitle.TabIndex = 2;
            this.lblNameTitle.Text = "Student Name\'s Schedule";
            // 
            // btnDone
            // 
            this.btnDone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(222)))), ((int)(((byte)(229)))));
            this.btnDone.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDone.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnDone.Location = new System.Drawing.Point(49, 450);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(131, 23);
            this.btnDone.TabIndex = 3;
            this.btnDone.Text = "Done";
            this.btnDone.UseVisualStyleBackColor = false;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // lblRemove
            // 
            this.lblRemove.AutoSize = true;
            this.lblRemove.Location = new System.Drawing.Point(336, 437);
            this.lblRemove.Name = "lblRemove";
            this.lblRemove.Size = new System.Drawing.Size(0, 13);
            this.lblRemove.TabIndex = 21;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TutorMaster.Properties.Resources.WatermarkR2;
            this.pictureBox1.Location = new System.Drawing.Point(720, 436);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(75, 70);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            // 
            // AdminSeeSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(232)))), ((int)(((byte)(239)))));
            this.ClientSize = new System.Drawing.Size(793, 505);
            this.Controls.Add(this.lblRemove);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.lblNameTitle);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.pictureBox1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(71)))), ((int)(((byte)(76)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AdminSeeSchedule";
            this.Text = "TutorMaster";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AdminSeeSchedule_FormClosed);
            this.Load += new System.EventHandler(this.AdminSeeSchedule_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
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

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListView lvOpen;
        private System.Windows.Forms.TabPage tabAccepted;
        private System.Windows.Forms.Button btnDeselect1;
        private System.Windows.Forms.Button btnCancelFinalized;
        private System.Windows.Forms.ListView lvFinalized;
        private System.Windows.Forms.TabPage tabPendingTutor;
        private System.Windows.Forms.Button btnDeselect2;
        private System.Windows.Forms.Button btnRejectTutor;
        private System.Windows.Forms.Button btnAcceptAddLoc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView lvPendingTutor;
        private System.Windows.Forms.ListView lvTutor;
        private System.Windows.Forms.TabPage tabPendingTutee;
        private System.Windows.Forms.Button btnDeselect3;
        private System.Windows.Forms.Button btnRejectTutee;
        private System.Windows.Forms.Button btnFinalize;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListView lvTutee;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListView lvPendingTutee;
        private System.Windows.Forms.Label lblNameTitle;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Button btnCreateAppointment;
        private System.Windows.Forms.Button btnAddAvailability;
        private System.Windows.Forms.Button btnRemoveAvailability;
        private System.Windows.Forms.Label lblRemove;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnEditFinalized;
    }
}