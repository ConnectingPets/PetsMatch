namespace Application.Matches
{
    using Domain;
    using MediatR;
    using Persistence.Repositories;
    using System.Threading;
    using System.Threading.Tasks;

    public class MatchUser
    {
        public class MatchUserCommand : IRequest<Unit>
        {
            public Guid AnimalOneId { get; set; }

            public Guid AnimalTwoId { get; set; }
        }

        public class MatchUserHandler : IRequestHandler<MatchUserCommand, Unit>
        {
            private readonly IRepository repository;

            public MatchUserHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Unit> Handle(MatchUserCommand request, CancellationToken cancellationToken)
            {
                Match match = new Match
                {
                    AnimalOneId = request.AnimalOneId,
                    AnimalTwoId = request.AnimalTwoId,
                    MatchOn = DateTime.Now
                };

                await repository.AddAsync(match);
                await repository.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
