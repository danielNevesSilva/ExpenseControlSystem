using Microsoft.AspNetCore.Mvc;

using ExpenseControlSystem.Application.Interfaces;
using ExpenseControlSystem.Application.ViewModel;
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

        /// <summary>
        /// Retorna todas as transações
        /// </summary>
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

        /// <summary>
        /// Retorna uma transação específica pelo ID
        /// </summary>
        /// <param name="id">ID da transação</param>
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

        /// <summary>
        /// Retorna transações por pessoa
        /// </summary>
        /// <param name="personId">ID da pessoa</param>
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

        /// <summary>
        /// Retorna transações por categoria
        /// </summary>
        /// <param name="categoryId">ID da categoria</param>
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

        /// <summary>
        /// Cria uma nova transação com todas as validações de negócio
        /// </summary>
        /// <param name="viewModel">Dados da transação</param>
        [HttpPost]
        [ProducesResponseType(typeof(TransactionViewModel), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<TransactionViewModel>> Create([FromBody] TransactionViewModel viewModel)
        {
            try
            {
                // Validação automática do FluentValidation (se configurado)
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _appTransaction.AddAsync(viewModel);

                // Pega a transação criada para retornar com dados completos
                var createdTransaction = await _appTransaction.GetByIdAsync(viewModel.Id);
                return CreatedAtAction(nameof(GetById), new { id = viewModel.Id }, createdTransaction);
            }
            catch (ValidationException ex)
            {
                // Validações de negócio (menor idade, categoria incompatível, etc.)
                return BadRequest(new { error = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Erro interno no servidor" });
            }
        }

        /// <summary>
        /// Atualiza uma transação existente
        /// </summary>
        /// <param name="id">ID da transação</param>
        /// <param name="viewModel">Dados atualizados</param>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Update(int id, [FromBody] TransactionViewModel viewModel)
        {
            try
            {
                if (id != viewModel.Id)
                {
                    return BadRequest(new { error = "ID na rota não corresponde ao ID no corpo da requisição" });
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _appTransaction.UpdateAsync(viewModel);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Erro interno no servidor" });
            }
        }

        /// <summary>
        /// Exclui uma transação
        /// </summary>
        /// <param name="id">ID da transação</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // Primeiro busca a transação
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

        /// <summary>
        /// Retorna o total de receitas de uma pessoa
        /// </summary>
        /// <param name="personId">ID da pessoa</param>
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

        /// <summary>
        /// Retorna o total de despesas de uma pessoa
        /// </summary>
        /// <param name="personId">ID da pessoa</param>
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

        /// <summary>
        /// Valida se uma transação pode ser criada para uma pessoa
        /// (Útil para o front-end validar antes de tentar criar)
        /// </summary>
        /// <param name="personId">ID da pessoa</param>
        /// <param name="type">Tipo da transação</param>
        /// <param name="categoryId">ID da categoria</param>
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
                // Esta validação precisa ser implementada no IAppTransaction
                // Você pode adicionar um método como: ValidateTransactionForPersonAsync
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