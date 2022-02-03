using System;
using Microsoft.EntityFrameworkCore;
using SignalRLiveDataProject.Entities;

namespace SignalRLiveDataProject.Data
{
	public class AppDbContext:DbContext
	{
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost,1433;Database=SignalRLiveDataAppDB;User Id=SA;Password={password};");
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Employee>()
        //        .HasMany(e => e.Sales)
        //        .WithOne(s => s.Employee);
        //}
        
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}

