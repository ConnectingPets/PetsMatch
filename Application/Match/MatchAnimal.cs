namespace Application.Matches
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    using Domain;
    using Persistence.Repositories;
    using Application.Response;

    using static Common.ExceptionMessages.Animal;
    using static Common.ExceptionMessages.Match;
    using static Common.SuccessMessages.Match;
    using static Common.FailMessages.Match;

    public class MatchAnimal
    {
        public class MatchAnimalCommand : IRequest<Result<Unit>>
        {
            public required string AnimalOneId { get; set; }

            public required string AnimalTwoId { get; set; }
        }

        public class MatchAnimalHandler : IRequestHandler<MatchAnimalCommand, Result<Unit>>
        {
            private readonly IRepository repository;

            public MatchAnimalHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Result<Unit>> Handle(MatchAnimalCommand request, CancellationToken cancellationToken)
            {
                if (await this.repository.AnyAsync<Animal>(animal => animal.AnimalId.ToString() == request.AnimalOneId) == false)
                {
                    return Result<Unit>.Failure(AnimalNotFound);
                }

                if (await this.repository.AnyAsync<Animal>(animal => animal.AnimalId.ToString() == request.AnimalTwoId) == false)
                {
                    return Result<Unit>.Failure(AnimalNotFound);
                }

                if (request.AnimalOneId.ToString() == request.AnimalTwoId.ToString())
                {
                    return Result<Unit>.Failure(SameAnimal);
                }

                bool isPresentMatch = await IsPresentMatch(request.AnimalOneId, request.AnimalTwoId);

                if (isPresentMatch)
                {
                    return Result<Unit>.Failure(AlreadyMatched);
                }

                try
                {
                    await CreateMatch(request.AnimalOneId, request.AnimalTwoId);
                    return Result<Unit>.Success(Unit.Value, SuccessMatch);
                }
                catch (Exception)
                {
                    return Result<Unit>.Failure(FailedMatch);
                }
            }

            private async Task CreateMatch(string animalOneId, string animalTwoId)
            {
                Match match = new Match
                {
                    MatchOn = DateTime.Now
                };

                match.AnimalMatches.Add(new AnimalMatch
                {
                    AnimalId = Guid.Parse(animalOneId)
                });

                match.AnimalMatches.Add(new AnimalMatch
                {
                    AnimalId = Guid.Parse(animalTwoId)
                });

                await repository.AddAsync(match);
                await repository.SaveChangesAsync();
            }

            private async Task<bool> IsPresentMatch(string animalOneId, string animalTwoId)
                => await this.repository.AnyAsync<AnimalMatch>(am => am.AnimalId.ToString() == animalOneId &&
                                                               am.Match.AnimalMatches
                                                                   .Any(m => m.AnimalId.ToString() == animalTwoId));
        }
    }
}
