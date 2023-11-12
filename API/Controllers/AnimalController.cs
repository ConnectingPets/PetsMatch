namespace API.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    using Infrastructure;
    using Application.DTOs;
    using static Application.Animal.AddAnimal;
    using static Application.Animal.AllAnimal;
    using static Application.Animal.ShowAnimalToAdd;

    public class AnimalController : BaseApiController
    {
        private readonly IMediator mediator;

        public AnimalController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAnimal(AddAnimalDto animalDto)
        {
            string userId = this.User.GetById();
            animalDto.OwnerId = userId;

            AddAnimalCommand command = new AddAnimalCommand()
            {
                AnimalDto = animalDto
            };

            await mediator.Send(command);

            return Ok(animalDto);
        }

        [HttpGet("AddAnimal")]
        public async Task<IActionResult> AddAnimal()
        {
            var animal = await mediator.Send(new ShowAnimalToAddQuery());
            return Ok(animal);
        }

        [HttpGet("AllAnimals")]
        public async Task<IActionResult> GetAllAnimals()
        {
            string ownerId = this.User.GetById();

            AllAnimalQuery allAnimalQuery = new AllAnimalQuery()
            {
                OwnerId = ownerId
            };

            var allAnimals = await mediator.Send(allAnimalQuery);

            return Ok(allAnimals);
        }  

    }
}
