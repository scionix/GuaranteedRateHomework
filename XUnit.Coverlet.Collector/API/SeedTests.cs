using GuaranteedRateHomeworkAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace GRUnitTests.API
{
    public class SeedTests : PersonControllerBaseTestClass
    {
        public SeedTests() : base(new DbContextOptionsBuilder<DataContext>()
                                                   .UseSqlite("Filename=Test.db")
                                                    .Options)
        { }

        [Fact]
        public async Task Seed_On_Populated_Db_Returns_False()
        {
            using (var context = new DataContext(ContextOptions))
            {
                var success = await Seed.SeedPeople(context, "TestData/TestSeedData.json");

                Assert.False(success);
            }
        }
    }
}
