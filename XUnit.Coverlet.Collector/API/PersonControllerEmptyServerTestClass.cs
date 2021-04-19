using GuaranteedRateHomeworkAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace GRUnitTests.API
{
    public class PersonControllerEmptyServerTestClass
    {
        protected PersonControllerEmptyServerTestClass(DbContextOptions<DataContext> context)
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
            }
        }

    }
}