using OfficeOpenXml.Style;
using OfficeOpenXml;
using ProfitTest_Cafeteria.API.Models;
using System.Drawing;

namespace ProfitTest_Cafeteria.API.Services
{
	public class FoodCatalogExcel : IFoodCatalogExcel
	{
		public MemoryStream FileStream(List<FoodCatalog> catalogs)
		{
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

			using ExcelPackage pck = new();

			ExcelWorksheet sheet = pck.Workbook.Worksheets.Add("Food Catalog");

			int top = 4;
			AddTemplate(sheet, 1);
			
			for (int i = 0; i < catalogs.Count; i++, top++)
			{
				PrintCatalog(sheet, catalogs[i], ref top);
				PrintFoods(sheet, catalogs[i].Foods, ref top);
			}

			return new MemoryStream(pck.GetAsByteArray());
		}

		private static void PrintCatalog(ExcelWorksheet sheet, FoodCatalog catalog, ref int top)
		{
			sheet.Cells[top, 1, top, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;
			sheet.Cells[top, 1, top, 6].Style.Fill.BackgroundColor.SetColor(Color.Yellow);

			sheet.Cells[top, 1].Value = catalog.Id;
			sheet.Cells[top, 2].Value = catalog.Name;

			top++;
		}

		private static void PrintFoods(ExcelWorksheet sheet, List<Food> foods, ref int top)
		{
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


		private static void AddTemplate(ExcelWorksheet sheet, int top)
		{
			sheet.Cells[top, 1, top, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;
			sheet.Cells[top, 1, top, 6].Style.Fill.BackgroundColor.SetColor(Color.Yellow);

			sheet.Cells[top, 1].Value = "CategoryId";
			sheet.Cells[top, 2].Value = "Name";
			sheet.Cells[top + 1, 2].Value = "Id";
			sheet.Cells[top + 1, 3].Value = "Name";
			sheet.Cells[top + 1, 4].Value = "Kcal";
			sheet.Cells[top + 1, 5].Value = "Price";
			sheet.Cells[top + 1, 6].Value = "CategoryId";

			sheet.Cells[top + 1, 1, top + 1, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;
			sheet.Cells[top + 1, 1, top + 1, 6].Style.Fill.BackgroundColor.SetColor(Color.Gray);
		}
	}
}
