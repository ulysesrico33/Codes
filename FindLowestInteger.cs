using System;
using System.Collections.Generic;

namespace FindLowestPositiveInteger
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            /*
            What the program should do:
            Find the lowest positive integer that does not exist in the array.
            The array can contain duplicates and negative numbers as well
             */

            //List to analyse
            List<int> lstExample = new List<int>();
            

            lstExample.Add(1);
            lstExample.Add(2);
            lstExample.Add(0);

            lstExample.Sort();

            List<int> lstDefinitive = CleanFirstList(lstExample);

            int savedNextNumber = 0;
            foreach(var item in lstDefinitive)
            {
                savedNextNumber = item + 1;
                if (!lstDefinitive.Contains(savedNextNumber))
                {
                    Console.WriteLine(savedNextNumber);
                    break;
                }
            }
   
        }

        /// <summary>
        /// Clean the given list from negative values
        /// and get rid of repetead values
        /// </summary>
        /// <param name="SourceList"></param>
        /// <returns></returns>
        public static List<int> CleanFirstList(List<int> SourceList)
        {
            List<int> lst = new List<int>();

            foreach (var item in SourceList)
            {
                //Take all positive integers
                if (item > 0)
                {
                    if(!lst.Contains(item))
                        lst.Add(item);
                }

            }

            return lst;

        }

        /// <summary>
        /// Show each member of the list
        /// </summary>
        /// <param name="lst"></param>
        public static void ShowList(List<int> lst)
        {

            foreach (var item in lst)
            {

                Console.WriteLine(item);
            }
        }
    }
}
