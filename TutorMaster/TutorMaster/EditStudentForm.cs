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
    public partial class EditStudentForm : Form
    {
        private int accID;
        public EditStudentForm(int id)
        {
            InitializeComponent();
            accID = id;
            setupClasses();
            loadFormInfo(accID);

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


        private void btnSave_Click(object sender, EventArgs e)
        {
            string fname = txtFirstname.Text;
            string lname = txtLastname.Text;
            string username = txtUsername.Text;
            if (username.Contains('?'))
            {
                username = username.Substring(0, username.Length-1);
            }
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
                TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
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

        }

        private void loadFormInfo(int accID)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            txtFirstname.Text = (from row in db.Users where row.ID == accID select row.FirstName).First();
            txtLastname.Text = (from row in db.Users where row.ID == accID select row.LastName).First();
            txtUsername.Text = (from row in db.Users where row.ID == accID select row.Username).First();
            txtPassword.Text = (from row in db.Users where row.ID == accID select row.Password).First();
            txtPhoneNumber.Text = (from row in db.Users where row.ID == accID select row.PhoneNumber).First();
            txtEmail.Text = (from row in db.Users where row.ID == accID select row.Email).First();
            cbxTutor.Checked = Convert.ToBoolean((from row in db.Students where row.ID == accID select row.Tutor).First());
            cbxTutee.Checked = Convert.ToBoolean((from row in db.Students where row.ID == accID select row.Tutee).First());

            getClassRequests(accID);


        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            AdminMain g = new AdminMain();
            g.Show();
            this.Dispose();
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

        private void getClassRequests(int accID)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            //var numReq = db.TutorRequests.Count(x => x.ID == accID);
            var requestCodes = (from row in db.TutorRequests.AsEnumerable() where row.ID == accID select row.ClassCode).ToArray();
            
            int numCourses = requestCodes.Length;
            string[] requestClasses = new string[numCourses];
            for(int n = 0; n < numCourses; n++)
            {
                requestClasses[n] = (from row in db.Classes.AsEnumerable() where requestCodes[n] == row.ClassCode select row.ClassName).First();
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

        private void EditStudentForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Login g = new Login();
            g.Show();
            this.Dispose();
        }
    }
}
