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

        private void populateColumns()
        {
            lvSunday.CheckBoxes = true;
            lvSunday.Columns.Add("     Start Time", 100);
            lvSunday.Columns.Add("End Time", 100);
            lvSunday.Columns.Add("Type", 100);
        }

        private void loadAvail()
        {

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


                string stringEndDay = combEndDay.Text;
                int intEndDay = getDayIndex(stringEndDay);

                int endHour = int.Parse(combEndHour.Text);
                int endMinute = int.Parse(combEndMinute.Text);
                string endAmPm = combEndAmPm.Text;

                MessageBox.Show(DateTime.Now.ToString("D"));
                DateTime startTime = new DateTime(2017, 1, intStartDay, 20, startMinute, 0);
                MessageBox.Show(startTime.ToString());
                DateTime endTime = new DateTime(2017, 1, intEndDay, endHour, endMinute, 0);
                //getAvail(startTime, endTime);
                //MessageBox.Show(startTime.ToString());
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

        private void getAvail(DateTime startTime, DateTime endTime)
        {
            DateTime begin = startTime;
            DateTime fifteen = startTime;
            fifteen = fifteen.AddMinutes(15);
            int compare = begin.CompareTo(endTime);

            while (compare < 0) //if the first date is less than the second date
            {
                if (!recordedTime(begin))
                {
                    add15Block(begin);
                    begin = begin.AddMinutes(15);
                    fifteen = fifteen.AddMinutes(15);
                }
                compare = begin.CompareTo(endTime);
            }
            lvSunday.Items.Add(new ListViewItem(new string[] { startTime.ToShortTimeString(), endTime.ToShortTimeString(), "open" }));
            lvSunday.Refresh();
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

        private void add15Block(DateTime begin)
        {
            TutorMasterDBEntities3 db = new TutorMasterDBEntities3();
            int lastRow = 0;
            try
            {
                lastRow = db.Commitments.OrderByDescending(u => u.CmtID).Select(r => r.CmtID).First();
            }
            catch(InvalidOperationException e)
            {
                lastRow = 1;
            }

            TutorMaster.Commitment newCommit = new TutorMaster.Commitment();
            newCommit.CmtID = lastRow;
            newCommit.StartTime = begin.ToString();
            newCommit.Open = "open";
            newCommit.Location = "null";
            newCommit.ID = id.ToString();
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
    }
}