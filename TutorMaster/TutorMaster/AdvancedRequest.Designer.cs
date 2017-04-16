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
            this.combTutorName = new System.Windows.Forms.ComboBox();
            this.tvClasses = new System.Windows.Forms.TreeView();
            this.lvTutorAvailability = new System.Windows.Forms.ListView();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblTutorName = new System.Windows.Forms.Label();
            this.lblClassesAvailable = new System.Windows.Forms.Label();
            this.lblAvailableTimes = new System.Windows.Forms.Label();
            this.lblChoice = new System.Windows.Forms.Label();
            this.btnByTutor = new System.Windows.Forms.Button();
            this.btnByClass = new System.Windows.Forms.Button();
            this.lblClasses = new System.Windows.Forms.Label();
            this.combClassBox = new System.Windows.Forms.ComboBox();
            this.combTutorName2 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lvTutorAvailability2 = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // combTutorName
            // 
            this.combTutorName.FormattingEnabled = true;
            this.combTutorName.Location = new System.Drawing.Point(24, 116);
            this.combTutorName.Name = "combTutorName";
            this.combTutorName.Size = new System.Drawing.Size(93, 21);
            this.combTutorName.Sorted = true;
            this.combTutorName.TabIndex = 3;
            this.combTutorName.SelectedIndexChanged += new System.EventHandler(this.combTutorName_SelectedIndexChanged);
            // 
            // tvClasses
            // 
            this.tvClasses.Location = new System.Drawing.Point(24, 179);
            this.tvClasses.Name = "tvClasses";
            this.tvClasses.Size = new System.Drawing.Size(161, 125);
            this.tvClasses.TabIndex = 4;
            // 
            // lvTutorAvailability
            // 
            this.lvTutorAvailability.Location = new System.Drawing.Point(24, 347);
            this.lvTutorAvailability.Name = "lvTutorAvailability";
            this.lvTutorAvailability.Size = new System.Drawing.Size(203, 125);
            this.lvTutorAvailability.TabIndex = 5;
            this.lvTutorAvailability.UseCompatibleStateImageBehavior = false;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(664, 12);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(101, 23);
            this.btnExit.TabIndex = 15;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblTutorName
            // 
            this.lblTutorName.AutoSize = true;
            this.lblTutorName.Location = new System.Drawing.Point(21, 87);
            this.lblTutorName.Name = "lblTutorName";
            this.lblTutorName.Size = new System.Drawing.Size(63, 13);
            this.lblTutorName.TabIndex = 16;
            this.lblTutorName.Text = "Tutor Name";
            // 
            // lblClassesAvailable
            // 
            this.lblClassesAvailable.AutoSize = true;
            this.lblClassesAvailable.Location = new System.Drawing.Point(21, 154);
            this.lblClassesAvailable.Name = "lblClassesAvailable";
            this.lblClassesAvailable.Size = new System.Drawing.Size(89, 13);
            this.lblClassesAvailable.TabIndex = 17;
            this.lblClassesAvailable.Text = "Classes Available";
            // 
            // lblAvailableTimes
            // 
            this.lblAvailableTimes.AutoSize = true;
            this.lblAvailableTimes.Location = new System.Drawing.Point(21, 322);
            this.lblAvailableTimes.Name = "lblAvailableTimes";
            this.lblAvailableTimes.Size = new System.Drawing.Size(81, 13);
            this.lblAvailableTimes.TabIndex = 18;
            this.lblAvailableTimes.Text = "Available Times";
            // 
            // lblChoice
            // 
            this.lblChoice.AutoSize = true;
            this.lblChoice.Location = new System.Drawing.Point(264, 22);
            this.lblChoice.Name = "lblChoice";
            this.lblChoice.Size = new System.Drawing.Size(138, 13);
            this.lblChoice.TabIndex = 19;
            this.lblChoice.Text = "Search by tutor or by class?";
            // 
            // btnByTutor
            // 
            this.btnByTutor.Location = new System.Drawing.Point(228, 38);
            this.btnByTutor.Name = "btnByTutor";
            this.btnByTutor.Size = new System.Drawing.Size(75, 23);
            this.btnByTutor.TabIndex = 20;
            this.btnByTutor.Text = "Tutor";
            this.btnByTutor.UseVisualStyleBackColor = true;
            this.btnByTutor.Click += new System.EventHandler(this.btnByTutor_Click);
            // 
            // btnByClass
            // 
            this.btnByClass.Location = new System.Drawing.Point(377, 38);
            this.btnByClass.Name = "btnByClass";
            this.btnByClass.Size = new System.Drawing.Size(75, 23);
            this.btnByClass.TabIndex = 21;
            this.btnByClass.Text = "Class";
            this.btnByClass.UseVisualStyleBackColor = true;
            this.btnByClass.Click += new System.EventHandler(this.btnByClass_Click);
            // 
            // lblClasses
            // 
            this.lblClasses.AutoSize = true;
            this.lblClasses.Location = new System.Drawing.Point(445, 106);
            this.lblClasses.Name = "lblClasses";
            this.lblClasses.Size = new System.Drawing.Size(43, 13);
            this.lblClasses.TabIndex = 22;
            this.lblClasses.Text = "Classes";
            // 
            // combClassBox
            // 
            this.combClassBox.FormattingEnabled = true;
            this.combClassBox.Location = new System.Drawing.Point(448, 131);
            this.combClassBox.Name = "combClassBox";
            this.combClassBox.Size = new System.Drawing.Size(121, 21);
            this.combClassBox.TabIndex = 23;
            this.combClassBox.SelectedIndexChanged += new System.EventHandler(this.combClassBox_SelectedIndexChanged);
            // 
            // combTutorName2
            // 
            this.combTutorName2.FormattingEnabled = true;
            this.combTutorName2.Location = new System.Drawing.Point(448, 224);
            this.combTutorName2.Name = "combTutorName2";
            this.combTutorName2.Size = new System.Drawing.Size(121, 21);
            this.combTutorName2.TabIndex = 24;
            this.combTutorName2.SelectedIndexChanged += new System.EventHandler(this.combTutorName2_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(445, 197);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Available Tutors";
            // 
            // lvTutorAvailability2
            // 
            this.lvTutorAvailability2.Location = new System.Drawing.Point(448, 347);
            this.lvTutorAvailability2.Name = "lvTutorAvailability2";
            this.lvTutorAvailability2.Size = new System.Drawing.Size(203, 125);
            this.lvTutorAvailability2.TabIndex = 26;
            this.lvTutorAvailability2.UseCompatibleStateImageBehavior = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(445, 322);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Available Times";
            // 
            // AdvancedRequest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 542);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lvTutorAvailability2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.combTutorName2);
            this.Controls.Add(this.combClassBox);
            this.Controls.Add(this.lblClasses);
            this.Controls.Add(this.btnByClass);
            this.Controls.Add(this.btnByTutor);
            this.Controls.Add(this.lblChoice);
            this.Controls.Add(this.lblAvailableTimes);
            this.Controls.Add(this.lblClassesAvailable);
            this.Controls.Add(this.lblTutorName);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lvTutorAvailability);
            this.Controls.Add(this.tvClasses);
            this.Controls.Add(this.combTutorName);
            this.Name = "AdvancedRequest";
            this.Text = "AdvancedRequest";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox combTutorName;
        private System.Windows.Forms.TreeView tvClasses;
        private System.Windows.Forms.ListView lvTutorAvailability;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblTutorName;
        private System.Windows.Forms.Label lblClassesAvailable;
        private System.Windows.Forms.Label lblAvailableTimes;
        private System.Windows.Forms.Label lblChoice;
        private System.Windows.Forms.Button btnByTutor;
        private System.Windows.Forms.Button btnByClass;
        private System.Windows.Forms.Label lblClasses;
        private System.Windows.Forms.ComboBox combClassBox;
        private System.Windows.Forms.ComboBox combTutorName2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView lvTutorAvailability2;
        private System.Windows.Forms.Label label2;
    }
}