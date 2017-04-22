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
        
        //constructor
        public SignUpTutorTutee()
        {
            InitializeComponent();
            getUsernames();
            setupClasses();
        }

        //this function gets all of the usernames in the database and puts them into a list
        private void getUsernames()
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            usernameList = db.Users.Select(u => u.Username).ToList();
        }

        //this function gets all of the classes in the database and the department names and loads them into a tree view
        private void setupClasses()
        {
            tvClasses.CheckBoxes = true;                                                         //have the treeview have checkboxes

            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();                            //connect to database
            var classes = from c in db.Classes select c;                                         //get the classes from the class table
            List<Class> cls = new List<Class>();
            cls = classes.ToList();                                                              //convert them into a list of classes

            foreach (Class cl in cls)                                                            //for each class in the list of classes
            {
                if (tvClasses.Nodes.ContainsKey(cl.Department))                                  //if it is contained in a department
                {
                    tvClasses.Nodes[cl.Department].Nodes.Add(new TreeNode(cl.ClassName));        //put it in its department as a node
                }
                else
                {
                    TreeNode nNode = new TreeNode(cl.Department);                                //add the department nodes
                    nNode.Name = cl.Department;
                    nNode.Nodes.Add(cl.ClassName);
                    tvClasses.Nodes.Add(nNode);
                }
            }
            tvClasses.Sort();                                                                    //sort the treeview
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {                                                                                        //if they pick cancel, take them back to the login screen
            Login g = new Login();
            g.Show();
            this.Dispose();
        }

        private int getNextID()                                                                  //go into database and get the last commitment ID
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
            return lastRow + 1;                                                                  //get index of last row and add 1. (This is our autoincrementer)
        }

        private bool validateInfo(string email, string phone)
        {
            string address = email.Substring(email.Length - 4);                                  //make sure the email is a .edu or .com email addess and has @
            if ((email.Contains('@')) && (phone.Length == 14) && (address == ".edu" || address == ".com"))
            {
                return true;
            }
            return false;
        }

        private void saveNewUser(string fname, string lname, string username, string password, string email, string phone, string accounttype, int id)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();                          //connection to new database
            User newUser = new User();                                                         //create a new user and load him up with the information
            newUser.ID = id;
            newUser.FirstName = fname;
            newUser.LastName = lname;
            newUser.Username = username + "?";
            newUser.Password = password;
            newUser.Email = email;
            newUser.AccountType = accounttype;
            newUser.PhoneNumber = phone;
            db.Users.AddObject(newUser);                                                       //add them to the users table in the database
            db.SaveChanges();                                                                  //save the changes to the database
        }

        private void saveNewTutorTutee(bool tutor, bool tutee, int id)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();                          //connect to database
            Student newStudent = new Student();                                                //make new student object
            newStudent.Tutee = tutee;                                                          //load up the information
            newStudent.Tutor = tutor;
            newStudent.ID = id;
            db.Students.AddObject(newStudent);                                                 //add the tutor/tutee to the database
            db.SaveChanges();                                                                  //save the changes
        }

        private int getNextRequestKey()
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();                          //connect to the database
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
                saveNewUser(fname, lname, username, password, email, phone, accounttype, ID);
                saveNewTutorTutee(tutor, tutee, ID);
                if (tutor)
                {
                    recordTutorRequests(ID);
                }
                Login g = new Login();
                g.Show();
                this.Dispose();
            }
        }

        private void cbxTutor_CheckStateChanged(object sender, EventArgs e)
        {
            if (!cbxTutor.Checked)
            {
                this.Width -= 250;
                imgLogo.Location = new Point(imgLogo.Location.X - 250, imgLogo.Location.Y);
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

        private void SignUpTutorTutee_Load(object sender, EventArgs e)
        {

        }
        
        private bool verifyTaken()
        {
            for (int i = 0; i < usernameList.Count(); i++)
            {
                if (txtUsername.Text == usernameList[i] || (txtUsername.Text+"?") == usernameList[i])
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
