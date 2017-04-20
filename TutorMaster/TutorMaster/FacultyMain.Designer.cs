namespace TutorMaster
{
    partial class FacultyMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FacultyMain));
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnReject = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.lvPendingRequests = new System.Windows.Forms.ListView();
            this.btnChangePassword = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAccept
            // 
            this.btnAccept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(222)))), ((int)(((byte)(229)))));
            this.btnAccept.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAccept.Location = new System.Drawing.Point(83, 307);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(103, 23);
            this.btnAccept.TabIndex = 1;
            this.btnAccept.Text = "Accept";
            this.btnAccept.UseVisualStyleBackColor = false;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnReject
            // 
            this.btnReject.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(222)))), ((int)(((byte)(229)))));
            this.btnReject.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReject.Location = new System.Drawing.Point(206, 307);
            this.btnReject.Name = "btnReject";
            this.btnReject.Size = new System.Drawing.Size(107, 23);
            this.btnReject.TabIndex = 2;
            this.btnReject.Text = "Reject";
            this.btnReject.UseVisualStyleBackColor = false;
            this.btnReject.Click += new System.EventHandler(this.btnReject_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(222)))), ((int)(((byte)(229)))));
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLogout.Location = new System.Drawing.Point(206, 336);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(107, 23);
            this.btnLogout.TabIndex = 3;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // lvPendingRequests
            // 
            this.lvPendingRequests.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.lvPendingRequests.Cursor = System.Windows.Forms.Cursors.Default;
            this.lvPendingRequests.Location = new System.Drawing.Point(45, 59);
            this.lvPendingRequests.Name = "lvPendingRequests";
            this.lvPendingRequests.Size = new System.Drawing.Size(303, 230);
            this.lvPendingRequests.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvPendingRequests.TabIndex = 4;
            this.lvPendingRequests.UseCompatibleStateImageBehavior = false;
            this.lvPendingRequests.View = System.Windows.Forms.View.Details;
            this.lvPendingRequests.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvPendingRequests_ItemChecked);
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(222)))), ((int)(((byte)(229)))));
            this.btnChangePassword.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnChangePassword.Location = new System.Drawing.Point(83, 336);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(103, 23);
            this.btnChangePassword.TabIndex = 6;
            this.btnChangePassword.Text = "Change Password";
            this.btnChangePassword.UseVisualStyleBackColor = false;
            this.btnChangePassword.Click += new System.EventHandler(this.btnChangePassword_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TutorMaster.Properties.Resources.WatermarkR2;
            this.pictureBox1.Location = new System.Drawing.Point(319, 299);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(81, 77);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 34;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(56, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(281, 15);
            this.label1.TabIndex = 35;
            this.label1.Text = "Please select students to approve or deny as tutors";
            // 
            // FacultyMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(232)))), ((int)(((byte)(239)))));
            this.ClientSize = new System.Drawing.Size(397, 373);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnChangePassword);
            this.Controls.Add(this.lvPendingRequests);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnReject);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.pictureBox1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(71)))), ((int)(((byte)(76)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FacultyMain";
            this.Text = "FacultyMain";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FacultyMain_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnReject;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.ListView lvPendingRequests;
        private System.Windows.Forms.Button btnChangePassword;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
    }
}