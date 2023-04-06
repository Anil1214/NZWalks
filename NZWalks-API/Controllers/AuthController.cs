using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks_API.Models.DTO;
using NZWalks_API.Repositories;

namespace NZWalks_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenHandler tokenHandler;

        public AuthController(IUserRepository userRepository, ITokenHandler tokenHandler)
        {
            this.userRepository = userRepository;
            this.tokenHandler = tokenHandler;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(LoginRequest loginRequest)
        {
            var user  = await userRepository.AuthenticateAsync(loginRequest.Username, loginRequest.Password);
            if (user != null)
            {
                //Create and return Jwt Token
                var token = await tokenHandler.CreateTokenAsync(user);
                return Ok(token);
            }
            return BadRequest("Username or password not found");
        }
    }
}
