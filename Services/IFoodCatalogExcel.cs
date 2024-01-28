using ProfitTest_Cafeteria.API.Models;

namespace ProfitTest_Cafeteria.API.Services
{
	public interface IFoodCatalogExcel
	{
		public MemoryStream FileStream(List<FoodCatalog> catalogs);
	}
}
