using AutoMapper;
using AutoMapper.QueryableExtensions;
using Finances.Common.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Finances.Services.Expenses.Queries.GetAllExpenses
{
    public class GetExpensesListQuery : IRequest<List<ExpenseDto>>
    {
        public int Month { get; set; }

        public int Year { get; set; }

        public string UserId { get; set; } = default!;
    }

    public class GetExpensesListQueryValidator : AbstractValidator<GetExpensesListQuery>
    {
        public GetExpensesListQueryValidator()
        {
            RuleFor(e => e.Month)
                .GreaterThan(1)
                .LessThanOrEqualTo(12)
                .WithMessage("Invalid month");


            RuleFor(e => e.Year)
                .GreaterThan(2020)
                .LessThanOrEqualTo(2120)
                .WithMessage("Invalid year");


            RuleFor(e => e.UserId)
                .NotEmpty()
                .WithMessage("User Id is required                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      ");
        }
    }

    public class GetExpensesListQueryHandler : IRequestHandler<GetExpensesListQuery, List<ExpenseDto>>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetExpensesListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<ExpenseDto>> Handle(GetExpensesListQuery request, CancellationToken cancellationToken)
        {
            return await context.Expenses
                    .Where(x => x.Date.Month == request.Month && x.Date.Year == request.Year && x.UserId == request.UserId)
                    .OrderByDescending(x => x.Id)
                    .Include(x => x.Category)
                    .ProjectTo<ExpenseDto>(mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
        }
    }
}