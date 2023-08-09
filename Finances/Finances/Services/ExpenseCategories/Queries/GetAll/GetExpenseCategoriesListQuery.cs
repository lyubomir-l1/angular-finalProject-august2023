using AutoMapper;
using AutoMapper.QueryableExtensions;
using Finances.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Finances.Services.ExpenseCategories.Queries.GetAll
{
    public class GetExpenseCategoriesListQuery : IRequest<ExpenseCategoriesListVm>
    {
        public string UserId { get; set; } = default!;
    }

    public class GetExpenseCategoriesListQueryHandler : IRequestHandler<GetExpenseCategoriesListQuery, ExpenseCategoriesListVm>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetExpenseCategoriesListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ExpenseCategoriesListVm> Handle(GetExpenseCategoriesListQuery request, CancellationToken cancellationToken)
        {
            return new ExpenseCategoriesListVm
            {
                List = await context.ExpenseCategories
                    .Where(ec => ec.UserId == request.UserId)
                    .OrderBy(ec => ec.TypeId)
                    .ThenBy(ec => ec.Name)
                    .ProjectTo<ExpenseCategoryDto>(mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken)
            };

        }
    }
}
