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

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            StudentMain f = new StudentMain(id);
            f.Show();
            this.Close();
        }


        private List<DateTime> MakeFifteenBlocks(DateTime startTime, DateTime endTime)
        {
            DateTime begin = startTime;
            int compare = begin.CompareTo(endTime);//returns -1 through postive 1 to represent the relation between the compared times 
            List<DateTime> RequestTimes = new List<DateTime>(); //create a list to store the broken up commitments.

            while (compare < 0) //if the first date is less than the second date
            {
                RequestTimes.Add(begin);
                begin = begin.AddMinutes(15);
                compare = begin.CompareTo(endTime);
            }
            return RequestTimes;
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            //1. create the time objects

            if ( (string.IsNullOrWhiteSpace(combStartHour.Text)) || (string.IsNullOrWhiteSpace(combStartMinute.Text) 
            || (string.IsNullOrWhiteSpace(combStartAmPm.Text)))  ||  (string.IsNullOrWhiteSpace(combEndHour.Text))
            || (string.IsNullOrWhiteSpace(combEndMinute.Text))   || (string.IsNullOrWhiteSpace(combEndAmPm.Text)))
            {
                MessageBox.Show("Please fill out a starting and ending day, hour, minute, and part of day"); // give a popup if they don't enter a valid time
            }

            else //If the time the enter is valid then,
            {
                //1. pull the information for the start date time object
                //string stringStartDay = combStartDay.Text;

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

                //1.5 Pull the information for the end date time object
                //string stringEndDay = combEndDay.Text;

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

                //check if it is weekly
                bool weekly = cbxWeekly.Checked;

                //create the date time objects
                DateTime startTime = new DateTime(dayStartDateTime.Value.Year, dayStartDateTime.Value.Month, dayStartDateTime.Value.Day, startHour, startMinute, 0);
                DateTime endTime = new DateTime(dayEndDateTime.Value.Year, dayEndDateTime.Value.Month, dayEndDateTime.Value.Day, endHour, endMinute, 0);

                //2. Break up the times into 15 minute blocks

                List<DateTime> RequestTimes = MakeFifteenBlocks(startTime, endTime); //this gives us the list that contains all the fifteen minute blocks to be compared

                //3. Match requests!

                //3.1 Check if there are any tutors for the class!
                TutorMasterDBEntities4 db = new TutorMasterDBEntities4(); //create new db entity to look at things
                string CC = (from row in db.Classes where row.ClassName == combCourseName.Text select row.ClassCode).First(); //pull out the classcode to compare
                List<int> IDs = new List<int>();
                foreach (StudentClass sc in db.StudentClasses)
                {
                    if (sc.ClassCode == CC)
                    {
                        IDs.Add(sc.ID); //add the ID of the tutor that teaches the class I'm looking for to the list
                    }
                }
                
            }
        }

        private void combCourseName_SelectedIndexChanged(object sender, EventArgs e)
        {

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