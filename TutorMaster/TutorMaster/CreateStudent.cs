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
            string phone = txtPhoneNumber.Text;
            string email = txtEmail.Text;
            string accounttype = "Student";
            bool tutor = cbxTutorTutee.GetItemChecked(0);
            bool tutee = cbxTutorTutee.GetItemChecked(1);

            if (!tutor && !tutee)
            {
                MessageBox.Show("Please select at least one of tutor and/or tutee");
            }
            else
            {
                TutorMaster.User newUser = new TutorMaster.User();
                newUser.ID = getNextID();
                newUser.FirstName = fname;
                newUser.LastName = lname;
                newUser.Username = username;
                newUser.Password = password;
                newUser.PhoneNumber = phone;
                newUser.Email = email;
                newUser.AccountType = accounttype;
                addUser(newUser);

                TutorMaster.Student newStudent = new TutorMaster.Student();
                newStudent.ID = newUser.ID;
                newStudent.Tutee = tutee;
                newStudent.Tutor = tutor;
                addStudent(newStudent);
            }
        }

        private void addUser(TutorMaster.User user)
        {
            TutorMasterDBEntities1 db = new TutorMasterDBEntities1();
            db.Users.AddObject(user);
            db.SaveChanges();
        }

        private void addStudent(TutorMaster.Student student)
        {
            TutorMasterDBEntities1 db = new TutorMasterDBEntities1();
            db.Students.AddObject(student);
            db.SaveChanges();
        }

        private int getNextID()
        {
            TutorMasterDBEntities1 db = new TutorMasterDBEntities1();
            int rowNum = db.Users.Count();
            var lastRow = db.Users.Skip(rowNum - 1).FirstOrDefault();
            return lastRow.ID + 1;
        }


        private void cbxTutorTutee_ItemChecked(object sender, ItemCheckEventArgs e)
        {
            if (!cbxTutorTutee.GetItemChecked(0))
            {
                this.Width += 400;
            }
            if (cbxTutorTutee.GetItemChecked(0))
            {
                this.Width -= 400;
            }

        }
    }
}

