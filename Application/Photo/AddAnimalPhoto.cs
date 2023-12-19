namespace Application.Photo
{
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    using Domain;
    using Response;
    using Service.Interfaces;
    using Persistence.Repositories;
    using static Common.ExceptionMessages.Photo;
    using static Common.ExceptionMessages.Animal;

    public class AddAnimalPhoto
    {
        public class AddAnimalPhotoCommand : IRequest<Result<string>>
        {
            public string AnimalId { get; set; } = null!;
            public IFormFile[] Files { get; set; } = null!;
        }

        public class AddAnimalPhotoCommandHandler :
            IRequestHandler<AddAnimalPhotoCommand, Result<string>>
        {
            private readonly IPhotoService photoService;
            private readonly IRepository repository;

            public AddAnimalPhotoCommandHandler(IPhotoService photoService,
                                                IRepository repository)
            {
                this.photoService = photoService;
                this.repository = repository;
            }

            public async Task<Result<string>> Handle(AddAnimalPhotoCommand request, CancellationToken cancellationToken)
            {
                IFormFile[] files = request.Files;
                string animalId = request.AnimalId;

                if (files == null || files.Length == 0)
                {
                    return
                        Result<string>.Failure(EmptyPhoto);
                }

                Animal? animal = await repository.
                    All<Animal>(a => a.AnimalId.ToString() == animalId).
                    Include(a => a.Photos).
                    FirstOrDefaultAsync();

                if (animal == null)
                {
                    return Result<string>.Failure(AnimalNotFound);
                }

                var result = await photoService.AddAnimalPhotosAsync(files, animal);

                return result;
            }
        }
    }
}
