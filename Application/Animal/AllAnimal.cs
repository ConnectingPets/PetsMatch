namespace Application.Animal
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using Domain;
    using Response;
    using Persistence;
    using Application.DTOs.Animal;
    using Persistence.Repositories;

    using static Common.ExceptionMessages.Animal;

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
                User? user = await repository.
                    All<User>(u => u.Id.ToString() == userId).
                    Include(u => u.Animals).FirstOrDefaultAsync();

                if (!(user!.Animals.Any()))
                {
                    return Result<IEnumerable<AllAnimalDto>>.Failure(NoPets);
                }

                var userAnimals = await repository.
                    AllReadonly<Animal>(a => a.OwnerId.ToString() == userId).
                    Select(a => new AllAnimalDto()
                    {
                        Id = a.AnimalId.ToString(),
                        Name = a.Name,
                        MainPhoto = a.Photos.First(p => p.IsMain).Url
                    }).ToListAsync();

                return Result<IEnumerable<AllAnimalDto>>.Success(userAnimals);
            }
        }
    }
}
