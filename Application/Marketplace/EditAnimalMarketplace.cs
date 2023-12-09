namespace Application.Marketplace
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Domain;
    using Response;
    using Persistence.Repositories;
    using Application.DTOs.Marketplace;
    using static Common.ExceptionMessages.Entity;

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
                    return Result<Unit>.Failure("This pet does not exist! Please select existing one");
                }
                if (animal.OwnerId.ToString() != request.UserId.ToLower())
                {
                    return Result<Unit>.Failure("This pet does not belong to you!");
                }

                int daysDifferenceName = (DateTime.UtcNow - animal.LastModifiedName).Days;
                int daysDifferenceBreed = (DateTime.UtcNow - animal.LastModifiedBreed).Days;
                int daysDifferenceGender = (DateTime.UtcNow - animal.LastModifiedGender).Days;

                bool isNameEdit = animal.Name != dto.Name;
                bool isBreedEdit = animal.BreedId != dto.BreedId;
                bool isGenderEdit = animal.Gender != dto.Gender;

                if (daysDifferenceName < 30 && isNameEdit)
                {
                    return Result<Unit>.Failure($"Can not update pet name {30 - daysDifferenceName} days.");
                }
                if (daysDifferenceBreed < 30 && isBreedEdit)
                {
                    return Result<Unit>.Failure($"Can not update pet breed for another {30 - daysDifferenceBreed} days.");
                }
                if (daysDifferenceGender < 30 && isGenderEdit)
                {
                    return Result<Unit>.Failure($"Can not update pet gender for another {30 - daysDifferenceName} days.");
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
                    return Result<Unit>.Success(Unit.Value, $"Successfully updated {animal.Name}");
                }
                catch (Exception)
                {
                    return Result<Unit>.Failure($"Failed to update pet - {animal.Name}");
                }
            }

        }
    }
}

