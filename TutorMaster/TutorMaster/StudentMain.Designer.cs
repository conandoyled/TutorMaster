namespace TutorMaster
{
    partial class StudentMain
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabAvailability = new System.Windows.Forms.TabPage();
            this.tabAppointments = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabAccepted = new System.Windows.Forms.TabPage();
            this.lbxAccepted = new System.Windows.Forms.ListBox();
            this.tabPendingTutor = new System.Windows.Forms.TabPage();
            this.cbxPendingTutor = new System.Windows.Forms.CheckedListBox();
            this.tabPendingTutee = new System.Windows.Forms.TabPage();
            this.cbxPendingTutee = new System.Windows.Forms.CheckedListBox();
            this.btnLogout = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.comboBox5 = new System.Windows.Forms.ComboBox();
            this.comboBox6 = new System.Windows.Forms.ComboBox();
            this.btnAddOpenBlock = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabAvailability.SuspendLayout();
            this.tabAppointments.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabAccepted.SuspendLayout();
            this.tabPendingTutor.SuspendLayout();
            this.tabPendingTutee.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabAvailability);
            this.tabControl1.Controls.Add(this.tabAppointments);
            this.tabControl1.Location = new System.Drawing.Point(13, 69);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(473, 332);
            this.tabControl1.TabIndex = 0;
            // 
            // tabAvailability
            // 
            this.tabAvailability.BackColor = System.Drawing.Color.Transparent;
            this.tabAvailability.Controls.Add(this.label2);
            this.tabAvailability.Controls.Add(this.label1);
            this.tabAvailability.Controls.Add(this.btnAddOpenBlock);
            this.tabAvailability.Controls.Add(this.comboBox6);
            this.tabAvailability.Controls.Add(this.comboBox5);
            this.tabAvailability.Controls.Add(this.comboBox4);
            this.tabAvailability.Controls.Add(this.comboBox3);
            this.tabAvailability.Controls.Add(this.comboBox2);
            this.tabAvailability.Controls.Add(this.comboBox1);
            this.tabAvailability.Location = new System.Drawing.Point(4, 22);
            this.tabAvailability.Name = "tabAvailability";
            this.tabAvailability.Padding = new System.Windows.Forms.Padding(3);
            this.tabAvailability.Size = new System.Drawing.Size(465, 306);
            this.tabAvailability.TabIndex = 0;
            this.tabAvailability.Text = "Availability";
            // 
            // tabAppointments
            // 
            this.tabAppointments.BackColor = System.Drawing.Color.Transparent;
            this.tabAppointments.Controls.Add(this.tabControl2);
            this.tabAppointments.Location = new System.Drawing.Point(4, 22);
            this.tabAppointments.Name = "tabAppointments";
            this.tabAppointments.Padding = new System.Windows.Forms.Padding(3);
            this.tabAppointments.Size = new System.Drawing.Size(465, 306);
            this.tabAppointments.TabIndex = 1;
            this.tabAppointments.Text = "Appointments";
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabAccepted);
            this.tabControl2.Controls.Add(this.tabPendingTutor);
            this.tabControl2.Controls.Add(this.tabPendingTutee);
            this.tabControl2.Location = new System.Drawing.Point(7, 19);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(452, 281);
            this.tabControl2.TabIndex = 0;
            // 
            // tabAccepted
            // 
            this.tabAccepted.Controls.Add(this.lbxAccepted);
            this.tabAccepted.Location = new System.Drawing.Point(4, 22);
            this.tabAccepted.Name = "tabAccepted";
            this.tabAccepted.Padding = new System.Windows.Forms.Padding(3);
            this.tabAccepted.Size = new System.Drawing.Size(444, 255);
            this.tabAccepted.TabIndex = 0;
            this.tabAccepted.Text = "Accepted";
            this.tabAccepted.UseVisualStyleBackColor = true;
            // 
            // lbxAccepted
            // 
            this.lbxAccepted.FormattingEnabled = true;
            this.lbxAccepted.Location = new System.Drawing.Point(6, 5);
            this.lbxAccepted.Name = "lbxAccepted";
            this.lbxAccepted.Size = new System.Drawing.Size(435, 238);
            this.lbxAccepted.TabIndex = 0;
            // 
            // tabPendingTutor
            // 
            this.tabPendingTutor.Controls.Add(this.cbxPendingTutor);
            this.tabPendingTutor.Location = new System.Drawing.Point(4, 22);
            this.tabPendingTutor.Name = "tabPendingTutor";
            this.tabPendingTutor.Padding = new System.Windows.Forms.Padding(3);
            this.tabPendingTutor.Size = new System.Drawing.Size(444, 255);
            this.tabPendingTutor.TabIndex = 1;
            this.tabPendingTutor.Text = "Pending Tutor";
            this.tabPendingTutor.UseVisualStyleBackColor = true;
            // 
            // cbxPendingTutor
            // 
            this.cbxPendingTutor.FormattingEnabled = true;
            this.cbxPendingTutor.Location = new System.Drawing.Point(3, 2);
            this.cbxPendingTutor.Name = "cbxPendingTutor";
            this.cbxPendingTutor.Size = new System.Drawing.Size(435, 244);
            this.cbxPendingTutor.TabIndex = 0;
            // 
            // tabPendingTutee
            // 
            this.tabPendingTutee.Controls.Add(this.cbxPendingTutee);
            this.tabPendingTutee.Location = new System.Drawing.Point(4, 22);
            this.tabPendingTutee.Name = "tabPendingTutee";
            this.tabPendingTutee.Size = new System.Drawing.Size(444, 255);
            this.tabPendingTutee.TabIndex = 2;
            this.tabPendingTutee.Text = "Pending Tutee";
            this.tabPendingTutee.UseVisualStyleBackColor = true;
            // 
            // cbxPendingTutee
            // 
            this.cbxPendingTutee.FormattingEnabled = true;
            this.cbxPendingTutee.Location = new System.Drawing.Point(3, 3);
            this.cbxPendingTutee.Name = "cbxPendingTutee";
            this.cbxPendingTutee.Size = new System.Drawing.Size(438, 244);
            this.cbxPendingTutee.TabIndex = 0;
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(493, 374);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(97, 23);
            this.btnLogout.TabIndex = 1;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(493, 331);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Make Request";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
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
            this.comboBox1.Location = new System.Drawing.Point(229, 51);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(44, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "00",
            "15",
            "30",
            "45"});
            this.comboBox2.Location = new System.Drawing.Point(279, 51);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(49, 21);
            this.comboBox2.TabIndex = 1;
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "AM",
            "PM"});
            this.comboBox3.Location = new System.Drawing.Point(334, 50);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(50, 21);
            this.comboBox3.TabIndex = 2;
            // 
            // comboBox4
            // 
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Items.AddRange(new object[] {
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
            this.comboBox4.Location = new System.Drawing.Point(229, 77);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(44, 21);
            this.comboBox4.TabIndex = 3;
            // 
            // comboBox5
            // 
            this.comboBox5.FormattingEnabled = true;
            this.comboBox5.Items.AddRange(new object[] {
            "00",
            "15",
            "30",
            "45"});
            this.comboBox5.Location = new System.Drawing.Point(279, 77);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.Size = new System.Drawing.Size(49, 21);
            this.comboBox5.TabIndex = 4;
            // 
            // comboBox6
            // 
            this.comboBox6.FormattingEnabled = true;
            this.comboBox6.Items.AddRange(new object[] {
            "AM",
            "PM"});
            this.comboBox6.Location = new System.Drawing.Point(334, 77);
            this.comboBox6.Name = "comboBox6";
            this.comboBox6.Size = new System.Drawing.Size(50, 21);
            this.comboBox6.TabIndex = 5;
            // 
            // btnAddOpenBlock
            // 
            this.btnAddOpenBlock.Location = new System.Drawing.Point(279, 131);
            this.btnAddOpenBlock.Name = "btnAddOpenBlock";
            this.btnAddOpenBlock.Size = new System.Drawing.Size(105, 23);
            this.btnAddOpenBlock.TabIndex = 6;
            this.btnAddOpenBlock.Text = "Add Availability";
            this.btnAddOpenBlock.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(391, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Start Time";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(391, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "End Time";
            // 
            // StudentMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 427);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.tabControl1);
            this.Name = "StudentMain";
            this.Text = "StudentMain";
            this.tabControl1.ResumeLayout(false);
            this.tabAvailability.ResumeLayout(false);
            this.tabAvailability.PerformLayout();
            this.tabAppointments.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabAccepted.ResumeLayout(false);
            this.tabPendingTutor.ResumeLayout(false);
            this.tabPendingTutee.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabAvailability;
        private System.Windows.Forms.TabPage tabAppointments;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabAccepted;
        private System.Windows.Forms.ListBox lbxAccepted;
        private System.Windows.Forms.TabPage tabPendingTutor;
        private System.Windows.Forms.CheckedListBox cbxPendingTutor;
        private System.Windows.Forms.TabPage tabPendingTutee;
        private System.Windows.Forms.CheckedListBox cbxPendingTutee;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnAddOpenBlock;
        private System.Windows.Forms.ComboBox comboBox6;
        private System.Windows.Forms.ComboBox comboBox5;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}