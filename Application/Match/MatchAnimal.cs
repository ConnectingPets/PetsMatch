namespace Application.Matches
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Domain;

    using Persistence.Repositories;
    using Application.Exceptions;

    public class MatchAnimal
    {
        public class MatchAnimalCommand : IRequest<Unit>
        {
            public required string AnimalOneId { get; set; }

            public required string AnimalTwoId { get; set; }
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
                if (!Guid.TryParse(request.AnimalOneId, out Guid animalOneId))
                {
                    throw new InvalidGuidFormatException();
                }

                if (!Guid.TryParse(request.AnimalTwoId, out Guid animalTwoId))
                {
                    throw new InvalidGuidFormatException();
                }

                if (await this.repository.AnyAsync<Animal>(animal => animal.AnimalId == animalOneId) == false)
                {
                    throw new AnimalNotFoundException();
                }

                if (await this.repository.AnyAsync<Animal>(animal => animal.AnimalId == animalTwoId) == false)
                {
                    throw new AnimalNotFoundException();
                }

                if (request.AnimalOneId.ToString() == request.AnimalTwoId.ToString())
                {
                    throw new SameAnimalException();
                }

                bool isPresentMatch = await IsPresentMatch(animalOneId, animalTwoId);

                if (isPresentMatch)
                {
                    throw new AlreadyMatchedException();
                }

                bool isMatch = await IsMatch(animalOneId, animalTwoId);

                if (isMatch)
                {
                    await CreateMatch(animalOneId, animalTwoId);
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
