namespace API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    
    using Application.DTOs;
    using Application.Service.Interfaces;
    using Application.Exceptions;

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
            try
            {
                await this.swipeService.Swipe(
                    Guid.Parse(swipe.SwiperAnimalId),
                    Guid.Parse(swipe.SwipeeAnimalId),
                    swipe.SwipedRight);
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
                return StatusCode(500, "Internal Server Error");
            }

            return Ok();
        }
    }
}
