namespace Application.Match
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using Domain;
    using Persistence.Repositories;
    using Application.DTOs.Match;
    using Application.Response;

    public class AnimalMatches
    {
        public class AnimalMatchesQuery : IRequest<Result<IEnumerable<AnimalMatchDto>>>
        {
            public required string AnimalId { get; set; }
        }

        public class AnimalMatchesHandler : IRequestHandler<AnimalMatchesQuery, Result<IEnumerable<AnimalMatchDto>>>
        {
            private readonly IRepository repository;

            public AnimalMatchesHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Result<IEnumerable<AnimalMatchDto>>> Handle(AnimalMatchesQuery request, CancellationToken cancellationToken)
            {
                Animal? animal = await this.repository
                    .All<Animal>(animal => animal.AnimalId.ToString() == request.AnimalId)
                    .Include(animal => animal.AnimalMatches)
                    .ThenInclude(am => am.Match)
                    .ThenInclude(m => m.AnimalMatches)
                    .ThenInclude(am => am.Animal)
                    .ThenInclude(a => a.Photos)
                    .FirstOrDefaultAsync();

                if (animal == null)
                {
                    return Result<IEnumerable<AnimalMatchDto>>.Failure("");
                }

                IEnumerable<AnimalMatch> matches = animal.AnimalMatches
                    .Select(am => am.Match.AnimalMatches
                        .FirstOrDefault(am => am.AnimalId.ToString() != request.AnimalId))!;

                IEnumerable<AnimalMatchDto> animalMatches = matches
                    .Select(am => new AnimalMatchDto
                    {
                        AnimalId = am.AnimalId.ToString(),
                        Name = am.Animal.Name,
                        Photo = am.Animal.Photos.First(p => p.IsMain).Url
                    })
                    .ToList();

                return Result<IEnumerable<AnimalMatchDto>>.Success(animalMatches);
            }
        }
    }
}
