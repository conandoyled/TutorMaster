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

            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
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
            this.Dispose();
        }

        private void CreateStudent_FormClosed(object sender, FormClosedEventArgs e)
        {
            Login g = new Login();
            g.Show();
            this.Dispose();
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
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();

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
            else if (!uniqueUsername(username))
            {
                MessageBox.Show("Username is already taken. Please pick another one.");
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

                if (tutor)
                {
                    int numDepartments = tvClasses.Nodes.Count;
                    for (int i = 0; i < numDepartments; i++)
                    {
                        int numNodes = tvClasses.Nodes[i].Nodes.Count;
                        for (int j = 0; j < numNodes; j++)
                        {
                            TreeNode tn = tvClasses.Nodes[i].Nodes[j];
                            if (tn.Checked)
                            {
                                TutorMaster.TutorRequest request = new TutorMaster.TutorRequest();
                                request.Key = getNextRequestKey();
                                request.ID = newStudent.ID;
                                string classCode = (from row in db.Classes where row.ClassName == tn.Text select row.ClassCode).First();
                                request.ClassCode = classCode;
                                addRequest(request);
                            }
                        }
                    }
                }

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
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            db.Users.AddObject(user);
            db.SaveChanges();
        }

        private void addStudent(TutorMaster.Student student)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            db.Students.AddObject(student);
            db.SaveChanges();
        }

        private void addRequest(TutorMaster.TutorRequest request)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            db.TutorRequests.AddObject(request);
            db.SaveChanges();
        }

        private int getNextID()
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            int rowNum = db.Users.Count();
            
            var lastRow = db.Users.OrderByDescending(u => u.ID).Select(r => r.ID).First();
            return lastRow + 1;
        }

        private int getNextRequestKey()
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            int rowNum = db.TutorRequests.Count();
            int lastRow;

            if (rowNum > 0)
            {
                lastRow = db.TutorRequests.OrderByDescending(u => u.Key).Select(r => r.Key).First();
            }
            else
            {
                lastRow = 0;
            }
            return lastRow + 1;
        }

        private void cbxTutor_CheckStateChanged(object sender, EventArgs e)
        {
            if (!cbxTutor.Checked)
            {
                this.Width -= 250;
                imgLogo.Location = new Point(imgLogo.Location.X - 250 , imgLogo.Location.Y);
                lblTClasses.Hide();
                tvClasses.Hide();
            }
            else
            {
                this.Width += 250;
                imgLogo.Location = new Point(imgLogo.Location.X + 250, imgLogo.Location.Y);
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

        private bool uniqueUsername(string username)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            return (!db.Users.Any(u => u.Username == username));
        }

        //Doesn't work on double click
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

