using AutoMapper;
using Finances.Common.Mapping;
using Finances.Models;

namespace Finances.Services.IncomeCategories.Queries.GetAll
{
    public class IncomeCategoryVm : IMapFrom<IncomeCategory>
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public int TypeId { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<IncomeCategory, IncomeCategoryVm>();
        }
    }
}
