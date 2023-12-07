namespace Application.Swipe
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Domain;

    using Persistence.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Application.Response;

    using static Common.ExceptionMessages.Animal;
    using static Common.ExceptionMessages.Swipe;
    using static Common.FailMessages.Swipe;

    public class SwipeAnimal
    {
        public class SwipeAnimalCommand : IRequest<Result<bool>>
        {
            public required string SwiperAnimalId { get; set; }

            public required string SwipeeAnimalId { get; set; }

            public required bool SwipedRight { get; set; }
        }

        public class SwipeAnimalHandler : IRequestHandler<SwipeAnimalCommand, Result<bool>>
        {
            private readonly IRepository repository;

            public SwipeAnimalHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Result<bool>> Handle(SwipeAnimalCommand request, CancellationToken cancellationToken)
            {
                if (await this.repository.AnyAsync<Animal>(animal => animal.AnimalId.ToString() == request.SwiperAnimalId) == false)
                {
                    return Result<bool>.Failure(AnimalNotFound);
                }

                if (await this.repository.AnyAsync<Animal>(animal => animal.AnimalId.ToString() == request.SwipeeAnimalId) == false)
                {
                    return Result<bool>.Failure(AnimalNotFound);
                }

                if (request.SwiperAnimalId.ToString() == request.SwipeeAnimalId.ToString())
                {
                    return Result<bool>.Failure(SameAnimal);
                }

                Guid guidSwiper = Guid.Parse(request.SwiperAnimalId);
                Guid guidSwipee = Guid.Parse(request.SwipeeAnimalId);

                Swipe swipe = new Swipe
                {
                    SwiperAnimalId = guidSwiper,
                    SwipeeAnimalId = guidSwipee,
                    SwipedRight = request.SwipedRight,
                    SwipedOn = DateTime.Now
                };

                await this.repository.AddAsync(swipe);

                try
                {
                    await this.repository.SaveChangesAsync();
                }
                catch (Exception)
                {
                    return Result<bool>.Failure(FailedSwipe);
                }

                bool isMatch = await IsMatch(guidSwiper, guidSwipee, request.SwipedRight);

                return Result<bool>.Success(isMatch);
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
