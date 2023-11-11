namespace Application.Swipe
{
    using Domain;
    using MediatR;
    using Persistence.Repositories;
    using System.Threading;
    using System.Threading.Tasks;

    public class SwipeUser
    {
        public class SwipeUserCommand : IRequest<Unit>
        {
            public Guid SwiperAnimalId { get; set; }

            public Guid SwipeeAnimalId { get; set; }

            public bool SwipedRight { get; set; }
        }

        public class SwipeUserHandler : IRequestHandler<SwipeUserCommand, Unit>
        {
            private readonly IRepository repository;

            public SwipeUserHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Unit> Handle(SwipeUserCommand request, CancellationToken cancellationToken)
            {
                Swipe swipe = new Swipe
                {
                    SwiperAnimalId = request.SwiperAnimalId,
                    SwipeeAnimalId = request.SwipeeAnimalId,
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
