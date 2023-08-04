using System.Globalization;

namespace Finances.Services.Incomes.Queries.GetTotalIncomesByYear
{
    public class IncomeByYearDto
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
