using Finances.Common.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Finances.Services.Incomes.Queries.GetTotalIncomesByYear
{
    public class GetIncomesByYearListQuery : IRequest<IncomesByYearListVm>
    {
        public int Year { get; set; }

        public string UserId { get; set; } = default!;
    }

    public class GetIncomesByYearListQueryValidator : AbstractValidator<GetIncomesByYearListQuery>
    {
        public GetIncomesByYearListQueryValidator()
        {
            RuleFor(e => e.Year)
                .GreaterThan(2020)
                .LessThanOrEqualTo(2120)
                .WithMessage("Invalid year");


            RuleFor(e => e.UserId)
                .NotEmpty()
                .WithMessage("User Id is required");
        }
    }

    public class GetIncomesByYearListQueryHandler : IRequestHandler<GetIncomesByYearListQuery, IncomesByYearListVm>
    {
        private readonly IApplicationDbContext context;

        public GetIncomesByYearListQueryHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IncomesByYearListVm> Handle(GetIncomesByYearListQuery request, CancellationToken cancellationToken)
        {
            var incomes = await context.Incomes
                    .Where(e => e.Date.Year == request.Year && e.UserId == request.UserId)
                    .GroupBy(e => new {
                        Month = e.Date.Month
                    })
                    .Select(eg => new IncomeByYearDto
                    {
                        Month = eg.Key.Month,
                        Sum = eg.Sum(e => e.Total)
                    })
                    .OrderBy(e => e.Month)
                    .ToListAsync(cancellationToken);

            var totalIncomes = incomes.Sum(e => e.Sum);

            var fullIncomes = new List<IncomeByYearDto>();

            for (int i = 1; i <= 12; i++)
            {
                var income = incomes.FirstOrDefault(income => income.Month == i);

                if (income != null)
                {
                    fullIncomes.Add(income);
                }
                else
                {
                    var zeroIncome = new IncomeByYearDto
                    {
                        Month = i,
                        Sum = 0.00M
                    };

                    fullIncomes.Add(zeroIncome);
                }
            }

            return new IncomesByYearListVm
            {
                IncomeSums = fullIncomes,
                Totals = totalIncomes
            };
        }
    }
}
