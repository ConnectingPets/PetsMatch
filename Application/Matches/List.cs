using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Matches
{
    public class List 
    {
        //CQRS + MediatR pattern Implemented. 
        public class Query : IRequest<List<Animal>> {

        }

        TODO: fix the login in the architecture
        public class Handler : IRequestHandler<Query, List<Animal>>
        {
            private readonly DataContext _ctx;
            public Handler(DataContext ctx)
            {
                _ctx = ctx;
            }

            public async Task<List<Animal>> Handle(Query req, CancellationToken cancellationToken)
            {
                return await _ctx.Animals.ToListAsync();
            }
        }
    }
}