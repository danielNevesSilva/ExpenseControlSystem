using ExpenseControlSystem.Application.Dto;
using ExpenseControlSystem.Application.Exceptions;
using ExpenseControlSystem.Application.Exceptions;
using ExpenseControlSystem.Application.Interfaces;
using ExpenseControlSystem.Application.ViewModel;
using ExpenseControlSystem.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ExpenseControlSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IAppTransaction _appTransaction;

        public TransactionController(IAppTransaction appTransaction)
        {
            _appTransaction = appTransaction;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TransactionViewModel>), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<TransactionViewModel>>> GetAll()
        {
            try
            {
                var transactions = await _appTransaction.GetAllAsync();
                return Ok(transactions);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Erro interno no servidor" });
            }
        }

     
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TransactionViewModel), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<TransactionViewModel>> GetById(int id)
        {
            try
            {
                var transaction = await _appTransaction.GetByIdAsync(id);
                return Ok(transaction);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Erro interno no servidor" });
            }
        }

     
        [HttpGet("person/{personId}")]
        [ProducesResponseType(typeof(IEnumerable<TransactionViewModel>), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<TransactionViewModel>>> GetByPersonId(int personId)
        {
            try
            {
                var transactions = await _appTransaction.GetByPersonIdAsync(personId);
                return Ok(transactions);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Erro interno no servidor" });
            }
        }

     
        [HttpGet("category/{categoryId}")]
        [ProducesResponseType(typeof(IEnumerable<TransactionViewModel>), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<TransactionViewModel>>> GetByCategoryId(int categoryId)
        {
            try
            {
                var transactions = await _appTransaction.GetByCategoryIdAsync(categoryId);
                return Ok(transactions);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Erro interno no servidor" });
            }
        }

   
        [HttpPost]
        [ProducesResponseType(typeof(TransactionViewModel), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<TransactionViewModel>> Create([FromBody] CreateTransactionDto viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await _appTransaction.Execute(viewModel);


                var createdTransaction = await _appTransaction.GetByIdAsync(viewModel.Id);
                return CreatedAtAction(nameof(GetById), new { id = viewModel.Id }, createdTransaction);
            }
            catch (ErrorOnValidationException ex)
            {
    
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Erro interno no servidor" });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTransactionDto viewModel)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _appTransaction.UpdateAsync(viewModel);
                return NoContent();
            }
            catch (ErrorOnValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Erro interno no servidor" });
            }
        }

     
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var transaction = await _appTransaction.GetByIdAsync(id);
                if (transaction == null)
                {
                    return NotFound(new { message = $"Transação com ID {id} não encontrada" });
                }

                await _appTransaction.DeleteAsync(transaction);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Erro interno no servidor" });
            }
        }

        [HttpGet("person/{personId}/total-income")]
        [ProducesResponseType(typeof(decimal), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<decimal>> GetTotalIncomeByPerson(int personId)
        {
            try
            {
                var total = await _appTransaction.GetTotalIncomeByPersonAsync(personId);
                return Ok(total);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Erro interno no servidor" });
            }
        }

        [HttpGet("person/{personId}/total-expense")]
        [ProducesResponseType(typeof(decimal), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<decimal>> GetTotalExpenseByPerson(int personId)
        {
            try
            {
                var total = await _appTransaction.GetTotalExpenseByPersonAsync(personId);
                return Ok(total);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Erro interno no servidor" });
            }
        }


        [HttpGet("validate")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<bool>> ValidateTransaction(
            [FromQuery] int personId,
            [FromQuery] int type,
            [FromQuery] int categoryId)
        {
            try
            {
              
                return Ok(new
                {
                    valid = true,
                    message = "Endpoint de validação não implementado ainda"
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Erro interno no servidor" });
            }
        }
    }
}