using Profit_Food.API.Models;
using Profit_Food.API.Models.DTO;

namespace Profit_Food.API.Mappers
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
