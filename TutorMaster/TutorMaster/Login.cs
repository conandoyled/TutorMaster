﻿using System;
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

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))             //make sure both fields are filled in
            {
                MessageBox.Show("Please enter in both a username and password.");
            }
            else if (isValidUser(username, password)) //checks to see if the account is valid
            {
                if (username.Contains('?'))         //usernames can't contain '?'
                {
                    lblErrMsg.Text = "Invalid Username or Password. Try again.";
                }
                else
                {
                    string accType = getAccType(username);
                    int accID = getID(username);
                    //lblErrMsg.Text = accType + accID.ToString();
                    switch (accType)
                    {
                        case "Student":
                            //send ID to student form
                            StudentMain a = new StudentMain(accID);
                            a.Show();
                            this.Hide();
                            break;
                        case "Faculty":
                            //send ID to faculty form
                            FacultyMain g = new FacultyMain(accID);

                            g.Show();
                            this.Hide();
                            break;
                        default:        //Admin account
                            //open admin form (shouldn't need ID?)
                            AdminMain f = new AdminMain();
                            f.Show();
                            this.Hide();
                            break;
                    }
                }
            }
            else
            {
                lblErrMsg.Text = "Invalid Username or Password. Try again.";
            }
        }

        private bool isValidUser(string username, string password)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4(); //This creates a reference to an entity so we can point and look through things (I think)

            bool found = false;
            if (db.Users.Any(u => u.Username == username)) //checks the User DB to see if the username matches any user in the DB
            {
                string pw = (from row in db.Users where row.Username == username select row.Password).Single(); //pull the password from the DB
                if (password == pw) //If the pulled password matches the entered one then found = true
                {
                    found = true;
                }
            }

            return (found);
        }

        private string getAccType(string username)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4(); //This creates a reference to an entity so we can point and look through things (I think)

            string accType = (from row in db.Users where row.Username == username select row.AccountType).Single(); //Same kind of idea, check all the entries in User to find which user we're talking about. Then pull out the single attribute accountType

            return (accType);
        }

        private int getID(string username)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4(); //This creates a reference to an entity so we can point and look through things (I think)

            int accID = (from row in db.Users where row.Username == username select row.ID).First(); //Same as accountType but with a different attribute

            return accID;
        }

        private void clearErrMsg(object sender, EventArgs e)
        {
            lblErrMsg.Text = "";
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            CreateStudent g = new CreateStudent(3);
            g.Show();
            this.Hide();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        //from password field, enter key tries to log you in
        {
            if (e.KeyValue == '\r')
            {
                btnSignIn_Click(sender, null);
            }
        }

        private void cbxShowPassword_CheckedChanged(object sender, EventArgs e)
        //allows user to see password they're typing if they wwant
        {
            if (cbxShowPassword.Checked == true)
            {
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '*';
            }
        }



    }
}
