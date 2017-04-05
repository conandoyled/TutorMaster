namespace TutorMaster
{
    partial class AdvancedRequest
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
            this.txtTutorName = new System.Windows.Forms.TextBox();
            this.txtClass = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.combTutorName = new System.Windows.Forms.ComboBox();
            this.tvClasses = new System.Windows.Forms.TreeView();
            this.lvTutorAvailability = new System.Windows.Forms.ListView();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtTutorName
            // 
            this.txtTutorName.Location = new System.Drawing.Point(44, 92);
            this.txtTutorName.Name = "txtTutorName";
            this.txtTutorName.Size = new System.Drawing.Size(93, 20);
            this.txtTutorName.TabIndex = 0;
            this.txtTutorName.Text = "Name of Tutor";
            // 
            // txtClass
            // 
            this.txtClass.Location = new System.Drawing.Point(234, 92);
            this.txtClass.Name = "txtClass";
            this.txtClass.Size = new System.Drawing.Size(110, 20);
            this.txtClass.TabIndex = 1;
            this.txtClass.Text = "Class to be tutored in";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(536, 92);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(203, 20);
            this.textBox3.TabIndex = 2;
            this.textBox3.Text = "What would be some good times for you?";
            // 
            // combTutorName
            // 
            this.combTutorName.FormattingEnabled = true;
            this.combTutorName.Location = new System.Drawing.Point(44, 156);
            this.combTutorName.Name = "combTutorName";
            this.combTutorName.Size = new System.Drawing.Size(93, 21);
            this.combTutorName.Sorted = true;
            this.combTutorName.TabIndex = 3;
            this.combTutorName.SelectedIndexChanged += new System.EventHandler(this.combTutorName_SelectedIndexChanged);
            // 
            // tvClasses
            // 
            this.tvClasses.Location = new System.Drawing.Point(165, 156);
            this.tvClasses.Name = "tvClasses";
            this.tvClasses.Size = new System.Drawing.Size(261, 337);
            this.tvClasses.TabIndex = 4;
            // 
            // lvTutorAvailability
            // 
            this.lvTutorAvailability.Location = new System.Drawing.Point(536, 156);
            this.lvTutorAvailability.Name = "lvTutorAvailability";
            this.lvTutorAvailability.Size = new System.Drawing.Size(203, 337);
            this.lvTutorAvailability.TabIndex = 5;
            this.lvTutorAvailability.UseCompatibleStateImageBehavior = false;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(536, 12);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(101, 23);
            this.btnExit.TabIndex = 15;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // AdvancedRequest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 542);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lvTutorAvailability);
            this.Controls.Add(this.tvClasses);
            this.Controls.Add(this.combTutorName);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.txtClass);
            this.Controls.Add(this.txtTutorName);
            this.Name = "AdvancedRequest";
            this.Text = "AdvancedRequest";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTutorName;
        private System.Windows.Forms.TextBox txtClass;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.ComboBox combTutorName;
        private System.Windows.Forms.TreeView tvClasses;
        private System.Windows.Forms.ListView lvTutorAvailability;
        private System.Windows.Forms.Button btnExit;
    }
}