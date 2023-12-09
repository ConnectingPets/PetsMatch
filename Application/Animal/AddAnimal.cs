namespace Application.Animal
{
    using MediatR;

    using Domain;
    using Response;
    using DTOs.Photo;
    using Domain.Enum;
    using DTOs.Animal;
    using Service.Interfaces;
    using Persistence.Repositories;

    public class AddAnimal
    {
        public class AddAnimalCommand : IRequest<Result<Unit>>
        {
            public AddAnimalDto AnimalDto { get; set; } = null!;

            public string OwnerId { get; set; } = null!;
        }

        public class AddAnimalCommandHandler :
            IRequestHandler<AddAnimalCommand, Result<Unit>>
        {
            private readonly IRepository repository;

            private readonly IPhotoService photoService;

            public AddAnimalCommandHandler(IRepository repository,
                                           IPhotoService photoService)
            {
                this.repository = repository;
                this.photoService = photoService;

            }

            public async Task<Result<Unit>> Handle(AddAnimalCommand request, CancellationToken cancellationToken)
            {
                AddAnimalDto animalDto = request.AnimalDto;
                MainPhotoDto[] photos = request.AnimalDto.Photos;

                Animal animal = new Animal()
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
                    AnimalStatus = AnimalStatus.ForSwiping
                };

                await repository.AddAsync(animal);

                try
                {
                    await repository.SaveChangesAsync();
                    await photoService.AddAnimalPhotosWithMainAsync(photos,animal);

                    return Result<Unit>.Success(Unit.Value,$"You have successfully add {animal.Name} to your pet's list");
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
