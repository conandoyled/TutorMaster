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
        private List<string> usernameList;

        int action;
        const int CREATE = 1;
        const int EDIT = 2;
        const int REQUEST = 3;

        int accID;

        public CreateStudent(int act, int id = 0)
        {
            InitializeComponent();

            action = act;
            accID = id;
            setupClasses();
            getUsernames();
            if (action == EDIT)
            {
                loadFormInfo(accID);
            }

            setButtonText();
        }

        //this function gets all of the usernames in the database and puts them into a list
        private void getUsernames()
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            usernameList = (from row in db.Users select row.Username).ToList();
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

        private void setButtonText()
        {
            switch (action)
            {
                case CREATE:
                    btnAdd.Text = "Add Student";
                    break;
                case EDIT:
                    btnAdd.Text = "Save Student";
                    break;
                case REQUEST:
                    btnAdd.Text = "Submit Request";
                    break;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (action == REQUEST)
            {
                Login g = new Login();
                g.Show();
                this.Dispose();
            }
            else
            {
                AdminMain g = new AdminMain();
                g.Show();
                this.Dispose();
            }
        }

        private void CreateStudent_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (action == REQUEST)
            {
                Login g = new Login();
                g.Show();
                this.Dispose();
            }
            else
            {
                AdminMain g = new AdminMain();
                g.Show();
                this.Dispose();
            }
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

            if (goodToAdd(fname, lname, username, password, phone, email, tutor, tutee))
            {
                if (action == EDIT)
                {
                    if (tutor)
                    {
                        saveClasses();
                    }
                    else
                    {
                        if ((bool)(from row in db.Students where row.ID == accID select row.Tutor).First())
                        {
                            removeAllClasses();
                        }
                    }

                    var updateUser = (from row in db.Users where row.ID == accID select row).Single();

                    updateUser.FirstName = fname;
                    updateUser.LastName = lname;
                    updateUser.Username = username;
                    updateUser.Password = password;
                    updateUser.PhoneNumber = phone;
                    updateUser.Email = email;
                    updateUser.AccountType = accounttype;
                    db.SaveChanges();

                    var updateStudent = (from row in db.Students where row.ID == accID select row).Single();
                    updateStudent.Tutor = tutor;
                    updateStudent.Tutee = tutee;
                    db.SaveChanges();

                    AdminMain g = new AdminMain();
                    g.Show();
                    this.Dispose();
                }
                else
                {
                    int ID = getNextID();
                    if (action == REQUEST)
                    {
                        username += "?";
                    }

                    saveNewUser(fname, lname, username, password, email, phone, accounttype, ID);
                    saveNewTutorTutee(tutor, tutee, ID);

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
                                    request.ID = ID;
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
                    if (action == CREATE)
                    {
                        MessageBox.Show("Student has been added to the database.");
                    }
                    else
                    {
                        MessageBox.Show("Request has been sent to the administrator.");
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

        private bool verifyTaken()
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            string oldUsername;
            try
            {
                oldUsername = (from row in db.Users where row.ID == accID select row.Username).First();
            }
            catch (Exception e)
            {
                oldUsername = "";
            }
            
            string username;
            for (int i = 0; i < usernameList.Count(); i++)
            {
                username = usernameList[i];
                if (txtUsername.Text == username || (txtUsername.Text + "?") == username)
                {

                    if (action == EDIT)
                    {
                        if (!username.Equals(oldUsername))
                        {
                            lblUsername.Text = "Already Taken";
                            return true;
                        }
                    }
                    else
                    {
                        lblUsername.Text = "Already Taken";
                        return true;
                    }
                }
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
            newUser.Username = username;
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

        private void addRequest(TutorMaster.TutorRequest request)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            db.TutorRequests.AddObject(request);
            db.SaveChanges();
        }

        private void saveClasses()
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            //var numReq = db.TutorRequests.Count(x => x.ID == accID);
            var requestClasses = (from stucla in db.TutorRequests
                                where stucla.ID == accID
                                join cla in db.Classes on stucla.ClassCode equals cla.ClassCode
                                select cla).ToList();
            var acceptedClasses = (from stucla in db.StudentClasses
                                 where stucla.ID == accID
                                 join cla in db.Classes on stucla.ClassCode equals cla.ClassCode
                                 select cla).ToList();

            int numDepartments = tvClasses.Nodes.Count;
            for (int i = 0; i < numDepartments; i++)
            {
                int numNodes = tvClasses.Nodes[i].Nodes.Count;
                for (int j = 0; j < numNodes; j++)
                {
                    TreeNode tn = tvClasses.Nodes[i].Nodes[j];
                    if (!tn.Checked)
                    {
                        //if already approved but not checked, delete from approved
                        if (acceptedClasses.Exists(x => x.ClassName.Equals(tn.Text)))
                        {
                           string classCode = acceptedClasses.Find(x => x.ClassName.Equals(tn.Text)).ClassCode;
                           db.StudentClasses.DeleteObject((from row in db.StudentClasses where row.ClassCode == classCode select row).First());
                        }
                        //if requested and unchecked, delete from requests
                        if (requestClasses.Exists(x => x.ClassName.Equals(tn.Text)))
                        {
                            string classCode = requestClasses.Find(x => x.ClassName.Equals(tn.Text)).ClassCode;
                            db.TutorRequests.DeleteObject((from row in db.TutorRequests where row.ClassCode == classCode select row).First());
                        }
                    }
                    //if not approved or request but check, request
                    else
                    {
                        if (!(acceptedClasses.Exists(x => x.ClassName.Equals(tn.Text))) && !(requestClasses.Exists(x => x.ClassName.Equals(tn.Text))))
                        {
                            TutorMaster.TutorRequest request = new TutorMaster.TutorRequest();
                            request.Key = getNextRequestKey();
                            request.ID = accID;
                            string classCode = (from row in db.Classes where row.ClassName == tn.Text select row.ClassCode).First();
                            request.ClassCode = classCode;
                            addRequest(request);
                        }
                    }
                }
            }

            db.SaveChanges();
        }

        private void removeAllClasses()
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();

            var requests = (from row in db.TutorRequests where row.ID == accID select row).ToList();
            var accepts = (from row in db.StudentClasses where row.ID == accID select row).ToList();

            foreach (TutorRequest tr in requests)
            {
                db.DeleteObject(tr);
            }

            foreach (StudentClass sc in accepts)
            {
                db.DeleteObject(sc);
            }

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

        private void loadFormInfo(int accID)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            txtFirstname.Text = (from row in db.Users where row.ID == accID select row.FirstName).First();
            txtLastname.Text = (from row in db.Users where row.ID == accID select row.LastName).First();
            txtUsername.Text = (from row in db.Users where row.ID == accID select row.Username).First().Replace("?","");
            txtPassword.Text = (from row in db.Users where row.ID == accID select row.Password).First();
            txtPhoneNumber.Text = (from row in db.Users where row.ID == accID select row.PhoneNumber).First();
            txtEmail.Text = (from row in db.Users where row.ID == accID select row.Email).First();
            cbxTutor.Checked = Convert.ToBoolean((from row in db.Students where row.ID == accID select row.Tutor).First());
            cbxTutee.Checked = Convert.ToBoolean((from row in db.Students where row.ID == accID select row.Tutee).First());

            getClasses(accID);
        }

        private void getClasses(int accID)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            //var numReq = db.TutorRequests.Count(x => x.ID == accID);
            var requestCodes = (from row in db.TutorRequests.AsEnumerable() where row.ID == accID select row.ClassCode).ToArray();
            var acceptedCodes = (from row in db.StudentClasses.AsEnumerable() where row.ID == accID select row.ClassCode).ToArray();

            int numCourses = requestCodes.Length + acceptedCodes.Length;
            string[] requestClasses = new string[numCourses];
            for (int n = 0; n < requestCodes.Length; n++)
            {
                requestClasses[n] = (from row in db.Classes.AsEnumerable() where requestCodes[n] == row.ClassCode select row.ClassName).First();
            }
            for (int n = requestCodes.Length; n < acceptedCodes.Length; n++)
            {
                requestClasses[n] = (from row in db.Classes.AsEnumerable() where acceptedCodes[n] == row.ClassCode select row.ClassName).First();
            }

            int numDepartments = tvClasses.Nodes.Count;
            for (int i = 0; i < numDepartments; i++)
            {
                int numNodes = tvClasses.Nodes[i].Nodes.Count;
                int count = 0;
                for (int j = 0; j < numNodes; j++)
                {
                    TreeNode tn = tvClasses.Nodes[i].Nodes[j];
                    if (requestClasses.Contains(tn.Text))
                    {
                        tn.Checked = true;
                        count++;
                    }
                }
                if (count == numNodes)
                {
                    tvClasses.Nodes[i].Checked = true;
                }
            }
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

