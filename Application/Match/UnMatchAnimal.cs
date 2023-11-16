namespace Application.Match
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Domain;
    using Persistence.Repositories;
    using Application.Exceptions;

    public class UnMatchAnimal
    {
        public class UnMatchAnimalCommand : IRequest<Unit>
        {
            public required string AnimalOneId { get; set; }

            public required string AnimalTwoId { get; set; }
        }

        public class UnMatchAnimalHandler : IRequestHandler<UnMatchAnimalCommand, Unit>
        {
            private readonly IRepository repository;

            public UnMatchAnimalHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Unit> Handle(UnMatchAnimalCommand request, CancellationToken cancellationToken)
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

                Match? existingMatch = await GetExistingMatch(
                    animalOneId,
                    animalTwoId
                );

                if (existingMatch == null)
                {
                    throw new MatchNotFoundException();
                }

                this.repository.DeleteRange<AnimalMatch>(am => am.MatchId == existingMatch.MatchId);
                this.repository.Delete(existingMatch);
                await this.repository.SaveChangesAsync();

                return Unit.Value;
            }

            private async Task<Match?> GetExistingMatch(Guid animalOneId, Guid animalTwoId)
                => await this.repository.FirstOrDefaultAsync<Match>(m =>
                    m.AnimalMatches.Count == 2 &&
                    m.AnimalMatches.Any(am => am.AnimalId == animalOneId) &&
                    m.AnimalMatches.Any(am => am.AnimalId == animalTwoId)
                   );
        }
    }
}
