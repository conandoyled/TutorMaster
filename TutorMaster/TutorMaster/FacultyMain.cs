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
    public partial class FacultyMain : Form
    {
        private int id;
        
        public FacultyMain(int accID)
        {
            id = accID;
            InitializeComponent();
            SetupPendingRequests(id);//Populate box with requests, similar to Admin main
        }

        public void SetupPendingRequests(int accID) //This function will populate the checked list box with pending requests
        {
            //1. find which classes the faculty members are qualified to approve tutors for
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4(); //create a new indirect entity to look at db
            TutorMasterDBEntities4 dbt = new TutorMasterDBEntities4();
            var Classes = (from row in db.FacultyClasses where row.FacultyID == accID select row.ClassCode); //This should pull out all the classes that the faculty member can approve tutors for

            //2. Look at all pending requests and see if they are for a class from the faculty embers department
   
            foreach (var x in Classes)
            {;}//use if maybe

            
            //3. If they are, add them to the box
        }

        private void FacultyMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            //System.Windows.Forms.Application.Exit();
        }


        private void btnLogout_Click(object sender, EventArgs e)
        {
            Login g = new Login();
            g.Show();
            this.Close();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            //This will need to change each student account selected and then remove the pending requests from the DB
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            //This will need to remove all the requests from the DB and leave all acounts unchanged. Eventually, it will send a message to the admin account
        }

        private void clbPendingRequests_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
