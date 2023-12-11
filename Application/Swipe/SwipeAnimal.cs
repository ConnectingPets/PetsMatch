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
    using static Common.ExceptionMessages.User;
    using static Common.FailMessages.Swipe;

    public class SwipeAnimal
    {
        public class SwipeAnimalCommand : IRequest<Result<bool>>
        {
            public required string SwiperAnimalId { get; set; }

            public required string SwipeeAnimalId { get; set; }

            public required bool SwipedRight { get; set; }

            public required string UserId { get; set; }
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
                Animal? swiper = await this.repository.All<Animal>(a => a.AnimalId.ToString() == request.SwiperAnimalId.ToLower())
                    .Include(a => a.SwipesFrom)
                    .FirstOrDefaultAsync();

                if (swiper == null)
                {
                    return Result<bool>.Failure(AnimalNotFound);
                }

                Animal? swipee = await this.repository.FirstOrDefaultAsync<Animal>(a => a.AnimalId.ToString() == request.SwipeeAnimalId.ToLower());
                if (swipee == null)
                {
                    return Result<bool>.Failure(AnimalNotFound);
                }

                if (await this.repository.AnyAsync<User>(u => u.Id.ToString() == request.UserId.ToLower()) == false)
                {
                    return Result<bool>.Failure(UserNotFound);
                }

                if (request.SwiperAnimalId == request.SwipeeAnimalId)
                {
                    return Result<bool>.Failure(SameAnimal);
                }

                if (swiper.OwnerId.ToString() != request.UserId.ToLower())
                {
                    return Result<bool>.Failure(NotOwner);
                }

                Swipe swipe = new Swipe
                {
                    SwiperAnimalId = Guid.Parse(request.SwiperAnimalId),
                    SwipeeAnimalId = Guid.Parse(request.SwipeeAnimalId),
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

                bool isMatch = this.IsMatch(swiper, request.SwipeeAnimalId, request.SwipedRight);

                return Result<bool>.Success(isMatch);
            }

            private bool IsMatch(Animal swiper, string swipeeAnimalId, bool swipedRight)
            {
                return swiper.SwipesFrom.Any(s => s.SwiperAnimalId.ToString() == swipeeAnimalId.ToLower() && s.SwipedRight) && swipedRight;
            }
        }
    }
}
