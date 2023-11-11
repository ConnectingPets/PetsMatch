namespace Application.Match
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Domain;
    using Persistence.Repositories;

    public class IsMatch
    {
        public class IsMatchQuery : IRequest<bool>
        {
            public Guid SwiperAnimalId { get; set; }

            public Guid SwipeeAnimalId { get; set; }

            public bool SwipedRight { get; set; }
        }

        public class IsMatchHandler : IRequestHandler<IsMatchQuery, bool>
        { 
            private readonly IRepository repository;

            public IsMatchHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<bool> Handle(IsMatchQuery request, CancellationToken cancellationToken)
                => await this.repository.AnyAsync<Swipe>(swipe =>
                    swipe.SwiperAnimalId == request.SwipeeAnimalId &&
                    swipe.SwipeeAnimalId == request.SwiperAnimalId &&
                    swipe.SwipedRight &&
                    request.SwipedRight);
        }
    }
}
