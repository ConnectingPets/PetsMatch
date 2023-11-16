using Application;
using Domain.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    public class AnimalController : Controller
    {
        private readonly IMediator mediator;

        public AnimalController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> AddAnimal(AnimalViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            CreateAnimalRequest request = new CreateAnimalRequest(model, userId);

           bool result  = await mediator.Send(request, CancellationToken.None);



            if (result == false)
            {
                return BadRequest(model);
            }

            return Ok(model);
        }
    }
}
