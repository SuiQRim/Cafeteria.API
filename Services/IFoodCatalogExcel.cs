using Profit_Food.API.Models;

namespace ProfitTest_Сafeteria.API.Services
{
	public interface IFoodCatalogExcel
	{
		public MemoryStream FileStream(List<FoodCatalog> catalogs);
	}
}
