// API/Controllers/CategoryController.cs
using ExpenseControlSystem.Application.Aplications;
using ExpenseControlSystem.Application.Dto;
using ExpenseControlSystem.Application.Exceptions;
using ExpenseControlSystem.Application.Interfaces;
using ExpenseControlSystem.Application.NovaPasta;
using ExpenseControlSystem.Application.ViewModel;
using ExpenseControlSystem.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseControlSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IAppCategory _appCategory;
        private readonly IAppTransaction _appTransaction;

        public CategoryController(IAppCategory appCategory, IAppTransaction appTransaction)
        {
            _appCategory = appCategory;
            _appTransaction = appTransaction;
        }

   
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CategoryViewModel>), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<CategoryViewModel>>> GetAll()
        {
            try
            {
                var categories = await _appCategory.GetAllAsync();
                return Ok(categories);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Erro interno no servidor" });
            }
        }

        /// <summary>
        /// Retorna uma categoria específica pelo ID
        /// </summary>
        /// <param name="id">ID da categoria</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategoryViewModel), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<CategoryViewModel>> GetById(int id)
        {
            try
            {
                var category = await _appCategory.GetByIdAsync(id);

                if (category == null)
                {
                    return NotFound(new { message = $"Categoria com ID {id} não encontrada" });
                }

                return Ok(category);
            }
            catch (ErrorOnValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
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

      
        [HttpGet("purpose/{purpose}")]
        [ProducesResponseType(typeof(IEnumerable<CategoryViewModel>), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<CategoryViewModel>>> GetByPurpose(PurposeType purpose)
        {
            try
            {
                var categories = await _appCategory.GetByPurposeAsync(purpose);
                return Ok(categories);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Erro interno no servidor" });
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(CategoryViewModel), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<CategoryViewModel>> Create([FromBody] CreateCategoryDto category)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _appCategory.AddAsync(category);
                return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
            }
            catch (ErrorOnValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
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
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryDto category)
        {
            try
            {
                if (id != category.Id)
                {
                    return BadRequest(new { message = "ID na rota não corresponde ao ID no corpo da requisição" });
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _appCategory.UpdateAsync(category);
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

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // Primeiro busca a pessoa para deletar
                var category = await _appCategory.GetByIdAsync(id);
                var transacion = await _appTransaction.GetAllAsync();

                bool hasTransactions = transacion.Any(t => t.CategoryId == id);

                if (hasTransactions is true)
                {
                    return BadRequest(new { message = $"Não é possivel excluir essa categoria, existe transações relacionadas." });
                }

                await _appCategory.DeleteAsync(category);
                return NoContent();
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
    }
}