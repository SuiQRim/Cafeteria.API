using ProfitTest_Cafeteria.API.Models;
using ProfitTest_Cafeteria.API.Models.DTO;

namespace ProfitTest_Cafeteria.API.Mappers
{
	public static class FoodCatalogMapper
	{
		public static FoodCatalog ToFoodCatalog(this FoodCatalogDTO foodCatalogDTO)
		{
			return new FoodCatalog()
			{
				Name = foodCatalogDTO.Name,
			};
		}
	}
}
