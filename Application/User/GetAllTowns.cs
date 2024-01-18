namespace Application.User
{
    using Application.Response;
    using MediatR;
    using Persistence.Repositories;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain;
    using Microsoft.EntityFrameworkCore;

    public class GetAllTowns
    {
        public class GetAllTownsQuery : IRequest<Result<IEnumerable<string>>>
        {

        }

        public class GetAllTownsQueryHandler :
            IRequestHandler<GetAllTownsQuery, Result<IEnumerable<string>>>
        {
            private readonly IRepository repository;

            public GetAllTownsQueryHandler(IRepository repository)
            {
                this.repository = repository;
            }


            public async Task<Result<IEnumerable<string>>> Handle(GetAllTownsQuery request, CancellationToken cancellationToken)
            {
                var towns = await repository.AllReadonly<User>().
                    Select(u => u.City).Distinct().ToArrayAsync();

                return Result<IEnumerable<string>>.Success(towns);
            }
        }
    }
}
