using System.ComponentModel.DataAnnotations;

namespace ProfitTest_Cafeteria.API.Models.DTO
{
	public class UserDTO
	{
		[Required]
		[MinLength(5)]
		[MaxLength(24)]
		public string Login { get; set; }

		[Required]
		[MinLength(5)]
		[MaxLength(24)]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
