using AutoMapper;
using Finances.Models;

namespace Finances.Services.Incomes.Queries.GetAllIncomes
{
    public class IncomeDto
    {
        public int Id { get; set; }

        public string Merchant { get; set; } = default!;

        public string Date { get; set; } = default!;

        public decimal Total { get; set; }

        public string Category { get; set; } = default!;

        public string Note { get; set; } = default!;

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Income, IncomeDto>()
                    .ForMember(x => x.Date, y => y.MapFrom(src => src.Date.ToShortDateString()))
                    .ForMember(x => x.Category, y => y.MapFrom(src => src.Category.Name));
            }
        }
    }
}