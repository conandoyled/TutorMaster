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
    public partial class ProposeLocationForm : Form
    {
        private int id;                                                //id of tutor
        private List<string> info = new List<string>();                //list of times the user wants to propose a location for
        private bool admin;

        //constructor
        public ProposeLocationForm(int accID, List<string> infoString, bool isAdmin)
        {
            InitializeComponent();
            id = accID;
            info = infoString;
            admin = isAdmin;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (!admin)
            {
                StudentMain g = new StudentMain(id);
                g.Show();
                this.Dispose();
            }
            else
            {
                this.Dispose();
            }
        }

        private void setTutorLocations()
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();                                                //Connect to the database
            string loc = txtLoc.Text;                                                                                //grab the text of the location the tutor typed in
            List<Commitment> tutorCmtList = (from stucmt in db.StudentCommitments                                    //get the tutor's commitments
                                             where stucmt.ID == id
                                             join cmt in db.Commitments on stucmt.CmtID equals cmt.CmtID
                                             select cmt).ToList();

            for (int i = 0; i < info.Count(); i++)                                                                   //for every time chosen
            {
                DateTime startDate = DateTimeMethods.getStartTime(info[i]);                                                          //get the startTime
                DateTime endDate = DateTimeMethods.getEndTime(info[i]);                                                              //get the endTime
                for (int c = 0; c < tutorCmtList.Count(); c++)                                                       //go through the tutor commitments
                {
                    if (DateTimeMethods.inTheTimeSlot(startDate, endDate, tutorCmtList[c]))                                          //if a commitment is in the time slot
                    {
                        if (!admin)
                        {
                            tutorCmtList[c].Location = loc + "?";                                                        //change the location to the string the user typed in + a question mark
                        }
                        else
                        {
                            tutorCmtList[c].Location = loc;                                                             //change the location to the string the user typed in
                        }
                        db.SaveChanges();                                                                            //save to database
                    }
                }
            }
        }

        private void setTuteeLocations()
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();                                          //Connect to the database  
            string loc = txtLoc.Text;                                                                          //grab the text of the location the tutor typed in
            for (int i = 0; i < info.Count(); i++)                                                             //for every time chosen
            {
                DateTime startDate = DateTimeMethods.getStartTime(info[i]);                                                    //get the startTime
                DateTime endDate = DateTimeMethods.getEndTime(info[i]);                                                        //get the endTime

                int partnerID = Convert.ToInt32(info[i].Split(',')[2]);

                List<Commitment> tuteeCmtList = (from stucmt in db.StudentCommitments                          //get the tutee's commitments
                                                 where stucmt.ID == partnerID
                                                 join cmt in db.Commitments on stucmt.CmtID equals cmt.CmtID
                                                 select cmt).ToList();
                //if a commitment is in the time slot
                for (int m = 0; m < tuteeCmtList.Count(); m++)                                                 //go through the tutee commitments
                {
                    if (DateTimeMethods.inTheTimeSlot(startDate, endDate, tuteeCmtList[m]))
                    {
                        tuteeCmtList[m].Class = tuteeCmtList[m].Class + "!";                                   //add an exclamation point to the tutee class so that the system knows this is a new commitment
                        if (!admin)
                        {
                            tuteeCmtList[m].Location = loc + "?";                                                  //change the location to the string the user typed in + a question mark
                        }
                        else
                        {
                            tuteeCmtList[m].Location = loc;
                        }
                        db.SaveChanges();                                                                      //save to database
                    }
                }
            }
        }

        private void btnSumbit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLoc.Text))                                                                 //make sure the user has put in a location
            {
                MessageBox.Show("Please write a location you would like to propose for each of the selected sites");    //if not, ask them to
            }
            else if (txtLoc.Text.Contains('?'))                                                                         //make sure they didn't put a question mark
            {
                MessageBox.Show("Please do not put in any question marks into your proposed location");
            }
            else
            {
                setTutorLocations();                                                                                    //set the tutor commitment locations where necessary
                setTuteeLocations();                                                                                    //set the tutee commitment locations where necessary
                if (!admin)
                {
                    MessageBox.Show("The location has been recorded. The appointment(s) must be accepted by the tutee before being finalized");
                    StudentMain g = new StudentMain(id);                                                                    //send the student back to student main
                    g.Show();
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("The location has been set and the appointment(s) have been finalized in both student's schedules");
                    this.Dispose();
                }
            }
        }

        private void ProposeLocationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Login g = new Login();
            g.Show();
            this.Dispose();
        }
    }
}
