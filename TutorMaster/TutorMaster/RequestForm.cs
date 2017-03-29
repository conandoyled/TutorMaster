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

            combHours.Text = "0";
            combMins.Text = "00";

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            StudentMain f = new StudentMain(id);
            f.Show();
            this.Close();
        }


        private void btnRequest_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(combCourseName.Text))
            {
                MessageBox.Show("Please select a course for the session.");
            }
            else if (string.IsNullOrWhiteSpace(combHours.Text) || string.IsNullOrWhiteSpace(combMins.Text))
            {
                MessageBox.Show("Please input values for the hours and minutes dropdown boxes");
            }
            else if(((Convert.ToInt32(combHours.Text) * 4 + (Convert.ToInt32(combMins.Text) / 15)) == 0) || 
                ((Convert.ToInt32(combHours.Text) * 4 + (Convert.ToInt32(combMins.Text) / 15)) > 12))
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

                    int sessionLength = Convert.ToInt32(combHours.Text) * 4 + (Convert.ToInt32(combMins.Text) / 15);

                    removeNotOpens(ref tuteeCommits);

                    if (tuteeCommits.Count == 0)
                    {
                        MessageBox.Show("You currently have no available slots, please add some availability before attempting to schedule a session of this length");
                    }
                    else
                    {
                        QuickSort(ref tuteeCommits, tuteeCommits.Count());

                        List<string> tuteeValidSlots = getValidSlots(ref tuteeCommits, sessionLength);

                        for (int i = 0; i < approvedTutorIds.Count(); i++)
                        {
                            if (approvedTutorIds[i] != id)
                            {
                                //var tutor = (from row in db.Users.AsEnumerable() where row.ID == approvedTutorIds[i] select row).First();
                                var tutorFirstName = (from row in db.Users.AsEnumerable() where row.ID == approvedTutorIds[i] select row.FirstName).First();
                                var tutorLastName = (from row in db.Users.AsEnumerable() where row.ID == approvedTutorIds[i] select row.LastName).First();
                                var tutorStdCommitments = (from row in db.StudentCommitments.AsEnumerable() where approvedTutorIds[i] == row.ID select row.CmtID).ToList();
                                List<TutorMaster.Commitment> tutorCommits = new List<Commitment>();
                                for (int j = 0; j < tutorStdCommitments.Count(); j++)                                                 //look each up and add to a list of commitments
                                {
                                    TutorMaster.Commitment commit = (from row in db.Commitments.AsEnumerable() where row.CmtID == tutorStdCommitments[j] select row).First();
                                    tutorCommits.Add(commit);
                                }
                                removeNotOpens(ref tutorCommits);
                                QuickSort(ref tutorCommits, tutorCommits.Count());
                                List<string> tutorValidSlots = getValidSlots(ref tutorCommits, sessionLength);
                                bool done = false;
                                for (int j = 0; j < tutorValidSlots.Count(); j++)
                                {
                                    if (BinarySearch(tuteeValidSlots, tutorValidSlots[j]))
                                    {
                                        DialogResult choice = MessageBox.Show("You have been matched with " + tutorFirstName + " " + tutorLastName +
                                            " for a time at: " + tutorValidSlots[j].Split(',')[0] + " - " + tutorValidSlots[j].Split(',')[1], "You've got a match!", MessageBoxButtons.YesNo);
                                        if (choice == DialogResult.Yes)
                                        {
                                            int tutorId = Convert.ToInt32(approvedTutorIds[i]);
                                            int tuteeId = Convert.ToInt32(id);
                                            addCommits(tutorValidSlots[j], tutorId, tuteeId, tutorCommits, tuteeCommits, classCode, db);
                                            break;
                                        }
                                        else if (choice == DialogResult.No)
                                        {
                                            break;
                                        }
                                    }
                                }
                                if (done)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    StudentMain g = new StudentMain(id);
                    g.Show();
                    this.Close();
                }
            }
        }

        private void addCommits(string timeSlot, int tutorId, int tuteeId, List<TutorMaster.Commitment> tutorCommits, List<TutorMaster.Commitment> tuteeCommits, string classCode, TutorMasterDBEntities4 db)
        {
            //TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            DateTime startTime = getStartTime(timeSlot);
            DateTime endTime = getEndTime(timeSlot);

          

            for (int j = 0; j < tuteeCommits.Count(); j++)
            {
                if (DateTime.Compare(startTime, Convert.ToDateTime(tuteeCommits[j].StartTime)) <= 0 && DateTime.Compare(endTime, Convert.ToDateTime(tuteeCommits[j].StartTime)) > 0)
                {
                    tuteeCommits[j].Open = false;
                    tuteeCommits[j].Tutoring = false;
                    tuteeCommits[j].ID = tutorId;
                    tuteeCommits[j].Class = classCode;
                    db.SaveChanges();
                    
                }
            }

            for (int i = 0; i < tutorCommits.Count(); i++)
            {
                if (DateTime.Compare(startTime, Convert.ToDateTime(tutorCommits[i].StartTime)) <= 0 && DateTime.Compare(endTime, Convert.ToDateTime(tutorCommits[i].StartTime)) > 0)
                {
                    tutorCommits[i].Open = false;
                    tutorCommits[i].Tutoring = true;
                    tutorCommits[i].ID = tuteeId;
                    tutorCommits[i].Class = classCode;
                    db.SaveChanges();
                }
            }
        }

        private List<string> getValidSlots(ref List<TutorMaster.Commitment> cmtList, int sessionLength)
        {
            int consecutiveCommits = 0;
            
            List<string> validSlots = new List<string>();
            TutorMaster.Commitment initialCommit = cmtList[0];
            string startTime = getCommitTime(cmtList[0]);
            DateTime startDate = Convert.ToDateTime(cmtList[0].StartTime);
            string endTime = getCommitTime15(cmtList[0]);
            DateTime endDate = Convert.ToDateTime(cmtList[0].StartTime).AddMinutes(15);
            
            consecutiveCommits++;

            if (sessionLength == 1)
            {
                for (int i = 0; i < cmtList.Count() - 1; i++)
                {
                    startTime = getCommitTime(cmtList[i]);
                    endTime = getCommitTime15(cmtList[i]);
                    DateTime start = Convert.ToDateTime(cmtList[i].StartTime);
                    DateTime end = Convert.ToDateTime(cmtList[i].StartTime).AddMinutes(15);
                    validSlots.Add(start.ToString() + "," + end.ToString());
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
                        endDate = Convert.ToDateTime(cmtList[i].StartTime).AddMinutes(15);
                        endTime = getCommitTime15(cmtList[i]);                                   //only update endTime
                    }
                    else if (DateTime.Compare(nextCommitDate, currentCommitDate.AddMinutes(15)) == 0 && consecutiveCommits >= sessionLength)
                    {
                        endTime = getCommitTime15(cmtList[i]);
                        endDate = Convert.ToDateTime(cmtList[i].StartTime).AddMinutes(15);
                        //MessageBox.Show(startDate.ToString() + "," + endDate.ToString());
                        validSlots.Add(startDate.ToString() + "," + endDate.ToString());
                        startTime = startDate.AddMinutes(15).ToString().Split(' ')[1] + " " + startDate.AddMinutes(15).ToString().Split(' ')[2];
                        startDate = startDate.AddMinutes(15);
                    }
                    else if(DateTime.Compare(nextCommitDate, currentCommitDate.AddMinutes(15)) != 0 && consecutiveCommits >= sessionLength)
                    {
                        endTime = getCommitTime15(cmtList[i]);                                   //if next commit is not, update endTime
                        endDate = Convert.ToDateTime(cmtList[i].StartTime).AddMinutes(15);
                        //MessageBox.Show(startDate.ToString() + "," + endDate.ToString());
                        validSlots.Add(startDate.ToString() + "," + endDate.ToString());

                        //update our carries
                        consecutiveCommits = 1;
                        startTime = getCommitTime(cmtList[i + 1]);
                        startDate = Convert.ToDateTime(cmtList[i + 1].StartTime);
                        endTime = getCommitTime15(cmtList[i + 1]);
                        endDate = Convert.ToDateTime(cmtList[i + 1].StartTime).AddMinutes(15);
                        initialCommit = cmtList[i + 1];
                    }
                    else if (DateTime.Compare(nextCommitDate, currentCommitDate.AddMinutes(15)) != 0 && consecutiveCommits < sessionLength)
                    {
                        consecutiveCommits = 1;
                        startTime = getCommitTime(cmtList[i + 1]);
                        startDate = Convert.ToDateTime(cmtList[i + 1].StartTime);
                        endTime = getCommitTime15(cmtList[i + 1]);
                        endDate = Convert.ToDateTime(cmtList[i + 1].StartTime).AddMinutes(15);
                        initialCommit = cmtList[i + 1];
                    }
                }
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

        private DateTime getEndTime(string slot)
        {
            string startDateTime = slot.Split(',')[1];
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