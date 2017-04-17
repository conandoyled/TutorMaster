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
            this.btnCreateAppointment = new System.Windows.Forms.Button();
            this.btnAddAvailability = new System.Windows.Forms.Button();
            this.lvOpen = new System.Windows.Forms.ListView();
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
            this.lblNameTitle = new System.Windows.Forms.Label();
            this.btnDone = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabAccepted.SuspendLayout();
            this.tabPendingTutor.SuspendLayout();
            this.tabPendingTutee.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabAccepted);
            this.tabControl1.Controls.Add(this.tabPendingTutor);
            this.tabControl1.Controls.Add(this.tabPendingTutee);
            this.tabControl1.Location = new System.Drawing.Point(12, 38);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(707, 393);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnCreateAppointment);
            this.tabPage1.Controls.Add(this.btnAddAvailability);
            this.tabPage1.Controls.Add(this.lvOpen);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(699, 367);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "Open Availability";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnCreateAppointment
            // 
            this.btnCreateAppointment.Location = new System.Drawing.Point(164, 336);
            this.btnCreateAppointment.Name = "btnCreateAppointment";
            this.btnCreateAppointment.Size = new System.Drawing.Size(152, 23);
            this.btnCreateAppointment.TabIndex = 19;
            this.btnCreateAppointment.Text = "Create Appointment";
            this.btnCreateAppointment.UseVisualStyleBackColor = true;
            this.btnCreateAppointment.Click += new System.EventHandler(this.btnCreateAppointment_Click);
            // 
            // btnAddAvailability
            // 
            this.btnAddAvailability.Location = new System.Drawing.Point(6, 336);
            this.btnAddAvailability.Name = "btnAddAvailability";
            this.btnAddAvailability.Size = new System.Drawing.Size(152, 23);
            this.btnAddAvailability.TabIndex = 18;
            this.btnAddAvailability.Text = "Add Availability";
            this.btnAddAvailability.UseVisualStyleBackColor = true;
            this.btnAddAvailability.Click += new System.EventHandler(this.btnAddAvailability_Click);
            // 
            // lvOpen
            // 
            this.lvOpen.Location = new System.Drawing.Point(0, 0);
            this.lvOpen.Name = "lvOpen";
            this.lvOpen.Size = new System.Drawing.Size(699, 330);
            this.lvOpen.TabIndex = 17;
            this.lvOpen.UseCompatibleStateImageBehavior = false;
            this.lvOpen.View = System.Windows.Forms.View.Details;
            // 
            // tabAccepted
            // 
            this.tabAccepted.Controls.Add(this.btnDeselect1);
            this.tabAccepted.Controls.Add(this.btnCancelFinalized);
            this.tabAccepted.Controls.Add(this.lvFinalized);
            this.tabAccepted.Location = new System.Drawing.Point(4, 22);
            this.tabAccepted.Name = "tabAccepted";
            this.tabAccepted.Padding = new System.Windows.Forms.Padding(3);
            this.tabAccepted.Size = new System.Drawing.Size(699, 367);
            this.tabAccepted.TabIndex = 0;
            this.tabAccepted.Text = "Finalized Appointments";
            this.tabAccepted.UseVisualStyleBackColor = true;
            // 
            // btnDeselect1
            // 
            this.btnDeselect1.Location = new System.Drawing.Point(592, 336);
            this.btnDeselect1.Name = "btnDeselect1";
            this.btnDeselect1.Size = new System.Drawing.Size(101, 23);
            this.btnDeselect1.TabIndex = 18;
            this.btnDeselect1.Text = "Deselect All";
            this.btnDeselect1.UseVisualStyleBackColor = true;
            // 
            // btnCancelFinalized
            // 
            this.btnCancelFinalized.Location = new System.Drawing.Point(6, 336);
            this.btnCancelFinalized.Name = "btnCancelFinalized";
            this.btnCancelFinalized.Size = new System.Drawing.Size(177, 23);
            this.btnCancelFinalized.TabIndex = 17;
            this.btnCancelFinalized.Text = "Cancel Appointment(s)";
            this.btnCancelFinalized.UseVisualStyleBackColor = true;
            this.btnCancelFinalized.Click += new System.EventHandler(this.btnCancelFinalized_Click);
            // 
            // lvFinalized
            // 
            this.lvFinalized.Location = new System.Drawing.Point(0, 0);
            this.lvFinalized.Name = "lvFinalized";
            this.lvFinalized.Size = new System.Drawing.Size(699, 330);
            this.lvFinalized.TabIndex = 16;
            this.lvFinalized.UseCompatibleStateImageBehavior = false;
            this.lvFinalized.View = System.Windows.Forms.View.Details;
            // 
            // tabPendingTutor
            // 
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
            this.tabPendingTutor.UseVisualStyleBackColor = true;
            // 
            // btnDeselect2
            // 
            this.btnDeselect2.Location = new System.Drawing.Point(592, 336);
            this.btnDeselect2.Name = "btnDeselect2";
            this.btnDeselect2.Size = new System.Drawing.Size(101, 23);
            this.btnDeselect2.TabIndex = 23;
            this.btnDeselect2.Text = "Deselect All";
            this.btnDeselect2.UseVisualStyleBackColor = true;
            // 
            // btnRejectTutor
            // 
            this.btnRejectTutor.Location = new System.Drawing.Point(189, 336);
            this.btnRejectTutor.Name = "btnRejectTutor";
            this.btnRejectTutor.Size = new System.Drawing.Size(177, 23);
            this.btnRejectTutor.TabIndex = 22;
            this.btnRejectTutor.Text = "Reject/Delete Appointment(s)";
            this.btnRejectTutor.UseVisualStyleBackColor = true;
            this.btnRejectTutor.Click += new System.EventHandler(this.btnRejectTutor_Click);
            // 
            // btnAcceptAddLoc
            // 
            this.btnAcceptAddLoc.Location = new System.Drawing.Point(6, 336);
            this.btnAcceptAddLoc.Name = "btnAcceptAddLoc";
            this.btnAcceptAddLoc.Size = new System.Drawing.Size(177, 23);
            this.btnAcceptAddLoc.TabIndex = 21;
            this.btnAcceptAddLoc.Text = "Finalize Appointment";
            this.btnAcceptAddLoc.UseVisualStyleBackColor = true;
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
            this.lvPendingTutor.Location = new System.Drawing.Point(0, 24);
            this.lvPendingTutor.Name = "lvPendingTutor";
            this.lvPendingTutor.Size = new System.Drawing.Size(699, 140);
            this.lvPendingTutor.TabIndex = 18;
            this.lvPendingTutor.UseCompatibleStateImageBehavior = false;
            this.lvPendingTutor.View = System.Windows.Forms.View.Details;
            // 
            // lvTutor
            // 
            this.lvTutor.Location = new System.Drawing.Point(0, 190);
            this.lvTutor.Name = "lvTutor";
            this.lvTutor.Size = new System.Drawing.Size(699, 140);
            this.lvTutor.TabIndex = 17;
            this.lvTutor.UseCompatibleStateImageBehavior = false;
            this.lvTutor.View = System.Windows.Forms.View.Details;
            // 
            // tabPendingTutee
            // 
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
            this.tabPendingTutee.UseVisualStyleBackColor = true;
            // 
            // btnDeselect3
            // 
            this.btnDeselect3.Location = new System.Drawing.Point(592, 336);
            this.btnDeselect3.Name = "btnDeselect3";
            this.btnDeselect3.Size = new System.Drawing.Size(101, 23);
            this.btnDeselect3.TabIndex = 26;
            this.btnDeselect3.Text = "Deselect All";
            this.btnDeselect3.UseVisualStyleBackColor = true;
            // 
            // btnRejectTutee
            // 
            this.btnRejectTutee.Location = new System.Drawing.Point(324, 336);
            this.btnRejectTutee.Name = "btnRejectTutee";
            this.btnRejectTutee.Size = new System.Drawing.Size(164, 23);
            this.btnRejectTutee.TabIndex = 25;
            this.btnRejectTutee.Text = "Reject/Delete Appointment(s)";
            this.btnRejectTutee.UseVisualStyleBackColor = true;
            this.btnRejectTutee.Click += new System.EventHandler(this.btnRejectTutee_Click);
            // 
            // btnFinalize
            // 
            this.btnFinalize.Location = new System.Drawing.Point(6, 336);
            this.btnFinalize.Name = "btnFinalize";
            this.btnFinalize.Size = new System.Drawing.Size(153, 23);
            this.btnFinalize.TabIndex = 24;
            this.btnFinalize.Text = "Finalize Appointment(s)";
            this.btnFinalize.UseVisualStyleBackColor = true;
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
            this.lvTutee.Location = new System.Drawing.Point(0, 24);
            this.lvTutee.Name = "lvTutee";
            this.lvTutee.Size = new System.Drawing.Size(699, 140);
            this.lvTutee.TabIndex = 22;
            this.lvTutee.UseCompatibleStateImageBehavior = false;
            this.lvTutee.View = System.Windows.Forms.View.Details;
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
            this.lvPendingTutee.Location = new System.Drawing.Point(0, 188);
            this.lvPendingTutee.Name = "lvPendingTutee";
            this.lvPendingTutee.Size = new System.Drawing.Size(699, 140);
            this.lvPendingTutee.TabIndex = 18;
            this.lvPendingTutee.UseCompatibleStateImageBehavior = false;
            this.lvPendingTutee.View = System.Windows.Forms.View.Details;
            // 
            // lblNameTitle
            // 
            this.lblNameTitle.AutoSize = true;
            this.lblNameTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNameTitle.Location = new System.Drawing.Point(13, 9);
            this.lblNameTitle.Name = "lblNameTitle";
            this.lblNameTitle.Size = new System.Drawing.Size(229, 24);
            this.lblNameTitle.TabIndex = 2;
            this.lblNameTitle.Text = "Student Name\'s Schedule";
            // 
            // btnDone
            // 
            this.btnDone.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnDone.Location = new System.Drawing.Point(627, 437);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(88, 23);
            this.btnDone.TabIndex = 3;
            this.btnDone.Text = "Done";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // AdminSeeSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 468);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.lblNameTitle);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AdminSeeSchedule";
            this.Text = "Student Schedule";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabAccepted.ResumeLayout(false);
            this.tabPendingTutor.ResumeLayout(false);
            this.tabPendingTutor.PerformLayout();
            this.tabPendingTutee.ResumeLayout(false);
            this.tabPendingTutee.PerformLayout();
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
    }
}