namespace Application.Match
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Domain;
    using Persistence.Repositories;
    using Application.Exceptions;
    using Microsoft.EntityFrameworkCore;
    using Application.Exceptions.Entity;

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

                this.repository.DeleteRange(existingMatch.AnimalMatches.ToArray());
                this.repository.DeleteRange(existingMatch.Messages.ToArray());
                this.repository.Delete(existingMatch);
                await this.repository.SaveChangesAsync();

                return Unit.Value;
            }

            private async Task<Match?> GetExistingMatch(Guid animalOneId, Guid animalTwoId)
                => await this.repository.All<AnimalMatch>(am => am.AnimalId == animalOneId &&
                                            am.Match.AnimalMatches
                                                .Any(m => m.AnimalId == animalTwoId))
                                        .Include(am => am.Match)
                                        .ThenInclude(m => m.AnimalMatches)
                                        .Include(m => m.Match.Messages)
                                        .Select(am => am.Match)
                                        .FirstOrDefaultAsync();
        }
    }
}
