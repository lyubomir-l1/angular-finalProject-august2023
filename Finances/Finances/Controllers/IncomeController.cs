using Finances.Controllers;
using Finances.Services.Incomes.Commands;
using Finances.Services.Incomes.Queries.GetAllIncomes;
using Finances.Services.Incomes.Queries.GetExpenseById;
using Finances.Services.Incomes.Queries.GetTotalIncomesByYear;
using Microsoft.AspNetCore.Mvc;

namespace Finance.WebApp.Controllers
{
    public class IncomeController : ApiController
    {
        //GET: api/Income/GetAll
        [HttpGet]
        public async Task<ActionResult<List<IncomeDto>>> GetAll(int month, int year, string userId)
        {
            var result = await Mediator.Send(new GetIncomesListQuery { Month = month, Year = year, UserId = userId });
            return Ok(result);
        }

        //GET: api/Income/Get/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IncomeVm>> Get(int id)
        {
            var result = await Mediator.Send(new GetIncomeByIdQuery { Id = id });
            return Ok(result);
        }

        //GET: api/Expense/GetByYear
        [HttpGet]
        public async Task<ActionResult<IncomesByYearListVm>> GetByYear(int year, string userId)
        {
            var result = await Mediator.Send(new GetIncomesByYearListQuery { Year = year, UserId = userId });
            return Ok(result);
        }

        // POST: api/Income/Create
        [HttpPost]
        public async Task<ActionResult> Create(CreateIncomeCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }


        // PUT: api/Income/Update
        [HttpPut]
        public async Task<ActionResult> Update(UpdateIncomeCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        // DELETE: api/Income/Delete/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteIncomeCommand { Id = id });

            return NoContent();
        }
    }
}
