﻿namespace Application.Animal
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using DTOs;
    using Domain;
    using Persistence.Repositories;
    using Application.Response;

    public class EditAnimal
    {
        public class EditAnimalCommand : IRequest<Result<Unit>>
        {
            public EditOrAddAnimalDto AnimalDto { get; set; } = null!;

            public string AnimalId { get; set; } = null!;

            public string UserId { get; set; } = null!;
        }

        public class EditAnimalCommandHandler :
            IRequestHandler<EditAnimalCommand, Result<Unit>>
        {
            private readonly IRepository repository;

            public EditAnimalCommandHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Result<Unit>> Handle(EditAnimalCommand request, CancellationToken cancellationToken)
            {
                EditOrAddAnimalDto dto = request.AnimalDto;
                Animal? animal =
                    await repository.GetById<Animal>(Guid.Parse(request.AnimalId));

                if (animal == null)
                {
                    return Result<Unit>.Failure("This pet does not exist! Please select existing one");
                }
                if (animal.OwnerId.ToString() != request.UserId.ToLower())
                {
                    return Result<Unit>.Failure("This pet does not belong to you!");
                }

                animal.BirthDate = dto.BirthDate;
                animal.Gender = dto.Gender;
                animal.Description = dto.Description;
                animal.HealthStatus = dto.HealthStatus;
                animal.SocialMedia = dto.SocialMedia;
                animal.Photo = dto.Photo;
                animal.Age = dto.Age;
                animal.BreedId = dto.BreedId;
                animal.IsEducated = dto.IsEducated;
                animal.IsHavingValidDocuments = dto.IsHavingValidDocuments;
                animal.Name = dto.Name;
                animal.Weight = dto.Weight;

                var result = await repository.SaveChangesAsync() > 0;
                if (!result)
                {
                   return 
                        Result<Unit>.Failure($"Failed to update pet - {animal.Name}");
                }

                return Result<Unit>.Success(Unit.Value, $"Successfully updated {animal.Name}");
            }
        }
    }
}
