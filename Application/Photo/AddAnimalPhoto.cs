﻿namespace Application.Photo
{
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    using Domain;
    using Persistence.Repositories;
    using Service.Interfaces;
    using Response;

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
                        Result<string>.Failure("File is not selected or empty");
                }

                Animal? animal = await repository.
                    All<Animal>(a => a.AnimalId.ToString() == animalId).
                    Include(a => a.Photos).
                    FirstOrDefaultAsync();

                if (animal == null)
                {
                    return Result<string>.Failure("This animal does not exist! please select existing one");
                }

                var result = await photoService.AddAnimalPhotosAsync(files, animal);

                return result;
            }
        }
    }
}
