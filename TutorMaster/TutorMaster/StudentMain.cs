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
    public partial class StudentMain : Form
    {
        private int id;

        public StudentMain(int accID)
        {
            id = accID;
            InitializeComponent();
            populateColumns();
            loadAvail();
        }

        private void loadAvail()
        {
            TutorMasterDBEntities3 db = new TutorMasterDBEntities3();
            string[] commits = (from row in db.Commitments.AsEnumerable() where row.ID == id.ToString() select row.StartTime).ToArray();
            //string[] types = (from row in db.Commitments.AsEnumerable() where row.ID == id.ToString() select row.T
            int numCommits = commits.Count();
            for (int i = 0; i < numCommits; i++)
            {
                string[] objectArray = commits[i].Split(' '); //split up the string date time by spaces, there should be 3 objects in array
                string[] dateArray = objectArray[0].Split('/'); //split date by slashes
                string[] timeArray = objectArray[1].Split(':'); //split time by colons
                DateTime date = new DateTime(2017, Convert.ToInt32(dateArray[0]), Convert.ToInt32(dateArray[1]), 
                    Convert.ToInt32(timeArray[0]), Convert.ToInt32(timeArray[1]), 0); //load a dateTime object
                string day = getDay(date);
            }
            //lvSunday.Items.Add(new ListViewItem(new string[] { startTime.ToShortTimeString(), endTime.ToShortTimeString(), "open" }));
            //lvSunday.Refresh();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Login g = new Login();
            g.Show();
            this.Close();
        }


        private void btnAddOpenBlock_Click(object sender, EventArgs e)
        {
            //first, error check to make sure that the user put something for each dropdownbox
            if ((string.IsNullOrWhiteSpace(combStartDay.Text)) || (string.IsNullOrWhiteSpace(combStartHour.Text))
            || (string.IsNullOrWhiteSpace(combStartMinute.Text) || (string.IsNullOrWhiteSpace(combStartAmPm.Text)))
            || (string.IsNullOrWhiteSpace(combEndDay.Text)) || (string.IsNullOrWhiteSpace(combEndHour.Text))
            || (string.IsNullOrWhiteSpace(combEndMinute.Text)) || (string.IsNullOrWhiteSpace(combEndAmPm.Text)))
            {
                MessageBox.Show("Please fill out a starting and ending day, hour, minute, and part of day");
            }
            else
            {
                string stringStartDay = combStartDay.Text;
                int intStartDay = getDayIndex(stringStartDay);
                int startHour = int.Parse(combStartHour.Text);
                int startMinute = int.Parse(combStartMinute.Text);
                string startAmPm = combStartAmPm.Text;

                if (startAmPm == "PM")
                {
                    startHour += 12;
                }

                string stringEndDay = combEndDay.Text;
                int intEndDay = getDayIndex(stringEndDay);
                int endHour = int.Parse(combEndHour.Text);
                int endMinute = int.Parse(combEndMinute.Text);
                string endAmPm = combEndAmPm.Text;

                if (endAmPm == "PM")
                {
                    endHour += 12;
                }

                bool weekly = cbxWeekly.Checked;

                DateTime startTime = new DateTime(2017, 1, intStartDay, startHour, startMinute, 0);
                DateTime endTime = new DateTime(2017, 1, intEndDay, endHour, endMinute, 0);
                getAvail(startTime, endTime, weekly);
            }
        }

        private int getDayIndex(string day)
        {
            int numDay = 0;
            switch (day)
            {
                case "Sunday":
                    numDay = 1;
                    break;
                case "Monday":
                    numDay = 2;
                    break;
                case "Tuesday":
                    numDay = 3;
                    break;
                case "Wednesday":
                    numDay = 4;
                    break;
                case "Thursday":
                    numDay = 5;
                    break;
                case "Friday":
                    numDay = 6;
                    break;
                case "Saturday":
                    numDay = 7;
                    break;
            }
            return numDay;
        }

        private void getAvail(DateTime startTime, DateTime endTime, bool weekly)
        {
            DateTime begin = startTime;
            int compare = begin.CompareTo(endTime);
            
            while (compare < 0) //if the first date is less than the second date
            {
                if (!recordedTime(begin))
                {
                    add15Block(begin, weekly);   
                } 
                begin = begin.AddMinutes(15);
                compare = begin.CompareTo(endTime);
            }
            
        }

        private bool recordedTime(DateTime begin)
        {
            TutorMasterDBEntities3 db = new TutorMasterDBEntities3();
            bool found = false;
            var storedCommits = (from row in db.Commitments.AsEnumerable() where row.ID == id.ToString() select row.StartTime).ToArray();
            int numCommits = storedCommits.Length;

            for(int i = 0; i < numCommits; i++)
            {
                if (begin.ToString() == storedCommits[i])
                {
                    found = true;
                }
            }
            return found;
        }

        private void add15Block(DateTime begin, bool weekly)
        {
            int lastRow = getNextCmtId();

            TutorMaster.Commitment newCommit = new TutorMaster.Commitment();
            newCommit.CmtID = lastRow;
            newCommit.StartTime = begin.ToString();
            newCommit.Class = "null";
            newCommit.Open = "open";
            newCommit.Tutoring = "null";
            newCommit.Location = "null";
            newCommit.Weekly = weekly.ToString();
            newCommit.ID = id.ToString();
            addCommit(newCommit);

            DateTime endOfSemester = new DateTime(2017, 5, 1, 0, 0, 0);

            if (weekly)
            {
                while (begin.AddDays(7).CompareTo(endOfSemester) < 0)
                {
                    begin = begin.AddDays(7);
                    lastRow += 1;
                    TutorMaster.Commitment newCommitW = new TutorMaster.Commitment();
                    newCommitW.CmtID = lastRow;
                    newCommitW.StartTime = begin.ToString();
                    newCommitW.Class = "null";
                    newCommitW.Open = "open";
                    newCommitW.Tutoring = "null";
                    newCommitW.Location = "null";
                    newCommitW.Weekly = weekly.ToString();
                    newCommitW.ID = id.ToString();
                    addCommit(newCommitW);
                }
            }
            loadAvail();

            /*string[] st = begin.ToString("D").Split(',');
            string startDay = st[0];
            switch (startDay)
            {
                case "Sunday":

                    break;
                case "Monday":

                    break;
                case "Tuesday":

                    break;
                case "Wednesday":

                    break;
                case "Thursday":

                    break;
                case "Friday":

                    break;
                case "Saturday":

                    break;
            }*/
        }

        private void addCommit(TutorMaster.Commitment commit)
        {
            TutorMasterDBEntities3 db = new TutorMasterDBEntities3();
            db.Commitments.AddObject(commit);
            db.SaveChanges();
        }

        private string getDay(DateTime date)
        {
            string[] st = date.ToString("D").Split(',');
            string day = st[0];
            return day;
        }

        private int getNextCmtId()
        {
            TutorMasterDBEntities3 db = new TutorMasterDBEntities3();
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

        private void populateColumns()
        {
            lvSunday.CheckBoxes = true;
            lvSunday.Columns.Add("     Start Time", 125);
            lvSunday.Columns.Add("End Time", 125);
            lvSunday.Columns.Add("Type", 90);

            lvMonday.CheckBoxes = true;
            lvMonday.Columns.Add("     Start Time", 125);
            lvMonday.Columns.Add("End Time", 125);
            lvMonday.Columns.Add("Type", 95);

            lvTuesday.CheckBoxes = true;
            lvTuesday.Columns.Add("     Start Time", 125);
            lvTuesday.Columns.Add("End Time", 125);
            lvTuesday.Columns.Add("Type", 95);

            lvWednesday.CheckBoxes = true;
            lvWednesday.Columns.Add("     Start Time", 125);
            lvWednesday.Columns.Add("End Time", 125);
            lvWednesday.Columns.Add("Type", 95);

            lvThursday.CheckBoxes = true;
            lvThursday.Columns.Add("     Start Time", 125);
            lvThursday.Columns.Add("End Time", 125);
            lvThursday.Columns.Add("Type", 95);

            lvFriday.CheckBoxes = true;
            lvFriday.Columns.Add("     Start Time", 125);
            lvFriday.Columns.Add("End Time", 125);
            lvFriday.Columns.Add("Type", 95);

            lvSaturday.CheckBoxes = true;
            lvSaturday.Columns.Add("     Start Time", 125);
            lvSaturday.Columns.Add("End Time", 125);
            lvSaturday.Columns.Add("Type", 95);
        }
    }
}