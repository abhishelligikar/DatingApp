using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController :ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            // Validate request

            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();
            if (await _repo.UserExists(userForRegisterDto.Username))
                return BadRequest("User already exists!");

            var UserToCreate = new User {
                Username = userForRegisterDto.Username
            };

            var CreatedUser = await _repo.Register(UserToCreate, userForRegisterDto.Password);

            // ToDo CreatedAtRoute();
            return StatusCode(201);
        }
    }
}