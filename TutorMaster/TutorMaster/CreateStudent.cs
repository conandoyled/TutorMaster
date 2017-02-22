using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Entity;


namespace TutorMaster
{
    public partial class CreateStudent : Form
    {
        public CreateStudent()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            AdminMain g = new AdminMain();
            g.Show();
            this.Close();
        }

        private void CreateStudent_FormClosed(object sender, FormClosedEventArgs e)
        {
            //System.Windows.Forms.Application.Exit();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string fname = txtFirstname.Text;
            string lname = txtLastname.Text;
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string phone = txtPassword.Text;
            string email = txtEmail.Text;
            string accounttype = "NULL";
            bool tutor = cbxTutorTutee.GetItemChecked(0);
            bool tutee = cbxTutorTutee.GetItemChecked(1);

            if (!tutor && !tutee)
            {
                MessageBox.Show("Please select at least one of tutor and/or tutee");
            }
            else
            {
                User newStudent = new User(4, fname, lname, email, phone, accounttype, password, username);
                addUser(newStudent);

                if (tutor && tutee)
                {
                    accounttype = "Tutor/Tutee";
                }

                else if (tutee)
                {
                    accounttype = "Tutee";
                }
                else
                {
                    accounttype = "Tutor";
                }
            }
        }

        private void addUser(User student)
        {
            TutorMasterDBEntities1 db = new TutorMasterDBEntities1();
            db.Users.
        }
    }
}
