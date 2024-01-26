using Profit_Food.API.Models;

namespace ProfitTest_Сafeteria.API.Services.Repositories
{
    public interface IFoodCatalogRepository
    {
        public Task<IEnumerable<FoodCatalog>> GetCatalogs();
        public Task<IEnumerable<FoodCatalog>> GetCatalogsWithFood();

        public Task<FoodCatalog> GetCatalog(int id);
        public Task<FoodCatalog> GetCatalogWithFood(int id);

        public Task AddCatalog(FoodCatalog food);

        public Task UpdateCatalog(int id, FoodCatalog food);
        public Task DeleteCatalog(int id);

        public Task<MemoryStream> GetExcelCatalog();
    }
}
