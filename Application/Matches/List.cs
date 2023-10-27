using Domain;
using MediatR;
using Persistence;

namespace Application.Matches
{
    public class List 
    {
        //CQRS + MediatR pattern Implemented. 
        public class Query : IRequest<List<Match>> {

        }

        public class Handler : IRequestHandler<Query, List<Match>>
        {
            private readonly DataContext _ctx;
            public Handler(DataContext ctx)
            {
                _ctx = ctx;
            }

            public async Task<List<Match>> Handle(Query req, CancellationToken cancellationToken)
            {
                return await _ctx.Matches.ToListAsync();
            }
        }
    }
}