namespace API.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Infrastructure;
    using Application.DTOs.Animal;
    using static Application.Animal.AddAnimalMarketplace;

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MarketplaceController : ControllerBase
    {
        private readonly IMediator mediator;

        public MarketplaceController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAnimal(
            [FromForm] AddAnimalMarketplaceDto animalDto)
        {
            AddAnimalMarketplaceCommand command =
                new AddAnimalMarketplaceCommand()
                {
                    OwnerId = this.User.GetById(),
                    AnimalDto = animalDto
                };

            var result = await mediator.Send(command);
            return new JsonResult(result);

        }
    }
}
