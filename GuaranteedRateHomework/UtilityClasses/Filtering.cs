using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GuaranteedRateHomework.Helpers
{
    public static class Filtering
    {
        public static void ProcessInput(string[] args)
        {
            ///filepath for input/output files
            string path = Directory.GetCurrentDirectory();

            ///holds all the person objects created from any files
            List<Person> allPersons = new List<Person>();

            if (args == null || args.Length == 0)
            {
                ///shut down if the user supplied no input files
                Console.WriteLine("No user file(s) specified! Aborting program...");
                Environment.Exit(0);
            }
            else if (args[0] == "generate")
            {
                ///if we need to generate some sample data for convenience's sake
                DataGenerator.GenerateData(10, path);
                allPersons = allPersons.Concat(FileIO.ReadFileData(path + "\\SampleInputPipe.txt")).ToList();
                allPersons = allPersons.Concat(FileIO.ReadFileData(path + "\\SampleInputComma.txt")).ToList();
                allPersons = allPersons.Concat(FileIO.ReadFileData(path + "\\SampleInputSpace.txt")).ToList();
            }
            else
            {
                ///read in any file(s) and parse them out
                foreach (string str in args)
                {
                    path = str;
                    allPersons = allPersons.Concat(FileIO.ReadFileData(path)).ToList();
                }
            }

            ///save combined records
            FileIO.SaveFile(allPersons, "..\\..\\..\\..\\GuaranteedRateHomeworkAPI\\Data\\Output.json");

            ///sort the output in the three ways specified
            IEnumerable<Person> genderSorted = Sorting.GenderSort(allPersons);
            IEnumerable<Person> birthdateSorted = Sorting.BirthdateSort(allPersons);
            IEnumerable<Person> lastnameSorted = Sorting.LastnameSort(allPersons);

            ///print the three sorted lists
            PrintOutput(genderSorted, "Output 1");
            PrintOutput(birthdateSorted, "Output 2");
            PrintOutput(lastnameSorted, "Output 3");
        }

        public static IEnumerable<Person> PopulatePersons(IEnumerable<string> lines)
        {
            List<Person> output = new List<Person>();

            ///split the string based on delimiter and create the person object
            ///we are going to assume the input is properly formatted for brevity's sake
            foreach (string str in lines)
            {
                Person p = CreatePersonFromString(str);

                ///this is kind of a janky way to check for an invalid person
                if (p.FavoriteColor != null)
                {
                    output.Add(p);
                }
            }

            return output;
        }

        public static Person CreatePersonFromString(string str)
        {
            ///split the strings based on our delimiters
            char[] delimiters = { '|', ',', ' ' };
            string[] personString = str.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            ///malformed person string, can't make an object out of it
            ///just return a new person object, which will have null fields we can check for
            if (personString.Length != 5)
            {
                return new Person();
            }

            Person pers = new Person
            {
                LastName = personString[0],
                FirstName = personString[1],
                Gender = personString[2],
                FavoriteColor = personString[3],
                DateOfBirth = DateTime.Parse(personString[4])
            };

            return pers;
        }

        public static void PrintOutput(IEnumerable<Person> personList, string header)
        {
            ///print a header before the list output
            Console.WriteLine("-----" + header + "-----");
            Console.WriteLine("{0,-15} {1,-15} {2,-15} {3,-15} {4,-15}\n", "LastName", "FirstName", "Gender", "FavoriteColor", "DateOfBirth");

            ///loop through the list and print it
            foreach (Person p in personList)
            {
                Console.WriteLine("{0,-15} {1,-15} {2,-15} {3,-15} {4,-15}\n", p.LastName, p.FirstName, p.Gender, p.FavoriteColor, p.DateOfBirth.ToShortDateString());
            }
            Console.WriteLine("\n");
        }
    }
}
