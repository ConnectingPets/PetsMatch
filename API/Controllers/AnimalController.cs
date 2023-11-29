namespace API.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Infrastructure;
    using Application.DTOs;
    using static Application.Animal.AddAnimal;
    using static Application.Animal.AllAnimal;
    using static Application.Animal.DeleteAnimal;
    using static Application.Animal.EditAnimal;
    using static Application.Animal.ShowAnimalToEdit;
    using static Application.AnimalCategory.AllAnimalCategories;
    using static Application.Breed.AllBreeds;

    //[Authorize]
    public class AnimalController : BaseApiController
    {
        private readonly IMediator mediator;

        public AnimalController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAnimal([FromBody] AddOrEditAnimalDto animalDto)
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
        public async Task<IActionResult> UpdateAnimal([FromBody] AddOrEditAnimalDto animalDto, string id)
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

        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var allCategories = await 
                mediator.Send(new AllAnimalCategoriesQuery());

            return new JsonResult(allCategories);
        }

        [HttpGet("GetBreeds/{categoryId}")]
        public async Task<IActionResult> GetBreeds(int categoryId)
        {
            AllBreedsQuery query = new AllBreedsQuery()
            {
                CategoryId = categoryId,
            };

            var allCategories = await mediator.Send(query);
            return new JsonResult(allCategories);
        }
    }
}