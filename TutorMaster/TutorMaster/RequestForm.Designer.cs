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
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(190, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Course Name";
            // 
            // btnRequest
            // 
            this.btnRequest.Location = new System.Drawing.Point(18, 122);
            this.btnRequest.Name = "btnRequest";
            this.btnRequest.Size = new System.Drawing.Size(69, 23);
            this.btnRequest.TabIndex = 16;
            this.btnRequest.Text = "Request";
            this.btnRequest.UseVisualStyleBackColor = true;
            this.btnRequest.Click += new System.EventHandler(this.btnRequest_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(93, 122);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(74, 23);
            this.btnExit.TabIndex = 17;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // cbxWeekly
            // 
            this.cbxWeekly.AutoSize = true;
            this.cbxWeekly.Location = new System.Drawing.Point(18, 99);
            this.cbxWeekly.Name = "cbxWeekly";
            this.cbxWeekly.Size = new System.Drawing.Size(165, 17);
            this.cbxWeekly.TabIndex = 18;
            this.cbxWeekly.Text = "Is this a weekly appointment?";
            this.cbxWeekly.UseVisualStyleBackColor = true;
            this.cbxWeekly.CheckedChanged += new System.EventHandler(this.cbxWeekly_CheckedChanged);
            // 
            // combCourseName
            // 
            this.combCourseName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.combHours.FormattingEnabled = true;
            this.combHours.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.combHours.Location = new System.Drawing.Point(18, 65);
            this.combHours.Name = "combHours";
            this.combHours.Size = new System.Drawing.Size(69, 21);
            this.combHours.TabIndex = 30;
            // 
            // combMins
            // 
            this.combMins.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combMins.FormattingEnabled = true;
            this.combMins.Items.AddRange(new object[] {
            "00",
            "15",
            "30",
            "45"});
            this.combMins.Location = new System.Drawing.Point(110, 65);
            this.combMins.Name = "combMins";
            this.combMins.Size = new System.Drawing.Size(57, 21);
            this.combMins.TabIndex = 31;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "Hours";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(110, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "Minutes";
            // 
            // RequestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 164);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.combMins);
            this.Controls.Add(this.combHours);
            this.Controls.Add(this.combCourseName);
            this.Controls.Add(this.cbxWeekly);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnRequest);
            this.Controls.Add(this.label3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RequestForm";
            this.Text = "RequestForm";
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
    }
}