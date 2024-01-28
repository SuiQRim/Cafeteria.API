using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfitTest_Cafeteria.API.Models;
using ProfitTest_Cafeteria.API.Models.DTO;
using ProfitTest_Cafeteria.API.Services.Repositories.IRepositories;

namespace ProfitTest_Cafeteria.API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class FoodsController : ControllerBase
	{
		private readonly IFoodRepository _foodRepository;

		public FoodsController(IFoodRepository foodRepository)
		{
			_foodRepository = foodRepository;
		}

		[HttpGet("collection")]
		public async Task<ActionResult<IEnumerable<Food>>> GetFoods()
		{
			List<Food> foods = await _foodRepository.GetFoods();

			return Ok(foods);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Food>> GetFood(int id)
		{
			Food? food = await _foodRepository.GetFood(id);

			return Ok(food);
		}

		[HttpPost("add")]
		[Authorize]
		public async Task<ActionResult<Food>> AddFood(FoodDTO foodDTO)
		{
			await _foodRepository.AddFood(foodDTO);

			return Accepted();
		}

		[HttpPut("edit/{id}")]
		[Authorize]
		public async Task<ActionResult> UpdateFood(int id, FoodDTO foodDTO)
		{
			await _foodRepository.UpdateFood(id, foodDTO);

			return Accepted();
		}

		[HttpDelete("{id}")]
		[Authorize]
		public async Task<ActionResult> RemoveFood(int id)
		{

			await _foodRepository.DeleteFood(id);
		
			return Accepted();
		}
	}
}
