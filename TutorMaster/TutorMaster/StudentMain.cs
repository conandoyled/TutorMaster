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
            string starthour = combStartHour.Text;
            string startminute = combStartMinute.Text;
            string startampm = combStartAmPm.Text;
            string endhour = combEndHour.Text;
            string endminute = combEndMinute.Text;
            string endampm = combEndAmPm.Text;
            if(string.IsNullOrWhiteSpace(starthour))
            {
                MessageBox.Show("Please fill out a starting and ending hour, minute, and part of day");
            }
        }
    }
}
