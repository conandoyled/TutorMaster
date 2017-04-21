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
            this.tbxLocation = new System.Windows.Forms.TextBox();
            this.lblClasses = new System.Windows.Forms.Label();
            this.lblPartner = new System.Windows.Forms.Label();
            this.lblHours = new System.Windows.Forms.Label();
            this.lblMinutes = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbxClasses
            // 
            this.cbxClasses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxClasses.FormattingEnabled = true;
            this.cbxClasses.Location = new System.Drawing.Point(12, 45);
            this.cbxClasses.Name = "cbxClasses";
            this.cbxClasses.Size = new System.Drawing.Size(170, 21);
            this.cbxClasses.TabIndex = 0;
            this.cbxClasses.SelectedIndexChanged += new System.EventHandler(this.cbxClasses_SelectedIndexChanged);
            // 
            // cbxStudents
            // 
            this.cbxStudents.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxStudents.FormattingEnabled = true;
            this.cbxStudents.Location = new System.Drawing.Point(211, 45);
            this.cbxStudents.Name = "cbxStudents";
            this.cbxStudents.Size = new System.Drawing.Size(153, 21);
            this.cbxStudents.TabIndex = 1;
            this.cbxStudents.SelectedIndexChanged += new System.EventHandler(this.cbxStudents_SelectedIndexChanged);
            // 
            // lvTimeMatches
            // 
            this.lvTimeMatches.Location = new System.Drawing.Point(12, 214);
            this.lvTimeMatches.Name = "lvTimeMatches";
            this.lvTimeMatches.Size = new System.Drawing.Size(352, 186);
            this.lvTimeMatches.TabIndex = 2;
            this.lvTimeMatches.UseCompatibleStateImageBehavior = false;
            this.lvTimeMatches.View = System.Windows.Forms.View.Details;
            this.lvTimeMatches.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvTimeMatches_ItemCheck);
            this.lvTimeMatches.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvTimeMatches_ItemChecked);
            // 
            // cbWeekly
            // 
            this.cbWeekly.AutoSize = true;
            this.cbWeekly.Location = new System.Drawing.Point(214, 175);
            this.cbWeekly.Name = "cbWeekly";
            this.cbWeekly.Size = new System.Drawing.Size(62, 17);
            this.cbWeekly.TabIndex = 3;
            this.cbWeekly.Text = "Weekly";
            this.cbWeekly.UseVisualStyleBackColor = true;
            this.cbWeekly.CheckedChanged += new System.EventHandler(this.cbWeekly_CheckedChanged);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(11, 406);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(102, 23);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "Set Appointment";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(289, 406);
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
            this.cbxHour.Location = new System.Drawing.Point(11, 110);
            this.cbxHour.Name = "cbxHour";
            this.cbxHour.Size = new System.Drawing.Size(170, 21);
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
            this.cbxMinutes.Location = new System.Drawing.Point(214, 110);
            this.cbxMinutes.Name = "cbxMinutes";
            this.cbxMinutes.Size = new System.Drawing.Size(153, 21);
            this.cbxMinutes.TabIndex = 7;
            this.cbxMinutes.SelectedIndexChanged += new System.EventHandler(this.cbxMinutes_SelectedIndexChanged);
            // 
            // tbxLocation
            // 
            this.tbxLocation.Location = new System.Drawing.Point(10, 173);
            this.tbxLocation.Name = "tbxLocation";
            this.tbxLocation.Size = new System.Drawing.Size(171, 20);
            this.tbxLocation.TabIndex = 8;
            // 
            // lblClasses
            // 
            this.lblClasses.AutoSize = true;
            this.lblClasses.Location = new System.Drawing.Point(13, 26);
            this.lblClasses.Name = "lblClasses";
            this.lblClasses.Size = new System.Drawing.Size(32, 13);
            this.lblClasses.TabIndex = 9;
            this.lblClasses.Text = "Class";
            // 
            // lblPartner
            // 
            this.lblPartner.AutoSize = true;
            this.lblPartner.Location = new System.Drawing.Point(211, 26);
            this.lblPartner.Name = "lblPartner";
            this.lblPartner.Size = new System.Drawing.Size(41, 13);
            this.lblPartner.TabIndex = 10;
            this.lblPartner.Text = "Partner";
            // 
            // lblHours
            // 
            this.lblHours.AutoSize = true;
            this.lblHours.Location = new System.Drawing.Point(12, 91);
            this.lblHours.Name = "lblHours";
            this.lblHours.Size = new System.Drawing.Size(85, 13);
            this.lblHours.TabIndex = 11;
            this.lblHours.Text = "Hours of session";
            // 
            // lblMinutes
            // 
            this.lblMinutes.AutoSize = true;
            this.lblMinutes.Location = new System.Drawing.Point(214, 91);
            this.lblMinutes.Name = "lblMinutes";
            this.lblMinutes.Size = new System.Drawing.Size(94, 13);
            this.lblMinutes.TabIndex = 12;
            this.lblMinutes.Text = "Minutes of session";
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(11, 154);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(98, 13);
            this.lblLocation.TabIndex = 13;
            this.lblLocation.Text = "Location of session";
            // 
            // AdminCreateAppointmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 441);
            this.Controls.Add(this.lblLocation);
            this.Controls.Add(this.lblMinutes);
            this.Controls.Add(this.lblHours);
            this.Controls.Add(this.lblPartner);
            this.Controls.Add(this.lblClasses);
            this.Controls.Add(this.tbxLocation);
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
        private System.Windows.Forms.TextBox tbxLocation;
        private System.Windows.Forms.Label lblClasses;
        private System.Windows.Forms.Label lblPartner;
        private System.Windows.Forms.Label lblHours;
        private System.Windows.Forms.Label lblMinutes;
        private System.Windows.Forms.Label lblLocation;

    }
}