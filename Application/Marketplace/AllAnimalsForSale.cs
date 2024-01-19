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
        public class AllAnimalsForSaleQuery : IRequest<Result<IEnumerable<AllAnimalsForSaleDto>>>
        {
            public string UserId { get; set; } = null!;
        }

        public class AllAnimalsForSaleQueryHandler : IRequestHandler<AllAnimalsForSaleQuery, Result<IEnumerable<AllAnimalsForSaleDto>>>
        {
            private readonly IRepository repository;

            public AllAnimalsForSaleQueryHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Result<IEnumerable<AllAnimalsForSaleDto>>> Handle(AllAnimalsForSaleQuery request, CancellationToken cancellationToken)
            {
                string userId = request.UserId;

                var allAnimals = await repository.
                    AllReadonly<Animal>(a => a.OwnerId.ToString() != userId 
                    && a.AnimalStatus == AnimalStatus.ForSale).
                    Select(a => new AllAnimalsForSaleDto()
                    {
                        Id = a.AnimalId.ToString(),
                        Name = a.Name,
                        MainPhoto = a.Photos.First(p => p.IsMain).Url,
                        Price = a.Price,
                        Breed = a.Breed.Name,
                        Category = a.Breed.Category.Name,
                        City = a.Owner.City!,
                        Gender = a.Gender.ToString(),
                    }).ToArrayAsync();

                if (!allAnimals.Any())
                {
                    return Result<IEnumerable<AllAnimalsForSaleDto>>.Failure(NoAnimalsForSale);
                }

                return Result<IEnumerable<AllAnimalsForSaleDto>>.Success(allAnimals);
            }
        }
    }
}
