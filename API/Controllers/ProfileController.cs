namespace API.Controllers
{
    using API.Infrastructure;
    using Application.DTOs.User;
    using Application.Service.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using static Common.ExceptionMessages.Entity;

    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService profileService;

        public ProfileController(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            UserProfileDto userProfileDto;
            try
            {
                userProfileDto = await this.profileService.GetProfile(User.GetById());
            }
            catch
            {
                return StatusCode(500, InternalServerError);
            }

            return Ok(userProfileDto);
        }

        [HttpGet]
        public async Task<ActionResult> Edit()
        {
            ShowEditUserDto userProfileDto;
            try
            {
                userProfileDto = await this.profileService.GetEditModel(User.GetById());
            }
            catch
            {
                return StatusCode(500, InternalServerError);
            }

            return Ok();
        }

        public async Task<ActionResult> Edit([FromBody] EditUserDto editUserDto)
        {
            try
            {
                await this.profileService.EditUser(
                    User.GetById(),
                    editUserDto);
            }
            catch
            {
                return StatusCode(500, InternalServerError);
            }

            return Ok();
        }
    }
}
