﻿namespace Application.Swipe
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Domain;

    using Persistence.Repositories;
    using static Common.ExceptionMessages.Animal;
    using Application.Exceptions;

    public class SwipeAnimal
    {
        public class SwipeAnimalCommand : IRequest<Unit>
        {
            public Guid SwiperAnimalId { get; set; }

            public Guid SwipeeAnimalId { get; set; }

            public bool SwipedRight { get; set; }
        }

        public class SwipeAnimalHandler : IRequestHandler<SwipeAnimalCommand, Unit>
        {
            private readonly IRepository repository;

            public SwipeAnimalHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Unit> Handle(SwipeAnimalCommand request, CancellationToken cancellationToken)
            {
                if (await this.repository.AnyAsync<Animal>(animal => animal.AnimalId == request.SwiperAnimalId) == false)
                {
                    throw new AnimalNotFoundException(AnimalNotFound);
                }

                if (await this.repository.AnyAsync<Animal>(animal => animal.AnimalId == request.SwipeeAnimalId) == false)
                {
                    throw new AnimalNotFoundException(AnimalNotFound);
                }

                if (request.SwiperAnimalId.ToString() == request.SwipeeAnimalId.ToString())
                {
                    throw new SameAnimalException(SameAnimal);
                }

                Swipe swipe = new Swipe
                {
                    SwiperAnimalId = request.SwiperAnimalId,
                    SwipeeAnimalId = request.SwipeeAnimalId,
                    SwipedRight = request.SwipedRight,
                    SwipedOn = DateTime.Now
                };

                await this.repository.AddAsync(swipe);
                await this.repository.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
