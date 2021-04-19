using GuaranteedRateHomework;
using System;
using System.IO;
using Xunit;

namespace UnitTests
{
    public class DataGeneratorTests
    {
        private string _path;
        private string _singleFilePath;
        private string _generatePipePath;
        private string _generateCommaPath;
        private string _generateSpacePath;

        public DataGeneratorTests()
        {
            _path = Directory.GetCurrentDirectory();
            _singleFilePath = Directory.GetCurrentDirectory() + "\\unitTestingOutput.txt";
            _generatePipePath = Directory.GetCurrentDirectory() + "\\SampleInputPipe.txt";
            _generateCommaPath = Directory.GetCurrentDirectory() + "\\SampleInputComma.txt";
            _generateSpacePath = Directory.GetCurrentDirectory() + "\\SampleInputSpace.txt";
        }

        [Theory]
        [InlineData(" | ")]
        [InlineData(", ")]
        [InlineData(" ")]
        public void BuildLine_Given_1_Produces_Male_Name(string delim)
        {
            string maleName = DataGenerator.BuildLine(1, delim);
            
            char[] delimiters = { '|', ',', ' ' };
            string[] personString = maleName.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            Assert.Equal(5, personString.Length);
            Assert.Contains(personString[1], DataGenerator._maleNames);
        }

        [Theory]
        [InlineData(" | ")]
        [InlineData(", ")]
        [InlineData(" ")]
        public void BuildLine_Given_0_Produces_Female_Name(string delim)
        {
            string maleName = DataGenerator.BuildLine(0, delim);

            char[] delimiters = { '|', ',', ' ' };
            string[] personString = maleName.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            Assert.Equal(5, personString.Length);
            Assert.Contains(personString[1], DataGenerator._femaleNames);
        }

        [Theory]
        [InlineData(" | ")]
        [InlineData(", ")]
        [InlineData(" ")]
        public void BuildLine_Given_3_Produces_Male_Name(string delim)
        {
            string maleName = DataGenerator.BuildLine(3, delim);

            char[] delimiters = { '|', ',', ' ' };
            string[] personString = maleName.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            Assert.Equal(5, personString.Length);
            Assert.Contains(personString[1], DataGenerator._maleNames);
        }

        [Fact]
        public void CreateDob_Makes_Proper_String()
        {
            string dateOfBirth = DataGenerator.CreateDob();

            char[] delimiters = { '/' };
            string[] dobString = dateOfBirth.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            Assert.Equal(3, dobString.Length);

            ///month
            Assert.InRange(Int32.Parse(dobString[0]), 1, 12);
            
            ///day
            Assert.InRange(Int32.Parse(dobString[1]), 1, 27);

            ///year
            Assert.InRange(Int32.Parse(dobString[2]), 1940, 2000);
        }

        [Theory]
        [InlineData(" | ")]
        [InlineData(", ")]
        [InlineData(" ")]
        public void GenerateFile_Given_10_Lines_Produces_Correct_Lines(string delim)
        {
            DataGenerator.GenerateFile(10, delim, _singleFilePath);

            string[] testOutput = File.ReadAllLines(_singleFilePath);

            Assert.Equal(10, testOutput.Length);
        }

        [Theory]
        [InlineData(" | ")]
        [InlineData(", ")]
        [InlineData(" ")]
        public void GenerateFile_Given_10_Lines_Produces_5_Males_5_Females(string delim)
        {
            DataGenerator.GenerateFile(10, delim, _singleFilePath);
            int maleCounter = 0;
            int femaleCounter = 0;

            string[] testOutput = File.ReadAllLines(_singleFilePath);

            ///break each line in the file down, so we can check the names
            foreach (string str in testOutput)
            { 
                string[] currentLine = str.Split(delim, StringSplitOptions.RemoveEmptyEntries);

                ///check if male or female name
                if (DataGenerator._maleNames.Contains(currentLine[1]))
                    maleCounter++;
                else
                    femaleCounter++;
            }

            Assert.Equal(10, testOutput.Length);
            Assert.True(maleCounter == 5);
            Assert.True(femaleCounter == 5);
        }

        [Fact]
        public void GenerateData_Produces_3_Output_Files()
        {
            DataGenerator.GenerateData(10, _path);

            string[] pipeOutput = File.ReadAllLines(_generatePipePath);
            string[] commaOutput = File.ReadAllLines(_generateCommaPath);
            string[] spaceOutput = File.ReadAllLines(_generateSpacePath);

            Assert.Equal(10, pipeOutput.Length);
            Assert.Equal(10, commaOutput.Length);
            Assert.Equal(10, spaceOutput.Length);
        }
    }
}