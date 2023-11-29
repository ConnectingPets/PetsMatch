namespace Application.Photo
{
    using Microsoft.AspNetCore.Http;
    using MediatR;

    using Response;
    using Service.Interfaces;
    using Domain;
    using Persistence.Repositories;
    using Persistence;
    using Microsoft.EntityFrameworkCore;

    public class AddAnimalPhoto
    {
        public class AddAnimalPhotoCommand : IRequest<Result<string>>
        {
            public string AnimalId { get; set; } = null!;
            public IFormFile File { get; set; } = null!;
        }

        public class AddAnimalPhotoCommandHandler :
            IRequestHandler<AddAnimalPhotoCommand, Result<string>>
        {
            private readonly IPhotoService photoService;
            private readonly IRepository repository;
            private readonly DataContext dataContext;

            public AddAnimalPhotoCommandHandler(IPhotoService photoService,
                                                IRepository repository,
                                                DataContext dataContext)
            {
                this.photoService = photoService;
                this.repository = repository;
                this.dataContext = dataContext;
            }

            public async Task<Result<string>> Handle(AddAnimalPhotoCommand request, CancellationToken cancellationToken)
            {
                IFormFile file = request.File;
                string animalId = request.AnimalId;

                if (file == null || file.Length == 0)
                {
                    return
                        Result<string>.Failure("File is not selected or empty");
                }

                if (!file.ContentType.StartsWith("image"))
                {
                    return
                        Result<string>.Failure("This file is not an image");
                }

                Animal? animal = await repository.
                    All<Animal>(a => a.AnimalId.ToString() == animalId).
                    Include(a => a.Photos).
                    FirstOrDefaultAsync();

                if (animal == null)
                {
                    return Result<string>.Failure("This animal does not exist! please select existing one");
                }

                if (animal.Photos.Count() >= 6)
                {
                    return Result<string>.Failure("You already have 6 photos of this animal. You cannot add more");
                }

                var result = await photoService.AddAnimalPhotoAsync(file, animalId, animal);

                return result;
            }
        }
    }
}
