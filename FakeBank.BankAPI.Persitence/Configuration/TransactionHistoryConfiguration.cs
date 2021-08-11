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
    public class TransactionHistoryConfiguration : IEntityTypeConfiguration<TransactionHistory>
    {
        public void Configure(EntityTypeBuilder<TransactionHistory> builder)
        {
            builder.Property(e => e.PaymentReference)
                    .IsRequired()
                    .HasMaxLength(75);

            builder.Property(e => e.TransactionRemarks)
                    .HasMaxLength(150);

            builder.Property(e => e.BeneficiaryAccountName)
                    .IsRequired()
                    .HasMaxLength(255);

            builder.Property(e => e.BeneficiaryAccountNumber)
                    .IsRequired();

            builder.Property(e => e.TransactionAmount)
                    .IsRequired();
        }
    }
}
