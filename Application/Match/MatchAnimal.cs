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
    using static Common.ExceptionMessages.User;

    public class MatchAnimal
    {
        public class MatchAnimalCommand : IRequest<Result<Unit>>
        {
            public required string AnimalOneId { get; set; }

            public required string AnimalTwoId { get; set; }

            public required string UserId { get; set; }
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

                if (await this.repository.AnyAsync<User>(u => u.Id.ToString() == request.UserId) == false)
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

                bool isPresentMatch = await IsPresentMatch(request.AnimalOneId, request.AnimalTwoId);

                if (isPresentMatch)
                {
                    return Result<Unit>.Failure(AlreadyMatched);
                }

                bool isMatch = await IsMatch(request.AnimalOneId, request.AnimalTwoId);

                if (!isMatch)
                {
                    return Result<Unit>.Failure(NotMatched);
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
                => await this.repository.AnyAsync<AnimalMatch>(am => am.AnimalId.ToString() == animalOneId.ToLower() &&
                                                               am.Match.AnimalMatches
                                                                   .Any(m => m.AnimalId.ToString() == animalTwoId.ToLower()));

            private async Task<bool> IsMatch(string animalOneId, string animalTwoId)
                => await this.repository.CountAsync<Swipe>(s => (s.SwiperAnimalId.ToString() == animalOneId.ToLower() &&
                                                                            s.SwipeeAnimalId.ToString() == animalTwoId.ToLower() && s.SwipedRight) ||
                                                                 s.SwiperAnimalId.ToString() == animalTwoId.ToLower() &&
                                                                    s.SwipeeAnimalId.ToString() == animalOneId.ToLower() && s.SwipedRight) == 2;
        }
    }
}
