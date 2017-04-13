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
        public RemoveAvailForm(int accID, List<string>removeList)
        {
            id = accID;
            InitializeComponent();
            populateColumns();
            loadListView(removeList);
        }

        private void loadListView(List<string> removeList)
        {
            for (int i = 0; i < removeList.Count(); i++)
            {
                DateTime startTime = getDate(removeList[i].Split(',')[0]);
                DateTime endTime = getDate(removeList[i].Split(',')[1]);
                while (startTime.CompareTo(endTime) < 0)
                {
                    lvTimeSlots.Items.Add(new ListViewItem(new string[] {startTime.ToString(), startTime.AddMinutes(15).ToString(), removeList[i].Split(',')[2] }));
                    startTime = startTime.AddMinutes(15);
                }
            }
        }

        private DateTime getDate(string day)
        {
            string totalDate = day.Split(' ')[0];
            int month = Convert.ToInt32(totalDate.Split('/')[0]);
            int date = Convert.ToInt32(totalDate.Split('/')[1]);
            int year = Convert.ToInt32(totalDate.Split('/')[2]);

            string time = day.Split(' ')[1];
            int hour = Convert.ToInt32(time.Split(':')[0]);
            int min = Convert.ToInt32(time.Split(':')[1]);
            string amPm = day.Split(' ')[2];

            if (amPm == "PM" && hour != 12)
            {
                hour += 12;
            }
            else if (amPm == "AM" && hour == 12)
            {
                hour = 0;
            }

            DateTime result = new DateTime(year, month, date, hour, min, 0);
            return result;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();

            List<Commitment> cmtList = (from stucmt in db.StudentCommitments
                                        where stucmt.ID == id
                                        join cmt in db.Commitments on stucmt.CmtID equals cmt.CmtID
                                        select cmt).ToList();

            QuickSort(ref cmtList, cmtList.Count());                                                         //sort the list by DateTime

            List<DateTime> searchList = new List<DateTime>();

            for(int n = 0; n < lvTimeSlots.CheckedItems.Count; n++)
            {
                searchList.Add(getDate(lvTimeSlots.CheckedItems[n].SubItems[0].Text.ToString()));
            }

            for (int i = 0; i < cmtList.Count(); i++)
            {
                if (cmtList[i].Weekly == true && BinarySearch(searchList, Convert.ToDateTime(cmtList[i].StartTime)))
                {
                    DateTime startSemes = new DateTime(2017, 1, 1, 0, 0, 0);
                    DateTime weekBack = Convert.ToDateTime(cmtList[i].StartTime).AddDays(-7);
                    while (DateTime.Compare(startSemes, weekBack) <= 0)
                    {
                        bool found = false;
                        int first = 0;
                        int last = cmtList.Count() - 1;
                        while (first <= last && !found)
                        {
                            int midpoint = (first + last) / 2;
                            if (DateTime.Compare(Convert.ToDateTime(cmtList[midpoint].StartTime), weekBack) == 0)
                            {
                                if (cmtList[midpoint].Open == true)
                                {
                                    cmtList[midpoint].Weekly = false;
                                    db.SaveChanges();
                                }
                                found = true;
                            }
                            else
                            {
                                if (DateTime.Compare(weekBack, Convert.ToDateTime(cmtList[midpoint].StartTime)) < 0)
                                {
                                    last = midpoint - 1;
                                }
                                else
                                {
                                    first = midpoint + 1;
                                }
                            }
                        }
                        weekBack = weekBack.AddDays(-7);
                    }
                }
                if (BinarySearch(searchList, Convert.ToDateTime(cmtList[i].StartTime)))
                {
                    if (cmtList[i].Weekly == true)
                    {
                        DialogResult choice = MessageBox.Show("Would you like to delete this time slot until the end of the semester?", "Delete weekly timeslot?", MessageBoxButtons.YesNo);
                        if (choice == DialogResult.Yes)
                        {
                            MessageBox.Show(cmtList[i].StartTime.ToString());
                            DateTime endSemes = new DateTime(2017, 5, 1, 0, 0, 0);
                            DateTime weekFoward = Convert.ToDateTime(cmtList[i].StartTime).AddDays(7);
                            while (DateTime.Compare(endSemes, weekFoward) > 0)
                            {
                                //MessageBox.Show(cmtList[i].StartTime.ToString());
                                bool found = false;
                                int first = 0;
                                int last = cmtList.Count() - 1;
                                while (first <= last && !found)
                                {
                                    int midpoint = (first + last) / 2;
                                    //MessageBox.Show((DateTime.Compare(Convert.ToDateTime(cmtList[midpoint].StartTime), weekFoward)).ToString());
                                    if (DateTime.Compare(Convert.ToDateTime(cmtList[midpoint].StartTime), weekFoward) == 0)
                                    {
                                        if (cmtList[midpoint].Open == true)
                                        {
                                            //MessageBox.Show("Deleting " + cmtList[midpoint].StartTime.ToString());
                                            db.Commitments.DeleteObject(cmtList[midpoint]);
                                            cmtList.Remove(cmtList[midpoint]);
                                            db.SaveChanges();
                                            //i--;
                                        }
                                        found = true;
                                        break;
                                    }
                                    else
                                    {
                                        if (DateTime.Compare(weekFoward, Convert.ToDateTime(cmtList[midpoint].StartTime)) < 0)
                                        {
                                            last = midpoint - 1;
                                        }
                                        else
                                        {
                                            first = midpoint + 1;
                                        }
                                    }
                                }
                                weekFoward = weekFoward.AddDays(7);
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

            for (int c = 0; c < lvTimeSlots.CheckedItems.Count; c++)
            {
                lvTimeSlots.CheckedItems[c].Remove();
                c--;
            }

            StudentMain g = new StudentMain(id);
            g.Show();
            this.Close();
        }



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
        {
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
        {
            for(int i = 0; i < lvTimeSlots.Items.Count; i++)
            {
                lvTimeSlots.Items[i].Checked = false;
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < lvTimeSlots.Items.Count; i++)
            {
                lvTimeSlots.Items[i].Checked = true;
            }
        }

    }
}
