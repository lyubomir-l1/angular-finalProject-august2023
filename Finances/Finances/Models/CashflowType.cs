namespace Finances.Models
{
    public class CashflowType
    {
        public CashflowType()
        {
            ExpenseCategories = new HashSet<ExpenseCategory>();
            IncomeCategories = new HashSet<IncomeCategory>();
        }

        public int Id { get; set; }

        public string? Description { get; set; }

        public virtual ICollection<ExpenseCategory> ExpenseCategories { get; private set; }

        public virtual ICollection<IncomeCategory> IncomeCategories { get; private set; }
    }
}
