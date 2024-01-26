using Microsoft.AspNetCore.Mvc;
using Profit_Food.API.Models;
using Profit_Food.API.Models.DTO;
using Profit_Food.API.Mappers;
using ProfitTest_Сafeteria.API.Services.Repositories;

namespace Profit_Food.API.Controllers
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
		public async Task<ActionResult<FoodCatalog>> GetCatalog(int id)
		{
			FoodCatalog catalog = await _catalogRepository.GetCatalog(id);
	
			return Ok(catalog);
		}

		[HttpGet("excel")]
		public async Task<IActionResult> GetExcel()
		{
			MemoryStream stream = await _catalogRepository.GetExcelCatalog();		

			return File(stream, "application/octet-stream", "FoodCatalog.xlsx");
		}


		[HttpGet("withFood/{id}")]
		public async Task<ActionResult<FoodCatalog>> GetCatalogWithFood(int id)
		{
			FoodCatalog catalog = await _catalogRepository.GetCatalogWithFood(id);
	
			return Ok(catalog);
		}

		[HttpGet("collection")]
		public async Task<ActionResult<IEnumerable<FoodCatalog>>> GetCatalogs()
		{
			IEnumerable<FoodCatalog> catalogs = await _catalogRepository.GetCatalogs();

			return Ok(catalogs);
		}

		[HttpGet("collection/withFood")]
		public async Task<ActionResult<IEnumerable<FoodCatalog>>> GetCatalogsWithFood()
		{
			IEnumerable<FoodCatalog> catalogs = await _catalogRepository.GetCatalogsWithFood();

			return Ok(catalogs);
		}

		[HttpPost()]
		public async Task<ActionResult> AddCatalog(FoodCatalogDTO catalog)
		{
			await _catalogRepository.AddCatalog(catalog.ToFoodCatalog());

			return Ok();
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> UpdateCatalog(int id, FoodCatalogDTO catalog)
		{
			await _catalogRepository.UpdateCatalog(id, catalog.ToFoodCatalog());

			return Accepted(catalog);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteCatalog(int id)
		{
			await _catalogRepository.DeleteCatalog(id);

			return Ok();
		}

	}
}
