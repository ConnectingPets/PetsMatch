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

    public class MyAnimalsForAdoption
    {
        public class MyAnimalsForAdoptionQuery : IRequest<Result<IEnumerable<AllAnimalDto>>>
        {
            public string UserId { get; set; } = null!;
        }

        public class MyAnimalsForAdoptionQueryHandler : IRequestHandler<MyAnimalsForAdoptionQuery, Result<IEnumerable<AllAnimalDto>>>
        {
            private readonly IRepository repository;

            public MyAnimalsForAdoptionQueryHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Result<IEnumerable<AllAnimalDto>>> Handle(MyAnimalsForAdoptionQuery request, CancellationToken cancellationToken)
            {
                string userId = request.UserId;

                var allAnimals = await repository
                    .AllReadonly<Animal>(a => a.OwnerId.ToString() == userId && a.AnimalStatus == AnimalStatus.ForAdoption).
                    Select(a => new AllAnimalDto()
                    {
                        Id = a.AnimalId.ToString(),
                        MainPhoto = a.Photos.First(a => a.IsMain).Url,
                        Name = a.Name,
                    }).ToArrayAsync();

                if (!allAnimals.Any())
                {
                    return Result<IEnumerable<AllAnimalDto>>.Failure("You still don't have animal for adoption");
                }

                return Result<IEnumerable<AllAnimalDto>>.Success(allAnimals);
            }
        }
    }
}
