using Microsoft.AspNetCore.Mvc;
using ProfitTest_Cafeteria.API.Models;
using ProfitTest_Cafeteria.API.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using ProfitTest_Cafeteria.API.Services.Repositories.IRepositories;

namespace ProfitTest_Cafeteria.API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class FoodCatalogsController : ControllerBase
	{
		private readonly IFoodCatalogRepository _catalogRepository;
		public FoodCatalogsController(IFoodCatalogRepository foodCatalogRepository)
        {
			_catalogRepository = foodCatalogRepository;
		}

        [HttpGet("{id}")]
		public async Task<ActionResult<FoodCatalog>> GetCatalog(int id, [FromQuery] bool withFood)
		{
			FoodCatalog catalog = withFood ? await _catalogRepository.GetCatalogWithFood(id) : await _catalogRepository.GetCatalog(id);
	
			return Ok(catalog);
		}

		[HttpGet("excel")]
		public async Task<IActionResult> GetExcel()
		{
			MemoryStream stream = await _catalogRepository.GetExcelCatalog();		

			return File(stream, "application/octet-stream", "FoodCatalog.xlsx");
		}

		[HttpGet("collection")]
		public async Task<ActionResult<IEnumerable<FoodCatalog>>> GetCatalogs([FromQuery] bool withFood)
		{
			IEnumerable<FoodCatalog> catalogs = withFood ? await _catalogRepository.GetCatalogsWithFood() : await _catalogRepository.GetCatalogs();

			return Ok(catalogs);
		}

		[HttpPost("add")]
		[Authorize]
		public async Task<ActionResult> AddCatalog(FoodCatalogDTO catalog)
		{
			await _catalogRepository.AddCatalog(catalog);

			return Accepted();
		}

		[HttpPut("{id}")]
		[Authorize]
		public async Task<ActionResult> UpdateCatalog(int id, FoodCatalogDTO catalog)
		{
			await _catalogRepository.UpdateCatalog(id, catalog);

			return Accepted(catalog);
		}

		[HttpDelete("{id}")]
		[Authorize]
		public async Task<ActionResult> DeleteCatalog(int id)
		{
			await _catalogRepository.DeleteCatalog(id);

			return Accepted();
		}

	}
}
