using Profit_Food.API.Models.DTO;
using Profit_Food.API.Models;

namespace Profit_Food.API.Mappers
{
	public static class FoodMapper
	{
		public static Food ToFood(this FoodDTO foodDTO)
		{
			return new Food()
			{
				Name = foodDTO.Name,
				Kcal = foodDTO.Kcal,
				Price = foodDTO.Price,
				CatalogId = foodDTO.CatalogId
			};
		}
	}
}
