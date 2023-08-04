using AutoMapper;
using AutoMapper.QueryableExtensions;
using Finances.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Finances.Services.IncomeCategories.Queries.GetAll
{
    public class GetIncomeCategoriesListQuery : IRequest<List<IncomeCategoryVm>>
    {
        public string UserId { get; set; } = default!;
    }

    public class GetAllIncomeCategoriesListQueryHandler : IRequestHandler<GetIncomeCategoriesListQuery, List<IncomeCategoryVm>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetAllIncomeCategoriesListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<IncomeCategoryVm>> Handle(GetIncomeCategoriesListQuery request, CancellationToken cancellationToken)
        {
            return await context.IncomeCategories
                    .Where(ic => ic.UserId == request.UserId)
                    .OrderBy(ec => ec.TypeId)
                    .ThenBy(ec => ec.Name)
                    .ProjectTo<IncomeCategoryVm>(mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
        }
    }
}
