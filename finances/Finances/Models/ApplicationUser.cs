using Microsoft.AspNetCore.Identity;

namespace Finances.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Expenses = new HashSet<Expense>();
            this.Incomes = new HashSet<Income>();
            this.ExpenseCategories = new HashSet<ExpenseCategory>();
            this.IncomeCategories = new HashSet<IncomeCategory>();
        }

        public ICollection<Expense> Expenses { get; private set; }

        public ICollection<Income> Incomes { get; private set; }

        public ICollection<ExpenseCategory> ExpenseCategories { get; private set; }

        public ICollection<IncomeCategory> IncomeCategories { get; private set; }
    }
}