namespace Finances.Services.Incomes.Queries.GetTotalIncomesByYear
{
    public class IncomesByYearListVm
    {
        public IList<IncomeByYearDto> IncomeSums { get; set; }

        public decimal Totals { get; set; }
    }
}
