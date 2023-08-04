using Finances.Common.Exceptions;
using Finances.Common.Interfaces;
using MediatR;

namespace Finances.Services.Expenses.Commands
{
    public class DeleteExpenseCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class DeleteExpenseCommandHandler : IRequestHandler<DeleteExpenseCommand, bool>
    {
        private const string Expense = "Expense";

        private readonly IApplicationDbContext context;

        public DeleteExpenseCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
        {
            var expense = await context.Expenses.FindAsync(request.Id);

            if (expense == null)
            {
                throw new NotFoundException(Expense, request.Id);
            }

            context.Expenses.Remove(expense);
            await context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
