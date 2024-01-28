using ProfitTest_Cafeteria.API.Models.DTO;
using ProfitTest_Cafeteria.API.Models;

namespace ProfitTest_Cafeteria.API.Mappers
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
