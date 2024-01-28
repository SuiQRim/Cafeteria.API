using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfitTest_Cafeteria.API.Mappers;
using ProfitTest_Cafeteria.API.Models;
using ProfitTest_Cafeteria.API.Models.DTO;
using ProfitTest_Cafeteria.API.Services.Repositories;

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

        [HttpGet]
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

		[HttpPost]
		[Authorize]
		public async Task<ActionResult<Food>> AddFood(FoodDTO foodDTO)
		{
			await _foodRepository.AddFood(foodDTO.ToFood());	

			return Accepted();
		}

		[HttpPut("{id}")]
		[Authorize]
		public async Task<ActionResult> UpdateFood(int id, FoodDTO foodDTO)
		{
            await _foodRepository.UpdateFood(id, foodDTO.ToFood());

			return Ok();
		}

		[HttpDelete]
		[Authorize]
		public async Task<ActionResult> RemoveFood(int id)
		{

			await _foodRepository.DeleteFood(id);
		
			return Ok();
		}
	}
}
