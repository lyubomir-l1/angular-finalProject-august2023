using AutoMapper;
using Finances.Common.Mapping;
using Finances.Models;

namespace Finances.Services.Expenses.Queries.GetExpenseById
{
    public class ExpenseVm : IMapFrom<Expense>
    {
        public int Id { get; set; }

        public string Merchant { get; set; } = default!;

        public DateTime Date { get; set; }

        public decimal Total { get; set; }

        public string Note { get; set; } = default!;

        public int CategoryId { get; set; } = default!;

        public string CategoryName { get; set; } = default!;

        public void Mapping(Profile profile)
            => profile.CreateMap<Expense, ExpenseVm>()
                      .ForMember(d => d.CategoryName, opt => opt.MapFrom(s => s.Category.Name));
    }
}
