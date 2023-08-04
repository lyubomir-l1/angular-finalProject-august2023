using AutoMapper;
using AutoMapper.QueryableExtensions;
using Finances.Common.Exceptions;
using Finances.Common.Interfaces;
using Finances.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Finances.Services.Expenses.Queries.GetExpenseById
{
    public class GetExpenseByIdQuery : IRequest<ExpenseVm>
    {
        public int Id { get; set; }
    }

    public class GetExpenseByIdQueryHandler : IRequestHandler<GetExpenseByIdQuery, ExpenseVm>
    {
        private readonly IApplicationDbContext context;
        private readonly IMapper mapper;

        public GetExpenseByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ExpenseVm> Handle(GetExpenseByIdQuery request, CancellationToken cancellationToken)
        {
            var expense = await this.context.Expenses
                .Include(c => c.Category)
                .Where(x => x.Id == request.Id)
                .ProjectTo<ExpenseVm>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();

            if (expense == null)
            {
                throw new NotFoundException(nameof(Expense), request.Id);
            }

            return expense;
        }
    }
}
