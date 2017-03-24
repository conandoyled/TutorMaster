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
            this.SuspendLayout();
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(73, 248);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 1;
            this.btnAccept.Text = "Accept";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnReject
            // 
            this.btnReject.Location = new System.Drawing.Point(168, 248);
            this.btnReject.Name = "btnReject";
            this.btnReject.Size = new System.Drawing.Size(75, 23);
            this.btnReject.TabIndex = 2;
            this.btnReject.Text = "Reject";
            this.btnReject.UseVisualStyleBackColor = true;
            this.btnReject.Click += new System.EventHandler(this.btnReject_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(119, 277);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(75, 23);
            this.btnLogout.TabIndex = 3;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // lvPendingRequests
            // 
            this.lvPendingRequests.Cursor = System.Windows.Forms.Cursors.Default;
            this.lvPendingRequests.Location = new System.Drawing.Point(12, 12);
            this.lvPendingRequests.Name = "lvPendingRequests";
            this.lvPendingRequests.Size = new System.Drawing.Size(303, 230);
            this.lvPendingRequests.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvPendingRequests.TabIndex = 4;
            this.lvPendingRequests.UseCompatibleStateImageBehavior = false;
            this.lvPendingRequests.View = System.Windows.Forms.View.Details;
            this.lvPendingRequests.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // FacultyMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 312);
            this.Controls.Add(this.lvPendingRequests);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnReject);
            this.Controls.Add(this.btnAccept);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FacultyMain";
            this.Text = "FacultyMain";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FacultyMain_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnReject;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.ListView lvPendingRequests;
    }
}