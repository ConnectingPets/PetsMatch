﻿namespace Application.Marketplace
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using Domain;
    using Response;
    using DTOs.Animal;
    using Domain.Enum;
    using Persistence.Repositories;

    using static Common.ExceptionMessages.Marketplace;

    public class AllAnimalsForAdoption
    {
        public class AllAnimalsForAdoptionQuery : IRequest<Result<IEnumerable<AllAnimalsDto>>>
        {
            public string UserId { get; set; } = null!;
        }

        public class AllAnimalsForAdoptionQueryHandler : IRequestHandler<AllAnimalsForAdoptionQuery, Result<IEnumerable<AllAnimalsDto>>>
        {
            private readonly IRepository repository;

            public AllAnimalsForAdoptionQueryHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Result<IEnumerable<AllAnimalsDto>>> Handle(AllAnimalsForAdoptionQuery request, CancellationToken cancellationToken)
            {
                string userId = request.UserId;

                var allAnimals = await repository.
                    AllReadonly<Animal>(a => a.OwnerId.ToString() != userId && a.AnimalStatus == AnimalStatus.ForAdoption).
                    Select(a => new AllAnimalsDto()
                    {
                        Id = a.AnimalId.ToString(),
                        MainPhoto = a.Photos.First(a => a.IsMain).Url,
                        Name = a.Name,
                    }).ToArrayAsync();

                if (!allAnimals.Any())
                {
                    return Result<IEnumerable<AllAnimalsDto>>.Failure(NoAnimalsForAdoption);
                }

                return Result<IEnumerable<AllAnimalsDto>>.Success(allAnimals);
            }
        }
    }
}
