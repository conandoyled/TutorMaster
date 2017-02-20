using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
            bool tutor = cbxTutorTutee.GetItemChecked(0);
            bool tutee = cbxTutorTutee.GetItemChecked(1);
            addStudent(fname, lname, username, password, phone, email, tutor, tutee);
        }

        private void addStudent(string fname, string lname, string username, string password, string phone, string email, bool tutor, bool tutee)
        {
            MessageBox.Show(tutor.ToString() + tutee.ToString());
        }
    }
}
