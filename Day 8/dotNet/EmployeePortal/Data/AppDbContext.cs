using EmployeePortal.Models;
using EmployeePortal.Repository;
using Microsoft.EntityFrameworkCore;

namespace EmployeePortal.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<AppUser> Users { get; set; }
        //public object Users { get; internal set; }
    }
}
