using System;
using System.Collections.Generic;

namespace GuaranteedRateHomework.Helpers
{
    public class Filtering
    {
        public static List<Person> PopulatePersons(IEnumerable<string> lines)
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

        public static void PrintOutput(IEnumerable<Person> personList, string header)
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
    }
}
