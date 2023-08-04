using System.Globalization;

namespace Finances.Services.Expenses.Queries.GetTotalExpensesByYear
{
    public class ExpenseByYearDto
    {
        public int Month { get; set; }

        public string MonthName
        {
            get
            {
                return CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(Month);
            }
        }

        public decimal Sum { get; set; }
    }
}
