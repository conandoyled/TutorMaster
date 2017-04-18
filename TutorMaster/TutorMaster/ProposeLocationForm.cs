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
    public partial class ProposeLocationForm : Form
    {
        private int id;                                                //id of tutor
        private List<string> info = new List<string>();                //list of times the user wants to propose a location for
        private bool admin;

        //constructor
        public ProposeLocationForm(int accID, List<string> infoString, bool isAdmin)
        {
            InitializeComponent();
            id = accID;
            info = infoString;
            admin = isAdmin;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            StudentMain g = new StudentMain(id);
            g.Show();
            this.Close();
        }

        private bool inTheTimeSlot(DateTime startDate, DateTime endDate, Commitment commit)
        { //this function is meant to see if a commitment is between two given times
            return DateTime.Compare(startDate, Convert.ToDateTime(commit.StartTime)) <= 0 && DateTime.Compare(endDate, Convert.ToDateTime(commit.StartTime)) > 0;
        }

        private void setTutorLocations()
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();                                                //Connect to the database
            string loc = txtLoc.Text;                                                                                //grab the text of the location the tutor typed in
            List<Commitment> tutorCmtList = (from stucmt in db.StudentCommitments                                    //get the tutor's commitments
                                             where stucmt.ID == id
                                             join cmt in db.Commitments on stucmt.CmtID equals cmt.CmtID
                                             select cmt).ToList();

            for (int i = 0; i < info.Count(); i++)                                                                   //for every time chosen
            {
                DateTime startDate = getStartTime(info[i]);                                                          //get the startTime
                DateTime endDate = getEndTime(info[i]);                                                              //get the endTime
                for (int c = 0; c < tutorCmtList.Count(); c++)                                                       //go through the tutor commitments
                {
                    if (inTheTimeSlot(startDate, endDate, tutorCmtList[c]))                                          //if a commitment is in the time slot
                    {
                        if (!admin)
                        {
                            tutorCmtList[c].Location = loc + "?";                                                        //change the location to the string the user typed in + a question mark
                        }
                        else
                        {
                            tutorCmtList[c].Location = loc;                                                             //change the location to the string the user typed in
                        }
                        db.SaveChanges();                                                                            //save to database
                    }
                }
            }
        }

        private void setTuteeLocations()
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();                                          //Connect to the database  
            string loc = txtLoc.Text;                                                                          //grab the text of the location the tutor typed in
            for (int i = 0; i < info.Count(); i++)                                                             //for every time chosen
            {
                DateTime startDate = getStartTime(info[i]);                                                    //get the startTime
                DateTime endDate = getEndTime(info[i]);                                                        //get the endTime

                int partnerID = Convert.ToInt32(info[i].Split(',')[2]);

                List<Commitment> tuteeCmtList = (from stucmt in db.StudentCommitments                          //get the tutee's commitments
                                                 where stucmt.ID == partnerID
                                                 join cmt in db.Commitments on stucmt.CmtID equals cmt.CmtID
                                                 select cmt).ToList();
                //if a commitment is in the time slot
                for (int m = 0; m < tuteeCmtList.Count(); m++)                                                 //go through the tutee commitments
                {
                    if (inTheTimeSlot(startDate, endDate, tuteeCmtList[m]))
                    {
                        tuteeCmtList[m].Class = tuteeCmtList[m].Class + "!";                                   //add an exclamation point to the tutee class so that the system knows this is a new commitment
                        if (!admin)
                        {
                            tuteeCmtList[m].Location = loc + "?";                                                  //change the location to the string the user typed in + a question mark
                        }
                        else
                        {
                            tuteeCmtList[m].Location = loc;
                        }
                        db.SaveChanges();                                                                      //save to database
                    }
                }
            }
        }

        private void btnSumbit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLoc.Text))                                                                 //make sure the user has put in a location
            {
                MessageBox.Show("Please write a location you would like to propose for each of the selected sites");    //if not, ask them to
            }
            else if (txtLoc.Text.Contains('?'))                                                                         //make sure they didn't put a question mark
            {
                MessageBox.Show("Please do not put in any question marks into your proposed location");
            }
            else
            {
                setTutorLocations();                                                                                    //set the tutor commitment locations where necessary
                setTuteeLocations();                                                                                    //set the tutee commitment locations where necessary
                StudentMain g = new StudentMain(id);                                                                    //send the student back to student main
                g.Show();
                this.Close();
            }
        }

        private DateTime getStartTime(string slot)                                    //take a string that has the start datetime seperated by a comma with the end datetime
        {
            string startDateTime = slot.Split(',')[0];
            string startDate = startDateTime.Split(' ')[0];                           //get the entire date of start datetime string
            string startTime = startDateTime.Split(' ')[1];                           //get the entire time of the start datetime string
            string amPm = startDateTime.Split(' ')[2];                                //record if this is in the morning or evening

            int month = Convert.ToInt32(startDate.Split('/')[0]);                     //get the month
            int day = Convert.ToInt32(startDate.Split('/')[1]);                       //get the day
            int year = Convert.ToInt32(startDate.Split('/')[2]);                      //get the year

            int hour = Convert.ToInt32(startTime.Split(':')[0]);                      //get the hour
            int min = Convert.ToInt32(startTime.Split(':')[1]);                       //get the minutes


            if (hour < 12 && amPm == "PM")                                            //add 12 to hours if necessary
            {
                hour += 12;
            }
            else if (hour == 12 && amPm == "AM")                                      //if first hour of the day, set hour to 0
            {
                hour = 0;
            }
            DateTime date = new DateTime(year, month, day, hour, min, 0);             //make a datetime instance with the collected data and return it
            return date;
        }

        private DateTime getEndTime(string slot)                                      //take a string that has the start datetime seperated by a comma with the end datetime
        {
            string startDateTime = slot.Split(',')[1];
            string startDate = startDateTime.Split(' ')[0];                           //get the entire date of end datetime string
            string startTime = startDateTime.Split(' ')[1];                           //get the entire time of the end datetime string
            string amPm = startDateTime.Split(' ')[2];                                //record if this is in the morning or evening

            int month = Convert.ToInt32(startDate.Split('/')[0]);                     //get the month
            int day = Convert.ToInt32(startDate.Split('/')[1]);                       //get the day
            int year = Convert.ToInt32(startDate.Split('/')[2]);                      //get the year

            int hour = Convert.ToInt32(startTime.Split(':')[0]);
            int min = Convert.ToInt32(startTime.Split(':')[1]);


            if (hour < 12 && amPm == "PM")                                            //add 12 to hours if necessary
            {
                hour += 12;
            }
            else if (hour == 12 && amPm == "AM")                                      //if first hour of the day, set hour to 0
            {
                hour = 0;
            }

            DateTime date = new DateTime(year, month, day, hour, min, 0);             //make a datetime instance with the collected data and return it
            return date;
        }
    }
}
