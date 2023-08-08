using AutoMapper;
using AutoMapper.QueryableExtensions;
using Finances.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Finances.Services.IncomeCategories.Queries.GetAll
{
    public class GetIncomeCategoriesListQuery : IRequest<IncomeCategoriesListVm>
    {
        public string UserId { get; set; } = default!;
    }

    public class GetAllIncomeCategoriesListQueryHandler : IRequestHandler<GetIncomeCategoriesListQuery, IncomeCategoriesListVm>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetAllIncomeCategoriesListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IncomeCategoriesListVm> Handle(GetIncomeCategoriesListQuery request, CancellationToken cancellationToken)
        {
            return new IncomeCategoriesListVm
            {
                List = await context.IncomeCategories
                    .Where(ic => ic.UserId == request.UserId)
                    .OrderBy(ec => ec.TypeId)
                    .ThenBy(ec => ec.Name)
                    .ProjectTo<IncomeCategoryDto>(mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken)
            };            
        }
    }
}
