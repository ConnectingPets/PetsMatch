namespace Application.Animal
{
    using System.Threading.Tasks;
    using System.Threading;

    using Microsoft.EntityFrameworkCore;
    using MediatR;

    using DTOs;
    using Persistence.Repositories;
    using Domain;

    public class AllAnimal
    {
        public class AllAnimalQuery : IRequest<IEnumerable<AnimalDto>>
        {
            public string OwnerId { get; set; } = null!;
        }

        public class AllAnimalQueryHandler :
            IRequestHandler<AllAnimalQuery, IEnumerable<AnimalDto>>
        {

            private readonly IRepository repository;

            public AllAnimalQueryHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<IEnumerable<AnimalDto>> Handle(AllAnimalQuery request, CancellationToken cancellationToken)
            {
                return  await repository.
                    AllReadonly<Animal>(a => a.OwnerId.ToString() == request.OwnerId).Select(a => new AnimalDto()
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
