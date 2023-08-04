using Finances.Common.Exceptions;

namespace Finances.Services.Incomes.Commands
{
    using Finances.Common.Interfaces;
    using MediatR;

    public class DeleteIncomeCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class DeleteIncomeCommandHandler : IRequestHandler<DeleteIncomeCommand, bool>
    {
        private const string Income = "Income";

        private readonly IApplicationDbContext context;

        public DeleteIncomeCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Handle(DeleteIncomeCommand request, CancellationToken cancellationToken)
        {
            var income = await context.Incomes.FindAsync(request.Id);

            if (income == null)
            {
                throw new NotFoundException(Income, request.Id);
            }

            context.Incomes.Remove(income);
            await context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
