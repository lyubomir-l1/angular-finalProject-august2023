namespace Finances.Models
{
    public class Income
    {
        public int Id { get; set; }

        public string? Merchant { get; set; }

        public DateTime Date { get; set; }

        public string? Note { get; set; }

        public decimal Total { get; set; }

        public int CategoryId { get; set; }

        public virtual IncomeCategory Category { get; set; } = default!;

        public string UserId { get; set; } = default!;

        public virtual ApplicationUser User { get; set; } = default!;
    }
}
