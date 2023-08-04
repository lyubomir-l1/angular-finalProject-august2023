using Finances.Controllers;
using Finances.Services.IncomeCategories.Commands;
using Finances.Services.IncomeCategories.Queries.GetAll;
using Finances.Services.IncomeCategories.Queries.GetIncomesByCategory;
using Microsoft.AspNetCore.Mvc;

namespace Finance.WebApp.Controllers
{
    public class IncomeCategoryController : ApiController
    {
        //GET: api/IncomeCategory/GetAll
        [HttpGet("[action]")]
        public async Task<ActionResult<List<IncomeCategoryVm>>> GetAll(string userId)
        {
            var result = await Mediator.Send(new GetIncomeCategoriesListQuery() { UserId = userId });
           
            return Ok(result);
        }

        //GET: api/IncomeCategory/GetIncomesByCategory
        [HttpGet("[action]")]
        public async Task<ActionResult<IncomesByCategoryListVm>> GetIncomesByCategory(int month, int year, string userId)
        {
            var result = await Mediator.Send(new GetIncomesByCategoryListQuery() { Month = month, Year = year, UserId = userId });

            return Ok(result);
        }

        // POST: api/IncomeCategory/Create
        [HttpPost]
        public async Task<ActionResult> Create(CreateIncomeCategoryCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // DELETE: api/IncomeCategory/Delete/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteIncomeCategoryCommand { Id = id });

            return NoContent();
        }
    }
}
