using ProfitTest_Cafeteria.API.Models;
using ProfitTest_Cafeteria.API.Models.DTO;

namespace ProfitTest_Cafeteria.API.Services.Repositories.IRepositories
{
    public interface IFoodRepository
    {
        public Task<List<Food>> GetFoods();

        public Task<Food?> GetFood(int id);

        public Task AddFood(FoodDTO food);

        public Task UpdateFood(int id, FoodDTO food);
        public Task DeleteFood(int id);
    }
}
