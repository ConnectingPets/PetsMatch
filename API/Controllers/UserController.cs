using Application;
using Application.DTOs;
using Application.Service.Interfaces;
using Domain;
using Domain.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly ITokenService tokenService;
        private readonly IMediator mediator;

        public UserController(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            IMediator mediator,
            ITokenService tokenService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.mediator = mediator;
            this.tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterUserDto model) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RegisterRequest request = new RegisterRequest(model);


            User user = await mediator.Send(request, CancellationToken.None);

            if (user == null)
            {
                return BadRequest();
            }

            await signInManager.SignInAsync(user, false);

            UserDto userObj = CreateUserObject(user);

            return Ok(userObj);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginUserDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            User? user = await this.userManager
                   .FindByEmailAsync(model.Email);

            if (user == null)
            {
                return Unauthorized();
            }

            var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

            if (!result.Succeeded)
            {
                return Unauthorized(new { Message = "Invalid username or password" });
            }

            UserDto userObj = CreateUserObject(user);

            return Ok(userObj);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return Ok(new { Message = "Logout successful" });
        }

        private UserDto CreateUserObject(User user)
            => new UserDto
            {
                Photo = user.Photo != null 
                    ? Convert.ToBase64String(user.Photo)
                    : null,
                Name = user.Name,
                Token = this.tokenService.CreateToken(user)
            };
    }
}
