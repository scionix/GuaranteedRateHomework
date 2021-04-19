using GuaranteedRateHomework;
using GuaranteedRateHomeworkAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace GRUnitTests.API
{
    public class PersonControllerBaseTestClass
    {
        protected PersonControllerBaseTestClass(DbContextOptions<DataContext> context)
        {
            ContextOptions = context;
            Seed();
        }

        protected DbContextOptions<DataContext> ContextOptions { get; }

        private void Seed()
        {
            using (var context = new DataContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var userData = File.ReadAllText("TestData/TestSeedData.json");
                var people = JsonSerializer.Deserialize<List<Person>>(userData);

                foreach (var person in people)
                {
                    context.Add(person);
                }

                context.SaveChanges();
            }
        }

    }
}