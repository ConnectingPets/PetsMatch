namespace API.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;

    using Infrastructure;
    using Application.DTOs.User;
    using Application.Service.Interfaces;
    using Application.Response;
    using Domain;

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService profileService;
        private readonly UserManager<User> userManager;

        public ProfileController(IProfileService profileService,
                                 UserManager<User> userManager)
        {
            this.profileService = profileService;
            this.userManager = userManager;
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

        [Route("deleteRole")]
        [HttpDelete]
        public async Task<ActionResult> DeleteRole([FromQuery] string roleName)
        {
            Result<Unit> result =
                await this.profileService.DeleteRole(User.GetById(), roleName);

            return Ok(result);
        }

        [Route("changePassword")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(
            [FromBody] ChangePasswordDto dto)
        {
            var user = await userManager.GetUserAsync(User);

            Result<Unit> result = await profileService.ChangePassword(dto, user!);

            return Ok(result);
        }
    }
}