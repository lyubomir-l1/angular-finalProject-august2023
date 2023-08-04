using AutoMapper;
using Finances.Common.Mapping;
using Finances.Models;

namespace Finances.Services.Expenses.Queries.GetAllExpenses
{
    public class ExpenseDto : IMapFrom<Expense>
    {
        public int Id { get; set; }

        public string Merchant { get; set; } = default!;

        public string Date { get; set; } = default!;

        public decimal Total { get; set; }

        public string Category { get; set; } = default!;

        public string Note { get; set; } = default!;

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Expense, ExpenseDto>()
                .ForMember(x => x.Date, y => y.MapFrom(src => src.Date.ToShortDateString()))
                .ForMember(x => x.Category, y => y.MapFrom(src => src.Category.Name));
        }
    }
}