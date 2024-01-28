namespace Application.Swipe
{
    using System.Threading;
    using System.Threading.Tasks;
    
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    
    using DTOs.Photo;
    using DTOs.Swipe;
    using Domain;
    using Response;
    using Persistence.Repositories;

    using static Common.ExceptionMessages.User;
    using static Common.ExceptionMessages.Animal;

    public class AnimalsToSwipe
    {
        public class AnimalsToSwipeQuery : IRequest<Result<IEnumerable<AnimalToSwipeDto>>>
        {
            public string UserId { get; set; } = null!;

            public string AnimalId { get; set; } = null!;
        }

        public class AnimalsToSwipeHandler : IRequestHandler<AnimalsToSwipeQuery, Result<IEnumerable<AnimalToSwipeDto>>>
        {
            private readonly IRepository repository;

            public AnimalsToSwipeHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Result<IEnumerable<AnimalToSwipeDto>>> Handle(AnimalsToSwipeQuery request, CancellationToken cancellationToken)
            {
                Animal? animal = await this.repository.FirstOrDefaultAsync<Animal>(a => a.AnimalId.ToString() == request.AnimalId.ToLower());

                if (animal == null)
                {
                    return Result<IEnumerable<AnimalToSwipeDto>>.Failure(AnimalNotFound);
                }

                if (await this.repository.AnyAsync<User>(u => u.Id.ToString() == request.UserId.ToLower()) == false)
                {
                    return Result<IEnumerable<AnimalToSwipeDto>>.Failure(UserNotFound);
                }

                if (animal.OwnerId.ToString() != request.UserId.ToLower())
                {
                    return Result<IEnumerable<AnimalToSwipeDto>>.Failure(NotOwner);
                }

                IEnumerable<Animal> animals = await this.repository
                    .All<Animal>(a => a.OwnerId.ToString() != request.UserId.ToLower())
                    .Include(a => a.SwipesFrom)
                    .Include(a => a.Breed)
                    .Include(a => a.Photos)
                    .ToArrayAsync();

                IEnumerable<AnimalToSwipeDto> animalsToSwipeDto = animals 
                    .Where(a => !a.SwipesFrom.Any(s => s.SwiperAnimalId.ToString() == animal.AnimalId.ToString()) 
                    && a.Breed.CategoryId == animal.Breed.CategoryId
                    && a.Gender != animal.Gender)
                    .Select(a => new AnimalToSwipeDto
                    {
                        AnimalId = a.AnimalId.ToString(),
                        Name = a.Name,
                        Description = a.Description,
                        Age = a.Age,
                        BirthDate = a.BirthDate,
                        IsEducated = a.IsEducated,
                        HealthStatus = a.HealthStatus.ToString(),
                        Gender = a.Gender.ToString(),
                        SocialMedia = a.SocialMedia,
                        Weight = a.Weight,
                        IsHavingValidDocuments = a.IsHavingValidDocuments,
                        Breed = a.Breed.Name,
                        Photos = a.Photos.Select(p => new PhotoDto
                        {
                            Id = p.Id,
                            IsMain = p.IsMain,
                            Url = p.Url
                        }).ToArray()
                    })
                    .ToArray();

                return Result<IEnumerable<AnimalToSwipeDto>>.Success(animalsToSwipeDto);
            }
        }
    }
}