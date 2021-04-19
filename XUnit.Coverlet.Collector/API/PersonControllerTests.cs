using GuaranteedRateHomework;
using GuaranteedRateHomeworkAPI.Controllers;
using GuaranteedRateHomeworkAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GRUnitTests.API
{
    public class PersonControllerTests : PersonControllerBaseTestClass
    {
        public PersonControllerTests() : base(new DbContextOptionsBuilder<DataContext>()
                                                   .UseSqlite("Filename=Test.db")
                                                    .Options)
        {
        }

        [Fact]
        public async Task Get_Users_By_Gender_Returns_Persons_Sorted_By_Gender()
        {
            using (var context = new DataContext(ContextOptions))
            {
                var loggerMock = new Mock<ILogger<PersonController>>();
                var controller = new PersonController(context, loggerMock.Object);

                var items = await controller.GetPeopleByGender();
                List<Person> personList = (List<Person>)items.Value;

                ///sanity check the sorting, first and last 3 entries
                Assert.Equal(30, personList.Count);
                Assert.Equal("Anderson", personList[0].LastName);
                Assert.Equal("Female", personList[0].Gender);

                Assert.Equal("Brown", personList[1].LastName);
                Assert.Equal("Female", personList[1].Gender);

                Assert.Equal("Davis", personList[2].LastName);
                Assert.Equal("Female", personList[2].Gender);

                Assert.Equal("Moore", personList[27].LastName);
                Assert.Equal("Male", personList[27].Gender);

                Assert.Equal("Rodriguez", personList[28].LastName);
                Assert.Equal("Male", personList[28].Gender);

                Assert.Equal("Wilson", personList[29].LastName);
                Assert.Equal("Male", personList[29].Gender);
            }
        }

        [Fact]
        public async Task Get_Users_By_Birthdate_Returns_Persons_Sorted_By_Birthdate()
        {
            using (var context = new DataContext(ContextOptions))
            {
                var loggerMock = new Mock<ILogger<PersonController>>();
                var controller = new PersonController(context, loggerMock.Object);

                var items = await controller.GetPeopleByBirthdate();
                List<Person> personList = (List<Person>)items.Value;

                ///sanity check the sorting, first and last 3 entries
                Assert.Equal(30, personList.Count);
                Assert.Equal("6/20/1941", personList[0].DateOfBirth.ToShortDateString());
                Assert.Equal("3/1/1945", personList[1].DateOfBirth.ToShortDateString());
                Assert.Equal("5/1/1947", personList[2].DateOfBirth.ToShortDateString());
                Assert.Equal("10/16/1995", personList[27].DateOfBirth.ToShortDateString());
                Assert.Equal("11/12/1995", personList[28].DateOfBirth.ToShortDateString());
                Assert.Equal("7/26/1996", personList[29].DateOfBirth.ToShortDateString());
            }
        }

        [Fact]
        public async Task Get_Users_By_LastName_Returns_Persons_Sorted_By_LastName()
        {
            using (var context = new DataContext(ContextOptions))
            {
                var loggerMock = new Mock<ILogger<PersonController>>();
                var controller = new PersonController(context, loggerMock.Object);

                var items = await controller .GetPeopleByLastName();
                List<Person> personList = (List<Person>)items.Value;

                ///sanity check the sorting, first and last 3 entries
                Assert.Equal(30, personList.Count);
                Assert.Equal("Wilson", personList[0].LastName);
                Assert.Equal("Jackson", personList[0].FirstName);

                Assert.Equal("Wilson", personList[1].LastName);
                Assert.Equal("Emma", personList[1].FirstName);

                Assert.Equal("Williams", personList[2].LastName);
                Assert.Equal("Mila", personList[2].FirstName);

                Assert.Equal("Brown", personList[27].LastName);
                Assert.Equal("Luna", personList[27].FirstName);

                Assert.Equal("Anderson", personList[28].LastName);
                Assert.Equal("Olivia", personList[28].FirstName);

                Assert.Equal("Anderson", personList[29].LastName);
                Assert.Equal("Henry", personList[29].FirstName);
            }
        }

        [Fact]
        public async Task Create_Record_Returns_Created_Person()
        {
            using (var context = new DataContext(ContextOptions))
            {
                var loggerMock = new Mock<ILogger<PersonController>>();
                var controller = new PersonController(context, loggerMock.Object);
                string person = "Test, Person, Male, Red, 1/5/1960";

                var items = await controller.CreateRecord(person);
                Person pers = items.Value;

                ///return value should be the person object we tried to create
                Assert.Equal("Test", pers.LastName);
                Assert.Equal("Person", pers.FirstName);
                Assert.Equal("Male", pers.Gender);
                Assert.Equal("Red", pers.FavoriteColor);
                Assert.Equal("1/5/1960", pers.DateOfBirth.ToShortDateString());
            }
        }

        [Fact]
        public async Task Create_Record_With_Bad_Formatting_Returns_Bad_Request()
        {
            using (var context = new DataContext(ContextOptions))
            {
                var loggerMock = new Mock<ILogger<PersonController>>();
                var controller = new PersonController(context, loggerMock.Object);
                string person = "Test ------- Person, Male, Red [][][[[] 1/5/1960";

                var items = await controller.CreateRecord(person);
                var pers = items.Value;

                ///Our return value on a BadRequest should be null
                Assert.Null(pers);
            }
        }
    }
}
