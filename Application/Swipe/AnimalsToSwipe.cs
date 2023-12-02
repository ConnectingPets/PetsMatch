namespace Application.Swipe
{
    using System.Threading;
    using System.Threading.Tasks;
    
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    
    using Domain;
    using Persistence.Repositories;
    using Application.DTOs.Swipe;
    using Application.Exceptions.Entity;
    using Application.Exceptions.User;

    public class AnimalsToSwipe
    {
        public class AnimalsToSwipeQuery : IRequest<IEnumerable<AnimalToSwipeDto>>
        {
            public string UserId { get; set; } = null!;
        }

        public class AnimalsToSwipeHandler : IRequestHandler<AnimalsToSwipeQuery, IEnumerable<AnimalToSwipeDto>>
        {
            private readonly IRepository repository;

            public AnimalsToSwipeHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<IEnumerable<AnimalToSwipeDto>> Handle(AnimalsToSwipeQuery request, CancellationToken cancellationToken)
            {
                if (!Guid.TryParse(request.UserId, out Guid guidUserId))
                {
                    throw new InvalidGuidFormatException();
                }

                if (await this.repository.AnyAsync<User>(u => u.Id == guidUserId) == false)
                {
                    throw new UserNotFoundException();
                }

                IEnumerable<AnimalToSwipeDto> animalsToSwipeDto = await this.repository
                    .All<Animal>(a => a.OwnerId != guidUserId)
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

                return animalsToSwipeDto;
            }
        }
    }
}