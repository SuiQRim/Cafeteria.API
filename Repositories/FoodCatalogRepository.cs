using Microsoft.EntityFrameworkCore;
using Profit_Food.API.DataBase;
using Profit_Food.API.Models;

namespace Profit_Food.API.Repositories
{
    public class FoodCatalogRepository : IFoodCatalogRepository
    {
        private readonly FoodstuffsApiContext _context;
        public FoodCatalogRepository(FoodstuffsApiContext context)
        {
            _context = context;
        }

        public async Task<FoodCatalog> GetCatalog(int id)
        {
            FoodCatalog? catalog = 
                await _context.FoodCatalogs.SingleOrDefaultAsync(c => c.Id == id);

			if (catalog == null)
				throw new NullReferenceException($"Entity (FoodCatalog) with id {id} is not found");

			return catalog;
        }
        public async Task<FoodCatalog> GetCatalogWithFood(int id)
        {
            FoodCatalog? catalog = 
                await _context.FoodCatalogs.Include(c => c.Foods).SingleOrDefaultAsync(c => c.Id == id);

			if (catalog == null)
				throw new NullReferenceException($"Entity (FoodCatalog) with id {id} is not found");

			return catalog;
        }

        public async Task<IEnumerable<FoodCatalog>> GetCatalogs()
        {
            IEnumerable<FoodCatalog> foodCatalogs = 
                await _context.FoodCatalogs.ToArrayAsync();

            return foodCatalogs;
        }

        public async Task<IEnumerable<FoodCatalog>> GetCatalogsWithFood()
        {
            IEnumerable<FoodCatalog> foodCatalogs = 
                await _context.FoodCatalogs.Include(c => c.Foods).ToArrayAsync();

            return foodCatalogs;
        }

        public async Task AddCatalog(FoodCatalog catalog)
        {
            await _context.FoodCatalogs.AddAsync(catalog);
            await _context.SaveChangesAsync();
        }



        public async Task UpdateCatalog(int id, FoodCatalog catalog)
        {
			var entity = await _context.FoodCatalogs.FirstOrDefaultAsync(f => f.Id == id);

			if (entity == null)
				throw new NullReferenceException($"Entity (FoodCatalog) with id {id} is not found");

			entity.Name = catalog.Name;
			await _context.SaveChangesAsync();
		}
        
        public async Task DeleteCatalog(int id)
        {
            FoodCatalog catalog = await _context.FoodCatalogs.FindAsync(id);

			if (catalog == null)
				throw new NullReferenceException($"Entity (FoodCatalog) with id {id} is not found");

			_context.FoodCatalogs.Remove(catalog);
            await _context.SaveChangesAsync();
		}
    }
}
