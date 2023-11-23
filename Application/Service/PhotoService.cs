namespace Application.Service
{
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;
    using MediatR;

    using Interfaces;
    using Response;
    using Persistence.Repositories;
    using Domain;

    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary cloudinary;
        private readonly IRepository repository;

        public PhotoService(Cloudinary cloudinary, IRepository repository)
        {
            this.cloudinary = cloudinary;
            this.repository = repository;
        }

        public async Task<Result<Unit>> AddAnimalPhotoAsync(IFormFile file, string animalId)
        {
            var imageUploadResult = new ImageUploadResult();

            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream)
                };

                try
                {
                    imageUploadResult =
                        await cloudinary.UploadAsync(uploadParams);
                }
                catch
                {
                    return Result<Unit>.Failure("Error occurred during image upload");
                }
            }

            Photo photo = new Photo()
            {
                Id = imageUploadResult.PublicId,
                IsMain = false,
                Url = imageUploadResult.Url.AbsoluteUri,
                AnimalId = Guid.Parse(animalId)
            };

            await repository.AddAsync(photo);
            try
            {
                await repository.SaveChangesAsync();
                return Result<Unit>.Success(Unit.Value, "Successfully upload image");
            }
            catch
            {
                return Result<Unit>.Failure("Error occurred during saving changes");
            }
        }

        public async Task<Result<Unit>> AddUserPhotoAsync(IFormFile file, string userId)
        {
            var imageUploadResult = new ImageUploadResult();

            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream)
                };

                try
                {
                    imageUploadResult =
                        await cloudinary.UploadAsync(uploadParams);
                }
                catch
                {
                    return Result<Unit>.Failure("Error occurred during image upload");
                }
            }

            Photo photo = new Photo()
            {
                Id = imageUploadResult.PublicId,
                IsMain = false,
                Url = imageUploadResult.Url.AbsoluteUri,
                UserId = Guid.Parse(userId)
            };

            await repository.AddAsync(photo);
            try
            {
                await repository.SaveChangesAsync();
                return Result<Unit>.Success(Unit.Value, "Successfully upload image");
            }
            catch 
            {
                return Result<Unit>.Failure("Error occurred during saving changes");
            }
        }

        public Task<Result<Unit>> DeleteAnimalPhotoAsync(string photoId, string animalId)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Unit>> DeleteUserPhotoAsync(string photoId, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Unit>> SetMainAnimalPhotoAsync(string photoId, string animalId)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Unit>> SetMainUserPhotoAsync(string photoId, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
