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
                    await repository.GetById<Animal>(Guid.Parse(request.AnimalId));

                if (animal == null)
                {
                    return Result<ShowAnimalToEditDto>.Failure("This pet does not exist! Please select existing one");
                }
                if (animal.OwnerId.ToString() != request.UserId.ToLower())
                {
                    return Result<ShowAnimalToEditDto>.Failure("This pet does not belong to you!");
                }

                ShowAnimalToEditDto animalDto = new ShowAnimalToEditDto()
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
                    IsEducated = animal.IsEducated,
                    IsHavingValidDocuments = animal.IsHavingValidDocuments,
                    Name = animal.Name,
                    SocialMedia = animal.SocialMedia,
                    Weight = animal.Weight,
                };

                if (!((DateTime.UtcNow - animal.LastModified).Days < 30))
                {
                    animalDto.CanEditAll = true;
                }

                return Result<ShowAnimalToEditDto>.Success(animalDto);
            }
        }
    }
}
