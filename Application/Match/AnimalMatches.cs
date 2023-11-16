namespace Application.Match
{
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MediatR;

    using Domain;
    using Application.DTOs;
    using Persistence.Repositories;
    using static Common.ExceptionMessages.Animal;
    using Application.Exceptions;

    public class AnimalMatches
    {
        public class AnimalMatchesQuery : IRequest<IEnumerable<AnimalMatchDto>>
        {
            public Guid AnimalId { get; set; }
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
                Animal? animal = await this.repository
                    .AllReadonly<Animal>(animal => animal.AnimalId == request.AnimalId)
                    .Include(animal => animal.AnimalMatches)
                    .ThenInclude(animalMatch => animalMatch.Animal)
                    .FirstOrDefaultAsync();

                if (animal == null)
                {
                    throw new AnimalNotFoundException(AnimalNotFound);
                }

                return animal.AnimalMatches
                    .Select(am => new AnimalMatchDto
                    {
                        AnimalId = am.AnimalId.ToString(),
                        Name = am.Animal.Name,
                        Photo = Convert.ToBase64String(am.Animal.Photo)
                    })
                    .ToList();
            }
        }
    }
}
