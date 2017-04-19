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
    public partial class AdvancedRequest : Form
    {
        private int ACCID;
        public AdvancedRequest(int accID)  //Get rid of all the references to tvClasses and set up the new comboboxjust like the other one
        {
            ACCID = accID;
            InitializeComponent();
            //hide everything so you can show it when appropriate
            lblAvailableTimes.Hide();
            lblClasses.Hide();
            lblClassesAvailable.Hide();
            lblTutorName.Hide();
            label1.Hide();
            combClassBoxRight.Hide();
            combTutorNameLeft.Hide();
            combTutorNameRight.Hide();
            lvTutorAvailability.Hide();
            lblHowLong.Hide();
            combMeetingLength.Hide();
            cbxWeekly.Hide();
            label2.Hide();
            btnManualTime.Hide();
            btnSendRequest.Hide();
            combClassBoxLeft.Hide();

            //initialize the tutor names and list of classes
            setupTutorList();           //populate combTutorName
            setupComboClasses(combClassBoxRight); //populate the right combo box with all the available classes

        }

        private void setupTutorList()
        {
            //1. pull all of the students that are available as tutors into a list
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();

            //1.1 Identify all the student IDs and put them in a list
            var T = (from row in db.Students where row.Tutor == true select row.ID);
            List<int> TutorID = new List<int>();
            TutorID = T.ToList<int>();
            
            //1.2 find all the tutors using the ids and put them in a list
            List<string> Tutors = new List<string>(); 
            foreach (User usrr in db.Users)
            {
                foreach (int i in TutorID)
                {
                    if (usrr.ID == i)
                        Tutors.Add(usrr.FirstName + ' ' + usrr.LastName);
                }
            }
   

            //2. set them up in the combo box 

            foreach (string name in Tutors)
            {
                combTutorNameLeft.Items.Add(name);
            }
        }
       
        private void setupComboClasses(ComboBox combClassBox)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            var Classes = (from row in db.StudentClasses select row); // pull out all the classes that are being tutored [including duplicates]
            List<StudentClass> ListOfClasses = Classes.ToList<StudentClass>(); //put all of those classes into a list to manipuate
            foreach (StudentClass SC in ListOfClasses)
            {
                combClassBox.Items.Add(SC.ClassCode);
            }
        }

        private void combTutorNameLeft_SelectedIndexChanged(object sender, EventArgs e)
        {
            //show the appropriate objercts 
            combClassBoxLeft.Show();
            lblClassesAvailable.Show();
            combMeetingLength.Show();
            lblHowLong.Show();
            cbxWeekly.Show(); 

            combClassBoxLeft.Items.Clear();         //clears the class box
            setupComboClasses(combClassBoxLeft);    //fills it with the appropriate classes
            MatchTimes(); //set up the tutor time matches
        }

        private void btnExit_Click(object sender, EventArgs e) 
        {
            StudentMain g = new StudentMain(ACCID);
            g.Show();
            this.Close();
        }

        private void btnByTutor_Click(object sender, EventArgs e)
        {
            //hide the right side of the form
            combClassBoxRight.Hide();
            combTutorNameRight.Hide();
            lblClasses.Hide();
            label1.Hide();
            label2.Hide();
            btnManualTime.Hide();
            btnSendRequest.Hide();
            combMeetingLength.Hide();
            lblHowLong.Hide();
            cbxWeekly.Hide();

            //show lblTutorName and combTutorName
            lblTutorName.Show();
            combTutorNameLeft.Show();
        }

        private void btnByClass_Click(object sender, EventArgs e)
        {
            //Hide left side of form
            lblTutorName.Hide();
            lblClassesAvailable.Hide();
            lblAvailableTimes.Hide();
            combTutorNameLeft.Hide();
            lvTutorAvailability.Hide();
            label2.Hide();
            btnManualTime.Hide();
            btnSendRequest.Hide();
            combClassBoxLeft.Hide();
            lblClassesAvailable.Hide();
            combMeetingLength.Hide();
            lblHowLong.Hide();
            cbxWeekly.Hide();

            //Show Class Options
            lblClasses.Show();
            combClassBoxRight.Show();
        }

        private void combClassBoxRight_SelectedIndexChanged(object sender, EventArgs e)
        {
            //1. Show and clear all the appropriate items
            label1.Show();
            combTutorNameRight.Show();
            combTutorNameRight.Items.Clear();
            combMeetingLength.Show();
            lblHowLong.Show();
            cbxWeekly.Show();

            //This populates the available tutors for the selected class
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();

            //Create a list of all the Users that tutor the class that was selected
            List<User> ListOfTutors =
                (from row in db.StudentClasses
                 where row.ClassCode == combClassBoxRight.Text
                 join usr in db.Users 
                 on row.ID equals usr.ID
                 select usr).ToList<User>();

            //display them in the combo box
            foreach(User usr in ListOfTutors) 
            {
                combTutorNameRight.Items.Add(usr.FirstName+' '+usr.LastName);
            }
        }

        private void combTutorNameRight_SelectedIndexChanged(object sender, EventArgs e)
        {
            //call our tutor match function
        }

        private void MatchTimes(ComboBox TutBox, ComboBox ClsBox)
        {
            //do the matching thing that myles and I talked about
            //try to meet with him tomorrow and talk to him about it all

            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            //0. Info needed to match

            //a. Name of Tutor, use it to find their ID
            string[] Tutorname = TutBox.Text.Split();
            int TutID = (from row in db.Users where row.FirstName == Tutorname[0] && row.LastName == Tutorname[1] select row.ID).First();

            //b. Length of tutoring session in minutes
            int lenght = int.Parse(combMeetingLength.Text);

            //c. whether it is weekly
            bool weekly = cbxWeekly.Checked;

            //d. tutee ID (ACCID), use to match the availiblity
            int TuteeID = ACCID;

            //1. Pull out the commitments, sort and clump them

            //1.1 Pull out the Tutor commits

            //a. sort
            //b. clump
            //1.2 Pull out the tutee stuff
            //a. sort
            //b. clump

            //2. Match up the clumps ++ maybe do at the same time as 4?

            //3. Display them in the list view


            DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

            string classCode = ClsBox.Text;

            List<Commitment> tuteeCommits = (from stucmt in db.StudentCommitments.AsEnumerable()    // create a list of tutee's commitments
                                                where stucmt.ID == TuteeID
                                                join cmt in db.Commitments.AsEnumerable() on stucmt.CmtID equals cmt.CmtID
                                                select cmt).ToList();


            //int sessionLength = Convert.ToInt32(combHours.Text) * 4 + (Convert.ToInt32(combMins.Text) / 15);

            QuickSort(ref tuteeCommits, tuteeCommits.Count()); //sort the tutee commits so that they are in chronological order

            checkMax(ref tuteeCommits);

            removeNotOpens(ref tuteeCommits, start, weekly); //remove all the things that are not 

            if (tuteeCommits.Count == 0)
            {
                MessageBox.Show("You currently have no available slots, please add some availability before attempting to schedule a session of this length");
            }
            else
            {
                List<string> tuteeValidSlots = getValidSlots(ref tuteeCommits, sessionLength);

                bool done = false;
                for (int i = 0; i < approvedTutorIds.Count(); i++)
                {

                    if (approvedTutorIds[i] != id)
                    {
                        //var tutor = (from row in db.Users.AsEnumerable() where row.ID == approvedTutorIds[i] select row).First();
                        var tutorFirstName = (from row in db.Users.AsEnumerable() where row.ID == approvedTutorIds[i] select row.FirstName).First();
                        var tutorLastName = (from row in db.Users.AsEnumerable() where row.ID == approvedTutorIds[i] select row.LastName).First();

                        List<TutorMaster.Commitment> tutorCommits = (from stucmt in db.StudentCommitments.AsEnumerable()
                                                                        where stucmt.ID == approvedTutorIds[i]
                                                                        join cmt in db.Commitments.AsEnumerable() on stucmt.CmtID equals cmt.CmtID
                                                                        select cmt).ToList();

                        QuickSort(ref tutorCommits, tutorCommits.Count());

                        checkMax(ref tutorCommits);

                        removeNotOpens(ref tutorCommits, start, weekly);


                        //MessageBox.Show(tuteeCommits.Count().ToString());
                        List<string> tutorValidSlots = getValidSlots(ref tutorCommits, sessionLength);

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
                                    addCommits(tutorValidSlots[j], tutorId, tuteeId, tutorCommits, tuteeCommits, classCode, db, weekly, sessionLength);
                                    done = true;
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
                if (!done)
                {
                    MessageBox.Show("There are no more tutors that meet your request requirements.");
                }
            }
            StudentMain g = new StudentMain(id);
            g.Show();
            this.Close();
        }

        private bool isOpen(TutorMaster.Commitment commit)
        {
            return (commit.Class == "-" && commit.Location == "-" && commit.Open == true && commit.Tutoring == false && commit.ID == -1);
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

        private void AdvancedRequest_Load(object sender, EventArgs e)
        {

        }

        private void combClassBoxLeft_SelectedIndexChanged(object sender, EventArgs e)
        {

        }





        private bool sameCategory(TutorMaster.Commitment commitFirst, TutorMaster.Commitment commitSecond)      //ask if the 15 minute time block of the first has the same values as the second
        {
            return (commitFirst.Class == commitSecond.Class && commitFirst.Location == commitSecond.Location
                    && commitFirst.Open == commitSecond.Open && commitFirst.Weekly == commitSecond.Weekly
                    && commitFirst.ID == commitSecond.ID);
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
                    MessageBox.Show(cmtList[i + 1].StartTime.ToString());
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
