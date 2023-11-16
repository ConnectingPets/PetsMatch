namespace Application.Matches
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Domain;

    using Persistence.Repositories;
    using static Common.ExceptionMessages.Match;
    using static Common.ExceptionMessages.Animal;
    using Application.Exceptions;

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
                if (await this.repository.AnyAsync<Animal>(animal => animal.AnimalId == request.AnimalOneId) == false)
                {
                    throw new AnimalNotFoundException(AnimalNotFound);
                }

                if (await this.repository.AnyAsync<Animal>(animal => animal.AnimalId == request.AnimalTwoId) == false)
                {
                    throw new AnimalNotFoundException(AnimalNotFound);
                }

                if (request.AnimalOneId.ToString() == request.AnimalTwoId.ToString())
                {
                    throw new SameAnimalException(SameAnimal);
                }

                bool isPresentMatch = await IsPresentMatch(request.AnimalOneId, request.AnimalTwoId);

                if (isPresentMatch)
                {
                    throw new AlreadyMatchedException(AlreadyMatched);
                }

                bool isMatch = await IsMatch(request.AnimalOneId, request.AnimalTwoId);

                if (isMatch)
                {
                    await CreateMatch(request.AnimalOneId, request.AnimalTwoId);
                }

                return Unit.Value;
            }

            private async Task<bool> IsMatch(Guid animalOneId, Guid animalTwoId)
                => await this.repository.CountAsync<Swipe>(swipe =>
                    (swipe.SwiperAnimalId == animalTwoId && swipe.SwipeeAnimalId == animalOneId && swipe.SwipedRight) ||
                    (swipe.SwiperAnimalId == animalOneId && swipe.SwipeeAnimalId == animalTwoId && swipe.SwipedRight)
                   ) == 2;

            private async Task CreateMatch(Guid animalOneId, Guid animalTwoId)
            {
                Match match = new Match
                {
                    MatchOn = DateTime.Now
                };

                match.AnimalMatches.Add(new AnimalMatch
                {
                    AnimalId = animalOneId
                });

                match.AnimalMatches.Add(new AnimalMatch
                {
                    AnimalId = animalTwoId
                });

                await repository.AddAsync(match);
                await repository.SaveChangesAsync();
            }

            private async Task<bool> IsPresentMatch(Guid animalOneId, Guid animalTwoId)
                => await this.repository.AnyAsync<Match>(m =>
                    m.AnimalMatches.Count == 2 &&
                    m.AnimalMatches.Any(am => am.AnimalId == animalOneId) &&
                    m.AnimalMatches.Any(am => am.AnimalId == animalTwoId)
                   );
        }
    }
}
