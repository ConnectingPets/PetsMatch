namespace API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Domain;
    using API.Infrastructure;
    using Application.DTOs.User;
    using Application.Service.Interfaces;
    using Application.Exceptions.User;
    using static Common.ExceptionMessages.Entity;

    [AllowAnonymous]
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
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerDto) 
        {
            User user;
            try
            {
                user = await this.userService.RegisterAsync(
                    registerDto.Email,
                    registerDto.Password,
                    registerDto.Name);
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
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginDto)
        {
            User user;
            try
            {
                user = await this.userService.LoginAsync(
                    loginDto.Email,
                    loginDto.Password,
                    loginDto.RememberMe);
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

        [Authorize]
        [Route("logout")]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await this.userService.LogoutAsync(User!.GetById());
            }
            catch (UserNotFoundException ex)
            {
                return Unauthorized(ex.Message);
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
                Photo = null,
                Name = user.Name,
                Token = this.tokenService.CreateToken(user)
            };
    }
}