using Microsoft.EntityFrameworkCore;
using Profit_Food.API.DataBase;
using Profit_Food.API.Models;

namespace Profit_Food.API.Repositories
{
    public class FoodRepository : IFoodRepository
    {
        private readonly FoodstuffsApiContext _context;
        public FoodRepository(FoodstuffsApiContext context)
        {
            _context = context;
        }
        public async Task<Food?> GetFood(int id)
        {
            return await _context.Foods.FindAsync(id);
        }

        public async Task<List<Food>> GetFoods()
        {
            var list = await _context.Foods.ToListAsync();

            return list;
        }

        public async Task AddFood(Food food)
        {
            await _context.Foods.AddAsync(food);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFood(int id)
        {
            _context.Foods.Remove(new Food { Id = id });
            await _context.SaveChangesAsync();
        }


        public async Task UpdateFood(int id, Food food)
        {
            var entity = await _context.Foods.FirstOrDefaultAsync(f => f.Id == id);

            if (entity == null)
                throw new NullReferenceException($"Entity (Food) with id {id} is not found");

            entity.Price = food.Price;
            entity.Kcal = food.Kcal;
            entity.Name = food.Name;
            entity.CatalogId = food.CatalogId;

            await _context.SaveChangesAsync();
        }
    }
}
