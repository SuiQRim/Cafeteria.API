using Microsoft.AspNetCore.Mvc;
using Profit_Food.API.Mappers;
using Profit_Food.API.Models;
using Profit_Food.API.Models.DTO;
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
			List<Food> foods;
			try
			{
				foods = await _foodRepository.GetFoods();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}

            return Ok(foods);
        }

		[HttpGet("{id}")]
		public async Task<ActionResult<Food>> GetFood(int id)
		{
			Food? food;
			try
			{
				food = await _foodRepository.GetFood(id);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}

			return Ok(food);
		}

		[HttpPost]
		public async Task<ActionResult<Food>> AddFood(FoodDTO foodDTO)
		{
			try
			{
				await _foodRepository.AddFood(foodDTO.ToFood());
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
			
			return Accepted();
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> UpdateFood(int id, FoodDTO foodDTO)
		{
			try
            {
                await _foodRepository.UpdateFood(id, foodDTO.ToFood());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
			return Ok();
		}

		[HttpDelete]
		public async Task<ActionResult> RemoveFood(int id)
		{
			try
			{
				await _foodRepository.DeleteFood(id);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}	
			return Ok();
		}
	}
}
