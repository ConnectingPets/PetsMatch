namespace Application.Matches
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Domain;

    using Persistence.Repositories;
    using Application.Exceptions;
    using Application.Exceptions.Entity;

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

                await CreateMatch(animalOneId, animalTwoId);

                return Unit.Value;
            }

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
                => await this.repository.AnyAsync<AnimalMatch>(am => am.AnimalId == animalOneId &&
                                                               am.Match.AnimalMatches
                                                                   .Any(m => m.AnimalId == animalTwoId));
        }
    }
}
