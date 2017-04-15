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
    public partial class AdvancedRequest : Form
    {
        private int ACCID;
        public AdvancedRequest(int accID)  //change this to initialize the tutor and class box once and then leave it alone so that we don't double fill it and don't have to constantly clear and refill it
        {
            ACCID = accID;
            InitializeComponent();
            //hide everything so you can show it when appropriate
            lblAvailableTimes.Hide();
            lblClasses.Hide();
            lblClassesAvailable.Hide();
            lblTutorName.Hide();
            label1.Hide();
            label2.Hide();
            combClassBox.Hide();
            combTutorName.Hide();
            combTutorName2.Hide();
            lvTutorAvailability.Hide();
            lvTutorAvailability2.Hide();
            tvClasses.Hide();
        }

        private void setupTutorList()
        {
            //1. pull all of the students that are available as tutors into a list
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();

            //1.1 Identify all the student IDs and put them in a list
            var T = (from row in db.Students where row.Tutor == true select row.ID);
            List<int> TutorID = new List<int>();
            TutorID = T.ToList<int>();
            
            //1.2 find all the tutors using the ids and put them in a list
            List<string> Tutors = new List<string>(); 
            foreach (User usrr in db.Users)
            {
                foreach (int i in TutorID)
                {
                    if (usrr.ID == i)
                        Tutors.Add(usrr.FirstName + ' ' + usrr.LastName);
                }
            }
   

            //2. set them up in the combo box 

            foreach (string name in Tutors)
            {
                combTutorName.Items.Add(name);
            }
        }

        private void setupTreeViewClasses(string TutorName)
        {
            tvClasses.CheckBoxes = true;

            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            string[] TName = TutorName.Split(' '); //split the name into 2 pieces
            string firstname= TName[0]; //get the first name
            string lastname=TName[1]; //get the last name
            
            //1. find the tutors ID
            int id = (from row in db.Users where ((firstname == row.FirstName) && (lastname == row.LastName)) select row.ID).First();

            //2. make a list of the classes they can tutor, turn those into class objects with join, then use them for the presentation set up

            /*var classes = (from c in db.StudentClasses where id == c.ID select c);
            List<StudentClass> cls = new List<StudentClass>();
            cls = classes.ToList();

            //2.5 turn the list of student classes into a list of Class objects so you can present them
            List<Class> cls2 = new List<Class>();
            //try using the join clause here. you may have to redo some of the code getting the list before to make this work

            foreach (Class c in db.Classes)
            {
                foreach (StudentClass s in cls)
                {
                    if (s.ClassCode == c.ClassCode)
                        cls2.Add(c);
                }
            }
            */
            List<Class> listofclasses =
                (from row in db.StudentClasses
                 where row.ID == id
                 join cl in db.Classes 
                 on row.ClassCode equals cl.ClassCode
                 select cl).ToList<Class>();

            //3. upload those classes to the display

            foreach (Class cl in listofclasses)
            {
                if (tvClasses.Nodes.ContainsKey(cl.Department))
                {
                    tvClasses.Nodes[cl.Department].Nodes.Add(new TreeNode(cl.ClassName));
                }
                else
                {
                    TreeNode nNode = new TreeNode(cl.Department);
                    nNode.Name = cl.Department;
                    nNode.Nodes.Add(cl.ClassName);
                    tvClasses.Nodes.Add(nNode);
                }
            }

            tvClasses.Sort();
        }

        private void setupComboClasses()
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            var Classes = (from row in db.StudentClasses select row); // pull out all the classes that are being tutored [including duplicates]
            List<StudentClass> ListOfClasses = Classes.ToList<StudentClass>(); //put all of those classes into a list to manipuate
            foreach (StudentClass SC in ListOfClasses)
            {
                combClassBox.Items.Add(SC.ClassCode);
            }

        }

        private void combTutorName_SelectedIndexChanged(object sender, EventArgs e)
        {
            tvClasses.Show();
            tvClasses.Nodes.Clear(); //clears the class box
            setupTreeViewClasses(combTutorName.Text); //fills it with the appropriate classes
            //set up the tutor time matches
        }

        private void btnExit_Click(object sender, EventArgs e) 
        {
            StudentMain g = new StudentMain(ACCID);
            g.Show();
            this.Close();
        }

        private void btnByTutor_Click(object sender, EventArgs e)
        {
            //hide the right side of the form
            combClassBox.Hide();
            lvTutorAvailability2.Hide();
            combTutorName2.Hide();
            lblClasses.Hide();
            label1.Hide();
            label2.Hide();

            //show lblTutorName and combTutorName
            lblTutorName.Show();
            combTutorName.Show();

            setupTutorList();           //populate combTutorName
        }

        private void btnByClass_Click(object sender, EventArgs e)
        {
            //Hide left side of form
            lblTutorName.Hide();
            lblClassesAvailable.Hide();
            lblAvailableTimes.Hide();
            combTutorName.Hide();
            tvClasses.Hide();
            lvTutorAvailability.Hide();

            //Show Class Options
            lblClasses.Show();
            combClassBox.Show();

            setupComboClasses(); //populate the combo box with all the available classes

        }
    }
}
