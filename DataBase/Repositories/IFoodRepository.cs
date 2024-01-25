using Profit_Food.API.Models;

namespace Profit_Food.API.DataBase.Repositories
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
