using Finances.Common.Exceptions;
using Finances.Common.Interfaces;
using Finances.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Finances.Services.IncomeCategories.Commands
{
    public class CreateIncomeCategoryCommand : IRequest<int>
    {
        public string Name { get; set; } = default!;

        public int TypeId { get; set; }

        public string UserId { get; set; } = default!;
    }

    public class CreateIncomeCategoryCommandValidator : AbstractValidator<CreateIncomeCategoryCommand>
    {
        public CreateIncomeCategoryCommandValidator()
        {
            RuleFor(e => e.Name)
                .MaximumLength(100)
                .NotNull()
                .NotEmpty()
                .WithMessage("Name is required");

            RuleFor(e => e.TypeId)
                .NotNull()
                .WithMessage("Cashflow type is required")
                .NotEmpty()
                .WithMessage("Cashflow type is required");


            RuleFor(e => e.UserId)
                .NotNull()
                .NotEmpty()
                .WithMessage("User Id is required");
        }
    }

    public class CreateIncomeCategoryCommandHandler : IRequestHandler<CreateIncomeCategoryCommand, int>
    {
        private readonly IApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public CreateIncomeCategoryCommandHandler(IApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<int> Handle(CreateIncomeCategoryCommand request, CancellationToken cancellationToken)
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

            var incomeCategory = new IncomeCategory
            {
                Name = request.Name,
                UserId = request.UserId,
                TypeId = request.TypeId
            };

            await context.IncomeCategories.AddAsync(incomeCategory);

            await context.SaveChangesAsync(cancellationToken);

            return incomeCategory.Id;
        }
    }
}
