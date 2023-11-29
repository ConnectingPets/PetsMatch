namespace Application.Breed
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using DTOs;
    using Domain;
    using Response;
    using Persistence.Repositories;

    public class AllBreeds
    {
        public class AllBreedsQuery : IRequest<Result<IEnumerable<BreedDto>>>
        {
            public int CategoryId { get; set; }
        }

        public class AllBreedsQueryHandler :
            IRequestHandler<AllBreedsQuery, Result<IEnumerable<BreedDto>>>
        {
            private readonly IRepository repository;

            public AllBreedsQueryHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Result<IEnumerable<BreedDto>>> Handle(AllBreedsQuery request, CancellationToken cancellationToken)
            {
                BreedDto[] breeds = await repository.
                   All<Breed>().Where(b => b.CategoryId == request.CategoryId).
                   Select(b => new BreedDto()
                   {
                       BreedId = b.BreedId,
                       Name = b.Name,
                   }).ToArrayAsync();

                return Result<IEnumerable<BreedDto>>.Success(breeds);
            }
        }
    }
}
