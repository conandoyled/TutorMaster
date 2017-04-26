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
        private List<int> tutorIDs = new List<int>();
        
        public AdvancedRequest(int accID)  
        {
            ACCID = accID;
            InitializeComponent();

            hideControls();
            setupListView();
        }

        private void hideControls()
        {
            lblFirstChoice.Hide();
            combFirstChoice.Hide();
            lblHowLong.Hide();
            lblHour.Hide();
            lblMin.Hide();
            combStartHour.Hide();
            combStartMinute.Hide();
            cbxWeekly.Hide();
            lblSecondChoice.Hide();
            combSecondChoice.Hide();
            btnFindMatches.Hide();
            lblAvailableTimes.Hide();
            lvAvailableTimes.Hide();

            btnSendRequest.Enabled = false;
            btnSendRequest.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
        }

        private void setupListView()
        {
            lvAvailableTimes.CheckBoxes = true;
            lvAvailableTimes.Columns.Add("Start Time", 140);
            lvAvailableTimes.Columns.Add("End Time", 140);
        }

        //User selects tutor
        private void btnByTutor_Click(object sender, EventArgs e)
        {
            //hide the right side of the form
            hideControls();
            tutorIDs.Clear();
            combFirstChoice.Items.Clear();

            //show lblTutorName and combTutorName
            setupTutorList();

            lblFirstChoice.Text = "Tutor Name";
            lblSecondChoice.Text = "Classes Available";
            lblFirstChoice.Show();
            combFirstChoice.Show();
        }

        private void setupTutorList() //add extra validation to this to prevent a double slotted student from seeing themselves
        {
            //1. pull all of the students that are available as tutors into a list
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();

            //1.1 Identify all the tutors and put them in a list
            List<User> tutors = (from stu in db.Students
                                   where stu.Tutor == true
                                   join u in db.Users on stu.ID equals u.ID
                                   select u).ToList();

            //2. set them up in the combo box 
            foreach (User u in tutors)
            {
                if (u.ID != ACCID)
                {
                    tutorIDs.Insert(0, u.ID);
                    combFirstChoice.Items.Add(u.FirstName + " " + u.LastName);
                }
            }
        }

        //User selects class
        private void btnByClass_Click(object sender, EventArgs e)
        {
            //Hide left side of form
            hideControls();
            tutorIDs.Clear();
            combFirstChoice.Items.Clear();

            //Show Class Options
            setupClassList();

            lblFirstChoice.Text = "Class Name";
            lblSecondChoice.Text = "Tutors Available";
            lblFirstChoice.Show();
            combFirstChoice.Show();
        }

        private void setupClassList()
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();

            List<string> userClasses = (from row in db.StudentClasses where row.ID == ACCID select row.ClassCode).ToList();

            foreach (Class c in db.Classes)
            {
                if(!userClasses.Contains(c.ClassCode))
                {
                    combFirstChoice.Items.Add(c.ClassName);
                }
            }
        }

        //User makes a choice
        private void combFirstChoice_DropDownClosed(object sender, EventArgs e)
        {
            combSecondChoice.Items.Clear();

            if (combFirstChoice.SelectedIndex != -1)
            {
                if (lblFirstChoice.Text.Equals("Tutor Name"))
                {
                    loadTutorClassList();
                }
                else
                {
                    loadClassTutorList();
                }

                showSecondChoice();
            }
        }

        //load second choice options based on what the first choice was
        private void loadTutorClassList()
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();

            int id = tutorIDs[combFirstChoice.SelectedIndex];
            List<Class> classes = (from row in db.StudentClasses where row.ID == id select row.Class).ToList();
            List<string> userClasses = (from row in db.StudentClasses where row.ID == ACCID select row.ClassCode).ToList();
            foreach (Class SC in classes)
            {
                if (!userClasses.Contains(SC.ClassCode))
                {
                    combSecondChoice.Items.Add(SC.ClassName);
                }
            }
        }

        private void loadClassTutorList()
        {
            //1. pull all of the students that are available as tutors into a list
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();

            //Get classcode of selected class
            string className = combFirstChoice.Items[combFirstChoice.SelectedIndex].ToString();
            string classCode = (from row in db.Classes where row.ClassName == className select row.ClassCode).First();

            //1.1 Identify all the tutors and put them in a list
            List<User> tutors = (from userCla in db.StudentClasses
                                 where userCla.ClassCode == classCode
                                 join u in db.Users on userCla.ID equals u.ID
                                 select u).ToList();

            //2. set them up in the combo box 
            foreach (User u in tutors)
            {
                if (u.ID != ACCID)
                {
                    tutorIDs.Insert(0, u.ID);
                    combSecondChoice.Items.Add(u.FirstName + " " + u.LastName);
                }
            }
        }

        private void showSecondChoice()
        {
            lblHowLong.Show();
            lblHour.Show();
            lblMin.Show();
            combStartHour.Show();
            combStartMinute.Show();
            cbxWeekly.Show();
            lblSecondChoice.Show();
            combSecondChoice.Show();

            btnFindMatches.Show();
        }

        private void btnExit_Click(object sender, EventArgs e) 
        {
            StudentMain g = new StudentMain(ACCID);
            g.Show();
            this.Dispose();
        }

        private void btnFindMatches_Click(object sender, EventArgs e)
        {
            if (formComplete())                                                         //If we're good to go then we move onto the matching process
            {
                if (lblFirstChoice.Text.Equals("Tutor Name"))
                {
                    MatchTimes(combFirstChoice, combSecondChoice);
                }
                else
                {
                    MatchTimes(combSecondChoice, combFirstChoice);
                }
            }
            else
            {
                MessageBox.Show("Please select a tutor, class, and a session length."); 
            }
        }

        private bool formComplete()
        {
            return (combSecondChoice.SelectedIndex >= 0 && combStartHour.SelectedIndex >= 0 && combStartMinute.SelectedIndex >= 0 && (combStartHour.SelectedIndex > 0 || combStartMinute.SelectedIndex > 0));
        }

        private void btnSendRequest_Click(object sender, EventArgs e, List<string> tutorValidSlot, int TutID, int TuteeID, List<TutorMaster.Commitment> tutorCommits, List<TutorMaster.Commitment> tuteeCommits, string classCode, TutorMasterDBEntities4 db, bool weekly, int length)
        {
            string slot = lvAvailableTimes.CheckedItems[0].SubItems[0].Text.ToString()+","+lvAvailableTimes.CheckedItems[0].SubItems[1].Text.ToString();
            addCommits(slot, TutID, TuteeID, tutorCommits, tuteeCommits, classCode, db, weekly, length);      //add the commits!  
            MessageBox.Show("Your appointments has been Scheduled!");                                                                                       //Let them know that it worked!
            btnExit_Click(this, e);                                                                                                                         //close this form and send them back to the student form!
        }

        //Match Function and it's helpers
        private void MatchTimes(ComboBox TutBox, ComboBox ClsBox)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            lvAvailableTimes.Items.Clear();
            lvAvailableTimes.Hide();

            //a. Name of Tutor, use it to find their ID
            int TutID = tutorIDs[TutBox.SelectedIndex];

            //b. Length of tutoring session in minutes
            int length = Convert.ToInt32(combStartHour.Text) * 4 + (Convert.ToInt32(combStartMinute.Text) / 15);

            //c. whether it is weekly
            bool weekly = cbxWeekly.Checked;

            //d. tutee ID (ACCID), use to match the availiblity
            int TuteeID = ACCID;


            DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

            string classCode = (from row in db.Classes where row.ClassName == ClsBox.Text select row.ClassCode).First();

            List<Commitment> tuteeCommits = (from stucmt in db.StudentCommitments.AsEnumerable()    // create a list of tutee's commitments
                                                where stucmt.ID == TuteeID
                                                join cmt in db.Commitments.AsEnumerable() on stucmt.CmtID equals cmt.CmtID
                                                select cmt).ToList();

            SortsAndSearches.QuickSort(ref tuteeCommits, tuteeCommits.Count()); //sort the tutee commits so that they are in chronological order

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

                SortsAndSearches.QuickSort(ref tutorCommits, tutorCommits.Count()); //sort it so we can use it
                checkMax(ref tutorCommits); 
                removeNotOpens(ref tutorCommits, start, weekly); //remove the occupied commitments

                List<string> tutorValidSlots = getValidSlots(ref tutorCommits, length); //create a list of all the valid tutor slots

                int numMatches = 0;
                lvAvailableTimes.Show();
                for (int j = 0; j < tutorValidSlots.Count(); j++) //iterate through all the available tutor slots 
                {
                    if (SortsAndSearches.BinarySearch(tuteeValidSlots, tutorValidSlots[j]))
                    {
                        //add all the slots to the tutor availibility timeslot
                        ListViewItem item = new ListViewItem(new string[] { tutorValidSlots[j].Split(',')[0], tutorValidSlots[j].Split(',')[1] });
                        lvAvailableTimes.Items.Add(item);//adds the time slot to the combo box 

                        numMatches++;
                    }
                }

                if (numMatches == 0)
                {
                    MessageBox.Show("This Tutor has no matching availibility.");
                }
                btnSendRequest.Click += (sender, e) => btnSendRequest_Click(sender, e, tutorValidSlots, TutID, TuteeID, tutorCommits, tuteeCommits, classCode, db, weekly, length);
                
            } 
        }

        private void removeNotOpens(ref List<TutorMaster.Commitment> cmtList, DateTime start, bool weekly)
        {
            if (weekly)                                                                                                                                     //if this is a weekly commitments
            {
                for (int i = 0; i < cmtList.Count() - 1; i++)
                {
                    if (!Commits.isOpen(cmtList[i]) || DateTime.Compare(start, Convert.ToDateTime(cmtList[i].StartTime)) > 0 || cmtList[i].Weekly != weekly)//remove them if they are not after start, and if they are not open or weekly
                    {
                        cmtList.Remove(cmtList[i]);
                        i--;
                    }
                }
            }
            else                                                                                                                                            //if its not weekly
            {
                for (int i = 0; i < cmtList.Count() - 1; i++)
                {
                    if (!Commits.isOpen(cmtList[i]) || DateTime.Compare(start, Convert.ToDateTime(cmtList[i].StartTime)) > 0)                               //only eliminate the commit if its not open or not after start
                    {
                        cmtList.Remove(cmtList[i]);
                        i--;
                    }
                }
            }
        }

        //this function makes sure that we do not take a time that is already at 3 hours and add to it
        private void checkMax(ref List<TutorMaster.Commitment> cmtList)
        {
            int consec = 1;

            for (int i = 0; i < cmtList.Count() - 1; i++)
            {
                DateTime currentCommit = Convert.ToDateTime(cmtList[i].StartTime);                                                                           //get the current commitment's starttime
                DateTime nextCommit = Convert.ToDateTime(cmtList[i + 1].StartTime);                                                                          //get the next commitment's starttime

                if (consec > 11)                                                                                                                             //if the consecutive counter is above 11, then remove the next commitment
                {
                    if (DateTime.Compare(currentCommit.AddMinutes(15), nextCommit) == 0 && Commits.sameCategory(cmtList[i], cmtList[i + 1]) && !Commits.isOpen(cmtList[i]))//if it is adjacent as well, remove the next commitment
                    {
                        cmtList.Remove(cmtList[i + 1]);
                        i--;
                    }
                    consec = 1;                                                                                                                             //set commit counter to 0
                }
                if (DateTime.Compare(currentCommit.AddMinutes(15), nextCommit) == 0 && Commits.sameCategory(cmtList[i], cmtList[i + 1]) && !Commits.isOpen(cmtList[i]))//if the next commit is adjacent, then increment consec counter
                {
                    consec++;
                }
                else
                {
                    consec = 1;
                }
            }
        }

        private List<string> getValidSlots(ref List<TutorMaster.Commitment> cmtList, int sessionLength)
        {
            int consecutiveCommits = 0;

            List<string> validSlots = new List<string>();
            TutorMaster.Commitment initialCommit = cmtList[0];
            DateTime startDate = Convert.ToDateTime(cmtList[0].StartTime);
            DateTime endDate = Convert.ToDateTime(cmtList[0].StartTime).AddMinutes(15);

            consecutiveCommits++;

            if (sessionLength == 1)
            {
                for (int i = 0; i < cmtList.Count(); i++)
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

                    if (DateTime.Compare(nextCommitDate, currentCommitDate.AddMinutes(15)) == 0 && consecutiveCommits < sessionLength) //if our next commit is 15 minutes later of our current and we are below threshold
                    {
                        consecutiveCommits++;                                                                                          //increment the consec counter
                        endDate = Convert.ToDateTime(cmtList[i].StartTime).AddMinutes(15);                                             //move end date up 15 minutes
                    }
                    else if (DateTime.Compare(nextCommitDate, currentCommitDate.AddMinutes(15)) == 0 && consecutiveCommits >= sessionLength) //if we are above the threshold and next commit is adjacent
                    {
                        endDate = Convert.ToDateTime(cmtList[i].StartTime).AddMinutes(15);                                             //move up the end date
                        validSlots.Add(startDate.ToString() + "," + endDate.ToString());                                               //add the valid slot
                        startDate = startDate.AddMinutes(15);                                                                          //move up the start date
                    }
                    else if (DateTime.Compare(nextCommitDate, currentCommitDate.AddMinutes(15)) != 0 && consecutiveCommits >= sessionLength)//if not adajacent but above threshold
                    {
                        endDate = Convert.ToDateTime(cmtList[i].StartTime).AddMinutes(15);                                             //move up the enddate
                        validSlots.Add(startDate.ToString() + "," + endDate.ToString());                                               //add the validslots

                        //update our carries
                        consecutiveCommits = 1;
                        startDate = Convert.ToDateTime(cmtList[i + 1].StartTime);
                        endDate = Convert.ToDateTime(cmtList[i + 1].StartTime).AddMinutes(15);
                        initialCommit = cmtList[i + 1];
                    }
                    else if (DateTime.Compare(nextCommitDate, currentCommitDate.AddMinutes(15)) != 0 && consecutiveCommits < sessionLength) //if not adjacent and not above the threshold
                    {
                        //update carries and set consec counter to 1
                        consecutiveCommits = 1;
                        startDate = Convert.ToDateTime(cmtList[i + 1].StartTime);
                        endDate = Convert.ToDateTime(cmtList[i + 1].StartTime).AddMinutes(15);
                        initialCommit = cmtList[i + 1];
                    }
                }

                //handle the last commitment
                if (consecutiveCommits >= sessionLength && DateTime.Compare(Convert.ToDateTime(cmtList[cmtList.Count() - 2].StartTime).AddMinutes(15), Convert.ToDateTime(cmtList[cmtList.Count() - 1].StartTime)) == 0)
                {
                    endDate = endDate.AddMinutes(15);                                                                                   //move the end date up by 15 minutes
                    validSlots.Add(startDate.ToString() + "," + endDate.ToString());                                                    //add the valid slots
                }
                else if (consecutiveCommits == sessionLength && DateTime.Compare(Convert.ToDateTime(cmtList[cmtList.Count() - 2].StartTime).AddMinutes(15), Convert.ToDateTime(cmtList[cmtList.Count() - 1].StartTime)) == 0)
                {
                    endDate = endDate.AddMinutes(15);
                    validSlots.Add(startDate.ToString() + "," + endDate.ToString());
                }
            }
            return validSlots;
        }

        private void addCommits(string timeSlot, int tutorId, int tuteeId, List<TutorMaster.Commitment> tutorCommits, List<TutorMaster.Commitment> tuteeCommits, string classCode, TutorMasterDBEntities4 db, bool weekly, int numSessions)
        {
            DateTime startTime = DateTimeMethods.getStartTime(timeSlot);               //get the start time of the time slot
            DateTime endTime = DateTimeMethods.getEndTime(timeSlot);                   //get the end time of the time slot
            DateTime saveFirst = startTime;                                            //make copies of them
            DateTime saveEnd = endTime;


            if (!weekly)                                                               //if the request is not weekly
            {
                for (int j = 0; j < tuteeCommits.Count(); j++)
                {
                    if (DateTime.Compare(startTime, Convert.ToDateTime(tuteeCommits[j].StartTime)) <= 0 && DateTime.Compare(endTime, Convert.ToDateTime(tuteeCommits[j].StartTime)) > 0)
                    {                                                                  //if the commitment's start time is between the end time and start time, update it to a new appointment
                        tuteeCommits[j].Open = false;
                        tuteeCommits[j].Tutoring = false;
                        tuteeCommits[j].ID = tutorId;
                        tuteeCommits[j].Class = classCode + "!";
                        tuteeCommits[j].Weekly = false;
                        db.SaveChanges();
                    }
                    else if (DateTime.Compare(endTime, Convert.ToDateTime(tuteeCommits[j].StartTime)) <= 0)
                    {                                                                  //else, break out of this for loop
                        break;
                    }
                }

                for (int i = 0; i < tutorCommits.Count(); i++)
                {
                    if (DateTime.Compare(startTime, Convert.ToDateTime(tutorCommits[i].StartTime)) <= 0 && DateTime.Compare(endTime, Convert.ToDateTime(tutorCommits[i].StartTime)) > 0)
                    {                                                                 //if the commitment's start time is between the end time and start time, update it to a new appointment
                        tutorCommits[i].Open = false;
                        tutorCommits[i].Tutoring = true;
                        tutorCommits[i].ID = tuteeId;
                        tutorCommits[i].Class = classCode + "!";
                        tutorCommits[i].Weekly = false;
                        db.SaveChanges();
                    }
                    else if (DateTime.Compare(endTime, Convert.ToDateTime(tutorCommits[i].StartTime)) <= 0)
                    {                                                                   //else, break out of this for loop
                        break;
                    }
                }
            }
            else
            {
                for (int j = 0; j < tuteeCommits.Count(); j++)
                {
                    if (DateTime.Compare(startTime, Convert.ToDateTime(tuteeCommits[j].StartTime)) <= 0 && DateTime.Compare(endTime, Convert.ToDateTime(tuteeCommits[j].StartTime)) > 0)
                    {
                        if (!tuteeCommits[j].Class.ToString().Contains('!'))
                        {                                                               //if the commitment is between the start and end times, put it as an appointment
                            tuteeCommits[j].Open = false;
                            tuteeCommits[j].Tutoring = false;
                            tuteeCommits[j].ID = tutorId;
                            tuteeCommits[j].Class = classCode + "!";
                            db.SaveChanges();
                        }
                    }
                    else if (DateTime.Compare(endTime, Convert.ToDateTime(tuteeCommits[j].StartTime)) <= 0)
                    {                                                                  //if it is after end time, move our range forward a week
                        startTime = startTime.AddDays(7);
                        endTime = endTime.AddDays(7);
                        j--;
                    }
                }
                startTime = saveFirst;
                endTime = saveEnd;
                for (int i = 0; i < tutorCommits.Count(); i++)
                {
                    if (DateTime.Compare(startTime, Convert.ToDateTime(tutorCommits[i].StartTime)) <= 0 && DateTime.Compare(endTime, Convert.ToDateTime(tutorCommits[i].StartTime)) > 0)
                    {
                        if (!tutorCommits[i].Class.ToString().Contains('!'))
                        {                                                              //if tutor commits is in the range, change it to a new appointment
                            tutorCommits[i].Open = false;
                            tutorCommits[i].Tutoring = true;
                            tutorCommits[i].ID = tuteeId;
                            tutorCommits[i].Class = classCode + "!";
                            db.SaveChanges();
                        }

                    }
                    else if (DateTime.Compare(endTime, Convert.ToDateTime(tutorCommits[i].StartTime)) <= 0)
                    {                                                                  //if it is after end time, move our range forward a week
                        startTime = startTime.AddDays(7);
                        endTime = endTime.AddDays(7);
                        i--;
                    }
                }
            }
        }

        private bool sameCategory(TutorMaster.Commitment commitFirst, TutorMaster.Commitment commitSecond)      //ask if the 15 minute time block of the first has the same values as the second
        {
            return (commitFirst.Class == commitSecond.Class && commitFirst.Location == commitSecond.Location
                    && commitFirst.Open == commitSecond.Open && commitFirst.Weekly == commitSecond.Weekly
                    && commitFirst.ID == commitSecond.ID);
        }

        private void lvAvailableTimes_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (lvAvailableTimes.CheckedItems.Count == 1)
            {
                btnSendRequest.Enabled = true;
                btnSendRequest.BackColor = System.Drawing.Color.FromArgb(208, 222, 229);
            }
            else
            {
                btnSendRequest.Enabled = false;
                btnSendRequest.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
            }
        }

        private void AdvancedRequest_FormClosed(object sender, FormClosedEventArgs e)
        {
            StudentMain g = new StudentMain(ACCID);
            g.Show();
            this.Dispose();
        }
    }
}

