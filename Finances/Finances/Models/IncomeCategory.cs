namespace Finances.Models
{
    public class IncomeCategory
    {
        public IncomeCategory()
        {
            Incomes = new HashSet<Income>();
        }

        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string UserId { get; set; } = default!;

        public virtual ApplicationUser User { get; set; } = default!;

        public int TypeId { get; set; }

        public virtual CashflowType Type { get; set; } = default!;

        public virtual ICollection<Income> Incomes { get; private set; }
    }
}
