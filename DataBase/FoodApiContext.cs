using Microsoft.EntityFrameworkCore;

namespace Profit_Food.API.DataBase
{
	public class FoodApiContext : DbContext
	{
		protected override void OnConfiguring
			(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseInMemoryDatabase(databaseName: "FoodApiDB");
		}
		

	}
}
