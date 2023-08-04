namespace Finances.Services.IncomeCategories.Queries.GetIncomesByCategory
{
    public class IncomesByCategoryListVm
    {
        public IList<IncomeByCategoryDto> IncomeCategories { get; set; }

        public decimal Totals { get; set; }
    }

    public class IncomeByCategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public int TypeId { get; set; }

        public string TypeDescription { get; set; } = default!;

        public decimal Sum { get; set; }
    }
}
