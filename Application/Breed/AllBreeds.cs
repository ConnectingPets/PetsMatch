namespace Application.Breed
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using Domain;
    using Response;
    using Application.DTOs.Breed;
    using Persistence.Repositories;
    using static Common.ExceptionMessages.AnimalCategory;

    public class AllBreeds
    {
        public class AllBreedsQuery : IRequest<Result<IEnumerable<BreedDto>>>
        {
            public int CategoryId { get; set; }
        }

        public class AllBreedsQueryHandler :
            IRequestHandler<AllBreedsQuery, Result<IEnumerable<BreedDto>>>
        {
            private readonly IRepository repository;

            public AllBreedsQueryHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Result<IEnumerable<BreedDto>>> Handle(AllBreedsQuery request, CancellationToken cancellationToken)
            {
                int categoryId = request.CategoryId;

                AnimalCategory? category = await repository.
                    All<AnimalCategory>(c => c.AnimalCategoryId == categoryId).Include(c => c.Breeds).
                    FirstOrDefaultAsync();

                if (category == null)
                {
                    return Result<IEnumerable<BreedDto>>.
                        Failure(CategoryNotExist);
                }

                BreedDto[] breeds = category.Breeds.
                   Select(b => new BreedDto()
                   {
                       BreedId = b.BreedId,
                       Name = b.Name,
                   }).ToArray();

                return Result<IEnumerable<BreedDto>>.Success(breeds);
            }
        }
    }
}
