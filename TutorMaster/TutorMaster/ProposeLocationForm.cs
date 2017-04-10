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
        private int id;
        private List<string> info = new List<string>();

        public ProposeLocationForm(int accID, List<string> infoString)
        {
            InitializeComponent();
            id = accID;
            info = infoString;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            StudentMain g = new StudentMain(id);
            g.Show();
            this.Close();
        }

        private void btnSumbit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLoc.Text))
            {
                MessageBox.Show("Please write a location you would like to propose for each of the selected sites");
            }
            else if(txtLoc.Text.Contains('?'))
            {
                MessageBox.Show("Please do not put in any question marks into your proposed location");
            }
            else
            {
                TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
                string loc = txtLoc.Text;
                List<Commitment> tutorCmtList = (from stucmt in db.StudentCommitments
                                                     where stucmt.ID == id
                                                     join cmt in db.Commitments on stucmt.CmtID equals cmt.CmtID
                                                     select cmt).ToList();
                
                for (int i = 0; i < info.Count(); i++)
                {
                    DateTime startDate = getStartTime(info[i]);
                    DateTime endDate = getEndTime(info[i]);
                    for (int c = 0; c < tutorCmtList.Count(); c++)
                    {
                        if ( DateTime.Compare(startDate, Convert.ToDateTime(tutorCmtList[c].StartTime)) <= 0 && DateTime.Compare(endDate, Convert.ToDateTime(tutorCmtList[c].StartTime)) > 0)
                        {
                            tutorCmtList[c].Location = loc + "?";
                            db.SaveChanges();
                        }
                    }
                    int partnerID = Convert.ToInt32(info[i].Split(',')[2]);

                    List<Commitment> tuteeCmtList = (from stucmt in db.StudentCommitments
                                                     where stucmt.ID == partnerID
                                                     join cmt in db.Commitments on stucmt.CmtID equals cmt.CmtID
                                                     select cmt).ToList();
                    for (int m = 0; m < tuteeCmtList.Count(); m++)
                    {
                        if (DateTime.Compare(startDate, Convert.ToDateTime(tuteeCmtList[m].StartTime)) <= 0 && DateTime.Compare(endDate, Convert.ToDateTime(tuteeCmtList[m].StartTime)) > 0)
                        {
                            tuteeCmtList[m].Location = loc + "?";
                            db.SaveChanges();
                        }
                    }
                }
                StudentMain g = new StudentMain(id);
                g.Show();
                this.Close();
            }
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
    }
}
