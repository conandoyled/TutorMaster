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
    public partial class ChangePasswordForm : Form
    {
        private int id;

        //constructor
        public ChangePasswordForm(int accID)
        {
            InitializeComponent();
            id = accID;
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();                                                                        //open the database
            User user = (from row in db.Users.AsEnumerable() where row.ID == id select row).First();                                         //get the user account
            string accountType = user.AccountType;                                                                                           //get the accountType of the user
            string oldPassword = user.Password;                                                                                              //get the password of the user
            string inputOld = txtOldPass.Text;                                                                                               //get the password they put in the old password textbox
            string inputNew = txtNewPass.Text;                                                                                               //get the new password they put in the new password textbox 
            string inputConfirm = txtConfirm.Text;                                                                                           //get the confirmation of the password they put in the confirmation textbox

            if (string.IsNullOrWhiteSpace(oldPassword) || string.IsNullOrWhiteSpace(inputNew) || string.IsNullOrWhiteSpace(inputConfirm))    //check if they put something in each text box
            {
                MessageBox.Show("Please enter your old password, new password, and confirm your new password.");
            }
            else if (oldPassword != inputOld)                                                                                                //check if the old password matches what they put in
            {
                MessageBox.Show("Your old password and the password you put in as your old password do not match.");
            }
            else if (inputNew != inputConfirm)                                                                                               //check if the new password they put in and the confirmation as the same
            {
                MessageBox.Show("The new password you typed in and its confirmation do not match.");
            }
            else
            {
                user.Password = inputNew;                                                                                                    //update the database
                db.SaveChanges();
                backToMain(accountType);                                                                                                     //go back to the right main form
            }
        }

        private void backToMain(string accountType)
        {                                                                                                                                     //check the user's accountType and use that to take them to the right main form
            if (accountType == "Student")
            {
                StudentMain g = new StudentMain(id);
                g.Show();
                this.Dispose();
            }
            else if (accountType == "Faculty")
            {
                FacultyMain g = new FacultyMain(id);
                g.Show();
                this.Dispose();
            }
            else if (accountType == "Administrator")
            {
                AdminMain g = new AdminMain();
                g.Show();
                this.Dispose();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)                                     
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();                                                                        //open the database
            User user = (from row in db.Users.AsEnumerable() where row.ID == id select row).First();                                         //get the user account
            string accountType = user.AccountType;                                                                                           //get the accountType of the user
            backToMain(accountType);                                                                                                         //send the user back to main without having changed anything
        }

        private void ChangePasswordForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Login g = new Login();
            g.Show();
            this.Dispose();
        }
    }
}
