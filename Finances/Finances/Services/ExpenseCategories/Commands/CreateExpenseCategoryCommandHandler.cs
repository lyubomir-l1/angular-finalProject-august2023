using Finances.Common.Exceptions;
using Finances.Common.Interfaces;
using Finances.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Finances.Services.ExpenseCategories.Commands
{
    public class CreateExpenseCategoryCommand : IRequest<int>
    {
        public string Name { get; set; } = default!;

        public int TypeId { get; set; }

        public string UserId { get; set; } = default!;
    }

    public class CreateExpenseCategoryCommandValidator : AbstractValidator<CreateExpenseCategoryCommand>
    {
        public CreateExpenseCategoryCommandValidator()
        {
            RuleFor(e => e.Name)
                .MaximumLength(100)
                .NotNull()
                .NotEmpty()
                .WithMessage(string.Format("Name is reqired"));

            RuleFor(e => e.TypeId)
                .NotNull()
                .NotEmpty()
                .WithMessage(string.Format("Type is required"));


            RuleFor(e => e.UserId)
                .NotNull()
                .NotEmpty()
                .WithMessage(string.Format("User is required"));
        }
    }

    public class CreateExpenseCategoryCommandHandler : IRequestHandler<CreateExpenseCategoryCommand, int>
    {
        private const string User = "User";
        private const string Type = "Cashflow Type";
        private const string ErrorMessage = "Cannot create entity of type Expense, because {0} does not exsists.";

        private readonly IApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public CreateExpenseCategoryCommandHandler(IApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<int> Handle(CreateExpenseCategoryCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                throw new NotFoundException(nameof(ApplicationUser), request.UserId);
            }

            var type = await context.CashflowTypes.FindAsync(request.TypeId);

            if (type == null)
            {
                throw new NotFoundException(nameof(CashflowType), request.TypeId);
            }

            var expenseCategory = new ExpenseCategory
            {
                Name = request.Name,
                UserId = request.UserId,
                TypeId = request.TypeId
            };

            await context.ExpenseCategories.AddAsync(expenseCategory);

            await context.SaveChangesAsync(cancellationToken);

            return expenseCategory.Id;
        }
    }
}
