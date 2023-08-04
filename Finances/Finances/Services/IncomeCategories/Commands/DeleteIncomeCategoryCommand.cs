using Finances.Common.Exceptions;
using Finances.Common.Interfaces;
using Finances.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Finances.Services.IncomeCategories.Commands
{
    public class DeleteIncomeCategoryCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class DeleteIncomeCategoryCommandHandler : IRequestHandler<DeleteIncomeCategoryCommand, bool>
    {
        private readonly IApplicationDbContext context;

        public DeleteIncomeCategoryCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Handle(DeleteIncomeCategoryCommand request, CancellationToken cancellationToken)
        {
            var incomeCategory = await context.IncomeCategories
                .Include(ec => ec.Incomes)
                .SingleOrDefaultAsync(ec => ec.Id == request.Id);

            if (incomeCategory == null)
            {
                throw new NotFoundException(nameof(IncomeCategory), request.Id);
            }

            context.IncomeCategories.Remove(incomeCategory);
            await context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
