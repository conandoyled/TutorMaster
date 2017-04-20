namespace TutorMaster
{
    partial class CreateStudent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateStudent));
            this.txtFirstname = new System.Windows.Forms.TextBox();
            this.txtLastname = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.cbxTutor = new System.Windows.Forms.CheckBox();
            this.cbxTutee = new System.Windows.Forms.CheckBox();
            this.txtPhoneNumber = new System.Windows.Forms.MaskedTextBox();
            this.tvClasses = new System.Windows.Forms.TreeView();
            this.lblTClasses = new System.Windows.Forms.Label();
            this.imgLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFirstname
            // 
            this.txtFirstname.Location = new System.Drawing.Point(32, 45);
            this.txtFirstname.Name = "txtFirstname";
            this.txtFirstname.Size = new System.Drawing.Size(100, 20);
            this.txtFirstname.TabIndex = 0;
            // 
            // txtLastname
            // 
            this.txtLastname.Location = new System.Drawing.Point(32, 84);
            this.txtLastname.Name = "txtLastname";
            this.txtLastname.Size = new System.Drawing.Size(100, 20);
            this.txtLastname.TabIndex = 1;
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(32, 127);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(100, 20);
            this.txtUsername.TabIndex = 2;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(32, 169);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(100, 20);
            this.txtPassword.TabIndex = 3;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(32, 249);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(100, 20);
            this.txtEmail.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(139, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "First Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(139, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Last Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(138, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Username";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(138, 176);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Password";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(138, 218);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Phone Number";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(139, 256);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Email";
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(222)))), ((int)(((byte)(229)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.Location = new System.Drawing.Point(70, 325);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(112, 23);
            this.btnAdd.TabIndex = 13;
            this.btnAdd.Text = "Add Student";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(222)))), ((int)(((byte)(229)))));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(70, 354);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(113, 23);
            this.btnExit.TabIndex = 14;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(32, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Create New Student!";
            // 
            // cbxTutor
            // 
            this.cbxTutor.AutoSize = true;
            this.cbxTutor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbxTutor.Location = new System.Drawing.Point(35, 276);
            this.cbxTutor.Name = "cbxTutor";
            this.cbxTutor.Size = new System.Drawing.Size(48, 17);
            this.cbxTutor.TabIndex = 16;
            this.cbxTutor.Text = "Tutor";
            this.cbxTutor.UseVisualStyleBackColor = true;
            this.cbxTutor.CheckStateChanged += new System.EventHandler(this.cbxTutor_CheckStateChanged);
            // 
            // cbxTutee
            // 
            this.cbxTutee.AutoSize = true;
            this.cbxTutee.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbxTutee.Location = new System.Drawing.Point(35, 300);
            this.cbxTutee.Name = "cbxTutee";
            this.cbxTutee.Size = new System.Drawing.Size(51, 17);
            this.cbxTutee.TabIndex = 17;
            this.cbxTutee.Text = "Tutee";
            this.cbxTutee.UseVisualStyleBackColor = true;
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Location = new System.Drawing.Point(32, 211);
            this.txtPhoneNumber.Mask = "(999) 000-0000";
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Size = new System.Drawing.Size(100, 20);
            this.txtPhoneNumber.TabIndex = 4;
            // 
            // tvClasses
            // 
            this.tvClasses.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.tvClasses.Location = new System.Drawing.Point(250, 44);
            this.tvClasses.Name = "tvClasses";
            this.tvClasses.Size = new System.Drawing.Size(205, 304);
            this.tvClasses.TabIndex = 19;
            this.tvClasses.Visible = false;
            this.tvClasses.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvClasses_AfterCheck);
            // 
            // lblTClasses
            // 
            this.lblTClasses.AutoSize = true;
            this.lblTClasses.Location = new System.Drawing.Point(250, 25);
            this.lblTClasses.Name = "lblTClasses";
            this.lblTClasses.Size = new System.Drawing.Size(74, 13);
            this.lblTClasses.TabIndex = 20;
            this.lblTClasses.Text = "Tutor Classes:";
            this.lblTClasses.Visible = false;
            // 
            // imgLogo
            // 
            this.imgLogo.Image = global::TutorMaster.Properties.Resources.WatermarkR2;
            this.imgLogo.Location = new System.Drawing.Point(176, 344);
            this.imgLogo.Name = "imgLogo";
            this.imgLogo.Size = new System.Drawing.Size(69, 66);
            this.imgLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgLogo.TabIndex = 35;
            this.imgLogo.TabStop = false;
            // 
            // CreateStudent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(232)))), ((int)(((byte)(239)))));
            this.ClientSize = new System.Drawing.Size(243, 407);
            this.Controls.Add(this.lblTClasses);
            this.Controls.Add(this.tvClasses);
            this.Controls.Add(this.txtPhoneNumber);
            this.Controls.Add(this.cbxTutee);
            this.Controls.Add(this.cbxTutor);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.txtLastname);
            this.Controls.Add(this.txtFirstname);
            this.Controls.Add(this.imgLogo);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(71)))), ((int)(((byte)(76)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CreateStudent";
            this.Text = "CreateStudent";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CreateStudent_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.imgLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFirstname;
        private System.Windows.Forms.TextBox txtLastname;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox cbxTutor;
        private System.Windows.Forms.CheckBox cbxTutee;
        private System.Windows.Forms.MaskedTextBox txtPhoneNumber;
        private System.Windows.Forms.TreeView tvClasses;
        private System.Windows.Forms.Label lblTClasses;
        private System.Windows.Forms.PictureBox imgLogo;
    }
}