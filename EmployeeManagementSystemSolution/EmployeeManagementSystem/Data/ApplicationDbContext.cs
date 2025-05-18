using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Helpers;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
             : base(options)
        { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<JobPosition> JobPositions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Atur value converter untuk properti DateOfBirth (enkripsi & dekripsi)
            modelBuilder.Entity<Employee>()
                .Property(e => e.DateOfBirth)
                .HasConversion(
                    v => EncryptionHelper.EncryptDate(v),
                    v => EncryptionHelper.DecryptDate(v)
                );
        }
    }
}
