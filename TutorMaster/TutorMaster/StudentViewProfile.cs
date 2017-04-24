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
    public partial class StudentViewProfile : Form
    {
        int accID; 

        public StudentViewProfile(int id)
        {
            InitializeComponent();

            accID = id;
            setInformation();
        }

        private void setInformation()
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();

            User u = (from row in db.Users where row.ID == accID select row).First();
            Student s = (from row in db.Students where row.ID == accID select row).First();

            lblFirstName.Text = u.FirstName;
            lblLastName.Text = u.LastName;
            lblUsername.Text = u.Username;
            lblPhone.Text = u.PhoneNumber;
            lblEmail.Text = u.Email;
            lblTutorTutee.Text = "";

            if ((bool)s.Tutee)
            {
                lblTutorTutee.Text += "Tutee\n";
            }
            if ((bool)s.Tutor)
            {
                lblTutorTutee.Text += "Tutor";

                this.Width += 280;
                imgLogo.Location = new Point(imgLogo.Location.X + 280, imgLogo.Location.Y);

                List<StudentClass> classes = (from row in db.StudentClasses where row.ID == accID select row).ToList();
                foreach (StudentClass c in classes)
                {
                    lvClasses.Items.Add(c.ClassCode);
                }

                List<TutorRequest> requests = (from row in db.TutorRequests where row.ID == accID select row).ToList();
                foreach (TutorRequest r in requests)
                {
                    lvRequests.Items.Add(r.ClassCode);
                }
            }
            else
            {
                lblClasses.Hide();
                lblRequests.Hide();

                lvClasses.Hide();
                lvRequests.Hide();
            }
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            ChangePasswordForm g = new ChangePasswordForm(accID);
            g.Show();

            this.Dispose();
        }

        private void StudentViewProfile_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
