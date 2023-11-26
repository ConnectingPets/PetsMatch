namespace Application.Animal
{
    using Application.DTOs.Animal;
    using Application.DTOs.Breed;
    using Domain;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Persistence.Repositories;
    using Response;
    using System.Threading;
    using System.Threading.Tasks;

    public class ShowAnimalToAdd
    {
        public class ShowAnimalToAddQuery : IRequest<Result<ShowAnimalToAddDto>>
        {

        }

        public class ShowAnimalToAddQueryHandler :
            IRequestHandler<ShowAnimalToAddQuery, Result<ShowAnimalToAddDto>>
        {
            private readonly IRepository repository;

            public ShowAnimalToAddQueryHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Result<ShowAnimalToAddDto>> Handle(ShowAnimalToAddQuery request, CancellationToken cancellationToken)
            {
                var animal = new ShowAnimalToEditDto()
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

                return Result<ShowAnimalToAddDto>.Success(animal);
            }
        }
    }
}
