namespace TutorMaster
{
    partial class RemoveAvailForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RemoveAvailForm));
            this.lvTimeSlots = new System.Windows.Forms.ListView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnDeselectAll = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lvTimeSlots
            // 
            this.lvTimeSlots.Location = new System.Drawing.Point(27, 96);
            this.lvTimeSlots.Name = "lvTimeSlots";
            this.lvTimeSlots.Size = new System.Drawing.Size(384, 211);
            this.lvTimeSlots.TabIndex = 0;
            this.lvTimeSlots.UseCompatibleStateImageBehavior = false;
            this.lvTimeSlots.View = System.Windows.Forms.View.Details;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(27, 313);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(336, 313);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 2;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(117, 313);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAll.TabIndex = 3;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnDeselectAll
            // 
            this.btnDeselectAll.Location = new System.Drawing.Point(255, 313);
            this.btnDeselectAll.Name = "btnDeselectAll";
            this.btnDeselectAll.Size = new System.Drawing.Size(75, 23);
            this.btnDeselectAll.TabIndex = 4;
            this.btnDeselectAll.Text = "Deselect All";
            this.btnDeselectAll.UseVisualStyleBackColor = true;
            this.btnDeselectAll.Click += new System.EventHandler(this.btnDeselectAll_Click);
            // 
            // RemoveAvailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 370);
            this.Controls.Add(this.btnDeselectAll);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lvTimeSlots);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RemoveAvailForm";
            this.Text = "TutorMaster";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RemoveAvailForm_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvTimeSlots;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnDeselectAll;
    }
}