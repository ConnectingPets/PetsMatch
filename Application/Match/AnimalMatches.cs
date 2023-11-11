﻿namespace Application.Match
{
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MediatR;

    using Domain;
    using Application.DTOs;
    using Persistence.Repositories;
    using static Common.ExceptionMessages.Animal;

    public class AnimalMatches
    {
        public class AnimalMatchesQuery : IRequest<IEnumerable<UserMatchDto>>
        {
            public Guid AnimalId { get; set; }
        }

        public class AnimalMatchesHandler : IRequestHandler<AnimalMatchesQuery, IEnumerable<UserMatchDto>>
        {
            private readonly IRepository repository;

            public AnimalMatchesHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<IEnumerable<UserMatchDto>> Handle(AnimalMatchesQuery request, CancellationToken cancellationToken)
            {
                Animal? animal = await this.repository
                    .AllReadonly<Animal>(animal => animal.AnimalId == request.AnimalId)
                    .Include(animal => animal.Matches)
                    .FirstOrDefaultAsync();

                if (animal == null)
                {
                    throw new InvalidOperationException(AnimalNotFound);
                }

                return animal.Matches
                    .Select(m => new UserMatchDto
                    {
                        AnimalId = default(Guid)
                    })
                    .ToList();
            }
        }
    }
}
