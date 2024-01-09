namespace API.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Infrastructure;
    using Application.DTOs.Animal;

    using static Application.Breed.AllBreeds;
    using static Application.Animal.AddAnimal;
    using static Application.Animal.AllAnimal;
    using static Application.Animal.EditAnimal;
    using static Application.Animal.DeleteAnimal;
    using static Application.Animal.ShowAnimalToEdit;
    using static Application.AnimalCategory.AllAnimalCategories;
    using static Common.GeneralApplicationConstants;
    using static Application.Animal.AnimalProfile;

    //[Authorize(Roles = MatchingRoleName)]
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalController : ControllerBase
    {
        private readonly IMediator mediator;

        public AnimalController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAnimal([FromForm]AddAnimalDto animal)
        {
            string ownerId = this.User.GetById();

            AddAnimalCommand command = new AddAnimalCommand()
            {
                AnimalDto = animal,
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
        public async Task<IActionResult> UpdateAnimal([FromBody] EditAnimalDto animalDto, [FromRoute]string id)
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

        [HttpGet("AnimalProfile/{animalId}")]
        public async Task<IActionResult> AnimalProfile(string animalId)
        {
            AnimalProfileQuery animalProfileQuery = new AnimalProfileQuery
            {
                AnimalId = animalId
            };

            var animalProfile = await mediator.Send(animalProfileQuery);

            return new JsonResult(animalProfile);
        }
    }
}