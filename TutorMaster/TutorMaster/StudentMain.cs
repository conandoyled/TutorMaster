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

        public StudentMain(int accID)
        {
            id = accID;
            InitializeComponent();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Login g = new Login();
            g.Show();
            this.Close();
        }

        private void btnAddOpenBlock_Click(object sender, EventArgs e)
        {
            //first, error check to make sure that the user put something for each dropdownbox
            if ((string.IsNullOrWhiteSpace(combStartDay.Text)) || (string.IsNullOrWhiteSpace(combStartHour.Text))
            || (string.IsNullOrWhiteSpace(combStartMinute.Text) || (string.IsNullOrWhiteSpace(combStartAmPm.Text)))
            || (string.IsNullOrWhiteSpace(combEndDay.Text)) || (string.IsNullOrWhiteSpace(combEndHour.Text))
            || (string.IsNullOrWhiteSpace(combEndMinute.Text)) || (string.IsNullOrWhiteSpace(combEndAmPm.Text)))
            {
                MessageBox.Show("Please fill out a starting and ending day, hour, minute, and part of day");
            }
            else
            {
                string stringStartDay = combStartDay.Text;
                int intStartDay = getDayIndex(stringStartDay);

                int startHour = int.Parse(combStartHour.Text);
                int startMinute = int.Parse(combStartMinute.Text);
                string startAmPm = combStartAmPm.Text;


                string stringEndDay = combEndDay.Text;
                int intEndDay = getDayIndex(stringEndDay);

                int endhour = int.Parse(combEndHour.Text);
                int endminute = int.Parse(combEndMinute.Text);
                string endampm = combEndAmPm.Text;

                DateTime startTime = new DateTime(2010, 1, intStartDay, startHour, startMinute, 0);
                MessageBox.Show(startTime.ToString());
            }
        }

        private int getDayIndex(string day)
        {
            int numDay = 0;
            switch (day)
            {
                case "Sunday":
                    numDay = 0; 
                    break;
                case "Monday":
                    numDay = 1;
                    break;
                case "Tuesday":
                    numDay = 2;
                    break;
                case "Wednesday":
                    numDay = 3;
                    break;
                case "Thursday":
                    numDay = 4;
                    break;
                case "Friday":
                    numDay = 5;
                    break;
                case "Saturday":
                    numDay = 6;
                    break;
            }
            return numDay;
        }
    }
}
