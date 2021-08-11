using FakeBank.BankAPI.Domain.Common;
using FakeBank.BankAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FakeBank.BankAPI.Persitence
{
    public class FakeBankDbContext : DbContext
    {
        public FakeBankDbContext(DbContextOptions<FakeBankDbContext> options)
                : base(options)
        {
        }

        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<TransactionHistory> TransactionHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FakeBankDbContext).Assembly);

            modelBuilder.Entity<Customers>().HasData(new Customers
            {
                CustomersID = 1,
                CustomerName = "Arisha Barron"
            });

            modelBuilder.Entity<Customers>().HasData(new Customers
            {
                CustomersID = 2,
                CustomerName = "Branden Gibson"
            });

            modelBuilder.Entity<Customers>().HasData(new Customers
            {
                CustomersID = 3,
                CustomerName = "Rhonda Church"
            });

            modelBuilder.Entity<Customers>().HasData(new Customers
            {
                CustomersID = 4,
                CustomerName = "Georgina Hazel"
            });
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
