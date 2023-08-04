using AutoMapper;
using AutoMapper.QueryableExtensions;
using Finances.Common.Exceptions;
using Finances.Common.Interfaces;
using Finances.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Finances.Services.Incomes.Queries.GetExpenseById
{
    public class GetIncomeByIdQuery : IRequest<IncomeVm>
    {
        public int Id { get; set; }
    }

    public class GetIncomeByIdQueryHandler : IRequestHandler<GetIncomeByIdQuery, IncomeVm>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetIncomeByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IncomeVm> Handle(GetIncomeByIdQuery request, CancellationToken cancellationToken)
        {
            var income = await context
                .Incomes
                .Include(c => c.Category)
                .Where(x => x.Id == request.Id)
                .ProjectTo<IncomeVm>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();


            if (income == null)
            {
                throw new NotFoundException(nameof(Income), request.Id);
            }

            return income;
        }
    }
}
