using AutoMapper;
using Finances.Common.Mapping;
using Finances.Models;

namespace Finances.Services.ExpenseCategories.Queries.GetAll
{
    public class ExpenseCategoryDto : IMapFrom<ExpenseCategory>
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public int TypeId { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<ExpenseCategory, ExpenseCategoryDto>();
            }
        }
    }
}
