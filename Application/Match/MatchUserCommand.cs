namespace Application.Matches
{
    using Domain;
    using MediatR;
    using Persistence.Repositories;
    using System.Threading;
    using System.Threading.Tasks;

    public class MatchUserCommand
    {
        public class MatchAnimalsCommand : IRequest<Unit>
        {
            public Guid AnimalOneId { get; set; }

            public Guid AnimalTwoId { get; set; }
        }

        public class MatchAnimalsHandler : IRequestHandler<MatchAnimalsCommand, Unit>
        {
            private readonly IRepository repository;

            public MatchAnimalsHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Unit> Handle(MatchAnimalsCommand request, CancellationToken cancellationToken)
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
