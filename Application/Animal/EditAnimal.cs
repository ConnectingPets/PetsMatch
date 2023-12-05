namespace Application.Animal
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Domain;
    using Persistence.Repositories;
    using Application.DTOs.Animal;
    using Response;

    public class EditAnimal
    {
        public class EditAnimalCommand : IRequest<Result<Unit>>
        {
            public AddOrEditAnimalDto AnimalDto { get; set; } = null!;

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
                AddOrEditAnimalDto dto = request.AnimalDto;
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

                bool isSomethingEdit = animal.Name != dto.Name 
                    || animal.BreedId != dto.BreedId 
                    || animal.Gender != dto.Gender;


                //int daysDifference = (DateTime.UtcNow - animal.LastModified).Days;

                //if (isSomethingEdit)
                //{
                //    animal.LastModified = DateTime.UtcNow;
                //}

                //if (daysDifference < 30 && isSomethingEdit)
                //{
                //    return Result<Unit>.Failure($"Can not update pet name, breed and gender for another {30 - daysDifference} days.");
                //}

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
