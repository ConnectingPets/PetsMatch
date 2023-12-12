namespace Application.Marketplace
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using Domain;
    using Response;
    using Domain.Enum;
    using DTOs.Animal;
    using Persistence.Repositories;

    public class AllAnimalsForSale
    {
        public class AllAnimalsForSaleQuery : IRequest<Result<IEnumerable<AllAnimalDto>>>
        {
            public string UserId { get; set; } = null!;
        }

        public class AllAnimalsForSaleQueryHandler : IRequestHandler<AllAnimalsForSaleQuery, Result<IEnumerable<AllAnimalDto>>>
        {
            private readonly IRepository repository;

            public AllAnimalsForSaleQueryHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Result<IEnumerable<AllAnimalDto>>> Handle(AllAnimalsForSaleQuery request, CancellationToken cancellationToken)
            {
                string userId = request.UserId;

                var allAnimals = await repository.
                    AllReadonly<Animal>(a => a.OwnerId.ToString() != userId 
                    && a.AnimalStatus == AnimalStatus.ForSale).
                    Select(a => new AllAnimalDto()
                    {
                        Id = a.AnimalId.ToString(),
                        Name = a.Name,
                        MainPhoto = a.Photos.First(p => p.IsMain).Url
                    }).ToArrayAsync();

                if (!allAnimals.Any())
                {
                    return Result<IEnumerable<AllAnimalDto>>.Failure("We still don't have animals for sale");
                }

                return Result<IEnumerable<AllAnimalDto>>.Success(allAnimals);
            }
        }
    }
}
