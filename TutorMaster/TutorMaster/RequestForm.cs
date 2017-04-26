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
    public partial class RequestForm : Form
    {
        private int id;

        //constructor
        public RequestForm(int accID)
        {
            id = accID;
            InitializeComponent();
            SetUpClassName();
            
        }


        private void SetUpClassName()
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();

            foreach (Class c in db.Classes)
            {
                combCourseName.Items.Add(c.ClassName); //add all the class names to this list 
            }

            removeInvalidCourses();                    //remove the courses that the student is an approved tutor for
            combHours.Text = "0";
            combMins.Text = "00";

        }

        private void removeInvalidCourses()
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            bool thisStudentATutor = (bool)(from row in db.Students where row.ID == id select row.Tutor).First();                           //get whether this person is a tutor
            if (thisStudentATutor)
            {
                List<string> removeClasses = (from stuClass in db.StudentClasses.AsEnumerable()
                                              where stuClass.ID == id
                                              join course in db.Classes.AsEnumerable() on stuClass.ClassCode equals course.ClassCode
                                              select course.ClassName).ToList();                                                           //if they are a tutor, get all of their approved courses

                for (int i = 0; i < combCourseName.Items.Count; i++)
                {
                    for (int j = 0; j < removeClasses.Count; j++)
                    {
                        if (removeClasses[j].ToString() == combCourseName.Items[i].ToString())                                             //if its name is in the combobox, then remove it from there
                        {
                            combCourseName.Items.Remove(combCourseName.Items[i]);
                        }
                    }
                }
            }
        }

        //exit button
        private void btnExit_Click(object sender, EventArgs e)
        {
            StudentMain f = new StudentMain(id);
            f.Show();
            this.Dispose();
        }


        private void btnRequest_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(combCourseName.Text))                                                                         //check if anything necessary is null
            {
                MessageBox.Show("Please select a course for the session.");
            }
            else if (string.IsNullOrWhiteSpace(combHours.Text) || string.IsNullOrWhiteSpace(combMins.Text))
            {
                MessageBox.Show("Please input values for the hours and minutes dropdown boxes");
            }
            else if (((Convert.ToInt32(combHours.Text) * 4 + (Convert.ToInt32(combMins.Text) / 15)) == 0) ||                            //check that the appointment is between 15 minutes and 3 hours
                ((Convert.ToInt32(combHours.Text) * 4 + (Convert.ToInt32(combMins.Text) / 15)) > 12))
            {
                MessageBox.Show("Please input values for the hours and minutes that are between a length of 15 minutes and 3 hours");
            }
            else
            {
                bool weekly = cbxWeekly.Checked;                                                                                        //get whether this is weekly
                DateTime start = DateTime.Now;                                                                                          //set start to now
                TutorMasterDBEntities4 db = new TutorMasterDBEntities4();                                                               //connect to database

                string classCode = (from row in db.Classes where combCourseName.Text == row.ClassName select row.ClassCode).First();    //get the class code

                var approvedTutorIds = (from row in db.StudentClasses.AsEnumerable() where classCode == row.ClassCode select row.ID).ToList();//get all of the approved tutors
                if (approvedTutorIds.Count() == 0)
                {
                    MessageBox.Show("There are currently no tutors approved to tutor this course. Sorry.");
                }
                else
                {

                    List<Commitment> tuteeCommits = (from stucmt in db.StudentCommitments.AsEnumerable()                               //get the tutee commitments
                                                     where stucmt.ID == id
                                                     join cmt in db.Commitments.AsEnumerable() on stucmt.CmtID equals cmt.CmtID
                                                     select cmt).ToList();


                    int sessionLength = Convert.ToInt32(combHours.Text) * 4 + (Convert.ToInt32(combMins.Text) / 15);                   //get the number of 15 minute time blocks of the session

                    SortsAndSearches.QuickSort(ref tuteeCommits, tuteeCommits.Count());                                                //sort the tutee commit list
                    
                    checkMax(ref tuteeCommits);                                                                                        //check if there are any 3 hour time blocks 

                    removeNotOpens(ref tuteeCommits, start, weekly);                                                                   //remove the commitments that are not open

                    if (tuteeCommits.Count == 0)
                    {
                        MessageBox.Show("You currently have no available slots, please add some availability before attempting to schedule a session of this length");
                    }
                    else
                    {
                        List<string> tuteeValidSlots = getValidSlots(ref tuteeCommits, sessionLength);                                //get the tutee's valid time slots for the length of the session we're looking for

                        bool done = false;
                        for (int i = 0; i < approvedTutorIds.Count(); i++)                                                            //go through each tutor in the approved tutor list
                        {
                            if (approvedTutorIds[i] != id)                                                                            //don't let a tutor tutor him/herself
                            {
                                var tutorFirstName = (from row in db.Users.AsEnumerable() where row.ID == approvedTutorIds[i] select row.FirstName).First();
                                var tutorLastName = (from row in db.Users.AsEnumerable() where row.ID == approvedTutorIds[i] select row.LastName).First();

                                List<TutorMaster.Commitment> tutorCommits = (from stucmt in db.StudentCommitments.AsEnumerable()     //get the tutor's commitments
                                                                             where stucmt.ID == approvedTutorIds[i]
                                                                             join cmt in db.Commitments.AsEnumerable() on stucmt.CmtID equals cmt.CmtID
                                                                             select cmt).ToList();

                                SortsAndSearches.QuickSort(ref tutorCommits, tutorCommits.Count());                                   //sort them

                                checkMax(ref tutorCommits);                                                                           //check for 3 tutoring blocks

                                removeNotOpens(ref tutorCommits, start, weekly);                                                      //remove the not opens

                                List<string> tutorValidSlots = getValidSlots(ref tutorCommits, sessionLength);                        //get the valid time slots for the tutors

                                for (int j = 0; j < tutorValidSlots.Count(); j++)                                                     //iterate through the valid tutor time slots
                                {
                                    if (SortsAndSearches.BinarySearch(tuteeValidSlots, tutorValidSlots[j]))                           //see if the tutorvalid slot is in the tutee list
                                    {                                                                                                 //if it is, ask the user if they'd this appointment
                                        DialogResult choice = MessageBox.Show("You have been matched with " + tutorFirstName + " " + tutorLastName +
                                            " for a time at: " + tutorValidSlots[j].Split(',')[0] + " - " + tutorValidSlots[j].Split(',')[1], "You've got a match!", MessageBoxButtons.YesNo);
                                        if (choice == DialogResult.Yes)                                                               //if they say yes, get their ids and add the commitments to their schedules
                                        {
                                            int tutorId = Convert.ToInt32(approvedTutorIds[i]);
                                            int tuteeId = Convert.ToInt32(id);
                                            addCommits(tutorValidSlots[j], tutorId, tuteeId, tutorCommits, tuteeCommits, classCode, db, weekly, sessionLength);
                                            done = true;
                                            break;
                                        }
                                        else if (choice == DialogResult.No)                                                           //if no, break out of this loop and repeat the same process with another approved tutor if there is one
                                        {
                                            break;
                                        }
                                    }
                                }
                                if (done)                                                                                             //if they picked this tutor and we're done, break out of this large for loop
                                {
                                    break;
                                }
                            }
                        }
                        if (!done)                                                                                                    //if we go through every tutor and do not pick one, put the message up
                        {
                            MessageBox.Show("There are no more tutors that meet your request requirements.");
                        }
                    }
                    StudentMain g = new StudentMain(id);                                                                              //return to student main
                    g.Show();
                    this.Dispose();
                }
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
                        tuteeCommits[j].Class = classCode+"!";
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
                        tutorCommits[i].Class = classCode+"!";
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

        private void RequestForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Login g = new Login();
            g.Show();
            this.Dispose();
        }
    }
}

