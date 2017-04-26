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
    public partial class AdminMain : Form
    {
        //Dynamic button declarations
        Button btnFacSave = new Button();
        Button btnFacCancel = new Button();
        Button btnClassSave = new Button();
        Button btnClassCancel = new Button();

        //constructor
        public AdminMain()
        {
            InitializeComponent(); 

            setupStudentLV();           //load up all the students
            setupFacultyLV();           //load up all the faculty
            setupClassLV();             //load up all the classes
            setupDepartmentBoxes();     //load up the boxes that hold departments
            setupHiddenButtons();       //create the dynamic buttons and hide them
            disableButtons();           //disable all the buttons that should be enabled yet
        }

        private void AdminMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Login g = new Login();
            g.Show();
            this.Dispose();
        }

        private void setupStudentLV() //This is what populates the box of students
        {
            lvStudent.CheckBoxes = true;
            lvStudent.Columns.Add("     Username", 110);        // This first block of commands sets up the top row.
            lvStudent.Columns.Add("Last Name", 100);
            lvStudent.Columns.Add("First Name", 100);
            lvStudent.Columns.Add("Tutor", 50);
            lvStudent.Columns.Add("Tutee", 50);
            lvStudent.Columns.Add("Email", 100);
            lvStudent.Columns.Add("Phone Number", 100);

            lvRequests.CheckBoxes = true;
            lvRequests.Columns.Add("     Username", 110);       // This first block of commands sets up the top row.
            lvRequests.Columns.Add("Last Name", 100);
            lvRequests.Columns.Add("First Name", 100);
            lvRequests.Columns.Add("Tutor", 50);
            lvRequests.Columns.Add("Tutee", 50);
            lvRequests.Columns.Add("Email", 100);
            lvRequests.Columns.Add("Phone Number", 100);

            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();       //create a new indirect entity
            List<Student> stus = (from c in db.Students select c).ToList();                  //pull all the students from the databse

            int newUsers = 0;
            foreach(Student stu in stus)                                    //add all the students in the list to the listview
            {
                string tutee = "No";
                string tutor = "Yes";
                User user = (from row in db.Users where row.ID == stu.ID select row).First();
                if ((bool)stu.Tutee)
                    tutee = "Yes";
                else
                    tutee = "No";
                if ((bool)stu.Tutor)
                    tutor = "Yes";
                else
                    tutor = "No";

                if (user.Username.Contains('?'))                            //track whether or not the user is a new one waiting for approval
                {
                    newUsers++;
                    lvRequests.Items.Add(new ListViewItem(new string[] { user.Username, user.LastName, user.FirstName, tutor, tutee, user.Email, user.PhoneNumber }));
                }
                else
                {
                    lvStudent.Items.Add(new ListViewItem(new string[] { user.Username, user.LastName, user.FirstName, tutor, tutee, user.Email, user.PhoneNumber }));
                }
            }

            if (newUsers > 0)                                               //alert admin of how many users are waiting for approval
            {
                MessageBox.Show("There are " + newUsers.ToString() + " student(s) that requesting to become tutor(s) or tutee(s)");
            }

            
        }

        private void setupFacultyLV()                                       //This is what populates the box of students
        {
            lvFaculty.CheckBoxes = true;
            lvFaculty.Columns.Add("     Username", 105);                    // This first block of commands sets up the top row.
            lvFaculty.Columns.Add("Last Name", 100);
            lvFaculty.Columns.Add("First Name", 100);
            lvFaculty.Columns.Add("Department", 100);

            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();       //create a new indirect entity
            List<Faculty> fs = (from c in db.Faculties select c).ToList();  //pull all the faculty from the database


            foreach (Faculty f in fs)                                       //add all the faculty tp the listview
            {
                User user = (from row in db.Users where row.ID == f.ID select row).First();
  
                lvFaculty.Items.Add(new ListViewItem(new string[] { user.Username, user.LastName, user.FirstName, f.Department }));
            }
        }

        private void setupClassLV()                                         //This is what populates the box of students
        {
            lvClass.CheckBoxes = true;
            lvClass.Columns.Add("     Class Code", 100);                    //This first block of commands sets up the top row.
            lvClass.Columns.Add("Class Name", 150);
            lvClass.Columns.Add("Department", 100);

            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            List<Class> cs = (from c in db.Classes select c).ToList();      //Get the classes from the database

            sortByCoursePrefix(ref cs);                                     //Sorting classes and then adding them to the list views                                      
            sortByNumAndAdd(cs);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Login g = new Login(); 
            g.Show();
            this.Dispose();
        }

        private void lvStudent_ItemChecked(object sender, ItemCheckedEventArgs e)
        //This function determines when certain buttons should be activated or deativated based on the number of items checked
        //(also adjust color of buttons to indicate wheter or not the button is enabled)
        {
            int itemsChecked = lvStudent.CheckedItems.Count;                        //get how many items are checked

            //only allow edits and seeing schedules if one item is checked
            if (itemsChecked == 1)                                                  
            {
                btnEdit.Enabled = true;
                btnEdit.BackColor = System.Drawing.Color.FromArgb(226, 226, 226);
                btnStudentSchedule.Enabled = true;
                btnStudentSchedule.BackColor = System.Drawing.Color.FromArgb(226, 226, 226);
            }
            else
            {
                btnEdit.Enabled = false;
                btnEdit.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
                btnStudentSchedule.Enabled = false;
                btnStudentSchedule.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
            }

            //only allow delete to be clicked if at least one item is checked
            if (itemsChecked > 0)
            {
                btnDelete.Enabled = true;
                btnDelete.BackColor = System.Drawing.Color.FromArgb(226, 226, 226);
            }
            else
            {
                btnDelete.Enabled = false;
                btnDelete.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
            }
        }

        private void lvFaculty_ItemChecked(object sender, ItemCheckedEventArgs e)
        //This function determines when certain buttons should be activated or deativated based on the number of items checked
        //(also adjust color of buttons to indicate wheter or not the button is enabled)
        {
            int itemsChecked = lvFaculty.CheckedItems.Count;                        //get how many items are checked

            //only allow edits if one item is checked
            if (itemsChecked == 1)
            {
                btnFacultyEdit.Enabled = true;
                btnFacultyEdit.BackColor = System.Drawing.Color.FromArgb(226, 226, 226);
            }
            else
            {
                btnFacultyEdit.Enabled = false;
                btnFacultyEdit.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
            }

            //only allow delete to be clicked if at least one item is checked
            if (itemsChecked > 0)
            {
                btnFacultyDelete.Enabled = true;
                btnFacultyDelete.BackColor = System.Drawing.Color.FromArgb(226, 226, 226);
            }
            else
            {
                btnFacultyDelete.Enabled = false;
                btnFacultyDelete.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
            }
        }

        private void lvClass_ItemChecked(object sender, ItemCheckedEventArgs e)
        //This function determines when certain buttons should be activated or deativated based on the number of items checked
        //(also adjust color of buttons to indicate wheter or not the button is enabled)
        {
            int itemsChecked = lvClass.CheckedItems.Count;                          //get how many items are checked

            //only allow edits if one item is checked
            if (itemsChecked == 1)
            {
                btnClassEdit.Enabled = true;
                btnClassEdit.BackColor = System.Drawing.Color.FromArgb(226, 226, 226);
            }
            else
            {
                btnClassEdit.Enabled = false;
                btnClassEdit.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
            }

            //only allow delete to be clicked if at least one item is checked
            if (itemsChecked > 0)
            {
                btnClassDelete.Enabled = true;
                btnClassDelete.BackColor = System.Drawing.Color.FromArgb(226, 226, 226);
            }
            else
            {
                btnClassDelete.Enabled = false;
                btnClassDelete.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------
        //functions for students
        private void btnCreateStudent_Click(object sender, EventArgs e)
        {
            CreateStudent g = new CreateStudent(1);                     //open the create student form in create mode
            g.Show();
            this.Dispose();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            string username = lvStudent.CheckedItems[0].Text.ToString();
            int studentID = (from row in db.Users where row.Username == username select row.ID).First();

            CreateStudent g = new CreateStudent(2, studentID);          //open the create student form in edit mode and pass the id of the student being edited
            g.Show();
            this.Dispose();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            int stuNum = lvStudent.CheckedItems.Count;
            for (int i = 0; i < stuNum; i++)
            {
                string username = lvStudent.CheckedItems[i].SubItems[0].Text;                               //gets the username of the user selected to delete
                User delU = (from row in db.Users where row.Username == username select row).First();       //gets the user from the database
                deletePartnerCommits(delU.ID);                                                              //removes all the commitments where the deleted student was the partner
                
                db.Users.DeleteObject(delU);                                                                //delete user from database
                db.SaveChanges();
            }


            lvStudent.Clear();                                                                              //reload the students listview
            setupStudentLV();
        }

        private void deletePartnerCommits(int deleteId)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            List<Commitment> cmtList = (from row in db.Commitments where row.ID == deleteId select row).ToList();       //get all the commits where the partner ID matches

            for (int p = 0; p < cmtList.Count; p++)                     //set all the commits back to  open status
            {
                if (cmtList[p].ID == deleteId)
                {
                    cmtList[p].ID = -1;
                    cmtList[p].Location = "-";
                    cmtList[p].Open = true;
                    cmtList[p].Class = "-";
                    cmtList[p].Tutoring = false;
                }
            }

            db.SaveChanges();
        }

        private void btnStudentSchedule_Click(object sender, EventArgs e)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            string username = lvStudent.CheckedItems[0].Text.ToString();                                    //get username of student
            int studentID = (from row in db.Users where row.Username == username select row.ID).First();    //get the ID of the student

            AdminSeeSchedule g = new AdminSeeSchedule(studentID);
            g.Show();
        }

        private void btnEditRequests_Click(object sender, EventArgs e)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            string lvUsername = lvRequests.CheckedItems[0].SubItems[0].Text.ToString();                         //get username of student to edit
            int studentID = (from row in db.Users where row.Username == lvUsername select row.ID).First();      //get ID of student to edit

            CreateStudent g = new CreateStudent(2, studentID);                              //open create student form in edit mode on the student ID to edit
            g.Show();
            this.Dispose();
        }

        private void btnRejectRequests_Click(object sender, EventArgs e)
        //reject the selected student account requests
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            List<int> idList = new List<int>();

            for (int i = 0; i < lvRequests.CheckedItems.Count; i++)             //go through all the checked requests
            {
                string username = lvRequests.CheckedItems[i].SubItems[0].Text.ToString();                                   //get the username of the checked request
                idList.Add((from row in db.Users.AsEnumerable() where row.Username == username select row.ID).First());     //get the id and add it to a list
            }

            for (int c = 0; c < idList.Count(); c++)
            {
                List<TutorRequest> tutorRequest = (from row in db.TutorRequests.AsEnumerable() where row.ID == idList[c] select row).ToList();      //get all the pending tutor requests
                for (int h = 0; h < tutorRequest.Count(); h++)                                                                                      //remove all the requests
                {
                    db.TutorRequests.DeleteObject(tutorRequest[h]);
                    db.SaveChanges();
                }

                Student student = (from row in db.Students.AsEnumerable() where row.ID == idList[c] select row).First();                            //delete the pending student account
                db.Students.DeleteObject(student);
                db.SaveChanges();

                User user = (from row in db.Users.AsEnumerable() where row.ID == idList[c] select row).First();                                     //delete the pending user account
                db.Users.DeleteObject(user);
                db.SaveChanges();
            }

            lvStudent.Items.Clear();                                                                                                                //reload the listviews
            lvRequests.Items.Clear();
            setupStudentLV();
        }

        private void btnAcceptRequest_Click(object sender, EventArgs e)
        //accept the selected student account requests
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            List<User> userList = new List<User>();

            for (int i = 0; i < lvRequests.CheckedItems.Count; i++)             //go through all the checked requests
            {
                string username = lvRequests.CheckedItems[i].SubItems[0].Text.ToString();                                   //get the username of the checked request
                userList.Add((from row in db.Users.AsEnumerable() where row.Username == username select row).First());      //get the user and add it to a list
            }

            for (int c = 0; c < userList.Count(); c++)
            {
                userList[c].Username = userList[c].Username.Substring(0, userList[c].Username.Length - 1);                  //take the ? off the username
            }
            db.SaveChanges();

            lvStudent.Items.Clear();                            //reload the listviews
            lvRequests.Items.Clear();
            setupStudentLV();
        }

        //-----------------------------------------------------------------------------------------------------------------
        //functions for faculty
        private void btnFacultyAdd_Click(object sender, EventArgs e)
        //adds a faculty member to the database
        {
            string fname = txtFirstname.Text;                   //get the information for the new faculty member
            string lname = txtLastname.Text;
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string phone = txtPhoneNumber.Text;
            string email = txtEmail.Text;
            string department = combDepartments.Text;
            string accounttype = "Faculty";

            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();

            if (string.IsNullOrEmpty(fname) || string.IsNullOrWhiteSpace(lname) ||                                      //check to make sure they have filled out the form appropriately
                string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) ||
                    department == "Department...")  
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
                TutorMaster.User newUser = new TutorMaster.User();              //create the user object
                newUser.ID = getNextID();
                newUser.FirstName = fname;
                newUser.LastName = lname;
                newUser.Username = username;
                newUser.Password = password;
                newUser.PhoneNumber = phone;
                newUser.Email = email;
                newUser.AccountType = accounttype;
                addUser(newUser);                                               //add the user object

                TutorMaster.Faculty newFaculty = new TutorMaster.Faculty();     //create the faculty object
                newFaculty.ID = newUser.ID;
                newFaculty.Department = department;
                addFaculty(newFaculty);                                         //add the faculty object

                List<string> classCodes = (from row in db.Classes where row.Department.Equals(department) select row.ClassCode).ToList();
                foreach (string cc in classCodes)
                {
                    TutorMaster.FacultyClass fc = new TutorMaster.FacultyClass();
                    fc.Key = getNextFacultyClassID().ToString();
                    fc.ClassCode = cc;
                    fc.FacultyID = newUser.ID;

                    db.AddToFacultyClasses(fc);
                    db.SaveChanges();
                }

                txtFirstname.Text = "";                                         //reset the faculty form
                txtLastname.Text = "";
                txtUsername.Text = "";
                txtPassword.Text = "";
                txtPhoneNumber.Text = "";
                txtEmail.Text = "";
                combDepartments.SelectedItem = combDepartments.Items[0];
               
                MessageBox.Show("Faculty has been added to the database");
            }

            lvFaculty.Clear();                                      //reload the faculty listviews
            setupFacultyLV();
        }

        private void btnFacultyDelete_Click(object sender, EventArgs e)
        //deletes a faculty member from the database
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            int facNum = lvFaculty.CheckedItems.Count;
            for (int i = 0; i < facNum; i++)
            {
                string username = lvFaculty.CheckedItems[i].SubItems[0].Text;                           //get the username of the faculty to delete
                User delU = (from row in db.Users where row.Username == username select row).First();   //get the user to delete
                db.Users.DeleteObject(delU);                                                            //delete user from database
                db.SaveChanges();
            }

            lvFaculty.Clear();                          //reload the listviews
            setupFacultyLV();
        }

        private void btnFacultyEdit_Click(object sender, EventArgs e)
        {
            setEditFacultyControls();                                       //set the edit buttons up
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            string username = lvFaculty.CheckedItems[0].SubItems[0].Text;   //get the username
            var fac = (from row in db.Users where row.Username == username select row).First();     //get the user account database object

            txtFirstname.Text = fac.FirstName;                          //set the form fields to the faculty information
            txtLastname.Text = fac.LastName;
            txtUsername.Text = username;
            txtPassword.Text = fac.Password;
            txtPhoneNumber.Text = fac.PhoneNumber; 
            txtEmail.Text = fac.Email;
            lblID.Text = fac.ID.ToString();

            String department = (from row in db.Faculties where row.ID == fac.ID select row.Department).First();
            combDepartments.SelectedItem = department;                  //set the department to the faculty's current department
        }

        private void btnFacSave_Click(object sender, EventArgs e)
        //editing a faculty member in the database
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            Int32 id = Int32.Parse(lblID.Text.ToString());

            var fac = (from r in db.Users where r.ID == id select r).First();               //get the faculty and faculty user object
            var facU = (from r in db.Faculties where r.ID == id select r).First();

            string fname = txtFirstname.Text;
            string lname = txtLastname.Text;
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string phone = txtPhoneNumber.Text;
            string email = txtEmail.Text;
            string department = combDepartments.Text;

            if (string.IsNullOrEmpty(fname) || string.IsNullOrWhiteSpace(lname) ||                          //check to make sure they have filled out the form appropriately
                string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) ||
                    department == "Department...")
            {
                MessageBox.Show("Please fill in all of the textboxes with the approriate information");
            }
            else if (!validateInfo(email, phone))
            {
                MessageBox.Show("Please put in a valid email address and phone number");
            }
            else if (!uniqueUsername(username) && !(username == fac.Username))
            {
                MessageBox.Show("Username is already taken. Please pick another one.");
            }
            else
            {
                fac.FirstName = fname;                                      //reset the object's fields to be what the admin entered into the form
                fac.LastName = lname;
                fac.Username = username;
                fac.Password = password;
                fac.PhoneNumber = phone;
                fac.Email = email;
                facU.Department = department;

                txtFirstname.Text = "";                                     //reset the form fields
                txtLastname.Text = "";
                txtUsername.Text = "";
                txtPassword.Text = "";
                txtPhoneNumber.Text = "";
                txtEmail.Text = "";
                lblID.Text = "";

                db.SaveChanges();

                unsetEditFacultyControls();                                 //reset the form comtrols
            }

        }

        private void btnFacCancel_Click(object sender, EventArgs e)
        //cancels the edit of a faculty member
        {
            unsetEditFacultyControls();         //return to the normal faculty controls

            txtFirstname.Text = "";             //reset the fields
            txtLastname.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtPhoneNumber.Text = "";
            txtEmail.Text = "";
            lblID.Text = "";
        }

        //-----------------------------------------------------------------------------------------------------------------
        //functions for classes
        private void sortByCoursePrefix(ref List<Class> cs)
        {
            List<string[]> tempList = new List<string[]>();

            for (int i = 0; i < cs.Count(); i++)
            {
                string[] temp = cs[i].ClassCode.ToString().Split('-');
                tempList.Add(temp);
            }

            for (int i = tempList.Count(); i >= 0; i--)
            {
                for (int j = 0; j < tempList.Count() - 1; j++)
                {
                    int k = j + 1;
                    if (string.Compare(tempList[j][0], tempList[k][0]) > 0)
                    {
                        string[] temp = tempList[j];
                        tempList[j] = tempList[k];
                        tempList[k] = temp;

                        Class tempClass = cs[j];
                        cs[j] = cs[k];
                        cs[k] = tempClass;
                    }
                }
            }
        }

        private void sortByNumber(ref List<Class> classList)
        {
            List<int> tempList = new List<int>();

            for (int i = 0; i < classList.Count(); i++)
            {
                int temp = Convert.ToInt16(classList[i].ClassCode.ToString().Split('-')[1]);
                tempList.Add(temp);
            }

            for (int i = tempList.Count(); i >= 0; i--)
            {
                for (int j = 0; j < tempList.Count() - 1; j++)
                {
                    int k = j + 1;
                    if (tempList[j] > tempList[k])
                    {
                        int temp = tempList[j];
                        tempList[j] = tempList[k];
                        tempList[k] = temp;

                        Class tempClass = classList[j];
                        classList[j] = classList[k];
                        classList[k] = tempClass;
                    }
                }
            }
        }

        private void sortByNumAndAdd(List<Class> cs)
        {
            Class init = cs[0];
            List<Class> classList = new List<Class>();
            classList.Add(init);

            for (int i = 1; i < cs.Count(); i++)
            {
                if (init.ClassCode.ToString().Split('-')[0] == cs[i].ClassCode.ToString().Split('-')[0])
                {
                    classList.Add(cs[i]);
                }
                else
                {
                    sortByNumber(ref classList);
                    foreach (Class c in classList)
                    {
                        lvClass.Items.Add(new ListViewItem(new string[] { c.ClassCode, c.ClassName, c.Department }));
                    }

                    classList.Clear();
                    init = cs[i];
                    classList.Add(cs[i]);
                }
            }

            sortByNumber(ref classList);
            foreach (Class c in classList)
            {
                lvClass.Items.Add(new ListViewItem(new string[] { c.ClassCode, c.ClassName, c.Department }));
            }
        }

        private void btnClassDelete_Click(object sender, EventArgs e)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();

            int clNum = lvClass.CheckedItems.Count;
            for (int i = 0; i < clNum; i++)
            {
                string classCode = lvClass.CheckedItems[i].SubItems[0].Text;
                Class delC = (from row in db.Classes where row.ClassCode == classCode select row).First();
                deleteCourseCommits(delC.ClassCode);                    //delete commitments connected to class
                db.Classes.DeleteObject(delC);                          //delete selected class
                db.SaveChanges();
            }

            lvClass.Clear();                //reload class listviews
            setupClassLV();
        }

        private void deleteCourseCommits(string courseCode)
        //delete the commits connected to a deleted course
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            List<Commitment> cmtList = (from row in db.Commitments where row.Class == courseCode select row).ToList();      //get the commits in a list

            for (int p = 0; p < cmtList.Count; p++)             //set the commits to open status
            {
                if (cmtList[p].Class == courseCode)
                {
                    cmtList[p].ID = -1;
                    cmtList[p].Location = "-";
                    cmtList[p].Open = true;
                    cmtList[p].Class = "-";
                    cmtList[p].Tutoring = false;
                }
            }
            db.SaveChanges();
        }

        private void btnClassAdd_Click(object sender, EventArgs e)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();

            string classCode = txtClassCode.Text.ToString();                    //get info from form
            string className = txtClassName.Text.ToString();
            int classSelection = combDepartmentsAdd.SelectedIndex;

            if (classSelection == combDepartmentsAdd.Items.Count - 1)           //if the department isn't Add Department
            {
                string classDepartment = txtDepartment.Text.ToString();                                 //set the class department to the user entered one
                if (string.IsNullOrEmpty(classCode) || string.IsNullOrEmpty(className)                              //check to make sure the form is filled out appropriately
                    || string.IsNullOrEmpty(classDepartment))
                {
                    MessageBox.Show("Please fill in all of the textboxes with the approriate information");
                }
                else if (!uniqueClassCode(classCode))
                {
                    MessageBox.Show("Class Code is already taken. Please pick another one.");
                }
                else if (!uniqueClassName(className))
                {
                    MessageBox.Show("Class Name is already taken. Please pick another one.");
                }
                else if (combDepartmentsAdd.Text.ToString() == "Department...")
                {
                    MessageBox.Show("Please select an academic department for the course.");
                }
                else if (!txtClassCode.Text.ToString().Contains('-'))
                {
                    MessageBox.Show("Please input a valid course number.");
                }
                else
                {
                    TutorMaster.Class newClass = new TutorMaster.Class();                   //create the class object
                    newClass.ClassCode = classCode;
                    newClass.ClassName = className;
                    newClass.Department = classDepartment;
                    addClass(newClass);                                                     //add the class to the database

                    List<int> faculty = (from row in db.Faculties where row.Department == classDepartment select row.ID).ToList();
                    foreach (int id in faculty)
                    {
                        TutorMaster.FacultyClass fc = new TutorMaster.FacultyClass();
                        fc.Key = getNextFacultyClassID().ToString();
                        fc.ClassCode = classCode;
                        fc.FacultyID = id;
                        db.AddToFacultyClasses(fc);
                        db.SaveChanges();
                    }

                    txtClassCode.Text = "";                                                 //reset the form fields
                    txtClassName.Text = "";
                    combDepartmentsAdd.SelectedIndex = 0;
                    txtDepartment.Hide();
                    lblDepartment.Hide();

                    MessageBox.Show("Class has been added to the database");

                    setupDepartmentBoxes();                                                 //reset the department box
                }
            }
            else
            {
                string classDepartment = combDepartmentsAdd.Items[classSelection].ToString();                       //if the department selected is an existing one
                if (string.IsNullOrEmpty(classCode) || string.IsNullOrEmpty(className)                              //check to make sure the form is filled out appropriately
                    || string.IsNullOrEmpty(classDepartment))
                {
                    MessageBox.Show("Please fill in all of the textboxes with the approriate information");
                }
                else if (!uniqueClassCode(classCode))
                {
                    MessageBox.Show("Class Code is already taken. Please pick another one.");
                }
                else if (!uniqueClassName(className))
                {
                    MessageBox.Show("Class Name is already taken. Please pick another one.");
                }
                else if (combDepartmentsAdd.Text.ToString() == "Department...")
                {
                    MessageBox.Show("Please select an academic department for the course.");
                }
                else if (!txtClassCode.Text.ToString().Contains('-'))
                {
                    MessageBox.Show("Please input a valid course number.");
                }
                else
                {
                    TutorMaster.Class newClass = new TutorMaster.Class();               //create new class object
                    newClass.ClassCode = classCode;
                    newClass.ClassName = className;
                    newClass.Department = classDepartment;
                    addClass(newClass);                                                 //add class to database

                    List<int> faculty = (from row in db.Faculties where row.Department == classDepartment select row.ID).ToList();
                    foreach (int id in faculty)
                    {
                        TutorMaster.FacultyClass fc = new TutorMaster.FacultyClass();
                        fc.Key = getNextFacultyClassID().ToString();
                        fc.ClassCode = classCode;
                        fc.FacultyID = id;
                        db.AddToFacultyClasses(fc);
                        db.SaveChanges();
                    }

                    txtClassCode.Text = "";                                             //reset the form
                    txtClassName.Text = "";
                    combDepartmentsAdd.SelectedIndex = 0;
                    txtDepartment.Hide();
                    lblDepartment.Hide();

                    MessageBox.Show("Class has been added to the database");
                }
            }

            lvClass.Clear();                //reload the class lsitview
            setupClassLV();
        }

        private void btnClassEdit_Click(object sender, EventArgs e)
        {
            combDepartmentsAdd.Text = lvClass.CheckedItems[0].SubItems[2].Text.ToString();
            combDepartmentsAdd.Enabled = false;
            setEditClassControls();                 //set controls for editing a class
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            string classCode = lvClass.CheckedItems[0].SubItems[0].Text;                                //get the classcode of the selected class
            var cl = (from row in db.Classes where row.ClassCode == classCode select row).First();      //get the class object from the database

            txtClassCode.Text = cl.ClassCode.ToString();                    //set fields in class form to the selected class's
            txtClassCode.Enabled = false;
            txtClassName.Text = cl.ClassName.ToString();
            //combDepartmentsAdd.SelectedText = cl.Department.ToString();
            lblID.Text = classCode;

        }

        private void btnClassSave_Click(object sender, EventArgs e)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            String code = lblID.Text.ToString();                //get the previous code from the hidden label

            var cla = (from r in db.Classes where r.ClassCode == code select r).First();

            string classCode = txtClassCode.Text.ToString();                    //get info from form
            string className = txtClassName.Text.ToString();
            int classSelection = combDepartmentsAdd.SelectedIndex;

            if (classSelection == combDepartmentsAdd.Items.Count - 1)           //if the department isn't Add Department
            {
                string classDepartment = txtDepartment.Text.ToString();                                 //set the class department to the user entered one
                if (string.IsNullOrEmpty(classCode) || string.IsNullOrEmpty(className)                              //check to make sure the form is filled out appropriately
                    || string.IsNullOrEmpty(classDepartment))
                {
                    MessageBox.Show("Please fill in all of the textboxes with the approriate information");
                }
                else if (!uniqueClassCode(classCode) && !(classCode == cla.ClassCode))
                {
                    MessageBox.Show("Class Code is already taken. Please pick another one.");
                }
                else if (!uniqueClassName(className) && !(className == cla.ClassName))
                {
                    MessageBox.Show("Class Name is already taken. Please pick another one.");
                }
                else
                {
                    cla.ClassCode = classCode;                          //set class object's field to info from form
                    cla.ClassName = className;
                    cla.Department = classDepartment;

                    txtClassCode.Text = "";                             //reset form
                    txtClassName.Text = "";
                    combDepartmentsAdd.SelectedIndex = 0;
                    txtDepartment.Hide();
                    lblDepartment.Hide();

                    MessageBox.Show("Class has been edited in the database");

                    setupDepartmentBoxes();

                    db.SaveChanges();
                    txtClassCode.Enabled = true;
                    unsetEditClassControls();
                }
            }
            else
            {
                string classDepartment = combDepartmentsAdd.Items[classSelection].ToString();
                if (string.IsNullOrEmpty(classCode) || string.IsNullOrEmpty(className)
                    || classDepartment == "Department...")
                {
                    MessageBox.Show("Please fill in all of the textboxes with the approriate information");
                }
                else if (!uniqueClassCode(classCode) && !(classCode == cla.ClassCode))
                {
                    MessageBox.Show("Class Code is already taken. Please pick another one.");
                    if (!(classCode == cla.ClassCode))
                    {
                        MessageBox.Show("AHH");
                    }
                }
                else if (!uniqueClassName(className) && !(className == cla.ClassName))
                {
                    MessageBox.Show("Class Name is already taken. Please pick another one.");
                }
                else
                {
                    cla.ClassCode = classCode;
                    cla.ClassName = className;
                    cla.Department = classDepartment;

                    txtClassCode.Text = "";
                    txtClassName.Text = "";
                    combDepartmentsAdd.SelectedIndex = 0;
                    txtDepartment.Hide();
                    lblDepartment.Hide();

                    txtClassCode.Enabled = true;
                    MessageBox.Show("Class has been edited in the database");

                    db.SaveChanges();

                    unsetEditClassControls();
                }
            }

            lvClass.Clear();
            setupClassLV();
        }

        private void btnClassCancel_Click(object sender, EventArgs e)
        {
            unsetEditClassControls();                   //reset class form
            
            txtClassCode.Enabled = true;
            txtClassCode.Text = "";
            txtClassName.Text = "";
            combDepartmentsAdd.SelectedIndex = 0;
            txtDepartment.Hide();
            lblDepartment.Hide();
        }

        //-----------------------------------------------------------------------------------------------------------------
        //functions for adding faculty or classes to the database
        private void addUser(TutorMaster.User user)         //add user to database
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            db.Users.AddObject(user);
            db.SaveChanges();
        }

        private void addFaculty(TutorMaster.Faculty faculty)    //add faculty to database
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            db.Faculties.AddObject(faculty);
            db.SaveChanges();
        }

        private void addClass(TutorMaster.Class cl)             //add class to database
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            db.Classes.AddObject(cl);
            db.SaveChanges();
        }

        private bool validateInfo(string email, string phone)       //validate valid email
        {
            string address = email.Substring(email.Length - 4);
            if ((email.Contains('@')) && (phone.Length == 14) && (address == ".edu" || address == ".com"))
            {
                return true;
            }
            return false;
        }

        private bool uniqueUsername(string username)        //checks if username is in database (not case sensitive)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            return (!db.Users.Any(u => u.Username == username));
        }

        private bool uniqueClassCode(string classCode)      //checks if classcode is in database (not case sensitive)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            return (!db.Classes.Any(c => c.ClassCode == classCode));
        }

        private bool uniqueClassName(string className)      //checks if classname is in database (not case sensitive)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            return (!db.Classes.Any(c => c.ClassName == className));
        }

        private int getNextID()         //get unused ID from the database
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            int rowNum = db.Users.Count();

            var lastRow = db.Users.OrderByDescending(u => u.ID).Select(r => r.ID).First();
            return lastRow + 1;
        }

        private int getNextFacultyClassID()         //get unused ID from the database
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            List<string> idStrings = (from row in db.FacultyClasses select row.Key).ToList();
            List<int> ids = new List<int>();

            foreach (string s in idStrings)
            {
                ids.Add(Convert.ToInt32(s));
            }

            int lastRow = ids.Max();

            return lastRow + 1;
        }

        //-----------------------------------------------------------------------------------------------------------------
        //functions that adjust the controls on the form
        private void setupDepartmentBoxes()
        {
            combDepartments.Items.Clear();                              //clear the existing items
            combDepartmentsAdd.Items.Clear();

            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            HashSet<String> departments = new HashSet<String>((from r in db.Classes select r.Department).ToList());  //gets departments as a set to get rid of duplicates

            List<String> dList = departments.ToList();                  //sort the departments alphabetically
            dList.Sort();

            combDepartments.Items.Add("Department...");                 //add the label to the drop down boxes
            combDepartmentsAdd.Items.Add("Department...");
            combDepartments.SelectedIndex = 0;                          //set the selected item to be the label
            combDepartmentsAdd.SelectedIndex = 0;

            foreach (String d in dList)                                 //add all the departments to the dropdown boxrs
            {
                combDepartments.Items.Add(d);
                combDepartmentsAdd.Items.Add(d);
            }

            combDepartmentsAdd.Items.Add("New Department...");          //add an option for adding a department to the dropdown
            //on the classes tab

            combDepartments.DropDownStyle = ComboBoxStyle.DropDownList;         //set the comboboxes to have the style of a dropdown box
            combDepartmentsAdd.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void setupHiddenButtons()
        //stylize, set click functions, add them to the form, and hide them
        {
            btnFacSave.Left = 446;
            btnFacSave.Top = 256;
            btnFacSave.Width = 130;
            btnFacSave.Height = 23;
            btnFacSave.Text = "Save Changes";
            btnFacSave.Click += new EventHandler(btnFacSave_Click);

            btnFacCancel.Left = 446;
            btnFacCancel.Top = 287;
            btnFacCancel.Width = 130;
            btnFacCancel.Height = 23;
            btnFacCancel.Text = "Cancel Changes";
            btnFacCancel.Click += new EventHandler(btnFacCancel_Click);

            tabFaculty.Controls.Add(btnFacSave);
            tabFaculty.Controls.Add(btnFacCancel);

            btnClassSave.Left = 434;
            btnClassSave.Top = 237;
            btnClassSave.Width = 130;
            btnClassSave.Height = 23;
            btnClassSave.Text = "Save Changes";
            btnClassSave.Click += new EventHandler(btnClassSave_Click);

            btnClassCancel.Left = 434;
            btnClassCancel.Top = 266;
            btnClassCancel.Width = 130;
            btnClassCancel.Height = 23;
            btnClassCancel.Text = "Cancel Changes";
            btnClassCancel.Click += new EventHandler(btnClassCancel_Click);

            tabClasses.Controls.Add(btnClassSave);
            tabClasses.Controls.Add(btnClassCancel);

            btnFacSave.Hide();
            btnFacCancel.Hide();

            btnClassSave.Hide();
            btnClassCancel.Hide();
        }

        private void disableButtons()
        //disable the inactive buttons
        {
            btnDelete.Enabled = false;
            btnDelete.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
            btnEdit.Enabled = false;
            btnEdit.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
        }

        private void setEditFacultyControls()
        //set controls to edit ones
        {
            btnFacultyAdd.Hide();
            btnFacultyDelete.Hide();
            btnFacultyEdit.Hide();

            combDepartments.Text = lvFaculty.CheckedItems[0].SubItems[3].Text.ToString();
            combDepartments.Enabled = false;
            btnFacSave.Show();
            btnFacCancel.Show();
        }

        private void unsetEditFacultyControls()
        //return controls to normal faculty ones
        {

            btnFacultyAdd.Show();
            btnFacultyDelete.Show();
            btnFacultyEdit.Show();

            combDepartments.Enabled = true;
            combDepartments.Text = combDepartments.Items[0].ToString();

            btnFacSave.Hide();
            btnFacCancel.Hide();
        }

        private void setEditClassControls()
        //set the controls to edit a class
        {
            btnClassAdd.Hide();
            btnClassDelete.Hide();
            btnClassEdit.Hide();

            combDepartmentsAdd.Enabled = false;
            btnClassSave.Show();
            btnClassCancel.Show();
        }

        private void unsetEditClassControls()
        //return controls to normal class ones
        {
            btnClassAdd.Show();
            btnClassDelete.Show();
            btnClassEdit.Show();
            
            combDepartmentsAdd.Enabled = true;
            btnClassSave.Hide();
            btnClassCancel.Hide();
        }

        private void combDepartmentsAdd_DropDownClosed(object sender, EventArgs e)
        //check if add department is selected
        {
            //show controls to add a department if Add Department is selected
            if (combDepartmentsAdd.SelectedIndex == combDepartmentsAdd.Items.Count - 1)
            {
                txtDepartment.Show();
                lblDepartment.Show();
            }
            else
            {
                txtDepartment.Hide();
                lblDepartment.Hide();
            }
        }

        private void lvRequests_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            //enable or disable certain buttons based on the number of items checked
            if (lvRequests.CheckedItems.Count == 0)
            {
                btnAcceptRequest.Enabled = false;
                btnAcceptRequest.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
                btnEditRequests.Enabled = false;
                btnEditRequests.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
                btnRejectRequests.Enabled = false;
                btnRejectRequests.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
            }
            else if (lvRequests.CheckedItems.Count == 1)
            {
                btnAcceptRequest.Enabled = true;
                btnAcceptRequest.BackColor = System.Drawing.Color.FromArgb(226, 226, 226);
                btnEditRequests.Enabled = true;
                btnEditRequests.BackColor = System.Drawing.Color.FromArgb(226, 226, 226);
                btnRejectRequests.Enabled = true;
                btnRejectRequests.BackColor = System.Drawing.Color.FromArgb(226, 226, 226);
            }
            else if (lvRequests.CheckedItems.Count > 1)
            {
                btnAcceptRequest.Enabled = true;
                btnAcceptRequest.BackColor = System.Drawing.Color.FromArgb(226, 226, 226);
                btnEditRequests.Enabled = false;
                btnEditRequests.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
                btnRejectRequests.Enabled = true;
                btnRejectRequests.BackColor = System.Drawing.Color.FromArgb(226, 226, 226);
            }

        }

        private void tabAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {
            unsetEditFacultyControls();                                         //reset controls if tab changes
            unsetEditClassControls();           
            setupDepartmentBoxes();

            if (lvStudent.CheckedItems.Count > 0)                               //reset listview checked items if tab changes
            {
                foreach (ListViewItem listItem in lvStudent.Items)
                {
                    listItem.Checked = false;
                }
            }

            if (lvFaculty.CheckedItems.Count > 0)
            {
                foreach (ListViewItem listItem in lvFaculty.Items)
                {
                    listItem.Checked = false;
                }
            } 
            
            if (lvClass.CheckedItems.Count > 0)
            {
                foreach (ListViewItem listItem in lvClass.Items)
                {
                    listItem.Checked = false;
                }
            }
        }
    }
}
