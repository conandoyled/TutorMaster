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
    public partial class RemoveAvailForm : Form
    {
        private int id;
        
        //constructor
        public RemoveAvailForm(int accID, List<string>removeList)
        {
            id = accID;
            InitializeComponent();
            populateColumns();
            loadListView(removeList);
        }

        private bool startEarlierThanEnd(DateTime startTime, DateTime endTime)
        {
            return startTime.CompareTo(endTime) < 0;
        }

        private void loadListView(List<string> removeList)
        {//this function is made to load the listviews with times of prospective desired remove times
            for (int i = 0; i < removeList.Count(); i++)
            {
                DateTime startTime = getDate(removeList[i].Split(',')[0]);                 //get the start time
                DateTime endTime = getDate(removeList[i].Split(',')[1]);                   //get the end time
                while (startEarlierThanEnd(startTime, endTime))                            //if the start time is before the end time, add the strings to the listviews
                {
                    lvTimeSlots.Items.Add(new ListViewItem(new string[] {startTime.ToString(), startTime.AddMinutes(15).ToString(), removeList[i].Split(',')[2] }));
                    startTime = startTime.AddMinutes(15);                                  //add the next 15 minute time block
                }
            }
        }

        private DateTime getDate(string day)
        {
            string totalDate = day.Split(' ')[0];                                          //get the date part of the string
            int month = Convert.ToInt32(totalDate.Split('/')[0]);                          //get the month part of the string
            int date = Convert.ToInt32(totalDate.Split('/')[1]);                           //get the date number of the string
            int year = Convert.ToInt32(totalDate.Split('/')[2]);                           //get the year number of the string

            string time = day.Split(' ')[1];                                               //get the time part of the string
            int hour = Convert.ToInt32(time.Split(':')[0]);                                //get the hour number from the time string
            int min = Convert.ToInt32(time.Split(':')[1]);                                 //get the minute number from the time string
            string amPm = day.Split(' ')[2];                                               //get whether this is in the morning or evening

            if (amPm == "PM" && hour != 12)                                                //if evening and not 12, then add 12
            {
                hour += 12;
            }
            else if (amPm == "AM" && hour == 12)                                           //if 12AM, then set hour to 0
            {
                hour = 0;
            }

            DateTime result = new DateTime(year, month, date, hour, min, 0);               //return the datetime
            return result;
        }

        private List<DateTime> getStartTimes()
        {//the purpose of this function is to get the starttimes from the checked items of the listviews
            List<DateTime> result = new List<DateTime>();
            
            for (int n = 0; n < lvTimeSlots.CheckedItems.Count; n++)                             //go through each of the checked items
            {
                result.Add(getDate(lvTimeSlots.CheckedItems[n].SubItems[0].Text.ToString()));    //add the start time date to the list
            }
            
            return result;                                                                       //return the desired list
        }

        private bool weeklyAndFound(Commitment commit, List<DateTime> searchList)
        {//this function checks if a commitment is weekly and found in the commitment list
            return commit.Weekly == true && BinarySearch(searchList, Convert.ToDateTime(commit.StartTime));
        }

        private bool weekBackEarlier(DateTime weekBack, Commitment commit)
        {//this function sees if the the weekback dateTime is before the commitment time
            return DateTime.Compare(weekBack, Convert.ToDateTime(commit.StartTime)) < 0;
        }

        private bool sameTime(Commitment commit, DateTime weekBack)
        {//this funciton sees if the the commitment is the sametime in the weekback
            return DateTime.Compare(Convert.ToDateTime(commit.StartTime), weekBack) == 0;
        }

        private bool endOfSemesIsLater(DateTime endSemes, DateTime weekForward)
        {
            return DateTime.Compare(endSemes, weekForward) > 0;
        }

        private bool forwardEarlierThanStart(DateTime weekForward, Commitment commit)
        {
            return DateTime.Compare(weekForward, Convert.ToDateTime(commit.StartTime)) < 0;
        }

        private void setPreviousWeekliesToFalse()
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();                                         //connect to the database

            List<Commitment> cmtList = (from stucmt in db.StudentCommitments                                  //get the commit list of the signed in student
                                        where stucmt.ID == id
                                        join cmt in db.Commitments on stucmt.CmtID equals cmt.CmtID
                                        select cmt).ToList();
            
            List<DateTime> searchList = new List<DateTime>();                                                 //initialize search list
            
            searchList = getStartTimes();                                                                     //get the startTimes from the listview

            for (int i = 0; i < cmtList.Count(); i++)                                                         //for each commitment in the commit list
            {
                if (weeklyAndFound(cmtList[i], searchList))                                                   //if the commitment is in the search list and weekly
                {
                    DateTime startSemes = new DateTime(2017, 1, 1, 0, 0, 0);
                    DateTime weekBack = Convert.ToDateTime(cmtList[i].StartTime).AddDays(-7);                 //go a week back in time
                    while (DateTime.Compare(startSemes, weekBack) <= 0)                                       //perform a binary search here
                    {
                        bool found = false;
                        int first = 0;
                        int last = cmtList.Count() - 1;
                        while (first <= last && !found)
                        {
                            int midpoint = (first + last) / 2;
                            if (sameTime(cmtList[midpoint], weekBack))                                        //if you find the weekBack date time
                            {
                                if (cmtList[midpoint].Open == true)                                           //if the commitment is open
                                {
                                    cmtList[midpoint].Weekly = false;                                         //set its weekly to false
                                    db.SaveChanges();                                                         //save the changes to the database
                                }
                                found = true;
                            }
                            else
                            {
                                if (weekBackEarlier(weekBack, cmtList[i]))                                    //if weekback is earlier, search first half of list
                                {
                                    last = midpoint - 1;
                                }
                                else                                                                          //else, search the second half of the list
                                {
                                    first = midpoint + 1;
                                }
                            }
                        }
                        weekBack = weekBack.AddDays(-7);                                                      //go a week back in time
                    }
                }
            }
        }

        private void deleteAvail()
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();                                        //connect to the database
            
            List<Commitment> cmtList = (from stucmt in db.StudentCommitments                                 //get the student's commitments
                                        where stucmt.ID == id
                                        join cmt in db.Commitments on stucmt.CmtID equals cmt.CmtID
                                        select cmt).ToList();

            QuickSort(ref cmtList, cmtList.Count());                                                         //sort the list by DateTime

            List<DateTime> searchList = new List<DateTime>();
            
            searchList = getStartTimes();                                                                    //get the starttimes from the listview                                  
            
            for (int i = 0; i < cmtList.Count(); i++)
            {
                if (BinarySearch(searchList, Convert.ToDateTime(cmtList[i].StartTime)))
                {
                    if (cmtList[i].Weekly == true)
                    {//ask the user if they want to delete the weekly commitment through the end of the semester
                        DialogResult choice = MessageBox.Show("Would you like to delete " + cmtList[i].StartTime.ToString()  + " slot until the end of the semester?", "Delete weekly timeslot?", MessageBoxButtons.YesNo);
                        if (choice == DialogResult.Yes)                                                       //if the user says yes
                        {
                            DateTime endSemes = new DateTime(2017, 5, 1, 0, 0, 0);                            //get end of semester
                            DateTime weekForward = Convert.ToDateTime(cmtList[i].StartTime).AddDays(7);       //go a week forward
                            while (endOfSemesIsLater(endSemes, weekForward))                                  //if the end of the semester is later than our commitment start Time
                            {                                                                                 //run a binary search
                                bool found = false;
                                int first = 0;
                                int last = cmtList.Count() - 1;
                                while (first <= last && !found)
                                {
                                    int midpoint = (first + last) / 2;
                                    if (sameTime(cmtList[midpoint], weekForward))                             //if commitment time and weekforward time are the same
                                    {
                                        if (cmtList[midpoint].Open == true)                                   //and if the midpoint commitment is open
                                        {
                                            db.Commitments.DeleteObject(cmtList[midpoint]);                   //delete the commitment from the database
                                            cmtList.Remove(cmtList[midpoint]);                                //remove it from the commit list as well
                                            db.SaveChanges();
                                        }
                                        found = true;                                                         //say we found what we were looking for
                                        break;                                                                //break out of the search
                                    }
                                    else
                                    {
                                        if (forwardEarlierThanStart(weekForward, cmtList[midpoint]))
                                        {
                                            last = midpoint - 1;
                                        }
                                        else
                                        {
                                            first = midpoint + 1;
                                        }
                                    }
                                }
                                weekForward = weekForward.AddDays(7);
                            }
                        }
                        else if (choice == DialogResult.No)
                        {

                        }
                    }
                    searchList.Remove(Convert.ToDateTime(cmtList[i].StartTime));
                    db.Commitments.DeleteObject(cmtList[i]);
                    i--;
                    db.SaveChanges();
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            setPreviousWeekliesToFalse();                                //set the previous weeklies to false
            deleteAvail();                                               //delete the future weeklies
            
            for (int c = 0; c < lvTimeSlots.CheckedItems.Count; c++)     //remove all of the selected time slots from the listview
            {
                lvTimeSlots.CheckedItems[c].Remove();
                c--;
            }

            StudentMain g = new StudentMain(id);                         //send the user back to the student main
            g.Show();
            this.Close();
        }


        //binary search to search by dateTime in a commitment list
        private bool BinarySearch(List<DateTime> cmtList, DateTime commit)
        {
            bool found = false;
            int first = 0;
            int last = cmtList.Count() - 1;
            while (first <= last && !found)
            {
                int midpoint = (first + last) / 2;
                if (DateTime.Compare(cmtList[midpoint], commit) == 0)
                {
                    found = true;
                    return found;
                }
                else
                {
                    if (DateTime.Compare(commit, cmtList[midpoint]) < 0)
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

        private void populateColumns()
        {//populate the columns and give each of them a checkbox
            lvTimeSlots.CheckBoxes = true;
            lvTimeSlots.Columns.Add("Start Time", 150);
            lvTimeSlots.Columns.Add("End Time", 150);
            lvTimeSlots.Columns.Add("Weekly", 75);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            StudentMain g = new StudentMain(id);
            g.Show();
            this.Close();
        }

        private void btnDeselectAll_Click(object sender, EventArgs e)
        {//deselect everything in the listview
            for(int i = 0; i < lvTimeSlots.Items.Count; i++)
            {
                lvTimeSlots.Items[i].Checked = false;
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {//select everything in the listview
            for(int i = 0; i < lvTimeSlots.Items.Count; i++)
            {
                lvTimeSlots.Items[i].Checked = true;
            }
        }

    }
}
