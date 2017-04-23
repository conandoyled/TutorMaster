using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TutorMaster
{
    class Commits
    {
        public static DateTime getStartTime(string slot)
        {
            string startDateTime = slot.Split(',')[0];
            string startDate = startDateTime.Split(' ')[0];
            string startTime = startDateTime.Split(' ')[1];
            string amPm = startDateTime.Split(' ')[2];

            int month = Convert.ToInt32(startDate.Split('/')[0]);
            int day = Convert.ToInt32(startDate.Split('/')[1]);
            int year = Convert.ToInt32(startDate.Split('/')[2]);

            int hour = Convert.ToInt32(startTime.Split(':')[0]);
            int min = Convert.ToInt32(startTime.Split(':')[1]);


            if (hour < 12 && amPm == "PM")
            {
                hour += 12;
            }
            else if (hour == 12 && amPm == "AM")
            {
                hour = 0;
            }
            DateTime date = new DateTime(year, month, day, hour, min, 0);
            return date;
        }

        public static DateTime getEndTime(string slot)
        {
            string startDateTime = slot.Split(',')[1];
            string startDate = startDateTime.Split(' ')[0];
            string startTime = startDateTime.Split(' ')[1];
            string amPm = startDateTime.Split(' ')[2];

            int month = Convert.ToInt32(startDate.Split('/')[0]);
            int day = Convert.ToInt32(startDate.Split('/')[1]);
            int year = Convert.ToInt32(startDate.Split('/')[2]);

            int hour = Convert.ToInt32(startTime.Split(':')[0]);
            int min = Convert.ToInt32(startTime.Split(':')[1]);


            if (hour < 12 && amPm == "PM")
            {
                hour += 12;
            }
            else if (hour == 12 && amPm == "AM")
            {
                hour = 0;
            }

            DateTime date = new DateTime(year, month, day, hour, min, 0);
            return date;
        }

        public static bool sameCategory(TutorMaster.Commitment commitFirst, TutorMaster.Commitment commitSecond)      //ask if the 15 minute time block of the first has the same values as the second
        {
            string class1;
            string class2;

            if (commitFirst.Class == "@")                                                                       //if there is an @ sign, treat it as if it is - so the cancelled time can go with its adjacent open times
            {
                class1 = "-";
            }
            else
            {
                class1 = commitFirst.Class;
            }

            if (commitSecond.Class == "@")                                                                     //do the same for the second commitment passed in here
            {
                class2 = "-";
            }
            else
            {
                class2 = commitSecond.Class;
            }


            return (class1 == class2 && commitFirst.Location == commitSecond.Location                           //check all of the information except for the dateTimes of both commitments
                    && commitFirst.Open == commitSecond.Open && commitFirst.Weekly == commitSecond.Weekly       //all of the information must be the same in order for them to be in the same category
                    && commitFirst.ID == commitSecond.ID);
        }

        public static bool openOrSameType(Commitment listCommit, Commitment midCommit)
        {
            return midCommit.Open == true || Commits.sameCategory(listCommit, midCommit);
        }

        public static string getNextEndTime(Commitment commit)
        {
            //this function gets the string version of the dateTime that is 15 minutes in the future of a given commitment
            return Convert.ToDateTime(commit.StartTime).AddMinutes(15).ToString();
        }

        public static void updateInformation(ref string start, ref string end, ref string today, ref Commitment oldCommit, Commitment newCommit)
        {
            //this function takes the information of our block's old start time, end time, day of the week, and old commitment and
            //updates each piece of information to the new commitments start time, end time, day of the week, and copies the new
            //commitment information into the old commitment data object. This signifies the program starting a new block to add to the listview
            start = getCommitTime(newCommit);
            end = getCommitTime15(newCommit);
            today = Convert.ToDateTime(newCommit.StartTime).DayOfWeek.ToString();
            oldCommit = newCommit;
        }

        public static string getCommitTime(TutorMaster.Commitment commit)                                             //get the c# datetime object of the commit's start time and cast it to a string
        {
            return Convert.ToDateTime(commit.StartTime).ToString().Split(' ')[1] + " " + Convert.ToDateTime(commit.StartTime).ToString().Split(' ')[2];
        }

        public static string getCommitTime15(TutorMaster.Commitment commit15)                                         //get the c# datetime object of the commit's start time 15 minutes in the future and cast it to a string
        {
            return Convert.ToDateTime(commit15.StartTime).AddMinutes(15).ToString().Split(' ')[1] + " " + Convert.ToDateTime(commit15.StartTime).ToString().Split(' ')[2];
        }

        public static bool nextCommitAdjacent(DateTime currentCommitDate, DateTime nextCommitDate)
        {
            //this sees if the commitment the for loop is currently on and the commitment in front of it has
            //a datetime that is exactly 15 minutes ahead of its dateTime
            return (DateTime.Compare(nextCommitDate, currentCommitDate.AddMinutes(15)) == 0);
        }

        public static void removeOpens(ref List<TutorMaster.Commitment> cmtList)
        {                                                                  //if commitment is open, remove it from the commitlist
            for (int i = 0; i < cmtList.Count(); i++)
            {
                if (isOpen(cmtList[i]))
                {
                    cmtList.Remove(cmtList[i]);
                }
            }
        }

        public static bool isOpen(TutorMaster.Commitment commit)                     //criteria for a commitment to be open
        {
            return (commit.Class == "-" && commit.Location == "-" && commit.Open == true && commit.Tutoring == false && commit.ID == -1);
        }

        public static bool waitingForTutor(TutorMaster.Commitment commit)            //criteria for a commitment to be waiting for a tutor
        {
            return (commit.Class != "-" && commit.Location == "-" && commit.Open == false && commit.Tutoring == false && commit.ID != -1);
        }

        public static bool waitingForLocation(TutorMaster.Commitment commit)         //criteria for a commitment to be waiting for a location
        {
            return (commit.Class != "-" && commit.Location == "-" && commit.Open == false && commit.Tutoring == true && commit.ID != -1);
        }

        public static bool waitingForLocationApproval(TutorMaster.Commitment commit) //criteria for a commitment to be waiting for a location approval
        {
            return (commit.Class != "-" && commit.Location.Contains('?') && commit.Open == false && commit.Tutoring == false && commit.ID != -1);
        }

        public static bool waitingForTutee(TutorMaster.Commitment commit)            //criteria for a commitment to be waiting for a tutee
        {
            return (commit.Class != "-" && commit.Location.Contains('?') && commit.Open == false && commit.Tutoring == true && commit.ID != -1);
        }

        public static bool isAccepted(TutorMaster.Commitment commit)                   //criteria for a commitment to be in the accepted state
        {
            return (commit.Class != "-" && !commit.Location.Contains('?') && commit.Location != "-" && commit.Open == false && commit.ID != -1);
        }
    }
}
