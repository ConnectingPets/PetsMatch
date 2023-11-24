namespace API.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using MediatR;

    using Infrastructure;
    using static Application.Photo.AddUserPhoto;
    using static Application.Photo.AddAnimalPhoto;
    using static Application.Photo.DeletePhoto;
    using static Application.Photo.SetUserMainPhoto;
    using static Application.Photo.SetAnimaMainPhoto;

    [Authorize]
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

        [HttpPost("SetUserMain/{photoId}")]
        public async Task<IActionResult> SetUserMain([FromRoute]string photoId)
        {
            SetUserMainPhotoCommand command = new SetUserMainPhotoCommand()
            {
                PhotoId = photoId
            };

            var result = await mediator.Send(command);
            return new JsonResult(result);
        }

        [HttpPost("DeletePhoto/{photoId}")]
        public async Task<IActionResult> Delete([FromRoute]string photoId)
        {
            DeletePhotoCommand command = new DeletePhotoCommand()
            {
                PublicId = photoId
            };

            var result = await mediator.Send(command);
            return new JsonResult(result);
        }

        [HttpPost("AddAnimalPhoto/{animalId}")]
        public async Task<IActionResult> AddAnimalPhoto(IFormFile file, [FromRoute] string animalId)
        {
            AddAnimalPhotoCommand command = new AddAnimalPhotoCommand()
            {
                File = file,
                AnimalId = animalId
            };

            var result = await mediator.Send(command);
            return new JsonResult(result);
        }

        [HttpPost("SetAnimalMain/{photoId}")]
        public async Task<IActionResult> SetAnimalMain([FromRoute] string photoId)
        {
            SetAnimalMainPhotoCommand command =
                new SetAnimalMainPhotoCommand()
                {
                    PhotoId = photoId
                };

            var result = await mediator.Send(command);
            return new JsonResult(result);
        }
    }
}
