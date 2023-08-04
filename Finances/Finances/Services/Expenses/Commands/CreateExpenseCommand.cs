using Finances.Common.Exceptions;
using Finances.Common.Interfaces;
using Finances.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Finances.Services.Expenses.Commands
{
    public class CreateExpenseCommand : IRequest<int>
    {
        public string Merchant { get; set; } = default!;

        public string Date { get; set; } = default!;

        public decimal Total { get; set; }

        public string Note { get; set; } = default!;

        public int CategoryId { get; set; }

        public string UserId { get; set; } = default!;
    }
    public class CreateExpenseCommandValidator : AbstractValidator<CreateExpenseCommand>
    {
        public CreateExpenseCommandValidator()
        {
            RuleFor(e => e.Merchant)
                .MaximumLength(100)
                .WithMessage("Invalid merchant");

            RuleFor(e => e.Note)
                .MaximumLength(1000)
                .WithMessage("Invalid note");

            RuleFor(e => e.Date)
                .NotEmpty()
                .Must(BeValidDate)
                .WithMessage("Invalid date");

            RuleFor(e => e.Total)
                .GreaterThan(0.00M)
                .WithMessage("Invalid total");

            RuleFor(e => e.CategoryId)
                .NotEmpty()
                .WithMessage("Category Id is required");


            RuleFor(e => e.UserId)
                .NotEmpty()
                .WithMessage("User Id is required");
        }

        private bool BeValidDate(string date)
        {
            var parsedDate = new DateTime();

            if (DateTime.TryParse(date, out parsedDate))
            {
                return true;
            }

            return false;
        }
    }

    public class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommand, int>
    {
        private readonly IApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public CreateExpenseCommandHandler(IApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<int> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                throw new NotFoundException(nameof(ApplicationUser), request.UserId);
            }

            var category = await context.ExpenseCategories.FindAsync(request.CategoryId);

            if (category == null)
            {
                throw new NotFoundException(nameof(ExpenseCategory), request.CategoryId);
            }

            var expense = new Expense
            {
                Merchant = request.Merchant,
                Date = DateTime.Parse(request.Date),
                Note = request.Note,
                Total = request.Total,
                UserId = request.UserId,
                CategoryId = request.CategoryId,
            };

            await context.Expenses.AddAsync(expense);

            await context.SaveChangesAsync(cancellationToken);

            return expense.Id;
        }
    }
}
