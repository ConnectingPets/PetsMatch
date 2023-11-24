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
            Animal? animal = await repository.
                FirstOrDefaultAsync<Animal>(a =>
                a.AnimalId.ToString() == animalId);

            if (animal == null)
            {
                return Result<Unit>.Failure("This animal does not exist! please select existing one");
            }

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
                catch (Exception)
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
            catch (Exception)
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
                catch (Exception)
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
            catch (Exception)
            {
                return Result<Unit>.Failure("Error occurred during saving changes");
            }
        }

        public async Task<Result<Unit>> DeletePhotoAsync(string photoId)
        {
            Photo? photo = await repository.
                FirstOrDefaultAsync<Photo>(p => p.Id == photoId);

            if (photo == null)
            {
                return Result<Unit>.Failure("This photo does not exist! Please select existing one");
            }

            if (photo.IsMain)
            {
                return Result<Unit>.Failure("This is your main photo! You can not delete it");
            }

            var deleteParams = new DeletionParams(photoId);

            try
            {
                await cloudinary.DestroyAsync(deleteParams);
            }
            catch (Exception)
            {
                return Result<Unit>.Failure("Failed to delete photo");
            }

            repository.Delete(photo);

            try
            {
                await repository.SaveChangesAsync();
                return Result<Unit>.Success(Unit.Value, "Successfully delete photo");
            }
            catch
            {
                return Result<Unit>.Failure("Error occurred during saving changes");
            }
        }

        public async Task<Result<Unit>> SetAnimalMainPhotoAsync(string photoId)
        {
            Photo? photo = await repository.
                FirstOrDefaultAsync<Photo>(p => p.Id == photoId);

            if (photo == null)
            {
                return Result<Unit>.Failure("This photo does not exist! Please select existing one");
            }

            Photo? oldMainPhoto = await repository
                .FirstOrDefaultAsync<Photo>(p => p.IsMain 
                && p.AnimalId == photo.AnimalId);

            if (oldMainPhoto != null)
            {
                oldMainPhoto.IsMain = false;
            }

            photo.IsMain = true;

            try
            {
                await repository.SaveChangesAsync();
                return Result<Unit>.Success(Unit.Value, "You successfully set main photo");
            }
            catch (Exception)
            {
                return Result<Unit>.Failure("Error occurred during saving changes");
            }
        }

        public async Task<Result<Unit>> SetUserMainPhotoAsync(string photoId)
        {
            Photo? photo = await repository.
                FirstOrDefaultAsync<Photo>(p => p.Id == photoId);

            if (photo == null)
            {
                return Result<Unit>.Failure("This photo does not exist! Please select existing one");
            }

            Photo? oldMainPhoto = await repository
                .FirstOrDefaultAsync<Photo>(p => p.IsMain && 
                p.UserId == photo.UserId);

            if (oldMainPhoto != null)
            {
                oldMainPhoto.IsMain = false;
            }

            photo.IsMain = true;

            try
            {
                await repository.SaveChangesAsync();
                return Result<Unit>.Success(Unit.Value, "You successfully set main photo");
            }
            catch (Exception)
            {
                return Result<Unit>.Failure("Error occurred during saving changes");
            }
        }
    }
}
