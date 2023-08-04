namespace Finances.Models
{
    public class ExpenseCategory
    {
        public ExpenseCategory()
        {
            Expenses = new HashSet<Expense>();
        }

        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string UserId { get; set; } = default!;

        public virtual ApplicationUser User { get; set; } = default!;

        public int TypeId { get; set; }

        public virtual CashflowType Type { get; set; } = default!;

        public virtual ICollection<Expense> Expenses { get; private set; }
    }
}
