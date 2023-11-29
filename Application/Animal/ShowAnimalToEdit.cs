namespace Application.Animal
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.EntityFrameworkCore;

    using Domain;
    using Response;
    using Persistence.Repositories;
    using Application.DTOs.Animal;
    using Application.DTOs.Breed;

    public class ShowAnimalToEdit
    {
        public class ShowAnimalToEditQuery : IRequest<Result<ShowAnimalToEditDto>>
        {
            public string AnimalId { get; set; } = null!;

            public string UserId { get; set; } = null!;
        }

        public class ShowAnimalToEditQueryHandler :
            IRequestHandler<ShowAnimalToEditQuery, Result<ShowAnimalToEditDto>>
        {
            private readonly IRepository repository;

            public ShowAnimalToEditQueryHandler(IRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Result<ShowAnimalToEditDto>> Handle(ShowAnimalToEditQuery request, CancellationToken cancellationToken)
            {
                Animal? animal =
                    await repository.All<Animal>().
                    Include(a => a.Photos).
                    FirstOrDefaultAsync(a => a.AnimalId.ToString() == request.AnimalId);

                if (animal == null)
                {
                    return Result<ShowAnimalToEditDto>.Failure("This pet does not exist! Please select existing one");
                }
                if (animal.OwnerId.ToString() != request.UserId.ToLower())
                {
                    return Result<ShowAnimalToEditDto>.Failure("This pet does not belong to you!");
                }

                ShowAnimalToEditDto animalDto = new ShowAnimalToEditDto()
                {
                    Age = animal.Age,
                    BirthDate = animal.BirthDate.ToString(),
                    Description = animal.Description,
                    IsEducated = animal.IsEducated,
                    IsHavingValidDocuments = animal.IsHavingValidDocuments,
                    Name = animal.Name,
                    SocialMedia = animal.SocialMedia,
                    Weight = animal.Weight,
                    Gender = animal.Gender,
                    HealthStatus = animal.HealthStatus,
                    Photos = animal.Photos.Select(p => new PhotoDto()
                    {
                        Id = p.Id,
                        IsMain = p.IsMain,
                        Url = p.Url,
                    }).ToArray(),
                };

                if (!((DateTime.UtcNow - animal.LastModified).Days < 30))
                {
                    animalDto.CanEditAll = true;
                }

                return Result<ShowAnimalToEditDto>.Success(animalDto);
            }
        }
    }
}
