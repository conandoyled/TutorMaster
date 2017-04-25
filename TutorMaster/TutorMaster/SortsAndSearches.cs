using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TutorMaster
{
    class SortsAndSearches
    {
        //this binary search is to find a commitment in a list of commitments by start times. if the commit's start time is in the list, then this returns true. otherwise, it returns false
        public static bool BinarySearch(List<string> cmtList, string commit)
        {
            bool found = false;
            int first = 0;
            int last = cmtList.Count() - 1;
            while (first <= last && !found)
            {
                int midpoint = (first + last) / 2;
                if (DateTime.Compare(DateTimeMethods.getStartTime(cmtList[midpoint]), DateTimeMethods.getStartTime(commit)) == 0)
                {
                    found = true;
                    return found;
                }
                else
                {
                    if (DateTime.Compare(DateTimeMethods.getStartTime(commit), DateTimeMethods.getStartTime(cmtList[midpoint])) < 0)
                    {
                        last = midpoint - 1;
                    }
                    else
                    {
                        first = midpoint + 1;
                    }
                }
            }
            return found;
        }

        //this finds the direct commitment's Date time and searches in a list of datetimes. if the start time of the commit is in the list, it returns true, otherwise, it returns false
        public static bool BinarySearch(List<DateTime> cmtList, DateTime commit)
        {
            bool found = false;
            int first = 0;
            int last = cmtList.Count() - 1;
            while (first <= last && !found)
            {
                int midpoint = (first + last) / 2;
                if (DateTime.Compare(cmtList[midpoint], commit) == 0)
                {
                    found = true;
                    return found;
                }
                else
                {
                    if (DateTime.Compare(commit, cmtList[midpoint]) < 0)
                    {
                        last = midpoint - 1;
                    }
                    else
                    {
                        first = midpoint + 1;
                    }
                }
            }
            return found;
        }

        //this binary search looks for a datetime in a commitment list. if it is, it returns true. otherwise, return false
        public static bool BinarySearch(DateTime date, List<TutorMaster.Commitment> searchList)
        {
            bool found = false;
            int first = 0;
            int last = searchList.Count() - 1;
            while (first <= last && !found)
            {
                int midpoint = (first + last) / 2;
                if (DateTime.Compare(Convert.ToDateTime(searchList[midpoint].StartTime), date) == 0)
                {
                    found = true;
                    return found;
                }
                else
                {
                    if (DateTime.Compare(date, Convert.ToDateTime(searchList[midpoint].StartTime)) < 0)
                    {
                        last = midpoint - 1;
                    }
                    else
                    {
                        first = midpoint + 1;
                    }
                }
            }
            return found;
        }

        //QuickSort functions
        //this QuickSort sorts a commitment list by datetimes
        private static void Split(ref List<TutorMaster.Commitment> values, int first, int last, ref int splitPoint)
        {
            int center = (first + last) / 2;
            int median = 0;
            DateTime valueF = Convert.ToDateTime(values[first].StartTime);
            DateTime valueC = Convert.ToDateTime(values[center].StartTime);
            DateTime valueL = Convert.ToDateTime(values[last].StartTime);

            if ((DateTime.Compare(valueF, valueC) >= 0 && DateTime.Compare(valueF, valueL) <= 0) ||
                (DateTime.Compare(valueF, valueL) >= 0 && DateTime.Compare(valueF, valueL) <= 0))
            {
                median = first;
            }
            else if (DateTime.Compare(valueC, valueF) >= 0 && (DateTime.Compare(valueC, valueL) <= 0) ||
                   (DateTime.Compare(valueC, valueF)) >= 0 && (DateTime.Compare(valueC, valueL) <= 0))
            {
                median = center;
            }
            else
            {
                median = last;
            }
            //Swap the median and first committments in the list
            TutorMaster.Commitment temp = values[first];
            values[first] = values[median];
            values[median] = temp;

            valueF = Convert.ToDateTime(values[first].StartTime); //get current first datetime
            valueC = Convert.ToDateTime(values[center].StartTime);//get current center datetime;
            valueL = Convert.ToDateTime(values[last].StartTime);

            TutorMaster.Commitment splitVal = values[first];
            DateTime splitDate = Convert.ToDateTime(values[first].StartTime);

            int saveFirst = first;
            bool onCorrectSide = true;

            first++;
            valueF = Convert.ToDateTime(values[first].StartTime);
            do
            {
                onCorrectSide = true;
                while (onCorrectSide)
                {
                    if (DateTime.Compare(valueF, splitDate) > 0)
                    {
                        onCorrectSide = false;
                    }
                    else
                    {
                        first++;
                        valueF = Convert.ToDateTime(values[first].StartTime);
                        onCorrectSide = (first <= last);
                    }
                }

                onCorrectSide = (first <= last);
                while (onCorrectSide)
                {
                    if (DateTime.Compare(valueL, splitDate) <= 0)
                    {
                        onCorrectSide = false;
                    }
                    else
                    {
                        last--;
                        valueL = Convert.ToDateTime(values[last].StartTime);
                        onCorrectSide = (first <= last);
                    }
                }

                if (first < last)
                {
                    TutorMaster.Commitment temp2 = values[first];
                    values[first] = values[last];
                    values[last] = temp2;
                    first++;
                    last--;

                    valueF = Convert.ToDateTime(values[first].StartTime);
                    valueL = Convert.ToDateTime(values[last].StartTime);
                }
            } while (first <= last);

            splitPoint = last;
            TutorMaster.Commitment temp3 = values[saveFirst];
            values[saveFirst] = values[splitPoint];
            values[splitPoint] = temp3;
        }

        private static void QuickSort2(ref List<TutorMaster.Commitment> values, int first, int last)
        {
            if (first < last)
            {
                int splitPoint = -99;

                Split(ref values, first, last, ref splitPoint);
                QuickSort2(ref values, first, splitPoint - 1);
                QuickSort2(ref values, splitPoint + 1, last);
            }
        }

        public static void QuickSort(ref List<TutorMaster.Commitment> values, int numValues)
        {
            QuickSort2(ref values, 0, numValues - 1);
        }
    }
}
