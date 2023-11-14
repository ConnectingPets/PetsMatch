namespace API.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Application.DTOs;
    using Application.Service.Interfaces;
    using Application.Exceptions;

    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase 
    {
        private readonly IMatchService matchService;
        
        public MatchController(IMatchService matchService)
        {
            this.matchService = matchService;
        }

        [Route("/match")]
        [HttpPost]
        public async Task<ActionResult> Match([FromBody] MatchDto matchDto)
        {
            try
            {
                await this.matchService.Match(
                    Guid.Parse(matchDto.AnimalOneId),
                    Guid.Parse(matchDto.AnimalTwoId)
                );
            }
            catch (AnimalNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (AlreadyMatchedException ex)
            {
                return Conflict(ex.Message);
            }
            catch
            {
                return StatusCode(500, "Internal Server Error");
            }

            return Ok();
        }

        [Route("/unmatch")]
        [HttpPost]
        public async Task<ActionResult> UnMatch([FromBody] UnMatchDto unMatchDto)
        {
            try
            {
                await this.matchService.UnMatch(
                    Guid.Parse(unMatchDto.AnimalOneId),
                    Guid.Parse(unMatchDto.AnimalTwoId)
                );
            }
            catch (AnimalNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (MatchNotFoundException ex)
            {
                return Conflict(ex.Message);
            }
            catch
            {
                return StatusCode(500, "Internal Server Error");
            }

            return Ok();
        }

        [Route("/animal-matches")]
        [HttpGet]
        public async Task<ActionResult> AnimalMatches([FromQuery] string animalId)
        {
            IEnumerable<AnimalMatchDto> matches;
            try
            {
                matches = await this.matchService.GetAnimalMatches(
                    Guid.Parse(animalId)
                );
            }
            catch (AnimalNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch
            {
                return StatusCode(500, "Internal Server Error");
            }

            return Ok(matches);
        }
    }
}