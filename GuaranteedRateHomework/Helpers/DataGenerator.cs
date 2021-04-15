using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuaranteedRateHomework
{
    class DataGenerator
    {
        //all name lists are size 20
        private static List<string> _maleNames = new List<String>
        {
            "Liam", "Noah", "Oliver", "William", "Elijah", "James", "Benjamin", "Lucas", "Mason", "Ethan",
            "Alexander", "Henry", "Jacob", "Michael", "Daniel", "Logan", "Jackson", "Sebastian", "Jack", "Aiden"
        };

        private static List<string> _femaleNames = new List<String>
        {
            "Olivia", "Emma", "Ava", "Sophia", "Isabella", "Charlotte", "Amelia", "Mia", "Harper", "Evelyn",
            "Abigail", "Emily", "Ella", "Elizabeth", "Camila", "Luna", "Sofia", "Avery", "Mila", "Aria"
        };

        private static List<string> _lastNames = new List<String>
        {
            "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez",
            "Hernandez", "Lopez", "Gonzalez", "Wilson", "Anderson", "Thomas", "Taylor", "Moore", "Jackson", "Martin"
        };

        //colors list is size 8
        private static List<string> _colors = new List<String>
        {
            "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "Black", "White"
        };

        public static async Task generateFile(int lines, string delim, string fileName)
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
                    holder = buildLine(1, delim);
                    toWrite.Add(holder);
                }
                else 
                {
                    holder = buildLine(0, delim);
                    toWrite.Add(holder);
                }
            }

            //output lines to file with given filename
            try
            {
                await File.WriteAllLinesAsync(fileName, toWrite);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        //build a line of input with a name
        //given 1, build male name, given 0, build female name
        //defaults to male name if given weird number
        private static string buildLine(int gender, string delim)
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
                                + createDob();
            }
            else
            {
                holder = _lastNames[rnd.Next(0, 19)] + delim
                                + _maleNames[rnd.Next(0, 19)] + delim
                                + "Male" + delim
                                + _colors[rnd.Next(0, 7)] + delim
                                + createDob();
            }

            return holder;
        }

        //create the DateOfBirth string for a given line
        private static string createDob()
        {
            Random rnd = new Random();

            //going to make it easy on myself and not go past the 27th day of the month
            //this avoids leap year issues with februrary
            string month = rnd.Next(1, 12).ToString();
            string day = rnd.Next(1, 27).ToString();
            string year = rnd.Next(1940, 2000).ToString();

            return month + "/" + day + "/" + year;
        }

        public static async Task GenerateData(int lines)
        {
            await DataGenerator.generateFile(lines, " | ", "SampleInputPipe.txt");
            await DataGenerator.generateFile(lines, ", ", "SampleInputComma.txt");
            await DataGenerator.generateFile(lines, " ", "SampleInputSpace.txt");
        }
    }
}
