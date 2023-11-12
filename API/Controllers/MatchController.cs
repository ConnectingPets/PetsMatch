namespace API.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Application.DTOs;
    using Application.Service.Interfaces;

    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase 
    {
        private readonly IMatchService matchService;
        
        public MatchController(IMatchService matchService)
        {
            this.matchService = matchService;
        }

        [Route("match")]
        public async Task<ActionResult> Match(Guid animalOneId, Guid animalTwoId, bool swipedRight)
        {
            try
            {
                await this.matchService.Match(animalOneId, animalTwoId, swipedRight);
            }
            catch (Exception)
            {
                throw;
            }

            return Ok();
        }

        [Route("unmatch")]
        public async Task<ActionResult> UnMatch(Guid animalOneId, Guid animalTwoId)
        {
            try
            {
                await this.matchService.UnMatch(animalOneId, animalTwoId);
            }
            catch (Exception)
            {
                throw;
            }

            return Ok();
        }

        [Route("animal-matches")]
        public async Task<ActionResult> AnimalMatches(Guid animalId)
        {
            IEnumerable<AnimalMatchDto> matches;
            try
            {
                matches = await this.matchService.GetAnimalMatches(animalId);
            }
            catch (Exception)
            {
                throw;
            }

            return Ok();
        }
    }
}