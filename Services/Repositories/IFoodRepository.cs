using ProfitTest_Cafeteria.API.Models;

namespace ProfitTest_Cafeteria.API.Services.Repositories
{
    public interface IFoodRepository
    {
        public Task<List<Food>> GetFoods();

        public Task<Food?> GetFood(int id);

        public Task AddFood(Food food);

        public Task UpdateFood(int id, Food food);
        public Task DeleteFood(int id);
    }
}
