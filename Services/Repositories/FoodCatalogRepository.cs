using Microsoft.EntityFrameworkCore;
using ProfitTest_Cafeteria.API.DataBase;
using ProfitTest_Cafeteria.API.Models;
using OfficeOpenXml;
using System.Drawing;
using OfficeOpenXml.Style;
using ProfitTest_Cafeteria.API.Exceptions;

namespace ProfitTest_Cafeteria.API.Services.Repositories
{
    public class FoodCatalogRepository : IFoodCatalogRepository
    {
        private readonly FoodstuffsApiContext _context;
        private readonly IFoodCatalogExcel _excelConverter;
		public FoodCatalogRepository(FoodstuffsApiContext context, IFoodCatalogExcel excelConverter)
        {
            _context = context;
            _excelConverter = excelConverter;

		}

        public async Task<FoodCatalog> GetCatalog(int id)
        {
            FoodCatalog? catalog =
                await _context.FoodCatalogs.SingleOrDefaultAsync(c => c.Id == id);

            if (catalog == null)
                throw new EntityNotFoundExceptions(typeof(FoodCatalog), id.ToString());

            return catalog;
        }
        public async Task<FoodCatalog> GetCatalogWithFood(int id)
        {
            FoodCatalog? catalog =
                await _context.FoodCatalogs.Include(c => c.Foods).SingleOrDefaultAsync(c => c.Id == id);

            if (catalog == null)
                throw new EntityNotFoundExceptions(typeof(FoodCatalog), id.ToString());

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
                throw new EntityNotFoundExceptions(typeof(FoodCatalog), id.ToString());

            entity.Name = catalog.Name;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCatalog(int id)
        {
            FoodCatalog? catalog = await _context.FoodCatalogs.FindAsync(id);

            if (catalog == null)
                throw new EntityNotFoundExceptions(typeof(FoodCatalog), id.ToString());

            _context.FoodCatalogs.Remove(catalog);
            await _context.SaveChangesAsync();
        }

        public async Task<MemoryStream> GetExcelCatalog()
        {
            List<FoodCatalog> catalogs = new(await GetCatalogsWithFood());
            return _excelConverter.FileStream(catalogs);
        }
    }
}
