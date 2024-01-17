namespace Application.Marketplace
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using Domain;
    using Response;
    using DTOs.Photo;
    using DTOs.Marketplace;
    using Persistence.Repositories;

    using static Common.ExceptionMessages.Animal;

    public class GetAnimal
    {
        public class GetAnimalQuery : IRequest<Result<GetAnimalDto>>
        {
            public string AnimalId { get; set; } = null!;
        }

        public class GetAnimalQueryHandler :
            IRequestHandler<GetAnimalQuery, Result<GetAnimalDto>>
        {
            private readonly IRepository repository;

            public GetAnimalQueryHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Result<GetAnimalDto>> Handle(GetAnimalQuery request, CancellationToken cancellationToken)
            {
                string animalId = request.AnimalId;
                Animal? animal = await repository.
                    All<Animal>(a => a.AnimalId.ToString() == animalId).
                    Include(a => a.Owner).
                    Include(a => a.Breed).
                    Include(a => a.Photos).
                    FirstOrDefaultAsync();

                if (animal == null)
                {
                    return Result<GetAnimalDto>.Failure(AnimalNotFound);
                }

                GetAnimalDto animalDto = new GetAnimalDto()
                {
                    Age = animal.Age,
                    BirthDate = animal.BirthDate.ToString(),
                    BreedName = animal.Breed.Name,
                    Name = animal.Name,
                    Description = animal.Description,
                    Gender = animal.Gender,
                    HealthStatus = animal.HealthStatus,
                    Price = animal.Price,
                    Weight = animal.Weight,
                    SocialMedia = animal.SocialMedia,
                    IsEducated = animal.IsEducated,
                    IsHavingValidDocuments = animal.IsHavingValidDocuments,
                    Address = animal.Owner.Address,
                    City = animal.Owner.City,
                    UserEmail = animal.Owner.Email,
                    UserName = animal.Owner.Name,
                    Photos = animal.Photos.Select(p => new PhotoDto()
                    {
                        Url = p.Url,
                    }).ToArray(),
                };

                return Result<GetAnimalDto>.Success(animalDto);
            }
        }
    }
}
