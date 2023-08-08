using AutoMapper;
using Finances.Common.Mapping;
using Finances.Models;

namespace Finances.Services.IncomeCategories.Queries.GetAll
{
    public class IncomeCategoryDto : IMapFrom<IncomeCategory>
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public int TypeId { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<IncomeCategory, IncomeCategoryDto>();
            }
        }
    }
}
