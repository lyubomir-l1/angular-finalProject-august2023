using Finances.Controllers;
using Finances.Services.ExpenseCategories.Commands;
using Finances.Services.ExpenseCategories.Queries.GetAll;
using Finances.Services.ExpenseCategories.Queries.GetExpensesByCategory;
using Microsoft.AspNetCore.Mvc;

namespace Finance.WebApp.Controllers
{
    public class ExpenseCategoryController : ApiController
    {
        //GET: api/ExpenseCategory/GetAll
        [HttpGet("[action]")]
        public async Task<ActionResult<ExpenseCategoriesListVm>> GetAll(string userId)
        {
            var result = await Mediator.Send(new GetExpenseCategoriesListQuery() { UserId = userId });
           
            return Ok(result);
        }

        //GET: api/ExpenseCategory/GetExpensesByCategory
        [HttpGet("[action]")]
        public async Task<ActionResult<ExpensesByCategoryListVm>> GetExpensesByCategory(int month, int year, string userId)
        {
            var result = await Mediator.Send(new GetExpensesByCategoryListQuery() { Month = month, Year = year, UserId = userId });

            return Ok(result);
        }

        // POST: api/ExpenseCategory/Create
        [HttpPost]
        public async Task<ActionResult> Create(CreateExpenseCategoryCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // DELETE: api/ExpenseCategory/Delete/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteExpenseCategoryCommand { Id = id });

            return NoContent();
        }
    }
}
