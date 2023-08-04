using AutoMapper;
using AutoMapper.QueryableExtensions;
using Finances.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Finances.Services.ExpenseCategories.Queries.GetAll
{
    public class GetExpenseCategoriesListQuery : IRequest<List<ExpenseCategoriesListVm>>
    {
        public string UserId { get; set; } = default!;
    }

    public class GetExpenseCategoriesListQueryHandler : IRequestHandler<GetExpenseCategoriesListQuery, List<ExpenseCategoriesListVm>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetExpenseCategoriesListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<ExpenseCategoriesListVm>> Handle(GetExpenseCategoriesListQuery request, CancellationToken cancellationToken)
        {
            return await context.ExpenseCategories
                    .Where(ec => ec.UserId == request.UserId)
                    .OrderBy(ec => ec.TypeId)
                    .ThenBy(ec => ec.Name)
                    .ProjectTo<ExpenseCategoriesListVm>(mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
        }
    }
}
