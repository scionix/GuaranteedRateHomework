using GuaranteedRateHomework;
using Microsoft.EntityFrameworkCore;

namespace GuaranteedRateHomeworkAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Person> People { get; set; }
    }
}
