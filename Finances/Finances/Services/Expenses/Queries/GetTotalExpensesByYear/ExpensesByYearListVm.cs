namespace Finances.Services.Expenses.Queries.GetTotalExpensesByYear
{
    public class ExpensesByYearListVm
    {
        public IList<ExpenseByYearDto> ExpenseSums { get; set; }

        public decimal Totals { get; set; }
    }
}
