namespace ProfitTest_Cafeteria.API.Models
{
	public class FoodCatalog
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public List<Food> Foods { get; set; }
	}
}
