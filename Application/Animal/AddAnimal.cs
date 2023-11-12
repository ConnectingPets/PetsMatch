namespace Application.Animal
{
    using MediatR;

    using Domain;
    using DTOs;
    using Persistence.Repositories;

    public class AddAnimal
    {
        public class AddAnimalCommand : IRequest<Unit>
        {
            public AddAnimalDto AnimalDto { get; set; } = null!;
        }

        public class AddAnimalCommandHandler : IRequestHandler<AddAnimalCommand, Unit>
        {
            private readonly IRepository repository;

            public AddAnimalCommandHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Unit> Handle(AddAnimalCommand request, CancellationToken cancellationToken)
            {
                AddAnimalDto animalDto = request.AnimalDto;

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
                    Photo = animalDto.Photo,
                    IsHavingValidDocuments = animalDto.IsHavingValidDocuments,
                    OwnerId = Guid.Parse(animalDto.OwnerId!),
                    BreedId = animalDto.BreedId,
                };

                await repository.AddAsync(animal);
                var result = await repository.SaveChangesAsync() > 0;

                if (!result)
                {
                    //return Result<Unit>.Failure("Failed to create activity");
                }

                return Unit.Value;
            }
        }
    }
}
