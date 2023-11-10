using Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Register(User model)
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
               PhoneNumber = model.PhoneNumber,
               Photo = model.Photo,
               UserName = model.UserName

            };

            await userManager.SetEmailAsync(user, model.Email);
            await userManager.SetUserNameAsync(user, model.Email);

            IdentityResult result =
                await userManager.CreateAsync(user, "test"); // the "test" must be password of the user but there is no password property in the User model.

            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(model);
            }

            await signInManager.SignInAsync(user, false);

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

           
           /* User model = new User()
            {
                ReturnUrl = returnUrl
            };*/ //In the model there is no ReturnUrl Property

            return View(/*model*/);
        }


        [HttpPost]
        public async Task<IActionResult> Login(User model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result =
                await signInManager.PasswordSignInAsync(model.Email!, "test", /*model.RememberMe*/true, false);


            return View(); //this is just for succesfull building we will make it later
           // return Redirect(/*model.ReturnUrl ?? "/Home/Index"*/);//this should happen later
        }
    }
}
