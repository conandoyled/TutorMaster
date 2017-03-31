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
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            bool tutor = (bool)(from row in db.Students where row.ID == id select row.Tutor).First();
            bool tutee = (bool)(from row in db.Students where row.ID == id select row.Tutee).First();
            InitializeComponent();
            if (!tutee)
            {
                btnMakeRequest.Visible = false;
            }
            populateColumns(tutor, tutee);
            weekStartDateTime.Value = DateTime.Today;
            dayStartDateTime.Value = DateTime.Today;
            dayEndDateTime.Value = DateTime.Today;
            DateTime start = DateTime.Now;
            loadAvail(start);
            setUpLabels(start);
            loadAppointments();
            disableButtons();
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
                    List<Commitment> cmtList = (from stucmt in db.StudentCommitments
                                                where stucmt.ID == id
                                                join cmt in db.Commitments on stucmt.CmtID equals cmt.CmtID
                                                select cmt).ToList();

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
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();

            string partner = "";
            string open = getYesNo((bool)commit.Open);
            string tutoring = getYesNo((bool)commit.Tutoring);
            string weekly = getYesNo((bool)commit.Weekly);

            if (commit.ID == -1)
            {
                partner = "None";
            }
            else
            {
                var partnerData = (from row in db.Users where row.ID == commit.ID select row).First();
                partner = partnerData.FirstName + " " + partnerData.LastName;
            }

            switch (day)
            {
                case "Sunday":
                    lvSunday.Items.Add(new ListViewItem(new string[] { startTime, endTime, commit.Class, commit.Location, open, tutoring, weekly, partner }));
                    break;
                case "Monday":
                    lvMonday.Items.Add(new ListViewItem(new string[] { startTime, endTime, commit.Class, commit.Location, open, tutoring, weekly, partner }));
                    break;
                case "Tuesday":
                    lvTuesday.Items.Add(new ListViewItem(new string[] { startTime, endTime, commit.Class, commit.Location, open, tutoring, weekly, partner }));
                    break;
                case "Wednesday":
                    lvWednesday.Items.Add(new ListViewItem(new string[] { startTime, endTime, commit.Class, commit.Location, open, tutoring, weekly, partner }));
                    break;
                case "Thursday":
                    lvThursday.Items.Add(new ListViewItem(new string[] { startTime, endTime, commit.Class, commit.Location, open, tutoring, weekly, partner }));
                    break;
                case "Friday":
                    lvFriday.Items.Add(new ListViewItem(new string[] { startTime, endTime, commit.Class, commit.Location, open, tutoring, weekly, partner }));
                    break;
                case "Saturday":
                    lvSaturday.Items.Add(new ListViewItem(new string[] { startTime, endTime, commit.Class, commit.Location, open, tutoring, weekly, partner }));
                    break;
            }
        }

        private string getYesNo(bool b)
        {
            string yesno = "";
            if (b)
            {
                yesno = "Yes";
            }
            else
            {
                yesno = "No";
            }

            return yesno;
        }

        private string getDay(DateTime date)
        {
            string[] st = date.ToString("D").Split(',');
            string day = st[0];
            return day;
        }

        //loading pending and accepted appointment functions
        private void loadAppointments()
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();                                                //open Database
            int num = db.StudentCommitments.Count();                                                                 //see if there are any student committments at all
            if (num > 0)
            {


                List<Commitment> cmtList = (from stucmt in db.StudentCommitments
                                            where stucmt.ID == id
                                            join cmt in db.Commitments on stucmt.CmtID equals cmt.CmtID
                                            select cmt).ToList();

                QuickSort(ref cmtList, cmtList.Count());
                //DateTime start = new DateTime(2017, 1, 1, 0, 0, 0);

                removeOpens(ref cmtList);
                if (cmtList.Count > 0)
                {
                    TutorMaster.Commitment initialCommit = cmtList[0];
                    string startTime = Convert.ToDateTime(cmtList[0].StartTime).ToString();
                    string endTime = Convert.ToDateTime(cmtList[0].StartTime).AddMinutes(15).ToString();
                    int numCmts = cmtList.Count;

                    for (int i = 0; i < numCmts - 1; i++)
                    {
                        DateTime currentCommitDate = Convert.ToDateTime(cmtList[i].StartTime);                   //get datetime of commitment we are on in loop
                        DateTime nextCommitDate = Convert.ToDateTime(cmtList[i + 1].StartTime);                  //get datetime of commitment ahead of it


                        //if the two commits are distinct besides time and current commit is within week of start time
                        if (!sameCategory(cmtList[i], cmtList[i + 1]) || currentCommitDate.AddMinutes(15) != nextCommitDate)
                        {
                            endTime = endTime = Convert.ToDateTime(cmtList[i].StartTime).AddMinutes(15).ToString();                                               //update endtime and add what we have so far
                            addToAppointments(initialCommit, startTime, endTime);

                            //initialize carries to be the next commitment and begin scanning for that
                            startTime = Convert.ToDateTime(cmtList[i + 1].StartTime).ToString();
                            endTime = Convert.ToDateTime(cmtList[i + 1].StartTime).AddMinutes(15).ToString();
                            initialCommit = cmtList[i + 1];

                        }

                    }
                    endTime = Convert.ToDateTime(cmtList[cmtList.Count() - 1].StartTime).ToString();
                    addToAppointments(initialCommit, startTime, endTime);
                }
            }

        }

        private void addToAppointments(TutorMaster.Commitment commit, string startTime, string endTime)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();

            string partner = "";
            string open = getYesNo((bool)commit.Open);
            string tutoring = getYesNo((bool)commit.Tutoring);
            string weekly = getYesNo((bool)commit.Weekly);

            if (commit.ID == -1)
            {
                partner = "None";
            }
            else
            {
                var partnerData = (from row in db.Users where row.ID == commit.ID select row).First();
                partner = partnerData.FirstName + " " + partnerData.LastName;
            }

            if (accepted(commit))
            {
                lvFinalized.Items.Add(new ListViewItem(new string[] { startTime, endTime, commit.Class, commit.Location, 
                    commit.Open.ToString(), commit.Tutoring.ToString(), commit.Weekly.ToString(), partner, commit.ID.ToString() }));
            }
            else if (waitingForLocation(commit))
            {
                lvPendingTutor.Items.Add(new ListViewItem(new string[] { startTime, endTime, commit.Class, commit.Location, 
                    commit.Open.ToString(), commit.Tutoring.ToString(), commit.Weekly.ToString(), partner, commit.ID.ToString() }));
            }
            else if (waitingForTutee(commit))
            {
                lvTutor.Items.Add(new ListViewItem(new string[] { startTime, endTime, commit.Class, commit.Location, 
                    commit.Open.ToString(), commit.Tutoring.ToString(), commit.Weekly.ToString(), partner, commit.ID.ToString() }));
            }
            else if (waitingForLocationApproval(commit))
            {
                lvPendingTutee.Items.Add(new ListViewItem(new string[] { startTime, endTime, commit.Class, commit.Location, 
                    commit.Open.ToString(), commit.Tutoring.ToString(), commit.Weekly.ToString(), partner, commit.ID.ToString() }));
            }
            else if (waitingForTutor(commit))
            {
                lvTutee.Items.Add(new ListViewItem(new string[] { startTime, endTime, commit.Class, commit.Location, 
                    commit.Open.ToString(), commit.Tutoring.ToString(), commit.Weekly.ToString(), partner, commit.ID.ToString() }));
            }

        }

        private void removeOpens(ref List<TutorMaster.Commitment> cmtList)
        {
            for (int i = 0; i < cmtList.Count(); i++)
            {
                if (isOpen(cmtList[i]))
                {
                    cmtList.Remove(cmtList[i]);
                }
            }
        }

        private bool isOpen(TutorMaster.Commitment commit)
        {
            return (commit.Class == "-" && commit.Location == "-" && commit.Open == true && commit.Tutoring == false && commit.ID == -1);
        }

        private bool waitingForTutor(TutorMaster.Commitment commit)
        {
            return (commit.Class != "-" && commit.Location == "-" && commit.Open == false && commit.Tutoring == false && commit.ID != -1);
        }

        private bool waitingForLocation(TutorMaster.Commitment commit)
        {
            return (commit.Class != "-" && commit.Location == "-" && commit.Open == false && commit.Tutoring == true && commit.ID != -1);
        }

        private bool waitingForLocationApproval(TutorMaster.Commitment commit)
        {
            return (commit.Class != "-" && commit.Location.Contains('?') && commit.Open == false && commit.Tutoring == false && commit.ID != -1);
        }

        private bool waitingForTutee(TutorMaster.Commitment commit)
        {
            return (commit.Class != "-" && commit.Location.Contains('?') && commit.Open == false && commit.Tutoring == true && commit.ID != -1);
        }

        private bool accepted(TutorMaster.Commitment commit)
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
            /*var storedCommits = (from row in db.StudentCommitments.AsEnumerable() where row.ID == id select row.CmtID).ToArray();
            int numCommits = storedCommits.Length;
            List<DateTime> date = new List<DateTime>();
            
            for (int j = 0; j < numCommits; j++)
            {
                date.Add(Convert.ToDateTime((from row in db.Commitments.AsEnumerable() where row.CmtID == storedCommits[j] select row.StartTime).First()));
            }*/

            var date = (from stucmt in db.StudentCommitments
                        where stucmt.ID == id
                        join cmt in db.Commitments on stucmt.CmtID equals cmt.CmtID
                        select cmt.StartTime).ToList();

            int dateCount = date.Count();

            for (int i = 0; i < dateCount; i++)
            {
                if (begin == Convert.ToDateTime(date[i]))
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

        //setup ListViews
        private void populateColumns(bool tutor, bool tutee)
        {
            lvSunday.CheckBoxes = true;
            lvSunday.Columns.Add("Start Time", 90);
            lvSunday.Columns.Add("End Time", 90);
            lvSunday.Columns.Add("Class", 70);
            lvSunday.Columns.Add("Location", 105);
            lvSunday.Columns.Add("Open", 50);
            lvSunday.Columns.Add("Tutoring", 75);
            lvSunday.Columns.Add("Weekly", 75);
            lvSunday.Columns.Add("Partner", 115);

            lvMonday.CheckBoxes = true;
            lvMonday.Columns.Add("Start Time", 90);
            lvMonday.Columns.Add("End Time", 90);
            lvMonday.Columns.Add("Class", 70);
            lvMonday.Columns.Add("Location", 105);
            lvMonday.Columns.Add("Open", 50);
            lvMonday.Columns.Add("Tutoring", 75);
            lvMonday.Columns.Add("Weekly", 75);
            lvMonday.Columns.Add("Partner", 115);

            lvTuesday.CheckBoxes = true;
            lvTuesday.Columns.Add("Start Time", 90);
            lvTuesday.Columns.Add("End Time", 90);
            lvTuesday.Columns.Add("Class", 70);
            lvTuesday.Columns.Add("Location", 105);
            lvTuesday.Columns.Add("Open", 50);
            lvTuesday.Columns.Add("Tutoring", 75);
            lvTuesday.Columns.Add("Weekly", 75);
            lvTuesday.Columns.Add("Partner", 115);

            lvWednesday.CheckBoxes = true;
            lvWednesday.Columns.Add("Start Time", 90);
            lvWednesday.Columns.Add("End Time", 90);
            lvWednesday.Columns.Add("Class", 70);
            lvWednesday.Columns.Add("Location", 105);
            lvWednesday.Columns.Add("Open", 50);
            lvWednesday.Columns.Add("Tutoring", 75);
            lvWednesday.Columns.Add("Weekly", 75);
            lvWednesday.Columns.Add("Partner", 115);

            lvThursday.CheckBoxes = true;
            lvThursday.Columns.Add("Start Time", 90);
            lvThursday.Columns.Add("End Time", 90);
            lvThursday.Columns.Add("Class", 70);
            lvThursday.Columns.Add("Location", 105);
            lvThursday.Columns.Add("Open", 50);
            lvThursday.Columns.Add("Tutoring", 75);
            lvThursday.Columns.Add("Weekly", 75);
            lvThursday.Columns.Add("Partner", 115);

            lvFriday.CheckBoxes = true;
            lvFriday.Columns.Add("Start Time", 90);
            lvFriday.Columns.Add("End Time", 90);
            lvFriday.Columns.Add("Class", 70);
            lvFriday.Columns.Add("Location", 105);
            lvFriday.Columns.Add("Open", 50);
            lvFriday.Columns.Add("Tutoring", 75);
            lvFriday.Columns.Add("Weekly", 75);
            lvFriday.Columns.Add("Partner", 115);

            lvSaturday.CheckBoxes = true;
            lvSaturday.Columns.Add("Start Time", 90);
            lvSaturday.Columns.Add("End Time", 90);
            lvSaturday.Columns.Add("Class", 70);
            lvSaturday.Columns.Add("Location", 105);
            lvSaturday.Columns.Add("Open", 50);
            lvSaturday.Columns.Add("Tutoring", 75);
            lvSaturday.Columns.Add("Weekly", 75);
            lvSaturday.Columns.Add("Partner", 115);

            lvFinalized.CheckBoxes = true;
            lvFinalized.Columns.Add("Start Time", 90);
            lvFinalized.Columns.Add("End Time", 90);
            lvFinalized.Columns.Add("Class", 70);
            lvFinalized.Columns.Add("Location", 105);
            lvFinalized.Columns.Add("Open", 50);
            lvFinalized.Columns.Add("Tutoring", 75);
            lvFinalized.Columns.Add("Weekly", 75);
            lvFinalized.Columns.Add("Partner", 115);
            lvFinalized.Columns.Add("PartnerID", 0);

            if (tutor)
            {
                lvPendingTutor.CheckBoxes = true;
                lvPendingTutor.Columns.Add("Start Time", 90);
                lvPendingTutor.Columns.Add("End Time", 90);
                lvPendingTutor.Columns.Add("Class", 70);
                lvPendingTutor.Columns.Add("Location", 105);
                lvPendingTutor.Columns.Add("Open", 50);
                lvPendingTutor.Columns.Add("Tutoring", 75);
                lvPendingTutor.Columns.Add("Weekly", 75);
                lvPendingTutor.Columns.Add("Partner", 115);
                lvPendingTutor.Columns.Add("PartnerID", 0);

                lvTutor.CheckBoxes = true;
                lvTutor.Columns.Add("Start Time", 90);
                lvTutor.Columns.Add("End Time", 90);
                lvTutor.Columns.Add("Class", 70);
                lvTutor.Columns.Add("Location", 105);
                lvTutor.Columns.Add("Open", 50);
                lvTutor.Columns.Add("Tutoring", 75);
                lvTutor.Columns.Add("Weekly", 75);
                lvTutor.Columns.Add("Partner", 115);
                lvTutor.Columns.Add("PartnerID", 0);
            }
            else
            {
                tabControl2.TabPages.Remove(tabPendingTutor);
            }

            if (tutee)
            {
                lvPendingTutee.CheckBoxes = true;
                lvPendingTutee.Columns.Add("Start Time", 90);
                lvPendingTutee.Columns.Add("End Time", 90);
                lvPendingTutee.Columns.Add("Class", 70);
                lvPendingTutee.Columns.Add("Location", 105);
                lvPendingTutee.Columns.Add("Open", 50);
                lvPendingTutee.Columns.Add("Tutoring", 75);
                lvPendingTutee.Columns.Add("Weekly", 75);
                lvPendingTutee.Columns.Add("Partner", 115);
                lvPendingTutee.Columns.Add("PartnerID", 0);

                lvTutee.CheckBoxes = true;
                lvTutee.Columns.Add("Start Time", 90);
                lvTutee.Columns.Add("End Time", 90);
                lvTutee.Columns.Add("Class", 70);
                lvTutee.Columns.Add("Location", 105);
                lvTutee.Columns.Add("Open", 50);
                lvTutee.Columns.Add("Tutoring", 75);
                lvTutee.Columns.Add("Weekly", 75);
                lvTutee.Columns.Add("Partner", 115);
                lvTutee.Columns.Add("PartnerID", 0);
            }
            else
            {
                tabControl2.TabPages.Remove(tabPendingTutee);
            }

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

        private void disableButtons()
        {
            btnCancelFinalized.Enabled = false;
            btnAcceptAddLoc.Enabled = false;
            btnRejectTutor.Enabled = false;
            btnFinalize.Enabled = false;
            btnRejectTutee.Enabled = false;
        }

        private void btnMakeRequest_Click(object sender, EventArgs e) //display the request form
        {
            RequestForm g = new RequestForm(id);
            g.Show();
            Close();
        }

        private void btnDeselect1_Click(object sender, EventArgs e)
        {
            if (lvFinalized.CheckedItems.Count > 0)
            {
                foreach (ListViewItem listItem in lvFinalized.Items)
                {
                    listItem.Checked = false;
                }
            }
            if (lvTutor.CheckedItems.Count > 0)
            {
                foreach (ListViewItem listItem in lvTutor.Items)
                {
                    listItem.Checked = false;
                }
            } if (lvTutee.CheckedItems.Count > 0)
            {
                foreach (ListViewItem listItem in lvTutee.Items)
                {
                    listItem.Checked = false;
                }
            } if (lvPendingTutor.CheckedItems.Count > 0)
            {
                foreach (ListViewItem listItem in lvPendingTutor.Items)
                {
                    listItem.Checked = false;
                }
            } if (lvPendingTutee.CheckedItems.Count > 0)
            {
                foreach (ListViewItem listItem in lvPendingTutee.Items)
                {
                    listItem.Checked = false;
                }
            }
        }

        private void btnDeselect1_Click(object sender, TabControlCancelEventArgs e)
        {
            if (lvFinalized.CheckedItems.Count > 0)
            {
                foreach (ListViewItem listItem in lvFinalized.Items)
                {
                    listItem.Checked = false;
                }
            }
            if (lvTutor.CheckedItems.Count > 0)
            {
                foreach (ListViewItem listItem in lvTutor.Items)
                {
                    listItem.Checked = false;
                }
            } if (lvTutee.CheckedItems.Count > 0)
            {
                foreach (ListViewItem listItem in lvTutee.Items)
                {
                    listItem.Checked = false;
                }
            } if (lvPendingTutor.CheckedItems.Count > 0)
            {
                foreach (ListViewItem listItem in lvPendingTutor.Items)
                {
                    listItem.Checked = false;
                }
            } if (lvPendingTutee.CheckedItems.Count > 0)
            {
                foreach (ListViewItem listItem in lvPendingTutee.Items)
                {
                    listItem.Checked = false;
                }
            }
        }

        private void lvFinalized_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            int itemsChecked = lvFinalized.CheckedItems.Count; // .CheckedItems.Count tells how many things in the list box are clicked
            if (itemsChecked > 0)
            {
                btnCancelFinalized.Enabled = true;
            }
            else
            {
                btnCancelFinalized.Enabled = false;
            }
        }

        private void lvTutor_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            int itemsChecked1 = lvTutor.CheckedItems.Count; // .CheckedItems.Count tells how many things in the list box are clicked
            int itemsChecked2 = lvPendingTutor.CheckedItems.Count;
            if (itemsChecked1 + itemsChecked2 > 0)
            {
                btnAcceptAddLoc.Enabled = true;
                btnRejectTutee.Enabled = true;
            }
            else
            {
                btnAcceptAddLoc.Enabled = false;
                btnRejectTutee.Enabled = false;
            }
            if (itemsChecked2 > 0 && itemsChecked1 == 0)
            {
                btnFinalize.Enabled = true;
            }
            else
            {
                btnFinalize.Enabled = false;
            }
        }

        private void lvTutee_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int itemsChecked1 = lvTutee.CheckedItems.Count; // .CheckedItems.Count tells how many things in the list box are clicked
            int itemsChecked2 = lvPendingTutee.CheckedItems.Count;
            if (itemsChecked1 + itemsChecked2 > 0)
            {
                btnRejectTutee.Enabled = true;
            }
            else
            {
                btnRejectTutee.Enabled = false;
            }
            if (itemsChecked2 > 0 && itemsChecked1 == 0)
            {
                btnFinalize.Enabled = true;
            }
            else
            {
                btnFinalize.Enabled = false;
            }
        }


        private void btnAcceptAddLoc_Click(object sender, EventArgs e)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            List<string> commits = new List<string>();

            for (int i = 0; i < lvPendingTutor.CheckedItems.Count; i++)
            {
                commits.Add(lvPendingTutor.CheckedItems[i].SubItems[0].Text.ToString() +  "," + lvPendingTutor.CheckedItems[i].SubItems[1].Text.ToString() + "," + Convert.ToString(lvPendingTutor.CheckedItems[i].SubItems[8].Text));
            }
            
            ProposeLocationForm g = new ProposeLocationForm(id, commits);
            g.Show();
            this.Close();
        }

        private void lvPendingTutor_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            if (e.ColumnIndex == 8)
            {
                e.NewWidth = 0;
                e.Cancel = true;
            }
        }

        private void lvTutor_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            if (e.ColumnIndex == 8)
            {
                e.NewWidth = 0;
                e.Cancel = true;
            }
        }

        private void lvTutee_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            if (e.ColumnIndex == 8)
            {
                e.NewWidth = 0;
                e.Cancel = true;
            }
        }

        private void lvPendingTutee_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            if (e.ColumnIndex == 8)
            {
                e.NewWidth = 0;
                e.Cancel = true;
            }
        }

        private void lvFinalized_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            if (e.ColumnIndex == 8)
            {
                e.NewWidth = 0;
                e.Cancel = true;
            }
        }
    }
}