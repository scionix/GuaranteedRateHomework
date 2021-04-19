using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuaranteedRateHomework
{
    public static class DataGenerator
    {
        //all name lists are size 20
        public static List<string> _maleNames = new List<String>
        {
            "Liam", "Noah", "Oliver", "William", "Elijah", "James", "Benjamin", "Lucas", "Mason", "Ethan",
            "Alexander", "Henry", "Jacob", "Michael", "Daniel", "Logan", "Jackson", "Sebastian", "Jack", "Aiden"
        };

        public static List<string> _femaleNames = new List<String>
        {
            "Olivia", "Emma", "Ava", "Sophia", "Isabella", "Charlotte", "Amelia", "Mia", "Harper", "Evelyn",
            "Abigail", "Emily", "Ella", "Elizabeth", "Camila", "Luna", "Sofia", "Avery", "Mila", "Aria"
        };

        public static List<string> _lastNames = new List<String>
        {
            "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez",
            "Hernandez", "Lopez", "Gonzalez", "Wilson", "Anderson", "Thomas", "Taylor", "Moore", "Jackson", "Martin"
        };

        //colors list is size 8
        public static List<string> _colors = new List<String>
        {
            "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "Black", "White"
        };

        public static void GenerateFile(int lines, string delim, string fileName)
        {
            //holds our lines to go out to the file
            List<string> toWrite = new List<string>();

            for (int i = 0; i < lines; i++)
            {
                //string to build with format of:
                //Last Name [delimiter] FirstName [delimiter] Gender [delimiter] FavoriteColor [delimiter] DateOfBirth
                string holder = string.Empty;

                //make a line with a male name if 'i' is even
                if (i % 2 == 0)
                {
                    holder = BuildLine(1, delim);
                    toWrite.Add(holder);
                }
                else 
                {
                    holder = BuildLine(0, delim);
                    toWrite.Add(holder);
                }
            }

            //output lines to file with given filename
            try
            {
                File.WriteAllLines(fileName, toWrite);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        //build a line of input with a name
        //given 1, build male name, given 0, build female name
        //defaults to male name if given weird number
        public static string BuildLine(int gender, string delim)
        {
            //random for indexing our list of names/colors to choose from
            Random rnd = new Random();
            string holder = string.Empty;

            if (gender == 0)
            {
                holder = _lastNames[rnd.Next(0, 19)] + delim
                                + _femaleNames[rnd.Next(0, 19)] + delim
                                + "Female" + delim
                                + _colors[rnd.Next(0, 7)] + delim
                                + CreateDob();
            }
            else
            {
                holder = _lastNames[rnd.Next(0, 19)] + delim
                                + _maleNames[rnd.Next(0, 19)] + delim
                                + "Male" + delim
                                + _colors[rnd.Next(0, 7)] + delim
                                + CreateDob();
            }

            return holder;
        }

        //create the DateOfBirth string for a given line
        public static string CreateDob()
        {
            Random rnd = new Random();

            //going to make it easy on myself and not go past the 27th day of the month
            //this avoids leap year issues with februrary
            string month = rnd.Next(1, 12).ToString();
            string day = rnd.Next(1, 27).ToString();
            string year = rnd.Next(1940, 2000).ToString();

            return month + "/" + day + "/" + year;
        }

        public static void GenerateData(int lines, string path)
        {
            GenerateFile(Math.Abs(lines), " | ", (path + "\\SampleInputPipe.txt"));
            GenerateFile(Math.Abs(lines), ", ", (path + "\\SampleInputComma.txt"));
            GenerateFile(Math.Abs(lines), " ", (path + "\\SampleInputSpace.txt"));
        }
    }
}
