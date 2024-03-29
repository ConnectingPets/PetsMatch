﻿namespace API.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Infrastructure;

    using static Application.Photo.AddUserPhoto;
    using static Application.Photo.AddAnimalPhoto;
    using static Application.Photo.DeleteAnimalPhoto;    
    using static Application.Photo.SetAnimaMainPhoto;
    using static Application.Photo.DeleteUserPhoto;

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PhotoController : ControllerBase
    {
        private readonly IMediator mediator;

        public PhotoController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("AddUserPhoto")]
        public async Task<IActionResult> AddUserPhoto([FromForm] IFormFile file)
        {
            AddUserPhotoCommand command = new AddUserPhotoCommand()
            {
                File = file,
                UserId = this.User.GetById()
            };

            var result = await mediator.Send(command);
            return new JsonResult(result);
        }

        [HttpDelete("DeleteAnimalPhoto/{photoId}")]
        public async Task<IActionResult> DeleteAnimalPhoto([FromRoute]string photoId)
        {
            DeleteAnimalPhotoCommand command = new DeleteAnimalPhotoCommand()
            {
                PhotoId = photoId
            };

            var result = await mediator.Send(command);
            return new JsonResult(result);
        }

        [HttpDelete("DeleteUserPhoto")]
        public async Task<IActionResult> DeleteUserPhoto()
        {
            DeleteUserPhotoCommand command = new DeleteUserPhotoCommand()
            {
                UserId = this.User.GetById()
            };

            var result = await mediator.Send(command);
            return new JsonResult(result);
        }

        [HttpPost("AddAnimalPhoto/{animalId}")]
        public async Task<IActionResult> AddAnimalPhoto([FromForm] IFormFile file, [FromRoute] string animalId)
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
