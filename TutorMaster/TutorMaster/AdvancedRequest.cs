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
        public AdvancedRequest(int accID)
        {
            ACCID = accID;
            InitializeComponent();
            setupTutorList();                   //intialize the list of tutors
            // this sets up the initial list with all the classes. I would change this to list all the classes that a specific tutor teaches after one is selected
            //set up the availability box with the tutors available times       
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

        private void setupClasses(string TutorName)
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

        private void combTutorName_SelectedIndexChanged(object sender, EventArgs e)
        {
            tvClasses.Nodes.Clear();
            setupClasses(combTutorName.Text);
        }

        private void btnExit_Click(object sender, EventArgs e) //This doesn't work still!!
        {
            StudentMain g = new StudentMain(ACCID);
            g.Show();
            this.Close();
        }
    }
}
