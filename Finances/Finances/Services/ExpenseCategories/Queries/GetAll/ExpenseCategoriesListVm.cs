using AutoMapper;
using Finances.Common.Mapping;
using Finances.Models;
using Finances.Services.ExpenseCategories.Queries.GetAll;

namespace Finances.Services.ExpenseCategories.Queries.GetAll
{
    public class ExpenseCategoriesListVm : IMapFrom<ExpenseCategory>
    {
        public IList<ExpenseCategoryDto> List { get; set; }
    }
}
