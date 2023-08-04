using AutoMapper;
using AutoMapper.QueryableExtensions;
using Finances.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Finances.Services.CashflowTypes.Queries.GetAll
{
    public class GetlCashflowTypesListQuery : IRequest<List<CashflowTypesVm>>
    {
    }

    public class GetCashflowTypesListQueryHandler : IRequestHandler<GetlCashflowTypesListQuery, List<CashflowTypesVm>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetCashflowTypesListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<CashflowTypesVm>> Handle(GetlCashflowTypesListQuery request, CancellationToken cancellationToken)
        {
            return await context.CashflowTypes.ProjectTo<CashflowTypesVm>(mapper.ConfigurationProvider).ToListAsync(cancellationToken);           
        }
    }
}
