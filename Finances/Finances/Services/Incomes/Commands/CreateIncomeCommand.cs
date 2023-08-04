namespace Finances.Services.Incomes.Commands
{
    using Finances.Common.Exceptions;
    using Finances.Common.Interfaces;
    using Finances.Models;
    using FluentValidation;
    using MediatR;
    using Microsoft.AspNetCore.Identity;

    public class CreateIncomeCommand : IRequest<int>
    {
        public string? Merchant { get; set; }

        public string Date { get; set; } = default!;

        public decimal Total { get; set; }

        public string? Note { get; set; }

        public int CategoryId { get; set; }

        public string UserId { get; set; } = default!;

    }

    public class CreateIncomeCommandValidator : AbstractValidator<CreateIncomeCommand>
    {
        public CreateIncomeCommandValidator()
        {
            RuleFor(e => e.Merchant)
                .MaximumLength(100)
                .WithMessage("Invalid Mechant");

            RuleFor(e => e.Note)
                .MaximumLength(1000)
                .WithMessage("Invalid Note");

            RuleFor(e => e.Date)
                .NotEmpty()
                .Must(BeValidDate)
                .WithMessage("Invaid Date");

            RuleFor(e => e.Total)
                .GreaterThan(0)
                .WithMessage("Invalid Total");


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

    public class CreateIncomeCommandHandler : IRequestHandler<CreateIncomeCommand, int>
    {
        private readonly IApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public CreateIncomeCommandHandler(IApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<int> Handle(CreateIncomeCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                throw new NotFoundException(nameof(ApplicationUser), request.UserId);
            }

            var category = await context.IncomeCategories.FindAsync(request.CategoryId);

            if (category == null)
            {
                throw new NotFoundException(nameof(IncomeCategory), request.CategoryId);
            }

            var income = new Income
            {
                Merchant = request.Merchant,
                Date = DateTime.Parse(request.Date),
                Note = request.Note,
                Total = request.Total,
                UserId = request.UserId,
                CategoryId = request.CategoryId
            };

            await context.Incomes.AddAsync(income);

            await context.SaveChangesAsync(cancellationToken);

            return income.Id;
        }
    }
}
