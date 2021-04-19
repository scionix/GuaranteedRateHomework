using GuaranteedRateHomework;
using GuaranteedRateHomework.Helpers;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace UnitTests
{
    public class FilteringTests
    {
        private string _path;
        private string _jsonPath;
        private string _generatePipePath;
        private string _generateCommaPath;
        private string _generateSpacePath;

        public FilteringTests()
        {
            _path = Directory.GetCurrentDirectory() + "\\unitTestingOutput.txt";
            _jsonPath = Directory.GetCurrentDirectory() + "\\TestJson.json";
            _generatePipePath = Directory.GetCurrentDirectory() + "\\SampleInputPipe.txt";
            _generateCommaPath = Directory.GetCurrentDirectory() + "\\SampleInputComma.txt";
            _generateSpacePath = Directory.GetCurrentDirectory() + "\\SampleInputSpace.txt";
        }

        [Fact]
        public void PopulatePersons_Given_5_Person_Strings_Produces_5_Person_Objects()
        {
            string[] people =
            {
                "Smith John Male Black 1/1/1950",
                "Black | Bill | Male | Green | 7/8/1952",
                "Rodriguez, Jalen, Male, Orange, 9/22/1970",
                "Doe Jane Female Blue 12/12/1955",
                "Jiminez Carla Female White 8/14/1990",
            };

            List<Person> populated = (List<Person>)Filtering.PopulatePersons(people);

            Assert.Equal(5, populated.Count);
        }

        [Fact]
        public void PopulatePersons_Given_0_Person_Strings_Produces_0_Person_Objects()
        {
            string[] people = { };

            List<Person> populated = (List<Person>)Filtering.PopulatePersons(people);

            Assert.Empty(populated);
        }

        [Fact]
        public void PopulatePersons_Given_3_Valid_Person_Strings_Produces_3_Person_Objects()
        {
            string[] people =
            {
                "Smith John Male Black 1/1/1950",
                "Black | Bill | Male | Green | 7/8/1952",
                "Rodriguez, Jalen, Male, Orange, 9/22/1970",
                "Doe --------- Jane Female Blue 12/12/1955",
                "Jiminez Carla Female ][][][ White 8/14/1990",
            };

            List<Person> populated = (List<Person>)Filtering.PopulatePersons(people);

            Assert.Equal(3, populated.Count);
        }

        [Fact]
        public void ProcessInput_Given_Generate_Args_Generates_Files()
        {
            string[] args = {"generate"};
            Filtering.ProcessInput(args);

            string[] pipeOutput = File.ReadAllLines(_generatePipePath);
            string[] commaOutput = File.ReadAllLines(_generateCommaPath);
            string[] spaceOutput = File.ReadAllLines(_generateSpacePath);

            Assert.Equal(10, pipeOutput.Length);
            Assert.Equal(10, commaOutput.Length);
            Assert.Equal(10, spaceOutput.Length);
        }
    }
}