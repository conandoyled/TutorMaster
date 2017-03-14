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
        public AdminMain()
        {
            InitializeComponent();

            setupStudentLV();
            disableButtons();
        }

        private void setupStudentLV() //This is what populates the box of students
        {
            lvStudent.CheckBoxes = true;
            lvStudent.Columns.Add("     Username", 100);// This first block of commands sets up the top row.
            lvStudent.Columns.Add("Last Name", 100);
            lvStudent.Columns.Add("First Name", 100);
            lvStudent.Columns.Add("Tutor", 50);
            lvStudent.Columns.Add("Tutee", 50);
            lvStudent.Columns.Add("Email", 100);
            lvStudent.Columns.Add("Phone Number", 100);

            TutorMasterDBEntities4 db = new TutorMasterDBEntities4(); //create a new indirect entity
            var students = from c in db.Students select c; // c is arbitay thing to pull. from var in tabletopullfrom select  
            List<Student> stus = new List<Student>();
            stus = students.ToList();

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
                lvStudent.Items.Add(new ListViewItem(new string[] { user.Username, user.LastName, user.FirstName, tutor, tutee, user.Email, user.PhoneNumber }));
            }
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

        private void AdminMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            //System.Windows.Forms.Application.Exit();
        }

        private void lvStudent_ItemChecked(object sender, ItemCheckedEventArgs e) //This function determines when certain buttons should be activated or deativated
        {
            int itemsChecked = lvStudent.CheckedItems.Count; // .CheckedItems.Count tells how many things in the list box are clicked
            if (itemsChecked == 1)
            {
                btnEdit.Enabled = true;
            }
            else
            {
                btnEdit.Enabled = false;
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

        private void lvStudent_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
