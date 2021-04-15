using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuaranteedRateHomework.Helpers
{
    public class Filtering
    {
        public static List<Person> ReadFileData(string path)
        {
            //'fileLinesList' holds raw strings from the files
            //'personsList' holds the Person objects created from those raw strings
            List<string> fileLinesList = new List<string>();
            List<Person> personsList = new List<Person>();

            //try to read our sample input, throw exceptions 
            try
            {
                string[] fileLines = System.IO.File.ReadAllLines(path);
                fileLinesList = new List<string>(fileLines);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
            //Filter our list of input into Persons objects
            personsList = PopulatePersons(fileLinesList);
            return personsList;
        }

        public static List<Person> PopulatePersons(List<string> lines)
        {
            List<Person> output = new List<Person>();
            char[] delimiters = { '|', ',', ' '};

            if (lines.Count == 0)
                Console.WriteLine("Input file was empty!");

            //split the string based on delimiter and create the person object
            //we are going to assume the input is properly formatted for brevity's sake
            foreach (string str in lines)
            {
                string[] personStrings = str.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                Person p = new Person
                {
                    LastName = personStrings[0],
                    FirstName = personStrings[1],
                    Gender = personStrings[2],
                    FavoriteColor = personStrings[3],
                    DateOfBirth = DateTime.Parse(personStrings[4])
                };
                output.Add(p);
            }

            return output;
        }

        public static void PrintOutput(List<Person> personList, string header)
        {
            //print a header before the list output
            Console.WriteLine("-----" + header + "-----");
            Console.WriteLine("{0,-15} {1,-15} {2,-15} {3,-15} {4,-15}\n", "LastName", "FirstName", "Gender", "FavoriteColor", "DateOfBirth");

            //loop through the list and print it
            foreach (Person p in personList)
            {
                Console.WriteLine("{0,-15} {1,-15} {2,-15} {3,-15} {4,-15}\n", p.LastName, p.FirstName, p.Gender, p.FavoriteColor, p.DateOfBirth.Date);
            }
            Console.WriteLine("\n");
        }

        public static void SaveFile(List<Person> personList)
        {
            List<string> fileOutput = new List<string>();
            string holder = string.Empty;

            //turn the person objects into strings and put them in the list
            foreach (Person pers in personList)
            {
                holder = pers.LastName + " " + pers.FirstName + " " + pers.Gender + " " + pers.FavoriteColor + " " + pers.DateOfBirth.ToString() + "\n";
                fileOutput.Add(holder);
            }

            //save output to file
            try
            {
                System.IO.File.WriteAllLines("TestOutput.txt", fileOutput);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
