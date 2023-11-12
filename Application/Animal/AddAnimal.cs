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
                Animal animal = new Animal()
                {
                    Age = request.AnimalDto.Age,
                    BirthDate = request.AnimalDto.BirthDate,
                    Description = request.AnimalDto.Description,
                    Gender = request.AnimalDto.Gender,
                    HealthStatus = request.AnimalDto.HealthStatus,
                    Name = request.AnimalDto.Name,
                    Weight = request.AnimalDto.Weight,
                    SocialMedia = request.AnimalDto.SocialMedia,
                    IsEducated = request.AnimalDto.IsEducated,
                    Photo = request.AnimalDto.Photo,
                    IsHavingValidDocuments = request.AnimalDto.IsHavingValidDocuments,
                    OwnerId = Guid.Parse(request.AnimalDto.OwnerId!),
                    BreedId = request.AnimalDto.BreedId,
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
