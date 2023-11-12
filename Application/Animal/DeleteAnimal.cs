namespace Application.Animal
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Domain;
    using Persistence.Repositories;

    public class DeleteAnimal
    {
        public class DeleteAnimalCommand : IRequest<Unit>
        {
            public string AnimalId { get; set; } = null!;
        }

        public class DeleteAnimalCommandHandler : 
            IRequestHandler<DeleteAnimalCommand, Unit>
        {
            private readonly IRepository repository;

            public DeleteAnimalCommandHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Unit> Handle(DeleteAnimalCommand request, CancellationToken cancellationToken)
            {

                await repository.DeleteAsync<Animal>(Guid.Parse(request.AnimalId));
                await repository.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
