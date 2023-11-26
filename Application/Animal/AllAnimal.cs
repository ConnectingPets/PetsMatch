namespace Application.Animal
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using DTOs;
    using Domain;
    using Response;
    using Persistence.Repositories;
    using Persistence;
    using Application.DTOs.Animal;

    public class AllAnimal
    {
        public class AllAnimalQuery : IRequest<Result<IEnumerable<AllAnimalDto>>>
        {
            public string OwnerId { get; set; } = null!;
        }

        public class AllAnimalQueryHandler :
            IRequestHandler<AllAnimalQuery, Result<IEnumerable<AllAnimalDto>>>
        {
            private readonly IRepository repository;
            private readonly DataContext dataContext;

            public AllAnimalQueryHandler(IRepository repository, DataContext dataContext)
            {
                this.repository = repository;
                this.dataContext = dataContext;
            }

            public async Task<Result<IEnumerable<AllAnimalDto>>> Handle(AllAnimalQuery request, CancellationToken cancellationToken)
            {
                string userId = request.OwnerId;
                User? user = await dataContext.Users.
                    Include(u => u.Animals).
                    FirstAsync(u => u.Id.ToString() == userId);

                if (!(user!.Animals.Any()))
                {
                    return Result<IEnumerable<AllAnimalDto>>.Failure("You don't hav pets yet");
                }

                var userAnimals = await repository.
                    AllReadonly<Animal>(a => a.OwnerId.ToString() == userId).
                    Select(a => new AllAnimalDto()
                    {
                        Id = a.AnimalId.ToString(),
                        Name = a.Name,
                        Photo = a.Photo,
                    }).ToListAsync();

                return Result<IEnumerable<AllAnimalDto>>.Success(userAnimals);
            }
        }
    }
}
