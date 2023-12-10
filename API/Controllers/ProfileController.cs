namespace API.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    
    using API.Infrastructure;
    using Application.DTOs.User;
    using Application.Service.Interfaces;
    using Application.Response;

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService profileService;

        public ProfileController(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        [Route("")]
        [HttpGet]
        public async Task<ActionResult> Profile()
        {
            Result<UserProfileDto> userProfileDto = await this.profileService.GetProfile(User.GetById());

            return Ok(userProfileDto);
        }

        [Route("edit")]
        [HttpPatch]
        public async Task<ActionResult> Edit([FromBody] EditUserDto editUserDto)
        {
            Result<Unit> result = await this.profileService.EditUser(
                User.GetById(),
                editUserDto);

            return Ok(result);
        }

        [Route("delete")]
        [HttpDelete]
        public async Task<ActionResult> Delete()
        {
            Result<Unit> result = await this.profileService.DeleteUser(User.GetById());

            return Ok(result);
        }
    }
}