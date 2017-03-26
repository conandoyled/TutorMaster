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
    public partial class DisplayMatchesForm : Form
    {
        private int tuteeID;
        List<int> tutorIDs;
        private DateTime startTime;
        private DateTime endTime;
        private string classCode;

        public DisplayMatchesForm(int id, List<int> studentIDs, DateTime start, DateTime end, string class1)
        {
            InitializeComponent();
            InitializeListView();
            btnSubmit.Enabled = false;
            tuteeID = id;
            startTime = start;
            endTime = end;
            classCode = class1;
            tutorIDs = studentIDs;
            LoadListView(studentIDs);
        }

        private void InitializeListView()
        {
            lvMatches.CheckBoxes = true;
            lvMatches.Columns.Add("First Name", 100);
            lvMatches.Columns.Add("Last Name", 100);
            
        }

        private void LoadListView(List<int> studentIDs)
        {
            if (studentIDs.Count() > 0)
            {
                TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
                string firstName = "";
                string lastName = "";

                for (int i = 0; i < studentIDs.Count(); i++)
                {
                    firstName = (from row in db.Users.AsEnumerable() where row.ID == studentIDs[i] select row.FirstName).First();
                    lastName = (from row in db.Users.AsEnumerable() where row.ID == studentIDs[i] select row.LastName).First();
                    lvMatches.Items.Add(new ListViewItem(new string[] { firstName, lastName }));
                }
                lvMatches.Invalidate();
            }
        }

        private void lvMatches_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            int itemsChecked = lvMatches.Items.Count;
            if (itemsChecked == 1)
            {
                btnSubmit.Enabled = true;
            }
            else
            {
                btnSubmit.Enabled = false;
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            TutorMasterDBEntities4 db = new TutorMasterDBEntities4();
            
            int tutorIndex = lvMatches.CheckedItems.IndexOf(lvMatches.CheckedItems[0]);
            int tutorID = tutorIndex;
            
            var tuteeStdCommits = (from row in db.StudentCommitments.AsEnumerable() where row.ID == tuteeID select row.CmtID).ToArray();
            var tutorStdCommits = (from row in db.StudentCommitments.AsEnumerable() where row.ID == tutorID select row.CmtID).ToArray();
            
            List<TutorMaster.Commitment> tuteeCommitList = new List<TutorMaster.Commitment>();
            List<TutorMaster.Commitment> tutorCommitList = new List<TutorMaster.Commitment>();

            for (int i = 0; i < tuteeStdCommits.Count(); i++)
            {
                var tuteeCommit = (from row in db.Commitments.AsEnumerable() where row.CmtID == tuteeStdCommits[i] select row).Single();
                tuteeCommitList.Add(tuteeCommit);
            }
            
            for(int n = 0; n < tutorStdCommits.Count(); n++)
            {
                var tutorCommit = (from row in db.Commitments.AsEnumerable() where row.CmtID == tutorStdCommits[n] select row).Single();
                tutorCommitList.Add(tutorCommit);
            }

            for (int f = 0; f < tuteeCommitList.Count(); f++)
            {
                if (inBetween(tuteeCommitList[f]))
                {
                    tuteeCommitList[f].Class = classCode;
                    tuteeCommitList[f].ID = tutorID;
                    tuteeCommitList[f].Open = false;
                    db.SaveChanges();
                }
            }

            for (int j = 0; j < tutorCommitList.Count(); j++)
            {
                if (inBetween(tutorCommitList[j]))
                {
                    tutorCommitList[j].Class = classCode;
                    tutorCommitList[j].ID = tuteeID;
                    tutorCommitList[j].Tutoring = true;
                    tutorCommitList[j].Open = false;
                    db.SaveChanges();
                }
            }
            StudentMain g = new StudentMain(tuteeID);
            g.Show();
            this.Close();
        }

        private bool inBetween(TutorMaster.Commitment commit)
        {
            return (DateTime.Compare(Convert.ToDateTime(commit.StartTime), startTime) >= 0 && DateTime.Compare(Convert.ToDateTime(commit.StartTime), endTime) < 0);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            StudentMain g = new StudentMain(tuteeID);
            g.Show();
            this.Close();
        }
    }
}
