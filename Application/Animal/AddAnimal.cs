﻿namespace Application.Animal
{
    using MediatR;

    using Domain;
    using Response;
    using DTOs.Animal;
    using Persistence.Repositories;

    public class AddAnimal
    {
        public class AddAnimalCommand : IRequest<Result<string>>
        {
            public AddOrEditAnimalDto AnimalDto { get; set; } = null!;

            public string OwnerId { get; set; } = null!;
        }

        public class AddAnimalCommandHandler :
            IRequestHandler<AddAnimalCommand, Result<string>>
        {
            private readonly IRepository repository;

            public AddAnimalCommandHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Result<string>> Handle(AddAnimalCommand request, CancellationToken cancellationToken)
            {
                AddOrEditAnimalDto animalDto = request.AnimalDto;

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
                    BreedId = animalDto.BreedId
                };

                await repository.AddAsync(animal);

                try
                {
                    await repository.SaveChangesAsync();
                    return Result<string>.Success(animal.AnimalId.ToString(), $"You have successfully add {animal.Name} to your pet's list");
                }
                catch (Exception)
                {
                    return
                        Result<string>.Failure($"Failed to create pet - {animal.Name}");
                }

            }
        }
    }
}
