namespace Application.Animal
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using DTOs;
    using Domain;
    using Persistence.Repositories;

    public class ShowAnimalToEdit
    {
        public class ShowAnimalToEditQuery : IRequest<ShowAnimalDto>
        {
            public string AnimalId { get; set; } = null!;
        }

        public class ShowAnimalToEditQueryHandler :
            IRequestHandler<ShowAnimalToEditQuery, ShowAnimalDto>
        {
            private readonly IRepository repository;

            public ShowAnimalToEditQueryHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<ShowAnimalDto> Handle(ShowAnimalToEditQuery request, CancellationToken cancellationToken)
            {
                Animal animal =
                    await repository.GetById<Animal>(Guid.Parse(request.AnimalId));

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
                    Gender = animal.Gender.ToString(),
                    HealthStatus = animal.HealthStatus.ToString(),
                    IsEducated = animal.IsEducated,
                    IsHavingValidDocuments = animal.IsHavingValidDocuments,
                    Name = animal.Name,
                    Photo = animal.Photo,
                    SocialMedia = animal.SocialMedia,
                    Weight = animal.Weight,
                };

                return animalDto;
            }
        }
    }
}
