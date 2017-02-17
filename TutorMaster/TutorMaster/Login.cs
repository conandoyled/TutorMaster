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
    public partial class Login : Form
    {

        public Login()
        {
            InitializeComponent();
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            string username = TextBoxUsername.Text;
            string password = TextBoxPassword.Text;

            if (isValidUser(username, password))
            {
                LabelTitle.Text = "True";
            }
            else
            {
                LabelTitle.Text = "False";
            }
        }

        private bool isValidUser(string username, string password)
        {
            TutorMasterDBEntities1 db = new TutorMasterDBEntities1();

            return (db.Users.Any(u => u.Username == username && u.Password == password));
        }



    }
}
