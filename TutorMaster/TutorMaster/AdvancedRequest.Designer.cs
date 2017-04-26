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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvancedRequest));
            this.combFirstChoice = new System.Windows.Forms.ComboBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblFirstChoice = new System.Windows.Forms.Label();
            this.lblSecondChoice = new System.Windows.Forms.Label();
            this.lblAvailableTimes = new System.Windows.Forms.Label();
            this.lblChoice = new System.Windows.Forms.Label();
            this.btnByTutor = new System.Windows.Forms.Button();
            this.btnByClass = new System.Windows.Forms.Button();
            this.combSecondChoice = new System.Windows.Forms.ComboBox();
            this.btnSendRequest = new System.Windows.Forms.Button();
            this.lblHowLong = new System.Windows.Forms.Label();
            this.cbxWeekly = new System.Windows.Forms.CheckBox();
            this.btnFindMatches = new System.Windows.Forms.Button();
            this.lvAvailableTimes = new System.Windows.Forms.ListView();
            this.lblMin = new System.Windows.Forms.Label();
            this.lblHour = new System.Windows.Forms.Label();
            this.combStartMinute = new System.Windows.Forms.ComboBox();
            this.combStartHour = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // combFirstChoice
            // 
            this.combFirstChoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combFirstChoice.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.combFirstChoice.FormattingEnabled = true;
            this.combFirstChoice.Location = new System.Drawing.Point(81, 89);
            this.combFirstChoice.Name = "combFirstChoice";
            this.combFirstChoice.Size = new System.Drawing.Size(173, 21);
            this.combFirstChoice.Sorted = true;
            this.combFirstChoice.TabIndex = 3;
            this.combFirstChoice.DropDownClosed += new System.EventHandler(this.combFirstChoice_DropDownClosed);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(222)))), ((int)(((byte)(229)))));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(95, 459);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(137, 23);
            this.btnExit.TabIndex = 15;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblFirstChoice
            // 
            this.lblFirstChoice.AutoSize = true;
            this.lblFirstChoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFirstChoice.Location = new System.Drawing.Point(130, 71);
            this.lblFirstChoice.Name = "lblFirstChoice";
            this.lblFirstChoice.Size = new System.Drawing.Size(72, 15);
            this.lblFirstChoice.TabIndex = 16;
            this.lblFirstChoice.Text = "Tutor Name";
            // 
            // lblSecondChoice
            // 
            this.lblSecondChoice.AutoSize = true;
            this.lblSecondChoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSecondChoice.Location = new System.Drawing.Point(115, 198);
            this.lblSecondChoice.Name = "lblSecondChoice";
            this.lblSecondChoice.Size = new System.Drawing.Size(102, 15);
            this.lblSecondChoice.TabIndex = 17;
            this.lblSecondChoice.Text = "Classes Available";
            // 
            // lblAvailableTimes
            // 
            this.lblAvailableTimes.AutoSize = true;
            this.lblAvailableTimes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvailableTimes.Location = new System.Drawing.Point(119, 288);
            this.lblAvailableTimes.Name = "lblAvailableTimes";
            this.lblAvailableTimes.Size = new System.Drawing.Size(93, 15);
            this.lblAvailableTimes.TabIndex = 18;
            this.lblAvailableTimes.Text = "Available Times";
            // 
            // lblChoice
            // 
            this.lblChoice.AutoSize = true;
            this.lblChoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChoice.Location = new System.Drawing.Point(86, 16);
            this.lblChoice.Name = "lblChoice";
            this.lblChoice.Size = new System.Drawing.Size(155, 15);
            this.lblChoice.TabIndex = 19;
            this.lblChoice.Text = "Search by tutor or by class?";
            // 
            // btnByTutor
            // 
            this.btnByTutor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(222)))), ((int)(((byte)(229)))));
            this.btnByTutor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnByTutor.Location = new System.Drawing.Point(89, 35);
            this.btnByTutor.Name = "btnByTutor";
            this.btnByTutor.Size = new System.Drawing.Size(75, 23);
            this.btnByTutor.TabIndex = 20;
            this.btnByTutor.Text = "Tutor";
            this.btnByTutor.UseVisualStyleBackColor = false;
            this.btnByTutor.Click += new System.EventHandler(this.btnByTutor_Click);
            // 
            // btnByClass
            // 
            this.btnByClass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(222)))), ((int)(((byte)(229)))));
            this.btnByClass.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnByClass.Location = new System.Drawing.Point(170, 35);
            this.btnByClass.Name = "btnByClass";
            this.btnByClass.Size = new System.Drawing.Size(75, 23);
            this.btnByClass.TabIndex = 21;
            this.btnByClass.Text = "Class";
            this.btnByClass.UseVisualStyleBackColor = false;
            this.btnByClass.Click += new System.EventHandler(this.btnByClass_Click);
            // 
            // combSecondChoice
            // 
            this.combSecondChoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combSecondChoice.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.combSecondChoice.FormattingEnabled = true;
            this.combSecondChoice.Location = new System.Drawing.Point(81, 218);
            this.combSecondChoice.Name = "combSecondChoice";
            this.combSecondChoice.Size = new System.Drawing.Size(173, 21);
            this.combSecondChoice.TabIndex = 24;
            // 
            // btnSendRequest
            // 
            this.btnSendRequest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(222)))), ((int)(((byte)(229)))));
            this.btnSendRequest.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSendRequest.Location = new System.Drawing.Point(95, 431);
            this.btnSendRequest.Margin = new System.Windows.Forms.Padding(2);
            this.btnSendRequest.Name = "btnSendRequest";
            this.btnSendRequest.Size = new System.Drawing.Size(137, 23);
            this.btnSendRequest.TabIndex = 26;
            this.btnSendRequest.Text = "Submit Request";
            this.btnSendRequest.UseVisualStyleBackColor = false;
            // 
            // lblHowLong
            // 
            this.lblHowLong.AutoSize = true;
            this.lblHowLong.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHowLong.Location = new System.Drawing.Point(78, 125);
            this.lblHowLong.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHowLong.Name = "lblHowLong";
            this.lblHowLong.Size = new System.Drawing.Size(178, 15);
            this.lblHowLong.TabIndex = 29;
            this.lblHowLong.Text = "How long do you want to meet?";
            // 
            // cbxWeekly
            // 
            this.cbxWeekly.AutoSize = true;
            this.cbxWeekly.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbxWeekly.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxWeekly.Location = new System.Drawing.Point(136, 170);
            this.cbxWeekly.Margin = new System.Windows.Forms.Padding(2);
            this.cbxWeekly.Name = "cbxWeekly";
            this.cbxWeekly.Size = new System.Drawing.Size(63, 19);
            this.cbxWeekly.TabIndex = 31;
            this.cbxWeekly.Text = "Weekly";
            this.cbxWeekly.UseVisualStyleBackColor = true;
            // 
            // btnFindMatches
            // 
            this.btnFindMatches.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(222)))), ((int)(((byte)(229)))));
            this.btnFindMatches.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFindMatches.Location = new System.Drawing.Point(110, 258);
            this.btnFindMatches.Name = "btnFindMatches";
            this.btnFindMatches.Size = new System.Drawing.Size(111, 23);
            this.btnFindMatches.TabIndex = 34;
            this.btnFindMatches.Text = "Find Matches";
            this.btnFindMatches.UseVisualStyleBackColor = false;
            this.btnFindMatches.Click += new System.EventHandler(this.btnFindMatches_Click);
            // 
            // lvAvailableTimes
            // 
            this.lvAvailableTimes.Location = new System.Drawing.Point(12, 306);
            this.lvAvailableTimes.Name = "lvAvailableTimes";
            this.lvAvailableTimes.Size = new System.Drawing.Size(307, 119);
            this.lvAvailableTimes.TabIndex = 36;
            this.lvAvailableTimes.UseCompatibleStateImageBehavior = false;
            this.lvAvailableTimes.View = System.Windows.Forms.View.Details;
            this.lvAvailableTimes.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvAvailableTimes_ItemChecked);
            // 
            // lblMin
            // 
            this.lblMin.AutoSize = true;
            this.lblMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(71)))), ((int)(((byte)(76)))));
            this.lblMin.Location = new System.Drawing.Point(157, 148);
            this.lblMin.Name = "lblMin";
            this.lblMin.Size = new System.Drawing.Size(31, 15);
            this.lblMin.TabIndex = 40;
            this.lblMin.Text = "Min:";
            // 
            // lblHour
            // 
            this.lblHour.AutoSize = true;
            this.lblHour.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHour.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(71)))), ((int)(((byte)(76)))));
            this.lblHour.Location = new System.Drawing.Point(91, 148);
            this.lblHour.Name = "lblHour";
            this.lblHour.Size = new System.Drawing.Size(23, 15);
            this.lblHour.TabIndex = 39;
            this.lblHour.Text = "Hr:";
            // 
            // combStartMinute
            // 
            this.combStartMinute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combStartMinute.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.combStartMinute.FormattingEnabled = true;
            this.combStartMinute.Items.AddRange(new object[] {
            "00",
            "15",
            "30",
            "45"});
            this.combStartMinute.Location = new System.Drawing.Point(191, 145);
            this.combStartMinute.Name = "combStartMinute";
            this.combStartMinute.Size = new System.Drawing.Size(39, 21);
            this.combStartMinute.TabIndex = 38;
            // 
            // combStartHour
            // 
            this.combStartHour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combStartHour.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.combStartHour.FormattingEnabled = true;
            this.combStartHour.Items.AddRange(new object[] {
            "00",
            "01",
            "02",
            "03"});
            this.combStartHour.Location = new System.Drawing.Point(117, 145);
            this.combStartHour.Name = "combStartHour";
            this.combStartHour.Size = new System.Drawing.Size(37, 21);
            this.combStartHour.TabIndex = 37;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TutorMaster.Properties.Resources.WatermarkR2;
            this.pictureBox1.Location = new System.Drawing.Point(255, 431);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(76, 74);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 41;
            this.pictureBox1.TabStop = false;
            // 
            // AdvancedRequest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(232)))), ((int)(((byte)(239)))));
            this.ClientSize = new System.Drawing.Size(331, 503);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblMin);
            this.Controls.Add(this.lblHour);
            this.Controls.Add(this.combStartMinute);
            this.Controls.Add(this.combStartHour);
            this.Controls.Add(this.lvAvailableTimes);
            this.Controls.Add(this.btnFindMatches);
            this.Controls.Add(this.cbxWeekly);
            this.Controls.Add(this.lblHowLong);
            this.Controls.Add(this.btnSendRequest);
            this.Controls.Add(this.combSecondChoice);
            this.Controls.Add(this.btnByClass);
            this.Controls.Add(this.btnByTutor);
            this.Controls.Add(this.lblChoice);
            this.Controls.Add(this.lblAvailableTimes);
            this.Controls.Add(this.lblSecondChoice);
            this.Controls.Add(this.lblFirstChoice);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.combFirstChoice);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(71)))), ((int)(((byte)(76)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AdvancedRequest";
            this.Text = "TutorMaster";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AdvancedRequest_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox combFirstChoice;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblFirstChoice;
        private System.Windows.Forms.Label lblSecondChoice;
        private System.Windows.Forms.Label lblAvailableTimes;
        private System.Windows.Forms.Label lblChoice;
        private System.Windows.Forms.Button btnByTutor;
        private System.Windows.Forms.Button btnByClass;
        private System.Windows.Forms.ComboBox combSecondChoice;
        private System.Windows.Forms.Button btnSendRequest;
        private System.Windows.Forms.Label lblHowLong;
        private System.Windows.Forms.CheckBox cbxWeekly;
        private System.Windows.Forms.Button btnFindMatches;
        private System.Windows.Forms.ListView lvAvailableTimes;
        private System.Windows.Forms.Label lblMin;
        private System.Windows.Forms.Label lblHour;
        private System.Windows.Forms.ComboBox combStartMinute;
        private System.Windows.Forms.ComboBox combStartHour;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}