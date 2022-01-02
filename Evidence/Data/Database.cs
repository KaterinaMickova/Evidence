using Evidence.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evidence.Data
{
    public class Database: DbContext
    {
            public DbSet<Customer> Customers { get; set; }
            public DbSet<Insurance> Insurances { get; set; }

        public Database(DbContextOptions<Database> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CustomerInsurance>().HasKey(ci => new
            {
                ci.CustomerId,
                ci.InsuranceId
            });

            modelBuilder.Entity<CustomerInsurance>()
                    .HasOne(ci => ci.Customers)
                    .WithMany(c => c.CustomerInsurances)
                    .HasForeignKey(ci => ci.CustomerId);

            modelBuilder.Entity<CustomerInsurance>()
                   .HasOne(ci => ci.Insurances)
                   .WithMany(i => i.CustomerInsurances)
                   .HasForeignKey(ci => ci.InsuranceId);
        }

    
    }
}
