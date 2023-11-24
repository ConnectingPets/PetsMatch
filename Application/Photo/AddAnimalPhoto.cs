namespace Application.Photo
{
    using Microsoft.AspNetCore.Http;
    using MediatR;

    using Response;
    using Service.Interfaces;
    using Domain;
    using Persistence.Repositories;

    public class AddAnimalPhoto
    {
        public class AddAnimalPhotoCommand : IRequest<Result<Unit>>
        {
            public string AnimalId { get; set; } = null!;
            public IFormFile File { get; set; } = null!;
        }

        public class AddAnimalPhotoCommandHandler :
            IRequestHandler<AddAnimalPhotoCommand, Result<Unit>>
        {
            private readonly IPhotoService photoService;
            private readonly IRepository repository;

            public AddAnimalPhotoCommandHandler(IPhotoService photoService,
                                                IRepository repository)
            {
                this.photoService = photoService;
                this.repository = repository;
            }

            public async Task<Result<Unit>> Handle(AddAnimalPhotoCommand request, CancellationToken cancellationToken)
            {
                IFormFile file = request.File;
                string animalId = request.AnimalId;

                if (file == null || file.Length == 0)
                {
                    return
                        Result<Unit>.Failure("File is not selected or empty");
                }

                if (!file.ContentType.StartsWith("image"))
                {
                    return
                        Result<Unit>.Failure("This file is not an image");
                }

                Animal? animal = await repository.
                FirstOrDefaultAsync<Animal>(a =>
                a.AnimalId.ToString() == animalId);

                if (animal == null)
                {
                    return Result<Unit>.Failure("This animal does not exist! please select existing one");
                }

                var result = await photoService.AddAnimalPhotoAsync(file, animalId, animal);

                return result;
            }
        }
    }
}
