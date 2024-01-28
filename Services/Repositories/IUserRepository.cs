using ProfitTest_Cafeteria.API.Models;
using ProfitTest_Cafeteria.API.Models.DTO;

namespace ProfitTest_Cafeteria.API.Services.Repositories
{
	public interface IUserRepository
	{
		public Task<string> Login(UserDTO user);

		public Task Add(UserDTO user);

		public Task Update(UserDTO user, int id);

		public Task Delete(int id);

		public Task<User> GetById(int id);

		public Task<IEnumerable<User>> GetCollection(int pos, int take);

	}
}
