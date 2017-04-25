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
            SetupPendingRequests(id);                                                                                       //Populate box with requests, similar to Admin main
            disableButtons();
        }

        public void SetupPendingRequests(int accID)                                                                         //This function will populate the checked list box with pending requests
        {

            lvPendingRequests.CheckBoxes = true;
            lvPendingRequests.Columns.Add("Student Name", 100);
            lvPendingRequests.Columns.Add("Class Code", 100);
            lvPendingRequests.Columns.Add("ID", 100);

            //1. find which classes the faculty members are qualified to approve tutors for
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();                                                       //create a new indirect entity to look at db

            var ApprovableClasses = (from row in db.FacultyClasses where row.FacultyID == accID select row.ClassCode);      //This should pull out all the classes that the faculty member can approve tutors for
            List<String> AC= new List<String>();                                                                            //create a list to store the DB info that was just pulled
            AC= ApprovableClasses.ToList<String>();                                                                         //Fill the list
            
            var PendingRequests = (from row in db.TutorRequests select row);                                                //This pulls out the whole table of pending requests
            List<TutorRequest> PC = new List<TutorRequest>();                                                               //create a list to store the DB info that was just pulled
            PC = PendingRequests.ToList<TutorRequest>();                                                                    //Fill the list
            for (int i = 0; i < PC.Count(); i++)
            {
                User user = (from row in db.Users.AsEnumerable() where PC[i].ID == row.ID select row).First();
                if (user.Username.Contains('?'))
                {
                    PC.Remove(PC[i]);
                    i--;
                }
            }

            //1.5 Create an empty list to fill with the right requests in step 2
            List<TutorRequest> FC = new List<TutorRequest>();                                                               //here is that beautiful empty list that will be filled with the tutor requests that should be displayed

            //2. Look at all pending requests and see if they are for a class from the faculty members department. If they are, add them to the box
            
            foreach(String classcode in AC)                                                                                 //Iterate through all of the class  codes and check them
            {
                foreach (TutorRequest tut in PC)                                                                            // Iterate through all of the requests
                {
                    if (classcode == tut.ClassCode)                                                                         // If the class code that we are checking matches the class code of the object,
                    {
                        FC.Add(tut);                                                                                        // Then add that tutor request to the list of requests to display
                    }
                }
            }

            //3. Now that all the requests are in the FC, we need to display each of the tutor requests in clbPendingReequests in the form of [STUDENT NAME, CLASS TO TUTOR]
            //3.5 I have to go through the student table and pull out the names of the people in it
            
            var students = from c in db.Users select c;                                                                     // This creates a list of Users that I can use to pull information from 
            List<User> stus = new List<User>();
            stus = students.ToList();                                                                                       //The list 'stus' includes all of the users from the users db

            List<string> Names=new List<string>();                                                                          //The list Names is empty, but will be used to contain all the Names that correspond to the proper requests
            string Name = "";

            foreach (TutorRequest req in FC)                                                                                //iterate through all the requests to display
            {
                foreach (User stu in stus)                                                                                  //iterate through all the users [probably not the most effecient way to do this]
                {
                    if (req.ID == stu.ID)                                                                                   //When you find the matching ID
                    {
                        Name = stu.FirstName + " " +stu.LastName;                                                           //Create the name string
                        Names.Add(Name);                                                                                    //Add the Name to the list of names 
                    }
                }
            }

            //4. Now that all the data has been prepared, display it properly
            for(int i =0; i<FC.Count();i++) //loop through the parrallel lists
            {
                lvPendingRequests.Items.Add(new ListViewItem(new string[] { Names.ElementAt(i), FC.ElementAt(i).ClassCode, FC.ElementAt(i).ID.ToString()})); //add the items as a string into the box!
            }
        }

        private void disableButtons()
        {
            btnAccept.Enabled = false;
            btnAccept.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
            btnReject.Enabled = false;
            btnReject.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
        }

        private void FacultyMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Login g = new Login();
            g.Show();
            this.Dispose();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Login g = new Login();
            g.Show();
            this.Dispose();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            //This will need to change each student account selected and then remove the pending requests from the DB
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();    //side note, we should carefully control the life cycle of object contexts like these   //open the db

            int reqNum = lvPendingRequests.CheckedItems.Count;                                                      //identify how many requests have been checked
            for (int i = 0; i < reqNum; i++)                                                                        //iterate for how many requests have been clicked
            {
                string CC = lvPendingRequests.CheckedItems[i].SubItems[1].Text;                                     //pull the classcode from the request in question
                string Id = lvPendingRequests.CheckedItems[i].SubItems[2].Text;
                int Id2 = Int32.Parse(Id);                                                                         //Change the string into an int, this is really fragile if a bad id number is put in the system somehow. typechecking for those ids should prevent it

                StudentClass newClass = new StudentClass();
                newClass.Key = getNextRequestKey();
                newClass.ClassCode = CC;
                newClass.ID = Id2;
                db.StudentClasses.AddObject(newClass);

                Student tutor = (from row in db.Students where row.ID==Id2 select row).First();
                tutor.Tutor = true;
                TutorRequest delU = (from row in db.TutorRequests where ((row.ClassCode == CC) && (row.ID == Id2)) select row).First(); //find the request that has the right ID and class code

                if (delU != null)
                {
                    db.TutorRequests.DeleteObject(delU);                                                                //delete the object in the db
                    db.SaveChanges();                                                                                   //save the cahnges to the db
                }
            }

            db.SaveChanges();
            MessageBox.Show("The selected tutor requests have been accepted and the tutors have been approved to tutor the selected courses");
            lvPendingRequests.Clear(); //clean out the box
            SetupPendingRequests(id); //Set up the box again
        }

        private void btnReject_Click(object sender, EventArgs e)                                                    //This Button now works fully! addition functionality would be eror checking for parse function and sending message to students and administrators once we set that up. 
        {
            //This will need to remove all the requests from the DB and leave all acounts unchanged. Eventually, it will send a message to the admin account
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();    //side note, we should carefully control the life cycle of object contexts like these                                           //open the db

            int reqNum = lvPendingRequests.CheckedItems.Count;                                                      //identify how many requests have been checked
            for (int i = 0; i < reqNum; i++)                                                                        //iterate for how many requests have been clicked
            {
                string CC = lvPendingRequests.CheckedItems[i].SubItems[1].Text;                                     //pull the classcode from the request in question
                string Id = lvPendingRequests.CheckedItems[i].SubItems[2].Text;
                int Id2 = Int32.Parse(Id);                                                                         //Change the string into an int


                //This line creates text objects that look like 'FIRST LAST CLASS-123 ID' you may want to split the string to get the class code, but you also need the student ID

                TutorRequest delU = (from row in db.TutorRequests where ((row.ClassCode == CC) && (row.ID == Id2)) select row).First(); //find the request that has the right ID and class code

                if (delU != null)
                {
                    db.TutorRequests.DeleteObject(delU);                                                                //delete the object in the db
                    db.SaveChanges();                                                                                   //save the cahnges to the db

                }
            }

            MessageBox.Show("The selected requests have been rejected and the selected tutors have not been approved for the requests courses.");
            lvPendingRequests.Clear(); //clean out the box
            SetupPendingRequests(id); //Set up the box again
        }

        private int getNextRequestKey()
        //gets unused key from database
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            int rowNum = db.StudentClasses.Count();
            int lastRow;

            if (rowNum > 0)
            {
                lastRow = db.StudentClasses.OrderByDescending(u => u.Key).Select(r => r.Key).First();
            }
            else
            {
                lastRow = 0;
            }
            return lastRow + 1;
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            ChangePasswordForm g = new ChangePasswordForm(id);
            g.Show();
            this.Dispose();
        }

        private void lvPendingRequests_ItemChecked(object sender, ItemCheckedEventArgs e)
        //ensures buttons are only enabled when the correct number of itemss are checked
        {
            if (lvPendingRequests.CheckedItems.Count > 0)
            {
                btnAccept.Enabled = true;
                btnAccept.BackColor = System.Drawing.Color.FromArgb(208, 222, 229);
                btnReject.Enabled = true;
                btnReject.BackColor = System.Drawing.Color.FromArgb(208, 222, 229);
            }
            else
            {
                disableButtons();
            }
        }
    }
}
