namespace API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MediatR;

    using Infrastructure;
    using Application.DTOs;
    using static Application.Animal.AddAnimal;
    using static Application.Animal.AllAnimal;
    using static Application.Animal.ShowAnimalToAdd;
    using static Application.Animal.DeleteAnimal;
    using static Application.Animal.ShowAnimalToEdit;
    using static Application.Animal.EditAnimal;

    public class AnimalController : BaseApiController
    {
        private readonly IMediator mediator;

        public AnimalController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAnimal(EditOrAddAnimalDto animalDto)
        {
            string ownerId = this.User.GetById();

            AddAnimalCommand command = new AddAnimalCommand()
            {
                AnimalDto = animalDto,
                OwnerId = ownerId
            };

            var result = await mediator.Send(command);
            return new JsonResult(result);
        }

        [HttpGet("AddAnimal")]
        public async Task<IActionResult> AddAnimal()
        {
            var animal = await mediator.Send(new ShowAnimalToAddQuery());
            return new JsonResult(animal);
        }

        [HttpGet("AllAnimals")]
        public async Task<IActionResult> GetAllAnimals()
        {
            string ownerId = this.User.GetById();

            AllAnimalQuery query = new AllAnimalQuery()
            {
                OwnerId = ownerId
            };

            var allAnimals = await mediator.Send(query);
            return new JsonResult(allAnimals);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal(string id)
        {
            DeleteAnimalCommand command = new DeleteAnimalCommand()
            {
                AnimalId = id,
                UserId = this.User.GetById()
            };

            return new JsonResult(await mediator.Send(command));
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateAnimal(EditOrAddAnimalDto animalDto, string id)
        {
            EditAnimalCommand command = new EditAnimalCommand()
            {
                AnimalDto = animalDto,
                AnimalId = id,
                UserId = this.User.GetById()
            };

            return new JsonResult(await mediator.Send(command));
        }

        [HttpGet("EditAnimal/{id}")]
        public async Task<IActionResult> EditAnimal(string id)
        {
            ShowAnimalToEditQuery query = new ShowAnimalToEditQuery()
            {
                AnimalId = id,
                UserId = this.User.GetById()
            };

            return new JsonResult(await mediator.Send(query));
        }

    }
}
