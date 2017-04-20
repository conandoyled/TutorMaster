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
        public TutorOrTuteeForm(int accID)
        {
            InitializeComponent();
            id = accID;
        }

        private void btnTutoring_Click(object sender, EventArgs e)
        {
            AdminCreateAppointmentForm g = new AdminCreateAppointmentForm(id, true);
            g.Show();
            this.Close();
        }

        private void btnBeingTutored_Click(object sender, EventArgs e)
        {
            AdminCreateAppointmentForm g = new AdminCreateAppointmentForm(id, false);
            g.Show();
            this.Close();
        }
    }
}
