using ExpenseControlSystem.Application.Interfaces;
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

        public CategoryController(IAppCategory appCategory)
        {
            _appCategory = appCategory;
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
    }
}
