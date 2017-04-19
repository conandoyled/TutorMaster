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
    public partial class AdminSeeSchedule : Form
    {
        private int id;

        public AdminSeeSchedule(int accID)
        {
            id = accID;
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();                                             //open database
            bool tutor = (bool)(from row in db.Students where row.ID == id select row.Tutor).First();             //get if they are a tutee and/or tutor
            bool tutee = (bool)(from row in db.Students where row.ID == id select row.Tutee).First();
            InitializeComponent();
            populateColumns(tutor, tutee);                                                                        //initialize the columns of listviews approriately
            DateTime start = DateTime.Now;
            //loadAvail(start);                                                                                     //load availability starting from today
            loadAppointments(false);                                                                              //load the appointments
            disableButtons();                                                                                     //disable necessary buttons

            lblNameTitle.Text = (from row in db.Users where row.ID == id select row.FirstName + " " + row.LastName).First() + "'s Schedule";
        }

        //setup ListViews
        private void populateColumns(bool tutor, bool tutee)
        {
            lvOpen.CheckBoxes = true;
            lvOpen.Columns.Add("Start Time", 150);
            lvOpen.Columns.Add("End Time", 150);
            //lvOpen.Columns.Add("Class", 70);
            //lvOpen.Columns.Add("Location", 105);
            //lvOpen.Columns.Add("Open", 50);
            //lvOpen.Columns.Add("Tutoring", 75);
            lvOpen.Columns.Add("Weekly", 75);
            //lvOpen.Columns.Add("Partner", 115);

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
                tabControl1.TabPages.Remove(tabPendingTutor);
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
                tabControl1.TabPages.Remove(tabPendingTutee);
            }

        }

        private void disableButtons()
        {
            btnCancelFinalized.Enabled = false;
            btnAcceptAddLoc.Enabled = false;
            btnRejectTutor.Enabled = false;
            btnFinalize.Enabled = false;
            btnRejectTutee.Enabled = false;
        }

        private void loadAppointments(bool reject)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();                                                //open Database
            int num = db.StudentCommitments.Count();                                                                 //see if there are any student committments at all
            if (num > 0)
            {


                List<Commitment> cmtList = (from stucmt in db.StudentCommitments
                                            where stucmt.ID == id
                                            join cmt in db.Commitments on stucmt.CmtID equals cmt.CmtID
                                            select cmt).ToList();                                                   //get all of the student commitments for this student that is signed in

                QuickSort(ref cmtList, cmtList.Count());                                                            //sort their list using QuickSort

                if (cmtList.Count > 0)
                {                                                                                                   //initialize carries to the first commitment
                    TutorMaster.Commitment initialCommit = cmtList[0];
                    string startTime = Convert.ToDateTime(cmtList[0].StartTime).ToString();
                    string endTime = Convert.ToDateTime(cmtList[0].StartTime).AddMinutes(15).ToString();
                    int numCmts = cmtList.Count;

                    for (int i = 0; i < numCmts - 1; i++)
                    {
                        DateTime currentCommitDate = Convert.ToDateTime(cmtList[i].StartTime);                      //get datetime of commitment we are on in loop
                        DateTime nextCommitDate = Convert.ToDateTime(cmtList[i + 1].StartTime);                     //get datetime of commitment ahead of it

                        //if the two commits are distinct besides time and current commit is within week of start time
                        if (!sameCategory(cmtList[i], cmtList[i + 1]) || currentCommitDate.AddMinutes(15) != nextCommitDate)
                        {
                            endTime = endTime = Convert.ToDateTime(cmtList[i].StartTime).AddMinutes(15).ToString();                                               //update endtime and add what we have so far
                            addToAppointments(initialCommit, startTime, endTime);                                   //add the chunk of time to our listviews

                            //initialize carries to be the next commitment and begin scanning for that
                            startTime = Convert.ToDateTime(cmtList[i + 1].StartTime).ToString();
                            endTime = Convert.ToDateTime(cmtList[i + 1].StartTime).AddMinutes(15).ToString();
                            initialCommit = cmtList[i + 1];
                        }

                    }
                    endTime = Convert.ToDateTime(cmtList[cmtList.Count() - 1].StartTime).AddMinutes(15).ToString();
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
            else
            {
                lvOpen.Items.Add(new ListViewItem(new string[] { startTime, endTime, commit.Class, commit.Location, 
                    commit.Open.ToString(), commit.Tutoring.ToString(), commit.Weekly.ToString(), partner, commit.ID.ToString() }));
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

        private bool sameCategory(TutorMaster.Commitment commitFirst, TutorMaster.Commitment commitSecond)      //ask if the 15 minute time block of the first has the same values as the second
        {
            return (commitFirst.Class == commitSecond.Class && commitFirst.Location == commitSecond.Location
                    && commitFirst.Open == commitSecond.Open && commitFirst.Weekly == commitSecond.Weekly
                    && commitFirst.ID == commitSecond.ID);
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

        private void btnAcceptAddLoc_Click(object sender, EventArgs e)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            List<string> commits = new List<string>();

            if (lvPendingTutor.CheckedItems.Count > 0)
            {
                for (int i = 0; i < lvPendingTutor.CheckedItems.Count; i++)
                {
                    commits.Add(lvPendingTutor.CheckedItems[i].SubItems[0].Text.ToString() + "," + lvPendingTutor.CheckedItems[i].SubItems[1].Text.ToString() + "," + Convert.ToString(lvPendingTutor.CheckedItems[i].SubItems[8].Text));
                }

                ProposeLocationForm g = new ProposeLocationForm(id, commits, true);
                g.Show();
            }
            else
            {
                List<Commitment> tuteeCmtList = (from stucmt in db.StudentCommitments
                                                 where stucmt.ID == id
                                                 join cmt in db.Commitments on stucmt.CmtID equals cmt.CmtID
                                                 select cmt).ToList();

                for (int i = 0; i < lvTutor.CheckedItems.Count; i++)
                {
                    DateTime startDate = getListViewTime(lvTutor.CheckedItems[i].SubItems[0].Text);
                    DateTime endDate = getListViewTime(lvTutor.CheckedItems[i].SubItems[1].Text);

                    for (int c = 0; c < tuteeCmtList.Count(); c++)
                    {
                        if (DateTime.Compare(startDate, Convert.ToDateTime(tuteeCmtList[c].StartTime)) <= 0 && DateTime.Compare(endDate, Convert.ToDateTime(tuteeCmtList[c].StartTime)) > 0)
                        {
                            tuteeCmtList[c].Location = tuteeCmtList[c].Location.Substring(0, tuteeCmtList[c].Location.Length - 1);
                            db.SaveChanges();
                        }
                    }

                    int partnerID = Convert.ToInt32(lvTutor.CheckedItems[i].SubItems[8].Text);

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
            }
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
            lvOpen.Items.Clear();

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
            lvOpen.Items.Clear();

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
            lvOpen.Items.Clear();

            loadAppointments(true);
        }

        private void btnFinalize_Click(object sender, EventArgs e)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();

            List<Commitment> tuteeCmtList = (from stucmt in db.StudentCommitments
                                             where stucmt.ID == id
                                             join cmt in db.Commitments on stucmt.CmtID equals cmt.CmtID
                                             select cmt).ToList();

            if (lvPendingTutee.CheckedItems.Count > 0)
            {
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
            }
            else
            {
                List<string> commits = new List<string>();
                for (int i = 0; i < lvTutee.CheckedItems.Count; i++)
                {
                    commits.Add(lvTutee.CheckedItems[i].SubItems[0].Text.ToString() + "," + lvTutee.CheckedItems[i].SubItems[1].Text.ToString() + "," + Convert.ToString(lvTutee.CheckedItems[i].SubItems[8].Text));
                }

                ProposeLocationForm g = new ProposeLocationForm(id, commits, true);
                g.Show();
            }

            lvFinalized.Items.Clear();
            lvPendingTutee.Items.Clear();
            lvPendingTutor.Items.Clear();
            lvTutee.Items.Clear();
            lvTutor.Items.Clear();
            lvOpen.Items.Clear();

            loadAppointments(false);
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAcceptAppointment_Click(object sender, EventArgs e)
        {

        }

        private void btnAddAvailability_Click(object sender, EventArgs e)
        {
            AddAvailability g = new AddAvailability(id);
            g.Show();
        }

        private void btnCreateAppointment_Click(object sender, EventArgs e)
        {

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

        private void lvOpen_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (lvOpen.CheckedItems.Count > 0)
            {
                btnAddAvailability.Enabled = false;
                btnCreateAppointment.Enabled = false;
                btnRemoveAvailability.Enabled = true;
            }
            else
            {
                btnAddAvailability.Enabled = true;
                btnCreateAppointment.Enabled = true;
                btnRemoveAvailability.Enabled = false;
            }
        }

        private void lvFinalized_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (lvFinalized.CheckedItems.Count > 0)
            {
                btnCancelFinalized.Enabled = true;
            }
            else
            {
                btnCancelFinalized.Enabled = false;
            }
        }

        private void lvPendingTutor_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            int numPendingTutor = lvPendingTutor.CheckedItems.Count;
            int numTutor = lvTutor.CheckedItems.Count;
            if (numPendingTutor > 0 && numTutor > 0)                                   //if both lv PendingTutor and lvTutor have something checked, only reject should be on
            {
                btnAcceptAddLoc.Enabled = false;
                btnRejectTutor.Enabled = true;
            }
            else if (numPendingTutor > 0 && numTutor == 0)                             //if PendingTutor has something checked by tutor doesn't then both buttons should be on
            {
                btnAcceptAddLoc.Enabled = true;
                btnRejectTutor.Enabled = true;
            }
            else if (numTutor > 0 && numPendingTutor == 0)                             //if tutor has something checked but pendingTutor does not, only reject should be on
            {
                btnAcceptAddLoc.Enabled = true;
                btnRejectTutor.Enabled = true;
            }
            else                                                                       //if neither of them have anything checked, then both buttons should be disabled
            {
                btnAcceptAddLoc.Enabled = false;
                btnRejectTutor.Enabled = false;
            }
        }

        private void lvTutor_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            int numPendingTutor = lvPendingTutor.CheckedItems.Count;
            int numTutor = lvTutor.CheckedItems.Count;
            if (numPendingTutor > 0 && numTutor > 0)                                   //if both lv PendingTutor and lvTutor have something checked, only reject should be on
            {
                btnAcceptAddLoc.Enabled = false;
                btnRejectTutor.Enabled = true;
            }
            else if (numPendingTutor > 0 && numTutor == 0)                             //if PendingTutor has something checked by tutor doesn't then both buttons should be on
            {
                btnAcceptAddLoc.Enabled = true;
                btnRejectTutor.Enabled = true;
            }
            else if (numTutor > 0 && numPendingTutor == 0)                             //if tutor has something checked but pendingTutor does not, only reject should be on
            {
                btnAcceptAddLoc.Enabled = true;
                btnRejectTutor.Enabled = true;
            }
            else                                                                       //if neither of them have anything checked, then both buttons should be disabled
            {
                btnAcceptAddLoc.Enabled = false;
                btnRejectTutor.Enabled = false;
            }
        }

        private void lvTutee_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            int numPendingTutee = lvPendingTutee.CheckedItems.Count;
            int numTutee = lvTutee.CheckedItems.Count;
            if (numPendingTutee > 0 && numTutee > 0)                                   //if both lv PendingTutee and lvTutee have something checked, only reject should be on
            {
                btnFinalize.Enabled = false;
                btnRejectTutee.Enabled = true;
            }
            else if (numPendingTutee > 0 && numTutee == 0)                             //if PendingTutee has something checked by tutee doesn't then both buttons should be on
            {
                btnFinalize.Enabled = true;
                btnRejectTutee.Enabled = true;
            }
            else if (numTutee > 0 && numPendingTutee == 0)                             //if tutee has something checked but pendingTutee does not, only reject should be on
            {
                btnFinalize.Enabled = true;
                btnRejectTutee.Enabled = true;
            }
            else                                                                       //if neither of them have anything checked, then both buttons should be disabled
            {
                btnFinalize.Enabled = false;
                btnRejectTutee.Enabled = false;
            }
        }

        private void lvPendingTutee_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            int numPendingTutee = lvPendingTutee.CheckedItems.Count;
            int numTutee = lvTutee.CheckedItems.Count;
            if (numPendingTutee > 0 && numTutee > 0)                                   //if both lv PendingTutee and lvTutee have something checked, only reject should be on
            {
                btnFinalize.Enabled = false;
                btnRejectTutee.Enabled = true;
            }
            else if (numPendingTutee > 0 && numTutee == 0)                             //if PendingTutee has something checked by tutee doesn't then both buttons should be on
            {
                btnFinalize.Enabled = true;
                btnRejectTutee.Enabled = true;
            }
            else if (numTutee > 0 && numPendingTutee == 0)                             //if tutee has something checked but pendingTutee does not, only reject should be on
            {
                btnFinalize.Enabled = true;
                btnRejectTutee.Enabled = true;
            }
            else                                                                       //if neither of them have anything checked, then both buttons should be disabled
            {
                btnFinalize.Enabled = false;
                btnRejectTutee.Enabled = false;
            }
        }

        private void btnRemoveAvailability_Click(object sender, EventArgs e)
        {
            string startTime = lvOpen.CheckedItems[0].SubItems[0].Text.ToString();
            string endTime = lvOpen.CheckedItems[0].SubItems[1].Text.ToString();
            string weekly = lvOpen.CheckedItems[0].SubItems[2].Text.ToString();
            
            List<string> removeList = new List<string>();
            removeList.Add(startTime + "," + endTime +","+weekly);
            
            RemoveAvailForm g = new RemoveAvailForm(id, removeList);
            g.Show();
        }

       
    }
}
