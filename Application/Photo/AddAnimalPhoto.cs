namespace Application.Photo
{
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    using Domain;
    using Response;
    using DTOs.Photo;
    using Service.Interfaces;
    using Persistence.Repositories;

    using static Common.ExceptionMessages.Photo;
    using static Common.ExceptionMessages.Animal;

    public class AddAnimalPhoto
    {
        public class AddAnimalPhotoCommand : IRequest<Result<PhotoDto>>
        {
            public string AnimalId { get; set; } = null!;
            public IFormFile File { get; set; } = null!;
        }

        public class AddAnimalPhotoCommandHandler :
            IRequestHandler<AddAnimalPhotoCommand, Result<PhotoDto>>
        {
            private readonly IPhotoService photoService;
            private readonly IRepository repository;

            public AddAnimalPhotoCommandHandler(IPhotoService photoService,
                                                IRepository repository)
            {
                this.photoService = photoService;
                this.repository = repository;
            }

            public async Task<Result<PhotoDto>> Handle(AddAnimalPhotoCommand request, CancellationToken cancellationToken)
            {
                IFormFile files = request.File;
                string animalId = request.AnimalId;

                if (files == null || files.Length == 0)
                {
                    return
                        Result<PhotoDto>.Failure(EmptyPhoto);
                }

                Animal? animal = await repository.
                    All<Animal>(a => a.AnimalId.ToString() == animalId).
                    Include(a => a.Photos).
                    FirstOrDefaultAsync();

                if (animal == null)
                {
                    return Result<PhotoDto>.Failure(AnimalNotFound);
                }

                var result = await photoService.
                    AddAnimalPhotoAsync(files, animal);

                return result;
            }
        }
    }
}
