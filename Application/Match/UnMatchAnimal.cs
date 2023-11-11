﻿namespace Application.Match
{
    using MediatR;
    using Persistence.Repositories;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain;
    using static Common.ExceptionMessages.Animal;
    using static Common.ExceptionMessages.Match;

    public class UnMatchAnimal
    {
        public class UnMatchAnimalCommand : IRequest<Unit>
        {
            public Guid AnimalOneId { get; set; }

            public Guid AnimalTwoId { get; set; }
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
                if (await this.repository.AnyAsync<Animal>(animal => animal.AnimalId == request.AnimalOneId) == false)
                {
                    throw new InvalidOperationException(AnimalNotFound);
                }

                if (await this.repository.AnyAsync<Animal>(animal => animal.AnimalId == request.AnimalTwoId) == false)
                {
                    throw new InvalidOperationException(AnimalNotFound);
                }

                Match? existingMatch = await this.repository.GetByIds<Match>(new
                {
                    request.AnimalOneId,
                    request.AnimalTwoId
                });

                if (existingMatch == null)
                {
                    throw new InvalidOperationException(NotMatched);
                }

                this.repository.Delete(existingMatch);
                await this.repository.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
