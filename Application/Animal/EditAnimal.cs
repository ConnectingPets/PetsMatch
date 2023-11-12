namespace Application.Animal
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using DTOs;
    using Domain;
    using Persistence.Repositories;

    public class EditAnimal
    {
        public class EditAnimalCommand : IRequest<Unit>
        {
            public EditAnimalDto AnimalDto { get; set; } = null!;

            public string AnimalId { get; set; } = null!;
        }

        public class EditAnimalCommandHandler :
            IRequestHandler<EditAnimalCommand, Unit>
        {
            private readonly IRepository repository;

            public EditAnimalCommandHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Unit> Handle(EditAnimalCommand request, CancellationToken cancellationToken)
            {
                EditAnimalDto dto = request.AnimalDto;
                Animal animal =
                    await repository.GetById<Animal>(Guid.Parse(request.AnimalId));

                //animal.BirthDate = dto.BirthDate;
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

                await repository.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
