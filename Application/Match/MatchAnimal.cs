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

                bool isMatch = await IsMatch(request.AnimalOneId, request.AnimalTwoId, request.SwipedRight);

                Match? existingMatch = await this.repository.GetByIds<Match>(new
                {
                    request.AnimalOneId,
                    request.AnimalTwoId,
                });

                if (existingMatch != null)
                {
                    throw new InvalidOperationException(AlreadyMatched);
                }

                Match match = new Match
                {
                    AnimalOneId = request.AnimalOneId,
                    AnimalTwoId = request.AnimalTwoId,
                    MatchOn = DateTime.Now
                };

                await repository.AddAsync(match);
                await repository.SaveChangesAsync();

                return Unit.Value;
            }

            private async Task<bool> IsMatch(Guid animalOneId, Guid animalTwoId, bool swipedRight)
                => await this.repository.AnyAsync<Swipe>(swipe =>
                    swipe.SwiperAnimalId == animalTwoId &&
                    swipe.SwipeeAnimalId == animalOneId &&
                    swipe.SwipedRight &&
                    swipedRight);
        }
    }
}
