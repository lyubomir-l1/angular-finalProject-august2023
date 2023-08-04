namespace Finances.Services.Incomes.Commands
{
    using Finances.Common.Exceptions;
    using Finances.Common.Interfaces;
    using Finances.Models;
    using FluentValidation;
    using MediatR;
    using Microsoft.AspNetCore.Identity;

    public class UpdateIncomeCommand : IRequest<int>
    {
        public int Id { get; set; }

        public string? Merchant { get; set; }

        public string Date { get; set; } = default!;

        public decimal Total { get; set; }

        public string? Note { get; set; }

        public int CategoryId { get; set; }

        public string UserId { get; set; } = default!;
    }

    public class UpdateIncomeCommandValidator : AbstractValidator<UpdateIncomeCommand>
    {
        public UpdateIncomeCommandValidator()
        {
            RuleFor(e => e.Id)
                .NotEmpty()
                .WithMessage("Id is required");

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

    public class UpdateIncomeCommandHandler : IRequestHandler<UpdateIncomeCommand, int>
    {
        private readonly IApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public UpdateIncomeCommandHandler(IApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<int> Handle(UpdateIncomeCommand request, CancellationToken cancellationToken)
        {
            var income = await context.Incomes.FindAsync(request.Id);

            if (income == null)
            {
                throw new NotFoundException(nameof(Income), request.Id);
            }

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


            income.Merchant = request.Merchant;
            income.Date = DateTime.Parse(request.Date);
            income.Note = request.Note;
            income.UserId = request.UserId;
            income.Total = request.Total;
            income.CategoryId = request.CategoryId;

            context.Incomes.Update(income);

            await context.SaveChangesAsync(cancellationToken);

            return income.Id;
        }
    }
}
