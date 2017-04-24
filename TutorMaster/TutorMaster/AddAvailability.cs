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
    public partial class AddAvailability : Form
    {
        int id;
        private List<string> alreadyRecordedTimes = new List<string>();

        public AddAvailability(int ID)
        {
            id = ID;
            InitializeComponent();
            dayStartDateTime.Value = DateTime.Now;
            dayEndDateTime.Value = DateTime.Now;
        }

        private void btnAddOpenBlock_Click(object sender, EventArgs e)
        {
            //first, error check to make sure that the user put something for each dropdownbox
            if ((string.IsNullOrWhiteSpace(combStartHour.Text)) || (string.IsNullOrWhiteSpace(combStartMinute.Text)
            || (string.IsNullOrWhiteSpace(combStartAmPm.Text))) || (string.IsNullOrWhiteSpace(combEndHour.Text))
            || (string.IsNullOrWhiteSpace(combEndMinute.Text)) || (string.IsNullOrWhiteSpace(combEndAmPm.Text)))
            {
                MessageBox.Show("Please fill out a starting and ending day, hour, minute, and part of day");
            }
            else
            {
                //get the starttime data for the commitments
                int startHour = int.Parse(combStartHour.Text);
                int startMinute = int.Parse(combStartMinute.Text);
                string startAmPm = combStartAmPm.Text;

                //convert hour to correct time given the input for the DateTime class
                //add 12 if its past 12PM 
                //if its 12AM, set the hour to 0
                if (startAmPm == "PM" && startHour != 12)
                {
                    startHour += 12;
                }
                else if (startAmPm == "AM" && startHour == 12)
                {
                    startHour = 0;
                }
                //get the endtime data for the commitments
                int endHour = int.Parse(combEndHour.Text);
                int endMinute = int.Parse(combEndMinute.Text);
                string endAmPm = combEndAmPm.Text;

                //convert hour to correct time given the input for the DateTime class
                //add 12 if its past 12PM 
                //if its 12AM, set the hour to 0
                if (endAmPm == "PM" && endHour != 12)
                {
                    endHour += 12;
                }
                else if (endAmPm == "AM" && endHour == 12)
                {
                    endHour = 0;
                }

                //get whether or not it is a weekly commitment
                bool weekly = cbxWeekly.Checked;

                //record start and end time based on what user input in adding for their availability
                DateTime startTime = new DateTime(dayStartDateTime.Value.Year, dayStartDateTime.Value.Month, dayStartDateTime.Value.Day, startHour, startMinute, 0);
                DateTime endTime = new DateTime(dayEndDateTime.Value.Year, dayEndDateTime.Value.Month, dayEndDateTime.Value.Day, endHour, endMinute, 0);
                addAvail(startTime, endTime, weekly);
            }
        }

        private void addAvail(DateTime startTime, DateTime endTime, bool weekly)
        {
            DateTime begin = startTime;
            int compare = begin.CompareTo(endTime);
            bool recorded = false;
            if (compare < 0)
            {
                while (compare < 0)                      //if the first date is earlier than the second date
                {
                    if (!recordedTime(begin))            //and if this time slot has not already been recorded
                    {
                        add15Block(begin, weekly);       //add the 15 minute time block and whether or not its weekly
                    }
                    else
                    {
                        recorded = true;
                    }
                    begin = begin.AddMinutes(15);        //repeat this process until we get to the endtime
                    compare = begin.CompareTo(endTime);
                    
                }
                if (recorded)
                {
                    string message = "These times are already in the database and have not been added as availability ";
                    for (int i = 0; i < alreadyRecordedTimes.Count-1; i++)
                    {
                        message += alreadyRecordedTimes[i] + ", ";
                    }
                    message += alreadyRecordedTimes[alreadyRecordedTimes.Count - 1];
                    MessageBox.Show(message);
                    alreadyRecordedTimes.Clear();
                }
                MessageBox.Show("All 15 minute time blocks that were not already occupied have been added to your availability.");
            }
            else
            {
                MessageBox.Show("Please have your start time at an earlier time then your end time.");
            }
        }

        private bool recordedTime(DateTime begin)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            bool found = false;

            var date = (from stucmt in db.StudentCommitments
                        where stucmt.ID == id
                        join cmt in db.Commitments on stucmt.CmtID equals cmt.CmtID
                        select cmt.StartTime).ToList(); //pull all of the student's commitments

            int dateCount = date.Count();               //count the number of dates

            for (int i = 0; i < dateCount; i++)
            {
                if (begin == Convert.ToDateTime(date[i]))//if a datetime is already taken, say it has and output a message about it
                {
                    found = true;
                    alreadyRecordedTimes.Add(date[i].ToString());
                }
            }

            //if (found)
            //{
            //    MessageBox.Show(begin.ToString() + " is already in the database, this will not be added");
            //}

            return found;
        }

        private void add15Block(DateTime begin, bool weekly)
        {
            int lastCR = getNextCmtId();     //last commit row
            int lastSCR = getNextStdCmtKey();//last student commit row


            //add the first student committment and comittment in case the commitment is not weekly


            TutorMaster.Commitment newCommit = new TutorMaster.Commitment();
            newCommit.CmtID = lastCR;
            newCommit.Class = "-";
            newCommit.Location = "-";
            newCommit.Tutoring = false;
            newCommit.StartTime = begin;
            newCommit.Open = true;
            newCommit.Weekly = weekly;
            newCommit.ID = -1;
            addCommit(newCommit);

            TutorMaster.StudentCommitment newStudentCommit = new TutorMaster.StudentCommitment();
            newStudentCommit.CmtID = lastCR;
            newStudentCommit.ID = id;
            newStudentCommit.Key = lastSCR;
            addStudentCommit(newStudentCommit);

            DateTime endOfSemester = new DateTime(2017, 5, 1, 0, 0, 0);

            //if it is weekly, keep going 7 days into future and if it is before end of semster, add the new commitment
            if (weekly)
            {
                while (begin.AddDays(7).CompareTo(endOfSemester) < 0)
                {
                    begin = begin.AddDays(7);
                    lastSCR += 1;
                    lastCR += 1;

                    //add the commitment first and then add the student commitment
                    TutorMaster.Commitment newCommitW = new TutorMaster.Commitment();
                    newCommitW.CmtID = lastCR;
                    newCommitW.Class = "-";
                    newCommitW.Location = "-";
                    newCommitW.Tutoring = false;
                    newCommitW.StartTime = begin;
                    newCommitW.Open = true;
                    newCommitW.Weekly = weekly;
                    newCommitW.ID = -1;
                    addCommit(newCommitW);

                    TutorMaster.StudentCommitment newStudentCommitW = new TutorMaster.StudentCommitment();
                    newStudentCommitW.CmtID = lastCR;
                    newStudentCommitW.ID = id;
                    newStudentCommitW.Key = lastSCR;
                    addStudentCommit(newStudentCommitW);
                }
            }
        }

        private void addCommit(TutorMaster.Commitment commit)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            db.Commitments.AddObject(commit);
            db.SaveChanges();
        }

        private void addStudentCommit(TutorMaster.StudentCommitment studentCommit)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            db.StudentCommitments.AddObject(studentCommit);
            db.SaveChanges();
        }

        private int getNextCmtId()                                                                      //go into database and get the last commitment ID
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            int rowNum = db.Commitments.Count();
            int lastRow;

            if (rowNum > 0)
            {
                lastRow = db.Commitments.OrderByDescending(u => u.CmtID).Select(r => r.CmtID).First();
            }
            else
            {
                lastRow = 0;
            }
            return lastRow + 1;
        }

        private int getNextStdCmtKey()                                                                 //go into database and get the last student commitment ID
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            int rowNum = db.StudentCommitments.Count();
            int lastRow;

            if (rowNum > 0)
            {
                lastRow = db.StudentCommitments.OrderByDescending(u => u.Key).Select(r => r.Key).First();
            }
            else
            {
                lastRow = 0;
            }
            return lastRow + 1;
        }

        private void AddAvailability_FormClosed(object sender, FormClosedEventArgs e)
        {
                this.Dispose();
        }
    }
}
