namespace Application.Marketplace
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using Domain;
    using Response;
    using Domain.Enum;
    using Persistence.Repositories;
    using Application.DTOs.Marketplace;

    using static Common.ExceptionMessages.Marketplace;

    public class AllAnimalsForAdoption
    {
        public class AllAnimalsForAdoptionQuery : IRequest<Result<IEnumerable<AllAnimalsMarketplaceDto>>>
        {
            public string UserId { get; set; } = null!;
        }

        public class AllAnimalsForAdoptionQueryHandler : IRequestHandler<AllAnimalsForAdoptionQuery, Result<IEnumerable<AllAnimalsMarketplaceDto>>>
        {
            private readonly IRepository repository;

            public AllAnimalsForAdoptionQueryHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Result<IEnumerable<AllAnimalsMarketplaceDto>>> Handle(AllAnimalsForAdoptionQuery request, CancellationToken cancellationToken)
            {
                string userId = request.UserId;

                var allAnimals = await repository.
                    AllReadonly<Animal>(a => a.OwnerId.ToString() != userId && a.AnimalStatus == AnimalStatus.ForAdoption).
                    Select(a => new AllAnimalsMarketplaceDto()
                    {
                        Id = a.AnimalId.ToString(),
                        MainPhoto = a.Photos.First(a => a.IsMain).Url,
                        Name = a.Name,
                        Breed = a.Breed.Name,
                        Category = a.Breed.Category.Name,
                        City = a.Owner.City!,
                        Gender = a.Gender.ToString(),
                    }).ToArrayAsync();

                if (!allAnimals.Any())
                {
                    return Result<IEnumerable<AllAnimalsMarketplaceDto>>.Failure(NoAnimalsForAdoption);
                }

                return Result<IEnumerable<AllAnimalsMarketplaceDto>>.Success(allAnimals);
            }
        }
    }
}
