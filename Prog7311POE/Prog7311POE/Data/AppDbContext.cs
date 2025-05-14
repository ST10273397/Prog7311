using Microsoft.EntityFrameworkCore;
using Prog7311POE.Models;

namespace Prog7311POE.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Farmer> Farmers { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed an employee
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                Id = 1,
                FullName = "Aiden Evard",
                Email = "aiden@agri.com",
                Password = "EmployeePassword123"
            });

            // Seed a farmer tied to that employee
            modelBuilder.Entity<Farmer>().HasData(new Farmer
            {
                Id = 1,
                FullName = "Joe Herder",
                Email = "joe@farm.com",
                Password = "secret",
                EmployeeId = 1
            });

            // Seed a product tied to that farmer
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 1,
                Name = "Corn",
                Category = "Grain",
                productionDate = new DateTime(2025, 5, 14),
                FarmerId = 1
            });
        }

    }

}
