using Finances.Common.Exceptions;
using Finances.Common.Interfaces;
using Finances.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Finances.Services.ExpenseCategories.Commands
{
    public class DeleteExpenseCategoryCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class DeleteExpenseCategoryCommandHandler : IRequestHandler<DeleteExpenseCategoryCommand, bool>
    {
        private readonly IApplicationDbContext context;

        public DeleteExpenseCategoryCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Handle(DeleteExpenseCategoryCommand request, CancellationToken cancellationToken)
        {
            var expenseCategory = await context.ExpenseCategories
                .Include(ec => ec.Expenses)
                .SingleOrDefaultAsync(ec => ec.Id == request.Id);

            if (expenseCategory == null)
            {
                throw new NotFoundException(nameof(ExpenseCategory), request.Id);
            }

            context.ExpenseCategories.Remove(expenseCategory);
            await context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
