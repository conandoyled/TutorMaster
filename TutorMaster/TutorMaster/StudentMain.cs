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
        private int count = 0;
        public StudentMain(int accID)
        {
            id = accID;
            InitializeComponent();
            populateColumns();
            //loadAvail();
        }

        private void loadAvail()
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            var studentCommits = (from row in db.StudentCommitments.AsEnumerable() where row.ID == id select row.CmtID).ToArray();
            DateTime start = new DateTime(2017, 1, 1, 0, 0, 0);
            List<TutorMaster.Commitment> cmtList = new List<TutorMaster.Commitment>();
            for (int j = 0; j < studentCommits.Count(); j++)
            {
                TutorMaster.Commitment commit = (from row in db.Commitments.AsEnumerable() where row.CmtID == studentCommits[j] select row).First();
                cmtList.Add(commit);
            }
            QuickSort(ref cmtList, cmtList.Count());
            


            string today = getDay(Convert.ToDateTime(cmtList[0].StartTime));
            string startTime = Convert.ToDateTime(cmtList[0].StartTime).ToString().Split(' ')[1];
            MessageBox.Show(startTime);
            //string todayPlusFifteen = 
            
            /*for (int i = 0; i < cmtList.Count()-1; i++)
            {
                DateTime date = Convert.ToDateTime(cmtList[i].StartTime);
                DateTime testFifteen = Convert.ToDateTime(cmtList[i+1].StartTime);
                
                if(DateTime.Compare(date, start.AddDays(7)) < 0)
                {
                    string day = getDay(date);
                    if (today == day)
                    {
                        
                        if (DateTime.Compare(testFifteen, date.AddMinutes(15)) == 0)
                        {
                            addToListView(cmtList[i], day);
                        }
                    }
                    else
                    {
                        today = day;
                    }

                }
            }*/
        }

        private void addToListView(TutorMaster.Commitment commit, string day)
        {
            switch (day)
            {
                case "Sunday":
                    //lvSunday.Items.Add(new ListViewItem(new string[] { commit.StartTime, user.LastName, user.FirstName, tutor, tutee, user.Email, user.PhoneNumber }));
                    break;
            }
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
            loadAvail();
        }

        private bool recordedTime(DateTime begin)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            bool found = false;
            var storedCommits = (from row in db.StudentCommitments.AsEnumerable() where row.ID == id select row.CmtID).ToArray();
            int numCommits = storedCommits.Length;
            List<DateTime> date = new List<DateTime>();
            
            for (int j = 0; j < numCommits; j++)
            {
                date.Add(Convert.ToDateTime((from row in db.Commitments.AsEnumerable() where row.CmtID == storedCommits[j] select row.StartTime).First()));
            }

            int dateCount = date.Count();

            for(int i = 0; i < dateCount; i++)
            {
                if (begin == date[i])
                {
                    found = true;
                    MessageBox.Show(date[i].ToString() + " is already in the database, this will not be added");
                }
            }
            return found;
        }

        private void add15Block(DateTime begin, bool weekly)
        {
            int lastCR = getNextCmtId();     //last commit row
            int lastSCR = getNextStdCmtKey();//last student commit row

            
            //add the first student committment and comittment in case the commitment is not weekly
            
            
            TutorMaster.Commitment newCommit = new TutorMaster.Commitment();
            newCommit.CmtID = lastCR;
            newCommit.StartTime = begin;
            newCommit.Open = true;
            newCommit.Weekly = weekly;
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
                    
                    TutorMaster.Commitment newCommitW = new TutorMaster.Commitment();
                    newCommitW.CmtID = lastCR;
                    newCommitW.StartTime = begin;
                    newCommitW.Open = true;
                    newCommitW.Weekly = weekly;
                    addCommit(newCommitW);
                    
                    TutorMaster.StudentCommitment newStudentCommitW = new TutorMaster.StudentCommitment();
                    newStudentCommitW.CmtID = lastCR;
                    newStudentCommitW.ID = id;
                    newStudentCommitW.Key = lastSCR;
                    addStudentCommit(newStudentCommitW);
                }
            }

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

        private string getDay(DateTime date)
        {
            string[] st = date.ToString("D").Split(',');
            string day = st[0];
            return day;
        }

        private int getNextCmtId()
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

        private int getNextStdCmtKey()
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            int rowNum = db.StudentCommitments.Count();
            //MessageBox.Show(rowNum.ToString());
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

        private void populateColumns()
        {
            lvSunday.CheckBoxes = true;
            lvSunday.Columns.Add("Start Time", 75);
            lvSunday.Columns.Add("End Time", 75);
            lvSunday.Columns.Add("Class", 75);
            lvSunday.Columns.Add("Location", 100);
            lvSunday.Columns.Add("Open", 50);
            lvSunday.Columns.Add("Tutoring", 75);
            lvSunday.Columns.Add("Weekly", 75);
            lvSunday.Columns.Add("Partner", 100);

            lvMonday.CheckBoxes = true;
            lvMonday.Columns.Add("Start Time", 75);
            lvMonday.Columns.Add("End Time", 75);
            lvMonday.Columns.Add("Class", 75);
            lvMonday.Columns.Add("Location", 75);
            lvMonday.Columns.Add("Open", 75);
            lvMonday.Columns.Add("Tutoring", 75);
            lvMonday.Columns.Add("Weekly", 75);
            lvMonday.Columns.Add("Partner", 75);

            lvTuesday.CheckBoxes = true;
            lvTuesday.Columns.Add("Start Time", 75);
            lvTuesday.Columns.Add("End Time", 75);
            lvTuesday.Columns.Add("Class", 75);
            lvTuesday.Columns.Add("Location", 75);
            lvTuesday.Columns.Add("Open", 75);
            lvTuesday.Columns.Add("Tutoring", 75);
            lvTuesday.Columns.Add("Weekly", 75);
            lvTuesday.Columns.Add("Partner", 75);

            lvWednesday.CheckBoxes = true;
            lvWednesday.Columns.Add("Start Time", 75);
            lvWednesday.Columns.Add("End Time", 75);
            lvWednesday.Columns.Add("Class", 75);
            lvWednesday.Columns.Add("Location", 75);
            lvWednesday.Columns.Add("Open", 75);
            lvWednesday.Columns.Add("Tutoring", 75);
            lvWednesday.Columns.Add("Weekly", 75);
            lvWednesday.Columns.Add("Partner", 75);

            lvThursday.CheckBoxes = true;
            lvThursday.Columns.Add("Start Time", 75);
            lvThursday.Columns.Add("End Time", 75);
            lvThursday.Columns.Add("Class", 75);
            lvThursday.Columns.Add("Location", 75);
            lvThursday.Columns.Add("Open", 75);
            lvThursday.Columns.Add("Tutoring", 75);
            lvThursday.Columns.Add("Weekly", 75);
            lvThursday.Columns.Add("Partner", 75);

            lvFriday.CheckBoxes = true;
            lvFriday.Columns.Add("Start Time", 75);
            lvFriday.Columns.Add("End Time", 75);
            lvFriday.Columns.Add("Class", 75);
            lvFriday.Columns.Add("Location", 75);
            lvFriday.Columns.Add("Open", 75);
            lvFriday.Columns.Add("Tutoring", 75);
            lvFriday.Columns.Add("Weekly", 75);
            lvFriday.Columns.Add("Partner", 75);

            lvSaturday.CheckBoxes = true;
            lvSaturday.Columns.Add("Start Time", 75);
            lvSaturday.Columns.Add("End Time", 75);
            lvSaturday.Columns.Add("Class", 75);
            lvSaturday.Columns.Add("Location", 75);
            lvSaturday.Columns.Add("Open", 75);
            lvSaturday.Columns.Add("Tutoring", 75);
            lvSaturday.Columns.Add("Weekly", 75);
            lvSaturday.Columns.Add("Partner", 75);
        }


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
            /*Swap the median and first committments in the list*/
            TutorMaster.Commitment temp = values[first];
            values[first] = values[median];
            values[median] = temp;

            valueF = Convert.ToDateTime(values[first].StartTime); //get current first datetime
            valueC = Convert.ToDateTime(values[center].StartTime);//get current center datetime;
            valueL = Convert.ToDateTime(values[last].StartTime);

            TutorMaster.Commitment splitVal = values[first];
            DateTime splitDate =  Convert.ToDateTime(values[first].StartTime);
            
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
            MessageBox.Show("Finish number: " + count.ToString());
            for (int i = 0; i < values.Count(); i++)
            {
                MessageBox.Show(values[i].StartTime.ToString());
            }

        }

        private void QuickSort2(ref List<TutorMaster.Commitment> values, int first, int last)
        {
            if (first < last)
            {
                int splitPoint = -99;

                Split(ref values, first, last, ref splitPoint);
                QuickSort2(ref values, first, splitPoint-1);
                QuickSort2(ref values, splitPoint + 1, last);
            }
        }

        private void QuickSort(ref List<TutorMaster.Commitment> values, int numValues)
        {
            QuickSort2(ref values, 0, numValues - 1);
        }
    }
}