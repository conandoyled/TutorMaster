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
        //id of student that signs into the account and a list of their scheduled commitments that will be used for checking for duplicates if necessary
        private int id;
        private List<TutorMaster.Commitment> searchList = new List<Commitment>();
        
        //constructor
        public StudentMain(int accID)
        {
            id = accID;                                                                                           //get the ID of the student
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();                                             //open database
            bool tutor = (bool)(from row in db.Students where row.ID == id select row.Tutor).First();             //get if they are a tutee and/or tutor
            bool tutee = (bool)(from row in db.Students where row.ID == id select row.Tutee).First();
            string name = (from row in db.Users where row.ID == id select row.FirstName + " " + row.LastName).First();
            InitializeComponent();
            label2.Text = "Welcome " + name + "!";
            
            if (!tutee)
            {
                btnMakeRequest.Visible = false;                                                                   //if they are not a tutee, they can't make requests
                btnAdvanceRequest.Visible = false;
                lblTutee.Visible = false;
                lblAttention.Location = new Point(lblAttention.Location.X, lblAttention.Location.Y - 29);
                lblWaiting.Location = new Point(lblWaiting.Location.X, lblWaiting.Location.Y - 29);
            }
            if (!tutor)
            {
                lblTutor.Visible = false;
                lblTutee.Location = new Point(lblTutee.Location.X, lblTutee.Location.Y - 29);
                lblAttention.Location = new Point(lblAttention.Location.X, lblAttention.Location.Y - 29);
                lblWaiting.Location = new Point(lblWaiting.Location.X, lblWaiting.Location.Y - 29);
            }
            populateColumns(tutor, tutee);                                                                        //initialize the columns of listviews approriately
            weekStartDateTime.Value = DateTime.Today;                                                             //initialize datetime pickers to be today
            DateTime start = DateTime.Now;
            setSelectedTab(start);
            loadAvail(start);                                                                                     //load availability starting from today
            setUpLabels(start);                                                                                   //set up the labels above each schedule list view a week from today
            loadAppointments(false);                                                                              //load the appointments
            disableButtons();                                                                                     //disable necessary buttons
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //helper functions for getting a students schedule and appointments and then adding them to the appropriate listviews

        private void clearDayListViews()
        {
            //Clear the ListViews
            
            lvSunday.Items.Clear();
            lvMonday.Items.Clear();
            lvTuesday.Items.Clear();
            lvWednesday.Items.Clear();
            lvThursday.Items.Clear();
            lvFriday.Items.Clear();
            lvSaturday.Items.Clear();
        }

        private void redrawDayListViews()
        {
            //redraw every listview
            lvSunday.Invalidate();
            lvMonday.Invalidate();
            lvTuesday.Invalidate();
            lvWednesday.Invalidate();
            lvThursday.Invalidate();
            lvFriday.Invalidate();
            lvSaturday.Invalidate();
        }

        private void getRidOfOutOfBounds(DateTime start, ref List<TutorMaster.Commitment> cmtList)              //trim the list of commitments to only be the ones that are a week from today
        {
            //this function takes a commitment list and removes all of the commitments that are not on the same day or within a week ahead of a given startTime
            int length = cmtList.Count();
            for (int i = 0; i < length; i++)
            {
                if (DateTime.Compare(Convert.ToDateTime(cmtList[i].StartTime), start.AddDays(7)) > 0 || DateTime.Compare(Convert.ToDateTime(cmtList[i].StartTime), start) <= 0) //if its not within a week, remove it
                {
                    cmtList.Remove(cmtList[i]);
                    i--;
                    length--;
                }
            }
        }

        private string getYesNo(bool b)                                                                         //pass in a boolean value and return yes if it is true and no if the value is false
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

        private void addOnlyCommit(Commitment commit)
        {
            addToListView(commit, Convert.ToDateTime(commit.StartTime).DayOfWeek.ToString(), Commits.getCommitTime(commit), Commits.getCommitTime15(commit));
        }

        private System.Drawing.Color getColor(Commitment commit)
        {
            if(Commits.isOpen(commit))
            {
                return System.Drawing.Color.FromArgb(7, 104, 10);
            }
            else if (Commits.isAccepted(commit))
            {
                if ((bool) commit.Tutoring)
                {
                    return System.Drawing.Color.FromArgb(6, 120, 122);
                }
                else
                {
                    return System.Drawing.Color.FromArgb(4, 76, 114);
                }
            }
            else if ((bool) commit.Tutoring)
            {
                if (Commits.waitingForTutor(commit) || Commits.waitingForLocation(commit))
                {
                    return System.Drawing.Color.FromArgb(137, 6, 6);
                }
                else
                {
                    return System.Drawing.Color.FromArgb(4, 13, 137);
                }
            }
            else
            {
                if (Commits.waitingForTutee(commit) || Commits.waitingForLocationApproval(commit))
                {
                    return System.Drawing.Color.FromArgb(137, 6, 6);
                }
                else
                {
                    return System.Drawing.Color.FromArgb(4, 13, 137);
                }
            }
        }

        private void setSelectedTab(DateTime setDate)
        {
            DayOfWeek a = setDate.DayOfWeek;
            switch (a)
            {
                case DayOfWeek.Sunday:
                    dayTabs.SelectedTab = tabSunday;
                    break;
                case DayOfWeek.Monday:
                    dayTabs.SelectedTab = tabMonday;
                    break;
                case DayOfWeek.Tuesday:
                    dayTabs.SelectedTab = tabTuesday;
                    break;
                case DayOfWeek.Wednesday:
                    dayTabs.SelectedTab = tabWednesday;
                    break;
                case DayOfWeek.Thursday:
                    dayTabs.SelectedTab = tabThursday;
                    break;
                case DayOfWeek.Friday:
                    dayTabs.SelectedTab = tabFriday;
                    break;
                case DayOfWeek.Saturday:
                    dayTabs.SelectedTab = tabSaturday;
                    break;
            }
        }

        //end of helper functions
        
        //load student's entire schedule into days listview functions

        private void loadAvail(DateTime start)
        {
            clearDayListViews();

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

                    SortsAndSearches.QuickSort(ref cmtList, cmtList.Count());                                                         //sort the list by DateTime

                    searchList = cmtList;                                                                            //get a copy of the commitList for checking for duplicates

                    getRidOfOutOfBounds(start, ref cmtList);

                    if (cmtList.Count() == 1)                                                                        //base case of having only one committment
                    {
                        addOnlyCommit(cmtList[0]);    
                    }
                    else if (cmtList.Count() > 1)                                                                    //general case of having more than one committment
                    {
                                                                                                                     //information on the block of time we will be carrying around to clump the commitments together
                        TutorMaster.Commitment initialCommit = new Commitment();                                     //start commit (because it has the information I'll need to load to listviews)
                        string today = "";                                                                           //day of this commitment so I know which listview to add it to
                        string startTime = "";                                                                       //start time of commitment
                        string endTime = "";                                                                         //end time of commitment

                        Commits.updateInformation(ref startTime, ref endTime, ref today, ref initialCommit, cmtList[0]);     //get the information from the first commitment

                        for (int i = 0; i < cmtList.Count() - 1; i++)                                                //for each commitment except for the last one
                        {
                            DateTime currentCommitDate = Convert.ToDateTime(cmtList[i].StartTime);                   //get datetime of commitment we are on in loop
                            DateTime nextCommitDate = Convert.ToDateTime(cmtList[i + 1].StartTime);                  //get datetime of commitment ahead of it
                                                                                                                     //if the two commitments are distinct besides time and current commit is within week of start time
                            if (!Commits.sameCategory(cmtList[i], cmtList[i + 1]))
                            {
                                endTime = Commits.getCommitTime15(cmtList[i]);                                               //update endtime and add what we have so far to the listview
                                addToListView(initialCommit, Convert.ToDateTime(initialCommit.StartTime).DayOfWeek.ToString(), startTime, endTime);

                                Commits.updateInformation(ref startTime, ref endTime, ref today, ref initialCommit, cmtList[i + 1]);
                                                                                                                     //update our startTime, endTime, day of the week, and commitment information to the next commitment
                                                                                                                     //and begin scanning for the next block of time to add to the listview
                            }
                            else                                                                                     //if the current and next commit are in the same category
                            {
                                string day = currentCommitDate.DayOfWeek.ToString();                                              //if it is, get the day of the week of the current commit in for loop
                                if (today == day)                                                                    //compare it to the day of the initial commit we are going to add
                                {
                                    if (Commits.nextCommitAdjacent(currentCommitDate, nextCommitDate))                       //if our next commit is 15 minutes later of our current
                                    {
                                        endTime = Commits.getCommitTime15(cmtList[i]);                                       //only update endTime
                                    }
                                    else
                                    {
                                        endTime = Commits.getCommitTime15(cmtList[i]);                                       //if next commit is not, update endTime
                                        addToListView(initialCommit, day, startTime, endTime);                       //add what we have so far

                                        Commits.updateInformation(ref startTime, ref endTime, ref today, ref initialCommit, cmtList[i + 1]);
                                                                                                                     //update our block information values to the next commitment
                                                                                                                     //and begin scanning for the next block of time to add to the listview
                                    }
                                }
                                else                                                                                 //if its not the same, update endTime and add it to the appropriate listview
                                {
                                    endTime = Commits.getCommitTime(cmtList[i]);
                                    addToListView(initialCommit, today, startTime, endTime);
                                                                                                                     //update carries including today so the program knows to move onto looking for next day
                                    Commits.updateInformation(ref startTime, ref endTime, ref today, ref initialCommit, cmtList[i]);
                                    today = day;
                                }
                            }
                        }
                        endTime = Commits.getCommitTime15(cmtList[cmtList.Count() - 1]);                                              //get the last endTime
                        addToListView(initialCommit, Convert.ToDateTime(initialCommit.StartTime).DayOfWeek.ToString(), startTime, endTime);//update last clump commitment into Listview
                    }
                }
                redrawDayListViews();
            }
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
            else if (commit.Class == "@")                                                                  //if commit has nothing but @, then just print -
            {
                commit.Class = "-";
            }

            System.Drawing.Color color = getColor(commit);
            ListViewItem itemCommit = new ListViewItem(new string[] { startTime, endTime, commit.Class, commit.Location, open, tutoring, weekly, partner });
            itemCommit.ForeColor = color;
            switch (day)                                                                                   //depending on the day of the week, add the block to the right listview
            {
                case "Sunday":
                    lvSunday.Items.Add(itemCommit);
                    break;
                case "Monday":
                    lvMonday.Items.Add(itemCommit);
                    break;
                case "Tuesday":
                    lvTuesday.Items.Add(itemCommit);
                    break;
                case "Wednesday":
                    lvWednesday.Items.Add(itemCommit);
                    break;
                case "Thursday":
                    lvThursday.Items.Add(itemCommit);
                    break;
                case "Friday":
                    lvFriday.Items.Add(itemCommit);
                    break;
                case "Saturday":
                    lvSaturday.Items.Add(itemCommit);
                    break;
            }
        }
        
        //end of functions dealing with adding schedule to day listviews
        
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        
        //beginning of pending and accepted appointment functions
        //BE AWARE: May use some of the helper functions from adding schedule to day listviews above
        //helper functions to break up appointments, send messages about new notifications and cancellations, and add items to appointment listviews
        private void updateInformationAppointments(ref string start, ref string end, ref Commitment oldCommit, Commitment newCommit)
        {
            //update the start time string, end time string, and commitment for the start of a new time block
            start = Convert.ToDateTime(newCommit.StartTime).ToString();
            end = Convert.ToDateTime(newCommit.StartTime).AddMinutes(15).ToString();
            oldCommit = newCommit;
        }

        private bool newAppointment(Commitment commit)
        {
            //check if the commitment's class has an exclamation point. if it does, this is a new appointment
            return commit.Class.Contains('!');
        }

        private bool newCancel(Commitment commit)
        {
            //if the commitment class is just the @ sign, then this time slot is open but is a newly cancelled appointment
            return commit.Class == "@";
        }
        
        private void sendNotificationsMessage(int newNotifs, bool reject)
        {
            if (newNotifs > 0 && !reject)                                                                             //if we have any new appointments, send the user a message about them
            {
                MessageBox.Show("You have " + newNotifs.ToString() + " new notification(s) in your appointments");
            }
        }

        private void sendCancellationsMessage(int numCancels, bool reject)
        {
            if (numCancels > 0 && !reject)                                                                             //if we have any cancellations, send the user a message about them
            {
                MessageBox.Show("You have " + numCancels.ToString() + " cancellations whose time periods have returned to your availability as open times that are not weekly.");
            }
        }

        private void markNewAppointmentsAsRead(ref List<Commitment> newAppointsList)
        {
            for (int j = 0; j < newAppointsList.Count(); j++)                                    //go through each time block in new appointment and get rid of !
            {
                newAppointsList[j].Class = newAppointsList[j].Class.Substring(0, newAppointsList[j].Class.Length - 1);
            }
            newAppointsList.Clear();                                                             //clear the newAppointments list to make room for the next chunk
        }

        private void markCancelsAsRead(ref List<Commitment> cancelList)
        {
            for (int c = 0; c < cancelList.Count(); c++)                                         //go through each time block in new appointment and get rid of !
            {
                cancelList[c].Class = "-";
            }
            cancelList.Clear();                                                                  //clear the newAppointments list to make room for the next chunk
        }
        //end of helper functions dealing with loading appointments into listviews
        //begin functions to break up schedule of a student, extract appointments and add to listviews

        private void loadAppointments(bool reject)
        {
            int newNotifs = 0;                                                                                      //begin count for new notifications
            int numCancels = 0;                                                                                     //begin count for cancellations
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();                                               //open Database
            int num = db.StudentCommitments.Count();                                                                //see if there are any student committments at all
            if (num > 0)
            {
                List<Commitment> cmtList = (from stucmt in db.StudentCommitments
                                            where stucmt.ID == id
                                            join cmt in db.Commitments on stucmt.CmtID equals cmt.CmtID
                                            select cmt).ToList();                                                   //get all of the student commitments for this student that is signed in

                SortsAndSearches.QuickSort(ref cmtList, cmtList.Count());                                                            //sort their list using QuickSort

                Commits.removeOpens(ref cmtList);                                                                           //remove their open slots

                if (cmtList.Count > 0)
                {                                                                                                   //initialize first block of time information to the information in the first commitment
                    TutorMaster.Commitment initialCommit = new Commitment();
                    string startTime = "";
                    string endTime = "";
                    updateInformationAppointments(ref startTime, ref endTime, ref initialCommit, cmtList[0]);

                    int numCmts = cmtList.Count;
                    List<TutorMaster.Commitment> newAppointsList = new List<Commitment>();                          //carry around a list for the new appointments that have !
                    List<TutorMaster.Commitment> cancelList = new List<Commitment>();                               //carry around a list for the cancellations that are classes that are just @

                    for (int i = 0; i < numCmts - 1; i++)
                    {
                        DateTime currentCommitDate = Convert.ToDateTime(cmtList[i].StartTime);                      //get datetime of commitment we are on in loop
                        DateTime nextCommitDate = Convert.ToDateTime(cmtList[i + 1].StartTime);                     //get datetime of commitment ahead of it

                        if (newAppointment(cmtList[i]))                                                             //if it is a new appointment, add it to the new notification list
                        {
                            newAppointsList.Add(cmtList[i]);
                        }
                        else if (newCancel(cmtList[i]))                                                             //if it is a new cancellation, add it to the cancellist
                        {
                            cancelList.Add(cmtList[i]);
                        }
                                                                                                                     //if the two commits are distinct besides time and current commit is within week of start time
                        if (!Commits.sameCategory(cmtList[i], cmtList[i + 1]) || currentCommitDate.AddMinutes(15) != nextCommitDate)
                        {
                            endTime = Commits.getNextEndTime(cmtList[i]);                                                    //update endtime and add what we have so far
                            if (newAppointment(cmtList[i]))                                                           //if this chunk of time is a new appointment
                            {
                                markNewAppointmentsAsRead(ref newAppointsList);                                      //mark the new notifications as read
                                newNotifs++;                                                                         //record the new appointment
                            }
                            else if (newCancel(cmtList[i]))
                            {
                                markCancelsAsRead(ref cancelList);                                                   //mark the new cancellations as read
                                numCancels++;                                                                        //record the new appointment
                            }
                            addToAppointments(initialCommit, startTime, endTime);                                    //add the chunk of time to our listviews

                                                                                                                     //update time block information to be the next commitment and begin scanning for that
                            updateInformationAppointments(ref startTime, ref endTime, ref initialCommit, cmtList[i + 1]);
                        }
                    }
                    db.SaveChanges();                                                                                //save ths changes to the database
                    endTime = Commits.getNextEndTime(cmtList[cmtList.Count() - 1]);                                          //update the end time for the last commitment
                    handleLastCommitment(cmtList, initialCommit, startTime, endTime, ref newNotifs, ref numCancels); //check the last commitment and then add it
                    
                    sendNotificationsMessage(newNotifs, reject);                                                     //send a notification message if one needs to be sent
                    sendCancellationsMessage(numCancels, reject);                                                    //send a cancellation mess if one needs to be sent
                }
            }
        }

        private void handleLastCommitment(List<Commitment> cmtList, Commitment initialCommit, string startTime, string endTime, ref int newNotifs, ref int numCancels)
        {
            DateTime currentCommitDate = Convert.ToDateTime(cmtList[cmtList.Count()-2].StartTime);                  //get datetime of second to last commitment
            DateTime nextCommitDate = Convert.ToDateTime(cmtList[cmtList.Count()-1].StartTime);                     //get datetime of the last commitment
            if (Commits.sameCategory(cmtList[cmtList.Count() - 1], cmtList[cmtList.Count() - 2]) && currentCommitDate.AddMinutes(15).CompareTo(nextCommitDate) == 0)
            {                                                                                                       //check if they are adjacent and in the same category
                addToAppointments(initialCommit, startTime, endTime);                                               //if they are, add the last time block as it is
            }
            else                                                                                                    //if they are not, then add the last time block as its own 15 minute time block
            {
                updateInformationAppointments(ref startTime, ref endTime, ref initialCommit, cmtList[cmtList.Count() - 1]);
                if (newAppointment(initialCommit))                                                                   //check if it will be a new notification or cancellation
                {
                    newNotifs++;
                }
                if (newCancel(initialCommit))
                {
                    numCancels++;
                }
                addToAppointments(initialCommit, startTime, endTime);                                               //add it to the listView
            }
        }

        //add time slots to appropriate appointment listviews for the student
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

            ListViewItem item = new ListViewItem(new string[] { startTime, endTime, commit.Class, commit.Location, 
                    commit.Open.ToString(), commit.Tutoring.ToString(), commit.Weekly.ToString(), partner, commit.ID.ToString() });
            if (Commits.isAccepted(commit))                                                                                                                //if commit accepted, add to accepted listview
            {
                lvFinalized.Items.Add(item);
            }
            else if (Commits.waitingForLocation(commit))                                                                                                 //if waiting for location to be proposed
            {                                                                                                                                    //add to pending tutor listview
                lvPendingTutor.Items.Add(item);
            }
            else if (Commits.waitingForTutee(commit))                                                                                                    //if tutor waiting for tutee to respond to location
            {                                                                                                                                    //add to pending tutee listview
                lvTutor.Items.Add(item);
            }
            else if (Commits.waitingForLocationApproval(commit))                                                                                         //if waiting for location approval
            {                                                                                                                                    //add to pending tutee listview
                lvPendingTutee.Items.Add(item);
            }
            else if (Commits.waitingForTutor(commit))                                                                                                    //if waiting for tutor to respond to appointment
            {                                                                                                                                    //add to tutee listview
                lvTutee.Items.Add(item);
            }

        }

        //end of functions to add appointments to appointments listviews
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //in case someone changes the startofweek datetime picker
        //change the listviews to show the appropriate data based on the user's selection
        private void weekStartDateTime_ValueChanged(object sender, EventArgs e)
        {
            //when upper right dateTime picker's value changes, reload the day listviews and reset the labels
            DateTime start = new DateTime(weekStartDateTime.Value.Year, weekStartDateTime.Value.Month, weekStartDateTime.Value.Day, 0, 0, 0);
            if (start.CompareTo(DateTime.Now) < 0)
            {
                start = DateTime.Now;
            }
            loadAvail(start);
            setUpLabels(start);
            setSelectedTab(start);
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
                DateTime dateD = start.AddDays(i);                                              //add days up to a week to our start date
                string day = dateD.ToString("D").Split(',')[0];                                 //get the day of the week of the day
                switch (day)                                                                    //set the appropriate label
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

        //disable buttons that should not be enabled at the start of the program and adjust their style to reflect such
        private void disableButtons()
        {
            btnCancelFinalized.Enabled = false;
            btnCancelFinalized.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
            btnAcceptAddLoc.Enabled = false;
            btnAcceptAddLoc.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
            btnRejectTutor.Enabled = false;
            btnRejectTutor.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
            btnFinalize.Enabled = false;
            btnFinalize.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
            btnRejectTutee.Enabled = false;
            btnRejectTutee.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
            btnRemoveAvail.Enabled = false;
            btnRemoveAvail.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
        }

        //display the request form
        private void btnMakeRequest_Click(object sender, EventArgs e) 
        {
            RequestForm g = new RequestForm(id);
            g.Show();
            Close();
        }

        //deselect every item in the appointments listview tabs
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
            } 
            if (lvTutee.CheckedItems.Count > 0)
            {
                foreach (ListViewItem listItem in lvTutee.Items)
                {
                    listItem.Checked = false;
                }
            } 
            if (lvPendingTutor.CheckedItems.Count > 0)
            {
                foreach (ListViewItem listItem in lvPendingTutor.Items)
                {
                    listItem.Checked = false;
                }
            } 
            if (lvPendingTutee.CheckedItems.Count > 0)
            {
                foreach (ListViewItem listItem in lvPendingTutee.Items)
                {
                    listItem.Checked = false;
                }
            }
        }

        //deselect every item in the appointments listview tabs
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
            } 
            if (lvTutee.CheckedItems.Count > 0)
            {
                foreach (ListViewItem listItem in lvTutee.Items)
                {
                    listItem.Checked = false;
                }
            } 
            if (lvPendingTutor.CheckedItems.Count > 0)
            {
                foreach (ListViewItem listItem in lvPendingTutor.Items)
                {
                    listItem.Checked = false;
                }
            } 
            if (lvPendingTutee.CheckedItems.Count > 0)
            {
                foreach (ListViewItem listItem in lvPendingTutee.Items)
                {
                    listItem.Checked = false;
                }
            }
        }

        //when an item is checked in the listview finalized, enable or disable the cancel button depending on the checked item count
        private void lvFinalized_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            int itemsChecked = lvFinalized.CheckedItems.Count; //CheckedItems.Count tells how many things in the list box are clicked
            if (itemsChecked > 0)
            {
                btnCancelFinalized.Enabled = true;
                btnCancelFinalized.BackColor = System.Drawing.Color.FromArgb(226, 226, 226);
            }
            else
            {
                btnCancelFinalized.Enabled = false;
                btnCancelFinalized.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
            }
        }

        private void btnAcceptAddLoc_Click(object sender, EventArgs e)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            List<string> commits = new List<string>();

            for (int i = 0; i < lvPendingTutor.CheckedItems.Count; i++)                             //go through each checked item and get a string of the starttime, endtime, and partner id and add it to the list
            {
                commits.Add(getlvPendingTutorCheckedInfo(i));
            }
            
            ProposeLocationForm g = new ProposeLocationForm(id, commits, false);                           //pass the signed in student's id and this list to the propose location form
            g.Show();
            this.Close();
        }

        private string getlvPendingTutorCheckedInfo(int i)                             //go through each checked item and get a string of the starttime, endtime, and partner id and add it to the list
        {
            return lvPendingTutor.CheckedItems[i].SubItems[0].Text.ToString() + "," + lvPendingTutor.CheckedItems[i].SubItems[1].Text.ToString() + "," + lvPendingTutor.CheckedItems[i].SubItems[8].Text.ToString();
        }

        private string getlvTutorCheckedInfo(int n)                                    //go through each checked item and get a string of the starttime, endtime, and partner id and add it to the list
        {
            return lvTutor.CheckedItems[n].SubItems[0].Text.ToString() + "," + lvTutor.CheckedItems[n].SubItems[1].Text.ToString() + "," + lvTutor.CheckedItems[n].SubItems[8].Text.ToString();
        }

        private string getlvFinalizedCheckedInfo(int d)                                //go through each checked item and get a string of the starttime, endtime, and partner id and add it to the list
        {
            return lvFinalized.CheckedItems[d].SubItems[0].Text.ToString() + "," + lvFinalized.CheckedItems[d].SubItems[1].Text.ToString() + "," + lvFinalized.CheckedItems[d].SubItems[8].Text.ToString();
        }

        private string getlvTuteeCheckedInfo(int e)                                    //go through each checked item and get a string of the starttime, endtime, and partner id and add it to the list
        {
            return lvTutee.CheckedItems[e].SubItems[0].Text.ToString() + "," + lvTutee.CheckedItems[e].SubItems[1].Text.ToString() + "," + lvTutee.CheckedItems[e].SubItems[8].Text.ToString();
        }

        private string getlvTuteePendingInfo(int x)                                    //go through each checked item and get a string of the starttime, endtime, and partner id and add it to the list
        {
            return lvPendingTutee.CheckedItems[x].SubItems[0].Text.ToString() + "," + lvPendingTutee.CheckedItems[x].SubItems[1].Text.ToString() + "," + lvPendingTutee.CheckedItems[x].SubItems[8].Text.ToString();
        }

        private bool inTheTimeSlot(DateTime startDate, DateTime endDate, Commitment commit)
        {
            return (DateTime.Compare(startDate, Convert.ToDateTime(commit.StartTime)) <= 0 && DateTime.Compare(endDate, Convert.ToDateTime(commit.StartTime)) > 0);
        }

        private void clearAppointmentListviews()
        {
            lvTutor.Items.Clear();
            lvPendingTutor.Items.Clear();
            lvFinalized.Items.Clear();
            lvPendingTutee.Items.Clear();
            lvTutee.Items.Clear();
        }

        private void btnRejectTutor_Click(object sender, EventArgs e)
        {
            List<string> commits = new List<string>();

            for (int i = 0; i < lvPendingTutor.CheckedItems.Count; i++)
            {
                commits.Add(getlvPendingTutorCheckedInfo(i));
            }

            for (int n = 0; n < lvTutor.CheckedItems.Count; n++)
            {
                commits.Add(getlvTutorCheckedInfo(n));
            }

            for (int f = 0; f < commits.Count(); f++)
            {
                int accID = id;
                cancelAppointments(commits, accID, false);

                int partnerID = Convert.ToInt32(commits[f].Split(',')[2]);
                cancelAppointments(commits, partnerID, true);
            }
            DateTime start = DateTime.Now;
            clearAppointmentListviews();

            loadAvail(start);
            loadAppointments(true);
        }
        
        private void btnCancelFinalized_Click(object sender, EventArgs e)
        {
            List<string> commits = new List<string>();

            for (int d = 0; d < lvFinalized.CheckedItems.Count; d++)
            {
                commits.Add(getlvFinalizedCheckedInfo(d));
            }

            for (int f = 0; f < commits.Count(); f++)
            {
                int accID = id;
                cancelAppointments(commits, accID, false);

                int partnerID = Convert.ToInt32(commits[f].Split(',')[2]);
                cancelAppointments(commits, partnerID, true);
            }
            DateTime start = DateTime.Now;
            clearAppointmentListviews();

            loadAvail(start);
            loadAppointments(true);
        }
        
        private void btnRejectTutee_Click(object sender, EventArgs e)
        {
            List<string> commits = new List<string>();

            for (int v = 0; v < lvTutee.CheckedItems.Count; v++)
            {
                commits.Add(getlvTuteeCheckedInfo(v));
            }
            
            for (int x = 0; x < lvPendingTutee.CheckedItems.Count; x++)
            {
                commits.Add(getlvTuteePendingInfo(x));
            }

            for (int f = 0; f < commits.Count(); f++)
            {
                int accID = id;
                cancelAppointments(commits, accID, false);

                int partnerID = Convert.ToInt32(commits[f].Split(',')[2]);
                cancelAppointments(commits, partnerID, true);
            }
            DateTime start = DateTime.Now;
            clearAppointmentListviews();

            loadAvail(start);
            loadAppointments(true);
        }

        private bool betweenGivenStartAndEndTime(DateTime startDate, DateTime endDate, Commitment commit)
        {
            return DateTime.Compare(startDate, Convert.ToDateTime(commit.StartTime)) <= 0 && DateTime.Compare(endDate, Convert.ToDateTime(commit.StartTime)) > 0;
        }

        private bool sameTime(Commitment commit, DateTime weekBack)
        {
            return DateTime.Compare(Convert.ToDateTime(commit.StartTime), weekBack) == 0;
        }

        private bool weekBackEarlier(DateTime weekBack, Commitment commit)
        {
            return DateTime.Compare(weekBack, Convert.ToDateTime(commit.StartTime)) < 0;
        }

        private void cancelAppointments(List<string> commits, int accID, bool partner)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();                                                   //Connect to database

            List<Commitment> stdCmtList = (from stucmt in db.StudentCommitments                                         //get the student's commitments
                                           where stucmt.ID == accID
                                           join cmt in db.Commitments on stucmt.CmtID equals cmt.CmtID
                                           select cmt).ToList();

            SortsAndSearches.QuickSort(ref stdCmtList, stdCmtList.Count());                                                              //sort them

            for (int f = 0; f < commits.Count(); f++)                                                                   //for each cancellation in the list
            {
                DateTime startDate = Commits.getStartTime(commits[f]);                                                          //get its start and end times of the cancellation
                DateTime endDate = Commits.getEndTime(commits[f]);

                for (int c = 0; c < stdCmtList.Count(); c++)                                                            //go through the student's commitments
                {
                    if (betweenGivenStartAndEndTime(startDate, endDate, stdCmtList[c]))                                 //if the commitment is between the start and end times
                    {
                        if (stdCmtList[c].Weekly == true)                                                               //if the commitment is weekly
                        {
                            DateTime startSemes = new DateTime(2017, 1, 1, 0, 0, 0);                                    //look a week back from the commitment
                            DateTime weekBack = Convert.ToDateTime(stdCmtList[c].StartTime).AddDays(-7);
                            while (DateTime.Compare(startSemes, weekBack) <= 0)                                         //if it is at the day of the start of the semester or before it
                            {                                                                                           //begin to execute a binary search
                                bool found = false;                                                                     //NOTE: this has to be done here so the changes made can be recorded in the database connection
                                int first = 0;
                                int last = stdCmtList.Count() - 1;
                                while (first <= last && !found)                                                         //if we haven't found the time in the list and start search pos is less than or equal to our end search position
                                {
                                    int midpoint = (first + last) / 2;                                                  //get the midpoint between the two
                                    if (sameTime(stdCmtList[midpoint], weekBack))                                       //if the mid commit and the weekback time at the same
                                    {
                                        if (Commits.openOrSameType(stdCmtList[c], stdCmtList[midpoint]))                        //ask if it is open or the same type of commitment as our cancel commit
                                        {
                                            stdCmtList[midpoint].Weekly = false;                                        //if it is, turn its weekly to false
                                            db.SaveChanges();                                                           //save the changes to the database
                                        }
                                        found = true;                                                                   //set found equal to true
                                    }
                                    else
                                    {
                                        if (weekBackEarlier(weekBack, stdCmtList[midpoint]))                            //adjust the start and end search indexes as necessary
                                        {
                                            last = midpoint - 1;
                                        }
                                        else
                                        {
                                            first = midpoint + 1;
                                        }
                                    }
                                }
                                weekBack = weekBack.AddDays(-7);                                                        //repeat the process for another date that is a week back until you reach the beginning of the semester
                            }
                        }

                        stdCmtList[c].Weekly = false;                                                                   //for this commitment, set it to a new, open state
                        stdCmtList[c].Tutoring = false;
                        stdCmtList[c].Location = "-";
                        if (partner)                                                                                 
                        {
                            stdCmtList[c].Class = "@";                                                                  //if they have a partner, set it to a cancel state
                        }
                        else
                        {
                            stdCmtList[c].Class = "-";                                                                  //else, just say its open
                        }
                        stdCmtList[c].Open = true;
                        stdCmtList[c].ID = -1;
                        db.SaveChanges();
                    }
                }
            }
        }

        private string takeOffLocationQuestionMark(Commitment commit)
        {
            return commit.Location.Substring(0, commit.Location.Length - 1);
        }

        private void FinalizeTutee()
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();                                                      //connect to the database

            List<Commitment> cmtList = (from stucmt in db.StudentCommitments                                               //get signed in student's list of commitments
                                             where stucmt.ID == id
                                             join cmt in db.Commitments on stucmt.CmtID equals cmt.CmtID
                                             select cmt).ToList();

            for (int i = 0; i < lvPendingTutee.CheckedItems.Count; i++)                                                    //go through each selected item in the pending tutee listview
            {
                DateTime startDate = getListViewTime(lvPendingTutee.CheckedItems[i].SubItems[0].Text);                     //get the dateTime of the time in the start time column
                DateTime endDate = getListViewTime(lvPendingTutee.CheckedItems[i].SubItems[1].Text);                       //get the dateTime of the time in the end time column
                for (int c = 0; c < cmtList.Count(); c++)
                {
                    if (inTheTimeSlot(startDate, endDate, cmtList[c]))                                                     //take off question mark if the commitment is in between the two times
                    {
                        cmtList[c].Location = takeOffLocationQuestionMark(cmtList[c]);
                        db.SaveChanges();                                                                                  //save the changes
                    }
                }
            }
        }

        private void FinalizeTutors()
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            for (int i = 0; i < lvPendingTutee.CheckedItems.Count; i++)
            {
                int partnerID = Convert.ToInt32(lvPendingTutee.CheckedItems[i].SubItems[8].Text);                  //for each checked item, get the partner id
                
                List<Commitment> cmtList = (from stucmt in db.StudentCommitments                                   //get that partner's commitment list
                                            where stucmt.ID == partnerID
                                            join cmt in db.Commitments on stucmt.CmtID equals cmt.CmtID
                                            select cmt).ToList();

                DateTime startDate = getListViewTime(lvPendingTutee.CheckedItems[i].SubItems[0].Text);             //get the dateTime of the time in the start time column 
                DateTime endDate = getListViewTime(lvPendingTutee.CheckedItems[i].SubItems[1].Text);               //get the dateTime of the time in the end time column
                for (int c = 0; c < cmtList.Count(); c++)
                {
                    if (inTheTimeSlot(startDate, endDate, cmtList[c]))                                             //if the commitment is between the times, take off the question mark
                    {
                        cmtList[c].Class = cmtList[c].Class + "!";
                        cmtList[c].Location = takeOffLocationQuestionMark(cmtList[c]);
                        db.SaveChanges();                                                                          //save the changes in the database
                    }
                }
            }
        }

        private void btnFinalize_Click(object sender, EventArgs e)
        {
            FinalizeTutee();                                                          //put the tutee appointments in a finalized state
            FinalizeTutors();                                                         //put the tutors appointments in finalized states

            DateTime start = DateTime.Now;
            clearAppointmentListviews();                                              //clear the appointment listviews

            loadAvail(start);                                                         //load schedule and appointment listviews again
            loadAppointments(false);
        }

        private DateTime getListViewTime(string slot)                                 //take a string of datetime from listview's string
        {                                                                             
            string dateString = slot.Split(' ')[0];                                   //get the entire start datetime string
                                                                                      
            int month = Convert.ToInt32(dateString.Split('/')[0]);                    //convert its month value into an integer
            int day = Convert.ToInt32(dateString.Split('/')[1]);                      //convert its day value into an integer
                                                                                      
            string timeString = slot.Split(' ')[1];                                   //get the time part of the start datetime
                                                                                      
            int hour = Convert.ToInt32(timeString.Split(':')[0]);                     //convert its hour into an integer
            int min = Convert.ToInt32(timeString.Split(':')[1]);                      //convert its minutes into an integer
                                                                                      
            string amPm = slot.Split(' ')[2];                                         //record whether this is in the morning or evening
                                                                                      
            if (hour < 12 && amPm == "PM")                                            //add 12 to hours if necessary
            {                                                                         
                hour += 12;                                                           
            }                                                                         
            else if (hour == 12 && amPm == "AM")                                      //if first hour of the day, set hour to 0
            {                                                                         
                hour = 0;                                                             
            }                                                                         
            DateTime date = new DateTime(2017, month, day, hour, min, 0);             //make a datetime instance with the collected data and return it
            return date;
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

        //end of code to have hidden columns that have partner ID numbers in the listviews

        private void btnRemoveAvail_Click(object sender, EventArgs e)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();                                              //connection to database

            List<string> removeList = loadItemsForRemoval();                                                       //get list of all the items the user has checked
            if (removeList.Count() != 1)                                                                           //only allow one and send a message otherwise
            {
                MessageBox.Show("Please pick exactly one open time slot to remove from your availability");
            }
            else
            {
                RemoveAvailForm g = new RemoveAvailForm(id, removeList);                                           //else, send that information to the remove availability form
                g.Show();
                this.Close();
            }
        }

        private List<string> loadItemsForRemoval()
        {
            //this function iterates through all of the day listviews and gets every item that is selected and puts the start date, end date, and partner id in a string and adds that string to a list

            List<string> all = new List<string>();
            
            for (int i = 0; i < lvSunday.CheckedItems.Count; i++)
            {
                DateTime startDate = getDate("Sunday", lvSunday.CheckedItems[i].SubItems[0].Text.ToString());
                DateTime endDate = getDate("Sunday", lvSunday.CheckedItems[i].SubItems[1].Text.ToString());
                string slot = startDate.ToString() + "," + endDate.ToString() + "," + lvSunday.CheckedItems[i].SubItems[6].Text.ToString(); ;
                all.Add(slot);
            }
            
            for (int n = 0; n < lvMonday.CheckedItems.Count; n++)
            {
                DateTime startDate = getDate("Monday", lvMonday.CheckedItems[n].SubItems[0].Text.ToString());
                DateTime endDate = getDate("Monday", lvMonday.CheckedItems[n].SubItems[1].Text.ToString());
                string slot = startDate.ToString() + "," + endDate.ToString() + "," + lvMonday.CheckedItems[n].SubItems[6].Text.ToString(); ;
                all.Add(slot);
            }

            for (int f = 0; f < lvTuesday.CheckedItems.Count; f++)
            {
                DateTime startDate = getDate("Tuesday", lvTuesday.CheckedItems[f].SubItems[0].Text.ToString());
                DateTime endDate = getDate("Tuesday", lvTuesday.CheckedItems[f].SubItems[1].Text.ToString());
                string slot = startDate.ToString() + "," + endDate.ToString() + "," + lvTuesday.CheckedItems[f].SubItems[6].Text.ToString(); ;
                all.Add(slot);
            }

            for (int j = 0; j < lvWednesday.CheckedItems.Count; j++)
            {
                DateTime startDate = getDate("Wednesday", lvWednesday.CheckedItems[j].SubItems[0].Text.ToString());
                DateTime endDate = getDate("Wednesday", lvWednesday.CheckedItems[j].SubItems[1].Text.ToString());
                string slot = startDate.ToString() + "," + endDate.ToString() + "," + lvWednesday.CheckedItems[j].SubItems[6].Text.ToString(); ;
                all.Add(slot);
            }

            for (int g = 0; g < lvThursday.CheckedItems.Count; g++)
            {
                DateTime startDate = getDate("Thursday", lvThursday.CheckedItems[g].SubItems[0].Text.ToString());
                DateTime endDate = getDate("Thursday", lvThursday.CheckedItems[g].SubItems[1].Text.ToString());
                string slot = startDate.ToString() + "," + endDate.ToString() + "," + lvThursday.CheckedItems[g].SubItems[6].Text.ToString(); ;
                all.Add(slot);
            }

            for (int p = 0; p < lvFriday.CheckedItems.Count; p++)
            {
                DateTime startDate = getDate("Friday", lvFriday.CheckedItems[p].SubItems[0].Text.ToString());
                DateTime endDate = getDate("Friday", lvFriday.CheckedItems[p].SubItems[1].Text.ToString());
                string slot = startDate.ToString() + "," + endDate.ToString() + "," + lvFriday.CheckedItems[p].SubItems[6].Text.ToString();
                all.Add(slot);
            }

            for (int q = 0; q < lvSaturday.CheckedItems.Count; q++)
            {
                DateTime startDate = getDate("Saturday", lvFriday.CheckedItems[q].SubItems[0].Text.ToString());
                DateTime endDate = getDate("Saturday", lvFriday.CheckedItems[q].SubItems[1].Text.ToString());
                string slot = startDate.ToString() + "," + endDate.ToString() + "," + lvSaturday.CheckedItems[q].SubItems[6].Text.ToString(); ;
                all.Add(slot);
            }

            return all;
        }

        private DateTime getDate(string dayOfWeek, string startTime)
        {
            string monthDay = "";

            switch (dayOfWeek)                                                                  //read a label based on the day of the week indicated and split the text on the comma and extract info
            {
                case "Sunday":
                    monthDay = lblSunday.Text.Split(',')[1];
                    break;
                case "Monday":
                    monthDay = lblMonday.Text.Split(',')[1];
                    break;
                case "Tuesday":
                    monthDay = lblTuesday.Text.Split(',')[1];
                    break;
                case "Wednesday":
                    monthDay = lblWednesday.Text.Split(',')[1];
                    break;
                case "Thursday":
                    monthDay = lblThursday.Text.Split(',')[1];
                    break;
                case "Friday":
                    monthDay = lblFriday.Text.Split(',')[1];
                    break;
                case "Saturday":
                    monthDay = lblSaturday.Text.Split(',')[1];
                    break;
            }

            int year = 2017;
            
            List<string> monthsList = new List<string>() {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"};

            
            string month = monthDay.Split(' ')[1];
            int monthInt = 0;
            for (int n = 0; n < monthsList.Count(); n++)                                                    //get the index of the month we are operating in
            {
                if (month == monthsList[n])
                {
                    monthInt = n+1;
                    break;
                }
            }
            int day = Convert.ToInt32(monthDay.Split(' ')[2]);                                              //get the number of the day we are interested in


            int hour = Convert.ToInt32(startTime.Split(':')[0]);                                            //get the hour from the time parameter
            int min = Convert.ToInt32(startTime.Split(':')[1]);                                             //get the minute from the time parameter
            string amPm = startTime.Split(' ')[1];                                                          //get whether this is the morning or evening

            if (amPm == "PM" && hour != 12)                                                                 //add 12 or set to 0 if necessary
            {
                hour += 12;
            }
            else if (amPm == "AM" && hour == 12)
            {
                hour = 0;
            }

            DateTime result = new DateTime(year, monthInt, day, hour, min ,0);                              //return dateTime of the strings we parsed
            return result;
        }

        private void lvPendingTutor_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            int numPendingTutor = lvPendingTutor.CheckedItems.Count;
            int numTutor = lvTutor.CheckedItems.Count;
            if (numPendingTutor > 0 && numTutor > 0)                                   //if both lv PendingTutor and lvTutor have something checked, only reject should be on
            {
                btnAcceptAddLoc.Enabled = false;
                btnAcceptAddLoc.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
                btnRejectTutor.Enabled = true;
                btnRejectTutor.BackColor = System.Drawing.Color.FromArgb(226, 226, 226);
            }
            else if (numPendingTutor > 0 && numTutor == 0)                             //if PendingTutor has something checked by tutor doesn't then both buttons should be on
            {
                btnAcceptAddLoc.Enabled = true;
                btnAcceptAddLoc.BackColor = System.Drawing.Color.FromArgb(226, 226, 226);
                btnRejectTutor.Enabled = true;
                btnRejectTutor.BackColor = System.Drawing.Color.FromArgb(226, 226, 226);
            }
            else if (numTutor > 0 && numPendingTutor == 0)                             //if tutor has something checked but pendingTutor does not, only reject should be on
            {
                btnAcceptAddLoc.Enabled = false;
                btnAcceptAddLoc.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
                btnRejectTutor.Enabled = true;
                btnRejectTutor.BackColor = System.Drawing.Color.FromArgb(226, 226, 226);
            }
            else                                                                       //if neither of them have anything checked, then both buttons should be disabled
            {
                btnAcceptAddLoc.Enabled = false;
                btnAcceptAddLoc.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
                btnRejectTutor.Enabled = false;
                btnRejectTutor.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
            }
        }

        private void lvTutor_ItemChecked_2(object sender, ItemCheckedEventArgs e)
        {
            int numPendingTutor = lvPendingTutor.CheckedItems.Count;
            int numTutor = lvTutor.CheckedItems.Count;
            if (numPendingTutor > 0 && numTutor > 0)                                   //if both lv PendingTutor and lvTutor have something checked, only reject should be on
            {
                btnAcceptAddLoc.Enabled = false;
                btnAcceptAddLoc.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
                btnRejectTutor.Enabled = true;
                btnRejectTutor.BackColor = System.Drawing.Color.FromArgb(226, 226, 226);
            }
            else if (numPendingTutor > 0 && numTutor == 0)                             //if PendingTutor has something checked by tutor doesn't then both buttons should be on
            {
                btnAcceptAddLoc.Enabled = true;
                btnAcceptAddLoc.BackColor = System.Drawing.Color.FromArgb(226, 226, 226);
                btnRejectTutor.Enabled = true;
                btnRejectTutor.BackColor = System.Drawing.Color.FromArgb(226, 226, 226);
            }
            else if (numTutor > 0 && numPendingTutor == 0)                             //if tutor has something checked but pendingTutor does not, only reject should be on
            {
                btnAcceptAddLoc.Enabled = false;
                btnAcceptAddLoc.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
                btnRejectTutor.Enabled = true;
                btnRejectTutor.BackColor = System.Drawing.Color.FromArgb(226, 226, 226);
            }
            else                                                                       //if neither of them have anything checked, then both buttons should be disabled
            {
                btnAcceptAddLoc.Enabled = false;
                btnAcceptAddLoc.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
                btnRejectTutor.Enabled = false;
                btnRejectTutor.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
            }
        }

        private void lvTutee_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            int numPendingTutee = lvPendingTutee.CheckedItems.Count;
            int numTutee = lvTutee.CheckedItems.Count;
            if (numPendingTutee > 0 && numTutee > 0)                                   //if both lv PendingTutee and lvTutee have something checked, only reject should be on
            {
                btnFinalize.Enabled = false;
                btnFinalize.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
                btnRejectTutee.Enabled = true;
                btnRejectTutee.BackColor = System.Drawing.Color.FromArgb(226, 226, 226);
            }
            else if (numPendingTutee > 0 && numTutee == 0)                             //if PendingTutee has something checked by tutee doesn't then both buttons should be on
            {
                btnFinalize.Enabled = true;
                btnFinalize.BackColor = System.Drawing.Color.FromArgb(226, 226, 226);
                btnRejectTutee.Enabled = true;
                btnRejectTutee.BackColor = System.Drawing.Color.FromArgb(226, 226, 226);
            }
            else if (numTutee > 0 && numPendingTutee == 0)                             //if tutee has something checked but pendingTutee does not, only reject should be on
            {
                btnFinalize.Enabled = false;
                btnFinalize.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
                btnRejectTutee.Enabled = true;
                btnRejectTutee.BackColor = System.Drawing.Color.FromArgb(226, 226, 226);
            }
            else                                                                       //if neither of them have anything checked, then both buttons should be disabled
            {
                btnFinalize.Enabled = false;
                btnFinalize.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
                btnRejectTutee.Enabled = false;
                btnRejectTutee.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
            }
        }

        private void lvPendingTutee_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            int numPendingTutee = lvPendingTutee.CheckedItems.Count;
            int numTutee = lvTutee.CheckedItems.Count;
            if (numPendingTutee > 0 && numTutee > 0)                                   //if both lv PendingTutee and lvTutee have something checked, only reject should be on
            {
                btnFinalize.Enabled = false;
                btnFinalize.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
                btnRejectTutee.Enabled = true;
                btnRejectTutee.BackColor = System.Drawing.Color.FromArgb(226, 226, 226);
            }
            else if (numPendingTutee > 0 && numTutee == 0)                             //if PendingTutee has something checked by tutee doesn't then both buttons should be on
            {
                btnFinalize.Enabled = true;
                btnFinalize.BackColor = System.Drawing.Color.FromArgb(226, 226, 226);
                btnRejectTutee.Enabled = true;
                btnRejectTutee.BackColor = System.Drawing.Color.FromArgb(226, 226, 226);
            }
            else if (numTutee > 0 && numPendingTutee == 0)                             //if tutee has something checked but pendingTutee does not, only reject should be on
            {
                btnFinalize.Enabled = false;
                btnFinalize.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
                btnRejectTutee.Enabled = true;
                btnRejectTutee.BackColor = System.Drawing.Color.FromArgb(226, 226, 226);
            }
            else                                                                       //if neither of them have anything checked, then both buttons should be disabled
            {
                btnFinalize.Enabled = false;
                btnFinalize.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
                btnRejectTutee.Enabled = false;
                btnRejectTutee.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
            }
        }

        //these functions check to make sure every selected item in the day listviews are open, it one is not in any of them, it disables the remove availability button
        private void lvSunday_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            int itemsChecked = lvSunday.CheckedItems.Count;
            if (itemsChecked == 0)
            {
                btnRemoveAvail.Enabled = false;
                btnRemoveAvail.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
            }
            else
            {
                bool allOpen = true;
                for (int i = 0; i < itemsChecked; i++)
                {
                    string yesNo = lvSunday.CheckedItems[i].SubItems[4].Text.ToString();
                    if (yesNo == "No")
                    {
                        allOpen = false;
                    }
                }

                if (allOpen)
                {
                    btnRemoveAvail.Enabled = true;
                    btnRemoveAvail.BackColor = System.Drawing.Color.FromArgb(226, 226, 226);
                }
                else
                {
                    btnRemoveAvail.Enabled = false;
                    btnRemoveAvail.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
                }
            }
        }

        private void lvMonday_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            int itemsChecked = lvMonday.CheckedItems.Count;
            bool allOpen = true;
            if (itemsChecked == 0)
            {
                btnRemoveAvail.Enabled = false;
                btnRemoveAvail.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
            }
            else
            {
                for (int i = 0; i < itemsChecked; i++)
                {
                    string yesNo = lvMonday.CheckedItems[i].SubItems[4].Text.ToString();
                    if (yesNo == "No")
                    {
                        allOpen = false;
                    }
                }

                if (allOpen)
                {
                    btnRemoveAvail.Enabled = true;
                    btnRemoveAvail.BackColor = System.Drawing.Color.FromArgb(226, 226, 226);
                }
                else
                {
                    btnRemoveAvail.Enabled = false;
                }
            }
        }

        private void lvTuesday_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            int itemsChecked = lvTuesday.CheckedItems.Count;
            if (itemsChecked == 0)
            {
                btnRemoveAvail.Enabled = false;
                btnRemoveAvail.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
            }
            else
            {
                bool allOpen = true;
                for (int i = 0; i < itemsChecked; i++)
                {
                    string yesNo = lvTuesday.CheckedItems[i].SubItems[4].Text.ToString();
                    if (yesNo == "No")
                    {
                        allOpen = false;
                    }
                }

                if (allOpen)
                {
                    btnRemoveAvail.Enabled = true;
                    btnRemoveAvail.BackColor = System.Drawing.Color.FromArgb(226, 226, 226);
                }
                else
                {
                    btnRemoveAvail.Enabled = false;
                    btnRemoveAvail.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
                }
            }
        }

        private void lvWednesday_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            int itemsChecked = lvWednesday.CheckedItems.Count;
            if (itemsChecked == 0)
            {
                btnRemoveAvail.Enabled = false;
                btnRemoveAvail.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
            }
            else
            {
                bool allOpen = true;
                for (int i = 0; i < itemsChecked; i++)
                {
                    string yesNo = lvWednesday.CheckedItems[i].SubItems[4].Text.ToString();
                    if (yesNo == "No")
                    {
                        allOpen = false;
                    }
                }

                if (allOpen)
                {
                    btnRemoveAvail.Enabled = true;
                    btnRemoveAvail.BackColor = System.Drawing.Color.FromArgb(226, 226, 226);
                }
                else
                {
                    btnRemoveAvail.Enabled = false;
                    btnRemoveAvail.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
                }
            }
        }

        private void lvThursday_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            int itemsChecked = lvThursday.CheckedItems.Count;
            if (itemsChecked == 0)
            {
                btnRemoveAvail.Enabled = false;
                btnRemoveAvail.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
            }
            else
            {
                bool allOpen = true;
                for (int i = 0; i < itemsChecked; i++)
                {
                    string yesNo = lvThursday.CheckedItems[i].SubItems[4].Text.ToString();
                    if (yesNo == "No")
                    {
                        allOpen = false;
                    }
                }

                if (allOpen)
                {
                    btnRemoveAvail.Enabled = true;
                    btnRemoveAvail.BackColor = System.Drawing.Color.FromArgb(226, 226, 226);
                }
                else
                {
                    btnRemoveAvail.Enabled = false;
                    btnRemoveAvail.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
                }
            }
        }

        private void lvFriday_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            int itemsChecked = lvFriday.CheckedItems.Count;
            if (itemsChecked == 0)
            {
                btnRemoveAvail.Enabled = false;
                btnRemoveAvail.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
            }
            else
            {
                bool allOpen = true;
                for (int i = 0; i < itemsChecked; i++)
                {
                    string yesNo = lvFriday.CheckedItems[i].SubItems[4].Text.ToString();
                    if (yesNo == "No")
                    {
                        allOpen = false;
                    }
                }

                if (allOpen)
                {
                    btnRemoveAvail.Enabled = true;
                    btnRemoveAvail.BackColor = System.Drawing.Color.FromArgb(226, 226, 226);
                }
                else
                {
                    btnRemoveAvail.Enabled = false;
                    btnRemoveAvail.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
                }
            }
        }

        private void lvSaturday_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            int itemsChecked = lvSaturday.CheckedItems.Count;
            if (itemsChecked == 0)
            {
                btnRemoveAvail.Enabled = false;
                btnRemoveAvail.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
            }
            else
            {
                bool allOpen = true;
                for (int i = 0; i < itemsChecked; i++)
                {
                    string yesNo = lvSaturday.CheckedItems[i].SubItems[4].Text.ToString();
                    if (yesNo == "No")
                    {
                        allOpen = false;
                    }
                }

                if (allOpen)
                {
                    btnRemoveAvail.Enabled = true;
                    btnRemoveAvail.BackColor = System.Drawing.Color.FromArgb(226, 226, 226);
                }
                else
                {
                    btnRemoveAvail.Enabled = false;
                    btnRemoveAvail.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
                }
            }
        }
        
        //end of functions check to make sure every selected item in the day listviews are open, it one is not in any of them, it disables the remove availability button
        private void dayTabs_Selected(object sender, TabControlEventArgs e)
        {
            for (int i = 0; i < lvSunday.CheckedItems.Count; i++)
            {
                lvSunday.CheckedItems[i].Checked = false;
            }
            
            for (int n = 0; n < lvMonday.CheckedItems.Count; n++)
            {
                lvMonday.CheckedItems[n].Checked = false;
            }
           
            for (int f = 0; f < lvTuesday.CheckedItems.Count; f++)
            {
                lvTuesday.CheckedItems[f].Checked = false;
            }
            
            for (int j = 0; j < lvWednesday.CheckedItems.Count; j++)
            {
                lvWednesday.CheckedItems[j].Checked = false;
            }

            for (int c = 0; c < lvThursday.CheckedItems.Count; c++)
            {
                lvThursday.CheckedItems[c].Checked = false;
            }

            for (int t = 0; t < lvFriday.CheckedItems.Count; t++)
            {
                lvFriday.CheckedItems[t].Checked = false;
            }

            for (int a = 0; a < lvSaturday.CheckedItems.Count; a++)
            {
                lvSaturday.CheckedItems[a].Checked = false;
            }
            btnRemoveAvail.Enabled = false;
            btnRemoveAvail.BackColor = System.Drawing.Color.FromArgb(193, 200, 204);
        }

        private void tabControl2_Selected(object sender, TabControlEventArgs e)
        {
            for (int i = 0; i < lvTutor.CheckedItems.Count; i++)
            {
                lvTutor.CheckedItems[i].Checked = false;
            }
            
            for (int n = 0; n < lvPendingTutor.CheckedItems.Count; n++)
            {
                lvPendingTutor.CheckedItems[n].Checked = false;
            }

            for (int f = 0; f < lvTutee.CheckedItems.Count; f++)
            {
                lvTutee.CheckedItems[f].Checked = false;
            }

            for (int j = 0; j < lvPendingTutee.CheckedItems.Count; j++)
            {
                lvPendingTutee.CheckedItems[j].Checked = false;
            }

            for (int c = 0; c < lvFinalized.CheckedItems.Count; c++)
            {
                lvFinalized.CheckedItems[c].Checked = false;
            }
        }
        
        //logout button
        private void btnLogout_Click(object sender, EventArgs e)
        {
            Login g = new Login();
            g.Show();
            this.Close();
        }

        private void btnAdvanceRequest_Click(object sender, EventArgs e)
        {
            AdvancedRequest g = new AdvancedRequest(id);
            g.Show();
            Close();
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            ChangePasswordForm g = new ChangePasswordForm(id);
            g.Show();
            this.Close();
        }
        
        
    }
}