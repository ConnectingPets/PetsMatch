namespace Application.Match
{
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MediatR;

    using Domain;
    using Persistence.Repositories;
    using Application.Exceptions;
    using Application.DTOs.Animal;

    public class AnimalMatches
    {
        public class AnimalMatchesQuery : IRequest<IEnumerable<AnimalMatchDto>>
        {
            public required string AnimalId { get; set; }
        }

        public class AnimalMatchesHandler : IRequestHandler<AnimalMatchesQuery, IEnumerable<AnimalMatchDto>>
        {
            private readonly IRepository repository;

            public AnimalMatchesHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<IEnumerable<AnimalMatchDto>> Handle(AnimalMatchesQuery request, CancellationToken cancellationToken)
            {
                if (!Guid.TryParse(request.AnimalId, out Guid animalId))
                {
                    throw new InvalidGuidFormatException();
                }

                Animal? animal = await this.repository
                    .All<Animal>(animal => animal.AnimalId == animalId)
                    .Include(animal => animal.AnimalMatches)
                    .ThenInclude(am => am.Match)
                    .ThenInclude(m => m.AnimalMatches)
                    .ThenInclude(am => am.Animal)
                    .FirstOrDefaultAsync();

                if (animal == null)
                {
                    throw new AnimalNotFoundException();
                }

                IEnumerable<AnimalMatch> matches = animal.AnimalMatches
                    .Select(am => am.Match.AnimalMatches
                        .FirstOrDefault(am => am.AnimalId != animalId))!;

                return matches
                    .Select(am => new AnimalMatchDto
                    {
                        AnimalId = am.AnimalId.ToString(),
                        Name = am.Animal.Name,
                        Photo = null
                    })
                    .ToList();
            }
        }
    }
}
