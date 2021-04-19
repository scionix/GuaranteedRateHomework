using GuaranteedRateHomework;
using GuaranteedRateHomework.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Xunit;

namespace UnitTests
{
    public class FileIOTests
    {
        private string _path;
        private string _jsonPath;

        public FileIOTests()
        {
            _path = Directory.GetCurrentDirectory() + "\\unitTestingOutput.txt";
            _jsonPath = Directory.GetCurrentDirectory() + "\\TestJson.json";
        }

        [Fact]
        public void ReadFile_Given_5_Lines_Produces_5_objects()
        {
            DataGenerator.GenerateFile(10, ", ", _path);

            List<Person> testOutput = (List<Person>)FileIO.ReadFileData(_path);

            Assert.Equal(10, testOutput.Count);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void ReadFile_Given_Invalid_Argument_Produces_InvalidArgumentException(string path)
        {
            var ex = Assert.Throws<ArgumentException>(() => FileIO.ReadFileData(path));
        }

        [Fact]
        public void ReadFile_Given_Invalid_Filepath_Produces_FileNotFoundException()
        {
            var ex = Assert.Throws<FileNotFoundException>(() => FileIO.ReadFileData("5"));
        }

        [Fact]
        public void SaveFile_Given_Person_List_Produces_Json_Document()
        {
            List<Person> people = new List<Person>();

            people.Add(new Person { LastName = "Doe", FirstName = "John", Gender = "Male", FavoriteColor = "Blue", DateOfBirth = DateTime.Parse("1/1/1950") });
            people.Add(new Person { LastName = "Smith", FirstName = "Jane", Gender = "Female", FavoriteColor = "Green", DateOfBirth = DateTime.Parse("1/1/1960") });
            people.Add(new Person { LastName = "Black", FirstName = "Bill", Gender = "Male", FavoriteColor = "Red", DateOfBirth = DateTime.Parse("1/1/1970") });

            FileIO.SaveFile(people, _jsonPath);

            var peopleData = File.ReadAllText(_jsonPath);
            var deserializedPeople = JsonSerializer.Deserialize<List<Person>>(peopleData);

            Assert.Equal(3, deserializedPeople.Count);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void SaveFile_Given_Invalid_Argument_Produces_ArgumentException(string path)
        {
            List<Person> people = new List<Person>();
            var ex = Assert.Throws<ArgumentException>(() => FileIO.SaveFile(people, path));
        }
    }
}