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
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (isValidUser(username, password))
            {
                string accType = getAccType(username);
                int accID = getID(username);
                lblErrMsg.Text = accType + accID.ToString();
            }
            else
            {
                lblErrMsg.Text = "Invalid Username or Password. Try again.";
            }
        }

        private bool isValidUser(string username, string password)
        {
            TutorMasterDBEntities1 db = new TutorMasterDBEntities1();

            return (db.Users.Any(u => u.Username == username && u.Password == password));
        }

        private string getAccType(string username)
        {
            TutorMasterDBEntities1 db = new TutorMasterDBEntities1();

            string accType = (from row in db.Users where row.Username == username select row.AccountType).Single();

            return (accType);
        }

        private int getID(string username)
        {
            TutorMasterDBEntities1 db = new TutorMasterDBEntities1();

            int accID = (from row in db.Users where row.Username == username select row.ID).First();

            return accID;
        }



        private void clearErrMsg(object sender, EventArgs e)
        {
            lblErrMsg.Text = "";
        }



    }
}
