namespace API.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using API.Infrastructure;
    using Application.DTOs.User;
    using Application.Service.Interfaces;
    using Application.Response;

    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(
            IUserService userService)
        {
            this.userService = userService;
        }

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerDto) 
        {
            Result<UserDto> result = await this.userService.RegisterAsync(
                registerDto.Email,
                registerDto.Password,
                registerDto.Name,
                registerDto.Roles);

            return Ok(result);
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginDto)
        {
            Result<UserDto> result = await this.userService.LoginAsync(
                loginDto.Email,
                loginDto.Password,
                loginDto.RememberMe);

            return Ok(result);
        }

        [Authorize]
        [Route("logout")]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            Result<Unit> result = await this.userService.LogoutAsync(User!.GetById());

            return Ok(result);
        }
    }
}