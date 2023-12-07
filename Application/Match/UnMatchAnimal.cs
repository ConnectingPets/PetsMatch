namespace Application.Match
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using Domain;
    using Persistence.Repositories;
    using Application.Response;

    using static Common.ExceptionMessages.Animal;
    using static Common.ExceptionMessages.Match;
    using static Common.SuccessMessages.Match;
    using static Common.FailMessages.Match;
    using static Common.ExceptionMessages.User;

    public class UnMatchAnimal
    {
        public class UnMatchAnimalCommand : IRequest<Result<Unit>>
        {
            public required string AnimalOneId { get; set; }

            public required string AnimalTwoId { get; set; }

            public required string UserId { get; set; }
        }

        public class UnMatchAnimalHandler : IRequestHandler<UnMatchAnimalCommand, Result<Unit>>
        {
            private readonly IRepository repository;

            public UnMatchAnimalHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Result<Unit>> Handle(UnMatchAnimalCommand request, CancellationToken cancellationToken)
            {
                Animal? animalOne = await this.repository.FirstOrDefaultAsync<Animal>(animal => animal.AnimalId.ToString() == request.AnimalOneId.ToLower());
                if (animalOne == null)
                {
                    return Result<Unit>.Failure(AnimalNotFound);
                }

                Animal? animalTwo = await this.repository.FirstOrDefaultAsync<Animal>(animal => animal.AnimalId.ToString() == request.AnimalTwoId.ToLower());
                if (animalTwo == null)
                {
                    return Result<Unit>.Failure(AnimalNotFound);
                }

                if (await this.repository.AnyAsync<User>(u => u.Id.ToString() == request.UserId))
                {
                    return Result<Unit>.Failure(UserNotFound);
                }

                if (request.AnimalOneId.ToString() == request.AnimalTwoId.ToString())
                {
                    return Result<Unit>.Failure(SameAnimal);
                }

                if (animalOne.OwnerId.ToString() != request.UserId.ToLower() &&
                    animalTwo.OwnerId.ToString() != request.UserId.ToLower())
                {
                    return Result<Unit>.Failure(NotOwner);
                }

                Match? existingMatch = await GetExistingMatch(
                    request.AnimalOneId,
                    request.AnimalTwoId
                );

                if (existingMatch == null)
                {
                    return Result<Unit>.Failure(MatchNotFound);
                }

                this.repository.DeleteRange(existingMatch.AnimalMatches.ToArray());
                this.repository.DeleteRange(existingMatch.Messages.ToArray());
                this.repository.Delete(existingMatch);

                try
                {
                    await this.repository.SaveChangesAsync();
                    return Result<Unit>.Success(Unit.Value, SuccessUnMatch);
                }
                catch (Exception)
                {
                    return Result<Unit>.Failure(FailedUnMatch);
                }
            }

            private async Task<Match?> GetExistingMatch(string animalOneId, string animalTwoId)
                => await this.repository.All<AnimalMatch>(am => am.AnimalId.ToString() == animalOneId &&
                                            am.Match.AnimalMatches
                                                .Any(m => m.AnimalId.ToString() == animalTwoId))
                                        .Include(am => am.Match)
                                        .ThenInclude(m => m.AnimalMatches)
                                        .Include(m => m.Match.Messages)
                                        .Select(am => am.Match)
                                        .FirstOrDefaultAsync();
        }
    }
}
