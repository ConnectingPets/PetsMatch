namespace API.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

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
            return Ok();
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
            return Ok();
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
