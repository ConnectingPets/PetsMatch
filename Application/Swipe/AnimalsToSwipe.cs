namespace Application.Swipe
{
    using Application.DTOs.Swipe;
    using Application.Exceptions;
    using MediatR;
    using Persistence.Repositories;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain;
    using Microsoft.EntityFrameworkCore;

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
                    .Select(a => new AnimalToSwipeDto
                    {

                    })
                    .ToArrayAsync();

                return animalsToSwipeDto;
            }
        }
    }
}
