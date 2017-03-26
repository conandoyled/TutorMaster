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
        //private int count = 0;
        public StudentMain(int accID)
        {
            id = accID;
            InitializeComponent();
            populateColumns();
            DateTime start = new DateTime(2017, 3, 23, 0, 0, 0);
            loadAvail(start);
            setUpLabels(start);
        }

        //loading availability functions
        private void loadAvail(DateTime start)
        {
                                                                                                                    //Clear the ListViews
            lvSunday.Items.Clear();
            lvMonday.Items.Clear();
            lvTuesday.Items.Clear();
            lvWednesday.Items.Clear();
            lvThursday.Items.Clear();
            lvFriday.Items.Clear();
            lvSaturday.Items.Clear();

            
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();                                                //open Database
            int num = db.StudentCommitments.Count();                                                                 //see if there are any student committments at all
            if (num > 0)
            {
                var studentCommits = (from row in db.StudentCommitments.AsEnumerable() where row.ID == id select row.CmtID).ToArray(); //look for student that's logged in student committments
                if (studentCommits.Count() > 0)                                                                      //if they have any
                {
                    List<TutorMaster.Commitment> cmtList = new List<TutorMaster.Commitment>();
                    for (int j = 0; j < studentCommits.Count(); j++)                                                 //look each up and add to a list of commitments
                    {
                        TutorMaster.Commitment commit = (from row in db.Commitments.AsEnumerable() where row.CmtID == studentCommits[j] select row).First();
                        cmtList.Add(commit);
                    }

                    QuickSort(ref cmtList, cmtList.Count());                                                         //sort the list by DateTime
                    //DateTime start = new DateTime(2017, 1, 1, 0, 0, 0);
                    getRidOfOutOfBounds(start, ref cmtList);

                    if (cmtList.Count() == 1)                                                                        //base case of having only one committment
                    {
                        addToListView(cmtList[0], getDay(Convert.ToDateTime(cmtList[0].StartTime)), getCommitTime(cmtList[0]), getCommitTime15(cmtList[0]));
                    }
                    else if (cmtList.Count() > 1)                                                                    //general case of having more than one committment
                    {
                        //Carries
                        TutorMaster.Commitment initialCommit = cmtList[0];                                           //start commit (because it has the information I'll need to load to listviews)
                        string today = getDay(Convert.ToDateTime(cmtList[0].StartTime));                             //day of this commitment so I know which listview to add it to
                        string startTime = getCommitTime(cmtList[0]);                                                //start time of commitment
                        string endTime = getCommitTime15(cmtList[0]);                                                //end time of commitment

                        TutorMaster.Commitment lastCommit = cmtList[cmtList.Count() - 1];                            //get last commitment in list to add last

                        for (int i = 0; i < cmtList.Count() - 1; i++)                                                //for each commitment except for the last one
                        {
                            DateTime currentCommitDate = Convert.ToDateTime(cmtList[i].StartTime);                   //get datetime of commitment we are on in loop
                            DateTime nextCommitDate = Convert.ToDateTime(cmtList[i + 1].StartTime);                  //get datetime of commitment ahead of it

                                                                                                                     //if the two commits are distinct besides time and current commit is within week of start time
                            if (!sameCategory(cmtList[i], cmtList[i + 1]) && (DateTime.Compare(currentCommitDate, start.AddDays(7)) < 0 && DateTime.Compare(currentCommitDate, start) >= 0))
                            {
                                endTime = getCommitTime15(cmtList[i]);                                               //update endtime and add what we have so far
                                addToListView(initialCommit, getDay(Convert.ToDateTime(initialCommit.StartTime)), startTime, endTime);

                                                                                                                     //initialize carries to be the next commitment and begin scanning for that
                                startTime = getCommitTime(cmtList[i + 1]);
                                endTime = getCommitTime15(cmtList[i + 1]);
                                today = getDay(Convert.ToDateTime(cmtList[i + 1].StartTime));
                                initialCommit = cmtList[i + 1];
                            }
                            else                                                                                     //if the current and next commit are in the same category
                            {
                                if (DateTime.Compare(currentCommitDate, start.AddDays(7)) < 0 && DateTime.Compare(currentCommitDate, start) >= 0) //see if its within a week of start
                                {
                                    string day = getDay(currentCommitDate);                                          //if it is, get the day of the week of the current commit in for loop
                                    if (today == day)                                                                //compare it to the day of the initial commit we are going to add
                                    {
                                        if (DateTime.Compare(nextCommitDate, currentCommitDate.AddMinutes(15)) == 0) //if our next commit is 15 minutes later of our current
                                        {
                                            endTime = getCommitTime15(cmtList[i]);                                   //only update endTime
                                        }
                                        else
                                        {
                                            endTime = getCommitTime15(cmtList[i]);                                   //if next commit is not, update endTime
                                            addToListView(initialCommit, day, startTime, endTime);                   //add what we have so far

                                                                                                                     //update our carries
                                            startTime = getCommitTime(cmtList[i + 1]);
                                            endTime = getCommitTime15(cmtList[i + 1]);
                                            today = getDay(Convert.ToDateTime(cmtList[i + 1].StartTime));
                                            initialCommit = cmtList[i + 1];
                                        }
                                    }
                                    else                                                                             //if its not the same, update endTime and add it to the appropriate listview
                                    {
                                        endTime = getCommitTime(cmtList[i]);
                                        addToListView(initialCommit, today, startTime, endTime);

                                                                                                                     //update carries including today so the program knows to move onto looking for next day
                                        startTime = getCommitTime(cmtList[i]);
                                        endTime = getCommitTime15(cmtList[i]);
                                        today = getDay(Convert.ToDateTime(cmtList[i].StartTime));
                                        initialCommit = cmtList[i];
                                        today = day;
                                    }
                                }
                            }
                        }
                        endTime = getCommitTime15(cmtList[cmtList.Count() - 1]);                                    //get the last endTime
                        addToListView(initialCommit, getDay(Convert.ToDateTime(initialCommit.StartTime)), startTime, endTime);//update last clump commitment into Listview
                    }
                                                                                                                    //redraw every listview
                    lvSunday.Invalidate();
                    lvMonday.Invalidate();
                    lvTuesday.Invalidate();
                    lvWednesday.Invalidate();
                    lvThursday.Invalidate();
                    lvFriday.Invalidate();
                    lvSaturday.Invalidate();
                    
                    loadPendings(cmtList, start);                                                                   //load accepted and pending listviews with already sorted list
                }
            }
        }

        private void getRidOfOutOfBounds(DateTime start, ref List<TutorMaster.Commitment> cmtList)
        {
            int lenght = cmtList.Count();
            for (int i = 0; i < lenght; i++)
            {
                if (DateTime.Compare(Convert.ToDateTime(cmtList[i].StartTime), start.AddDays(7)) >= 0 || DateTime.Compare(Convert.ToDateTime(cmtList[i].StartTime), start) < 0)
                {
                    cmtList.Remove(cmtList[i]);
                    i--;
                    lenght--;
                }   
            }
        }
        
        private bool sameCategory(TutorMaster.Commitment commitFirst, TutorMaster.Commitment commitSecond)
        {
            return (commitFirst.Class == commitSecond.Class && commitFirst.Location == commitSecond.Location 
                && commitFirst.Open == commitSecond.Open && commitFirst.Weekly == commitSecond.Weekly 
                && commitFirst.ID == commitSecond.ID);
        }

        private string getCommitTime(TutorMaster.Commitment commit)
        {
            return Convert.ToDateTime(commit.StartTime).ToString().Split(' ')[1] + " " + Convert.ToDateTime(commit.StartTime).ToString().Split(' ')[2];
        }

        private string getCommitTime15(TutorMaster.Commitment commit15)
        {
            return Convert.ToDateTime(commit15.StartTime).AddMinutes(15).ToString().Split(' ')[1] + " " + Convert.ToDateTime(commit15.StartTime).ToString().Split(' ')[2];
        }
        
        private void addToListView(TutorMaster.Commitment commit, string day, string startTime, string endTime)
        {
            string partner = "";
            if (commit.ID == -1)
            {
                partner = "None";
            }
            switch (day)
            {
                case "Sunday":
                    lvSunday.Items.Add(new ListViewItem(new string[] { startTime, endTime, commit.Class, commit.Location, commit.Open.ToString(), commit.Tutoring.ToString(), commit.Weekly.ToString(), partner }));
                    break;
                case "Monday":
                    lvMonday.Items.Add(new ListViewItem(new string[] { startTime, endTime, commit.Class, commit.Location, commit.Open.ToString(), commit.Tutoring.ToString(), commit.Weekly.ToString(), partner }));
                    break;
                case "Tuesday":
                    lvTuesday.Items.Add(new ListViewItem(new string[] { startTime, endTime, commit.Class, commit.Location, commit.Open.ToString(), commit.Tutoring.ToString(), commit.Weekly.ToString(), partner }));
                    break;
                case "Wednesday":
                    lvWednesday.Items.Add(new ListViewItem(new string[] { startTime, endTime, commit.Class, commit.Location, commit.Open.ToString(), commit.Tutoring.ToString(), commit.Weekly.ToString(), partner }));
                    break;
                case "Thursday":
                    lvThursday.Items.Add(new ListViewItem(new string[] { startTime, endTime, commit.Class, commit.Location, commit.Open.ToString(), commit.Tutoring.ToString(), commit.Weekly.ToString(), partner }));
                    break;
                case "Friday":
                    lvFriday.Items.Add(new ListViewItem(new string[] { startTime, endTime, commit.Class, commit.Location, commit.Open.ToString(), commit.Tutoring.ToString(), commit.Weekly.ToString(), partner }));
                    break;
                case "Saturday":
                    lvSaturday.Items.Add(new ListViewItem(new string[] { startTime, endTime, commit.Class, commit.Location, commit.Open.ToString(), commit.Tutoring.ToString(), commit.Weekly.ToString(), partner }));
                    break;
            }
        }

        private string getDay(DateTime date)
        {
            string[] st = date.ToString("D").Split(',');
            string day = st[0];
            return day;
        }
        
        //loading pending and accepted appointment functions
        private void loadPendings(List<TutorMaster.Commitment> cmtList, DateTime start)
        {
            //DateTime start = new DateTime(2017, 1, 1, 0, 0, 0);


            removeOpens(ref cmtList);
            if (cmtList.Count > 0)
            {
                TutorMaster.Commitment initialCommit = cmtList[0];
                string startTime = getCommitTime(cmtList[0]);
                string endTime = getCommitTime15(cmtList[0]);

                for (int i = 0; i < cmtList.Count() - 1; i++)
                {
                    DateTime currentCommitDate = Convert.ToDateTime(cmtList[i].StartTime);                   //get datetime of commitment we are on in loop
                    DateTime nextCommitDate = Convert.ToDateTime(cmtList[i + 1].StartTime);                  //get datetime of commitment ahead of it

                    //if the two commits are distinct besides time and current commit is within week of start time
                    if (!sameCategory(cmtList[i], cmtList[i + 1]) && (DateTime.Compare(currentCommitDate, start.AddDays(7)) < 0 && DateTime.Compare(currentCommitDate, start) >= 0))
                    {
                        endTime = getCommitTime15(cmtList[i]);                                               //update endtime and add what we have so far
                        addToAppointments(initialCommit, startTime, endTime);

                        //initialize carries to be the next commitment and begin scanning for that
                        startTime = getCommitTime(cmtList[i + 1]);
                        endTime = getCommitTime15(cmtList[i + 1]);
                        initialCommit = cmtList[i + 1];
                    }
                    else                                                                                     //if the current and next commit are in the same category
                    {
                        if (DateTime.Compare(currentCommitDate, start.AddDays(7)) < 0 && DateTime.Compare(currentCommitDate, start) >= 0) //see if its within a week of start
                        {
                            if (DateTime.Compare(nextCommitDate, currentCommitDate.AddMinutes(15)) == 0) //if our next commit is 15 minutes later of our current
                            {
                                endTime = getCommitTime15(cmtList[i]);                                   //only update endTime
                            }
                            else
                            {
                                endTime = getCommitTime15(cmtList[i]);                                   //if next commit is not, update endTime
                                addToAppointments(initialCommit, startTime, endTime);

                                //update our carries
                                startTime = getCommitTime(cmtList[i + 1]);
                                endTime = getCommitTime15(cmtList[i + 1]);
                                initialCommit = cmtList[i + 1];
                            }
                        }
                    }
                }
                endTime = getCommitTime15(cmtList[cmtList.Count() - 1]);
                addToAppointments(initialCommit, startTime, endTime);
            }
        }

        private void addToAppointments(TutorMaster.Commitment commit, string startTime, string endTime)
        {
            string partner = "";
            if (commit.ID == -1)
            {
                partner = "None";
            }
            if (locAccepted(commit))
            {
                lvAccepted.Items.Add(new ListViewItem(new string[] { startTime, endTime, commit.Class, commit.Location, commit.Open.ToString(), commit.Tutoring.ToString(), commit.Weekly.ToString(), partner }));
            }
            else if (tuteeWaitingForResponse(commit) || tutorWaitingForResponse(commit))
            {
                lvPendingTutor.Items.Add(new ListViewItem(new string[] { startTime, endTime, commit.Class, commit.Location, commit.Open.ToString(), commit.Tutoring.ToString(), commit.Weekly.ToString(), partner }));
            }
            else if (locProposed(commit) || locAcceptWaiting(commit))
            {
                lvPendingTutee.Items.Add(new ListViewItem(new string[] { startTime, endTime, commit.Class, commit.Location, commit.Open.ToString(), commit.Tutoring.ToString(), commit.Weekly.ToString(), partner }));
            }
        }
                
        private void removeOpens(ref List<TutorMaster.Commitment> cmtList)
        {
            for (int i = 0; i < cmtList.Count(); i++)
            {
                if(isOpen(cmtList[i]))
                {
                    cmtList.Remove(cmtList[i]);
                }
            }
        }

        private bool isOpen(TutorMaster.Commitment commit)
        {
            return (commit.Class == "-" && commit.Location == "-" && commit.Open == true && commit.Tutoring == false && commit.ID == -1);
        }

        private bool tuteeWaitingForResponse(TutorMaster.Commitment commit)
        {
            return (commit.Class != "-" && commit.Location == "-" && commit.Open == false && commit.Tutoring == false && commit.ID != -1);
        }

        private bool tutorWaitingForResponse(TutorMaster.Commitment commit)
        {
            return (commit.Class != "-" && commit.Location == "-" && commit.Open == false && commit.Tutoring == true && commit.ID != -1);
        }

        private bool locProposed(TutorMaster.Commitment commit)
        {
            return (commit.Class != "-" && commit.Location.Contains('?') && commit.Open == false && commit.Tutoring == false && commit.ID != -1);
        }

        private bool locAcceptWaiting(TutorMaster.Commitment commit)
        {
            return (commit.Class != "-" && commit.Location.Contains('?') && commit.Open == false && commit.Tutoring == true && commit.ID != -1);
        }

        private bool locAccepted(TutorMaster.Commitment commit)
        {
            return (commit.Class != "-" && !commit.Location.Contains('?') && commit.Location != "-" && commit.Open == false && commit.ID != -1);
        }
     
        //btn to add open slots and its helper functions
        private void btnAddOpenBlock_Click(object sender, EventArgs e)
        {
            //first, error check to make sure that the user put something for each dropdownbox
            if (/*(string.IsNullOrWhiteSpace(combStartDay.Text)) ||*/ (string.IsNullOrWhiteSpace(combStartHour.Text))
            || (string.IsNullOrWhiteSpace(combStartMinute.Text) || (string.IsNullOrWhiteSpace(combStartAmPm.Text)))
            /*|| (string.IsNullOrWhiteSpace(combEndDay.Text))*/ || (string.IsNullOrWhiteSpace(combEndHour.Text))
            || (string.IsNullOrWhiteSpace(combEndMinute.Text)) || (string.IsNullOrWhiteSpace(combEndAmPm.Text)))
            {
                MessageBox.Show("Please fill out a starting and ending day, hour, minute, and part of day");
            }
            else
            {
                //string stringStartDay = combStartDay.Text;
                //int intStartDay = getDayIndex(stringStartDay);
                int startHour = int.Parse(combStartHour.Text);
                int startMinute = int.Parse(combStartMinute.Text);
                string startAmPm = combStartAmPm.Text;

                if (startAmPm == "PM" && startHour != 12)
                {
                    startHour += 12;
                }
                else if (startAmPm == "AM" && startHour == 12)
                {
                    startHour = 0;
                }

               // string stringEndDay = combEndDay.Text;
                //int intEndDay = getDayIndex(stringEndDay);
                int endHour = int.Parse(combEndHour.Text);
                int endMinute = int.Parse(combEndMinute.Text);
                string endAmPm = combEndAmPm.Text;

                if (endAmPm == "PM" && endHour != 12)
                {
                    endHour += 12;
                }
                else if (endAmPm == "AM" && endHour == 12)
                {
                    endHour = 0;
                }

                bool weekly = cbxWeekly.Checked;

                DateTime startTime = new DateTime(dayStartDateTime.Value.Year, dayStartDateTime.Value.Month, dayStartDateTime.Value.Day, startHour, startMinute, 0); //new DateTime(2017, 1, intStartDay, startHour, startMinute, 0);
                DateTime endTime = new DateTime(dayEndDateTime.Value.Year, dayEndDateTime.Value.Month, dayEndDateTime.Value.Day, endHour, endMinute, 0); //new DateTime(2017, 1, intEndDay, endHour, endMinute, 0);
                getAvail(startTime, endTime, weekly);
            }
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
            DateTime start = new DateTime(weekStartDateTime.Value.Year, weekStartDateTime.Value.Month, weekStartDateTime.Value.Day, 0, 0, 0);
            loadAvail(start);
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
            newCommit.Class = "-";
            newCommit.Location = "-";
            newCommit.Tutoring = false;
            newCommit.StartTime = begin;
            newCommit.Open = true;
            newCommit.Weekly = weekly;
            newCommit.ID = -1;
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
                    newCommitW.Class = "-";
                    newCommitW.Location = "-";
                    newCommitW.Tutoring = false;
                    newCommitW.StartTime = begin;
                    newCommitW.Open = true;
                    newCommitW.Weekly = weekly;
                    newCommitW.ID = -1;
                    addCommit(newCommitW);
                    
                    TutorMaster.StudentCommitment newStudentCommitW = new TutorMaster.StudentCommitment();
                    newStudentCommitW.CmtID = lastCR;
                    newStudentCommitW.ID = id;
                    newStudentCommitW.Key = lastSCR;
                    addStudentCommit(newStudentCommitW);
                }
            }
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
        
        //in case someone changes the startofweek datetime picker
        private void weekStartDateTime_ValueChanged(object sender, EventArgs e)
        {
            DateTime start = new DateTime(weekStartDateTime.Value.Year, weekStartDateTime.Value.Month, weekStartDateTime.Value.Day, 0, 0, 0);
            loadAvail(start);
            setUpLabels(start);
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

        //setup ListViews
        private void populateColumns()
        {
            lvSunday.CheckBoxes = true;
            lvSunday.Columns.Add("Start Time", 90);
            lvSunday.Columns.Add("End Time", 90);
            lvSunday.Columns.Add("Class", 75);
            lvSunday.Columns.Add("Location", 105);
            lvSunday.Columns.Add("Open", 50);
            lvSunday.Columns.Add("Tutoring", 75);
            lvSunday.Columns.Add("Weekly", 50);
            lvSunday.Columns.Add("Partner", 115);

            lvMonday.CheckBoxes = true;
            lvMonday.Columns.Add("Start Time", 90);
            lvMonday.Columns.Add("End Time", 90);
            lvMonday.Columns.Add("Class", 75);
            lvMonday.Columns.Add("Location", 105);
            lvMonday.Columns.Add("Open", 50);
            lvMonday.Columns.Add("Tutoring", 75);
            lvMonday.Columns.Add("Weekly", 75);
            lvMonday.Columns.Add("Partner", 115);

            lvTuesday.CheckBoxes = true;
            lvTuesday.Columns.Add("Start Time", 90);
            lvTuesday.Columns.Add("End Time", 90);
            lvTuesday.Columns.Add("Class", 75);
            lvTuesday.Columns.Add("Location", 105);
            lvTuesday.Columns.Add("Open", 50);
            lvTuesday.Columns.Add("Tutoring", 75);
            lvTuesday.Columns.Add("Weekly", 75);
            lvTuesday.Columns.Add("Partner", 115);

            lvWednesday.CheckBoxes = true;
            lvWednesday.Columns.Add("Start Time", 90);
            lvWednesday.Columns.Add("End Time", 90);
            lvWednesday.Columns.Add("Class", 75);
            lvWednesday.Columns.Add("Location", 105);
            lvWednesday.Columns.Add("Open", 50);
            lvWednesday.Columns.Add("Tutoring", 75);
            lvWednesday.Columns.Add("Weekly", 75);
            lvWednesday.Columns.Add("Partner", 115);

            lvThursday.CheckBoxes = true;
            lvThursday.Columns.Add("Start Time", 90);
            lvThursday.Columns.Add("End Time", 90);
            lvThursday.Columns.Add("Class", 75);
            lvThursday.Columns.Add("Location", 105);
            lvThursday.Columns.Add("Open", 50);
            lvThursday.Columns.Add("Tutoring", 75);
            lvThursday.Columns.Add("Weekly", 75);
            lvThursday.Columns.Add("Partner", 115);

            lvFriday.CheckBoxes = true;
            lvFriday.Columns.Add("Start Time", 90);
            lvFriday.Columns.Add("End Time", 90);
            lvFriday.Columns.Add("Class", 75);
            lvFriday.Columns.Add("Location", 105);
            lvFriday.Columns.Add("Open", 50);
            lvFriday.Columns.Add("Tutoring", 75);
            lvFriday.Columns.Add("Weekly", 75);
            lvFriday.Columns.Add("Partner", 115);

            lvSaturday.CheckBoxes = true;
            lvSaturday.Columns.Add("Start Time", 90);
            lvSaturday.Columns.Add("End Time", 90);
            lvSaturday.Columns.Add("Class", 75);
            lvSaturday.Columns.Add("Location", 105);
            lvSaturday.Columns.Add("Open", 50);
            lvSaturday.Columns.Add("Tutoring", 75);
            lvSaturday.Columns.Add("Weekly", 75);
            lvSaturday.Columns.Add("Partner", 115);

            lvAccepted.CheckBoxes = true;
            lvAccepted.Columns.Add("Start Time", 90);
            lvAccepted.Columns.Add("End Time", 90);
            lvAccepted.Columns.Add("Class", 75);
            lvAccepted.Columns.Add("Location", 105);
            lvAccepted.Columns.Add("Open", 50);
            lvAccepted.Columns.Add("Tutoring", 75);
            lvAccepted.Columns.Add("Weekly", 75);
            lvAccepted.Columns.Add("Partner", 115);

            lvPendingTutor.CheckBoxes = true;
            lvPendingTutor.Columns.Add("Start Time", 90);
            lvPendingTutor.Columns.Add("End Time", 90);
            lvPendingTutor.Columns.Add("Class", 75);
            lvPendingTutor.Columns.Add("Location", 105);
            lvPendingTutor.Columns.Add("Open", 50);
            lvPendingTutor.Columns.Add("Tutoring", 75);
            lvPendingTutor.Columns.Add("Weekly", 75);
            lvPendingTutor.Columns.Add("Partner", 115);

            lvPendingTutee.CheckBoxes = true;
            lvPendingTutee.Columns.Add("Start Time", 90);
            lvPendingTutee.Columns.Add("End Time", 90);
            lvPendingTutee.Columns.Add("Class", 75);
            lvPendingTutee.Columns.Add("Location", 105);
            lvPendingTutee.Columns.Add("Open", 50);
            lvPendingTutee.Columns.Add("Tutoring", 75);
            lvPendingTutee.Columns.Add("Weekly", 75);
            lvPendingTutee.Columns.Add("Partner", 115);

        }

        private void setUpLabels(DateTime start)
        {
            for (int i = 0; i < 7; i++)
            {
                DateTime dateD = start.AddDays(i);
                string day = dateD.ToString("D").Split(',')[0];
                switch (day)
                {
                    case "Sunday":
                        lblSunday.Text = dateD.ToString("D");
                        break;
                    case "Monday":
                        lblMonday.Text = dateD.ToString("D");
                        break;
                    case "Tuesday":
                        lblTuesday.Text = dateD.ToString("D");
                        break;
                    case "Wednesday":
                        lblWednesday.Text = dateD.ToString("D");
                        break;
                    case "Thursday":
                        lblThursday.Text = dateD.ToString("D");
                        break;
                    case "Friday":
                        lblFriday.Text = dateD.ToString("D");
                        break;
                    case "Saturday":
                        lblSaturday.Text = dateD.ToString("D");
                        break;
                }
            }
        }

        //logout button
        private void btnLogout_Click(object sender, EventArgs e)
        {
            Login g = new Login();
            g.Show();
            this.Close();
        }
    
        //old function that we may need later
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
    }
}