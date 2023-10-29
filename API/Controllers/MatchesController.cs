using MediatR;

namespace API.Controllers
{
    public class MatchesController : BaseApiController 
    {
        private readonly IMediator _mediator;
        
        public MatchesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // [HttpGet]
        //Routes to be added
        // public async Task<ActionResult<List<Match>>> GetMatches()
        // {
        //     return await _mediator.Send(new List.Query);
        // }
    }
}