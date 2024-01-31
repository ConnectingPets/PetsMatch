namespace Application.Marketplace
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using Domain;
    using Response;
    using DTOs.Photo;
    using Persistence.Repositories;
    using Application.DTOs.Marketplace;

    using static Common.ExceptionMessages.Animal;

    public class ShowAnimalMarketplaceToEdit
    {
        public class ShowAnimalMarketplaceToEditQuery :
            IRequest<Result<ShowAnimalMarketplaceToEditDto>>
        {
            public string AnimalId { get; set; } = null!;

            public string UserId { get; set; } = null!;
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
                    await repository.
                    All<Animal>(a => a.AnimalId.ToString() == request.AnimalId).
                    Include(a => a.Photos).
                    Include(a => a.Breed).
                    FirstOrDefaultAsync();

                if (animal == null)
                {
                    return Result<ShowAnimalMarketplaceToEditDto>.Failure(AnimalNotFound);
                }
                if (animal.OwnerId.ToString() != request.UserId.ToLower())
                {
                    return Result<ShowAnimalMarketplaceToEditDto>.Failure(NotRightUser);
                }

                ShowAnimalMarketplaceToEditDto animalDto =
                    new ShowAnimalMarketplaceToEditDto()
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
