using ExpenseControlSystem.Application.Exceptions;
using ExpenseControlSystem.Application.Interfaces;
using ExpenseControlSystem.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseControlSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IAppPerson _appPerson;

        public PersonController(IAppPerson appPerson)
        {
            _appPerson = appPerson;
        }

     
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PersonViewModel>), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<PersonViewModel>>> GetAll()
        {
            try
            {
                var persons = await _appPerson.GetAllAsync();
                return Ok(persons);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Erro interno no servidor" });
            }
        }

      
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PersonViewModel), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<PersonViewModel>> GetById(int id)
        {
            try
            {
                var person = await _appPerson.GetByIdAsync(id);

                if(person is null)
                    return NotFound();

                return Ok(person);
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

      
        [HttpGet("{id}/with-transactions")]
        [ProducesResponseType(typeof(PersonViewModel), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<PersonViewModel>> GetByIdWithTransactions(int id)
        {
            try
            {
                var person = await _appPerson.GetByIdWithTransactionsAsync(id);
                return Ok(person);
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

       
        [HttpGet("with-transactions")]
        [ProducesResponseType(typeof(IEnumerable<PersonViewModel>), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<PersonViewModel>>> GetAllWithTransactions()
        {
            try
            {
                var persons = await _appPerson.GetAllWithTransactionsAsync();
                return Ok(persons);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Erro interno no servidor" });
            }
        }

       
        [HttpPost]
        [ProducesResponseType(typeof(PersonViewModel), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<PersonViewModel>> Create([FromBody] PersonViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _appPerson.AddAsync(viewModel);
                return CreatedAtAction(nameof(GetById), new { id = viewModel.Id }, viewModel);
            }
            catch (ErrorOnValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (ArgumentException ex)
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
        public async Task<IActionResult> Update(int id, [FromBody] PersonViewModel viewModel)
        {
            try
            {
                if (id != viewModel.Id)
                {
                    return BadRequest(new { message = "ID na rota não corresponde ao ID no corpo da requisição" });
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _appPerson.UpdateAsync(viewModel);
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
                var person = await _appPerson.GetByIdAsync(id);
                if (person == null)
                {
                    return NotFound(new { message = $"Pessoa com ID {id} não encontrada" });
                }

                await _appPerson.DeleteAsync(person);
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
            catch (Exception)
            {
                return StatusCode(500, new { message = "Erro interno no servidor" });
            }
        }


        [HttpGet("{id}/exists")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<bool>> Exists(int id)
        {
            try
            {
                var exists = await _appPerson.ExistsAsync(id);
                return Ok(exists);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Erro interno no servidor" });
            }
        }
    }
}
