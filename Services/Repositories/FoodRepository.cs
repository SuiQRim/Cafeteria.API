using Microsoft.EntityFrameworkCore;
using ProfitTest_Cafeteria.API.DataBase;
using ProfitTest_Cafeteria.API.Exceptions;
using ProfitTest_Cafeteria.API.Models;

namespace ProfitTest_Cafeteria.API.Services.Repositories
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
            Food? food = await FindById(id);

            if (food == null)
                throw new EntityNotFoundExceptions(typeof(Food), id.ToString());

            return food;
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
            Food? food = await FindById(id);

            if (food == null)
                throw new EntityNotFoundExceptions(typeof(Food), id.ToString());

            _context.Foods.Remove(food);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateFood(int id, Food food)
        {
            Food? entity = await FindById(id);

            if (food == null)
                throw new EntityNotFoundExceptions(typeof(Food), id.ToString());

            entity.Price = food.Price;
            entity.Kcal = food.Kcal;
            entity.Name = food.Name;
            entity.CatalogId = food.CatalogId;

            await _context.SaveChangesAsync();
        }

        private async Task<Food?> FindById(int id)
        {
            return await _context.Foods.FindAsync(id);
        }
    }
}
