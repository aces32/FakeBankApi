using FakeBank.BankAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBank.BankAPI.Persitence.Configuration
{
    public class AccountsConfiguration : IEntityTypeConfiguration<Accounts>
    {
        public void Configure(EntityTypeBuilder<Accounts> builder)
        {
            builder.Property(e => e.AccountNumber)
                .IsRequired()
                .HasMaxLength(10);
            builder.Property(e => e.AccountType)
                .IsRequired();
            builder.Property(e => e.LedgerBalance)
                .IsRequired();
            builder.Property(e => e.ClearedBalance)
                .IsRequired();
        }
    }
}
