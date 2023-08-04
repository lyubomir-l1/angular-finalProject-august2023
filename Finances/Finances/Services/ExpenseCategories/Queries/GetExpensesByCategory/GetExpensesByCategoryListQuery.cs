using Finances.Common.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Finances.Services.ExpenseCategories.Queries.GetExpensesByCategory
{
    public class GetExpensesByCategoryListQuery : IRequest<ExpensesByCategoryListVm>
    {
        public int Month { get; set; }

        public int Year { get; set; }

        public string UserId { get; set; } = default!;
    }

    public class GetExpensesByCategoryListQueryValidator : AbstractValidator<GetExpensesByCategoryListQuery>
    {
        public GetExpensesByCategoryListQueryValidator()
        {
            RuleFor(e => e.Month)
                .GreaterThan(1)
                .LessThanOrEqualTo(12)
                .WithMessage(string.Format("Invalid month"));

            RuleFor(e => e.Year)
                .GreaterThan(2020)
                .LessThanOrEqualTo(2120)
                .WithMessage(string.Format("Invalid year"));

            RuleFor(e => e.UserId)
                .NotEmpty()
                .WithMessage(string.Format("User Id is required"));
        }
    }

    public class GetExpensesByCategoryListQueryHandler : IRequestHandler<GetExpensesByCategoryListQuery, ExpensesByCategoryListVm>
    {
        private readonly IApplicationDbContext context;

        public GetExpensesByCategoryListQueryHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<ExpensesByCategoryListVm> Handle(GetExpensesByCategoryListQuery request, CancellationToken cancellationToken)
        {
            var expenseCategories = await context.ExpenseCategories
                    .Include(ec => ec.Expenses)
                    .Include(ex => ex.Type)
                    .Where(ex => ex.UserId == request.UserId)
                    .Select(ec => new ExpenseByCategoryVm
                    {
                        Id = ec.Id,
                        Name = ec.Name,
                        TypeId = ec.TypeId,
                        TypeDescription = ec.Type.Description ?? string.Empty,
                        Sum = ec.Expenses.Where(e => e.Date.Month == request.Month && e.Date.Year == request.Year && e.UserId == request.UserId).Sum(e => e.Total)
                    })
                    .OrderBy(ec => ec.TypeId)
                    .ToListAsync(cancellationToken);

            var totalExpenses = expenseCategories.Sum(e => e.Sum);

            return new ExpensesByCategoryListVm
            {
                ExpenseCategories = expenseCategories,
                Totals = totalExpenses
            };
        }
    }
}
