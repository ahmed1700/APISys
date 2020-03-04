using Entities.Configurations.Seed;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities.Data
{
    public class RepositryContext : DbContext
    {
        public RepositryContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companies { get; set; }
    }
}
