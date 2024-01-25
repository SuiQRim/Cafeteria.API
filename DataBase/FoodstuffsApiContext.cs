using Microsoft.EntityFrameworkCore;
using Profit_Food.API.Models;

namespace Profit_Food.API.DataBase
{
	public class FoodstuffsApiContext : DbContext
	{
		public FoodstuffsApiContext(DbContextOptions<FoodstuffsApiContext> options)
			: base(options)
		{
			Database.EnsureCreated();
		}

        public DbSet<Food> Foods { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Food>().HasData(
				new Food
				{
					Id = 1,
					Name = "Hamburger",
					Kcal =  290,
					Price = 100,
					CatalogId = 0
				},
				new Food
				{
					Id = 2,
					Name = "Cheeseburger",
					Kcal = 290,
					Price = 110,
					CatalogId = 0
				},
				new Food
				{
					Id = 3,
					Name = "French fries",
					Kcal = 310,
					Price = 65,
					CatalogId = 1
				},
				new Food
				{
					Id = 4,
					Name = "Nuggets",
					Kcal = 290,
					Price = 70,
					CatalogId = 1
				},
				new Food
				{
					Id = 5,
					Name = "Cola",
					Kcal = 38,
					Price = 65,
					CatalogId = 2
				},
				new Food
				{
					Id = 6,
					Name = "Tea",
					Kcal = 1,
					Price = 20,
					CatalogId = 2
				}
			);
		}

		
	}
}
