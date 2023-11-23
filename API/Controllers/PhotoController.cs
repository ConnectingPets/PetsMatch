namespace API.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using MediatR;

    using Infrastructure;
    using static Application.Photo.AddUserPhoto;
    using static Application.Photo.AddAnimalPhoto;

    public class PhotoController : BaseApiController
    {
        private readonly IMediator mediator;

        public PhotoController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("AddUserPhoto")]
        public async Task<IActionResult> AddUserPhoto(IFormFile file)
        {
            AddUserPhotoCommand command = new AddUserPhotoCommand()
            {
                File = file,
                UserId = this.User.GetById()
            };

            var result = await mediator.Send(command);
            return new JsonResult(result);
        }

        [HttpPost("SetUserMain")]
        public async Task<IActionResult> SetMain(IFormFile file)
        {
            return Ok();
        }

        [HttpPost("DeleteUserPhoto")]
        public async Task<IActionResult> Delete(string photoId)
        {
            return Ok();
        }

        [HttpPost("AddAnimalPhoto")]
        public async Task<IActionResult> AddAnimalPhoto(IFormFile file, string animalId)
        {
            AddAnimalPhotoCommand command = new AddAnimalPhotoCommand()
            {
                File = file,
                AnimalId = animalId
            };

            var result = await mediator.Send(command);
            return new JsonResult(result);
        }

        [HttpPost("SetAnimalMain")]
        public async Task<IActionResult> SetMain(IFormFile file, string animalId)
        {
            return Ok();
        }

        [HttpPost("DeleteAnimalPhoto")]
        public async Task<IActionResult> Delete(string photoId, string animalId)
        {
            return Ok();
        }
    }
}
