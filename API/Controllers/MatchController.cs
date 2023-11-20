namespace API.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Application.DTOs;
    using Application.Service.Interfaces;
    using Application.Exceptions;
    using static Common.ExceptionMessages.Entity;

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
                    matchDto.AnimalOneId,
                    matchDto.AnimalTwoId
                );
            }
            catch (InvalidGuidFormatException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (AnimalNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (AlreadyMatchedException ex)
            {
                return Conflict(ex.Message);
            }
            catch (SameAnimalException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500, InternalServerError);
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
                    unMatchDto.AnimalOneId,
                    unMatchDto.AnimalTwoId
                );
            }
            catch (InvalidGuidFormatException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (AnimalNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (MatchNotFoundException ex)
            {
                return Conflict(ex.Message);
            }
            catch (SameAnimalException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500, InternalServerError);
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
                matches = await this.matchService.GetAnimalMatches(animalId);
            }
            catch (InvalidGuidFormatException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (AnimalNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch
            {
                return StatusCode(500, InternalServerError);
            }

            return Ok(matches);
        }
    }
}