using dotnet_rpg.Dtos.User;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;

        public AuthController(IAuthRepository authRepo)
        {
            this._authRepo = authRepo;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto newUser)
        {
            var response = await _authRepo.Register(
                new User { Username = newUser.Username }, newUser.Password
            );
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<int>>> Login(UserLoginDto user)
        {
            var response = await _authRepo.Login(user.Username, user.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}