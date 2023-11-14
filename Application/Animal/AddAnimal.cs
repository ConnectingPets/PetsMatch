﻿namespace Application.Animal
{
    using MediatR;

    using DTOs;
    using Domain;
    using Response;
    using Persistence.Repositories;

    public class AddAnimal
    {
        public class AddAnimalCommand : IRequest<Result<Unit>>
        {
            public EditOrAddAnimalDto AnimalDto { get; set; } = null!;

            public string OwnerId { get; set; } = null!;
        }

        public class AddAnimalCommandHandler :
            IRequestHandler<AddAnimalCommand, Result<Unit>>
        {
            private readonly IRepository repository;

            public AddAnimalCommandHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Result<Unit>> Handle(AddAnimalCommand request, CancellationToken cancellationToken)
            {
                EditOrAddAnimalDto animalDto = request.AnimalDto;

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
                    OwnerId = Guid.Parse(request.OwnerId),
                    BreedId = animalDto.BreedId,
                };

                await repository.AddAsync(animal);

                try
                {
                    await repository.SaveChangesAsync();
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
