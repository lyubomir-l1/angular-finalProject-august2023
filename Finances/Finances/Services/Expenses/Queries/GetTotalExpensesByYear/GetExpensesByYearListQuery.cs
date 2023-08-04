using Finances.Common.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Finances.Services.Expenses.Queries.GetTotalExpensesByYear
{
    public class GetExpensesByYearListQuery : IRequest<ExpensesByYearListVm>
    {
        public int Year { get; set; }

        public string UserId { get; set; } = default!;
    }

    public class GetExpensesByYearListQueryValidator : AbstractValidator<GetExpensesByYearListQuery>
    {
        public GetExpensesByYearListQueryValidator()
        {
            RuleFor(e => e.Year)
                .GreaterThan(2020)
                .LessThanOrEqualTo(2120)
                .WithMessage("Invalid year");


            RuleFor(e => e.UserId)
                .NotEmpty()
                .WithMessage("User is required");
        }
    }

    public class GetExpensesByYearListQueryHandler : IRequestHandler<GetExpensesByYearListQuery, ExpensesByYearListVm>
    {
        private readonly IApplicationDbContext context;

        public GetExpensesByYearListQueryHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<ExpensesByYearListVm> Handle(GetExpensesByYearListQuery request, CancellationToken cancellationToken)
        {
            var expenses = await context.Expenses
                    .Where(e => e.Date.Year == request.Year && e.UserId == request.UserId)
                    .GroupBy(e => new {
                        Month = e.Date.Month
                    })
                    .Select(eg => new ExpenseByYearDto
                    {
                        Month = eg.Key.Month,
                        Sum = eg.Sum(e => e.Total)
                    })
                    .OrderBy(e => e.Month)
                    .ToListAsync(cancellationToken);

            var totalExpenses = expenses.Sum(e => e.Sum);

            var fullExpenses = new List<ExpenseByYearDto>();

            for (int i = 1; i <= 12; i++)
            {
                var expense = expenses.FirstOrDefault(income => income.Month == i);

                if (expense != null)
                {
                    fullExpenses.Add(expense);
                }
                else
                {
                    var zeroExpense = new ExpenseByYearDto
                    {
                        Month = i,
                        Sum = 0.00M
                    };

                    fullExpenses.Add(zeroExpense);
                }
            }

            return new ExpensesByYearListVm
            {
                ExpenseSums = fullExpenses,
                Totals = totalExpenses
            };
        }
    }
}
