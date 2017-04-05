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
        private int newNotifs = 0;
        private int numCancels = 0;
        private List<TutorMaster.Commitment> searchList = new List<Commitment>();

        public StudentMain(int accID)
        {
            id = accID;                                                                                           //get the ID of the student
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();                                             //open database
            bool tutor = (bool)(from row in db.Students where row.ID == id select row.Tutor).First();             //get if they are a tutee and/or tutor
            bool tutee = (bool)(from row in db.Students where row.ID == id select row.Tutee).First();
            InitializeComponent();
            if (!tutee)
            {
                btnMakeRequest.Visible = false;                                                                   //if they are not a tutee, they can't make requests
            }
            populateColumns(tutor, tutee);                                                                        //initialize the columns of listviews approriately
            weekStartDateTime.Value = DateTime.Today;                                                             //initialize datetime pickers to be today
            dayStartDateTime.Value = DateTime.Today;
            dayEndDateTime.Value = DateTime.Today;
            DateTime start = DateTime.Now;
            loadAvail(start);                                                                                     //load availability starting from today
            setUpLabels(start);                                                                                   //set up the labels above each schedule list view a week from today
            loadAppointments(false);                                                                              //load the appointments
            disableButtons();                                                                                     //disable necessary buttons
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

                    searchList = cmtList;

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

        private void getRidOfOutOfBounds(DateTime start, ref List<TutorMaster.Commitment> cmtList)                 //trim the list of commitments to only be the ones that are a week from today
        {
            int lenght = cmtList.Count();
            for (int i = 0; i < lenght; i++)
            {
                if (DateTime.Compare(Convert.ToDateTime(cmtList[i].StartTime), start.AddDays(7)) >= 0 || DateTime.Compare(Convert.ToDateTime(cmtList[i].StartTime), start) < 0) //if its not within a week, remove it
                {
                    cmtList.Remove(cmtList[i]);
                    i--;
                    lenght--;
                }
            }
        }

        private bool sameCategory(TutorMaster.Commitment commitFirst, TutorMaster.Commitment commitSecond)      //ask if the 15 minute time block of the first has the same values as the second
        {
            return (commitFirst.Class == commitSecond.Class && commitFirst.Location == commitSecond.Location
                    && commitFirst.Open == commitSecond.Open && commitFirst.Weekly == commitSecond.Weekly
                    && commitFirst.ID == commitSecond.ID);
        }

        private string getCommitTime(TutorMaster.Commitment commit)                                             //get the c# datetime object of the commit's start time and cast it to a string
        {
            return Convert.ToDateTime(commit.StartTime).ToString().Split(' ')[1] + " " + Convert.ToDateTime(commit.StartTime).ToString().Split(' ')[2];
        }

        private string getCommitTime15(TutorMaster.Commitment commit15)                                         //get the c# datetime object of the commit's start time 15 minutes in the future and cast it to a string
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
                var partnerData = (from row in db.Users where row.ID == commit.ID select row).First();     //if there is a partner, get their first and lastname
                partner = partnerData.FirstName + " " + partnerData.LastName;
            }
            if(commit.Class.Contains('!'))                                                                 //if commit has ! point in its class, print everything but the !
            {
                commit.Class = commit.Class.Substring(0, commit.Class.Length-1);
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

        private string getDay(DateTime date)                                                                         //get string on date time object that has the day
        {
            string[] st = date.ToString("D").Split(',');                                                             //split it by the comma
            string day = st[0];                                                                                      //only take the day
            return day;
        }

        //loading pending and accepted appointment functions
        private void loadAppointments(bool reject)
        {
            newNotifs = 0;
            numCancels = 0;
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();                                                //open Database
            int num = db.StudentCommitments.Count();                                                                 //see if there are any student committments at all
            if (num > 0)
            {


                List<Commitment> cmtList = (from stucmt in db.StudentCommitments
                                            where stucmt.ID == id
                                            join cmt in db.Commitments on stucmt.CmtID equals cmt.CmtID
                                            select cmt).ToList();                                                   //get all of the student commitments for this student that is signed in

                QuickSort(ref cmtList, cmtList.Count());                                                            //sort their list using QuickSort


                removeOpens(ref cmtList);                                                                           //remove their open slots

                if (cmtList.Count > 0)
                {                                                                                                   //initialize carries to the first commitment
                    TutorMaster.Commitment initialCommit = cmtList[0];
                    string startTime = Convert.ToDateTime(cmtList[0].StartTime).ToString();
                    string endTime = Convert.ToDateTime(cmtList[0].StartTime).AddMinutes(15).ToString();
                    int numCmts = cmtList.Count;
                    List<TutorMaster.Commitment> newAppoints = new List<Commitment>();                              //carry around a list for the new appointments that have !
                    List<TutorMaster.Commitment> cancels = new List<Commitment>();

                    for (int i = 0; i < numCmts - 1; i++)
                    {
                        DateTime currentCommitDate = Convert.ToDateTime(cmtList[i].StartTime);                      //get datetime of commitment we are on in loop
                        DateTime nextCommitDate = Convert.ToDateTime(cmtList[i + 1].StartTime);                     //get datetime of commitment ahead of it

                        if (cmtList[i].Class.Contains('!'))                                                         //if it has an exclamation, add it to the new appointments list
                        {
                            newAppoints.Add(cmtList[i]);
                        }
                        else if (cmtList[i].Class == "@")
                        {
                            cancels.Add(cmtList[i]);
                        }

                        //if the two commits are distinct besides time and current commit is within week of start time
                        if (!sameCategory(cmtList[i], cmtList[i + 1]) || currentCommitDate.AddMinutes(15) != nextCommitDate)
                        {
                            endTime = endTime = Convert.ToDateTime(cmtList[i].StartTime).AddMinutes(15).ToString();                                               //update endtime and add what we have so far
                            if(cmtList[i].Class.Contains('!'))                                                      //if this chunk of time is a new appointment
                            {
                                for (int j = 0; j < newAppoints.Count(); j++)                                       //go through each time block in new appointment and get rid of !
                                {
                                    newAppoints[j].Class = newAppoints[j].Class.Substring(0, newAppoints[j].Class.Length - 1);
                                }
                                newAppoints.Clear();                                                                //clear the newAppointments list to make room for the next chunk
                                db.SaveChanges();                                                                   //save changes to database
                                newNotifs++;                                                                        //record the new appointment
                            }
                            else if (cmtList[i].Class == "@")
                            {
                                for (int c = 0; c < cancels.Count(); c++)                                       //go through each time block in new appointment and get rid of !
                                {
                                    cancels[c].Class = "-";
                                }
                                cancels.Clear();                                                                //clear the newAppointments list to make room for the next chunk
                                db.SaveChanges();                                                                   //save changes to database
                                numCancels++;                                                                        //record the new appointment
                            }
                            addToAppointments(initialCommit, startTime, endTime);                                   //add the chunk of time to our listviews

                            //initialize carries to be the next commitment and begin scanning for that
                            startTime = Convert.ToDateTime(cmtList[i + 1].StartTime).ToString();
                            endTime = Convert.ToDateTime(cmtList[i + 1].StartTime).AddMinutes(15).ToString();
                            initialCommit = cmtList[i + 1];
                        }

                    }
                    endTime = Convert.ToDateTime(cmtList[cmtList.Count() - 1].StartTime).AddMinutes(15).ToString();
                    addToAppointments(initialCommit, startTime, endTime);
                    if (newNotifs > 0 && !reject)                                                                             //if we have any new appointments, send the user a message about them
                    {
                        MessageBox.Show("You have " + newNotifs.ToString() + " new notification(s) in your appointments");
                    }
                    if (numCancels > 0 && !reject)
                    {
                        MessageBox.Show("You have " + numCancels.ToString() + " cancellations whose time periods have returned to your availability as open times that are not weekly.");
                    }
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

            if (accepted(commit))                                                                                                                //if commit accepted, add to accepted listview
            {
                lvFinalized.Items.Add(new ListViewItem(new string[] { startTime, endTime, commit.Class, commit.Location, 
                    commit.Open.ToString(), commit.Tutoring.ToString(), commit.Weekly.ToString(), partner, commit.ID.ToString() }));
            }
            else if (waitingForLocation(commit))                                                                                                 //if waiting for location to be proposed
            {                                                                                                                                    //add to pending tutor listview
                lvPendingTutor.Items.Add(new ListViewItem(new string[] { startTime, endTime, commit.Class, commit.Location, 
                    commit.Open.ToString(), commit.Tutoring.ToString(), commit.Weekly.ToString(), partner, commit.ID.ToString() }));
            }
            else if (waitingForTutee(commit))                                                                                                    //if tutor waiting for tutee to respond to location
            {                                                                                                                                    //add to pending tutee listview
                lvTutor.Items.Add(new ListViewItem(new string[] { startTime, endTime, commit.Class, commit.Location, 
                    commit.Open.ToString(), commit.Tutoring.ToString(), commit.Weekly.ToString(), partner, commit.ID.ToString() }));
            }
            else if (waitingForLocationApproval(commit))                                                                                         //if waiting for location approval
            {                                                                                                                                    //add to pending tutee listview
                lvPendingTutee.Items.Add(new ListViewItem(new string[] { startTime, endTime, commit.Class, commit.Location, 
                    commit.Open.ToString(), commit.Tutoring.ToString(), commit.Weekly.ToString(), partner, commit.ID.ToString() }));
            }
            else if (waitingForTutor(commit))                                                                                                    //if waiting for tutor to respond to appointment
            {                                                                                                                                    //add to tutee listview
                lvTutee.Items.Add(new ListViewItem(new string[] { startTime, endTime, commit.Class, commit.Location, 
                    commit.Open.ToString(), commit.Tutoring.ToString(), commit.Weekly.ToString(), partner, commit.ID.ToString() }));
            }

        }

        private void removeOpens(ref List<TutorMaster.Commitment> cmtList)
        {                                                                  //if commitment is open, remove it from the commitlist
            for (int i = 0; i < cmtList.Count(); i++)
            {
                if (isOpen(cmtList[i]))
                {
                    cmtList.Remove(cmtList[i]);
                }
            }
        }

        private bool isOpen(TutorMaster.Commitment commit)                     //criteria for a commitment to be open
        {
            return (commit.Class == "-" && commit.Location == "-" && commit.Open == true && commit.Tutoring == false && commit.ID == -1);
        }

        private bool waitingForTutor(TutorMaster.Commitment commit)            //criteria for a commitment to be waiting for a tutor
        {
            return (commit.Class != "-" && commit.Location == "-" && commit.Open == false && commit.Tutoring == false && commit.ID != -1);
        }

        private bool waitingForLocation(TutorMaster.Commitment commit)         //criteria for a commitment to be waiting for a location
        {
            return (commit.Class != "-" && commit.Location == "-" && commit.Open == false && commit.Tutoring == true && commit.ID != -1);
        }

        private bool waitingForLocationApproval(TutorMaster.Commitment commit) //criteria for a commitment to be waiting for a location approval
        {
            return (commit.Class != "-" && commit.Location.Contains('?') && commit.Open == false && commit.Tutoring == false && commit.ID != -1);
        }

        private bool waitingForTutee(TutorMaster.Commitment commit)            //criteria for a commitment to be waiting for a tutee
        {
            return (commit.Class != "-" && commit.Location.Contains('?') && commit.Open == false && commit.Tutoring == true && commit.ID != -1);
        }

        private bool accepted(TutorMaster.Commitment commit)                   //criteria for a commitment to be in the accepted state
        {
            return (commit.Class != "-" && !commit.Location.Contains('?') && commit.Location != "-" && commit.Open == false && commit.ID != -1);
        }

        //btn to add open slots and its helper functions
        private void btnAddOpenBlock_Click(object sender, EventArgs e)
        {
            //first, error check to make sure that the user put something for each dropdownbox
            if ((string.IsNullOrWhiteSpace(combStartHour.Text))|| (string.IsNullOrWhiteSpace(combStartMinute.Text)
            || (string.IsNullOrWhiteSpace(combStartAmPm.Text)))|| (string.IsNullOrWhiteSpace(combEndHour.Text))
            || (string.IsNullOrWhiteSpace(combEndMinute.Text)) || (string.IsNullOrWhiteSpace(combEndAmPm.Text)))
            {
                MessageBox.Show("Please fill out a starting and ending day, hour, minute, and part of day");
            }
            else
            {
                //get the starttime data for the commitments
                int startHour = int.Parse(combStartHour.Text);
                int startMinute = int.Parse(combStartMinute.Text);
                string startAmPm = combStartAmPm.Text;

                //convert hour to correct time given the input for the DateTime class
                //add 12 if its past 12PM 
                //if its 12AM, set the hour to 0
                if (startAmPm == "PM" && startHour != 12)
                {
                    startHour += 12;
                }
                else if (startAmPm == "AM" && startHour == 12)
                {
                    startHour = 0;
                }
                //get the endtime data for the commitments
                int endHour = int.Parse(combEndHour.Text);
                int endMinute = int.Parse(combEndMinute.Text);
                string endAmPm = combEndAmPm.Text;

                //convert hour to correct time given the input for the DateTime class
                //add 12 if its past 12PM 
                //if its 12AM, set the hour to 0
                if (endAmPm == "PM" && endHour != 12)
                {
                    endHour += 12;
                }
                else if (endAmPm == "AM" && endHour == 12)
                {
                    endHour = 0;
                }

                //get whether or not it is a weekly commitment
                bool weekly = cbxWeekly.Checked;
                
                //record start and end time based on what user input in adding for their availability
                DateTime startTime = new DateTime(dayStartDateTime.Value.Year, dayStartDateTime.Value.Month, dayStartDateTime.Value.Day, startHour, startMinute, 0); 
                DateTime endTime = new DateTime(dayEndDateTime.Value.Year, dayEndDateTime.Value.Month, dayEndDateTime.Value.Day, endHour, endMinute, 0);
                getAvail(startTime, endTime, weekly);
            }
        }

        private void getAvail(DateTime startTime, DateTime endTime, bool weekly)
        {
            DateTime begin = startTime;
            int compare = begin.CompareTo(endTime);
            if (compare < 0)
            {
                while (compare < 0)                      //if the first date is earlier than the second date
                {
                    if (!recordedTime(begin))            //and if this time slot has not already been recorded
                    {
                        add15Block(begin, weekly);       //add the 15 minute time block and whether or not its weekly
                    }
                    begin = begin.AddMinutes(15);        //repeat this process until we get to the endtime
                    compare = begin.CompareTo(endTime);
                }
                DateTime start = new DateTime(weekStartDateTime.Value.Year, weekStartDateTime.Value.Month, weekStartDateTime.Value.Day, 0, 0, 0);
                loadAvail(start);                        //reload the availability
            }
            else
            {
                MessageBox.Show("Please have your start time at an earlier time then your end time.");
            }
        }

        private bool recordedTime(DateTime begin)
        {
            //TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            //bool found = false;

            //var date = (from stucmt in db.StudentCommitments
            //            where stucmt.ID == id
            //            join cmt in db.Commitments on stucmt.CmtID equals cmt.CmtID
            //            select cmt.StartTime).ToList(); //pull all of the student's commitments

            //int dateCount = date.Count();               //count the number of dates

            //for (int i = 0; i < dateCount; i++)
            //{
            //    if (begin == Convert.ToDateTime(date[i]))//if a datetime is already taken, say it has and output a message about it
            //    {
            //        found = true;
            //        MessageBox.Show(date[i].ToString() + " is already in the database, this will not be added");
            //    }
            //}

            bool found = BinarySearch(begin);
            if (found)
            {
                MessageBox.Show(begin.ToString() + " is already in the database, this will not be added");
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

                    //add the commitment first and then add the student commitment
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

        private int getNextCmtId()                                                                      //go into database and get the last commitment ID
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

        private int getNextStdCmtKey()                                                                 //go into database and get the last student commitment ID
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
        //change the listviews to show the appropriate data based on the user's selection
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

        //set texts of the labels up above each listview in the student's schedule
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
                btnRejectTutor.Enabled = true;
            }
            else
            {
                btnAcceptAddLoc.Enabled = false;
                btnRejectTutor.Enabled = false;
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
        
        private void btnRejectTutor_Click(object sender, EventArgs e)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            List<string> commits = new List<string>();

            for (int i = 0; i < lvPendingTutor.CheckedItems.Count; i++)
            {
                commits.Add(lvPendingTutor.CheckedItems[i].SubItems[0].Text.ToString() + "," + lvPendingTutor.CheckedItems[i].SubItems[1].Text.ToString() + "," + lvPendingTutor.CheckedItems[i].SubItems[8].Text.ToString());
            }

            for (int n = 0; n < lvTutor.CheckedItems.Count; n++)
            {
                commits.Add(lvTutor.CheckedItems[n].SubItems[0].Text.ToString() + "," + lvTutor.CheckedItems[n].SubItems[1].Text.ToString() + "," + lvTutor.CheckedItems[n].SubItems[8].Text.ToString());
            }

            List<Commitment> tutorCmtList = (from stucmt in db.StudentCommitments
                                             where stucmt.ID == id
                                             join cmt in db.Commitments on stucmt.CmtID equals cmt.CmtID
                                             select cmt).ToList();

            for (int f = 0; f < commits.Count(); f++)
            {
                DateTime startDate = getStartTime(commits[f]);
                DateTime endDate = getEndTime(commits[f]);

                for (int c = 0; c < tutorCmtList.Count(); c++)
                {
                    if (DateTime.Compare(startDate, Convert.ToDateTime(tutorCmtList[c].StartTime)) <= 0 && DateTime.Compare(endDate, Convert.ToDateTime(tutorCmtList[c].StartTime)) > 0)
                    {
                        tutorCmtList[c].Weekly = false;
                        tutorCmtList[c].Tutoring = false;
                        tutorCmtList[c].Class = "-";
                        tutorCmtList[c].Location = "-";
                        tutorCmtList[c].Open = true;
                        tutorCmtList[c].ID = -1;
                        db.SaveChanges();
                    }
                }

                int partnerID = Convert.ToInt32(commits[f].Split(',')[2]);
                
                List<Commitment> tuteeCmtList = (from stucmt in db.StudentCommitments
                                                 where stucmt.ID == partnerID
                                                 join cmt in db.Commitments on stucmt.CmtID equals cmt.CmtID
                                                 select cmt).ToList();
                
                for (int m = 0; m < tuteeCmtList.Count(); m++)
                {
                    if (DateTime.Compare(startDate, Convert.ToDateTime(tuteeCmtList[m].StartTime)) <= 0 && DateTime.Compare(endDate, Convert.ToDateTime(tuteeCmtList[m].StartTime)) > 0)
                    {
                        tuteeCmtList[m].Weekly = false;
                        tuteeCmtList[m].Tutoring = false;
                        tuteeCmtList[m].Class = "@";
                        tuteeCmtList[m].Location = "-";
                        tuteeCmtList[m].Open = true;
                        tuteeCmtList[m].ID = -1;
                        db.SaveChanges();
                    }
                }
            }
            DateTime start = DateTime.Now;
            lvTutor.Items.Clear();
            lvPendingTutor.Items.Clear();
            lvFinalized.Items.Clear();
            lvPendingTutee.Items.Clear();
            lvTutee.Items.Clear();

            loadAvail(start);
            loadAppointments(true);
        }
        
        private void btnCancelFinalized_Click(object sender, EventArgs e)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            List<string> commits = new List<string>();

            for (int i = 0; i < lvFinalized.CheckedItems.Count; i++)
            {
                commits.Add(lvFinalized.CheckedItems[i].SubItems[0].Text.ToString() + "," + lvFinalized.CheckedItems[i].SubItems[1].Text.ToString() + "," + lvFinalized.CheckedItems[i].SubItems[8].Text.ToString());
            }


            List<Commitment> stdCmtList = (from stucmt in db.StudentCommitments
                                             where stucmt.ID == id
                                             join cmt in db.Commitments on stucmt.CmtID equals cmt.CmtID
                                             select cmt).ToList();

            for (int f = 0; f < commits.Count(); f++)
            {
                DateTime startDate = getStartTime(commits[f]);
                DateTime endDate = getEndTime(commits[f]);

                for (int c = 0; c < stdCmtList.Count(); c++)
                {
                    if (DateTime.Compare(startDate, Convert.ToDateTime(stdCmtList[c].StartTime)) <= 0 && DateTime.Compare(endDate, Convert.ToDateTime(stdCmtList[c].StartTime)) > 0)
                    {
                        stdCmtList[c].Weekly = false;
                        stdCmtList[c].Tutoring = false;
                        stdCmtList[c].Class = "-";
                        stdCmtList[c].Location = "-";
                        stdCmtList[c].Open = true;
                        stdCmtList[c].ID = -1;
                        db.SaveChanges();
                    }
                }

                int partnerID = Convert.ToInt32(commits[f].Split(',')[2]);

                List<Commitment> partnerCmtList = (from stucmt in db.StudentCommitments
                                                 where stucmt.ID == partnerID
                                                 join cmt in db.Commitments on stucmt.CmtID equals cmt.CmtID
                                                 select cmt).ToList();

                for (int m = 0; m < partnerCmtList.Count(); m++)
                {
                    if (DateTime.Compare(startDate, Convert.ToDateTime(partnerCmtList[m].StartTime)) <= 0 && DateTime.Compare(endDate, Convert.ToDateTime(partnerCmtList[m].StartTime)) > 0)
                    {
                        partnerCmtList[m].Weekly = false;
                        partnerCmtList[m].Tutoring = false;
                        partnerCmtList[m].Class = "@";
                        partnerCmtList[m].Location = "-";
                        partnerCmtList[m].Open = true;
                        partnerCmtList[m].ID = -1;
                        db.SaveChanges();
                    }
                }
            }

            DateTime start = DateTime.Now;
            lvTutor.Items.Clear();
            lvPendingTutor.Items.Clear();
            lvFinalized.Items.Clear();
            lvPendingTutee.Items.Clear();
            lvTutee.Items.Clear();

            loadAvail(start);
            loadAppointments(true);
        }

        private void btnRejectTutee_Click(object sender, EventArgs e)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            List<string> commits = new List<string>();

            for (int i = 0; i < lvPendingTutee.CheckedItems.Count; i++)
            {
                commits.Add(lvPendingTutee.CheckedItems[i].SubItems[0].Text.ToString() + "," + lvPendingTutee.CheckedItems[i].SubItems[1].Text.ToString() + "," + lvPendingTutee.CheckedItems[i].SubItems[8].Text.ToString());
            }

            for (int n = 0; n < lvTutee.CheckedItems.Count; n++)
            {
                commits.Add(lvTutee.CheckedItems[n].SubItems[0].Text.ToString() + "," + lvTutee.CheckedItems[n].SubItems[1].Text.ToString() + "," + lvTutee.CheckedItems[n].SubItems[8].Text.ToString());
            }


            List<Commitment> tuteeCmtList = (from stucmt in db.StudentCommitments
                                           where stucmt.ID == id
                                           join cmt in db.Commitments on stucmt.CmtID equals cmt.CmtID
                                           select cmt).ToList();

            for (int f = 0; f < commits.Count(); f++)
            {
                DateTime startDate = getStartTime(commits[f]);
                DateTime endDate = getEndTime(commits[f]);

                for (int c = 0; c < tuteeCmtList.Count(); c++)
                {
                    if (DateTime.Compare(startDate, Convert.ToDateTime(tuteeCmtList[c].StartTime)) <= 0 && DateTime.Compare(endDate, Convert.ToDateTime(tuteeCmtList[c].StartTime)) > 0)
                    {
                        tuteeCmtList[c].Weekly = false;
                        tuteeCmtList[c].Tutoring = false;
                        tuteeCmtList[c].Class = "-";
                        tuteeCmtList[c].Location = "-";
                        tuteeCmtList[c].Open = true;
                        tuteeCmtList[c].ID = -1;
                        db.SaveChanges();
                    }
                }

                int partnerID = Convert.ToInt32(commits[f].Split(',')[2]);

                List<Commitment> tutorCmtList = (from stucmt in db.StudentCommitments
                                                   where stucmt.ID == partnerID
                                                   join cmt in db.Commitments on stucmt.CmtID equals cmt.CmtID
                                                   select cmt).ToList();

                for (int m = 0; m < tutorCmtList.Count(); m++)
                {
                    if (DateTime.Compare(startDate, Convert.ToDateTime(tutorCmtList[m].StartTime)) <= 0 && DateTime.Compare(endDate, Convert.ToDateTime(tutorCmtList[m].StartTime)) > 0)
                    {
                        tutorCmtList[m].Weekly = false;
                        tutorCmtList[m].Tutoring = false;
                        tutorCmtList[m].Class = "@";
                        tutorCmtList[m].Location = "-";
                        tutorCmtList[m].Open = true;
                        tutorCmtList[m].ID = -1;
                        db.SaveChanges();
                    }
                }
            }
            DateTime start = DateTime.Now;
            lvTutor.Items.Clear();
            lvPendingTutor.Items.Clear();
            lvFinalized.Items.Clear();
            lvPendingTutee.Items.Clear();
            lvTutee.Items.Clear();

            loadAvail(start);
            loadAppointments(true);
        }

        private void btnFinalize_Click(object sender, EventArgs e)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();

            List<Commitment> tuteeCmtList = (from stucmt in db.StudentCommitments
                                             where stucmt.ID == id
                                             join cmt in db.Commitments on stucmt.CmtID equals cmt.CmtID
                                             select cmt).ToList();

            for (int i = 0; i < lvPendingTutee.CheckedItems.Count; i++)
            {
                DateTime startDate = getListViewTime(lvPendingTutee.CheckedItems[i].SubItems[0].Text);
                DateTime endDate = getListViewTime(lvPendingTutee.CheckedItems[i].SubItems[1].Text);

                for (int c = 0; c < tuteeCmtList.Count(); c++)
                {
                    if (DateTime.Compare(startDate, Convert.ToDateTime(tuteeCmtList[c].StartTime)) <= 0 && DateTime.Compare(endDate, Convert.ToDateTime(tuteeCmtList[c].StartTime)) > 0)
                    {
                        tuteeCmtList[c].Location = tuteeCmtList[c].Location.Substring(0, tuteeCmtList[c].Location.Length - 1);
                        db.SaveChanges();
                    }
                }

                int partnerID = Convert.ToInt32(lvPendingTutee.CheckedItems[i].SubItems[8].Text);

                List<Commitment> tutorCmtList = (from stucmt in db.StudentCommitments
                                                 where stucmt.ID == partnerID
                                                 join cmt in db.Commitments on stucmt.CmtID equals cmt.CmtID
                                                 select cmt).ToList();

                for (int m = 0; m < tutorCmtList.Count(); m++)
                {
                    if (DateTime.Compare(startDate, Convert.ToDateTime(tutorCmtList[m].StartTime)) <= 0 && DateTime.Compare(endDate, Convert.ToDateTime(tutorCmtList[m].StartTime)) > 0)
                    {
                        tutorCmtList[m].Location = tutorCmtList[m].Location.Substring(0, tutorCmtList[m].Location.Length - 1);
                        db.SaveChanges();
                    }
                }
            }
            DateTime start = DateTime.Now;
            
            lvFinalized.Items.Clear();
            lvPendingTutee.Items.Clear();
            lvPendingTutor.Items.Clear();
            lvTutee.Items.Clear();
            lvTutor.Items.Clear();
            
            loadAvail(start);
            
            loadAppointments(false);
        }


        private DateTime getListViewTime(string slot)                                    //take a string of datetime from listview's string
        {
            string dateString = slot.Split(' ')[0];                                      //get the entire start datetime string
            
            int month = Convert.ToInt32(dateString.Split('/')[0]);                       //convert its month value into an integer
            int day = Convert.ToInt32(dateString.Split('/')[1]);                         //convert its day value into an integer

            string timeString = slot.Split(' ')[1];                                      //get the time part of the start datetime

            int hour = Convert.ToInt32(timeString.Split(':')[0]);                        //convert its hour into an integer
            int min = Convert.ToInt32(timeString.Split(':')[1]);                         //convert its minutes into an integer

            string amPm = slot.Split(' ')[2];                                            //record whether this is in the morning or evening

            if (hour < 12 && amPm == "PM")                                               //add 12 to hours if necessary
            {
                hour += 12;
            }
            else if (hour == 12 && amPm == "AM")                                         //if first hour of the day, set hour to 0
            {
                hour = 0;
            }
            DateTime date = new DateTime(2017, month, day, hour, min, 0);                //make a datetime instance with the collected data and return it
            return date;
        }



        private DateTime getStartTime(string slot)                                    //take a string that has the start datetime seperated by a comma with the end datetime
        {
            string startDateTime = slot.Split(',')[0];
            string startDate = startDateTime.Split(' ')[0];                           //get the entire date of start datetime string
            string startTime = startDateTime.Split(' ')[1];                           //get the entire time of the start datetime string
            string amPm = startDateTime.Split(' ')[2];                                //record if this is in the morning or evening

            int month = Convert.ToInt32(startDate.Split('/')[0]);                     //get the month
            int day = Convert.ToInt32(startDate.Split('/')[1]);                       //get the day
            int year = Convert.ToInt32(startDate.Split('/')[2]);                      //get the year

            int hour = Convert.ToInt32(startTime.Split(':')[0]);                      //get the hour
            int min = Convert.ToInt32(startTime.Split(':')[1]);                       //get the minutes


            if (hour < 12 && amPm == "PM")                                            //add 12 to hours if necessary
            {
                hour += 12;
            }
            else if (hour == 12 && amPm == "AM")                                      //if first hour of the day, set hour to 0
            {
                hour = 0;
            }
            DateTime date = new DateTime(year, month, day, hour, min, 0);             //make a datetime instance with the collected data and return it
            return date;
        }

        private DateTime getEndTime(string slot)                                      //take a string that has the start datetime seperated by a comma with the end datetime
        {
            string startDateTime = slot.Split(',')[1];
            string startDate = startDateTime.Split(' ')[0];                           //get the entire date of end datetime string
            string startTime = startDateTime.Split(' ')[1];                           //get the entire time of the end datetime string
            string amPm = startDateTime.Split(' ')[2];                                //record if this is in the morning or evening

            int month = Convert.ToInt32(startDate.Split('/')[0]);                     //get the month
            int day = Convert.ToInt32(startDate.Split('/')[1]);                       //get the day
            int year = Convert.ToInt32(startDate.Split('/')[2]);                      //get the year

            int hour = Convert.ToInt32(startTime.Split(':')[0]);
            int min = Convert.ToInt32(startTime.Split(':')[1]);


            if (hour < 12 && amPm == "PM")                                            //add 12 to hours if necessary
            {
                hour += 12;
            }
            else if (hour == 12 && amPm == "AM")                                      //if first hour of the day, set hour to 0
            {
                hour = 0;
            }

            DateTime date = new DateTime(year, month, day, hour, min, 0);             //make a datetime instance with the collected data and return it
            return date;
        }

        private bool BinarySearch(DateTime date)
        {
            bool found = false;
            int first = 0;
            int last = searchList.Count()-1;
            while (first <= last && !found)
            {
                int midpoint = (first + last) / 2;
                if (DateTime.Compare(Convert.ToDateTime(searchList[midpoint].StartTime), date) == 0)
                {
                    found = true;
                    return found;
                }
                else
                {
                    if (DateTime.Compare(date, Convert.ToDateTime(searchList[midpoint].StartTime)) < 0)
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

        //code to have hidden columns that have partner ID numbers in the listviews and prevent user from seeing said columns
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