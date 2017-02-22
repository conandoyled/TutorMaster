﻿namespace TutorMaster
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
            this.combEndHalf = new System.Windows.Forms.ComboBox();
            this.combEndMinute = new System.Windows.Forms.ComboBox();
            this.combEndHour = new System.Windows.Forms.ComboBox();
            this.combStartHalf = new System.Windows.Forms.ComboBox();
            this.combStartMinute = new System.Windows.Forms.ComboBox();
            this.combStartHour = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCourse = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnRequest = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // combEndHalf
            // 
            this.combEndHalf.FormattingEnabled = true;
            this.combEndHalf.Items.AddRange(new object[] {
            "AM",
            "PM"});
            this.combEndHalf.Location = new System.Drawing.Point(118, 115);
            this.combEndHalf.Name = "combEndHalf";
            this.combEndHalf.Size = new System.Drawing.Size(50, 21);
            this.combEndHalf.TabIndex = 11;
            // 
            // combEndMinute
            // 
            this.combEndMinute.FormattingEnabled = true;
            this.combEndMinute.Items.AddRange(new object[] {
            "00",
            "15",
            "30",
            "45"});
            this.combEndMinute.Location = new System.Drawing.Point(63, 115);
            this.combEndMinute.Name = "combEndMinute";
            this.combEndMinute.Size = new System.Drawing.Size(49, 21);
            this.combEndMinute.TabIndex = 10;
            // 
            // combEndHour
            // 
            this.combEndHour.FormattingEnabled = true;
            this.combEndHour.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12"});
            this.combEndHour.Location = new System.Drawing.Point(13, 115);
            this.combEndHour.Name = "combEndHour";
            this.combEndHour.Size = new System.Drawing.Size(44, 21);
            this.combEndHour.TabIndex = 9;
            // 
            // combStartHalf
            // 
            this.combStartHalf.FormattingEnabled = true;
            this.combStartHalf.Items.AddRange(new object[] {
            "AM",
            "PM"});
            this.combStartHalf.Location = new System.Drawing.Point(118, 88);
            this.combStartHalf.Name = "combStartHalf";
            this.combStartHalf.Size = new System.Drawing.Size(50, 21);
            this.combStartHalf.TabIndex = 8;
            // 
            // combStartMinute
            // 
            this.combStartMinute.FormattingEnabled = true;
            this.combStartMinute.Items.AddRange(new object[] {
            "00",
            "15",
            "30",
            "45"});
            this.combStartMinute.Location = new System.Drawing.Point(63, 89);
            this.combStartMinute.Name = "combStartMinute";
            this.combStartMinute.Size = new System.Drawing.Size(49, 21);
            this.combStartMinute.TabIndex = 7;
            // 
            // combStartHour
            // 
            this.combStartHour.FormattingEnabled = true;
            this.combStartHour.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12"});
            this.combStartHour.Location = new System.Drawing.Point(13, 89);
            this.combStartHour.Name = "combStartHour";
            this.combStartHour.Size = new System.Drawing.Size(44, 21);
            this.combStartHour.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(174, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "End Time";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(174, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Start Time";
            // 
            // txtCourse
            // 
            this.txtCourse.Location = new System.Drawing.Point(68, 37);
            this.txtCourse.Name = "txtCourse";
            this.txtCourse.Size = new System.Drawing.Size(100, 20);
            this.txtCourse.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(174, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Course";
            // 
            // btnRequest
            // 
            this.btnRequest.Location = new System.Drawing.Point(13, 153);
            this.btnRequest.Name = "btnRequest";
            this.btnRequest.Size = new System.Drawing.Size(75, 23);
            this.btnRequest.TabIndex = 16;
            this.btnRequest.Text = "Request";
            this.btnRequest.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(94, 153);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(74, 23);
            this.btnExit.TabIndex = 17;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // RequestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 198);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnRequest);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCourse);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.combEndHalf);
            this.Controls.Add(this.combEndMinute);
            this.Controls.Add(this.combEndHour);
            this.Controls.Add(this.combStartHalf);
            this.Controls.Add(this.combStartMinute);
            this.Controls.Add(this.combStartHour);
            this.Name = "RequestForm";
            this.Text = "RequestForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox combEndHalf;
        private System.Windows.Forms.ComboBox combEndMinute;
        private System.Windows.Forms.ComboBox combEndHour;
        private System.Windows.Forms.ComboBox combStartHalf;
        private System.Windows.Forms.ComboBox combStartMinute;
        private System.Windows.Forms.ComboBox combStartHour;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCourse;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRequest;
        private System.Windows.Forms.Button btnExit;
    }
}