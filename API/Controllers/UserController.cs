namespace API.Controllers
{
    using Application.DTOs;
    using Application.Exceptions;
    using Application.Service.Interfaces;
    using Domain;
    using Microsoft.AspNetCore.Mvc;
    using static Common.ExceptionMessages.Entity;

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly ITokenService tokenService;

        public UserController(
            IUserService userService,
            ITokenService tokenService)
        {
            this.userService = userService;
            this.tokenService = tokenService;
        }

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterUserDto model) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user;
            try
            {
                user = await this.userService.RegisterAsync(
                    model.Email,
                    model.Password,
                    model.Name);
            }
            catch (UserResultNotSucceededException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch
            {
                return StatusCode(500, InternalServerError);
            }

            UserDto userObj = CreateUserObject(user);

            return Ok(userObj);
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUserDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            User user;
            try
            {
                user = await this.userService.LoginAsync(
                    model.Email,
                    model.Password,
                    model.RememberMe);
            }
            catch (UserNotFoundException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (UserResultNotSucceededException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch
            {
                return StatusCode(500, InternalServerError);
            }

            UserDto userObj = CreateUserObject(user);

            return Ok(userObj);
        }

        [Route("logout")]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await this.userService.LogoutAsync();
            }
            catch
            {
                return StatusCode(500, InternalServerError);
            }

            return Ok();
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
