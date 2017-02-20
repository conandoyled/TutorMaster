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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabAvailability = new System.Windows.Forms.TabPage();
            this.tabAppointments = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabAccepted = new System.Windows.Forms.TabPage();
            this.tabPendingTutor = new System.Windows.Forms.TabPage();
            this.tabPendingTutee = new System.Windows.Forms.TabPage();
            this.lbxAccepted = new System.Windows.Forms.ListBox();
            this.cbxPendingTutor = new System.Windows.Forms.CheckedListBox();
            this.cbxPendingTutee = new System.Windows.Forms.CheckedListBox();
            this.btnLogout = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
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
            this.tabControl1.Size = new System.Drawing.Size(473, 332);
            this.tabControl1.TabIndex = 0;
            // 
            // tabAvailability
            // 
            this.tabAvailability.Location = new System.Drawing.Point(4, 22);
            this.tabAvailability.Name = "tabAvailability";
            this.tabAvailability.Padding = new System.Windows.Forms.Padding(3);
            this.tabAvailability.Size = new System.Drawing.Size(349, 227);
            this.tabAvailability.TabIndex = 0;
            this.tabAvailability.Text = "Availability";
            this.tabAvailability.UseVisualStyleBackColor = true;
            // 
            // tabAppointments
            // 
            this.tabAppointments.Controls.Add(this.tabControl2);
            this.tabAppointments.Location = new System.Drawing.Point(4, 22);
            this.tabAppointments.Name = "tabAppointments";
            this.tabAppointments.Padding = new System.Windows.Forms.Padding(3);
            this.tabAppointments.Size = new System.Drawing.Size(465, 306);
            this.tabAppointments.TabIndex = 1;
            this.tabAppointments.Text = "Appointments";
            this.tabAppointments.UseVisualStyleBackColor = true;
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
            // lbxAccepted
            // 
            this.lbxAccepted.FormattingEnabled = true;
            this.lbxAccepted.Location = new System.Drawing.Point(6, 5);
            this.lbxAccepted.Name = "lbxAccepted";
            this.lbxAccepted.Size = new System.Drawing.Size(435, 238);
            this.lbxAccepted.TabIndex = 0;
            // 
            // cbxPendingTutor
            // 
            this.cbxPendingTutor.FormattingEnabled = true;
            this.cbxPendingTutor.Location = new System.Drawing.Point(3, 2);
            this.cbxPendingTutor.Name = "cbxPendingTutor";
            this.cbxPendingTutor.Size = new System.Drawing.Size(435, 244);
            this.cbxPendingTutor.TabIndex = 0;
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
            this.btnLogout.Location = new System.Drawing.Point(493, 374);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(97, 23);
            this.btnLogout.TabIndex = 1;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(493, 331);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Make Request";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // StudentMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 427);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.tabControl1);
            this.Name = "StudentMain";
            this.Text = "StudentMain";
            this.tabControl1.ResumeLayout(false);
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
        private System.Windows.Forms.Button button1;
    }
}