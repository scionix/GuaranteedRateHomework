using System;
using System.Collections.Generic;
using System.Linq;

namespace GuaranteedRateHomework.Helpers
{
    public class Filtering
    {
        public static IEnumerable<Person> PopulatePersons(IEnumerable<string> lines)
        {
            List<Person> output = new List<Person>();

            if (lines.Count() == 0)
                Console.WriteLine("Input file was empty!");

            //split the string based on delimiter and create the person object
            //we are going to assume the input is properly formatted for brevity's sake
            foreach (string str in lines)
            {
                Person p = CreatePersonFromString(str);
                output.Add(p);
            }

            return output;
        }

        public static Person CreatePersonFromString(string str)
        {
            //split the strings based on our delimiters
            char[] delimiters = { '|', ',', ' ' };
            string[] personString = str.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            //malformed person string, can't make an object out of it
            //just return a new person object
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
