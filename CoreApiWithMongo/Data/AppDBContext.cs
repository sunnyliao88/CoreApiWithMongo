using CoreApiWithMongo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiWithMongo.Data
{
    public class AppDBContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, DepartmentName = "IT" },
                new Department { Id = 2, DepartmentName = "HR" }
                );
            modelBuilder.Entity<Employee>().HasData(
                new Employee { ID = 1, DepartmentId = 1, Email = "n1@a.com", Name = "n1" },
                new Employee { ID = 2, DepartmentId = 2, Email = "n2@a.com", Name = "n2" }
                );

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
