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
        private bool tutoring;
        private List<int> rememberStudIDs = new List<int>();
        private ListViewItem lastItemChecked;


        public AdminCreateAppointmentForm(int accID, bool tutoringP)
        {
            InitializeComponent();
            id = accID;
            tutoring = tutoringP;
            populateListview();
            hideControls();
            if (tutoring)
            {
                loadTutorOptions();
            }
            else
            {
                loadTuteeOptions();
            }
        }

        private void hideControls()
        {
            cbxStudents.Hide();
            cbxHour.Hide();
            cbxMinutes.Hide();
            cbWeekly.Hide();
            lvTimeMatches.Hide();
        }

        private void populateListview()
        {
            lvTimeMatches.CheckBoxes = true;
            lvTimeMatches.Columns.Add("Start Time", 150);
            lvTimeMatches.Columns.Add("End Time", 150);
        }

        private void loadTutorOptions()
        {
            loadTutorClassesCheckBox();
            loadStudentCheckBox();
            //loadMatchingTimeSlots();
        }

        private void loadTutorClassesCheckBox()
        {
            
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();

            List<string> approvedClasses = (from stuClass in db.StudentClasses
                                            where stuClass.ID == id
                                            join appClass in db.Classes on stuClass.ClassCode equals appClass.ClassCode
                                            select appClass.ClassName).ToList();


            for (int i = 0; i < approvedClasses.Count; i++)
            {
                cbxClasses.Items.Add(approvedClasses[i]);
            }
        }

        private void loadStudentCheckBox()
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            List<int> tuteeIds = (from row in db.Students where row.Tutee == true select row.ID).ToList();
            if (tuteeIds.Contains(id))
            {
                tuteeIds.Remove(id);
            }

            for (int i = 0; i < tuteeIds.Count; i++)
            {
                User student = (from row in db.Users.AsEnumerable() where row.ID == tuteeIds[i] select row).First();
                rememberStudIDs.Add(student.ID);
                string name = student.FirstName + " " + student.LastName;
                cbxStudents.Items.Add(name);
            }
        }

        private void loadMatchingTimeSlots()
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            bool weekly = cbWeekly.Checked;
            
            if (rememberStudIDs.Count == 0)
            {

            }
            else
            {
                DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                string classCode = (from row in db.Classes where cbxClasses.Text == row.ClassName select row.ClassCode).First();
                int sessionLength = Convert.ToInt32(cbxHour.Text) * 4 + (Convert.ToInt32(cbxMinutes.Text) / 15);

                if (sessionLength > 12 || sessionLength == 0)
                {

                }
                else
                {

                    List<TutorMaster.Commitment> tutorCommits = (from stucmt in db.StudentCommitments.AsEnumerable()
                                                                 where stucmt.ID == id
                                                                 join cmt in db.Commitments.AsEnumerable() on stucmt.CmtID equals cmt.CmtID
                                                                 select cmt).ToList();

                    QuickSort(ref tutorCommits, tutorCommits.Count);

                    removeNotOpens(ref tutorCommits, start, weekly);

                    if (tutorCommits.Count == 0)
                    {

                    }
                    else
                    {
                        List<string> tutorValidSlots = getValidSlots(ref tutorCommits, sessionLength);
                        int tuteeId = rememberStudIDs[cbxStudents.SelectedIndex];
                        
                        List<TutorMaster.Commitment> tuteeCommits = (from stucmt in db.StudentCommitments.AsEnumerable()
                                                                     where stucmt.ID == tuteeId
                                                                     join cmt in db.Commitments.AsEnumerable() on stucmt.CmtID equals cmt.CmtID
                                                                     select cmt).ToList();
                        
                        QuickSort(ref tuteeCommits, tuteeCommits.Count);

                        removeNotOpens(ref tuteeCommits, start, weekly);

                        if (tuteeCommits.Count == 0)
                        {

                        }
                        else
                        {
                            List<string> tuteeValidSlots = getValidSlots(ref tuteeCommits, sessionLength);

                            for (int c = 0; c < tutorValidSlots.Count(); c++)
                            {
                                if(BinarySearch(tuteeValidSlots, tutorValidSlots[c]))
                                {
                                    lvTimeMatches.Items.Add(new ListViewItem(new string[] {tutorValidSlots[c].Split(',')[0], tutorValidSlots[c].Split(',')[1] }));
                                }
                            }

                        }
                    }
                }
            }
        }

        private DateTime getStartTime(string slot)
        {
            string startDateTime = slot.Split(',')[0];
            string startDate = startDateTime.Split(' ')[0];
            string startTime = startDateTime.Split(' ')[1];
            string amPm = startDateTime.Split(' ')[2];

            int month = Convert.ToInt32(startDate.Split('/')[0]);
            int day = Convert.ToInt32(startDate.Split('/')[1]);
            int year = Convert.ToInt32(startDate.Split('/')[2]);

            int hour = Convert.ToInt32(startTime.Split(':')[0]);
            int min = Convert.ToInt32(startTime.Split(':')[1]);


            if (hour < 12 && amPm == "PM")
            {
                hour += 12;
            }
            else if (hour == 12 && amPm == "AM")
            {
                hour = 0;
            }
            DateTime date = new DateTime(year, month, day, hour, min, 0);
            return date;
        }

        private bool BinarySearch(List<string> cmtList, string commit)
        {
            bool found = false;
            int first = 0;
            int last = cmtList.Count() - 1;
            while (first <= last && !found)
            {
                int midpoint = (first + last) / 2;
                if (DateTime.Compare(getStartTime(cmtList[midpoint]), getStartTime(commit)) == 0)
                {
                    found = true;
                    return found;
                }
                else
                {
                    if (DateTime.Compare(getStartTime(commit), getStartTime(cmtList[midpoint])) < 0)
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

                    if (DateTime.Compare(nextCommitDate, currentCommitDate.AddMinutes(15)) == 0 && consecutiveCommits < sessionLength) //if our next commit is 15 minutes later of our current
                    {
                        consecutiveCommits++;
                        endDate = Convert.ToDateTime(cmtList[i].StartTime).AddMinutes(15);
                    }
                    else if (DateTime.Compare(nextCommitDate, currentCommitDate.AddMinutes(15)) == 0 && consecutiveCommits >= sessionLength)
                    {
                        endDate = Convert.ToDateTime(cmtList[i].StartTime).AddMinutes(15);
                        validSlots.Add(startDate.ToString() + "," + endDate.ToString());
                        startDate = startDate.AddMinutes(15);
                    }
                    else if (DateTime.Compare(nextCommitDate, currentCommitDate.AddMinutes(15)) != 0 && consecutiveCommits >= sessionLength)
                    {
                        endDate = Convert.ToDateTime(cmtList[i].StartTime).AddMinutes(15);
                        validSlots.Add(startDate.ToString() + "," + endDate.ToString());

                        //update our carries
                        consecutiveCommits = 1;
                        startDate = Convert.ToDateTime(cmtList[i + 1].StartTime);
                        endDate = Convert.ToDateTime(cmtList[i + 1].StartTime).AddMinutes(15);
                        initialCommit = cmtList[i + 1];
                    }
                    else if (DateTime.Compare(nextCommitDate, currentCommitDate.AddMinutes(15)) != 0 && consecutiveCommits < sessionLength)
                    {
                        consecutiveCommits = 1;
                        startDate = Convert.ToDateTime(cmtList[i + 1].StartTime);
                        endDate = Convert.ToDateTime(cmtList[i + 1].StartTime).AddMinutes(15);
                        initialCommit = cmtList[i + 1];
                    }
                }
                //i believe i have the algorithm to catch the very last 15 minute time block in a person's schedule here. I've tested the first if statement and it seems to work. I don't know about the second one
                //but the second one makes sense to me and I know I need a statement like it. the second statement is in case we are just one short in our block and the last commit is what's needed to get the valid slot
                if (consecutiveCommits >= sessionLength && DateTime.Compare(Convert.ToDateTime(cmtList[cmtList.Count() - 2].StartTime).AddMinutes(15), Convert.ToDateTime(cmtList[cmtList.Count() - 1].StartTime)) == 0)
                {
                    startDate = startDate.AddMinutes(15);
                    endDate = endDate.AddMinutes(15);
                    validSlots.Add(startDate.ToString() + "," + endDate.ToString());
                }
                else if (consecutiveCommits == sessionLength - 1 && DateTime.Compare(Convert.ToDateTime(cmtList[cmtList.Count() - 2].StartTime).AddMinutes(15), Convert.ToDateTime(cmtList[cmtList.Count() - 1].StartTime)) == 0)
                {
                    endDate = endDate.AddMinutes(15);
                    validSlots.Add(startDate.ToString() + "," + endDate.ToString());
                }
            }
            return validSlots;
        }


        private void loadTuteeOptions()
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();

            //List<string> classes = (from row
        }


        private void removeNotOpens(ref List<TutorMaster.Commitment> cmtList, DateTime start, bool weekly)
        {
            if (weekly)
            {
                for (int i = 0; i < cmtList.Count() - 1; i++)
                {
                    if (!isOpen(cmtList[i]) || DateTime.Compare(start, Convert.ToDateTime(cmtList[i].StartTime)) > 0 || cmtList[i].Weekly != weekly)
                    {
                        cmtList.Remove(cmtList[i]);
                        i--;
                    }
                }
            }
            else
            {
                for (int i = 0; i < cmtList.Count() - 1; i++)
                {
                    if (!isOpen(cmtList[i]) || DateTime.Compare(start, Convert.ToDateTime(cmtList[i].StartTime)) > 0)
                    {
                        cmtList.Remove(cmtList[i]);
                        i--;
                    }
                }
            }
        }

        private void checkMax(ref List<TutorMaster.Commitment> cmtList)
        {
            int consec = 1;

            for (int i = 0; i < cmtList.Count() - 1; i++)
            {
                DateTime currentCommit = Convert.ToDateTime(cmtList[i].StartTime);
                DateTime nextCommit = Convert.ToDateTime(cmtList[i + 1].StartTime);

                if (consec > 11)
                {
                    cmtList.Remove(cmtList[i + 1]);
                    i--;
                    consec = 1;
                }
                if (DateTime.Compare(currentCommit.AddMinutes(15), nextCommit) == 0 && sameCategory(cmtList[i], cmtList[i + 1]) && !isOpen(cmtList[i]))
                {
                    consec++;
                }
                else
                {
                    consec = 1;
                }
            }
        }

        private bool sameCategory(TutorMaster.Commitment commitFirst, TutorMaster.Commitment commitSecond)      //ask if the 15 minute time block of the first has the same values as the second
        {
            return (commitFirst.Class == commitSecond.Class && commitFirst.Location == commitSecond.Location
                    && commitFirst.Open == commitSecond.Open && commitFirst.Weekly == commitSecond.Weekly
                    && commitFirst.ID == commitSecond.ID);
        }

        private bool isOpen(TutorMaster.Commitment commit)
        {
            return (commit.Class == "-" && commit.Location == "-" && commit.Open == true && commit.Tutoring == false && commit.ID == -1);
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AdminSeeSchedule g = new AdminSeeSchedule(id);
            g.Show();
            this.Close();
        }

        private void cbxClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(cbxClasses.Text.ToString()))
            {
                cbxStudents.Show();
            }
        }

        private void cbxStudents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(cbxStudents.Text.ToString()))
            {
                cbxHour.Show();
                cbxMinutes.Show();
                cbWeekly.Show();
                lvTimeMatches.Show();
            }
        }

        private void cbxHour_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(cbxHour.Text.ToString()) && !string.IsNullOrWhiteSpace(cbxMinutes.Text.ToString()))
            {
                resetListView();
            }
        }

        private void cbxMinutes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(cbxHour.Text.ToString()) && !string.IsNullOrWhiteSpace(cbxMinutes.Text.ToString()))
            {
                resetListView();
            }
        }

        private void resetListView()
        {
            lvTimeMatches.Items.Clear();
            loadMatchingTimeSlots();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (lvTimeMatches.CheckedItems.Count == 0)
            {

            }
            else
            {
                TutorMasterDBEntities4 db = new TutorMasterDBEntities4();

            }

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
    }
}
