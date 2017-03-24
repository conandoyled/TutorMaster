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
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            StudentMain f = new StudentMain(id);
            f.Show();
            this.Close();
        }

        private void RequestForm_Load(object sender, EventArgs e)
        {

        }

        private int getDayIndex(string day) //This helper function is used to index days for date time object purposes
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

        private void btnAddOpenBlock_Click(object sender, EventArgs e)
        {
            //first, error check to make sure that the user put something for each dropdownbox
            if ((string.IsNullOrWhiteSpace(combStartDay.Text)) || (string.IsNullOrWhiteSpace(combStartHour.Text))
            || (string.IsNullOrWhiteSpace(combStartMinute.Text) || (string.IsNullOrWhiteSpace(combStartAmPm.Text)))
            || (string.IsNullOrWhiteSpace(combEndDay.Text)) || (string.IsNullOrWhiteSpace(combEndHour.Text))
            || (string.IsNullOrWhiteSpace(combEndMinute.Text)) || (string.IsNullOrWhiteSpace(combEndAmPm.Text)))
            {
                MessageBox.Show("Please fill out a starting and ending day, hour, minute, and part of day"); // give a popup if they don't enter a valid time
            }

            else //If the time the enter is valid then,
            {
                //1. pull the information for the start date time object
                string stringStartDay = combStartDay.Text;
                int intStartDay = getDayIndex(stringStartDay);
                int startHour = int.Parse(combStartHour.Text);
                int startMinute = int.Parse(combStartMinute.Text);
                string startAmPm = combStartAmPm.Text;

                if (startAmPm == "PM")
                {
                    startHour += 12;
                }

                //2. Pull the information for the end date time object
                string stringEndDay = combEndDay.Text;
                int intEndDay = getDayIndex(stringEndDay);
                int endHour = int.Parse(combEndHour.Text);
                int endMinute = int.Parse(combEndMinute.Text);
                string endAmPm = combEndAmPm.Text;

                if (endAmPm == "PM")
                {
                    endHour += 12;
                }

                //check if it is weekly
                bool weekly = cbxWeekly.Checked;

                //create the date time objects
                DateTime startTime = new DateTime(2017, 1, intStartDay, startHour, startMinute, 0);
                DateTime endTime = new DateTime(2017, 1, intEndDay, endHour, endMinute, 0);
            }
        }

        private void label3_Click(object sender, EventArgs e)
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