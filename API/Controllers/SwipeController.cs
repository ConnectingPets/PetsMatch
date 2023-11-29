namespace API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    
    using Application.Service.Interfaces;
    using Application.Exceptions;
    using Application.DTOs.Swipe;
    using static Common.ExceptionMessages.Entity;
    using Microsoft.AspNetCore.Authorization;
    using API.Infrastructure;

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SwipeController : ControllerBase
    {
        private readonly ISwipeService swipeService;

        public SwipeController(ISwipeService swipeService)
        {
            this.swipeService = swipeService;
        }

        [Route("/swipe")]
        [HttpPost]
        public async Task<ActionResult> Swipe([FromBody] SwipeDto swipe)
        {
            bool isMatch = false;
            try
            {
                isMatch = await this.swipeService.Swipe(
                    swipe.SwiperAnimalId,
                    swipe.SwipeeAnimalId,
                    swipe.SwipedRight);
            }
            catch (InvalidGuidFormatException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (AnimalNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (SameAnimalException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500, InternalServerError);
            }

            return Ok(isMatch);
        }

        [Route("animals")]
        [HttpGet]
        public async Task<ActionResult> AnimalsToSwipe()
        {
            IEnumerable<AnimalToSwipeDto> animalsToSwipe;
            try
            {
                animalsToSwipe = await this.swipeService.GetAnimalsToSwipe(User.GetById());
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

            return Ok(animalsToSwipe);
        }
    }
}
