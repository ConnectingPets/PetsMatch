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

    using static Common.ExceptionMessages.Animal;
    using static Common.ExceptionMessages.User;

    public class AnimalMatches
    {
        public class AnimalMatchesQuery : IRequest<Result<IEnumerable<AnimalMatchDto>>>
        {
            public string AnimalId { get; set; } = null!;

            public string UserId { get; set; } = null!;
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
                    .All<Animal>(animal => animal.AnimalId.ToString() == request.AnimalId.ToLower())
                    .Include(animal => animal.AnimalMatches)
                    .FirstOrDefaultAsync();

                if (animal == null)
                {
                    return Result<IEnumerable<AnimalMatchDto>>.Failure(AnimalNotFound);
                }

                if (await this.repository.AnyAsync<User>(u => u.Id.ToString() == request.UserId.ToLower()) == false)
                {
                    return Result<IEnumerable<AnimalMatchDto>>.Failure(UserNotFound);
                }

                if (animal.OwnerId.ToString() != request.UserId.ToLower())
                {
                    return Result<IEnumerable<AnimalMatchDto>>.Failure(NotOwner);
                }

                IEnumerable<string> matchesIds = animal.AnimalMatches
                    .Select(am => am.MatchId.ToString());

                IEnumerable<AnimalMatchDto> animalMatches = new List<AnimalMatchDto>();
                if (matchesIds.Any())
                {
                    IEnumerable<AnimalMatch> matches = await this.repository
                        .All<AnimalMatch>(am => matchesIds.Contains(am.MatchId.ToString()) &&
                            am.AnimalId.ToString() != request.AnimalId.ToLower())
                        .Include(am => am.Animal)
                        .ThenInclude(a => a.Photos)
                        .Include(am => am.Match.Messages)
                        .ToListAsync();

                    animalMatches = matches
                        .Select(am => new AnimalMatchDto
                        {
                            AnimalId = am.AnimalId.ToString(),
                            Name = am.Animal.Name,
                            Photo = am.Animal.Photos.First(p => p.IsMain).Url,
                            MatchId = am.MatchId.ToString(),
                            IsChatStarted = am.Match.Messages.Any(m => m.AnimalId.ToString() == request.AnimalId.ToLower())
                        })
                        .ToList();
                }

                return Result<IEnumerable<AnimalMatchDto>>.Success(animalMatches);
            }
        }
    }
}
