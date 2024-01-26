using Microsoft.AspNetCore.Mvc;
using Profit_Food.API.Repositories;
using Profit_Food.API.Models;
using Profit_Food.API.Models.DTO;
using Profit_Food.API.Mappers;

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
			FoodCatalog catalog;
			try
			{
				catalog = await _catalogRepository.GetCatalog(id);		
            }
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}

			return Ok(catalog);
		}


		[HttpGet("withFood/{id}")]
		public async Task<ActionResult<FoodCatalog>> GetCatalogWithFood(int id)
		{
			FoodCatalog catalog;
			try
			{
				catalog = await _catalogRepository.GetCatalogWithFood(id);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}

			return Ok(catalog);
		}

		[HttpGet("collection")]
		public async Task<ActionResult<IEnumerable<FoodCatalog>>> GetCatalogs()
		{
			IEnumerable<FoodCatalog> catalogs;
			try
			{
				catalogs = await _catalogRepository.GetCatalogs();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
			return Ok(catalogs);
		}

		[HttpGet("collection/withFood")]
		public async Task<ActionResult<IEnumerable<FoodCatalog>>> GetCatalogsWithFood()
		{
			IEnumerable<FoodCatalog> catalogs;
			try
			{
				catalogs = await _catalogRepository.GetCatalogsWithFood();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
			return Ok(catalogs);
		}

		[HttpPost()]
		public async Task<ActionResult> AddCatalog(FoodCatalogDTO catalog)
		{
			try
			{
				await _catalogRepository.AddCatalog(catalog.ToFoodCatalog());
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
			return Ok();
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> UpdateCatalog(int id, FoodCatalogDTO catalog)
		{
			try
			{
				await _catalogRepository.UpdateCatalog(id, catalog.ToFoodCatalog());
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}

			return Accepted(catalog);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteCatalog(int id)
		{
			try
			{
				await _catalogRepository.DeleteCatalog(id);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
			return Ok();
		}

	}
}
