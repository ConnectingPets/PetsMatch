using Application;
using Domain;
using Domain.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Win32;
using System.Security.AccessControl;

namespace API.Controllers
{
    public class UserController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly IMediator mediator;

        public UserController(SignInManager<User> signInManager,
                              UserManager<User> userManager, IMediator mediator)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.mediator = mediator;
        }



        [HttpPost("/register")]
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

            return Ok(new { Message = "Registration successful" });
        }




        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody]LoginUserDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (!result.Succeeded)
                {
                    return Unauthorized(new { Message = "Invalid username or password" });
                }

                return Ok(new { Message = "Login successful" });
            }

            else
            {
                return BadRequest(model);
            }
        }
    }
}
