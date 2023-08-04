using Finances.Common.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Finances.Services.IncomeCategories.Queries.GetIncomesByCategory
{
    public class GetIncomesByCategoryListQuery : IRequest<IncomesByCategoryListVm>
    {
        public int Month { get; set; }

        public int Year { get; set; }

        public string UserId { get; set; } = default!;
    }

    public class GetIncomesByCategoryListQueryValidator : AbstractValidator<GetIncomesByCategoryListQuery>
    {
        public GetIncomesByCategoryListQueryValidator()
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
                .WithMessage("User Id is required");
        }
    }

    public class GetIncomesByCategoryListQueryHandler : IRequestHandler<GetIncomesByCategoryListQuery, IncomesByCategoryListVm>
    {
        private readonly IApplicationDbContext context;

        public GetIncomesByCategoryListQueryHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IncomesByCategoryListVm> Handle(GetIncomesByCategoryListQuery request, CancellationToken cancellationToken)
        {
            var incomeCategories = await context.IncomeCategories
                    .Include(ec => ec.Incomes)
                    .Include(ex => ex.Type)
                    .Where(ex => ex.UserId == request.UserId)
                    .Select(ec => new IncomeByCategoryDto
                    {
                        Id = ec.Id,
                        Name = ec.Name,
                        TypeId = ec.TypeId,
                        TypeDescription = ec.Type.Description ?? string.Empty,
                        Sum = ec.Incomes.Where(e => e.Date.Month == request.Month && e.Date.Year == request.Year && e.UserId == request.UserId).Sum(e => e.Total)
                    })
                    .OrderBy(ec => ec.TypeId)
                    .ToListAsync(cancellationToken);

            var totalIncomes = incomeCategories.Sum(e => e.Sum);

            return new IncomesByCategoryListVm
            {
                IncomeCategories = incomeCategories,
                Totals = totalIncomes
            };
        }
    }
}
