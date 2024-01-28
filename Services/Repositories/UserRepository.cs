using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProfitTest_Cafeteria.API.DataBase;
using ProfitTest_Cafeteria.API.Exceptions;
using ProfitTest_Cafeteria.API.Models;
using ProfitTest_Cafeteria.API.Models.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ProfitTest_Cafeteria.API.Services.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly FoodstuffsApiContext _context;

		public UserRepository(FoodstuffsApiContext context)
        {
            _context = context;
        }
        public async Task Add(UserDTO user)
		{
			await _context.AddAsync(user);
			await _context.SaveChangesAsync();
		}

		public async Task Update(UserDTO user, int id)
		{
			User? entity = await GetById(id);

			if (entity == null)
				throw new EntityNotFoundExceptions(typeof(Food), id.ToString());

			entity.Login = user.Login;
			entity.Password = user.Password;

			await _context.SaveChangesAsync();
		}

		public async Task Delete(int id)
		{
			User? user = await GetById(id);

			if (user == null)
				throw new EntityNotFoundExceptions(typeof(User), id.ToString());

			_context.Users.Remove(user);
			await _context.SaveChangesAsync();
		}

		public async Task<User> GetById(int id)
		{
			User? user = await _context.Users.FindAsync(id);

			if (user == null)
				throw new EntityNotFoundExceptions(typeof(User), id.ToString());

			return user;
		}

		public async Task<IEnumerable<User>> GetCollection(int pos = 0, int take = 20)
		{
			return await _context.Users.Skip(pos).Take(take).ToArrayAsync();
		}

		public async Task<string> Login(UserDTO user)
		{
			User? userFromDB = await _context.Users
				.SingleOrDefaultAsync(u => u.Login == user.Login && u.Password == user.Password);

			if (userFromDB == null)
				throw new EntityNotFoundExceptions();

			//TODO: Не уверен что это должно токен должен генерироваться на этом слое и таким образом

			var claims = new List<Claim> {
				new (ClaimTypes.Name, user.Login)
			};

			JwtSecurityToken jwt = new(
				issuer: AuthOptions.ISSUER,
				audience: AuthOptions.AUDIENCE,
				claims: claims,
				expires: DateTime.UtcNow.Add(TimeSpan.FromHours(AuthOptions.LIFETIME)),
				signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

			return new JwtSecurityTokenHandler().WriteToken(jwt);

		}
	}
}
