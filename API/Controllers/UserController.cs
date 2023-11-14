using Domain;
using Domain.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;

namespace API.Controllers
{
    public class UserController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public UserController(SignInManager<User> signInManager,
                              UserManager<User> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {


            if (!ModelState.IsValid)
            {
                return View(model);
            }

            User user = new Domain.User()
            {
                Address = model.Address,
                Age = model.Age,
                Email = model.Email,
                City = model.City,
                Description = model.Description,
                Education = model.Education,
                Gender = model.Gender,
                JobTitle = model.JobTitle,
                Name = model.Name,
                Photo = model.Photo,
                Animals = model.Animals,
                UsersPassions = model.UsersPassions
            };

            await userManager.SetEmailAsync(user, user.Email);
            await userManager.SetUserNameAsync(user, user.Name);

            IdentityResult result =
                await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return BadRequest();
            }

            await signInManager.SignInAsync(user, false);

            return Ok(new { Message = "Registration successful" });
        }


        [HttpGet]
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            return Ok(new { Message = "Login successful" });
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(new { Message = "Login successful" });
            }

            return Unauthorized(new { Message = "Invalid username or password" });
        }
    }
}
