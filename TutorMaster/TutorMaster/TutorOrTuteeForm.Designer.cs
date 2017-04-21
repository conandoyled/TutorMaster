namespace TutorMaster
{
    partial class TutorOrTuteeForm
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
            this.btnTutoring = new System.Windows.Forms.Button();
            this.btnBeingTutored = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnTutoring
            // 
            this.btnTutoring.Location = new System.Drawing.Point(3, 83);
            this.btnTutoring.Name = "btnTutoring";
            this.btnTutoring.Size = new System.Drawing.Size(75, 23);
            this.btnTutoring.TabIndex = 0;
            this.btnTutoring.Text = "Tutoring";
            this.btnTutoring.UseVisualStyleBackColor = true;
            this.btnTutoring.Click += new System.EventHandler(this.btnTutoring_Click);
            // 
            // btnBeingTutored
            // 
            this.btnBeingTutored.Location = new System.Drawing.Point(147, 83);
            this.btnBeingTutored.Name = "btnBeingTutored";
            this.btnBeingTutored.Size = new System.Drawing.Size(88, 23);
            this.btnBeingTutored.TabIndex = 1;
            this.btnBeingTutored.Text = "Being Tutored";
            this.btnBeingTutored.UseVisualStyleBackColor = true;
            this.btnBeingTutored.Click += new System.EventHandler(this.btnBeingTutored_Click);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Enabled = false;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(247, 80);
            this.label1.TabIndex = 2;
            this.label1.Text = "This student is registered as both a tutee and tutor. Which role will they play i" +
                "n this appointment?";
            // 
            // TutorOrTuteeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(247, 120);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBeingTutored);
            this.Controls.Add(this.btnTutoring);
            this.Name = "TutorOrTuteeForm";
            this.Text = "TutorOrTuteeForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTutoring;
        private System.Windows.Forms.Button btnBeingTutored;
        private System.Windows.Forms.Label label1;
    }
}