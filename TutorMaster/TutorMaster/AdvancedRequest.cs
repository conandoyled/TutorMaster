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
    public partial class AdvancedRequest : Form
    {
        private int ACCID;
        public AdvancedRequest(int accID)  //Get rid of all the references to tvClasses and set up the new comboboxjust like the other one
        {
            ACCID = accID;
            InitializeComponent();
            //hide everything so you can show it when appropriate
            lblAvailableTimes.Hide();
            lblClasses.Hide();
            lblClassesAvailable.Hide();
            lblTutorName.Hide();
            label1.Hide();
            combClassBoxRight.Hide();
            combTutorNameLeft.Hide();
            combTutorNameRight.Hide();
            lvTutorAvailability.Hide();
            lblHowLong.Hide();
            combMeetingLength.Hide();
            cbxWeekly.Hide();
            label2.Hide();
            btnManualTime.Hide();
            btnSendRequest.Hide();
            combClassBoxLeft.Hide();

            //initialize the tutor names and list of classes
            setupTutorList();           //populate combTutorName
            setupComboClasses(combClassBoxRight); //populate the right combo box with all the available classes

        }

        private void setupTutorList()
        {
            //1. pull all of the students that are available as tutors into a list
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();

            //1.1 Identify all the student IDs and put them in a list
            var T = (from row in db.Students where row.Tutor == true select row.ID);
            List<int> TutorID = new List<int>();
            TutorID = T.ToList<int>();
            
            //1.2 find all the tutors using the ids and put them in a list
            List<string> Tutors = new List<string>(); 
            foreach (User usrr in db.Users)
            {
                foreach (int i in TutorID)
                {
                    if (usrr.ID == i)
                        Tutors.Add(usrr.FirstName + ' ' + usrr.LastName);
                }
            }
   

            //2. set them up in the combo box 

            foreach (string name in Tutors)
            {
                combTutorNameLeft.Items.Add(name);
            }
        }
        /*
        private void setupTreeViewClasses(string TutorName)
        {
            tvClasses.CheckBoxes = true;

            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            string[] TName = TutorName.Split(' '); //split the name into 2 pieces
            string firstname= TName[0]; //get the first name
            string lastname=TName[1]; //get the last name
            
            //1. find the tutors ID
            int id = (from row in db.Users where ((firstname == row.FirstName) && (lastname == row.LastName)) select row.ID).First();

            //2. make a list of the classes they can tutor, turn those into class objects with join, then use them for the presentation set up
            //2.5 turn the list of student classes into a list of Class objects so you can present them

            List<Class> listofclasses =
                (from row in db.StudentClasses
                 where row.ID == id
                 join cl in db.Classes 
                 on row.ClassCode equals cl.ClassCode
                 select cl).ToList<Class>();

            //3. upload those classes to the display

            foreach (Class cl in listofclasses)
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
        */
       
        private void setupComboClasses(ComboBox combClassBox)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            var Classes = (from row in db.StudentClasses select row); // pull out all the classes that are being tutored [including duplicates]
            List<StudentClass> ListOfClasses = Classes.ToList<StudentClass>(); //put all of those classes into a list to manipuate
            foreach (StudentClass SC in ListOfClasses)
            {
                combClassBox.Items.Add(SC.ClassCode);
            }
        }

        private void combTutorNameLeft_SelectedIndexChanged(object sender, EventArgs e)
        {
            //show the appropriate objercts 
            combClassBoxLeft.Show();
            lblClassesAvailable.Show();
            combMeetingLength.Show();
            lblHowLong.Show();
            cbxWeekly.Show(); 

            combClassBoxLeft.Items.Clear();         //clears the class box
            setupComboClasses(combClassBoxLeft);    //fills it with the appropriate classes
            MatchTimes(); //set up the tutor time matches
        }

        private void btnExit_Click(object sender, EventArgs e) 
        {
            StudentMain g = new StudentMain(ACCID);
            g.Show();
            this.Close();
        }

        private void btnByTutor_Click(object sender, EventArgs e)
        {
            //hide the right side of the form
            combClassBoxRight.Hide();
            combTutorNameRight.Hide();
            lblClasses.Hide();
            label1.Hide();
            label2.Hide();
            btnManualTime.Hide();
            btnSendRequest.Hide();
            combMeetingLength.Hide();
            lblHowLong.Hide();
            cbxWeekly.Hide();

            //show lblTutorName and combTutorName
            lblTutorName.Show();
            combTutorNameLeft.Show();
        }

        private void btnByClass_Click(object sender, EventArgs e)
        {
            //Hide left side of form
            lblTutorName.Hide();
            lblClassesAvailable.Hide();
            lblAvailableTimes.Hide();
            combTutorNameLeft.Hide();
            lvTutorAvailability.Hide();
            label2.Hide();
            btnManualTime.Hide();
            btnSendRequest.Hide();
            combClassBoxLeft.Hide();
            lblClassesAvailable.Hide();
            combMeetingLength.Hide();
            lblHowLong.Hide();
            cbxWeekly.Hide();

            //Show Class Options
            lblClasses.Show();
            combClassBoxRight.Show();
        }

        private void combClassBoxRight_SelectedIndexChanged(object sender, EventArgs e)
        {
            //1. Show and clear all the appropriate items
            label1.Show();
            combTutorNameRight.Show();
            combTutorNameRight.Items.Clear();
            combMeetingLength.Show();
            lblHowLong.Show();
            cbxWeekly.Show();

            //This populates the available tutors for the selected class
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();

            //Create a list of all the Users that tutor the class that was selected
            List<User> ListOfTutors =
                (from row in db.StudentClasses
                 where row.ClassCode == combClassBoxRight.Text
                 join usr in db.Users 
                 on row.ID equals usr.ID
                 select usr).ToList<User>();

            //display them in the combo box
            foreach(User usr in ListOfTutors) 
            {
                combTutorNameRight.Items.Add(usr.FirstName+' '+usr.LastName);
            }
        }

        private void combTutorNameRight_SelectedIndexChanged(object sender, EventArgs e)
        {
            //call our tutor match function
        }

        private void MatchTimes()
        {
            //do the matching thing that myles and I talked about
            //try to meet with him tomorrow and talk to him about it all
        }

        private void AdvancedRequest_Load(object sender, EventArgs e)
        {

        }

        private void combClassBoxLeft_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
