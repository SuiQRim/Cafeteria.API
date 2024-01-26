﻿using Microsoft.EntityFrameworkCore;
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
			modelBuilder.Entity<FoodCatalog>()
				.HasMany(e => e.Foods)
				.WithOne()
				.HasForeignKey(e => e.CatalogId)
				.IsRequired();

			List<FoodCatalog> catalogs = new()
			{
				new () {
					Id = 1,
					Name = "Burgers"
				},
				new () {	
					Id = 2,
					Name = "Snacks"
				},
				new () {
					Id = 3,
					Name = "Drinks"
				},
			};

			var foods = new List<Food>() { 
				new (){
					Id = 1,
					Name = "Hamburger",
					Kcal =  290,
					Price = 100,
					CatalogId = 1,
				},
				new () {
					Id = 2,
					Name = "Cheeseburger",
					Kcal = 290,
					Price = 110,
					CatalogId = 1,
				},
				new () {
					Id = 3,
					Name = "French fries",
					Kcal = 310,
					Price = 65,
					CatalogId = 2,
				},
				new () {
					Id = 4,
					Name = "Nuggets",
					Kcal = 290,
					Price = 70,
					CatalogId = 2,
				},
				new () {
					Id = 5,
					Name = "Cola",
					Kcal = 38,
					Price = 65,
					CatalogId = 3,
				},
				new() {
					Id = 6,
					Name = "Tea",
					Kcal = 1,
					Price = 20,
					CatalogId = 3,
				}
			};

			modelBuilder.Entity<FoodCatalog>().HasData(catalogs);
			modelBuilder.Entity<Food>().HasData(foods);

		}
		public DbSet<FoodCatalog> FoodCatalogs { get; set; } = default!;

		
	}
}
