using Finances.Services.Expenses.Commands;
using Finances.Services.Expenses.Queries.GetAllExpenses;
using Finances.Services.Expenses.Queries.GetExpenseById;
using Finances.Services.Expenses.Queries.GetTotalExpensesByYear;
using Microsoft.AspNetCore.Mvc;

namespace Finances.Controllers
{
    public class ExpenseController : ApiController
    {
        //GET: api/Expenses/GetAll
        [HttpGet("[action]")]
        public async Task<ActionResult<List<ExpenseDto>>> GetAll(int month, int year, string userId)
        {
            var result = await Mediator.Send(new GetExpensesListQuery { Month = month, Year = year, UserId = userId });
            return Ok(result);
        }

        //GET: api/Expense/Get/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseVm>> Get(int id)
        {
            var result = await Mediator.Send(new GetExpenseByIdQuery { Id = id });
            return Ok(result);
        }

        //GET: api/Expense/GetByYear
        [HttpGet]
        public async Task<ActionResult<ExpensesByYearListVm>> GetByYear(int year, string userId)
        {
            var result = await Mediator.Send(new GetExpensesByYearListQuery { Year = year, UserId = userId });
            return Ok(result);
        }

        // POST: api/Expense/Create
        [HttpPost]
        public async Task<ActionResult> Create(CreateExpenseCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }


        // PUT: api/Expense/Update
        [HttpPut]
        public async Task<ActionResult> Update(UpdateExpenseCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // DELETE: api/Expense/Delete/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteExpenseCommand { Id = id });

            return NoContent();
        }
    }
}
