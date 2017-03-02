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
            loadFormInfo(accID);
            setupClasses();

        }

        private void setupClasses()
        {
            tvClasses.CheckBoxes = true;

            TutorMasterDBEntities2 db = new TutorMasterDBEntities2();
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
                TutorMasterDBEntities2 db = new TutorMasterDBEntities2();
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

                AdminMain g = new AdminMain();
                g.Show();
                this.Close();
            }

        }

        private void loadFormInfo(int accID)
        {
            TutorMasterDBEntities2 db = new TutorMasterDBEntities2();
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
            this.Close();
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
            TutorMasterDBEntities2 db = new TutorMasterDBEntities2();
            var numReq = db.TutorRequests.Count(x => x.ID == accID); //(from row in db.TutorRequests group row.ID by row.ID into rowsByValue where rowsByValue.Count > (accID-1) && rowsByValue.Count < (accID+1)
            //var numReqList = numReq.ToList();
            //var countsWithTotal = numReqList.Concat(new[] { numReqList.Sum() });
            MessageBox.Show(Convert.ToString(numReq));

            
        }
    }
}
