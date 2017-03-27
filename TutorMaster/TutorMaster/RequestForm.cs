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

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            StudentMain f = new StudentMain(id);
            f.Show();
            this.Close();
        }


        //private List<DateTime> MakeFifteenBlocks(DateTime startTime, DateTime endTime)
        //{
        //    DateTime begin = startTime;
        //    int compare = begin.CompareTo(endTime);//returns -1 through postive 1 to represent the relation between the compared times 
        //    List<DateTime> RequestTimes = new List<DateTime>(); //create a list to store the broken up commitments.

        //    while (compare < 0) //if the first date is less than the second date
        //    {
        //        RequestTimes.Add(begin);
        //        begin = begin.AddMinutes(15);
        //        compare = begin.CompareTo(endTime);
        //    }
        //    return RequestTimes;
        //}

        private void btnRequest_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(combCourseName.Text))
            {
                MessageBox.Show("Please select a course for the session.");
            }
            else if (string.IsNullOrWhiteSpace(combHours.Text) || string.IsNullOrWhiteSpace(combMins.Text))// || (Convert.ToInt32(combMins.Text) == 0 &&
                //Convert.ToInt32(combHours.Text) == 0) || ((Convert.ToInt32(combHours.Text) * 4 + (Convert.ToInt32(combMins) / 15)) > 12)
            {
                MessageBox.Show("Please input values for the hours and minutes dropdown boxes";
            }
            else if((Convert.ToInt32(combMins.Text) == 0 && Convert.ToInt32(combHours.Text) == 0) || ((Convert.ToInt32(combHours.Text) * 4 + (Convert.ToInt32(combMins) / 15)) > 12))
            {
                MessageBox.Show("Please input values for the hours and minutes that are between a length of 15 minutes and 3 hours");
            }
            else
            {
                TutorMasterDBEntities4 db = new TutorMasterDBEntities4();

                string classCode = (from row in db.Classes where combCourseName.Text == row.ClassName select row.ClassCode).First();

                var approvedTutorIds = (from row in db.StudentClasses.AsEnumerable() where classCode == row.ClassCode select row.ID).ToList();

                var tuteeStdCommitments = (from row in db.StudentCommitments.AsEnumerable() where id == row.ID select row.CmtID).ToList();

                if (tuteeStdCommitments.Count == 0)
                {
                    MessageBox.Show("You currently have no available slots, please add some availability before attempting to schedule a session of this length");
                }
                else
                {
                    List<TutorMaster.Commitment> tuteeCommits = new List<Commitment>();
                    for (int j = 0; j < tuteeStdCommitments.Count(); j++)                                                 //look each up and add to a list of commitments
                    {
                        TutorMaster.Commitment commit = (from row in db.Commitments.AsEnumerable() where row.CmtID == tuteeStdCommitments[j] select row).First();
                        tuteeCommits.Add(commit);
                    }

                    int sessionLength = Convert.ToInt32(combHours.Text) * 4 + (Convert.ToInt32(combMins) / 15);

                    removeNotOpens(ref tuteeCommits);

                    if (tuteeCommits.Count == 0)
                    {
                        MessageBox.Show("You currently have no available slots, please add some availability before attempting to schedule a session of this length");
                    }
                    else
                    {
                        QuickSort(ref tuteeCommits, tuteeCommits.Count());

                        List<string> tuteeValidSlots = getValidSlots(ref tuteeCommits, sessionLength);
                    }
                }
            }
        }

        private List<string> getValidSlots(ref List<TutorMaster.Commitment> cmtList, int sessionLength)
        {
            int consecutiveCommits = 0;
            
            List<string> validSlots = new List<string>();
            TutorMaster.Commitment initialCommit = cmtList[0];
            string startTime = getCommitTime(cmtList[0]);
            string endTime = getCommitTime15(cmtList[0]);
            
            consecutiveCommits++;

            if (sessionLength == 1)
            {
                for (int i = 0; i < cmtList.Count() - 1; i++)
                {
                    startTime = getCommitTime(cmtList[i]);
                    endTime = getCommitTime15(cmtList[i]);
                    validSlots.Add(startTime + "," + endTime);
                }
            }
            else
            {
                for (int i = 0; i < cmtList.Count()-1; i++)
                {
                    DateTime currentCommitDate = Convert.ToDateTime(cmtList[i].StartTime);                   //get datetime of commitment we are on in loop
                    DateTime nextCommitDate = Convert.ToDateTime(cmtList[i + 1].StartTime);                  //get datetime of commitment ahead of it

                    if (DateTime.Compare(nextCommitDate, currentCommitDate.AddMinutes(15)) == 0 && consecutiveCommits < sessionLength) //if our next commit is 15 minutes later of our current
                    {
                        consecutiveCommits++;
                        endTime = getCommitTime15(cmtList[i]);                                   //only update endTime
                    }
                    else if (DateTime.Compare(nextCommitDate, currentCommitDate.AddMinutes(15)) == 0 && consecutiveCommits >= sessionLength)
                    {
                        endTime = getCommitTime15(cmtList[i]);
                        validSlots.Add(startTime + "," + endTime);
                    }
                    else if(DateTime.Compare(nextCommitDate, currentCommitDate.AddMinutes(15)) != 0 && consecutiveCommits >= sessionLength)
                    {
                        endTime = getCommitTime15(cmtList[i]);                                   //if next commit is not, update endTime
                        validSlots.Add(startTime + "," + endTime);

                        //update our carries
                        consecutiveCommits = 1;
                        startTime = getCommitTime(cmtList[i + 1]);
                        endTime = getCommitTime15(cmtList[i + 1]);
                        initialCommit = cmtList[i + 1];
                    }
                    else if (DateTime.Compare(nextCommitDate, currentCommitDate.AddMinutes(15)) != 0 && consecutiveCommits < sessionLength)
                    {
                        consecutiveCommits = 1;
                        startTime = getCommitTime(cmtList[i + 1]);
                        endTime = getCommitTime15(cmtList[i + 1]);
                        initialCommit = cmtList[i + 1];
                    }
                    else if (cmtList[i + 1] == cmtList[cmtList.Count() - 1] )
                    {

                    }
                }
                endTime = getCommitTime15(cmtList[cmtList.Count() - 1]);
                validSlots.Add(startTime + "," + endTime);
            }
            return validSlots;
        }

        private void removeNotOpens(ref List<TutorMaster.Commitment> cmtList)
        {
            for (int i = 0; i < cmtList.Count(); i++)
            {
                if (!isOpen(cmtList[i]))
                {
                    cmtList.Remove(cmtList[i]);
                }
            }
        }

        private bool isOpen(TutorMaster.Commitment commit)
        {
            return (commit.Class == "-" && commit.Location == "-" && commit.Open == true && commit.Tutoring == false && commit.ID == -1);
        }

        private string getCommitTime(TutorMaster.Commitment commit)
        {
            return Convert.ToDateTime(commit.StartTime).ToString().Split(' ')[1] + " " + Convert.ToDateTime(commit.StartTime).ToString().Split(' ')[2];
        }

        private string getCommitTime15(TutorMaster.Commitment commit15)
        {
            return Convert.ToDateTime(commit15.StartTime).AddMinutes(15).ToString().Split(' ')[1] + " " + Convert.ToDateTime(commit15.StartTime).ToString().Split(' ')[2];
        }
        //    //1. create the time objects

        //    if ( (string.IsNullOrWhiteSpace(combStartHour.Text)) || (string.IsNullOrWhiteSpace(combStartMinute.Text) 
        //    || (string.IsNullOrWhiteSpace(combStartAmPm.Text)))  ||  (string.IsNullOrWhiteSpace(combEndHour.Text))
        //    || (string.IsNullOrWhiteSpace(combEndMinute.Text))   || (string.IsNullOrWhiteSpace(combEndAmPm.Text)))
        //    {
        //        MessageBox.Show("Please fill out a starting and ending day, hour, minute, and part of day"); // give a popup if they don't enter a valid time
        //    }

        //    else //If the time the enter is valid then,
        //    {
        //        //1. pull the information for the start date time object
        //        //string stringStartDay = combStartDay.Text;

        //        int startHour = int.Parse(combStartHour.Text);
        //        int startMinute = int.Parse(combStartMinute.Text);
        //        string startAmPm = combStartAmPm.Text;

        //        if (startAmPm == "PM" && startHour != 12)
        //        {
        //            startHour += 12;
        //        }
        //        else if (startAmPm == "AM" && startHour == 12)
        //        {
        //            startHour = 0;
        //        }

        //        //1.5 Pull the information for the end date time object
        //        //string stringEndDay = combEndDay.Text;

        //        int endHour = int.Parse(combEndHour.Text);
        //        int endMinute = int.Parse(combEndMinute.Text);
        //        string endAmPm = combEndAmPm.Text;

        //        if (endAmPm == "PM" && endHour != 12)
        //        {
        //            endHour += 12;
        //        }
        //        else if (endAmPm == "AM" && endHour == 12)
        //        {
        //            endHour = 0;
        //        }

        //        //check if it is weekly
        //        bool weekly = cbxWeekly.Checked;

        //        //create the date time objects
        //        DateTime startTime = new DateTime(dayStartDateTime.Value.Year, dayStartDateTime.Value.Month, dayStartDateTime.Value.Day, startHour, startMinute, 0);
        //        DateTime endTime = new DateTime(dayEndDateTime.Value.Year, dayEndDateTime.Value.Month, dayEndDateTime.Value.Day, endHour, endMinute, 0);

        //        //2. Break up the times into 15 minute blocks

        //        List<DateTime> RequestTimes = MakeFifteenBlocks(startTime, endTime); //this gives us the list that contains all the fifteen minute blocks to be compared

        //        //3. Match requests!

        //        //3.1 Check if there are any tutors for the class!
        //        TutorMasterDBEntities4 db = new TutorMasterDBEntities4(); //create new db entity to look at things
        //        string CC = (from row in db.Classes where row.ClassName == combCourseName.Text select row.ClassCode).First(); //pull out the classcode to compare
        //        List<int> IDs = new List<int>();
        //        foreach (StudentClass sc in db.StudentClasses)
        //        {
        //            if (sc.ClassCode == CC)
        //            {
        //                IDs.Add(sc.ID); //add the ID of the tutor that teaches the class I'm looking for to the list
        //            }
        //        }
                
        //    }
        //}

        private void combCourseName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbxWeekly_CheckedChanged(object sender, EventArgs e)
        {

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
    }
}



//Things to add:
//1. use course name to identify tutors that are available for that course
//  a. error check that the course name exists and give a pop up if it doesn't
//2. check starting and ending times and chunck them together to see if they are compatible with a chunk of time that a tutor is offering
//  a. set up drop boxes to offer time
//  b. validate times
//  c. chunk the request time
//  d. pull the commitments of each tutor offer and compare them against request time 