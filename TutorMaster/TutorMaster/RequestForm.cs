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
    }
}
