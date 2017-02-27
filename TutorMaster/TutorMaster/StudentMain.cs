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

                int endHour = int.Parse(combEndHour.Text);
                int endMinute = int.Parse(combEndMinute.Text);
                string endAmPm = combEndAmPm.Text;

                //MessageBox.Show(DateTime.Now.ToString("D"));
                DateTime startTime = new DateTime(2017, 1, intStartDay, startHour, startMinute, 0);
                DateTime endTime = new DateTime(2017, 1, intEndDay, endHour, endMinute, 0);
                getAvail(startTime, endTime);
                //MessageBox.Show(startTime.ToString());
            }
        }

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

        private void getAvail(DateTime startTime, DateTime endTime)
        {
            DateTime begin = startTime;
            DateTime fifteen = startTime;
            fifteen.AddMinutes(15);
            MessageBox.Show(begin.ToString());
            MessageBox.Show(fifteen.ToString());
            int compare = begin.CompareTo(endTime);
            string add = "";
            
            while (compare < 0) //if the first date is less than the second date
            {
                add += begin.ToString() + fifteen.ToString();
                lbxSunday.Items.Add(add);
                add = "";
                begin.AddMinutes(15);
                fifteen.AddMinutes(15);
                compare = begin.CompareTo(endTime);
                compare = 1;
            }
        }
    }
}
