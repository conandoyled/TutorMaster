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
        Button btnFacSave = new Button();
        Button btnFacCancel = new Button();
        Button btnClassSave = new Button();
        Button btnClassCancel = new Button();

        public AdminMain()
        {
            InitializeComponent(); 

            setupStudentLV();
            setupFacultyLV();
            setupClassLV();
            setupDepartmentBoxes();
            setupHiddenButtons();
            disableButtons();
        }

        private void AdminMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            //System.Windows.Forms.Application.Exit();
        }

        private void setupStudentLV() //This is what populates the box of students
        {
            lvStudent.CheckBoxes = true;
            lvStudent.Columns.Add("     Username", 110);// This first block of commands sets up the top row.
            lvStudent.Columns.Add("Last Name", 100);
            lvStudent.Columns.Add("First Name", 100);
            lvStudent.Columns.Add("Tutor", 50);
            lvStudent.Columns.Add("Tutee", 50);
            lvStudent.Columns.Add("Email", 100);
            lvStudent.Columns.Add("Phone Number", 100);

            lvRequests.CheckBoxes = true;
            lvRequests.Columns.Add("     Username", 110);// This first block of commands sets up the top row.
            lvRequests.Columns.Add("Last Name", 100);
            lvRequests.Columns.Add("First Name", 100);
            lvRequests.Columns.Add("Tutor", 50);
            lvRequests.Columns.Add("Tutee", 50);
            lvRequests.Columns.Add("Email", 100);
            lvRequests.Columns.Add("Phone Number", 100);

            TutorMasterDBEntities4 db = new TutorMasterDBEntities4(); //create a new indirect entity
            var students = from c in db.Students select c; // c is arbitay thing to pull. from var in tabletopullfrom select 
            
            List<Student> stus = new List<Student>();
            stus = students.ToList();

            int newUsers = 0;
            foreach(Student stu in stus)
            {
                string tutee = "No";
                string tutor = "Yes";
                var user = (from row in db.Users where row.ID == stu.ID select row).First();
                if ((bool)stu.Tutee)
                    tutee = "Yes";
                else
                    tutee = "No";
                if ((bool)stu.Tutor)
                    tutor = "Yes";
                else
                    tutor = "No";

                if (user.Username.Contains('?'))
                {
                    newUsers++;
                    lvRequests.Items.Add(new ListViewItem(new string[] { user.Username, user.LastName, user.FirstName, tutor, tutee, user.Email, user.PhoneNumber }));
                }
                else
                {
                    lvStudent.Items.Add(new ListViewItem(new string[] { user.Username, user.LastName, user.FirstName, tutor, tutee, user.Email, user.PhoneNumber }));
                }
            }

            if (newUsers > 0)
            {
                MessageBox.Show("There are " + newUsers.ToString() + " student(s) that requesting to become tutor(s) or tutee(s)");
            }

            
        }

        private void setupFacultyLV() //This is what populates the box of students
        {
            lvFaculty.CheckBoxes = true;
            lvFaculty.Columns.Add("     Username", 105);// This first block of commands sets up the top row.
            lvFaculty.Columns.Add("Last Name", 100);
            lvFaculty.Columns.Add("First Name", 100);
            lvFaculty.Columns.Add("Department", 100);

            TutorMasterDBEntities4 db = new TutorMasterDBEntities4(); //create a new indirect entity
            var facultys = from c in db.Faculties select c; // c is arbitay thing to pull. from var in tabletopullfrom select  
            List<Faculty> fs = new List<Faculty>();
            fs = facultys.ToList();

            foreach (Faculty f in fs)
            {
                var user = (from row in db.Users where row.ID == f.ID select row).First();
  
                lvFaculty.Items.Add(new ListViewItem(new string[] { user.Username, user.LastName, user.FirstName, f.Department }));
            }
        }

        private void setupClassLV() //This is what populates the box of students
        {
            lvClass.CheckBoxes = true;
            lvClass.Columns.Add("     Class Code", 100);// This first block of commands sets up the top row.
            lvClass.Columns.Add("Class Name", 150);
            lvClass.Columns.Add("Department", 100);

            TutorMasterDBEntities4 db = new TutorMasterDBEntities4(); //create a new indirect entity
            var classes = from c in db.Classes select c; // c is arbitay thing to pull. from var in tabletopullfrom select  
            List<Class> cs = new List<Class>();
            cs = classes.ToList();

            foreach (Class c in cs)
            {
                lvClass.Items.Add(new ListViewItem(new string[] { c.ClassCode, c.ClassName, c.Department }));
            }
        }

        private void setupDepartmentBoxes()
        {
            combDepartments.Items.Clear();
            combDepartmentsAdd.Items.Clear();

            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            var classes = from r in db.Classes select r;
            HashSet<String> departments = new HashSet<String>();

            foreach(Class c in classes)
            {
                departments.Add(c.Department);
            }

            List<String> dList = departments.ToList();
            dList.Sort();

            combDepartments.Items.Add("Department...");
            combDepartmentsAdd.Items.Add("Department...");
            combDepartments.SelectedIndex = 0;
            combDepartmentsAdd.SelectedIndex = 0;

            foreach (String d in dList)
            {
                combDepartments.Items.Add(d);
                combDepartmentsAdd.Items.Add(d);
            }

            combDepartmentsAdd.Items.Add("New Department...");

            combDepartments.DropDownStyle = ComboBoxStyle.DropDownList;
            combDepartmentsAdd.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void setupHiddenButtons()
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
        {
            btnDelete.Enabled = false;
            btnEdit.Enabled = false;
        }

        private void btnCreateStudent_Click(object sender, EventArgs e)
        {
            CreateStudent g = new CreateStudent();
            g.Show();
            this.Close();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Login g = new Login(); //Are we going to create problems by create new loginb boxes on top of the hidden ones we already have?
            g.Show();
            this.Close();
        }

        private void btnFacSave_Click(object sender, EventArgs e)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            Int32 id = Int32.Parse(lblID.Text.ToString());

            var fac = (from r in db.Users where r.ID == id select r).First();
            var facU = (from r in db.Faculties where r.ID == id select r).First();

            string fname = txtFirstname.Text;
            string lname = txtLastname.Text;
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string phone = txtPhoneNumber.Text;
            string email = txtEmail.Text;
            string department = combDepartments.Text;

            if (string.IsNullOrEmpty(fname) || string.IsNullOrWhiteSpace(lname) ||
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
                fac.FirstName = fname;
                fac.LastName = lname;
                fac.Username = username;
                fac.Password = password;
                fac.PhoneNumber = phone;
                fac.Email = email;
                facU.Department = department;

                txtFirstname.Text = "";
                txtLastname.Text = "";
                txtUsername.Text = "";
                txtPassword.Text = "";
                txtPhoneNumber.Text = "";
                txtEmail.Text = "";
                lblID.Text = "";

                db.SaveChanges();

                unsetEditFacultyControls();
                
            }

        }

        private void btnFacCancel_Click(object sender, EventArgs e)
        {
            unsetEditFacultyControls();

            txtFirstname.Text = "";
            txtLastname.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtPhoneNumber.Text = "";
            txtEmail.Text = "";
            lblID.Text = "";
        }

        private void btnClassSave_Click(object sender, EventArgs e)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            String code = lblID.Text.ToString();

            var cla = (from r in db.Classes where r.ClassCode == code select r).First();

            string classCode = txtClassCode.Text.ToString();
            string className = txtClassName.Text.ToString();
            int classSelection = combDepartmentsAdd.SelectedIndex;

            if (classSelection == combDepartmentsAdd.Items.Count - 1)
            {
                string classDepartment = txtDepartment.Text.ToString();
                if (string.IsNullOrEmpty(classCode) || string.IsNullOrEmpty(className)
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
                    cla.ClassCode = classCode;
                    cla.ClassName = className;
                    cla.Department = classDepartment;

                    txtClassCode.Text = "";
                    txtClassName.Text = "";
                    combDepartmentsAdd.SelectedIndex = 0;
                    txtDepartment.Hide();
                    lblDepartment.Hide();

                    MessageBox.Show("Class has been edited in the database");

                    setupDepartmentBoxes();

                    db.SaveChanges();

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
            unsetEditClassControls();

            txtClassCode.Text = "";
            txtClassName.Text = "";
            combDepartmentsAdd.SelectedIndex = 0;
            txtDepartment.Hide();
            lblDepartment.Hide();
        }

        private void lvStudent_ItemChecked(object sender, ItemCheckedEventArgs e) //This function determines when certain buttons should be activated or deativated
        {
            int itemsChecked = lvStudent.CheckedItems.Count; // .CheckedItems.Count tells how many things in the list box are clicked
            if (itemsChecked == 1)
            {
                btnEdit.Enabled = true;
                btnStudentSchedule.Enabled = true;
            }
            else
            {
                btnEdit.Enabled = false;
                btnStudentSchedule.Enabled = false;
            }
            if (itemsChecked > 0)
            {
                btnDelete.Enabled = true;
            }
            else
            {
                btnDelete.Enabled = false;
            }
        }

        private void lvFaculty_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            int itemsChecked = lvFaculty.CheckedItems.Count; // .CheckedItems.Count tells how many things in the list box are clicked
            if (itemsChecked == 1)
            {
                btnFacultyEdit.Enabled = true;
            }
            else
            {
                btnFacultyEdit.Enabled = false;
            }
            if (itemsChecked > 0)
            {
                btnFacultyDelete.Enabled = true;
            }
            else
            {
                btnFacultyDelete.Enabled = false;
            }
        }

        private void lvClass_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            int itemsChecked = lvClass.CheckedItems.Count; // .CheckedItems.Count tells how many things in the list box are clicked
            if (itemsChecked == 1)
            {
                btnClassEdit.Enabled = true;
            }
            else
            {
                btnClassEdit.Enabled = false;
            }
            if (itemsChecked > 0)
            {
                btnClassDelete.Enabled = true;
            }
            else
            {
                btnClassDelete.Enabled = false;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            string username = lvStudent.CheckedItems[0].Text.ToString();
            int studentID = (from row in db.Users where row.Username == username select row.ID).First();
            EditStudentForm g = new EditStudentForm(studentID);
            g.Show();
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            int stuNum = lvStudent.CheckedItems.Count;
            for (int i = 0; i < stuNum; i++)
            {
                string username = lvStudent.CheckedItems[i].SubItems[0].Text;
                User delU = (from row in db.Users where row.Username == username select row).First();
                db.Users.DeleteObject(delU);
                db.SaveChanges();
            }

            lvStudent.Clear();
            setupStudentLV();
        }

        private void btnFacultyAdd_Click(object sender, EventArgs e)
        {
            string fname = txtFirstname.Text;
            string lname = txtLastname.Text;
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string phone = txtPhoneNumber.Text;
            string email = txtEmail.Text;
            string department = combDepartments.Text;
            string accounttype = "Faculty";
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();

            if (string.IsNullOrEmpty(fname) || string.IsNullOrWhiteSpace(lname) ||
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

                TutorMaster.Faculty newFaculty = new TutorMaster.Faculty();
                newFaculty.ID = newUser.ID;
                newFaculty.Department = department;
                addFaculty(newFaculty);

                txtFirstname.Text = "";
                txtLastname.Text = "";
                txtUsername.Text = "";
                txtPassword.Text = "";
                txtPhoneNumber.Text = "";
                txtEmail.Text = "";
                combDepartments.SelectedItem = combDepartments.Items[0];
               
                MessageBox.Show("Faculty has been added to the database");
            }

            lvFaculty.Clear();
            setupFacultyLV();
        }

        private void btnFacultyDelete_Click(object sender, EventArgs e)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            int facNum = lvFaculty.CheckedItems.Count;
            for (int i = 0; i < facNum; i++)
            {
                string username = lvFaculty.CheckedItems[i].SubItems[0].Text;
                User delU = (from row in db.Users where row.Username == username select row).First();
                db.Users.DeleteObject(delU);
                db.SaveChanges();
            }

            lvFaculty.Clear();
            setupFacultyLV();
        }

        private void btnFacultyEdit_Click(object sender, EventArgs e)
        {
            setEditFacultyControls();
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            string username = lvFaculty.CheckedItems[0].SubItems[0].Text;
            var fac = (from row in db.Users where row.Username == username select row).First();

            txtFirstname.Text = fac.FirstName;
            txtLastname.Text = fac.LastName;
            txtUsername.Text = username;
            txtPassword.Text = fac.Password;
            txtPhoneNumber.Text = fac.PhoneNumber; 
            txtEmail.Text = fac.Email;
            lblID.Text = fac.ID.ToString();

            String department = (from row in db.Faculties where row.ID == fac.ID select row.Department).First();
            combDepartments.SelectedItem = department;

      
            //combDepartments.SelectedItem = combDepartments.Items[0];
                
        }

        private void btnClassDelete_Click(object sender, EventArgs e)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            int clNum = lvClass.CheckedItems.Count;
            for (int i = 0; i < clNum; i++)
            {
                string classCode = lvClass.CheckedItems[i].SubItems[0].Text;
                Class delC = (from row in db.Classes where row.ClassCode == classCode select row).First();
                db.Classes.DeleteObject(delC);
                db.SaveChanges();
            }

            lvClass.Clear();
            setupClassLV();
        }

        private void combDepartmentsAdd_DropDownClosed(object sender, EventArgs e)
        {
            if (combDepartmentsAdd.SelectedIndex == combDepartmentsAdd.Items.Count-1)
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

        private void btnClassAdd_Click(object sender, EventArgs e)
        {
            string classCode = txtClassCode.Text.ToString();
            string className = txtClassName.Text.ToString();
            int classSelection = combDepartmentsAdd.SelectedIndex;

            if (classSelection == combDepartmentsAdd.Items.Count - 1)
            {
                string classDepartment = txtDepartment.Text.ToString();
                if (string.IsNullOrEmpty(classCode) || string.IsNullOrEmpty(className)
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
                else
                {
                    TutorMaster.Class newClass = new TutorMaster.Class();
                    newClass.ClassCode = classCode;
                    newClass.ClassName = className;
                    newClass.Department = classDepartment;
                    addClass(newClass);

                    txtClassCode.Text = "";
                    txtClassName.Text = "";
                    combDepartmentsAdd.SelectedIndex = 0;
                    txtDepartment.Hide();
                    lblDepartment.Hide();

                    MessageBox.Show("Class has been added to the database");

                    setupDepartmentBoxes();
                }
            }
            else
            {
                string classDepartment = combDepartmentsAdd.Items[classSelection].ToString();
                if (string.IsNullOrEmpty(classCode) || string.IsNullOrEmpty(className)
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
                else
                {
                    TutorMaster.Class newClass = new TutorMaster.Class();
                    newClass.ClassCode = classCode;
                    newClass.ClassName = className;
                    newClass.Department = classDepartment;
                    addClass(newClass);

                    txtClassCode.Text = "";
                    txtClassName.Text = "";
                    combDepartmentsAdd.SelectedIndex = 0;
                    txtDepartment.Hide();
                    lblDepartment.Hide();

                    MessageBox.Show("Class has been added to the database");
                }
            }

            lvClass.Clear();
            setupClassLV();
        }

        private void btnClassEdit_Click(object sender, EventArgs e)
        {
            setEditClassControls();
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            string classCode = lvClass.CheckedItems[0].SubItems[0].Text;
            var cl = (from row in db.Classes where row.ClassCode == classCode select row).First();

            txtClassCode.Text = cl.ClassCode.ToString();
            txtClassName.Text = cl.ClassName.ToString();
            combDepartmentsAdd.SelectedText = cl.Department.ToString();
            lblID.Text = classCode;
        }

        private void btnStudentSchedule_Click(object sender, EventArgs e)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            string username = lvStudent.CheckedItems[0].Text.ToString();
            int studentID = (from row in db.Users where row.Username == username select row.ID).First();
            AdminSeeSchedule g = new AdminSeeSchedule(studentID);
            g.Show();
        }

        private void addUser(TutorMaster.User user)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            db.Users.AddObject(user);
            db.SaveChanges();
        }

        private void addFaculty(TutorMaster.Faculty faculty)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            db.Faculties.AddObject(faculty);
            db.SaveChanges();
        }

        private void addClass(TutorMaster.Class cl)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            db.Classes.AddObject(cl);
            db.SaveChanges();
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

        private bool uniqueClassCode(string classCode)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            return (!db.Classes.Any(c => c.ClassCode == classCode));
        }

        private bool uniqueClassName(string className)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            return (!db.Classes.Any(c => c.ClassName == className));
        }

        private int getNextID()
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            int rowNum = db.Users.Count();

            var lastRow = db.Users.OrderByDescending(u => u.ID).Select(r => r.ID).First();
            return lastRow + 1;
        }

        private void setEditFacultyControls()
        {
            btnFacultyAdd.Hide();
            btnFacultyDelete.Hide();
            btnFacultyEdit.Hide();

            btnFacSave.Show();
            btnFacCancel.Show();
        }

        private void unsetEditFacultyControls()
        {
            btnFacultyAdd.Show();
            btnFacultyDelete.Show();
            btnFacultyEdit.Show();

            btnFacSave.Hide();
            btnFacCancel.Hide();
        }

        private void setEditClassControls()
        {
            btnClassAdd.Hide();
            btnClassDelete.Hide();
            btnClassEdit.Hide();

            btnClassSave.Show();
            btnClassCancel.Show();
        }

        private void unsetEditClassControls()
        {
            btnClassAdd.Show();
            btnClassDelete.Show();
            btnClassEdit.Show();

            btnClassSave.Hide();
            btnClassCancel.Hide();
        }

        private void tabAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {
            unsetEditFacultyControls();
            unsetEditClassControls();
            setupDepartmentBoxes();

            if (lvStudent.CheckedItems.Count > 0)
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
            } if (lvClass.CheckedItems.Count > 0)
            {
                foreach (ListViewItem listItem in lvClass.Items)
                {
                    listItem.Checked = false;
                }
            } 
        }

        private void btnEditRequests_Click(object sender, EventArgs e)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            string lvUsername = lvRequests.CheckedItems[0].SubItems[0].Text.ToString();
            int studentID = (from row in db.Users where row.Username == lvUsername select row.ID).First();
            
            EditStudentForm g = new EditStudentForm(studentID);
            g.Show();
            this.Close();
        }

        private void lvRequests_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (lvRequests.CheckedItems.Count == 0)
            {
                btnAcceptRequest.Enabled = false;
                btnEditRequests.Enabled = false;
                btnRejectRequests.Enabled = false;
            }
            else if (lvRequests.CheckedItems.Count == 1)
            {
                btnAcceptRequest.Enabled = true;
                btnEditRequests.Enabled = true;
                btnRejectRequests.Enabled = true;
            }
            else if (lvRequests.CheckedItems.Count > 1)
            {
                btnAcceptRequest.Enabled = true;
                btnEditRequests.Enabled = false;
                btnRejectRequests.Enabled = true;
            }

        }

        private void btnRejectRequests_Click(object sender, EventArgs e)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            List<string> usernameList = new List<string>();
            List<int> idList = new List<int>();

            for (int i = 0; i < lvRequests.CheckedItems.Count; i++)
            {
                usernameList.Add(lvRequests.CheckedItems[i].SubItems[0].Text.ToString());
            }

            for (int j = 0; j < usernameList.Count(); j++)
            {
                idList.Add((from row in db.Users.AsEnumerable() where row.Username == usernameList[j] select row.ID).First());
            }

            for (int c = 0; c < idList.Count(); c++)
            {
                List<TutorRequest> tutorRequest = (from row in db.TutorRequests.AsEnumerable() where row.ID == idList[c] select row).ToList();
                for (int h = 0; h < tutorRequest.Count(); h++)
                {
                    db.TutorRequests.DeleteObject(tutorRequest[h]);
                    db.SaveChanges();
                }

                Student student = (from row in db.Students.AsEnumerable() where row.ID == idList[c] select row).First();
                db.Students.DeleteObject(student);
                db.SaveChanges();

                User user = (from row in db.Users.AsEnumerable() where row.ID == idList[c] select row).First();
                db.Users.DeleteObject(user);
                db.SaveChanges();
            }

            lvStudent.Items.Clear();
            lvRequests.Items.Clear();
            setupStudentLV();
        }

        private void btnAcceptRequest_Click(object sender, EventArgs e)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            List<string> usernameList = new List<string>();
            List<User> userList = new List<User>();

            for (int i = 0; i < lvRequests.CheckedItems.Count; i++)
            {
                usernameList.Add(lvRequests.CheckedItems[i].SubItems[0].Text.ToString());
            }

            for (int j = 0; j < usernameList.Count(); j++)
            {
                userList.Add((from row in db.Users.AsEnumerable() where row.Username == usernameList[j] select row).First());
            }

            for (int c = 0; c < userList.Count(); c++)
            {
                userList[c].Username = userList[c].Username.Substring(0, userList[c].Username.Length - 1);
            }
            db.SaveChanges();
            lvStudent.Items.Clear();
            lvRequests.Items.Clear();
            setupStudentLV();
        }
    }
}
