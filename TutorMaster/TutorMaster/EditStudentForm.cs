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
    public partial class EditStudentForm : Form
    {
        public EditStudentForm(int accID)
        {
            InitializeComponent();
            TutorMasterDBEntities2 db = new TutorMasterDBEntities2();
            txtFirstname.Text = (from row in db.Users where row.ID == accID select row.FirstName).First();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
