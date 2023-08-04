using Finances.Models;
using Microsoft.EntityFrameworkCore;

namespace Finances.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Income> Incomes { get; set; }

        DbSet<Expense> Expenses { get; set; }

        DbSet<IncomeCategory> IncomeCategories { get; set; }

        DbSet<ExpenseCategory> ExpenseCategories { get; set; }

        DbSet<CashflowType> CashflowTypes { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
