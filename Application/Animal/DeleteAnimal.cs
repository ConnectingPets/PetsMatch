namespace Application.Animal
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Domain;
    using Response;
    using Persistence.Repositories;

    public class DeleteAnimal
    {
        public class DeleteAnimalCommand : IRequest<Result<Unit>>
        {
            public string AnimalId { get; set; } = null!;

            public string UserId { get; set; } = null!;
        }

        public class DeleteAnimalCommandHandler :
            IRequestHandler<DeleteAnimalCommand, Result<Unit>>
        {
            private readonly IRepository repository;

            public DeleteAnimalCommandHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Result<Unit>> Handle(DeleteAnimalCommand request, CancellationToken cancellationToken)
            {
                string animalId = request.AnimalId;

                try
                {
                    await repository.
                        DeleteAsync<Animal>(Guid.Parse(animalId));
                }
                catch (Exception)
                {
                    return Result<Unit>.Failure("This pet does not exist! Please select existing one.");
                }

                Animal? animal =
                    await repository.GetById<Animal>(Guid.Parse(animalId));

                if (animal!.OwnerId.ToString() != request.UserId.ToLower())
                {
                    return Result<Unit>.Failure("This pet does not belong to you!");
                }

                var result =  await repository.SaveChangesAsync() > 0;

                if (!result)
                {
                    return 
                        Result<Unit>.Failure("Failed to delete pet - {animal.Name}");
                }

                return Result<Unit>.Success(Unit.Value, $"You have successfully delete {animal!.Name} to your pet's list");
            }
        }
    }
}
