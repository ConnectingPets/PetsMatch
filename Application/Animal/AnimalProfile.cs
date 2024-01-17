namespace Application.Animal
{
    using System.Threading.Tasks;
    using System.Threading;
    
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    
    using Domain;
    using Persistence.Repositories;
    using Application.DTOs.Animal;
    using Application.DTOs.Photo;
    using Application.Response;
    
    using static Common.ExceptionMessages.Animal;

    public class AnimalProfile
    {
        public class AnimalProfileQuery : IRequest<Result<AnimalProfileDto>>
        {
            public string AnimalId { get; set; } = null!;
        }

        public class AnimalProfileHandler : IRequestHandler<AnimalProfileQuery, Result<AnimalProfileDto>>
        {
            private readonly IRepository repository;

            public AnimalProfileHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Result<AnimalProfileDto>> Handle(AnimalProfileQuery request, CancellationToken cancellationToken)
            {
                Animal? animal = await this.repository.All<Animal>(a => a.AnimalId.ToString() == request.AnimalId.ToLower())
                    .Include(a => a.Breed)
                    .Include(a => a.Photos)
                    .FirstOrDefaultAsync();

                if (animal == null)
                {
                    return Result<AnimalProfileDto>.Failure(AnimalNotFound);
                }

                AnimalProfileDto animalProfileDto = new AnimalProfileDto
                {
                    AnimalId = animal.AnimalId.ToString(),
                    Name = animal.Name,
                    Description = animal.Description,
                    BirthDate = animal.BirthDate,
                    Age = animal.Age,
                    Breed = animal.Breed.Name,
                    Gender = animal.Gender.ToString(),
                    HealthStatus = animal.HealthStatus.ToString(),
                    IsEducated = animal.IsEducated,
                    IsHavingValidDocuments = animal.IsHavingValidDocuments,
                    SocialMedia = animal.SocialMedia,
                    Weight = animal.Weight,
                    Photos = animal.Photos.Select(p => new PhotoDto
                    {
                        Id = p.Id,
                        IsMain = p.IsMain,
                        Url = p.Url
                    }).ToList()
                };

                return Result<AnimalProfileDto>.Success(animalProfileDto);
            }
        }
    }
}
