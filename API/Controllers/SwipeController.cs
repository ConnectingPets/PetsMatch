namespace API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using API.Infrastructure;
    using Application.DTOs.Swipe;
    using Application.Service.Interfaces;
    using Application.Response;

    using static Common.GeneralApplicationConstants;

    [Authorize(Roles = MatchingRoleName)]
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
            Result<bool> result = await this.swipeService.Swipe(
                swipe.SwiperAnimalId,
                swipe.SwipeeAnimalId,
                swipe.SwipedRight,
                User.GetById());

            return Ok(result);
        }

        [Route("animals")]
        [HttpGet]
        public async Task<ActionResult> AnimalsToSwipe()
        {
            Result<IEnumerable<AnimalToSwipeDto>> result = await this.swipeService.GetAnimalsToSwipe(User.GetById());

            return Ok(result);
        }
    }
}
