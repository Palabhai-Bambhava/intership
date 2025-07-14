using AuthWebApiDemo.Entities;
using Microsoft.EntityFrameworkCore;


namespace AuthWebApiDemo.Data
{
    public class MyDbContext : DbContext  // ✅ Inherit from DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options):base() 
        { 
        }

        public MyDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } // ✅ D should be capital: DbSet
    }
}
