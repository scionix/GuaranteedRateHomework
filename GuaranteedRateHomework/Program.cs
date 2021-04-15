using GuaranteedRateHomework.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
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
            await ProcessAndSortInput(args);
        }

        public static async Task ProcessAndSortInput(string[] args)
        {
            //filepath for input/output files
            string path = string.Empty;

            //holds all the person objects created from any files
            List<Person> allPersons = new List<Person>();

            if (args == null || args.Length == 0)
            {
                //make some Sample Data if the user supplied no Filename
                //set the filepath to release or debug folder
                Console.WriteLine("No user file(s) specified, generating sample test data");
                path = Directory.GetCurrentDirectory();
                await DataGenerator.GenerateData(10);
            }
            else
            {
                //read in any file(s) and parse them out
                foreach (string str in args)
                {
                    path = str;
                    allPersons = allPersons.Concat(Filtering.ReadFileData(path)).ToList();
                }
            }

            //save combined records
            Filtering.SaveFile(allPersons);

            //sort the output in the three ways specified
            var genderTask = Task.Run(() => Sorting.GenderSort(allPersons));
            var birthdateTask = Task.Run(() => Sorting.BirthdateSort(allPersons));
            var lastnameTask = Task.Run(() => Sorting.LastnameSort(allPersons));
            List<Person> genderSorted = await genderTask;
            List<Person> birthdateSorted = await birthdateTask;
            List<Person> lastnameSorted = await lastnameTask;

            //print the three sorted lists
            Filtering.PrintOutput(genderSorted, "Output 1");
            Filtering.PrintOutput(birthdateSorted, "Output 2");
            Filtering.PrintOutput(lastnameSorted, "Output 3");
        }
    }
}
