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

            setupClasses();
        }

        private void setupClasses()
        {
            tvClasses.CheckBoxes = true;

            TutorMasterDBEntities1 db = new TutorMasterDBEntities1();
            var classes = from c in db.Classes select c;
            List<Class> cls = new List<Class>();
            cls = classes.ToList();

            foreach (Class cl in cls)
            {
                if (tvClasses.Nodes.ContainsKey(cl.Department))
                {
                    tvClasses.Nodes[cl.Department].Nodes.Add(new TreeNode(cl.ClassName));
                }
                else
                {
                    TreeNode nNode = new TreeNode(cl.Department);
                    nNode.Name = cl.Department;
                    nNode.Nodes.Add(cl.ClassName);
                    tvClasses.Nodes.Add(nNode);
                }
            }

            tvClasses.Sort();
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
            bool tutor = cbxTutor.Checked;
            bool tutee = cbxTutee.Checked;

            if (!tutor && !tutee)
            {
                MessageBox.Show("Please select at least one of tutor and/or tutee");
            }
            else if (string.IsNullOrEmpty(fname) || string.IsNullOrWhiteSpace(lname) ||
                string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) ||
                    string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Please fill in all of the textboxes with the approriate information");
            }
            else if (!validateInfo(email, phone))
            {
                MessageBox.Show("Please put in a valid email address and phone number");
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

                txtFirstname.Text = "";
                txtLastname.Text = "";
                txtUsername.Text = "";
                txtPassword.Text = "";
                txtPhoneNumber.Text = "";
                txtEmail.Text = "";
                cbxTutor.Checked = false;
                cbxTutee.Checked = false;
                MessageBox.Show("Student has been added to the database");
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
            
            var lastRow = db.Users.OrderByDescending(u => u.ID).Select(r => r.ID).First();
            return lastRow + 1;
        }

        private void cbxTutor_CheckStateChanged(object sender, EventArgs e)
        {
            if (!cbxTutor.Checked)
            {
                this.Width -= 250;
                lblTClasses.Hide();
                tvClasses.Hide();
            }
            else
            {
                this.Width += 250;
                tvClasses.Show();
                lblTClasses.Show();
            }
        }

        private bool validateInfo(string email, string phone)
        {
            string address = email.Substring(email.Length - 4);
            if ((email.Contains('@')) && (phone.Length == 14) && (address == ".edu" || address == ".com"))
            {
                return true;
            }
            return false;
        }

        //Doesn't work when checked too fast
        private void tvClasses_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node.Nodes.Count > 0)
                {
                    foreach (TreeNode node in e.Node.Nodes)
                    {
                        node.Checked = e.Node.Checked;
                    }
                }
            }
        }
    }
}

