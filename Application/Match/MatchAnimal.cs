namespace Application.Matches
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Domain;

    using Persistence.Repositories;
    using static Common.ExceptionMessages.Match;

    public class MatchAnimal
    {
        public class MatchAnimalCommand : IRequest<Unit>
        {
            public Guid AnimalOneId { get; set; }

            public Guid AnimalTwoId { get; set; }
        }

        public class MatchAnimalHandler : IRequestHandler<MatchAnimalCommand, Unit>
        {
            private readonly IRepository repository;

            public MatchAnimalHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Unit> Handle(MatchAnimalCommand request, CancellationToken cancellationToken)
            {
                Match? existestingMatch = await this.repository.GetByIds<Match>(new
                {
                    request.AnimalOneId,
                    request.AnimalTwoId,
                });

                if (existestingMatch != null)
                {
                    throw new InvalidOperationException(AlreadyMatched);
                }

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
