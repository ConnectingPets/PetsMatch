﻿namespace API.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Infrastructure;
    using Application.DTOs.Marketplace;
    using static Application.Marketplace.AddAnimalMarketplace;
    using static Application.Marketplace.ShowAnimalMarketplaceToEdit;
    using static Application.Marketplace.EditAnimalMarketplace;
    using static Application.Marketplace.DeleteAnimalMarketplace;

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

        [HttpGet("EditAnimal/{id}")]
        public async Task<IActionResult> EditAnimal([FromRoute] string id)
        {
            ShowAnimalMarketplaceToEditQuery query =
                new ShowAnimalMarketplaceToEditQuery()
                {
                    AnimalId = id,
                    UserId = this.User.GetById()
                };

            var result = await mediator.Send(query);
            return new JsonResult(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateAnimal([FromBody] EditAnimalMarketplaceDto animalDto, [FromRoute] string id)
        {
            EditAnimalMarketplaceCommand command =
                new EditAnimalMarketplaceCommand()
                {
                    AnimalDto = animalDto,
                    AnimalId = id,
                    UserId = this.User.GetById()
                };

            var result = await mediator.Send(command);
            return new JsonResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal([FromRoute] string id)
        {
            DeleteAnimalMarketplaceCommand command =
                new DeleteAnimalMarketplaceCommand()
                {
                    AnimalId = id,
                    UserId = this.User.GetById()
                };

            var result = await mediator.Send(command);
            return new JsonResult(result);
        }
    }
}
