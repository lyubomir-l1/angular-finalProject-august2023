using Finances.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finance.Persistence.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(f => f.Id);

            builder.HasMany(f => f.Incomes)
                   .WithOne(i => i.User)
                   .HasForeignKey(i => i.UserId);

            builder.HasMany(f => f.Expenses)
                   .WithOne(i => i.User)
                   .HasForeignKey(i => i.UserId);

            builder.HasMany(f => f.IncomeCategories)
                   .WithOne(i => i.User)
                   .HasForeignKey(i => i.UserId);

            builder.HasMany(f => f.ExpenseCategories)
                   .WithOne(i => i.User)
                   .HasForeignKey(i => i.UserId);
        }
    }
}
