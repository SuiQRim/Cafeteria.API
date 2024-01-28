using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfitTest_Cafeteria.API.Exceptions;
using ProfitTest_Cafeteria.API.Models;
using ProfitTest_Cafeteria.API.Models.DTO;
using ProfitTest_Cafeteria.API.Services.Repositories.IRepositories;

namespace ProfitTest_Cafeteria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	[Authorize]
	public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UsersController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserDTO user)
        {
            try
            {
                string token = await _repository.Login(user);
				HttpContext.Response.Cookies.Append(AuthOptions.PARAMNAME, token,
                new CookieOptions
                {
	                MaxAge = TimeSpan.FromMinutes(60)
                });
				return Accepted();
			}
            catch (EntityNotFoundExceptions)
            {
                return NotFound("Incorrect Login or Password");
            }
        }


		[HttpDelete("logout")]
		[Authorize]
		public IActionResult Logout()
		{
            // TODO: В идеале реализовать хранение refresher и success токенов и удалять их оттуда в том числе
            HttpContext.Response.Cookies.Delete(AuthOptions.PARAMNAME);
			return Ok();	
		}

		[HttpGet("collection")]
		[Authorize]
		public async Task<ActionResult<IEnumerable<User>>> GetUsers([FromQuery] int start, [FromQuery] int batchSize)
        {
            return Ok(await _repository.GetCollection(start, batchSize));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            return await _repository.GetById(id);
        }


        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditUser(int id, UserDTO user)
        {
            await _repository.Update(user, id);

            return Accepted();
        }


        [HttpPost("add")]
        public async Task<ActionResult<User>> AddUser(UserDTO user)
        {
            await _repository.Add(user);
            return Accepted();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _repository.Delete(id);
            return Accepted();
        }
    }
}
