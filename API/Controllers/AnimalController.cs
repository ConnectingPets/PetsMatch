namespace API.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using MediatR;

    using Application.DTOs;
    using static Application.Animal.AddAnimal;

    public class AnimalController : BaseApiController
    {
        private readonly IMediator mediator;

        public AnimalController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddAnimal(AddAnimalDto animalDto)
        {
            await mediator.Send(new AddAnimalCommand(animalDto));
            return null;
        }

    }
}
