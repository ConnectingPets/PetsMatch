namespace Application.Matches
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Domain;

    using Persistence.Repositories;
    using static Common.ExceptionMessages.Match;
    using static Common.ExceptionMessages.Animal;
    using Azure.Core;

    public class MatchAnimal
    {
        public class MatchAnimalCommand : IRequest<Unit>
        {
            public Guid AnimalOneId { get; set; }

            public Guid AnimalTwoId { get; set; }

            public bool SwipedRight { get; set; }
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
                    throw new InvalidOperationException(AnimalNotFound);
                }

                if (await this.repository.AnyAsync<Animal>(animal => animal.AnimalId == request.AnimalTwoId) == false)
                {
                    throw new InvalidOperationException(AnimalNotFound);
                }

                bool isPresentMatch = await IsPresentMatch(request.AnimalOneId, request.AnimalTwoId);

                if (isPresentMatch)
                {
                    throw new InvalidOperationException(AlreadyMatched);
                }

                bool isMatch = await IsMatch(request.AnimalOneId, request.AnimalTwoId, request.SwipedRight);

                if (isMatch)
                {
                    await CreateMatch(request.AnimalOneId, request.AnimalTwoId);
                }

                return Unit.Value;
            }

            private async Task<bool> IsMatch(Guid animalOneId, Guid animalTwoId, bool swipedRight)
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
                => await this.repository.AnyAsync<Match>(match =>
                    match.AnimalMatches.Count(am => am.AnimalId == animalOneId) == 1 &&
                    match.AnimalMatches.Count(am => am.AnimalId == animalTwoId) == 1
                   );
        }
    }
}
