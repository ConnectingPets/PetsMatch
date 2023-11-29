namespace Application.AnimalCategory
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using DTOs;
    using Domain;
    using Response;
    using Persistence.Repositories;

    public class AllAnimalCategories
    {
        public class AllAnimalCategoriesQuery : IRequest<Result<IEnumerable<AnimalCategoryDto>>>
        {

        }

        public class AllAnimalCategoriesQueryHandler : IRequestHandler<AllAnimalCategoriesQuery, Result<IEnumerable<AnimalCategoryDto>>>
        {
            private readonly IRepository repository;

            public AllAnimalCategoriesQueryHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Result<IEnumerable<AnimalCategoryDto>>> Handle(AllAnimalCategoriesQuery request, CancellationToken cancellationToken)
            {
                AnimalCategoryDto[] allCategories = await repository.
                    AllReadonly<AnimalCategory>().
                    Select(ac => new AnimalCategoryDto()
                    {
                        Name = ac.Name,
                        AnimalCategoryId = ac.AnimalCategoryId
                    }).ToArrayAsync();

                return Result<IEnumerable<AnimalCategoryDto>>.Success(allCategories);
            }
        }
    }
}
