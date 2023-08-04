﻿using Finances.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finance.Persistence.Configurations
{
    public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Merchant)
                   .HasMaxLength(50);

            builder.Property(e => e.Note)
                   .HasMaxLength(200);

            builder.Property(e => e.Date)
                   .IsRequired();

            builder.Property(e => e.CategoryId)
                   .IsRequired();

            builder.Property(e => e.UserId)
                   .IsRequired();

            builder.Property(e => e.Total)
                .HasColumnType("decimal(16, 2)");
        }
    }
}
