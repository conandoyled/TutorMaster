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

        public ProposeLocationForm(int accID)
        {
            InitializeComponent();
            id = accID;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void btnSumbit_Click(object sender, EventArgs e)
        {

        }
    }
}
