using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence.Entities;

namespace Infrastructure.Persistence.Context
{
    public partial class TestDbContext : DbContext
    {
        public TestDbContext()
        {
        }

        public TestDbContext(DbContextOptions<TestDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }

    }
}
