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

        //constructor
        public CreateStudent(int act, int id = 0)
        {
            InitializeComponent();

            action = act;
            accID = id;
            setupClasses();             //sets up treeview of classes         
            getUsernames();             //gets a list of existing usernames
            if (action == EDIT)         //if in edit mode, sets fields to existing information on the student being edited
            {
                loadFormInfo(accID);
            }

            setButtonText();            //sets the button text based on action user is taking
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
        //sets the button text based on what the user wants to do
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
        //sends you back to the correct form
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
        //send you back to the correct form
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
            string fname = txtFirstname.Text;                               //gets information from the form fields
            string lname = txtLastname.Text;
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string phone = txtPhoneNumber.Text;
            string email = txtEmail.Text;
            string accounttype = "Student";
            bool tutor = cbxTutor.Checked;
            bool tutee = cbxTutee.Checked;

            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();

            if (goodToAdd(fname, lname, username, password, phone, email, tutor, tutee))            //checks if the form is filled out appropriately
            {
                if (action == EDIT)                 //if the user is editing a student
                {
                    if (tutor)                      //save the classes if student is a tutor
                    {
                        saveClasses();
                    }
                    else                            //if tutor status has been removed, remove all the classes they were approved for/had requested
                    {
                        if ((bool)(from row in db.Students where row.ID == accID select row.Tutor).First())
                        {
                            removeAllClasses();
                        }
                    }

                    User updateUser = (from row in db.Users where row.ID == accID select row).Single();      //get the user object from the database

                    updateUser.FirstName = fname;                                       //update the object to admin input
                    updateUser.LastName = lname;
                    updateUser.Username = username;
                    updateUser.Password = password;
                    updateUser.PhoneNumber = phone;
                    updateUser.Email = email;
                    updateUser.AccountType = accounttype;

                    Student updateStudent = (from row in db.Students where row.ID == accID select row).Single();    //get the Student object from the database
                    updateStudent.Tutor = tutor;                                        //update the object to admin input
                    updateStudent.Tutee = tutee;

                    db.SaveChanges();           //save changes

                    AdminMain g = new AdminMain();      //return to admin main
                    g.Show();
                    this.Dispose();
                }
                else                                       //user is in request or create mode
                {
                    int ID = getNextID();                   //gets ID for new user
                    if (action == REQUEST)                  //if in request mode, adds the indicator to the end of the username
                    {
                        username += "?";
                    }

                    saveNewUser(fname, lname, username, password, email, phone, accounttype, ID);       //saves the user and student account to the database
                    saveNewTutorTutee(tutor, tutee, ID);

                    if (tutor)                                                      //if tutor, sends appropriate tutor requests
                    {
                        int numDepartments = tvClasses.Nodes.Count;
                        for (int i = 0; i < numDepartments; i++)                    //loop through departments
                        {
                            int numNodes = tvClasses.Nodes[i].Nodes.Count;          //for each class in a department
                            for (int j = 0; j < numNodes; j++)
                            {
                                TreeNode tn = tvClasses.Nodes[i].Nodes[j];
                                if (tn.Checked)                                     //check if the class is checked
                                {
                                    TutorMaster.TutorRequest request = new TutorMaster.TutorRequest();              //create the request
                                    request.Key = getNextRequestKey();
                                    request.ID = ID;
                                    string classCode = (from row in db.Classes where row.ClassName == tn.Text select row.ClassCode).First();
                                    request.ClassCode = classCode;
                                    addRequest(request);                                //add request to database
                                }
                            }
                        }
                    }

                    uncheckTree();                                          //reset the form
                    txtFirstname.Text = "";
                    txtLastname.Text = "";
                    txtUsername.Text = "";
                    txtPassword.Text = "";
                    txtPhoneNumber.Text = "";
                    txtEmail.Text = "";
                    cbxTutor.Checked = false;
                    cbxTutee.Checked = false;

                    if (action == CREATE)                                                       //show user appropriate message
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

        private void uncheckTree()
        //uncheck all the classes in the treeview
        {
            int numDepartments = tvClasses.Nodes.Count;
            for (int i = 0; i < numDepartments; i++)
            {
                int numNodes = tvClasses.Nodes[i].Nodes.Count;
                tvClasses.Nodes[i].Checked = false;
                for (int j = 0; j < numNodes; j++)
                {
                    TreeNode tn = tvClasses.Nodes[i].Nodes[j];
                    tn.Checked = false;
                }
            }
        }

        private bool goodToAdd(string fname, string lname, string username, string password, string phone, string email, bool tutor, bool tutee)
        //checks that the form is filled out approprately to add a student
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
        //checks if a username is taken
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();

            string oldUsername;
            if (action == EDIT)                         //if user is in edit mode, gets their previous username
            {
                oldUsername = (from row in db.Users where row.ID == accID select row.Username).First();
            }
            else
            {
                oldUsername = "";
            }
            
            string username;
            for (int i = 0; i < usernameList.Count(); i++)          //for each username in the list
            {
                username = usernameList[i];
                if (txtUsername.Text == username || (txtUsername.Text + "?") == username)       //check if the username field exists already with or without the request indicator
                {
                    if (action == EDIT)                             //if user is in edit mode, allow them to keep the username if it was already theirs
                    {
                        if (!username.Equals(oldUsername))
                        {
                            lblUsername.Text = "Already Taken";
                            return true;
                        }
                    }
                    else                                            //otherwise alert user the username is already taken
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
        //add tutor request to the datbase
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
                for (int j = 0; j < numNodes; j++)                          //loop through each class in each department and check whether the node is checked
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
                    //if not approved or requested but checked, submit a request
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
        //remove all a users tutor classes and tutor requests
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
        //get unused id for a new user
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            int rowNum = db.Users.Count();
            
            var lastRow = db.Users.OrderByDescending(u => u.ID).Select(r => r.ID).First();
            return lastRow + 1;
        }

        private int getNextRequestKey()
        //get unused id for a new tutor request
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
        //expand the window and show the classes if tutor is checked
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
        //make sure valid email and phone number have been entered
        {
            string address = email.Substring(email.Length - 4);
            if ((email.Contains('@')) && (phone.Length == 14) && (address == ".edu" || address == ".com"))
            {
                return true;
            }
            return false;
        }

        private void loadFormInfo(int accID)
        //gets called if user is in edit mode
        //gets object being edited from the database and loads the form fields
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

            if (cbxTutor.Checked)
            {
                getClasses(accID);          //gets and loads the classes of the student if they're a tutor
            }
        }

        private void getClasses(int accID)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            
            var requestCodes = (from row in db.TutorRequests.AsEnumerable() where row.ID == accID select row.ClassCode).ToList();      //gets the class codes of a student's tutor request and tutor classes
            var acceptedCodes = (from row in db.StudentClasses.AsEnumerable() where row.ID == accID select row.ClassCode).ToList();

            List<string> classes = new List<string>();

            foreach (string r in requestCodes)                          //adds all the request class names to a list
            {
                classes.Add((from row in db.Classes.AsEnumerable() where r == row.ClassCode select row.ClassName).First());
            }
            foreach (string a in acceptedCodes)                         //adds all the accepeted class names to a list
            {
                classes.Add((from row in db.Classes.AsEnumerable() where a == row.ClassCode select row.ClassName).First());
            }

            int numDepartments = tvClasses.Nodes.Count;
            for (int i = 0; i < numDepartments; i++)                    //loop through all the departments
            {
                int numNodes = tvClasses.Nodes[i].Nodes.Count;
                int count = 0;
                for (int j = 0; j < numNodes; j++)                      //go through all the classes in each department
                {
                    TreeNode tn = tvClasses.Nodes[i].Nodes[j];
                    if (classes.Contains(tn.Text))                      //if the class was in requests or accepted, check the node
                    {
                        tn.Checked = true;
                        count++;
                    }
                }
                if (count == numNodes)                                  //if the number of classes checked is equal to the number of the classes in the department, check the department 
                {
                    tvClasses.Nodes[i].Checked = true;
                }
            }
        }

        private void tvClasses_AfterCheck(object sender, TreeViewEventArgs e)
        //if department node is check, check all the classes in that department
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
        //check if the username is taken already as the user types
        {
            bool taken = verifyTaken();
            if (!taken)
            {
                lblUsername.Text = "Username";
            }
        }
    }
}

