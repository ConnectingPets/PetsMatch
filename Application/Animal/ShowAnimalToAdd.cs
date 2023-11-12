namespace Application.Animal
{
    using System.Threading.Tasks;
    using System.Threading;

    using MediatR;

    using DTOs;
    using Persistence.Repositories;
    using Domain;
    using Microsoft.EntityFrameworkCore;

    public class ShowAnimalToAdd
    {
        public class ShowAnimalToAddQuery : IRequest<ShowAnimalToAddDto>
        {

        }

        public class ShowAnimalToAddQueryHandler :
            IRequestHandler<ShowAnimalToAddQuery, ShowAnimalToAddDto>
        {
            private readonly IRepository repository;

            public ShowAnimalToAddQueryHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<ShowAnimalToAddDto> Handle(ShowAnimalToAddQuery request, CancellationToken cancellationToken)
            {
                var animal = new ShowAnimalToAddDto()
                {
                    Breeds = await repository.AllReadonly<Breed>().
                    Select(b => new BreedDto()
                    {
                        BreedId = b.BreedId,
                        Name = b.Name,
                        AnimalCategoryId = b.CategoryId
                    }).ToArrayAsync(),
                    AnimalCategories = await repository.AllReadonly<AnimalCategory>().
                    Select(ac => new AnimalCategoryDto()
                    {
                        AnimalCategoryId = ac.AnimalCategoryId,
                        Name = ac.Name
                    }).ToArrayAsync()
                };

                return animal;
            }
        }
    }
}
