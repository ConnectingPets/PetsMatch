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

    public class MyAnimalsForSale
    {
        public class MyAnimalsForSaleQuery : IRequest<Result<IEnumerable<AllAnimalsForSaleDto>>>
        {
            public string UserId { get; set; } = null!;
        }

        public class MyAnimalForSaleQueryHandler : IRequestHandler<MyAnimalsForSaleQuery, Result<IEnumerable<AllAnimalsForSaleDto>>>
        {
            private readonly IRepository repository;

            public MyAnimalForSaleQueryHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Result<IEnumerable<AllAnimalsForSaleDto>>> Handle(MyAnimalsForSaleQuery request, CancellationToken cancellationToken)
            {
                string userId = request.UserId;

                var allAnimals = await repository
                    .AllReadonly<Animal>(a => a.OwnerId.ToString() == userId && a.AnimalStatus == AnimalStatus.ForSale).
                    Select(a => new AllAnimalsForSaleDto()
                    {
                        Id = a.AnimalId.ToString(),
                        MainPhoto = a.Photos.First(a => a.IsMain).Url,
                        Name = a.Name,
                        Price = a.Price
                    }).ToArrayAsync();

                if (!allAnimals.Any())
                {
                    return Result<IEnumerable<AllAnimalsForSaleDto>>.Failure(DoNotHaveAnimalForSale);
                }

                return Result<IEnumerable<AllAnimalsForSaleDto>>.Success(allAnimals);
            }
        }
    }
}
