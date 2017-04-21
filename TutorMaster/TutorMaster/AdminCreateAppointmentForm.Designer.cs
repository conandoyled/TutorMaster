namespace TutorMaster
{
    partial class AdminCreateAppointmentForm
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
            this.cbxClasses = new System.Windows.Forms.ComboBox();
            this.cbxStudents = new System.Windows.Forms.ComboBox();
            this.lvTimeMatches = new System.Windows.Forms.ListView();
            this.cbWeekly = new System.Windows.Forms.CheckBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbxHour = new System.Windows.Forms.ComboBox();
            this.cbxMinutes = new System.Windows.Forms.ComboBox();
            this.lblClass = new System.Windows.Forms.Label();
            this.lblStudent = new System.Windows.Forms.Label();
            this.lblHour = new System.Windows.Forms.Label();
            this.lblMinutes = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbxClasses
            // 
            this.cbxClasses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxClasses.FormattingEnabled = true;
            this.cbxClasses.Location = new System.Drawing.Point(13, 36);
            this.cbxClasses.Name = "cbxClasses";
            this.cbxClasses.Size = new System.Drawing.Size(116, 21);
            this.cbxClasses.TabIndex = 0;
            this.cbxClasses.SelectedIndexChanged += new System.EventHandler(this.cbxClasses_SelectedIndexChanged);
            // 
            // cbxStudents
            // 
            this.cbxStudents.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxStudents.FormattingEnabled = true;
            this.cbxStudents.Location = new System.Drawing.Point(187, 36);
            this.cbxStudents.Name = "cbxStudents";
            this.cbxStudents.Size = new System.Drawing.Size(121, 21);
            this.cbxStudents.TabIndex = 1;
            this.cbxStudents.SelectedIndexChanged += new System.EventHandler(this.cbxStudents_SelectedIndexChanged);
            // 
            // lvTimeMatches
            // 
            this.lvTimeMatches.Location = new System.Drawing.Point(12, 134);
            this.lvTimeMatches.Name = "lvTimeMatches";
            this.lvTimeMatches.Size = new System.Drawing.Size(306, 186);
            this.lvTimeMatches.TabIndex = 2;
            this.lvTimeMatches.UseCompatibleStateImageBehavior = false;
            this.lvTimeMatches.View = System.Windows.Forms.View.Details;
            this.lvTimeMatches.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvTimeMatches_ItemCheck);
            // 
            // cbWeekly
            // 
            this.cbWeekly.AutoSize = true;
            this.cbWeekly.Location = new System.Drawing.Point(13, 111);
            this.cbWeekly.Name = "cbWeekly";
            this.cbWeekly.Size = new System.Drawing.Size(62, 17);
            this.cbWeekly.TabIndex = 3;
            this.cbWeekly.Text = "Weekly";
            this.cbWeekly.UseVisualStyleBackColor = true;
            this.cbWeekly.CheckedChanged += new System.EventHandler(this.cbWeekly_CheckedChanged);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(13, 327);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(102, 23);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "Set Appointment";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(243, 326);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cbxHour
            // 
            this.cbxHour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxHour.FormattingEnabled = true;
            this.cbxHour.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.cbxHour.Location = new System.Drawing.Point(12, 84);
            this.cbxHour.Name = "cbxHour";
            this.cbxHour.Size = new System.Drawing.Size(117, 21);
            this.cbxHour.TabIndex = 6;
            this.cbxHour.SelectedIndexChanged += new System.EventHandler(this.cbxHour_SelectedIndexChanged);
            // 
            // cbxMinutes
            // 
            this.cbxMinutes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMinutes.FormattingEnabled = true;
            this.cbxMinutes.Items.AddRange(new object[] {
            "00",
            "15",
            "30",
            "45"});
            this.cbxMinutes.Location = new System.Drawing.Point(187, 83);
            this.cbxMinutes.Name = "cbxMinutes";
            this.cbxMinutes.Size = new System.Drawing.Size(121, 21);
            this.cbxMinutes.TabIndex = 7;
            this.cbxMinutes.SelectedIndexChanged += new System.EventHandler(this.cbxMinutes_SelectedIndexChanged);
            // 
            // lblClass
            // 
            this.lblClass.AutoSize = true;
            this.lblClass.Location = new System.Drawing.Point(12, 17);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(32, 13);
            this.lblClass.TabIndex = 8;
            this.lblClass.Text = "Class";
            // 
            // lblStudent
            // 
            this.lblStudent.AutoSize = true;
            this.lblStudent.Location = new System.Drawing.Point(187, 17);
            this.lblStudent.Name = "lblStudent";
            this.lblStudent.Size = new System.Drawing.Size(41, 13);
            this.lblStudent.TabIndex = 9;
            this.lblStudent.Text = "Partner";
            // 
            // lblHour
            // 
            this.lblHour.AutoSize = true;
            this.lblHour.Location = new System.Drawing.Point(12, 65);
            this.lblHour.Name = "lblHour";
            this.lblHour.Size = new System.Drawing.Size(35, 13);
            this.lblHour.TabIndex = 10;
            this.lblHour.Text = "Hours";
            // 
            // lblMinutes
            // 
            this.lblMinutes.AutoSize = true;
            this.lblMinutes.Location = new System.Drawing.Point(190, 65);
            this.lblMinutes.Name = "lblMinutes";
            this.lblMinutes.Size = new System.Drawing.Size(44, 13);
            this.lblMinutes.TabIndex = 11;
            this.lblMinutes.Text = "Minutes";
            // 
            // AdminCreateAppointmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 364);
            this.Controls.Add(this.lblMinutes);
            this.Controls.Add(this.lblHour);
            this.Controls.Add(this.lblStudent);
            this.Controls.Add(this.lblClass);
            this.Controls.Add(this.cbxMinutes);
            this.Controls.Add(this.cbxHour);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.cbWeekly);
            this.Controls.Add(this.lvTimeMatches);
            this.Controls.Add(this.cbxStudents);
            this.Controls.Add(this.cbxClasses);
            this.Name = "AdminCreateAppointmentForm";
            this.Text = "AdminCreateAppointmentForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxClasses;
        private System.Windows.Forms.ComboBox cbxStudents;
        private System.Windows.Forms.ListView lvTimeMatches;
        private System.Windows.Forms.CheckBox cbWeekly;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cbxHour;
        private System.Windows.Forms.ComboBox cbxMinutes;
        private System.Windows.Forms.Label lblClass;
        private System.Windows.Forms.Label lblStudent;
        private System.Windows.Forms.Label lblHour;
        private System.Windows.Forms.Label lblMinutes;

    }
}