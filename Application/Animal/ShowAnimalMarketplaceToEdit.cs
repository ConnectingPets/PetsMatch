namespace Application.Animal
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using Domain;
    using Response;
    using DTOs.Photo;
    using DTOs.Animal;
    using Persistence.Repositories;

    public class ShowAnimalMarketplaceToEdit
    {
        public class ShowAnimalMarketplaceToEditQuery :
            IRequest<Result<ShowAnimalMarketplaceToEditDto>>
        {
            public string AnimalId { get; set; } = null!;

            public string OwnerId { get; set; } = null!;
        }

        public class ShowAnimalMarketplaceToEditQueryHandler :
           IRequestHandler<ShowAnimalMarketplaceToEditQuery, Result<ShowAnimalMarketplaceToEditDto>>
        {
            private readonly IRepository repository;

            public ShowAnimalMarketplaceToEditQueryHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Result<ShowAnimalMarketplaceToEditDto>> Handle(ShowAnimalMarketplaceToEditQuery request,
                CancellationToken cancellationToken)
            {
                Animal? animal =
                    await repository.All<Animal>().
                    Include(a => a.Photos).
                    Include(a => a.Breed).
                    FirstOrDefaultAsync(a => a.AnimalId.ToString() == request.AnimalId);

                if (animal == null)
                {
                    return Result<ShowAnimalMarketplaceToEditDto>.Failure("This pet does not exist! Please select existing one");
                }
                if (animal.OwnerId.ToString() != request.OwnerId.ToLower())
                {
                    return Result<ShowAnimalMarketplaceToEditDto>.Failure("This pet does not belong to you!");
                }

                ShowAnimalMarketplaceToEditDto animalDto =
                    new ShowAnimalMarketplaceToEditDto()
                    {
                        Age = animal.Age,
                        BirthDate = animal.BirthDate.ToString(),
                        Description = animal.Description,
                        IsEducated = animal.IsEducated,
                        IsHavingValidDocuments =                                  animal.IsHavingValidDocuments,
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
                        AnimalStatus = animal.AnimalStatus,
                        Price = animal.Price,
                        Photos = animal.Photos.Select(p => new PhotoDto()
                        {
                            Id = p.Id,
                            IsMain = p.IsMain,
                            Url = p.Url,
                        }).ToArray(),
                    };

                return Result<ShowAnimalMarketplaceToEditDto>.Success(animalDto);
            }
        }
    }
}
