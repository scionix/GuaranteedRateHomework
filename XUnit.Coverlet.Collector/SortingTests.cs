using GuaranteedRateHomework;
using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTests
{
    public class SortingTests
    {
        [Fact]
        public void GenderSort_Puts_Females_First()
        {
            List<Person> people = new List<Person>();

            people.Add(new Person { LastName = "Doe", FirstName = "John", Gender = "Male", FavoriteColor = "Blue", DateOfBirth = DateTime.Parse("1/1/1950") });
            people.Add(new Person { LastName = "Smith", FirstName = "Jane", Gender = "Female", FavoriteColor = "Green", DateOfBirth = DateTime.Parse("1/1/1960") });
            people.Add(new Person { LastName = "Burtold", FirstName = "Bill", Gender = "Male", FavoriteColor = "Red", DateOfBirth = DateTime.Parse("1/1/1970") });
            people.Add(new Person { LastName = "Gracie", FirstName = "Paula", Gender = "Female", FavoriteColor = "Purple", DateOfBirth = DateTime.Parse("1/1/1980") });

            people = (List<Person>)Sorting.GenderSort(people);

            ///order should be: 
            ///1. Paula Gracie
            ///2. Jane Smith
            ///3. Bill Burtold
            ///4. John Doe
            Assert.Equal("Gracie", people[0].LastName);
            Assert.Equal("Smith", people[1].LastName);
            Assert.Equal("Burtold", people[2].LastName);
            Assert.Equal("Doe", people[3].LastName);
        }

        [Fact]
        public void GenderSort_Puts_Females_First_LastName_TieBreaker()
        {
            List<Person> people = new List<Person>();

            people.Add(new Person { LastName = "Doe", FirstName = "John", Gender = "Male", FavoriteColor = "Blue", DateOfBirth = DateTime.Parse("1/1/1950") });
            people.Add(new Person { LastName = "Smith", FirstName = "Jane", Gender = "Female", FavoriteColor = "Green", DateOfBirth = DateTime.Parse("1/1/1960") });
            people.Add(new Person { LastName = "Burtold", FirstName = "Bill", Gender = "Male", FavoriteColor = "Red", DateOfBirth = DateTime.Parse("1/1/1970") });
            people.Add(new Person { LastName = "Gracie", FirstName = "Paula", Gender = "Female", FavoriteColor = "Purple", DateOfBirth = DateTime.Parse("1/1/1980") });
            people.Add(new Person { LastName = "Gracie", FirstName = "Anna", Gender = "Female", FavoriteColor = "Purple", DateOfBirth = DateTime.Parse("1/1/1980") });

            people = (List<Person>)Sorting.GenderSort(people);

            ///order should be: 
            ///1. Anna Gracie
            ///2. Paula Gracie
            ///3. Jane Smith
            ///4. Bill Burtold
            ///5. John Doe
            Assert.Equal("Anna", people[0].FirstName);
            Assert.Equal("Paula", people[1].FirstName);
            Assert.Equal("Paula", people[1].FirstName);
            Assert.Equal("Smith", people[2].LastName);
            Assert.Equal("Burtold", people[3].LastName);
            Assert.Equal("Doe", people[4].LastName);
        }

        [Fact]
        public void BirthdateSort_Orders_DateOfBirth_Ascending()
        {
            List<Person> people = new List<Person>();

            people.Add(new Person { LastName = "Doe", FirstName = "John", Gender = "Male", FavoriteColor = "Blue", DateOfBirth = DateTime.Parse("1/1/1950") });
            people.Add(new Person { LastName = "Smith", FirstName = "Jane", Gender = "Female", FavoriteColor = "Green", DateOfBirth = DateTime.Parse("1/1/1960") });
            people.Add(new Person { LastName = "Burtold", FirstName = "Bill", Gender = "Male", FavoriteColor = "Red", DateOfBirth = DateTime.Parse("1/1/1970") });
            people.Add(new Person { LastName = "Gracie", FirstName = "Paula", Gender = "Female", FavoriteColor = "Purple", DateOfBirth = DateTime.Parse("1/1/1980") });

            people = (List<Person>)Sorting.BirthdateSort(people);

            ///order should be: 
            ///1. John Doe
            ///2. Jane Smith
            ///3. Bill Burtold
            ///4. Paula Gracie
            Assert.Equal("Doe", people[0].LastName);
            Assert.Equal("Smith", people[1].LastName);
            Assert.Equal("Burtold", people[2].LastName);
            Assert.Equal("Gracie", people[3].LastName);
        }

        [Fact]
        public void BirthdateSort_Orders_DateOfBirth_Ascending_LastName_TieBreaker()
        {
            List<Person> people = new List<Person>();

            people.Add(new Person { LastName = "Doe", FirstName = "John", Gender = "Male", FavoriteColor = "Blue", DateOfBirth = DateTime.Parse("1/1/1950") });
            people.Add(new Person { LastName = "Smith", FirstName = "Jane", Gender = "Female", FavoriteColor = "Green", DateOfBirth = DateTime.Parse("1/1/1950") });
            people.Add(new Person { LastName = "Burtold", FirstName = "Bill", Gender = "Male", FavoriteColor = "Red", DateOfBirth = DateTime.Parse("1/1/1970") });
            people.Add(new Person { LastName = "Gracie", FirstName = "Paula", Gender = "Female", FavoriteColor = "Purple", DateOfBirth = DateTime.Parse("1/1/1980") });

            people = (List<Person>)Sorting.BirthdateSort(people);

            ///order should be: 
            ///1. John Doe
            ///2. Jane Smith
            ///3. Bill Burtold
            ///4. Paula Gracie
            Assert.Equal("Doe", people[0].LastName);
            Assert.Equal("Smith", people[1].LastName);
            Assert.Equal("Burtold", people[2].LastName);
            Assert.Equal("Gracie", people[3].LastName);
        }

        [Fact]
        public void BirthdateSort_Orders_DateOfBirth_Ascending_FirstName_TieBreaker()
        {
            List<Person> people = new List<Person>();

            people.Add(new Person { LastName = "Smith", FirstName = "John", Gender = "Male", FavoriteColor = "Blue", DateOfBirth = DateTime.Parse("1/1/1950") });
            people.Add(new Person { LastName = "Smith", FirstName = "Jane", Gender = "Female", FavoriteColor = "Green", DateOfBirth = DateTime.Parse("1/1/1950") });
            people.Add(new Person { LastName = "Burtold", FirstName = "Bill", Gender = "Male", FavoriteColor = "Red", DateOfBirth = DateTime.Parse("1/1/1970") });
            people.Add(new Person { LastName = "Gracie", FirstName = "Paula", Gender = "Female", FavoriteColor = "Purple", DateOfBirth = DateTime.Parse("1/1/1980") });

            people = (List<Person>)Sorting.BirthdateSort(people);

            ///order should be: 
            ///1. Jane Smith
            ///2. John Smith
            ///3. Bill Burtold
            ///4. Paula Gracie
            Assert.Equal("Jane", people[0].FirstName);
            Assert.Equal("John", people[1].FirstName);
            Assert.Equal("Burtold", people[2].LastName);
            Assert.Equal("Gracie", people[3].LastName);
        }

        [Fact]
        public void LastNameSort_Orders_LastName_Descending()
        {
            List<Person> people = new List<Person>();

            people.Add(new Person { LastName = "Doe", FirstName = "John", Gender = "Male", FavoriteColor = "Blue", DateOfBirth = DateTime.Parse("1/1/1950") });
            people.Add(new Person { LastName = "Smith", FirstName = "Jane", Gender = "Female", FavoriteColor = "Green", DateOfBirth = DateTime.Parse("1/1/1950") });
            people.Add(new Person { LastName = "Burtold", FirstName = "Bill", Gender = "Male", FavoriteColor = "Red", DateOfBirth = DateTime.Parse("1/1/1970") });
            people.Add(new Person { LastName = "Gracie", FirstName = "Paula", Gender = "Female", FavoriteColor = "Purple", DateOfBirth = DateTime.Parse("1/1/1980") });

            people = (List<Person>)Sorting.LastnameSort(people);

            ///order should be: 
            ///1. Jane Smith
            ///2. Paula Gracie
            ///3. John Doe
            ///4. Bill Burtold
            Assert.Equal("Smith", people[0].LastName);
            Assert.Equal("Gracie", people[1].LastName);
            Assert.Equal("Doe", people[2].LastName);
            Assert.Equal("Burtold", people[3].LastName);
        }

        [Fact]
        public void LastNameSort_Orders_LastName_Descending_FirstName_Tiebreaker()
        {
            List<Person> people = new List<Person>();

            people.Add(new Person { LastName = "Smith", FirstName = "John", Gender = "Male", FavoriteColor = "Blue", DateOfBirth = DateTime.Parse("1/1/1950") });
            people.Add(new Person { LastName = "Smith", FirstName = "Jane", Gender = "Female", FavoriteColor = "Green", DateOfBirth = DateTime.Parse("1/1/1950") });
            people.Add(new Person { LastName = "Burtold", FirstName = "Bill", Gender = "Male", FavoriteColor = "Red", DateOfBirth = DateTime.Parse("1/1/1970") });
            people.Add(new Person { LastName = "Gracie", FirstName = "Paula", Gender = "Female", FavoriteColor = "Purple", DateOfBirth = DateTime.Parse("1/1/1980") });

            people = (List<Person>)Sorting.LastnameSort(people);

            ///order should be: 
            ///1. John Smith
            ///2. Jane Smith
            ///3. Paula Gracie
            ///4. Bill Burtold
            Assert.Equal( "John", people[0].FirstName);
            Assert.Equal( "Jane", people[1].FirstName);
            Assert.Equal("Gracie", people[2].LastName);
            Assert.Equal("Burtold", people[3].LastName);
        }
    }
}