namespace TutorMaster
{
    partial class AdminMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminMain));
            this.tabAdmin = new System.Windows.Forms.TabControl();
            this.tabStudent = new System.Windows.Forms.TabPage();
            this.btnStudentSchedule = new System.Windows.Forms.Button();
            this.lvStudent = new System.Windows.Forms.ListView();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCreateStudent = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.tabFaculty = new System.Windows.Forms.TabPage();
            this.txtPhoneNumber = new System.Windows.Forms.MaskedTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.btnFacultyDelete = new System.Windows.Forms.Button();
            this.btnFacultyAdd = new System.Windows.Forms.Button();
            this.btnFacultyEdit = new System.Windows.Forms.Button();
            this.combDepartments = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtLastname = new System.Windows.Forms.TextBox();
            this.txtFirstname = new System.Windows.Forms.TextBox();
            this.lvFaculty = new System.Windows.Forms.ListView();
            this.tabClasses = new System.Windows.Forms.TabPage();
            this.btnClassDelete = new System.Windows.Forms.Button();
            this.btnClassAdd = new System.Windows.Forms.Button();
            this.btnClassEdit = new System.Windows.Forms.Button();
            this.combDepartmentsAdd = new System.Windows.Forms.ComboBox();
            this.lblDepartment = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDepartment = new System.Windows.Forms.TextBox();
            this.txtClassName = new System.Windows.Forms.TextBox();
            this.txtClassCode = new System.Windows.Forms.TextBox();
            this.lvClass = new System.Windows.Forms.ListView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnRejectRequests = new System.Windows.Forms.Button();
            this.btnEditRequests = new System.Windows.Forms.Button();
            this.btnAcceptRequest = new System.Windows.Forms.Button();
            this.lvRequests = new System.Windows.Forms.ListView();
            this.btnLogout = new System.Windows.Forms.Button();
            this.lblID = new System.Windows.Forms.Label();
            this.tabAdmin.SuspendLayout();
            this.tabStudent.SuspendLayout();
            this.tabFaculty.SuspendLayout();
            this.tabClasses.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabAdmin
            // 
            this.tabAdmin.Controls.Add(this.tabStudent);
            this.tabAdmin.Controls.Add(this.tabFaculty);
            this.tabAdmin.Controls.Add(this.tabClasses);
            this.tabAdmin.Controls.Add(this.tabPage1);
            this.tabAdmin.Location = new System.Drawing.Point(12, 39);
            this.tabAdmin.Name = "tabAdmin";
            this.tabAdmin.SelectedIndex = 0;
            this.tabAdmin.Size = new System.Drawing.Size(625, 398);
            this.tabAdmin.TabIndex = 0;
            this.tabAdmin.SelectedIndexChanged += new System.EventHandler(this.tabAdmin_SelectedIndexChanged);
            // 
            // tabStudent
            // 
            this.tabStudent.Controls.Add(this.btnStudentSchedule);
            this.tabStudent.Controls.Add(this.lvStudent);
            this.tabStudent.Controls.Add(this.btnDelete);
            this.tabStudent.Controls.Add(this.btnCreateStudent);
            this.tabStudent.Controls.Add(this.btnEdit);
            this.tabStudent.Location = new System.Drawing.Point(4, 22);
            this.tabStudent.Name = "tabStudent";
            this.tabStudent.Padding = new System.Windows.Forms.Padding(3);
            this.tabStudent.Size = new System.Drawing.Size(617, 372);
            this.tabStudent.TabIndex = 0;
            this.tabStudent.Text = "Students";
            this.tabStudent.UseVisualStyleBackColor = true;
            // 
            // btnStudentSchedule
            // 
            this.btnStudentSchedule.Location = new System.Drawing.Point(415, 343);
            this.btnStudentSchedule.Name = "btnStudentSchedule";
            this.btnStudentSchedule.Size = new System.Drawing.Size(140, 23);
            this.btnStudentSchedule.TabIndex = 5;
            this.btnStudentSchedule.Text = "See and Edit Student Schedule";
            this.btnStudentSchedule.UseVisualStyleBackColor = true;
            this.btnStudentSchedule.Click += new System.EventHandler(this.btnStudentSchedule_Click);
            // 
            // lvStudent
            // 
            this.lvStudent.Location = new System.Drawing.Point(0, 0);
            this.lvStudent.Name = "lvStudent";
            this.lvStudent.Size = new System.Drawing.Size(617, 337);
            this.lvStudent.TabIndex = 0;
            this.lvStudent.UseCompatibleStateImageBehavior = false;
            this.lvStudent.View = System.Windows.Forms.View.Details;
            this.lvStudent.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvStudent_ItemChecked);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(278, 343);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(130, 23);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete Student";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCreateStudent
            // 
            this.btnCreateStudent.Location = new System.Drawing.Point(6, 343);
            this.btnCreateStudent.Name = "btnCreateStudent";
            this.btnCreateStudent.Size = new System.Drawing.Size(130, 23);
            this.btnCreateStudent.TabIndex = 2;
            this.btnCreateStudent.Text = "Create New Student";
            this.btnCreateStudent.UseVisualStyleBackColor = true;
            this.btnCreateStudent.Click += new System.EventHandler(this.btnCreateStudent_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(142, 343);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(130, 23);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Text = "Edit Student Account";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // tabFaculty
            // 
            this.tabFaculty.Controls.Add(this.txtPhoneNumber);
            this.tabFaculty.Controls.Add(this.label6);
            this.tabFaculty.Controls.Add(this.label5);
            this.tabFaculty.Controls.Add(this.txtEmail);
            this.tabFaculty.Controls.Add(this.btnFacultyDelete);
            this.tabFaculty.Controls.Add(this.btnFacultyAdd);
            this.tabFaculty.Controls.Add(this.btnFacultyEdit);
            this.tabFaculty.Controls.Add(this.combDepartments);
            this.tabFaculty.Controls.Add(this.label4);
            this.tabFaculty.Controls.Add(this.label3);
            this.tabFaculty.Controls.Add(this.label2);
            this.tabFaculty.Controls.Add(this.label1);
            this.tabFaculty.Controls.Add(this.txtPassword);
            this.tabFaculty.Controls.Add(this.txtUsername);
            this.tabFaculty.Controls.Add(this.txtLastname);
            this.tabFaculty.Controls.Add(this.txtFirstname);
            this.tabFaculty.Controls.Add(this.lvFaculty);
            this.tabFaculty.Location = new System.Drawing.Point(4, 22);
            this.tabFaculty.Name = "tabFaculty";
            this.tabFaculty.Padding = new System.Windows.Forms.Padding(3);
            this.tabFaculty.Size = new System.Drawing.Size(617, 372);
            this.tabFaculty.TabIndex = 2;
            this.tabFaculty.Text = "Faculty";
            this.tabFaculty.UseVisualStyleBackColor = true;
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Location = new System.Drawing.Point(431, 156);
            this.txtPhoneNumber.Mask = "(999) 000-0000";
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Size = new System.Drawing.Size(100, 20);
            this.txtPhoneNumber.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(538, 197);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "Email";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(537, 163);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Phone Number";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(431, 190);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(100, 20);
            this.txtEmail.TabIndex = 5;
            // 
            // btnFacultyDelete
            // 
            this.btnFacultyDelete.Location = new System.Drawing.Point(446, 333);
            this.btnFacultyDelete.Name = "btnFacultyDelete";
            this.btnFacultyDelete.Size = new System.Drawing.Size(130, 23);
            this.btnFacultyDelete.TabIndex = 21;
            this.btnFacultyDelete.Text = "Delete Faculty Account";
            this.btnFacultyDelete.UseVisualStyleBackColor = true;
            this.btnFacultyDelete.Click += new System.EventHandler(this.btnFacultyDelete_Click);
            // 
            // btnFacultyAdd
            // 
            this.btnFacultyAdd.Location = new System.Drawing.Point(446, 256);
            this.btnFacultyAdd.Name = "btnFacultyAdd";
            this.btnFacultyAdd.Size = new System.Drawing.Size(130, 38);
            this.btnFacultyAdd.TabIndex = 19;
            this.btnFacultyAdd.Text = "Create New Faculty Account";
            this.btnFacultyAdd.UseVisualStyleBackColor = true;
            this.btnFacultyAdd.Click += new System.EventHandler(this.btnFacultyAdd_Click);
            // 
            // btnFacultyEdit
            // 
            this.btnFacultyEdit.Location = new System.Drawing.Point(446, 302);
            this.btnFacultyEdit.Name = "btnFacultyEdit";
            this.btnFacultyEdit.Size = new System.Drawing.Size(130, 23);
            this.btnFacultyEdit.TabIndex = 20;
            this.btnFacultyEdit.Text = "Edit Faculty Account";
            this.btnFacultyEdit.UseVisualStyleBackColor = true;
            this.btnFacultyEdit.Click += new System.EventHandler(this.btnFacultyEdit_Click);
            // 
            // combDepartments
            // 
            this.combDepartments.FormattingEnabled = true;
            this.combDepartments.Location = new System.Drawing.Point(431, 221);
            this.combDepartments.Name = "combDepartments";
            this.combDepartments.Size = new System.Drawing.Size(159, 21);
            this.combDepartments.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(537, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(537, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(538, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Last Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(538, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "First Name";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(431, 124);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(100, 20);
            this.txtPassword.TabIndex = 3;
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(431, 92);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(100, 20);
            this.txtUsername.TabIndex = 2;
            // 
            // txtLastname
            // 
            this.txtLastname.Location = new System.Drawing.Point(431, 59);
            this.txtLastname.Name = "txtLastname";
            this.txtLastname.Size = new System.Drawing.Size(100, 20);
            this.txtLastname.TabIndex = 1;
            // 
            // txtFirstname
            // 
            this.txtFirstname.Location = new System.Drawing.Point(431, 26);
            this.txtFirstname.Name = "txtFirstname";
            this.txtFirstname.Size = new System.Drawing.Size(100, 20);
            this.txtFirstname.TabIndex = 0;
            // 
            // lvFaculty
            // 
            this.lvFaculty.Location = new System.Drawing.Point(0, 0);
            this.lvFaculty.Name = "lvFaculty";
            this.lvFaculty.Size = new System.Drawing.Size(410, 372);
            this.lvFaculty.TabIndex = 100;
            this.lvFaculty.UseCompatibleStateImageBehavior = false;
            this.lvFaculty.View = System.Windows.Forms.View.Details;
            this.lvFaculty.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvFaculty_ItemChecked);
            // 
            // tabClasses
            // 
            this.tabClasses.Controls.Add(this.btnClassDelete);
            this.tabClasses.Controls.Add(this.btnClassAdd);
            this.tabClasses.Controls.Add(this.btnClassEdit);
            this.tabClasses.Controls.Add(this.combDepartmentsAdd);
            this.tabClasses.Controls.Add(this.lblDepartment);
            this.tabClasses.Controls.Add(this.label7);
            this.tabClasses.Controls.Add(this.label8);
            this.tabClasses.Controls.Add(this.txtDepartment);
            this.tabClasses.Controls.Add(this.txtClassName);
            this.tabClasses.Controls.Add(this.txtClassCode);
            this.tabClasses.Controls.Add(this.lvClass);
            this.tabClasses.Location = new System.Drawing.Point(4, 22);
            this.tabClasses.Name = "tabClasses";
            this.tabClasses.Padding = new System.Windows.Forms.Padding(3);
            this.tabClasses.Size = new System.Drawing.Size(617, 372);
            this.tabClasses.TabIndex = 3;
            this.tabClasses.Text = "Classes";
            this.tabClasses.UseVisualStyleBackColor = true;
            // 
            // btnClassDelete
            // 
            this.btnClassDelete.Location = new System.Drawing.Point(434, 297);
            this.btnClassDelete.Name = "btnClassDelete";
            this.btnClassDelete.Size = new System.Drawing.Size(130, 23);
            this.btnClassDelete.TabIndex = 29;
            this.btnClassDelete.Text = "Delete Class(es)";
            this.btnClassDelete.UseVisualStyleBackColor = true;
            this.btnClassDelete.Click += new System.EventHandler(this.btnClassDelete_Click);
            // 
            // btnClassAdd
            // 
            this.btnClassAdd.Location = new System.Drawing.Point(434, 237);
            this.btnClassAdd.Name = "btnClassAdd";
            this.btnClassAdd.Size = new System.Drawing.Size(130, 23);
            this.btnClassAdd.TabIndex = 27;
            this.btnClassAdd.Text = "Create New Class";
            this.btnClassAdd.UseVisualStyleBackColor = true;
            this.btnClassAdd.Click += new System.EventHandler(this.btnClassAdd_Click);
            // 
            // btnClassEdit
            // 
            this.btnClassEdit.Location = new System.Drawing.Point(434, 266);
            this.btnClassEdit.Name = "btnClassEdit";
            this.btnClassEdit.Size = new System.Drawing.Size(130, 23);
            this.btnClassEdit.TabIndex = 28;
            this.btnClassEdit.Text = "Edit Class";
            this.btnClassEdit.UseVisualStyleBackColor = true;
            this.btnClassEdit.Click += new System.EventHandler(this.btnClassEdit_Click);
            // 
            // combDepartmentsAdd
            // 
            this.combDepartmentsAdd.FormattingEnabled = true;
            this.combDepartmentsAdd.Location = new System.Drawing.Point(416, 107);
            this.combDepartmentsAdd.Name = "combDepartmentsAdd";
            this.combDepartmentsAdd.Size = new System.Drawing.Size(159, 21);
            this.combDepartmentsAdd.TabIndex = 9;
            this.combDepartmentsAdd.DropDownClosed += new System.EventHandler(this.combDepartmentsAdd_DropDownClosed);
            // 
            // lblDepartment
            // 
            this.lblDepartment.AutoSize = true;
            this.lblDepartment.Location = new System.Drawing.Point(522, 157);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(62, 13);
            this.lblDepartment.TabIndex = 25;
            this.lblDepartment.Text = "Department";
            this.lblDepartment.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(523, 72);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "Class Name";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(523, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "Class Code";
            // 
            // txtDepartment
            // 
            this.txtDepartment.Location = new System.Drawing.Point(416, 150);
            this.txtDepartment.Name = "txtDepartment";
            this.txtDepartment.Size = new System.Drawing.Size(100, 20);
            this.txtDepartment.TabIndex = 10;
            this.txtDepartment.Visible = false;
            // 
            // txtClassName
            // 
            this.txtClassName.Location = new System.Drawing.Point(416, 65);
            this.txtClassName.Name = "txtClassName";
            this.txtClassName.Size = new System.Drawing.Size(100, 20);
            this.txtClassName.TabIndex = 8;
            // 
            // txtClassCode
            // 
            this.txtClassCode.Location = new System.Drawing.Point(416, 26);
            this.txtClassCode.Name = "txtClassCode";
            this.txtClassCode.Size = new System.Drawing.Size(100, 20);
            this.txtClassCode.TabIndex = 7;
            // 
            // lvClass
            // 
            this.lvClass.Location = new System.Drawing.Point(0, 0);
            this.lvClass.Name = "lvClass";
            this.lvClass.Size = new System.Drawing.Size(372, 372);
            this.lvClass.TabIndex = 1;
            this.lvClass.UseCompatibleStateImageBehavior = false;
            this.lvClass.View = System.Windows.Forms.View.Details;
            this.lvClass.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvClass_ItemChecked);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnRejectRequests);
            this.tabPage1.Controls.Add(this.btnEditRequests);
            this.tabPage1.Controls.Add(this.btnAcceptRequest);
            this.tabPage1.Controls.Add(this.lvRequests);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(617, 372);
            this.tabPage1.TabIndex = 4;
            this.tabPage1.Text = "Tutor/Tutee Requests";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnRejectRequests
            // 
            this.btnRejectRequests.Location = new System.Drawing.Point(291, 343);
            this.btnRejectRequests.Name = "btnRejectRequests";
            this.btnRejectRequests.Size = new System.Drawing.Size(116, 23);
            this.btnRejectRequests.TabIndex = 4;
            this.btnRejectRequests.Text = "Reject Requests";
            this.btnRejectRequests.UseVisualStyleBackColor = true;
            this.btnRejectRequests.Click += new System.EventHandler(this.btnRejectRequests_Click);
            // 
            // btnEditRequests
            // 
            this.btnEditRequests.Location = new System.Drawing.Point(147, 343);
            this.btnEditRequests.Name = "btnEditRequests";
            this.btnEditRequests.Size = new System.Drawing.Size(119, 23);
            this.btnEditRequests.TabIndex = 3;
            this.btnEditRequests.Text = "Edit Request";
            this.btnEditRequests.UseVisualStyleBackColor = true;
            this.btnEditRequests.Click += new System.EventHandler(this.btnEditRequests_Click);
            // 
            // btnAcceptRequest
            // 
            this.btnAcceptRequest.Location = new System.Drawing.Point(14, 343);
            this.btnAcceptRequest.Name = "btnAcceptRequest";
            this.btnAcceptRequest.Size = new System.Drawing.Size(113, 23);
            this.btnAcceptRequest.TabIndex = 2;
            this.btnAcceptRequest.Text = "Accept Requests";
            this.btnAcceptRequest.UseVisualStyleBackColor = true;
            this.btnAcceptRequest.Click += new System.EventHandler(this.btnAcceptRequest_Click);
            // 
            // lvRequests
            // 
            this.lvRequests.Location = new System.Drawing.Point(-4, 0);
            this.lvRequests.Name = "lvRequests";
            this.lvRequests.Size = new System.Drawing.Size(621, 337);
            this.lvRequests.TabIndex = 1;
            this.lvRequests.UseCompatibleStateImageBehavior = false;
            this.lvRequests.View = System.Windows.Forms.View.Details;
            this.lvRequests.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvRequests_ItemChecked);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(479, 10);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(144, 23);
            this.btnLogout.TabIndex = 5;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(12, 10);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(35, 13);
            this.lblID.TabIndex = 6;
            this.lblID.Text = "label9";
            this.lblID.Visible = false;
            // 
            // AdminMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 449);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.tabAdmin);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AdminMain";
            this.Text = "TutorMaster";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AdminMain_FormClosed);
            this.tabAdmin.ResumeLayout(false);
            this.tabStudent.ResumeLayout(false);
            this.tabFaculty.ResumeLayout(false);
            this.tabFaculty.PerformLayout();
            this.tabClasses.ResumeLayout(false);
            this.tabClasses.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabAdmin;
        private System.Windows.Forms.TabPage tabStudent;
        private System.Windows.Forms.Button btnCreateStudent;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.ListView lvStudent;
        private System.Windows.Forms.TabPage tabFaculty;
        private System.Windows.Forms.TabPage tabClasses;
        private System.Windows.Forms.ListView lvFaculty;
        private System.Windows.Forms.ListView lvClass;
        private System.Windows.Forms.Button btnFacultyDelete;
        private System.Windows.Forms.Button btnFacultyAdd;
        private System.Windows.Forms.Button btnFacultyEdit;
        private System.Windows.Forms.ComboBox combDepartments;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtLastname;
        private System.Windows.Forms.TextBox txtFirstname;
        private System.Windows.Forms.Label lblDepartment;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDepartment;
        private System.Windows.Forms.TextBox txtClassName;
        private System.Windows.Forms.TextBox txtClassCode;
        private System.Windows.Forms.Button btnClassDelete;
        private System.Windows.Forms.Button btnClassAdd;
        private System.Windows.Forms.Button btnClassEdit;
        private System.Windows.Forms.ComboBox combDepartmentsAdd;
        private System.Windows.Forms.MaskedTextBox txtPhoneNumber;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Button btnStudentSchedule;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnRejectRequests;
        private System.Windows.Forms.Button btnEditRequests;
        private System.Windows.Forms.Button btnAcceptRequest;
        private System.Windows.Forms.ListView lvRequests;
    }
}