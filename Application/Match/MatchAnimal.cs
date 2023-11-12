namespace Application.Matches
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Domain;

    using Persistence.Repositories;
    using static Common.ExceptionMessages.Match;
    using static Common.ExceptionMessages.Animal;

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
                    Match match = new Match
                    {
                        AnimalOneId = request.AnimalOneId,
                        AnimalTwoId = request.AnimalTwoId,
                        MatchOn = DateTime.Now
                    };

                    await repository.AddAsync(match);
                    await repository.SaveChangesAsync();
                }

                return Unit.Value;
            }

            private async Task<bool> IsMatch(Guid animalOneId, Guid animalTwoId, bool swipedRight)
                => await this.repository.AnyAsync<Swipe>(swipe =>
                    swipe.SwiperAnimalId == animalTwoId &&
                    swipe.SwipeeAnimalId == animalOneId &&
                    swipe.SwipedRight &&
                    swipedRight
                );

            private async Task<bool> IsPresentMatch(Guid animalOneId, Guid animalTwoId)
                => await this.repository.AnyAsync<Match>(match =>
                    (match.AnimalOneId == animalOneId && match.AnimalTwoId == animalTwoId) ||
                    (match.AnimalOneId == animalTwoId && match.AnimalTwoId == animalOneId)
                );
        }
    }
}
