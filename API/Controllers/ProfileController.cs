﻿namespace API.Controllers
{
    using API.Infrastructure;
    using Application.DTOs.User;
    using Application.Exceptions;
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

        [Route("")]
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            UserProfileDto userProfileDto;
            try
            {
                userProfileDto = await this.profileService.GetProfile(User.GetById());
            }
            catch (InvalidGuidFormatException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch
            {
                return StatusCode(500, InternalServerError);
            }

            return Ok(userProfileDto);
        }

        [Route("edit")]
        [HttpPost]
        public async Task<ActionResult> Edit([FromBody] EditUserDto editUserDto)
        {
            try
            {
                await this.profileService.EditUser(
                    User.GetById(),
                    editUserDto);
            }
            catch (InvalidGuidFormatException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidEnumException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500, InternalServerError);
            }

            return Ok();
        }

        [Route("delete")]
        [HttpPost]
        public async Task<ActionResult> Delete()
        {
            try
            {
                await this.profileService.DeleteUser(User.GetById());
            }
            catch (InvalidGuidFormatException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch
            {
                return StatusCode(500, InternalServerError);
            }

            return Ok();
        }
    }
}
