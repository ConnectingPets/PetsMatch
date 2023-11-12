namespace API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    
    using Application.DTOs;
    using Application.Service.Interfaces;

    [Route("api/[controller]")]
    [ApiController]
    public class SwipeController : ControllerBase
    {
        private readonly ISwipeService swipeService;

        public SwipeController(ISwipeService swipeService)
        {
            this.swipeService = swipeService;
        }

        [Route("swipe")]
        public async Task<ActionResult> Swipe([FromBody] SwipeDto swipe)
        {
            try
            {
                await this.swipeService.Swipe(swipe.SwiperAnimalId, swipe.SwipeeAnimalId, swipe.SwipedRight);
            }
            catch (Exception)
            {
                throw;
            }

            return Ok();
        }
    }
}
