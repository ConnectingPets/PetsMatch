namespace Application.Swipe
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Domain;

    using Persistence.Repositories;
    using static Common.ExceptionMessages.Animal;

    public class SwipeUser
    {
        public class SwipeUserCommand : IRequest<bool>
        {
            public Guid SwiperAnimalId { get; set; }

            public Guid SwipeeAnimalId { get; set; }

            public bool SwipedRight { get; set; }
        }

        public class SwipeUserHandler : IRequestHandler<SwipeUserCommand, bool>
        {
            private readonly IRepository repository;

            public SwipeUserHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<bool> Handle(SwipeUserCommand request, CancellationToken cancellationToken)
            {
                if (await this.repository.AnyAsync<Animal>(animal => animal.AnimalId == request.SwiperAnimalId) == false)
                {
                    throw new InvalidOperationException(AnimalNotFound);
                }

                if (await this.repository.AnyAsync<Animal>(animal => animal.AnimalId == request.SwipeeAnimalId) == false)
                {
                    throw new InvalidOperationException(AnimalNotFound);
                }

                Swipe swipe = new Swipe
                {
                    SwiperAnimalId = request.SwiperAnimalId,
                    SwipeeAnimalId = request.SwipeeAnimalId,
                    SwipedRight = request.SwipedRight,
                    SwipedOn = DateTime.Now
                };

                await this.repository.AddAsync(swipe);
                await this.repository.SaveChangesAsync();

                return await this.repository.AnyAsync<Swipe>(swipe => 
                    swipe.SwiperAnimalId == request.SwipeeAnimalId &&
                    swipe.SwipeeAnimalId == request.SwiperAnimalId &&
                    swipe.SwipedRight &&
                    request.SwipedRight);
            }
        }
    }
}
