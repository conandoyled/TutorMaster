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

            var ApprovableClasses = (from row in db.FacultyClasses where row.FacultyID == accID select row.ClassCode); //This should pull out all the classes that the faculty member can approve tutors for
            List<String> AC= new List<String>(); //create a list to store the DB info that was just pulled
            AC= ApprovableClasses.ToList<String>(); //Fill the list
            
            var PendingRequests = (from row in db.TutorRequests select row); //This pulls out the whole table of pending requests
            List<TutorRequest> PC = new List<TutorRequest>(); //create a list to store the DB info that was just pulled
            PC = PendingRequests.ToList<TutorRequest>(); //Fill the list

            //1.5 Create an empty list to fill with the right requests in step 2
            List<TutorRequest> FC = new List<TutorRequest>(); //here is that beautiful empty list that will be filled with the tutor requests that should be displayed

            //2. Look at all pending requests and see if they are for a class from the faculty members department. If they are, add them to the box
            
            foreach(String classcode in AC) //Iterate through all of the class  codes and check them
            {
                foreach (TutorRequest tut in PC) // Iterate through all of the requests
                {
                    if (classcode == tut.ClassCode) // If the class code that we are checking matches the class code of the object,
                    {
                        FC.Add(tut); // Then add that tutor request to the list of requests to display
                    }
                }
            }

            //3. Now that all the requests are in the FC, we need to display each of the tutor requests in clbPendingReequests in the form of [STUDENT NAME, CLASS TO TUTOR]
            //3.5 I have to go through the student table and pull out the names of the people in it
            
            var students = from c in db.Users select c; // This creates a list of Users that I can use to pull information from 
            List<User> stus = new List<User>();
            stus = students.ToList(); //The list 'stus' includes all of the users from the users db

            List<string> Names=new List<string>(); //The list Names is empty, but will be used to contain all the Names that correspond to the proper requests
            string Name = "";

            foreach (TutorRequest req in FC)//iterate through all the requests to display
            {
                foreach (User stu in stus)//iterate through all the users [probably not the most effecient way to do this]
                {
                    if (req.ID == stu.ID) //When you find the matching ID
                    {
                        Name = stu.FirstName + " " +stu.LastName; //Create the name string
                        Names.Add(Name); //Add the Name to the list of names 
                    }
                }
            }

            //4. Now that all the data has been prepared, display it properly
            for(int i =0; i<FC.Count();i++) //loop through the parrallel lists
            {
                clbPendingRequests.Items.Add(Names.ElementAt(i)+" "+FC.ElementAt(i).ClassCode); //add the items as a string into the box!
            }
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
