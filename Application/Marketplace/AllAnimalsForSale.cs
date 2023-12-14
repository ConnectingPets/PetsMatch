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

    public class AllAnimalsForSale
    {
        public class AllAnimalsForSaleQuery : IRequest<Result<IEnumerable<AllAnimalForSaleDto>>>
        {
            public string UserId { get; set; } = null!;
        }

        public class AllAnimalsForSaleQueryHandler : IRequestHandler<AllAnimalsForSaleQuery, Result<IEnumerable<AllAnimalForSaleDto>>>
        {
            private readonly IRepository repository;

            public AllAnimalsForSaleQueryHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Result<IEnumerable<AllAnimalForSaleDto>>> Handle(AllAnimalsForSaleQuery request, CancellationToken cancellationToken)
            {
                string userId = request.UserId;

                var allAnimals = await repository.
                    AllReadonly<Animal>(a => a.OwnerId.ToString() != userId 
                    && a.AnimalStatus == AnimalStatus.ForSale).
                    Select(a => new AllAnimalForSaleDto()
                    {
                        Id = a.AnimalId.ToString(),
                        Name = a.Name,
                        MainPhoto = a.Photos.First(p => p.IsMain).Url,
                        Price = a.Price
                    }).ToArrayAsync();

                if (!allAnimals.Any())
                {
                    return Result<IEnumerable<AllAnimalForSaleDto>>.Failure(NoAnimalsForSale);
                }

                return Result<IEnumerable<AllAnimalForSaleDto>>.Success(allAnimals);
            }
        }
    }
}
