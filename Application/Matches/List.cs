using Domain;
using MediatR;

namespace Application.Matches
{
    public class List 
    {
        //CQRS + MediatR pattern Implemented. 
        //public class Query : IRequest<List<Cat>> {

        //}

        //TODO: fix the login in the architecture
        // public class Handler : IRequestHandler<Query, List<Cat>>
        // {
        //     private readonly DataContext _ctx;
        //     public Handler(DataContext ctx)
        //     {
        //         _ctx = ctx;
        //     }

        //     public async Task<List<Cat>> Handle(Query req, CancellationToken cancellationToken)
        //     {
        //         return await _ctx.Cat.ToListAsync();
        //     }
        // }
    }
}