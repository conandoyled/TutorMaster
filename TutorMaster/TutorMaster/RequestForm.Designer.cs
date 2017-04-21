namespace TutorMaster
{
    partial class RequestForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RequestForm));
            this.label3 = new System.Windows.Forms.Label();
            this.btnRequest = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.cbxWeekly = new System.Windows.Forms.CheckBox();
            this.combCourseName = new System.Windows.Forms.ComboBox();
            this.combHours = new System.Windows.Forms.ComboBox();
            this.combMins = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(173, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Course Name";
            // 
            // btnRequest
            // 
            this.btnRequest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(222)))), ((int)(((byte)(229)))));
            this.btnRequest.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRequest.Location = new System.Drawing.Point(18, 122);
            this.btnRequest.Name = "btnRequest";
            this.btnRequest.Size = new System.Drawing.Size(74, 23);
            this.btnRequest.TabIndex = 16;
            this.btnRequest.Text = "Request";
            this.btnRequest.UseVisualStyleBackColor = false;
            this.btnRequest.Click += new System.EventHandler(this.btnRequest_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(222)))), ((int)(((byte)(229)))));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(98, 122);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(74, 23);
            this.btnExit.TabIndex = 17;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // cbxWeekly
            // 
            this.cbxWeekly.AutoSize = true;
            this.cbxWeekly.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbxWeekly.Location = new System.Drawing.Point(18, 96);
            this.cbxWeekly.Name = "cbxWeekly";
            this.cbxWeekly.Size = new System.Drawing.Size(163, 17);
            this.cbxWeekly.TabIndex = 18;
            this.cbxWeekly.Text = "Is this a weekly appointment?";
            this.cbxWeekly.UseVisualStyleBackColor = true;
            this.cbxWeekly.CheckedChanged += new System.EventHandler(this.cbxWeekly_CheckedChanged);
            // 
            // combCourseName
            // 
            this.combCourseName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combCourseName.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.combCourseName.FormattingEnabled = true;
            this.combCourseName.Location = new System.Drawing.Point(18, 12);
            this.combCourseName.Name = "combCourseName";
            this.combCourseName.Size = new System.Drawing.Size(149, 21);
            this.combCourseName.TabIndex = 29;
            this.combCourseName.SelectedIndexChanged += new System.EventHandler(this.combCourseName_SelectedIndexChanged);
            // 
            // combHours
            // 
            this.combHours.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combHours.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.combHours.FormattingEnabled = true;
            this.combHours.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.combHours.Location = new System.Drawing.Point(18, 62);
            this.combHours.Name = "combHours";
            this.combHours.Size = new System.Drawing.Size(57, 21);
            this.combHours.TabIndex = 30;
            // 
            // combMins
            // 
            this.combMins.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combMins.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.combMins.FormattingEnabled = true;
            this.combMins.Items.AddRange(new object[] {
            "00",
            "15",
            "30",
            "45"});
            this.combMins.Location = new System.Drawing.Point(97, 62);
            this.combMins.Name = "combMins";
            this.combMins.Size = new System.Drawing.Size(57, 21);
            this.combMins.TabIndex = 31;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "Hours";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(94, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "Minutes";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TutorMaster.Properties.Resources.WatermarkR2;
            this.pictureBox1.Location = new System.Drawing.Point(188, 96);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(67, 69);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 34;
            this.pictureBox1.TabStop = false;
            // 
            // RequestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(232)))), ((int)(((byte)(239)))));
            this.ClientSize = new System.Drawing.Size(253, 162);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.combMins);
            this.Controls.Add(this.combHours);
            this.Controls.Add(this.combCourseName);
            this.Controls.Add(this.cbxWeekly);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnRequest);
            this.Controls.Add(this.label3);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(71)))), ((int)(((byte)(76)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RequestForm";
            this.Text = "TutorMaster";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRequest;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.CheckBox cbxWeekly;
        private System.Windows.Forms.ComboBox combCourseName;
        private System.Windows.Forms.ComboBox combHours;
        private System.Windows.Forms.ComboBox combMins;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}