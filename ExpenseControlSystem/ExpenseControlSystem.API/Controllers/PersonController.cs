using ExpenseControlSystem.Application.Interfaces;
using ExpenseControlSystem.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseControlSystem.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    namespace ExpenseControlSystem.API.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class PersonController : ControllerBase
        {
            private readonly IAppPerson _appPerson;
            private readonly ILogger<PersonController> _logger;

            public PersonController(IAppPerson appPerson, ILogger<PersonController> logger)
            {
                _appPerson = appPerson;
                _logger = logger;
            }

            /// <summary>
            /// Retorna todas as pessoas cadastradas
            /// </summary>
            [HttpGet]
            [ProducesResponseType(typeof(IEnumerable<PersonViewModel>), 200)]
            [ProducesResponseType(500)]
            public async Task<ActionResult<IEnumerable<PersonViewModel>>> GetAll()
            {
                try
                {
                    _logger.LogInformation("Buscando todas as pessoas");
                    var persons = await _appPerson.GetAllAsync();
                    return Ok(persons);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao buscar pessoas");
                    return StatusCode(500, new { message = "Erro interno no servidor" });
                }
            }

            /// <summary>
            /// Retorna uma pessoa específica pelo ID
            /// </summary>
            /// <param name="id">ID da pessoa</param>
            [HttpGet("{id}")]
            [ProducesResponseType(typeof(PersonViewModel), 200)]
            [ProducesResponseType(404)]
            [ProducesResponseType(500)]
            public async Task<ActionResult<PersonViewModel>> GetById(int id)
            {
                try
                {
                    _logger.LogInformation("Buscando pessoa com ID: {Id}", id);
                    var person = _appPerson.GetByIdAsync(id);

                    if (person == null)
                    {
                        return NotFound(new { message = $"Pessoa com ID {id} não encontrada" });
                    }

                    return Ok(person);
                }
                catch (KeyNotFoundException ex)
                {
                    _logger.LogWarning(ex, "Pessoa não encontrada: {Id}", id);
                    return NotFound(new { message = ex.Message });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao buscar pessoa com ID: {Id}", id);
                    return StatusCode(500, new { message = "Erro interno no servidor" });
                }
            }

            /// <summary>
            /// Cria uma nova pessoa
            /// </summary>
            /// <param name="viewModel">Dados da pessoa</param>
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
                        _logger.LogWarning("Dados inválidos para criação de pessoa");
                        return BadRequest(ModelState);
                    }

                    _logger.LogInformation("Criando nova pessoa: {Name}", viewModel.Name);
                    var createdPerson =  _appPerson.AddAsync(viewModel);

                    return CreatedAtAction(nameof(GetById), new { id = createdPerson.Id }, createdPerson);
                }
                catch (ArgumentException ex)
                {
                    _logger.LogWarning(ex, "Dados inválidos para criação de pessoa");
                    return BadRequest(new { message = ex.Message });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao criar pessoa");
                    return StatusCode(500, new { message = "Erro interno no servidor" });
                }
            }

            /// <summary>
            /// Atualiza os dados de uma pessoa existente
            /// </summary>
            /// <param name="id">ID da pessoa</param>
            /// <param name="viewModel">Dados atualizados</param>
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
                        _logger.LogWarning("ID na rota ({RouteId}) diferente do ID no corpo ({BodyId})", id, viewModel.Id);
                        return BadRequest(new { message = "ID na rota não corresponde ao ID no corpo da requisição" });
                    }

                    if (!ModelState.IsValid)
                    {
                        _logger.LogWarning("Dados inválidos para atualização da pessoa ID: {Id}", id);
                        return BadRequest(ModelState);
                    }

                    _logger.LogInformation("Atualizando pessoa ID: {Id}", id);
                    await _appPerson.UpdateAsync(viewModel);

                    return NoContent();
                }
                catch (KeyNotFoundException ex)
                {
                    _logger.LogWarning(ex, "Pessoa não encontrada para atualização: {Id}", id);
                    return NotFound(new { message = ex.Message });
                }
                catch (ArgumentException ex)
                {
                    _logger.LogWarning(ex, "Dados inválidos para atualização da pessoa ID: {Id}", id);
                    return BadRequest(new { message = ex.Message });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao atualizar pessoa ID: {Id}", id);
                    return StatusCode(500, new { message = "Erro interno no servidor" });
                }
            }

            /// <summary>
            /// Exclui uma pessoa (e suas transações em cascata)
            /// </summary>
            /// <param name="id">ID da pessoa</param>
            [HttpDelete("{id}")]
            [ProducesResponseType(204)]
            [ProducesResponseType(404)]
            [ProducesResponseType(500)]
            public async Task<IActionResult> Delete(int id)
            {
                try
                {
                    _logger.LogInformation("Excluindo pessoa ID: {Id}", id);
                    _appPerson.GetByIdAsync(id);

                    return NoContent();
                }
                catch (KeyNotFoundException ex)
                {
                    _logger.LogWarning(ex, "Pessoa não encontrada para exclusão: {Id}", id);
                    return NotFound(new { message = ex.Message });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao excluir pessoa ID: {Id}", id);
                    return StatusCode(500, new { message = "Erro interno no servidor" });
                }
            }
        }
    }
}
