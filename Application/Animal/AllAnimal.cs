﻿namespace Application.Animal
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using Domain;
    using Response;
    using Application.DTOs.Animal;
    using Persistence.Repositories;

    using static Common.ExceptionMessages.Animal;
    using Domain.Enum;

    public class AllAnimal
    {
        public class AllAnimalQuery : IRequest<Result<IEnumerable<AllAnimalDto>>>
        {
            public string OwnerId { get; set; } = null!;
        }

        public class AllAnimalQueryHandler :
            IRequestHandler<AllAnimalQuery, Result<IEnumerable<AllAnimalDto>>>
        {
            private readonly IRepository repository;

            public AllAnimalQueryHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Result<IEnumerable<AllAnimalDto>>> Handle(AllAnimalQuery request, CancellationToken cancellationToken)
            {
                string userId = request.OwnerId;
                User? user = await repository.
                    All<User>(u => u.Id.ToString() == userId).
                    Include(u => u.Animals).
                    ThenInclude(a => a.Photos).
                    FirstOrDefaultAsync();

                var userAnimals = user!.Animals.
                    Where(a => a.AnimalStatus == AnimalStatus.ForSwiping).
                    Select(a => new AllAnimalDto()
                    {
                        Id = a.AnimalId.ToString(),
                        Name = a.Name,
                        MainPhoto = a.Photos.First(p => p.IsMain).Url
                    }).ToList();

                if (!userAnimals.Any())
                {
                    return Result<IEnumerable<AllAnimalDto>>.Failure(NoPets);
                }

                return Result<IEnumerable<AllAnimalDto>>.Success(userAnimals);
            }
        }
    }
}
