namespace Application.Swipe
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Domain;

    using Persistence.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Application.Exceptions.Entity;
    using Application.Exceptions.Animal;

    public class SwipeAnimal
    {
        public class SwipeAnimalCommand : IRequest<bool>
        {
            public required string SwiperAnimalId { get; set; }

            public required string SwipeeAnimalId { get; set; }

            public required bool SwipedRight { get; set; }
        }

        public class SwipeAnimalHandler : IRequestHandler<SwipeAnimalCommand, bool>
        {
            private readonly IRepository repository;

            public SwipeAnimalHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<bool> Handle(SwipeAnimalCommand request, CancellationToken cancellationToken)
            {
                if (!Guid.TryParse(request.SwiperAnimalId, out Guid swiperAnimalId))
                {
                    throw new InvalidGuidFormatException();
                }

                if (!Guid.TryParse(request.SwipeeAnimalId, out Guid swipeeAnimalId))
                {
                    throw new InvalidGuidFormatException();
                }

                if (await this.repository.AnyAsync<Animal>(animal => animal.AnimalId == swiperAnimalId) == false)
                {
                    throw new AnimalNotFoundException();
                }

                if (await this.repository.AnyAsync<Animal>(animal => animal.AnimalId == swipeeAnimalId) == false)
                {
                    throw new AnimalNotFoundException();
                }

                if (request.SwiperAnimalId.ToString() == request.SwipeeAnimalId.ToString())
                {
                    throw new SameAnimalException();
                }

                Swipe swipe = new Swipe
                {
                    SwiperAnimalId = swiperAnimalId,
                    SwipeeAnimalId = swipeeAnimalId,
                    SwipedRight = request.SwipedRight,
                    SwipedOn = DateTime.Now
                };

                await this.repository.AddAsync(swipe);
                await this.repository.SaveChangesAsync();

                return await IsMatch(swiperAnimalId, swipeeAnimalId, request.SwipedRight);
            }

            private async Task<bool> IsMatch(Guid swiperAnimalId, Guid swipeeAnimalId, bool swipedRight)
            {
                Animal? animal = await this.repository.All<Animal>(a => a.AnimalId == swiperAnimalId)
                    .Include(a => a.SwipesFrom)
                    .FirstOrDefaultAsync();

                return animal!.SwipesFrom.Any(s => s.SwiperAnimalId == swipeeAnimalId && s.SwipedRight) && swipedRight;
            }
        }
    }
}
