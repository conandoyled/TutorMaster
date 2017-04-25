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

            removeInvalidCourses();
            combHours.Text = "0";
            combMins.Text = "00";

        }

        private void removeInvalidCourses()
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            bool thisStudentATutor = (bool)(from row in db.Students where row.ID == id select row.Tutor).First();
            if (thisStudentATutor)
            {
                List<string> removeClasses = (from stuClass in db.StudentClasses.AsEnumerable()
                                              where stuClass.ID == id
                                              join course in db.Classes.AsEnumerable() on stuClass.ClassCode equals course.ClassCode
                                              select course.ClassName).ToList();

                for (int i = 0; i < combCourseName.Items.Count; i++)
                {
                    for (int j = 0; j < removeClasses.Count; j++)
                    {
                        if (removeClasses[j].ToString() == combCourseName.Items[i].ToString())
                        {
                            combCourseName.Items.Remove(combCourseName.Items[i]);
                        }
                    }
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            StudentMain f = new StudentMain(id);
            f.Show();
            this.Dispose();
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
            else if (((Convert.ToInt32(combHours.Text) * 4 + (Convert.ToInt32(combMins.Text) / 15)) == 0) ||
                ((Convert.ToInt32(combHours.Text) * 4 + (Convert.ToInt32(combMins.Text) / 15)) > 12))
            {
                MessageBox.Show("Please input values for the hours and minutes that are between a length of 15 minutes and 3 hours");
            }
            else
            {
                bool weekly = cbxWeekly.Checked;
                DateTime start = DateTime.Now;
                TutorMasterDBEntities4 db = new TutorMasterDBEntities4();

                string classCode = (from row in db.Classes where combCourseName.Text == row.ClassName select row.ClassCode).First();

                var approvedTutorIds = (from row in db.StudentClasses.AsEnumerable() where classCode == row.ClassCode select row.ID).ToList();
                if (approvedTutorIds.Count() == 0)
                {
                    MessageBox.Show("There are currently no tutors approved to tutor this course. Sorry.");
                }
                else
                {

                    List<Commitment> tuteeCommits = (from stucmt in db.StudentCommitments.AsEnumerable()
                                                     where stucmt.ID == id
                                                     join cmt in db.Commitments.AsEnumerable() on stucmt.CmtID equals cmt.CmtID
                                                     select cmt).ToList();


                    int sessionLength = Convert.ToInt32(combHours.Text) * 4 + (Convert.ToInt32(combMins.Text) / 15);

                    SortsAndSearches.QuickSort(ref tuteeCommits, tuteeCommits.Count());
                    
                    checkMax(ref tuteeCommits);

                    removeNotOpens(ref tuteeCommits, start, weekly);

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

                                SortsAndSearches.QuickSort(ref tutorCommits, tutorCommits.Count());

                                checkMax(ref tutorCommits);

                                removeNotOpens(ref tutorCommits, start, weekly);
                                
                                
                                //MessageBox.Show(tuteeCommits.Count().ToString());
                                List<string> tutorValidSlots = getValidSlots(ref tutorCommits, sessionLength);

                                for (int j = 0; j < tutorValidSlots.Count(); j++)
                                {
                                    if (SortsAndSearches.BinarySearch(tuteeValidSlots, tutorValidSlots[j]))
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
                    this.Dispose();
                }
            }
        }

        private void removeNotOpens(ref List<TutorMaster.Commitment> cmtList, DateTime start, bool weekly)
        {
            if (weekly)
            {
                for (int i = 0; i < cmtList.Count() - 1; i++)
                {
                    if (!Commits.isOpen(cmtList[i]) || DateTime.Compare(start, Convert.ToDateTime(cmtList[i].StartTime)) > 0 || cmtList[i].Weekly != weekly)
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
                    if (!Commits.isOpen(cmtList[i]) || DateTime.Compare(start, Convert.ToDateTime(cmtList[i].StartTime)) > 0)
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
            
            for (int i = 0; i < cmtList.Count()-1; i++)
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
                if (DateTime.Compare(currentCommit.AddMinutes(15), nextCommit) == 0 && Commits.sameCategory(cmtList[i], cmtList[i + 1]) && !Commits.isOpen(cmtList[i]))
                {
                    consec++;
                }
                else
                {
                    consec = 1;
                }
            }
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
                for (int i = 0; i < cmtList.Count() - 1; i++)
                {
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
                    }
                    else if (DateTime.Compare(nextCommitDate, currentCommitDate.AddMinutes(15)) == 0 && consecutiveCommits >= sessionLength)
                    {
                        endDate = Convert.ToDateTime(cmtList[i].StartTime).AddMinutes(15);
                        validSlots.Add(startDate.ToString() + "," + endDate.ToString());
                        startDate = startDate.AddMinutes(15);
                    }
                    else if(DateTime.Compare(nextCommitDate, currentCommitDate.AddMinutes(15)) != 0 && consecutiveCommits >= sessionLength)
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
        
        private void addCommits(string timeSlot, int tutorId, int tuteeId, List<TutorMaster.Commitment> tutorCommits, List<TutorMaster.Commitment> tuteeCommits, string classCode, TutorMasterDBEntities4 db, bool weekly, int numSessions)
        {
            //TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            DateTime startTime = DateTimeMethods.getStartTime(timeSlot);
            DateTime endTime = DateTimeMethods.getEndTime(timeSlot);
            DateTime saveFirst = startTime;
            DateTime saveEnd = endTime;


            if (!weekly)
            {
                for (int j = 0; j < tuteeCommits.Count(); j++)
                {
                    if (DateTime.Compare(startTime, Convert.ToDateTime(tuteeCommits[j].StartTime)) <= 0 && DateTime.Compare(endTime, Convert.ToDateTime(tuteeCommits[j].StartTime)) > 0)
                    {
                        tuteeCommits[j].Open = false;
                        tuteeCommits[j].Tutoring = false;
                        tuteeCommits[j].ID = tutorId;
                        tuteeCommits[j].Class = classCode+"!";
                        tuteeCommits[j].Weekly = false;
                        db.SaveChanges();
                    }
                    else if (DateTime.Compare(endTime, Convert.ToDateTime(tuteeCommits[j].StartTime)) <= 0)
                    {
                        break;
                    }
                }

                for (int i = 0; i < tutorCommits.Count(); i++)
                {
                    if (DateTime.Compare(startTime, Convert.ToDateTime(tutorCommits[i].StartTime)) <= 0 && DateTime.Compare(endTime, Convert.ToDateTime(tutorCommits[i].StartTime)) > 0)
                    {
                        tutorCommits[i].Open = false;
                        tutorCommits[i].Tutoring = true;
                        tutorCommits[i].ID = tuteeId;
                        tutorCommits[i].Class = classCode+"!";
                        tutorCommits[i].Weekly = false;
                        db.SaveChanges();
                    }
                    else if (DateTime.Compare(endTime, Convert.ToDateTime(tutorCommits[i].StartTime)) <= 0)
                    {
                        break;
                    }
                }
            }
            else
            {
                for (int j = 0; j < tuteeCommits.Count(); j++)
                {
                    if (DateTime.Compare(startTime, Convert.ToDateTime(tuteeCommits[j].StartTime)) <= 0 && DateTime.Compare(endTime, Convert.ToDateTime(tuteeCommits[j].StartTime)) > 0)
                    {
                        if (!tuteeCommits[j].Class.ToString().Contains('!'))
                        {
                            tuteeCommits[j].Open = false;
                            tuteeCommits[j].Tutoring = false;
                            tuteeCommits[j].ID = tutorId;
                            tuteeCommits[j].Class = classCode + "!";
                            db.SaveChanges();
                        }
                    }
                    else if (DateTime.Compare(endTime, Convert.ToDateTime(tuteeCommits[j].StartTime)) <= 0)
                    {
                        startTime = startTime.AddDays(7);
                        endTime = endTime.AddDays(7);
                        j--;
                    }
                }
                startTime = saveFirst;
                endTime = saveEnd;
                for (int i = 0; i < tutorCommits.Count(); i++)
                {
                    if (DateTime.Compare(startTime, Convert.ToDateTime(tutorCommits[i].StartTime)) <= 0 && DateTime.Compare(endTime, Convert.ToDateTime(tutorCommits[i].StartTime)) > 0)
                    {
                        if (!tutorCommits[i].Class.ToString().Contains('!'))
                        {
                            tutorCommits[i].Open = false;
                            tutorCommits[i].Tutoring = true;
                            tutorCommits[i].ID = tuteeId;
                            tutorCommits[i].Class = classCode + "!";
                            db.SaveChanges();
                        }
                        
                    }
                    else if (DateTime.Compare(endTime, Convert.ToDateTime(tutorCommits[i].StartTime)) <= 0)
                    {
                        startTime = startTime.AddDays(7);
                        endTime = endTime.AddDays(7);
                        i--;
                    }
                }
            }
        }

        private void RequestForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Login g = new Login();
            g.Show();
            this.Dispose();
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