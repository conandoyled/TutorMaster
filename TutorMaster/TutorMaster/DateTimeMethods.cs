using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TutorMaster
{
    class DateTimeMethods
    {
        public static DateTime getDate(string day)
        {
            string totalDate = day.Split(' ')[0];                                          //get the date part of the string
            int month = Convert.ToInt32(totalDate.Split('/')[0]);                          //get the month part of the string
            int date = Convert.ToInt32(totalDate.Split('/')[1]);                           //get the date number of the string
            int year = Convert.ToInt32(totalDate.Split('/')[2]);                           //get the year number of the string

            string time = day.Split(' ')[1];                                               //get the time part of the string
            int hour = Convert.ToInt32(time.Split(':')[0]);                                //get the hour number from the time string
            int min = Convert.ToInt32(time.Split(':')[1]);                                 //get the minute number from the time string
            string amPm = day.Split(' ')[2];                                               //get whether this is in the morning or evening

            if (amPm == "PM" && hour != 12)                                                //if evening and not 12, then add 12
            {
                hour += 12;
            }
            else if (amPm == "AM" && hour == 12)                                           //if 12AM, then set hour to 0
            {
                hour = 0;
            }

            DateTime result = new DateTime(year, month, date, hour, min, 0);               //return the datetime
            return result;
        }

        public static DateTime getDate(string monthDay, string startTime)
        {
            int year = 2017;

            List<string> monthsList = new List<string>() { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };


            string month = monthDay.Split(' ')[1];
            int monthInt = 0;
            for (int n = 0; n < monthsList.Count(); n++)                                                    //get the index of the month we are operating in
            {
                if (month == monthsList[n])
                {
                    monthInt = n + 1;
                    break;
                }
            }
            int day = Convert.ToInt32(monthDay.Split(' ')[2]);                                              //get the number of the day we are interested in


            int hour = Convert.ToInt32(startTime.Split(':')[0]);                                            //get the hour from the time parameter
            int min = Convert.ToInt32(startTime.Split(':')[1]);                                             //get the minute from the time parameter
            string amPm = startTime.Split(' ')[1];                                                          //get whether this is the morning or evening

            if (amPm == "PM" && hour != 12)                                                                 //add 12 or set to 0 if necessary
            {
                hour += 12;
            }
            else if (amPm == "AM" && hour == 12)
            {
                hour = 0;
            }

            DateTime result = new DateTime(year, monthInt, day, hour, min, 0);                              //return dateTime of the strings we parsed
            return result;
        }

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

        public static DateTime getListViewTime(string slot)                                    //take a string of datetime from listview's string
        {
            string dateString = slot.Split(' ')[0];                                      //get the entire start datetime string

            int month = Convert.ToInt32(dateString.Split('/')[0]);                       //convert its month value into an integer
            int day = Convert.ToInt32(dateString.Split('/')[1]);                         //convert its day value into an integer

            string timeString = slot.Split(' ')[1];                                      //get the time part of the start datetime

            int hour = Convert.ToInt32(timeString.Split(':')[0]);                        //convert its hour into an integer
            int min = Convert.ToInt32(timeString.Split(':')[1]);                         //convert its minutes into an integer

            string amPm = slot.Split(' ')[2];                                            //record whether this is in the morning or evening

            if (hour < 12 && amPm == "PM")                                               //add 12 to hours if necessary
            {
                hour += 12;
            }
            else if (hour == 12 && amPm == "AM")                                         //if first hour of the day, set hour to 0
            {
                hour = 0;
            }
            DateTime date = new DateTime(2017, month, day, hour, min, 0);                //make a datetime instance with the collected data and return it
            return date;
        }

        public static bool weeklyAndFound(Commitment commit, List<DateTime> searchList)
        {//this function checks if a commitment is weekly and found in the commitment list
            return commit.Weekly == true && SortsAndSearches.BinarySearch(searchList, Convert.ToDateTime(commit.StartTime));
        }

        public static bool weekBackEarlier(DateTime weekBack, Commitment commit)
        {//this function sees if the the weekback dateTime is before the commitment time
            return DateTime.Compare(weekBack, Convert.ToDateTime(commit.StartTime)) < 0;
        }

        public static bool sameTime(Commitment commit, DateTime weekBack)
        {//this funciton sees if the the commitment is the sametime in the weekback
            return DateTime.Compare(Convert.ToDateTime(commit.StartTime), weekBack) == 0;
        }

        public static bool endOfSemesIsLater(DateTime endSemes, DateTime weekForward)
        {
            return DateTime.Compare(endSemes, weekForward) > 0;
        }

        public static bool forwardEarlierThanStart(DateTime weekForward, Commitment commit)
        {
            return DateTime.Compare(weekForward, Convert.ToDateTime(commit.StartTime)) < 0;
        }

        public static bool startEarlierThanEnd(DateTime startTime, DateTime endTime)
        {
            return startTime.CompareTo(endTime) < 0;
        }

        public static bool betweenGivenStartAndEndTime(DateTime startDate, DateTime endDate, Commitment commit)
        {
            return DateTime.Compare(startDate, Convert.ToDateTime(commit.StartTime)) <= 0 && DateTime.Compare(endDate, Convert.ToDateTime(commit.StartTime)) > 0;
        }

        public static bool inTheTimeSlot(DateTime startDate, DateTime endDate, Commitment commit)
        {
            return (DateTime.Compare(startDate, Convert.ToDateTime(commit.StartTime)) <= 0 && DateTime.Compare(endDate, Convert.ToDateTime(commit.StartTime)) > 0);
        }
    }
}
