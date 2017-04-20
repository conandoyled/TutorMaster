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
            this.SuspendLayout();
            // 
            // btnTutoring
            // 
            this.btnTutoring.Location = new System.Drawing.Point(25, 161);
            this.btnTutoring.Name = "btnTutoring";
            this.btnTutoring.Size = new System.Drawing.Size(75, 23);
            this.btnTutoring.TabIndex = 0;
            this.btnTutoring.Text = "Tutoring";
            this.btnTutoring.UseVisualStyleBackColor = true;
            this.btnTutoring.Click += new System.EventHandler(this.btnTutoring_Click);
            // 
            // btnBeingTutored
            // 
            this.btnBeingTutored.Location = new System.Drawing.Point(160, 161);
            this.btnBeingTutored.Name = "btnBeingTutored";
            this.btnBeingTutored.Size = new System.Drawing.Size(88, 23);
            this.btnBeingTutored.TabIndex = 1;
            this.btnBeingTutored.Text = "Being Tutored";
            this.btnBeingTutored.UseVisualStyleBackColor = true;
            this.btnBeingTutored.Click += new System.EventHandler(this.btnBeingTutored_Click);
            // 
            // TutorOrTuteeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.btnBeingTutored);
            this.Controls.Add(this.btnTutoring);
            this.Name = "TutorOrTuteeForm";
            this.Text = "TutorOrTuteeForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTutoring;
        private System.Windows.Forms.Button btnBeingTutored;
    }
}