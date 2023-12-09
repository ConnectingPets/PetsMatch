namespace Application.Animal
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Domain;
    using Response;
    using DTOs.Photo;
    using DTOs.Animal;
    using Domain.Enum;
    using Service.Interfaces;
    using Persistence.Repositories;

    public class AddAnimalMarketplace
    {
        public class AddAnimalMarketplaceCommand : IRequest<Result<Unit>>
        {
            public AddAnimalMarketplaceDto AnimalDto { get; set; } = null!;
            public string OwnerId { get; set; } = null!;
        }

        public class AddMarketplaceAnimalCommandHandler :
            IRequestHandler<AddAnimalMarketplaceCommand, Result<Unit>>
        {
            private readonly IRepository repository;
            private readonly IPhotoService photoService;

            public AddMarketplaceAnimalCommandHandler(IRepository repository,
                                                    IPhotoService photoService)
            {
                this.repository = repository;
                this.photoService = photoService;
            }

            public async Task<Result<Unit>> Handle(AddAnimalMarketplaceCommand request, CancellationToken cancellationToken)
            {
                AddAnimalMarketplaceDto animalDto = request.AnimalDto;
                MainPhotoDto[] photos = request.AnimalDto.Photos;
                Animal animal;

                if (animalDto.IsForSale)
                {
                    animal = new Animal()
                    {
                        Age = animalDto.Age,
                        BirthDate = animalDto.BirthDate,
                        Description = animalDto.Description,
                        Gender = animalDto.Gender,
                        HealthStatus = animalDto.HealthStatus,
                        Name = animalDto.Name,
                        Weight = animalDto.Weight,
                        SocialMedia = animalDto.SocialMedia,
                        IsEducated = animalDto.IsEducated,
                        IsHavingValidDocuments = animalDto.IsHavingValidDocuments,
                        OwnerId = Guid.Parse(request.OwnerId),
                        BreedId = animalDto.BreedId,
                        AnimalStatus = AnimalStatus.ForSale,
                        Price = animalDto.Price
                    };
                }
                else
                {
                    animal = new Animal()
                    {
                        Age = animalDto.Age,
                        BirthDate = animalDto.BirthDate,
                        Description = animalDto.Description,
                        Gender = animalDto.Gender,
                        HealthStatus = animalDto.HealthStatus,
                        Name = animalDto.Name,
                        Weight = animalDto.Weight,
                        SocialMedia = animalDto.SocialMedia,
                        IsEducated = animalDto.IsEducated,
                        IsHavingValidDocuments = animalDto.IsHavingValidDocuments,
                        OwnerId = Guid.Parse(request.OwnerId),
                        BreedId = animalDto.BreedId,
                        AnimalStatus = AnimalStatus.ForAdoption
                    };
                }

                await repository.AddAsync(animal);

                try
                {
                    await repository.SaveChangesAsync();
                    await photoService.AddAnimalPhotosWithMainAsync(photos, animal);

                    return Result<Unit>.Success(Unit.Value, $"You have successfully add {animal.Name} to your pet's list");
                }
                catch (Exception)
                {
                    return
                        Result<Unit>.Failure($"Failed to create pet - {animal.Name}");
                }
            }
        }
    }
}
