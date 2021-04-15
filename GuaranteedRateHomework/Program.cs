using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GuaranteedRateHomework
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            //make some Sample Data
            await GenerateData(10);

            //get current directory for the input files, prepare lists to hold the formatted lines from the files
            string path = Directory.GetCurrentDirectory();

            //read all the files into lists asynchronously
            var pipeFileReadTask = Task.Run(() => ReadFileData(path + "/SampleInputPipe.txt"));
            var commaFileReadTask = Task.Run(() => ReadFileData(path + "/SampleInputComma.txt"));
            var spaceFileReadTask = Task.Run(() => ReadFileData(path + "/SampleInputSpace.txt"));
            List<string> pipeDelimited = await pipeFileReadTask;
            List<string> commaDelimited = await commaFileReadTask;
            List<string> spaceDelimited = await spaceFileReadTask;

            //go through each list of strings and populate the 'allPersons' list
            //making tasks such that we can populate these three lists asynchronously
            var pipeTask = Task.Run(() => PopulatePersons(pipeDelimited, " | "));
            var commaTask = Task.Run(() => PopulatePersons(commaDelimited, ", "));
            var spaceTask = Task.Run(() => PopulatePersons(spaceDelimited, " "));
            List<Person> pipePersons = await pipeTask;
            List<Person> commaPersons = await commaTask;
            List<Person> spacePersons = await spaceTask;

            //combine all our persons
            var allPersons = pipePersons.Concat(commaPersons).Concat(spacePersons).ToList();

            //sort the output in the three ways specified
            //again, do the three sorting methods asynchronously
            var genderTask = Task.Run(() => GenderSort(allPersons));
            var birthdateTask = Task.Run(() => BirthdateSort(allPersons));
            var lastnameTask = Task.Run(() => LastnameSort(allPersons));
            List<Person> genderSorted = await genderTask;
            List<Person> birthdateSorted = await birthdateTask;
            List<Person> lastnameSorted = await lastnameTask;

            //print the three sorted lists
            PrintOutput(genderSorted, "Output 1");
            PrintOutput(birthdateSorted, "Output 2");
            PrintOutput(lastnameSorted, "Output 3");
        }

        public static List<Person> PopulatePersons(List<string> lines, string delim)
        {
            List<Person> output = new List<Person>();

            //split the string based on delimiter and create the person object
            //we are going to assume the input is properly formatted for brevity's sake
            foreach (string str in lines)
            {
                string[] personStrings = str.Split(delim);
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

        public static List<Person> GenderSort(List<Person> personList)
        {
            List<Person> genderSorted = personList.OrderBy(o => o.Gender)
                                                  .ThenBy(o => o.LastName)
                                                  .ThenBy(o => o.FirstName)
                                                  .ToList();
            return genderSorted;
        }
        public static List<Person> BirthdateSort(List<Person> personList)
        {
            List<Person> birthdateSorted = personList.OrderBy(o => o.DateOfBirth)
                                                     .ThenBy(o => o.LastName)
                                                     .ThenBy(o => o.FirstName)
                                                     .ToList();
            return birthdateSorted;
        }

        public static List<Person> LastnameSort(List<Person> personList)
        {
            List<Person> lastnameSorted = personList.OrderByDescending(o => o.LastName)
                                                    .ThenByDescending(o => o.FirstName)
                                                    .ToList();
            return lastnameSorted;
        }

        public static async Task GenerateData(int lines)
        {
            await DataGenerator.generateFile(lines, " | ", "SampleInputPipe.txt");
            await DataGenerator.generateFile(lines, ", ", "SampleInputComma.txt");
            await DataGenerator.generateFile(lines, " ", "SampleInputSpace.txt");
        }

        public static List<string> ReadFileData(string path)
        {
            List<string> listDelimited = new List<string>();

            //try to read our sample input, throw exceptions 
            try
            {
                string[] delimitedArr = System.IO.File.ReadAllLines(path);
                listDelimited = new List<string>(delimitedArr);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return listDelimited;
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
    }
}
