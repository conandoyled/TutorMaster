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
    public partial class AdminCreateAppointmentForm : Form
    {
        private int id;
        private bool tutoring;

        public AdminCreateAppointmentForm(int accID, bool tutoringP)
        {
            InitializeComponent();
            id = accID;
            tutoring = tutoringP;
            if (tutoring)
            {
                loadTutorOptions();
            }
            else
            {
                loadTuteeOptions();
            }
        }

        private void loadTutorOptions()
        {
            loadTutorClassesCheckBox();
            loadStudentCheckBox();

        }

        private void loadTutorClassesCheckBox()
        {
            
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();

            List<string> approvedClasses = (from stuClass in db.StudentClasses
                                            where stuClass.ID == id
                                            join appClass in db.Classes on stuClass.ClassCode equals appClass.ClassCode
                                            select appClass.ClassName).ToList();


            for (int i = 0; i < approvedClasses.Count; i++)
            {
                cbxClasses.Items.Add(approvedClasses[i]);
            }
        }

        private void loadStudentCheckBox()
        {
            //List<string> tutees = (from 
        }

        private void loadTuteeOptions()
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();

            //List<string> classes = (from row
        }
    }
}
