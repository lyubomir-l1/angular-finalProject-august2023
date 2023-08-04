namespace Finances.Services.Incomes.Queries.GetExpenseById
{
    using AutoMapper;
    using Finances.Common.Mapping;
    using Finances.Models;
    using System;

    public class IncomeVm : IMapFrom<Income>
    {
        public int Id { get; set; }

        public string Merchant { get; set; } = default!;

        public DateTime Date { get; set; }

        public decimal Total { get; set; }

        public string Note { get; set; } = default!;

        public string CategoryId { get; set; } = default!;

        public string CategoryName { get; set; } = default!;

        public void Mapping(Profile profile)
            => profile.CreateMap<Income, IncomeVm>()
                      .ForMember(d => d.CategoryName, opt => opt.MapFrom(s => s.Category.Name));
    }
}
