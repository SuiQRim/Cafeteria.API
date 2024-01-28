using ProfitTest_Cafeteria.API.Models;
using ProfitTest_Cafeteria.API.Models.DTO;

namespace ProfitTest_Cafeteria.API.Services.Repositories.IRepositories
{
    public interface IFoodCatalogRepository
    {
        public Task<IEnumerable<FoodCatalog>> GetCatalogs();
        public Task<IEnumerable<FoodCatalog>> GetCatalogsWithFood();

        public Task<FoodCatalog> GetCatalog(int id);
        public Task<FoodCatalog> GetCatalogWithFood(int id);

        public Task AddCatalog(FoodCatalogDTO food);

        public Task UpdateCatalog(int id, FoodCatalogDTO food);
        public Task DeleteCatalog(int id);

        public Task<MemoryStream> GetExcelCatalog();
    }
}
