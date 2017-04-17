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
    public partial class SignUpTutorTutee : Form
    {
        private List<string> usernameList;
        public SignUpTutorTutee()
        {
            InitializeComponent();
            getUsernames();
            setupClasses();
        }

        private void getUsernames()
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            usernameList = db.Users.Select(u => u.Username).ToList();
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


        private void btnCancel_Click(object sender, EventArgs e)
        {
            Login g = new Login();
            g.Show();
            this.Close();
        }

        private int getNextID()                                                                     //go into database and get the last commitment ID
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            int rowNum = db.Users.Count();
            int lastRow;

            if (rowNum > 0)
            {
                lastRow = db.Users.OrderByDescending(u => u.ID).Select(r => r.ID).First();
            }
            else
            {
                lastRow = 0;
            }
            return lastRow + 1;
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

        private void saveNewUser(string fname, string lname, string username, string password, string email, string accounttype, int id)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            User newUser = new User();
            newUser.ID = id;
            newUser.FirstName = fname;
            newUser.LastName = lname;
            newUser.Username = username + "?";
            newUser.Password = password;
            newUser.Email = email;
            newUser.AccountType = accounttype;
            db.Users.AddObject(newUser);
            db.SaveChanges();
        }

        private void saveNewTutorTutee(bool tutor, bool tutee, int id)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            Student newStudent = new Student();
            newStudent.Tutee = tutee;
            newStudent.Tutor = tutor;
            newStudent.ID = id;
            db.Students.AddObject(newStudent);
            db.SaveChanges();
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

        private void addRequest(TutorMaster.TutorRequest request)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            db.TutorRequests.AddObject(request);
            db.SaveChanges();
        }

        private void recordTutorRequests(int ID)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
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
                        request.ID = ID;
                        string classCode = (from row in db.Classes where row.ClassName == tn.Text select row.ClassCode).First();
                        request.ClassCode = classCode;
                        addRequest(request);
                    }
                }
            }
        }

        private bool goodToAdd(string fname, string lname, string username, string password, string phone, string email, bool tutor, bool tutee)
        {
            bool good = true;
            if (string.IsNullOrEmpty(fname) || string.IsNullOrWhiteSpace(lname) ||
                string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) ||
                    string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Please fill in all of the text fields.");
                good = false;
            }
            else if (!tutor && !tutee)
            {
                MessageBox.Show("Please select at least one of tutor and/or tutee.");
                good = false;
            }
            else if (!validateInfo(email, phone))
            {
                MessageBox.Show("Please put in a valid email address and phone number.");
                good = false;
            }
            else if (username.Contains("?"))
            {
                MessageBox.Show("Please do not put a question mark into your username.");
                good = false;
            }
            else if (verifyTaken())
            {
                MessageBox.Show("Please pick a unique username.");
                good = false;
            }
            return good;
        }

        private void btnRegister_Click(object sender, EventArgs e)
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


            if(goodToAdd(fname, lname, username, password, phone, email, tutor, tutee))
            {
                int ID = getNextID();
                saveNewUser(fname, lname, username, password, email, accounttype, ID);
                saveNewTutorTutee(tutor, tutee, ID);
                if (tutor)
                {
                    recordTutorRequests(ID);
                }
            }
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

        private void SignUpTutorTutee_Load(object sender, EventArgs e)
        {

        }
        
        private bool verifyTaken()
        {
            for (int i = 0; i < usernameList.Count(); i++)
            {
                if (txtUsername.Text == usernameList[i])
                {
                    lblUsername.Text = "Already Taken";
                    return true;
                }
            }
            return false;
        }

        private void txtUsername_KeyUp(object sender, KeyEventArgs e)
        {

            bool taken = verifyTaken();
            if (!taken)
            {
                lblUsername.Text = "Username";
            }
        }
    }
}
