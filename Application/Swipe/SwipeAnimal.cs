namespace Application.Swipe
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Domain;

    using Persistence.Repositories;
    using Application.Exceptions;

    public class SwipeAnimal
    {
        public class SwipeAnimalCommand : IRequest<Unit>
        {
            public required string SwiperAnimalId { get; set; }

            public required string SwipeeAnimalId { get; set; }

            public required bool SwipedRight { get; set; }
        }

        public class SwipeAnimalHandler : IRequestHandler<SwipeAnimalCommand, Unit>
        {
            private readonly IRepository repository;

            public SwipeAnimalHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Unit> Handle(SwipeAnimalCommand request, CancellationToken cancellationToken)
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

                return Unit.Value;
            }
        }
    }
}
