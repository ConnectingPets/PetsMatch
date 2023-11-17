namespace API.Controllers
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Mvc;
    using MediatR;

    using Application;
    using Domain.ViewModels;
    using Infrastructure;
    using Application.DTOs;
    using static Application.Animal.AddAnimal;
    using static Application.Animal.AllAnimal;
    using static Application.Animal.ShowAnimalToAdd;
    using static Application.Animal.DeleteAnimal;
    using static Application.Animal.ShowAnimalToEdit;
    using static Application.Animal.EditAnimal;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class AnimalController : BaseApiController
    {
        private readonly IMediator mediator;

        public AnimalController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAnimal([FromBody] AddAnimalDto animalDto)
        {
            string ownerId = this.User.GetById();

            AddAnimalCommand command = new AddAnimalCommand()
            {
                AnimalDto = animalDto,
                OwnerId = this.User.GetById()
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
        public async Task<IActionResult> DeleteAnimal([FromRoute] string id)
        {
            DeleteAnimalCommand command = new DeleteAnimalCommand()
            {
                AnimalId = id,
                UserId = this.User.GetById()
            };

            return new JsonResult(await mediator.Send(command));
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateAnimal([FromBody] EditAnimalDto animalDto, string id)
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
        public async Task<IActionResult> EditAnimal([FromRoute] string id)
        {
            ShowAnimalToEditQuery query = new ShowAnimalToEditQuery()
            {
                AnimalId = id,
                UserId = this.User.GetById()
            };

            return new JsonResult(await mediator.Send(query));
        }
        public async Task<IActionResult> AddAnimal(AnimalViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            CreateAnimalRequest request = new CreateAnimalRequest(model, userId);

            bool result = await mediator.Send(request, CancellationToken.None);

            if (result == false)
            {
                return BadRequest(model);
            }

            return Ok(model);
        }

    }
}