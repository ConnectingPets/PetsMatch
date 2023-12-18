namespace Application.Animal
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using Domain;
    using Response;
    using DTOs.Photo;
    using DTOs.Animal;
    using Persistence.Repositories;
    using static Common.ExceptionMessages.Animal;

    public class ShowAnimalToEdit
    {
        public class ShowAnimalToEditQuery : IRequest<Result<ShowAnimalToEditDto>>
        {
            public string AnimalId { get; set; } = null!;

            public string UserId { get; set; } = null!;
        }

        public class ShowAnimalToEditQueryHandler :
            IRequestHandler<ShowAnimalToEditQuery, Result<ShowAnimalToEditDto>>
        {
            private readonly IRepository repository;

            public ShowAnimalToEditQueryHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Result<ShowAnimalToEditDto>> Handle(ShowAnimalToEditQuery request, CancellationToken cancellationToken)
            {
                Animal? animal =
                    await repository.
                    All<Animal>(a => a.AnimalId.ToString()
                    == request.AnimalId).
                    Include(a => a.Photos).
                    Include(a => a.Breed).
                    FirstOrDefaultAsync();

                if (animal == null)
                {
                    return Result<ShowAnimalToEditDto>.Failure(AnimalNotFound);
                }
                if (animal.OwnerId.ToString() != request.UserId.ToLower())
                {
                    return Result<ShowAnimalToEditDto>.Failure(NotRightUser);
                }

                ShowAnimalToEditDto animalDto = new ShowAnimalToEditDto()
                {
                    Age = animal.Age,
                    BirthDate = animal.BirthDate.ToString(),
                    Description = animal.Description,
                    IsEducated = animal.IsEducated,
                    IsHavingValidDocuments = animal.IsHavingValidDocuments,
                    Name = animal.Name,
                    SocialMedia = animal.SocialMedia,
                    Weight = animal.Weight,
                    Gender = animal.Gender,
                    HealthStatus = animal.HealthStatus,
                    LastModifiedBreed = animal.LastModifiedBreed,
                    LastModifiedGender = animal.LastModifiedGender,
                    LastModifiedName = animal.LastModifiedName,
                    BreedId = animal.BreedId,
                    BreedName = animal.Breed.Name,
                    CategoryId = animal.Breed.CategoryId,
                    Photos = animal.Photos.Select(p => new PhotoDto()
                    {
                        Id = p.Id,
                        IsMain = p.IsMain,
                        Url = p.Url,
                    }).ToArray(),
                };

                return Result<ShowAnimalToEditDto>.Success(animalDto);
            }
        }
    }
}
