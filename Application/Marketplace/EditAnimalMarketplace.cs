﻿namespace Application.Marketplace
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Domain;
    using Response;
    using Persistence.Repositories;
    using Application.DTOs.Marketplace;

    using static Common.ExceptionMessages.Entity;
    using static Common.ExceptionMessages.Animal;
    using static Common.SuccessMessages.Animal;

    public class EditAnimalMarketplace
    {
        public class EditAnimalMarketplaceCommand : IRequest<Result<Unit>>
        {
            public EditAnimalMarketplaceDto AnimalDto { get; set; } = null!;

            public string UserId { get; set; } = null!;
            public string AnimalId { get; set; } = null!;
        }

        public class EditAnimalMarketplaceCommandHandler : IRequestHandler<EditAnimalMarketplaceCommand, Result<Unit>>
        {
            private readonly IRepository repository;

            public EditAnimalMarketplaceCommandHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Result<Unit>> Handle(EditAnimalMarketplaceCommand request, CancellationToken cancellationToken)
            {
                string animalId = request.AnimalId;
                EditAnimalMarketplaceDto dto = request.AnimalDto;

                if (!Guid.TryParse(animalId, out Guid result))
                {
                    return Result<Unit>.Failure(InvalidGuidFormat);
                }
                Animal? animal =
                    await repository.GetById<Animal>(Guid.Parse(animalId));

                if (animal == null)
                {
                    return Result<Unit>.Failure(AnimalNotFound);
                }
                if (animal.OwnerId.ToString() != request.UserId.ToLower())
                {
                    return Result<Unit>.Failure(NotRightUser);
                }

                int daysDifferenceName = (DateTime.UtcNow - animal.LastModifiedName).Days;
                int daysDifferenceBreed = (DateTime.UtcNow - animal.LastModifiedBreed).Days;
                int daysDifferenceGender = (DateTime.UtcNow - animal.LastModifiedGender).Days;

                bool isNameEdit = animal.Name != dto.Name;
                bool isBreedEdit = animal.BreedId != dto.BreedId;
                bool isGenderEdit = animal.Gender != dto.Gender;

                if (daysDifferenceName < 30 && isNameEdit)
                {
                    return Result<Unit>.Failure(string.Format(CannotUpdateName, 30 - daysDifferenceName));
                }
                if (daysDifferenceBreed < 30 && isBreedEdit)
                {
                    return Result<Unit>.Failure(string.Format(CannotUpdateBreed, 30 - daysDifferenceBreed));
                }
                if (daysDifferenceGender < 30 && isGenderEdit)
                {
                    return Result<Unit>.Failure(string.Format(CannotUpdateGender, 30 - daysDifferenceGender));
                }

                if (isNameEdit)
                {
                    animal.LastModifiedName = DateTime.UtcNow;
                }
                if (isBreedEdit)
                {
                    animal.LastModifiedBreed = DateTime.UtcNow;
                }
                if (isGenderEdit)
                {
                    animal.LastModifiedGender = DateTime.UtcNow;
                }

                animal.BirthDate = dto.BirthDate;
                animal.Gender = dto.Gender;
                animal.Description = dto.Description;
                animal.HealthStatus = dto.HealthStatus;
                animal.SocialMedia = dto.SocialMedia;
                animal.Age = dto.Age;
                animal.BreedId = dto.BreedId;
                animal.IsEducated = dto.IsEducated;
                animal.IsHavingValidDocuments = dto.IsHavingValidDocuments;
                animal.Name = dto.Name;
                animal.Weight = dto.Weight;
                animal.Price = dto.Price;
                animal.AnimalStatus = dto.AnimalStatus;

                try
                {
                    await repository.SaveChangesAsync();
                    return Result<Unit>.Success(Unit.Value, string.Format(SuccessfullyEditAnimal, animal.Name));
                }
                catch (Exception)
                {
                    return Result<Unit>.Failure(string.Format(FailedToEdit, animal.Name));
                }
            }

        }
    }
}

