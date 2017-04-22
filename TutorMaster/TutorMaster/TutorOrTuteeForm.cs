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
    public partial class TutorOrTuteeForm : Form
    {
        private int id;
        //constructor
        public TutorOrTuteeForm(int accID)
        {
            InitializeComponent();
            id = accID;
        }

        //if they choose to send the student to the next form as a tutor, say they are a tutor
        private void btnTutoring_Click(object sender, EventArgs e)
        {
            AdminCreateAppointmentForm g = new AdminCreateAppointmentForm(id, true);
            g.Show();
            this.Dispose();
        }

        //if they choose the being tutored option, say they are a tutee
        private void btnBeingTutored_Click(object sender, EventArgs e)
        {
            AdminCreateAppointmentForm g = new AdminCreateAppointmentForm(id, false);
            g.Show();
            this.Dispose();
        }

        private void TutorOrTuteeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
    }
}
