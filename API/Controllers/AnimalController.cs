using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AnimalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
