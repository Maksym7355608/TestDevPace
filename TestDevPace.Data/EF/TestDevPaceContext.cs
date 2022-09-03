using Microsoft.EntityFrameworkCore;
using TestDevPace.Data.Entities;

namespace TestDevPace.Data.EF
{
    public class TestDevPaceContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public TestDevPaceContext(DbContextOptions<TestDevPaceContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        public TestDevPaceContext()
        { }
    }
}
