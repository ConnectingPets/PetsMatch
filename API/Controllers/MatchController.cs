namespace API.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    
    using Application.Response;
    using Application.DTOs.Match;
    using Application.Service.Interfaces;
    using API.Infrastructure;

    using static Common.GeneralApplicationConstants;

    [Authorize(Roles = MatchingRoleName)]
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
            Result<Unit> result = await this.matchService.Match(
                matchDto.AnimalOneId,
                matchDto.AnimalTwoId,
                User.GetById());

            return Ok(result);
        }

        [Route("/unmatch")]
        [HttpPost]
        public async Task<ActionResult> UnMatch([FromBody] UnMatchDto unMatchDto)
        {
            Result<Unit> result = await this.matchService.UnMatch(
                unMatchDto.AnimalOneId,
                unMatchDto.AnimalTwoId,
                User.GetById());

            return Ok(result);
        }

        [Route("/animal-matches")]
        [HttpGet]
        public async Task<ActionResult> AnimalMatches([FromQuery] string animalId)
        {
            Result<IEnumerable<AnimalMatchDto>> result = await this.matchService.GetAnimalMatches(
                animalId,
                User.GetById());

            return Ok(result);
        }
    }
}