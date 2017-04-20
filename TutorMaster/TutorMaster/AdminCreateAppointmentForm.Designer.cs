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
            this.SuspendLayout();
            // 
            // cbxClasses
            // 
            this.cbxClasses.FormattingEnabled = true;
            this.cbxClasses.Location = new System.Drawing.Point(13, 36);
            this.cbxClasses.Name = "cbxClasses";
            this.cbxClasses.Size = new System.Drawing.Size(153, 21);
            this.cbxClasses.TabIndex = 0;
            // 
            // cbxStudents
            // 
            this.cbxStudents.FormattingEnabled = true;
            this.cbxStudents.Location = new System.Drawing.Point(13, 84);
            this.cbxStudents.Name = "cbxStudents";
            this.cbxStudents.Size = new System.Drawing.Size(153, 21);
            this.cbxStudents.TabIndex = 1;
            // 
            // lvTimeMatches
            // 
            this.lvTimeMatches.Location = new System.Drawing.Point(12, 134);
            this.lvTimeMatches.Name = "lvTimeMatches";
            this.lvTimeMatches.Size = new System.Drawing.Size(306, 186);
            this.lvTimeMatches.TabIndex = 2;
            this.lvTimeMatches.UseCompatibleStateImageBehavior = false;
            this.lvTimeMatches.View = System.Windows.Forms.View.Details;
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
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(13, 327);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(102, 23);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "Set Appointment";
            this.btnSubmit.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(243, 326);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // AdminCreateAppointmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 364);
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

    }
}