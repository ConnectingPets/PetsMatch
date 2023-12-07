namespace Application.Swipe
{
    using System.Threading;
    using System.Threading.Tasks;
    
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    
    using Domain;
    using Persistence.Repositories;
    using Application.DTOs.Swipe;
    using Application.Response;

    using static Common.ExceptionMessages.User;

    public class AnimalsToSwipe
    {
        public class AnimalsToSwipeQuery : IRequest<Result<IEnumerable<AnimalToSwipeDto>>>
        {
            public string UserId { get; set; } = null!;
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
                if (await this.repository.AnyAsync<User>(u => u.Id.ToString() == request.UserId) == false)
                {
                    return Result<IEnumerable<AnimalToSwipeDto>>.Failure(UserNotFound);
                }

                IEnumerable<AnimalToSwipeDto> animalsToSwipeDto = await this.repository
                    .All<Animal>(a => a.OwnerId.ToString() != request.UserId)
                    .Include(a => a.Breed)
                    .Include(a => a.Photos)
                    .Select(a => new AnimalToSwipeDto
                    {
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
                        Photo = a.Photos.First(p => p.IsMain).Url
                    })
                    .ToArrayAsync();

                return Result<IEnumerable<AnimalToSwipeDto>>.Success(animalsToSwipeDto);
            }
        }
    }
}