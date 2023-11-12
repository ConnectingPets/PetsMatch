namespace Application.Animal
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using DTOs;
    using Domain;
    using Persistence.Repositories;

    public class AllAnimal
    {
        public class AllAnimalQuery : IRequest<IEnumerable<AllAnimalDto>>
        {
            public string OwnerId { get; set; } = null!;
        }

        public class AllAnimalQueryHandler :
            IRequestHandler<AllAnimalQuery, IEnumerable<AllAnimalDto>>
        {

            private readonly IRepository repository;

            public AllAnimalQueryHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<IEnumerable<AllAnimalDto>> Handle(AllAnimalQuery request, CancellationToken cancellationToken)
            {
                return  await repository.
                    AllReadonly<Animal>(a => a.OwnerId.ToString() == request.OwnerId).Select(a => new AllAnimalDto()
                {
                    Id = a.OwnerId.ToString(),
                    Name = a.Name,
                    Age = a.Age,
                    Description = a.Description,
                    Weight = a.Weight,
                    SocialMedia = a.SocialMedia,
                    BirthDate = a.BirthDate.ToString(),
                    Gender = a.Gender.ToString(),
                    HealthStatus = a.HealthStatus.ToString(),
                    Photo = a.Photo,
                    IsEducated = a.IsEducated,
                    IsHavingValidDocuments = a.IsHavingValidDocuments,
                    Breed = a.Breed.Name,
                    Category = a.Breed.Category.Name
                }).ToListAsync();
            }
        }
    }
}
