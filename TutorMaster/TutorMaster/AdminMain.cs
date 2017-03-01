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

        private void setupStudentLV()
        {
            lvStudent.CheckBoxes = true;
            lvStudent.Columns.Add("     Username", 100);
            lvStudent.Columns.Add("Last Name", 100);
            lvStudent.Columns.Add("First Name", 100);
            lvStudent.Columns.Add("Tutor", 50);
            lvStudent.Columns.Add("Tutee", 50);
            lvStudent.Columns.Add("Email", 100);
            lvStudent.Columns.Add("Phone Number", 100);

            TutorMasterDBEntities2 db = new TutorMasterDBEntities2();
            var students = from c in db.Students select c;
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
                    tutee = "No";
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
            Login g = new Login();
            g.Show();
            this.Close();
        }

        private void AdminMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            //System.Windows.Forms.Application.Exit();
        }

        private void lvStudent_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            int itemsChecked = lvStudent.CheckedItems.Count;
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
    }
}
