using AutoMapper;
using AutoMapper.QueryableExtensions;
using Finances.Common.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Finances.Services.Incomes.Queries.GetAllIncomes
{
    public class GetIncomesListQuery : IRequest<List<IncomeDto>>
    {
        public int Month { get; set; }

        public int Year { get; set; }

        public string UserId { get; set; } = default!;
    }

    public class GetIncomesListQueryValidator : AbstractValidator<GetIncomesListQuery>
    {
        public GetIncomesListQueryValidator()
        {
            RuleFor(e => e.Month)
                .GreaterThan(0)
                .LessThanOrEqualTo(12)
                .WithMessage("Invalid month");


            RuleFor(e => e.Year)
                .GreaterThan(2020)
                .LessThanOrEqualTo(2120)
                .WithMessage("Invalid year");


            RuleFor(e => e.UserId)
                .NotEmpty()
                .WithMessage("User Id is required");
        }
    }

    public class GetAllIncomesListQueryHandler : IRequestHandler<GetIncomesListQuery, List<IncomeDto>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetAllIncomesListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<IncomeDto>> Handle(GetIncomesListQuery request, CancellationToken cancellationToken)
        {
            return await context.Incomes
                    .Where(x => x.Date.Month == request.Month && x.Date.Year == request.Year && x.UserId == request.UserId)
                    .OrderByDescending(x => x.Id)
                    .Include(x => x.Category)
                    .ProjectTo<IncomeDto>(mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
        }
    }
}