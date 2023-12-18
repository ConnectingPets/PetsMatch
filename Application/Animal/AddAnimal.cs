namespace Application.Animal
{
    using MediatR;

    using Domain;
    using Response;
    using DTOs.Photo;
    using DTOs.Animal;
    using Domain.Enum;
    using Service.Interfaces;
    using Persistence.Repositories;
    using static Common.SuccessMessages.Animal;
    using static Common.ExceptionMessages.Animal;

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

                    return Result<Unit>.Success(Unit.Value,string.Format(SuccessfullyAddedAnimal,animal.Name));
                }
                catch (Exception)
                {
                    return
                        Result<Unit>.Failure(string.Format(FailedToCreate,animal.Name));
                }
            }
        }
    }
}
