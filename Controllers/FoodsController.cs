using Microsoft.AspNetCore.Mvc;
using Profit_Food.API.Models;
using Profit_Food.API.Repositories;

namespace Profit_Food.API.Controllers
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

			if (foods == null)
            {
                return NotFound();
            }
            return Ok(foods);
        }

		[HttpGet("{id}")]
		public async Task<ActionResult<IEnumerable<Food>>> GetFood(int id)
		{
			Food? food = await _foodRepository.GetFood(id);

			if (food == null)
			{
				return NotFound();
			}

			return Ok(food);
		}

		[HttpPost]
		public async Task<ActionResult<Food>> AddFood(Food food)
		{
			await _foodRepository.AddFood(food);
			
			return Accepted(food);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> UpdateFood(int id, Food food)
		{
            try
            {
                await _foodRepository.UpdateFood(id, food);
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(ex.Message);
            }
			return Ok();
		}

		[HttpDelete]
		public async Task<ActionResult> RemoveFood(int id)
		{
			await _foodRepository.DeleteFood(id);
			return Ok();
		}
	}
}
