using Microsoft.EntityFrameworkCore;
using Profit_Food.API.DataBase;
using Profit_Food.API.Models;
using OfficeOpenXml;
using System.Drawing;
using OfficeOpenXml.Style;

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
            FoodCatalog? catalog = await _context.FoodCatalogs.FindAsync(id);

			if (catalog == null)
				throw new NullReferenceException($"Entity (FoodCatalog) with id {id} is not found");

			_context.FoodCatalogs.Remove(catalog);
            await _context.SaveChangesAsync();
		}

		public async Task<MemoryStream> GetExcelCatalog()
		{
            List<FoodCatalog> catalogs = new(await GetCatalogsWithFood());

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

			using (ExcelPackage pck = new())
			{
				ExcelWorksheet sheet = pck.Workbook.Worksheets.Add("Food Catalog");

				int top = 1;
				for (int i = 0; i < catalogs.Count; i++, top++)
				{
                    sheet.Cells[top, -1, top, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells[top, 1, top, 6].Style.Fill.BackgroundColor.SetColor(Color.Yellow);

					sheet.Cells[top, 1].Value = catalogs[i].Id;
					sheet.Cells[top, 2].Value = catalogs[i].Name;

					List<Food> foods = catalogs[i].Foods;
                    top++;

                    int categoryStartY = top;
					for (int j = 0; j < foods.Count; j++, top++)
					{
						sheet.Cells[top, 2].Value = foods[j].Id;
						sheet.Cells[top, 3].Value = foods[j].Name;
						sheet.Cells[top, 4].Value = foods[j].Kcal;
						sheet.Cells[top, 5].Value = foods[j].Price;
						sheet.Cells[top, 6].Value = foods[j].CatalogId;
					}

					top--;
					sheet.Cells[categoryStartY, 1, top, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;
					sheet.Cells[categoryStartY, 1, top, 6].Style.Fill.BackgroundColor.SetColor(Color.Gray);
				}

				var stream = new MemoryStream(pck.GetAsByteArray());
                return stream;
			}
		}
	}
}
