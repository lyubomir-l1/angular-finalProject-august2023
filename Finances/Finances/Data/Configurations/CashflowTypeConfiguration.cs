using Finances.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finances.Data.Configurations
{
    public class CashflowTypeConfiguration : IEntityTypeConfiguration<CashflowType>
    {
        public void Configure(EntityTypeBuilder<CashflowType> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasMany(c => c.ExpenseCategories)
                   .WithOne(ec => ec.Type)
                   .HasForeignKey(ec => ec.TypeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.IncomeCategories)
                   .WithOne(ic => ic.Type)
                   .HasForeignKey(ic => ic.TypeId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
