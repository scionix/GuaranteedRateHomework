using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace GuaranteedRateHomework.Helpers
{
    public class FileIO
    {
        public static IEnumerable<Person> ReadFileData(string path)
        {
            //'fileLinesList' holds raw strings from the files
            //'personsList' holds the Person objects created from those raw strings
            IEnumerable<string> fileLinesList = new List<string>();
            IEnumerable<Person> personsList = new List<Person>();

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
            personsList = Filtering.PopulatePersons(fileLinesList);
            return personsList;
        }

        public static void SaveFile(IEnumerable<Person> personList)
        {
            //serialize the list of Persons into JSON text
            var json = JsonSerializer.Serialize(personList);

            //save output to file
            try
            {
                File.WriteAllText("C:\\Users\\Will\\source\\repos\\GuaranteedRateHomework\\GuaranteedRateHomeworkAPI\\Data\\TestOutput.json", json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
