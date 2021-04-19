using GuaranteedRateHomeworkAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace GRUnitTests.API
{
    public class EmptyServerSeedTests : PersonControllerEmptyServerTestClass
    {
        public EmptyServerSeedTests() : base(new DbContextOptionsBuilder<DataContext>()
                                                   .UseSqlite("Filename=Test.db")
                                                    .Options)
        { }

        [Fact]
        public async Task Seed_On_Empty_Db_Returns_False()
        {
            using (var context = new DataContext(ContextOptions))
            {
                var success = await Seed.SeedPeople(context, "TestData/TestSeedData.json");

                Assert.True(success);
            }
        }
    }
}
