namespace Application.Animal
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using DTOs;
    using Domain;
    using Response;
    using Persistence.Repositories;
    using Domain.Enum;

    public class ShowAnimalToEdit
    {
        public class ShowAnimalToEditQuery : IRequest<Result<ShowAnimalDto>>
        {
            public string AnimalId { get; set; } = null!;

            public string UserId { get; set; } = null!;
        }

        public class ShowAnimalToEditQueryHandler :
            IRequestHandler<ShowAnimalToEditQuery, Result<ShowAnimalDto>>
        {
            private readonly IRepository repository;

            public ShowAnimalToEditQueryHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Result<ShowAnimalDto>> Handle(ShowAnimalToEditQuery request, CancellationToken cancellationToken)
            {
                Animal? animal =
                    await repository.GetById<Animal>(Guid.Parse(request.AnimalId));

                if (animal == null)
                {
                    return Result<ShowAnimalDto>.Failure("This pet does not exist! Please select existing one");
                }
                if (animal.OwnerId.ToString() != request.UserId.ToLower())
                {
                    return Result<ShowAnimalDto>.Failure("This pet does not belong to you!");
                }

                ShowAnimalDto animalDto = new ShowAnimalDto()
                {
                    Breeds = await repository.AllReadonly<Breed>().
                    Select(b => new BreedDto()
                    {
                        BreedId = b.BreedId,
                        Name = b.Name,
                        AnimalCategoryId = b.CategoryId
                    }).ToArrayAsync(),
                    AnimalCategories = await repository.AllReadonly<AnimalCategory>().
                    Select(ac => new AnimalCategoryDto()
                    {
                        AnimalCategoryId = ac.AnimalCategoryId,
                        Name = ac.Name
                    }).ToArrayAsync(),
                    Age = animal.Age,
                    BirthDate = animal.BirthDate.ToString(),
                    Description = animal.Description,
                    Gender = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList(),
                    HealthStatus = 
                    Enum.GetValues(typeof(HealthStatus)).Cast<HealthStatus>().ToList(),
                    IsEducated = animal.IsEducated,
                    IsHavingValidDocuments = animal.IsHavingValidDocuments,
                    Name = animal.Name,
                    Photo = animal.Photo,
                    SocialMedia = animal.SocialMedia,
                    Weight = animal.Weight,
                };

                return Result<ShowAnimalDto>.Success(animalDto);
            }
        }
    }
}
