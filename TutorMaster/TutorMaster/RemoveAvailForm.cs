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
        private bool admin;

        //constructor
        public RemoveAvailForm(int accID, List<string>removeList, bool adminP)
        {
            id = accID;
            admin = adminP;
            InitializeComponent();
            populateColumns();
            loadListView(removeList);
        }

        private void loadListView(List<string> removeList)
        {//this function is made to load the listviews with times of prospective desired remove times
            for (int i = 0; i < removeList.Count(); i++)
            {
                DateTime startTime = DateTimeMethods.getDate(removeList[i].Split(',')[0]);                 //get the start time
                DateTime endTime = DateTimeMethods.getDate(removeList[i].Split(',')[1]);                   //get the end time
                string weekly = "No";
                if (removeList[i].Split(',')[2] == "True" || removeList[i].Split(',')[2] == "Yes")
                {
                    weekly = "Yes";
                }
                while (DateTimeMethods.startEarlierThanEnd(startTime, endTime))                            //if the start time is before the end time, add the strings to the listviews
                {
                    lvTimeSlots.Items.Add(new ListViewItem(new string[] {startTime.ToString(), startTime.AddMinutes(15).ToString(), weekly}));
                    startTime = startTime.AddMinutes(15);                                  //add the next 15 minute time block
                }
            }
        }


        private List<DateTime> getStartTimes()
        {//the purpose of this function is to get the starttimes from the checked items of the listviews
            List<DateTime> result = new List<DateTime>();
            
            for (int n = 0; n < lvTimeSlots.CheckedItems.Count; n++)                             //go through each of the checked items
            {
                result.Add(DateTimeMethods.getDate(lvTimeSlots.CheckedItems[n].SubItems[0].Text.ToString()));    //add the start time date to the list
            }
            
            return result;                                                                       //return the desired list
        }

        private void setPreviousWeekliesToFalse()
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();                                         //connect to the database

            List<Commitment> cmtList = (from stucmt in db.StudentCommitments                                  //get the commit list of the signed in student
                                        where stucmt.ID == id
                                        join cmt in db.Commitments on stucmt.CmtID equals cmt.CmtID
                                        select cmt).ToList();
            
            List<DateTime> searchList = new List<DateTime>();                                                 //initialize search list

            SortsAndSearches.QuickSort(ref cmtList, cmtList.Count());
            
            searchList = getStartTimes();                                                                     //get the startTimes from the listview

            for (int i = 0; i < cmtList.Count(); i++)                                                         //for each commitment in the commit list
            {
                if (DateTimeMethods.weeklyAndFound(cmtList[i], searchList))                                                   //if the commitment is in the search list and weekly
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
                            if (DateTimeMethods.sameTime(cmtList[midpoint], weekBack))                                        //if you find the weekBack date time
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
                                if (DateTimeMethods.weekBackEarlier(weekBack, cmtList[midpoint]))                                    //if weekback is earlier, search first half of list
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

        private void deleteAvail(bool week)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();                                        //connect to the database

            List<Commitment> cmtList = (from stucmt in db.StudentCommitments                                 //get the student's commitments
                                        where stucmt.ID == id
                                        join cmt in db.Commitments on stucmt.CmtID equals cmt.CmtID
                                        select cmt).ToList();

            SortsAndSearches.QuickSort(ref cmtList, cmtList.Count());                                                         //sort the list by DateTime

            List<DateTime> searchList = new List<DateTime>();

            searchList = getStartTimes();                                                                    //get the starttimes from the listview                                  

            if (week)
            {
                for (int i = 0; i < cmtList.Count(); i++)
                {
                    if (SortsAndSearches.BinarySearch(searchList, Convert.ToDateTime(cmtList[i].StartTime)))
                    {
                        if (cmtList[i].Weekly == true)
                        {//ask the user if they want to delete the weekly commitment through the end of the semester
                            DateTime endSemes = new DateTime(2017, 5, 1, 0, 0, 0);                            //get end of semester
                            DateTime weekForward = Convert.ToDateTime(cmtList[i].StartTime).AddDays(7);       //go a week forward
                            while (DateTimeMethods.endOfSemesIsLater(endSemes, weekForward))                                  //if the end of the semester is later than our commitment start Time
                            {                                                                                 //run a binary search
                                bool found = false;
                                int first = 0;
                                int last = cmtList.Count() - 1;
                                while (first <= last && !found)
                                {
                                    int midpoint = (first + last) / 2;
                                    if (DateTimeMethods.sameTime(cmtList[midpoint], weekForward))                             //if commitment time and weekforward time are the same
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
                                        if (DateTimeMethods.forwardEarlierThanStart(weekForward, cmtList[midpoint]))
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

                        searchList.Remove(Convert.ToDateTime(cmtList[i].StartTime));
                        db.Commitments.DeleteObject(cmtList[i]);
                        i--;
                        db.SaveChanges();
                    }
                }
                MessageBox.Show("The checked 15 minute time blocks have been removed from your availability until the end of the semster.");
            }
            else
            {
                for (int i = 0; i < cmtList.Count(); i++)
                {
                    if (SortsAndSearches.BinarySearch(searchList, Convert.ToDateTime(cmtList[i].StartTime)))
                    {
                        searchList.Remove(Convert.ToDateTime(cmtList[i].StartTime));
                        db.Commitments.DeleteObject(cmtList[i]);
                        i--;
                        db.SaveChanges();
                    }
                }
                MessageBox.Show("Only the checked 15 minute time blocks have been removed from your availability.");
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            DialogResult sureDelete = MessageBox.Show("Are you sure you want to delete all of the selected timeslots?", "Deletion confirmation", MessageBoxButtons.YesNo);
            if (sureDelete == DialogResult.Yes)
            {
                bool weeklyChoice = checkChecked();
                setPreviousWeekliesToFalse();                                //set the previous weeklies to false
                if (weeklyChoice)
                {
                    DialogResult choice = MessageBox.Show("Would you like to delete the weekly time slots until the end of the semester?", "Delete weekly timeslots?", MessageBoxButtons.YesNo);
                    if (choice == DialogResult.Yes)                                                       //if the user says yes
                    {
                        deleteAvail(true);                                  //delete the time block and all of its weekly future ones
                    }
                    else if (choice == DialogResult.No)                     //if they say no
                    {
                        deleteAvail(false);                                 //delete the time slots of only the ones that have checked off
                    }
                }
                else
                {
                    deleteAvail(false);
                }

                for (int c = 0; c < lvTimeSlots.CheckedItems.Count; c++)    //remove all of the selected time slots from the listview
                {
                    lvTimeSlots.CheckedItems[c].Remove();
                    c--;
                }

                if (!admin)
                {
                    StudentMain g = new StudentMain(id);                    //send the user back to the student main
                    g.Show();
                    this.Dispose();
                }
                else
                {
                    AdminSeeSchedule g = new AdminSeeSchedule(id);         //send the user to admin
                    g.Show();
                    this.Dispose();
                }
            }
            else
            {
                MessageBox.Show("Cancellation confirmed, the selected time slots have not been deleted from your availability.");
            }
        }

        private bool checkChecked()
        {
            for (int i = 0; i < lvTimeSlots.CheckedItems.Count; i++)
            {
                if (lvTimeSlots.CheckedItems[i].SubItems[2].Text.ToString() == "Yes")
                {
                    return true;
                }
            }
            return false;
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
            if (!admin)//send the user to student main
            {
                StudentMain g = new StudentMain(id);
                g.Show();
                this.Dispose();
            }
            else//otherwise, send the user to admin
            {
                AdminSeeSchedule g = new AdminSeeSchedule(id);
                g.Show();
                this.Dispose();
            }
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

        private void RemoveAvailForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Login g = new Login();
            g.Show();
            this.Dispose();
        }

    }
}
