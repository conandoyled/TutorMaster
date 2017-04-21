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
        //Initial Set up
        private int ACCID;
        private bool leftside;
        private bool Auto = false;
        public AdvancedRequest(int accID)  
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
            combTutorAvailability.Hide();
            lblHowLong.Hide();
            combMeetingLength.Hide();
            cbxWeekly.Hide();
            label2.Hide();
            btnManualTime.Hide();
            btnSendRequest.Hide();
            combClassBoxLeft.Hide();
            btnFindMatches.Hide();
            label3.Hide();

            //initialize the tutor names and list of classes
            setupTutorList();           //populate combTutorName
            setupComboClassesRight(); //populate the right combo box with all the available classes
            dayStartDateTime.Value = DateTime.Today;

        }
        private void setupTutorList() //add extra validation to this to prevent a double slotted student from seeing themselves
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
        private void AdvancedRequest_Load(object sender, EventArgs e)
        {

        }

        //Left Side
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

            //Set the bool for later
            leftside = true;
        }

        private void setupComboClassesLeft()
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();

            string[] Tutorname = combTutorNameLeft.Text.Split(); //Get the Tutor's ID
            string fname = Tutorname[0];
            string lname = Tutorname[1];
            int TutID = (from row in db.Users where row.FirstName == fname && row.LastName == lname select row.ID).First();
            
            var Classes = (from row in db.StudentClasses where row.ID == TutID select row.Class); // pull out all the classes that are being tutored by the particular student [including duplicates]
           
            List<Class> ListOfClasses = Classes.ToList<Class>(); //put all of those classes into a list to manipuate
            foreach (Class SC in ListOfClasses)
            {
                combClassBoxLeft.Items.Add(SC.ClassName);
            }
        }

        private void combTutorNameLeft_SelectedIndexChanged(object sender, EventArgs e)
        {
            //show the appropriate objects 
            combClassBoxLeft.Show();
            lblClassesAvailable.Show();
            combMeetingLength.Show();
            lblHowLong.Show();
            cbxWeekly.Show();
            btnFindMatches.Show();
            label3.Show();
            combClassBoxLeft.Items.Clear();         //clears the class box
            setupComboClassesLeft();    //fills it with the appropriate classes
            
        }

        private void combClassBoxLeft_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //Right Side 
        private void btnByClass_Click(object sender, EventArgs e)
        {
            //Hide left side of form
            lblTutorName.Hide();
            lblClassesAvailable.Hide();
            lblAvailableTimes.Hide();
            combTutorNameLeft.Hide();
            combTutorAvailability.Hide();
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

            //Set Left for use in matching
            leftside = false;
        }

        private void setupComboClassesRight()
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            var Classes = (from row in db.StudentClasses select row.Class); // pull out all the classes that are being tutored [including duplicates]
            List<Class> ListOfClasses = Classes.ToList<Class>(); //put all of those classes into a list to manipuate
            foreach (Class SC in ListOfClasses)
            {
                combClassBoxRight.Items.Add(SC.ClassName);
            }
        }

        private void btnExit_Click(object sender, EventArgs e) 
        {
            StudentMain g = new StudentMain(ACCID);
            g.Show();
            this.Close();
        }

        private void combClassBoxRight_SelectedIndexChanged(object sender, EventArgs e)
        {
            //1. Show and clear all the appropriate items
            label1.Show();
            label3.Show();
            combTutorNameRight.Show();
            combTutorNameRight.Items.Clear();
            combMeetingLength.Show();
            lblHowLong.Show();
            cbxWeekly.Show();
            btnFindMatches.Show();

            //This populates the available tutors for the selected class
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();

            //Create a list of all the Users that tutor the class that was selected
            List<User> ListOfTutors =
                (from row in db.StudentClasses
                 where row.Class.ClassName == combClassBoxRight.Text
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

        //Middle
        private void btnFindMatches_Click(object sender, EventArgs e)
        {
            bool g2 = false;

            //Validate
            if (leftside && combClassBoxLeft.SelectedItem != null && combTutorNameLeft.SelectedItem != null && combMeetingLength.SelectedItem != null) //If we're looking at the left side and everything is filled in
            {
                g2 = true;
            }

            if (!leftside && combClassBoxRight.SelectedItem != null && combTutorNameRight.SelectedItem != null && combMeetingLength.SelectedItem != null) //If we're looking at the right side and everything is filled in
            {
                g2 = true;
            }


            if (g2) //If we're good to go then we move onto the matching process
            {
                //show the appropriate boxes
                lblAvailableTimes.Show();
                combTutorAvailability.Show();
                btnSendRequest.Show();
                label2.Show();
                btnManualTime.Show();

                if (leftside)
                    MatchTimes(combTutorNameLeft, combClassBoxLeft); //match the left
                if (!leftside)
                    MatchTimes(combTutorNameRight, combClassBoxRight); //call the match for the right side
            }
            else
            {
                DialogResult choice = MessageBox.Show("Please select a tutor, class, and a session length."); 
            }
        }

        private void combMeetingLength_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        //Match Function and it's helpers
        private void MatchTimes(ComboBox TutBox, ComboBox ClsBox)
        {

            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            combTutorAvailability.Items.Clear(); //clear the box so that it is 
            //0. Info needed to match

            //a. Name of Tutor, use it to find their ID
            string[] Tutorname = TutBox.Text.Split();
            string fname = Tutorname[0];
            string lname = Tutorname[1];
            int TutID = (from row in db.Users where row.FirstName == fname && row.LastName == lname select row.ID).First();

            //b. Length of tutoring session in minutes
            int length = (int.Parse(combMeetingLength.Text))/15;

            //c. whether it is weekly
            bool weekly = cbxWeekly.Checked;

            //d. tutee ID (ACCID), use to match the availiblity
            int TuteeID = ACCID;


            DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

            string NameOfClassToBeTutored = ClsBox.Text;

            List<Commitment> tuteeCommits = (from stucmt in db.StudentCommitments.AsEnumerable()    // create a list of tutee's commitments
                                                where stucmt.ID == TuteeID
                                                join cmt in db.Commitments.AsEnumerable() on stucmt.CmtID equals cmt.CmtID
                                                select cmt).ToList();

            QuickSort(ref tuteeCommits, tuteeCommits.Count()); //sort the tutee commits so that they are in chronological order

            checkMax(ref tuteeCommits);

            removeNotOpens(ref tuteeCommits, start, weekly); //remove all the things that are not open

            if (tuteeCommits.Count == 0) //If the tuttee doesn't have any compatible availibility then give a pop up box to let them know
            {
                MessageBox.Show("You currently have no available slots, please add some availability before attempting to schedule a session of this length");
            }
            else
            {
                List<string> tuteeValidSlots = getValidSlots(ref tuteeCommits, length);//this is returning 0 for some reason

                List<TutorMaster.Commitment> tutorCommits = (from stucmt in db.StudentCommitments.AsEnumerable() //This creates a list of all the tutor commitments. 
                                                                where stucmt.ID == TutID
                                                                join cmt in db.Commitments.AsEnumerable() on stucmt.CmtID equals cmt.CmtID
                                                                select cmt).ToList();

                QuickSort(ref tutorCommits, tutorCommits.Count()); //sort it so we can use it
                checkMax(ref tutorCommits); 
                removeNotOpens(ref tutorCommits, start, weekly); //remove the occupied commitments

                List<string> tutorValidSlots = getValidSlots(ref tutorCommits, length); //create a list of all the valid tutor slots
  
                for (int j = 0; j < tutorValidSlots.Count(); j++) //iterate through all the available tutor slots 
                {
                    if (BinarySearch(tuteeValidSlots, tutorValidSlots[j]))
                    {
                        //instead of a dailog box add them to the combo box for TutAvail
                        string MatchedTime = tutorValidSlots[j].Split(',')[0] + " - " + tutorValidSlots[j].Split(',')[1]; //this string is the matched time
                        combTutorAvailability.Items.Add(MatchedTime);


                        /*DialogResult choice = MessageBox.Show("You have been matched with " + tutorFirstName + " " + tutorLastName +
                            " for a time at: " + tutorValidSlots[j].Split(',')[0] + " - " + tutorValidSlots[j].Split(',')[1], "You've got a match!", MessageBoxButtons.YesNo);
                        if (choice == DialogResult.Yes)
                        {
                            int tutorId = Convert.ToInt32(approvedTutorIds[i]);
                            int tuteeId = Convert.ToInt32(id);
                            addCommits(tutorValidSlots[j], tutorId, tuteeId, tutorCommits, tuteeCommits, classCode, db, weekly, sessionLength);
                            done = true;
                            break;
                        }*/
                    }
                }
            }
        }

        private bool isOpen(TutorMaster.Commitment commit)
        {
            return (commit.Class == "-" && commit.Location == "-" && commit.Open == true && commit.Tutoring == false && commit.ID == -1);
        }

        private void removeNotOpens(ref List<TutorMaster.Commitment> cmtList, DateTime start, bool weekly)
        {
            if (weekly)
            {
                for (int i = 0; i < cmtList.Count() - 1; i++)
                {
                    if (!isOpen(cmtList[i]) || DateTime.Compare(start, Convert.ToDateTime(cmtList[i].StartTime)) > 0 || cmtList[i].Weekly != weekly)
                    {
                        cmtList.Remove(cmtList[i]);
                        i--;
                    }
                }
            }
            else
            {
                for (int i = 0; i < cmtList.Count() - 1; i++)
                {
                    if (!isOpen(cmtList[i]) || DateTime.Compare(start, Convert.ToDateTime(cmtList[i].StartTime)) > 0)
                    {
                        cmtList.Remove(cmtList[i]);
                        i--;
                    }
                }
            }
        }

        private List<string> getValidSlots(ref List<TutorMaster.Commitment> cmtList, int sessionLength)
        {
            int consecutiveCommits = 0;

            List<string> validSlots = new List<string>();   //creates a list to be filled with calid slots and returned
            TutorMaster.Commitment initialCommit = cmtList[0];  //
            DateTime startDate = Convert.ToDateTime(cmtList[0].StartTime);
            DateTime endDate = Convert.ToDateTime(cmtList[0].StartTime).AddMinutes(15);

            consecutiveCommits++;

            if (sessionLength == 1)
            {
                for (int i = 0; i < cmtList.Count() - 1; i++)
                {
                    DateTime start = Convert.ToDateTime(cmtList[i].StartTime);
                    DateTime end = Convert.ToDateTime(cmtList[i].StartTime).AddMinutes(15);
                    validSlots.Add(start.ToString() + "," + end.ToString());
                }
            }
            else
            {
                for (int i = 0; i < cmtList.Count() - 1; i++)
                {
                    DateTime currentCommitDate = Convert.ToDateTime(cmtList[i].StartTime);                   //get datetime of commitment we are on in loop
                    DateTime nextCommitDate = Convert.ToDateTime(cmtList[i + 1].StartTime);                  //get datetime of commitment ahead of it

                    if (DateTime.Compare(nextCommitDate, currentCommitDate.AddMinutes(15)) == 0 && consecutiveCommits < sessionLength) //if our next commit is 15 minutes later of our current
                    {
                        consecutiveCommits++;
                        endDate = Convert.ToDateTime(cmtList[i].StartTime).AddMinutes(15);
                    }
                    else if (DateTime.Compare(nextCommitDate, currentCommitDate.AddMinutes(15)) == 0 && consecutiveCommits >= sessionLength)
                    {
                        endDate = Convert.ToDateTime(cmtList[i].StartTime).AddMinutes(15);
                        validSlots.Add(startDate.ToString() + "," + endDate.ToString());
                        startDate = startDate.AddMinutes(15);
                    }
                    else if (DateTime.Compare(nextCommitDate, currentCommitDate.AddMinutes(15)) != 0 && consecutiveCommits >= sessionLength)
                    {
                        endDate = Convert.ToDateTime(cmtList[i].StartTime).AddMinutes(15);
                        validSlots.Add(startDate.ToString() + "," + endDate.ToString());

                        //update our carries
                        consecutiveCommits = 1;
                        startDate = Convert.ToDateTime(cmtList[i + 1].StartTime);
                        endDate = Convert.ToDateTime(cmtList[i + 1].StartTime).AddMinutes(15);
                        initialCommit = cmtList[i + 1];
                    }
                    else if (DateTime.Compare(nextCommitDate, currentCommitDate.AddMinutes(15)) != 0 && consecutiveCommits < sessionLength)
                    {
                        consecutiveCommits = 1;
                        startDate = Convert.ToDateTime(cmtList[i + 1].StartTime);
                        endDate = Convert.ToDateTime(cmtList[i + 1].StartTime).AddMinutes(15);
                        initialCommit = cmtList[i + 1];
                    }
                }
                //i believe i have the algorithm to catch the very last 15 minute time block in a person's schedule here. I've tested the first if statement and it seems to work. I don't know about the second one
                //but the second one makes sense to me and I know I need a statement like it. the second statement is in case we are just one short in our block and the last commit is what's needed to get the valid slot
                if (consecutiveCommits >= sessionLength && DateTime.Compare(Convert.ToDateTime(cmtList[cmtList.Count() - 2].StartTime).AddMinutes(15), Convert.ToDateTime(cmtList[cmtList.Count() - 1].StartTime)) == 0)
                {
                    startDate = startDate.AddMinutes(15);
                    endDate = endDate.AddMinutes(15);
                    validSlots.Add(startDate.ToString() + "," + endDate.ToString());
                }
                else if (consecutiveCommits == sessionLength - 1 && DateTime.Compare(Convert.ToDateTime(cmtList[cmtList.Count() - 2].StartTime).AddMinutes(15), Convert.ToDateTime(cmtList[cmtList.Count() - 1].StartTime)) == 0)
                {
                    endDate = endDate.AddMinutes(15);
                    validSlots.Add(startDate.ToString() + "," + endDate.ToString());
                }
            }
            return validSlots;
        }

        private bool sameCategory(TutorMaster.Commitment commitFirst, TutorMaster.Commitment commitSecond)      //ask if the 15 minute time block of the first has the same values as the second
        {
            return (commitFirst.Class == commitSecond.Class && commitFirst.Location == commitSecond.Location
                    && commitFirst.Open == commitSecond.Open && commitFirst.Weekly == commitSecond.Weekly
                    && commitFirst.ID == commitSecond.ID);
        }

        private void checkMax(ref List<TutorMaster.Commitment> cmtList)
        {
            int consec = 1;

            for (int i = 0; i < cmtList.Count() - 1; i++)
            {
                DateTime currentCommit = Convert.ToDateTime(cmtList[i].StartTime);
                DateTime nextCommit = Convert.ToDateTime(cmtList[i + 1].StartTime);

                if (consec > 11)
                {
                    MessageBox.Show(cmtList[i + 1].StartTime.ToString());
                    cmtList.Remove(cmtList[i + 1]);
                    i--;
                    consec = 1;
                }
                if (DateTime.Compare(currentCommit.AddMinutes(15), nextCommit) == 0 && sameCategory(cmtList[i], cmtList[i + 1]) && !isOpen(cmtList[i]))
                {
                    consec++;
                }
                else
                {
                    consec = 1;
                }
            }
        }

        private DateTime getStartTime(string slot)
        {
            string startDateTime = slot.Split(',')[0];
            string startDate = startDateTime.Split(' ')[0];
            string startTime = startDateTime.Split(' ')[1];
            string amPm = startDateTime.Split(' ')[2];

            int month = Convert.ToInt32(startDate.Split('/')[0]);
            int day = Convert.ToInt32(startDate.Split('/')[1]);
            int year = Convert.ToInt32(startDate.Split('/')[2]);

            int hour = Convert.ToInt32(startTime.Split(':')[0]);
            int min = Convert.ToInt32(startTime.Split(':')[1]);


            if (hour < 12 && amPm == "PM")
            {
                hour += 12;
            }
            else if (hour == 12 && amPm == "AM")
            {
                hour = 0;
            }
            DateTime date = new DateTime(year, month, day, hour, min, 0);
            return date;
        }

        private DateTime getEndTime(string slot)
        {
            string startDateTime = slot.Split(',')[1];
            string startDate = startDateTime.Split(' ')[0];
            string startTime = startDateTime.Split(' ')[1];
            string amPm = startDateTime.Split(' ')[2];

            int month = Convert.ToInt32(startDate.Split('/')[0]);
            int day = Convert.ToInt32(startDate.Split('/')[1]);
            int year = Convert.ToInt32(startDate.Split('/')[2]);

            int hour = Convert.ToInt32(startTime.Split(':')[0]);
            int min = Convert.ToInt32(startTime.Split(':')[1]);


            if (hour < 12 && amPm == "PM")
            {
                hour += 12;
            }
            else if (hour == 12 && amPm == "AM")
            {
                hour = 0;
            }

            DateTime date = new DateTime(year, month, day, hour, min, 0);
            return date;
        }

        private bool BinarySearch(List<string> cmtList, string commit)
        {
            bool found = false;
            int first = 0;
            int last = cmtList.Count() - 1;
            while (first <= last && !found)
            {
                int midpoint = (first + last) / 2;
                if (DateTime.Compare(getStartTime(cmtList[midpoint]), getStartTime(commit)) == 0)
                {
                    found = true;
                    return found;
                }
                else
                {
                    if (DateTime.Compare(getStartTime(commit), getStartTime(cmtList[midpoint])) < 0)
                    {
                        last = midpoint - 1;
                    }
                    else
                    {
                        first = midpoint + 1;
                    }
                }
            }
            return found;
        }

        //QuickSort functions
        private void Split(ref List<TutorMaster.Commitment> values, int first, int last, ref int splitPoint)
        {
            int center = (first + last) / 2;
            int median = 0;
            DateTime valueF = Convert.ToDateTime(values[first].StartTime);
            DateTime valueC = Convert.ToDateTime(values[center].StartTime);
            DateTime valueL = Convert.ToDateTime(values[last].StartTime);

            if ((DateTime.Compare(valueF, valueC) >= 0 && DateTime.Compare(valueF, valueL) <= 0) ||
                (DateTime.Compare(valueF, valueL) >= 0 && DateTime.Compare(valueF, valueL) <= 0))
            {
                median = first;
            }
            else if (DateTime.Compare(valueC, valueF) >= 0 && (DateTime.Compare(valueC, valueL) <= 0) ||
                   (DateTime.Compare(valueC, valueF)) >= 0 && (DateTime.Compare(valueC, valueL) <= 0))
            {
                median = center;
            }
            else
            {
                median = last;
            }
            //Swap the median and first committments in the list
            TutorMaster.Commitment temp = values[first];
            values[first] = values[median];
            values[median] = temp;

            valueF = Convert.ToDateTime(values[first].StartTime); //get current first datetime
            valueC = Convert.ToDateTime(values[center].StartTime);//get current center datetime;
            valueL = Convert.ToDateTime(values[last].StartTime);

            TutorMaster.Commitment splitVal = values[first];
            DateTime splitDate = Convert.ToDateTime(values[first].StartTime);

            int saveFirst = first;
            bool onCorrectSide = true;

            first++;
            valueF = Convert.ToDateTime(values[first].StartTime);
            do
            {
                onCorrectSide = true;
                while (onCorrectSide)
                {
                    if (DateTime.Compare(valueF, splitDate) > 0)
                    {
                        onCorrectSide = false;
                    }
                    else
                    {
                        first++;
                        valueF = Convert.ToDateTime(values[first].StartTime);
                        onCorrectSide = (first <= last);
                    }
                }

                onCorrectSide = (first <= last);
                while (onCorrectSide)
                {
                    if (DateTime.Compare(valueL, splitDate) <= 0)
                    {
                        onCorrectSide = false;
                    }
                    else
                    {
                        last--;
                        valueL = Convert.ToDateTime(values[last].StartTime);
                        onCorrectSide = (first <= last);
                    }
                }

                if (first < last)
                {
                    TutorMaster.Commitment temp2 = values[first];
                    values[first] = values[last];
                    values[last] = temp2;
                    first++;
                    last--;

                    valueF = Convert.ToDateTime(values[first].StartTime);
                    valueL = Convert.ToDateTime(values[last].StartTime);
                }
            } while (first <= last);

            splitPoint = last;
            TutorMaster.Commitment temp3 = values[saveFirst];
            values[saveFirst] = values[splitPoint];
            values[splitPoint] = temp3;
        }

        private void QuickSort2(ref List<TutorMaster.Commitment> values, int first, int last)
        {
            if (first < last)
            {
                int splitPoint = -99;

                Split(ref values, first, last, ref splitPoint);
                QuickSort2(ref values, first, splitPoint - 1);
                QuickSort2(ref values, splitPoint + 1, last);
            }
        }

        private void QuickSort(ref List<TutorMaster.Commitment> values, int numValues)
        {
            QuickSort2(ref values, 0, numValues - 1);
        }

        private void btnManualTime_Click(object sender, EventArgs e)
        {
            //Show everything
            Auto = false;
            dayStartDateTime.Show();
            combStartAmPm.Show();
            combStartHour.Show();
            combStartMinute.Show();

        }
    }
}

