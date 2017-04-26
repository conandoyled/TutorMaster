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
    public partial class AdminCreateAppointmentForm : Form
    {
        private int id;
        private bool edit = false;
        private bool tutoring;
        private List<int> rememberStudIDs = new List<int>();
        private ListViewItem lastItemChecked;
        private int chosenStudentIndex;
        //private bool mustPick = false;

        //constructor for not editing an appointment
        public AdminCreateAppointmentForm(int accID, bool tutoringP)
        {
            InitializeComponent(); 
            id = accID;                                                        //get the student's id number
            tutoring = tutoringP;                                              //get whether or not they are tutoring
            populateListview();                                                //load the listview features
            hideControls();                                                    //hide the necessary controls
            if (tutoring)                                                      //if tutoring, load the tutor options
            {
                loadTutorOptions();
            }
            else                                                               //otherwise, load the tutee options
            {
                loadTuteeOptions();
            }
        }

        //constructor for editting an appointment
        public AdminCreateAppointmentForm(int accID, string info)
        {
            InitializeComponent();
            id = accID;                                                     //get the student's id
            edit = true;                                                    //remember that we are editting an appointment
            populateListview();                                             //load the listview features
            rememberStudIDs.Add(Convert.ToInt16(info.Split(',')[8]));       //throw the partner id in the remember list
            loadAppointmentInformation(info);                               //load the appointment information
            
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();       //open the database
            string class1;
            if (info.Split(',')[2].Contains('!'))                           //if its a new commitment, ignore the !
            {
                class1 = info.Split(',')[2].Substring(0, info.Split(',')[2].Length - 1);
            }
            else                                                            //if its not, then just get the classcode
            {
                class1 = info.Split(',')[2];
            }
            btnCancel.Hide();                                               //hide the cancel button
            cbWeekly.Enabled = false;
            Class course = (from row in db.StudentClasses.AsEnumerable() where row.ClassCode == class1 select row.Class).First(); //get the class based on the course number
            cbxClasses.Text = course.ClassName.ToString();                  //set this dropdown text to the courses classname
            cbxStudents.Text = info.Split(',')[7];                          //set the student text to the name of the partner
            loadMinutesAndHours(info);                                      //set the minutes and hour dropdowns
            pickCurrent(info);                                              //have the current timeslot of the appointment be checked in the listview
        }

        private void pickCurrent(string info)
        {
            for (int i = 0; i < lvTimeMatches.Items.Count; i++)
            {                                                               //get the start time and end time of the appointment and compare it the listview items
                if (lvTimeMatches.Items[i].SubItems[0].Text.ToString() == info.Split(',')[0] && lvTimeMatches.Items[i].SubItems[1].Text.ToString() == info.Split(',')[1])
                {
                    lvTimeMatches.Items[i].Checked = true;                  //mark the one that is the same as the original
                }
            }
        }

        private void loadAppointmentInformation(string info)
        {
            string tutoringString = info.Split(',')[5];                   //get whether this student was tutoring in the appointment
            bool tutoring;
            if (tutoringString == "True")                                 //then set tutoring bool to true
            {
                tutoring = true;
            }
            else                                                          //if not, then set the tutoring bool to false
            {
                tutoring = false;
            }

            if (tutoring)                                                 //load the appropriate options for a tutor if they are tutoring
            {
                loadTutorOptions();
            }
            else
            {
                loadTuteeOptions();
            }

            tbxLocation.Text = info.Split(',')[3];                        //set the location text to the original text from the commitment
            string weeklyString = info.Split(',')[6];                     //get whether this was weekly
            bool weekly;
            
            if (weeklyString == "True")                                   //if this is a weekly appointment, say true
            {
                weekly = true;
            }
            else                                                          //else, say this is not weekly
            {
                weekly = false;
            }
            cbWeekly.Checked = weekly;                                    //set the checkbox to the appropriate boolean

        }


        private void loadMinutesAndHours(string info)
        {
            int numSession = getNumSession(info);                        //get the number of 15 minute timeblocks
            int hour = 0;
            while (numSession > 3)                                       //if its greater than 3, modulo it and add 1 to hour
            {
                hour++;
                numSession = numSession % 4;
            }
            cbxHour.Text = hour.ToString();                              //set hour to the appropriate value
            if (numSession > 0)
            {
                cbxMinutes.Text = (numSession * 15).ToString();          //set the minutes to the remainder of the numsessions multiplied by 15
            }
            else
            {
                string zero = "00";                                      //if remainder is 0, then set the text to 0
                cbxMinutes.Text = zero;
            }
        }


        private int getNumSession(string info)
        {
            int numSession = 0;
            string startTime = info.Split(',')[0];
            string endTime = info.Split(',')[1];
            string timeSlot = startTime + "," + endTime;             //load a timeslot string
            DateTime start = DateTimeMethods.getStartTime(timeSlot); //get the datetime start time
            DateTime end = DateTimeMethods.getEndTime(timeSlot);     //get the datetime end time
            while (DateTime.Compare(start, end) < 0)
            {
                numSession++;                                        //increment numsessions until you get to appropriate value
                start = start.AddMinutes(15);
            }
            return numSession;
        }

        private void hideControls()
        {
            //hide all of the appropriate controls that need to be shown later
            lblPartner.Hide();
            lblMinutes.Hide();
            lblLocation.Hide();
            lblHours.Hide();
            btnSubmit.Hide();
            cbxStudents.Hide();
            cbxHour.Hide();
            cbxMinutes.Hide();
            cbWeekly.Hide();
            tbxLocation.Hide();
            lvTimeMatches.Hide();
        }

        //put in the right features into the listview
        private void populateListview()
        {
            lvTimeMatches.CheckBoxes = true;
            lvTimeMatches.Columns.Add("Start Time", 150);
            lvTimeMatches.Columns.Add("End Time", 150);
        }


        //load the classes to the tutor checkbox and the students to the student checkboxes
        private void loadTutorOptions()
        {
            loadTutorClassesCheckBox();
            loadTutorStudentCheckBox();
        }

        //load the tutee classes checkbox
        private void loadTuteeOptions()
        {
            loadTuteeClassesCheckBox();
        }

        //load the tutee classes checkbox
        private void loadTuteeClassesCheckBox()
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();                                                           //open the database

            List<string> classes = (from offeredClass in db.Classes.AsEnumerable() select offeredClass.ClassName).ToList();     //get all the class names
            for (int i = 0; i < classes.Count(); i++)                                                                           //go thru the list of class names
            {
                cbxClasses.Items.Add(classes[i]);                                                                               //add the classes to the drop down
            }

            removeInvalidCourses();                                                                                             //remove the courses the student can tutor
        }

        private void removeInvalidCourses()
        //remove the courses the student is approved to tutor
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            bool thisStudentATutor = (bool)(from row in db.Students where row.ID == id select row.Tutor).First();           
            if (thisStudentATutor)                                                                                              //check if the student is a tutor
            {
                List<string> removeClasses = (from stuClass in db.StudentClasses.AsEnumerable()                                 //if they are, get all the courses that tutor from the database
                                              where stuClass.ID == id
                                              join course in db.Classes.AsEnumerable() on stuClass.ClassCode equals course.ClassCode
                                              select course.ClassName).ToList();

                for (int i = 0; i < cbxClasses.Items.Count; i++)                                                                //go through the courses in the dropdown
                {
                    for (int j = 0; j < removeClasses.Count; j++)                                                               //go through the courses the student can tutor
                    {
                        if (removeClasses[j].ToString() == cbxClasses.Items[i].ToString())                                      //if you get a match, remove the class from the dropdown
                        {
                            cbxClasses.Items.Remove(cbxClasses.Items[i]);
                        }
                    }
                }
            }
        }


        private void loadTutorClassesCheckBox()
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();                                                           //open the database

            List<string> approvedClasses = (from stuClass in db.StudentClasses                                                  //get the course names the student is allowed to tutor from the databse
                                            where stuClass.ID == id
                                            join appClass in db.Classes on stuClass.ClassCode equals appClass.ClassCode
                                            select appClass.ClassName).ToList();

            for (int i = 0; i < approvedClasses.Count; i++)                                                                     //go thru the list of class names
            {
                cbxClasses.Items.Add(approvedClasses[i]);                                                                       //add approved classes to the dropdown
            }
        }

        private void loadTutorStudentCheckBox()
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            List<int> tuteeIds = (from row in db.Students where row.Tutee == true select row.ID).ToList();                      //get tutee IDs
            if (tuteeIds.Contains(id))                                                                                          //remove the tutee id of the student we are signed in as
            {
                tuteeIds.Remove(id);
            }

            for (int i = 0; i < tuteeIds.Count; i++)
            {
                User student = (from row in db.Users.AsEnumerable() where row.ID == tuteeIds[i] select row).First();           //get all the user objects of each tutee
                if (!student.Username.ToString().Contains('?'))                                                                //if they haven't been approved by the admin, ignore them
                {
                    rememberStudIDs.Add(student.ID);                                                                           //add to the remember student list
                    string name = student.FirstName + " " + student.LastName;                                                  //get the student's name
                    cbxStudents.Items.Add(name);
                }
            }
        }

        private void loadMatchingTimeSlots()
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            bool weekly = cbWeekly.Checked;

            if (rememberStudIDs.Count == 0)                                                                                     //if there are no valid students, print this message
            {
                MessageBox.Show("There are currently no students available for this course");
            }
            else
            {
                DateTime start = DateTime.Now;
                string classCode = (from row in db.Classes where cbxClasses.Text == row.ClassName select row.ClassCode).First();//get the classcode
                int sessionLength = Convert.ToInt32(cbxHour.Text) * 4 + (Convert.ToInt32(cbxMinutes.Text) / 15);                //calculate session length from the hour and minute dropdowns

                if (sessionLength > 12 || sessionLength == 0)                                                                   //make sure the length is of the appropriate size
                {
                    MessageBox.Show("Please pick a session legnth between 15 minutes and 3 hours.");
                }
                else
                {

                    List<TutorMaster.Commitment> studentCommits = (from stucmt in db.StudentCommitments.AsEnumerable()
                                                                 where stucmt.ID == id
                                                                 join cmt in db.Commitments.AsEnumerable() on stucmt.CmtID equals cmt.CmtID
                                                                 select cmt).ToList();                                         //get all of the signed in student's commitments
                    
                    SortsAndSearches.QuickSort(ref studentCommits, studentCommits.Count);                                      //sort them

                    removeNotOpens(ref studentCommits, start, weekly);                                                         //remove the not opens

                    if (studentCommits.Count == 0)                                                                             //if the student we are signed in on has no open slots, print this message
                    {
                        MessageBox.Show("The student you are trying to create an appointment for currently has no open time slots with which to make an appointment.");
                    }
                    else
                    {
                        List<string> studentValidSlots = getValidSlots(ref studentCommits, sessionLength);                     //get all the slots of the appropriate length
                        int partnerId = rememberStudIDs[cbxStudents.SelectedIndex];                                            //get the chosen partner id

                        List<TutorMaster.Commitment> partnerCommits = (from stucmt in db.StudentCommitments.AsEnumerable()
                                                                     where stucmt.ID == partnerId
                                                                     join cmt in db.Commitments.AsEnumerable() on stucmt.CmtID equals cmt.CmtID
                                                                     select cmt).ToList();

                        SortsAndSearches.QuickSort(ref partnerCommits, partnerCommits.Count);                                 //sort the partner's commitments

                        removeNotOpens(ref partnerCommits, start, weekly);                                                    //remove the not opens

                        if (partnerCommits.Count == 0)                                                                        //make sure they have some open available time
                        {
                            MessageBox.Show("The chosen partner currently has no open time slots with which to make an appointment.");
                        }
                        else
                        {
                            List<string> tuteeValidSlots = getValidSlots(ref partnerCommits, sessionLength);                  //get every possible valid slot

                            for (int c = 0; c < studentValidSlots.Count(); c++)
                            {
                                if (SortsAndSearches.BinarySearch(tuteeValidSlots, studentValidSlots[c]))                    //look for overlaps and then add them to the listview
                                {
                                    lvTimeMatches.Items.Add(new ListViewItem(new string[] { studentValidSlots[c].Split(',')[0], studentValidSlots[c].Split(',')[1] }));
                                }
                            }
                        }
                    }
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

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (lvTimeMatches.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please pick one time slot for the appointment.");
            }
            else if (string.IsNullOrWhiteSpace(tbxLocation.Text.ToString()))
            {
                MessageBox.Show("Please input a location for this appointment.");
            }
            else
            {
                TutorMasterDBEntities4 db = new TutorMasterDBEntities4();

                List<Commitment> tuteeCommits = new List<Commitment>();
                List<Commitment> tutorCommits = new List<Commitment>();


                if (!tutoring)                                                                                                                                      //if this person is not a tutor
                {
                    tuteeCommits = (from stucmt in db.StudentCommitments.AsEnumerable()                                                                             //get the tutee commitment list
                                    where stucmt.ID == id
                                    join cmt in db.Commitments.AsEnumerable() on stucmt.CmtID equals cmt.CmtID
                                    select cmt).ToList();
                    SortsAndSearches.QuickSort(ref tuteeCommits, tuteeCommits.Count);                                                                               //sort the tutee

                    tutorCommits = (from stucmt in db.StudentCommitments.AsEnumerable()
                                    where stucmt.ID == rememberStudIDs[chosenStudentIndex]
                                    join cmt in db.Commitments.AsEnumerable() on stucmt.CmtID equals cmt.CmtID
                                    select cmt).ToList();

                    SortsAndSearches.QuickSort(ref tutorCommits, tutorCommits.Count);
                    string classCode = (from row in db.Classes.AsEnumerable() where row.ClassName == cbxClasses.Text.ToString() select row.ClassCode).First();

                    string timeSlot = lvTimeMatches.CheckedItems[0].SubItems[0].Text.ToString() + "," + lvTimeMatches.CheckedItems[0].SubItems[1].Text.ToString();
                    int sessionLength = Convert.ToInt32(cbxHour.Text) * 4 + (Convert.ToInt32(cbxMinutes.Text) / 15);
                    
                    addCommits(timeSlot, rememberStudIDs[chosenStudentIndex], id, tutorCommits, tuteeCommits, classCode, db, cbWeekly.Checked, sessionLength);
                    
                }
                else
                {
                    tutorCommits = (from stucmt in db.StudentCommitments.AsEnumerable()
                                    where stucmt.ID == id
                                    join cmt in db.Commitments.AsEnumerable() on stucmt.CmtID equals cmt.CmtID
                                    select cmt).ToList();
                    SortsAndSearches.QuickSort(ref tutorCommits, tutorCommits.Count);

                    tuteeCommits = (from stucmt in db.StudentCommitments.AsEnumerable()
                                    where stucmt.ID == rememberStudIDs[chosenStudentIndex]
                                    join cmt in db.Commitments.AsEnumerable() on stucmt.CmtID equals cmt.CmtID
                                    select cmt).ToList();

                    SortsAndSearches.QuickSort(ref tuteeCommits, tuteeCommits.Count);
                    string classCode = (from row in db.Classes.AsEnumerable() where row.ClassName == cbxClasses.Text.ToString() select row.ClassCode).First();

                    string timeSlot = lvTimeMatches.CheckedItems[0].SubItems[0].Text.ToString() + "," + lvTimeMatches.CheckedItems[0].SubItems[1].Text.ToString();
                    int sessionLength = Convert.ToInt32(cbxHour.Text) * 4 + (Convert.ToInt32(cbxMinutes.Text) / 15);

                    addCommits(timeSlot, id, rememberStudIDs[chosenStudentIndex], tutorCommits, tuteeCommits, classCode, db, cbWeekly.Checked, sessionLength);
                }

                if (edit)
                {
                    MessageBox.Show("The appointment has been updated for both students and availability schedules have been adjusted approriately.");
                }
                else
                {
                    MessageBox.Show("The appointment has been set and finalized in both student's schedules.");
                }

                AdminSeeSchedule g = new AdminSeeSchedule(id);
                g.Show();
                this.Dispose();
                
            }
        }

        private void hideEverything()
        {
            lblPartner.Hide();
            cbxStudents.Hide();
            lblHours.Hide();
            cbxHour.Hide();
            lblMinutes.Hide();
            cbxMinutes.Hide();
            lblLocation.Hide();
            tbxLocation.Hide();
            cbWeekly.Hide();
            lvTimeMatches.Hide();
        }

        private void addCommits(string timeSlot, int tutorId, int tuteeId, List<TutorMaster.Commitment> tutorCommits, List<TutorMaster.Commitment> tuteeCommits, string classCode, TutorMasterDBEntities4 db, bool weekly, int numSessions)
        {
            //TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            DateTime startTime = DateTimeMethods.getStartTime(timeSlot);
            DateTime endTime = DateTimeMethods.getEndTime(timeSlot);
            DateTime saveFirst = startTime;
            DateTime saveEnd = endTime;


            if (!weekly)
            {
                for (int j = 0; j < tuteeCommits.Count(); j++)
                {
                    if (DateTime.Compare(startTime, Convert.ToDateTime(tuteeCommits[j].StartTime)) <= 0 && DateTime.Compare(endTime, Convert.ToDateTime(tuteeCommits[j].StartTime)) > 0)
                    {
                        tuteeCommits[j].Location = tbxLocation.Text.ToString();
                        tuteeCommits[j].Open = false;
                        tuteeCommits[j].Tutoring = false;
                        tuteeCommits[j].ID = tutorId;
                        tuteeCommits[j].Class = classCode + "!";
                        tuteeCommits[j].Weekly = false;
                        db.SaveChanges();
                    }
                    else if (DateTime.Compare(endTime, Convert.ToDateTime(tuteeCommits[j].StartTime)) <= 0)
                    {
                        break;
                    }
                }

                for (int i = 0; i < tutorCommits.Count(); i++)
                {
                    if (DateTime.Compare(startTime, Convert.ToDateTime(tutorCommits[i].StartTime)) <= 0 && DateTime.Compare(endTime, Convert.ToDateTime(tutorCommits[i].StartTime)) > 0)
                    {
                        tutorCommits[i].Location = tbxLocation.Text.ToString();
                        tutorCommits[i].Open = false;
                        tutorCommits[i].Tutoring = true;
                        tutorCommits[i].ID = tuteeId;
                        tutorCommits[i].Class = classCode + "!";
                        tutorCommits[i].Weekly = false;
                        db.SaveChanges();
                    }
                    else if (DateTime.Compare(endTime, Convert.ToDateTime(tutorCommits[i].StartTime)) <= 0)
                    {
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
                        tuteeCommits[j].Location = tbxLocation.Text.ToString();
                        tuteeCommits[j].Open = false;
                        tuteeCommits[j].Tutoring = false;
                        tuteeCommits[j].ID = tutorId;
                        tuteeCommits[j].Class = classCode + "!";
                        db.SaveChanges();
                    }
                    else if (DateTime.Compare(endTime, Convert.ToDateTime(tuteeCommits[j].StartTime)) <= 0)
                    {
                        startTime = startTime.AddDays(7);
                        endTime = endTime.AddDays(7);
                    }
                }
                startTime = saveFirst;
                endTime = saveEnd;
                for (int i = 0; i < tutorCommits.Count(); i++)
                {
                    if (DateTime.Compare(startTime, Convert.ToDateTime(tutorCommits[i].StartTime)) <= 0 && DateTime.Compare(endTime, Convert.ToDateTime(tutorCommits[i].StartTime)) > 0)
                    {
                        tutorCommits[i].Location = tbxLocation.Text.ToString();
                        tutorCommits[i].Open = false;
                        tutorCommits[i].Tutoring = true;
                        tutorCommits[i].ID = tuteeId;
                        tutorCommits[i].Class = classCode + "!";
                        db.SaveChanges();
                    }
                    else if (DateTime.Compare(endTime, Convert.ToDateTime(tutorCommits[i].StartTime)) <= 0)
                    {
                        startTime = startTime.AddDays(7);
                        endTime = endTime.AddDays(7);
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AdminSeeSchedule g = new AdminSeeSchedule(id);
            g.Show();
            this.Dispose();
        }

        private void cbxClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tutoring)
            {
                if (!string.IsNullOrWhiteSpace(cbxClasses.Text.ToString()))
                {
                    lblPartner.Show();
                    cbxStudents.Show();
                }
                else
                {
                    lblPartner.Hide();
                    cbxStudents.Hide();
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(cbxClasses.Text.ToString()))
                {
                    bool tutors = loadTuteeStudentCheckBox();

                    if (tutors)
                    {
                        lblPartner.Show();
                        cbxStudents.Show();
                    }
                    else
                    {
                        tbxLocation.Hide();
                        lblLocation.Hide();
                        lblHours.Hide();
                        lblMinutes.Hide();
                        cbWeekly.Hide();
                        cbxHour.Hide();
                        cbxMinutes.Hide();
                        lvTimeMatches.Hide();
                        btnSubmit.Hide();
                        lblPartner.Hide();
                        cbxStudents.Hide();
                    }
                }
            }
        }

        private bool loadTuteeStudentCheckBox()
        {
            cbxStudents.Items.Clear();
            rememberStudIDs.Clear();
            bool tutorsExist = false;
            
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            
            string chosenClass = cbxClasses.Text.ToString();

            var approvedTutors = (from Classname in db.Classes
                                        where chosenClass == Classname.ClassName
                                        join stuClass in db.StudentClasses on Classname.ClassCode equals stuClass.ClassCode
                                        select stuClass.ID).ToList();

            if (approvedTutors.Count == 0)
            {
                MessageBox.Show("There are currently no tutors approved for this course, please try again later or choose another course."); 
            }
            else
            {
                for (int i = 0; i < approvedTutors.Count; i++)
                {
                    User tutor = (from row in db.Users.AsEnumerable() where row.ID == approvedTutors[i] select row).First();
                    if (!tutor.Username.ToString().Contains('?'))
                    {
                        cbxStudents.Items.Add(tutor.FirstName + " " + tutor.LastName);
                        rememberStudIDs.Add(tutor.ID);
                        tutorsExist = true;
                    }
                }
            }
            return tutorsExist;
        }

        private void cbxStudents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(cbxStudents.Text.ToString()))
            {
                lblLocation.Show();
                lblMinutes.Show();
                lblHours.Show();
                tbxLocation.Show();
                cbxHour.Show();
                cbxMinutes.Show();
                cbWeekly.Show();
                lvTimeMatches.Show();
                btnSubmit.Show();
                lvTimeMatches.Items.Clear();
                tbxLocation.Text = "";
                cbxHour.Text = "";
                cbxMinutes.Text = "";
                
                btnSubmit.Enabled = false;
                chosenStudentIndex = cbxStudents.SelectedIndex;
            }
            else
            {
                lblLocation.Hide();
                lblMinutes.Hide();
                lblHours.Hide();
                btnSubmit.Hide();
                tbxLocation.Hide();
                cbxHour.Hide();
                cbxMinutes.Hide();
                cbWeekly.Hide();
                lvTimeMatches.Hide();
            }
        }

        private void cbxHour_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(cbxHour.Text.ToString()) && !string.IsNullOrWhiteSpace(cbxMinutes.Text.ToString()))
            {
                resetListView();
            }
            else
            {
                lvTimeMatches.Items.Clear();
            }
        }

        private void cbxMinutes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(cbxHour.Text.ToString()) && !string.IsNullOrWhiteSpace(cbxMinutes.Text.ToString()))
            {
                resetListView();
            }
            else
            {
                lvTimeMatches.Items.Clear();
            }
        }
        
        private void resetListView()
        {
            lvTimeMatches.Items.Clear();
            loadMatchingTimeSlots();
        }

        private void cbWeekly_CheckedChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(cbxHour.Text.ToString()) && !string.IsNullOrWhiteSpace(cbxMinutes.Text.ToString()))
            {
                resetListView();
            }
        }

        private void lvTimeMatches_ItemCheck(object sender, ItemCheckEventArgs e)
        {
             if (lastItemChecked != null && lastItemChecked.Checked
        && lastItemChecked != lvTimeMatches.Items[e.Index])
            {
                lastItemChecked.Checked = false;
            }

            lastItemChecked = lvTimeMatches.Items[e.Index];
        }

        private void lvTimeMatches_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (lvTimeMatches.CheckedItems.Count > 0)
            {
                btnSubmit.Enabled = true;
            }
            else
            {
                btnSubmit.Enabled = false;
            }
        }

        private void AdminCreateAppointmentForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
    }
}
