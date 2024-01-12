namespace Application.Service
{
    using System.Threading.Tasks;

    using MediatR;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    using Domain;
    using Response;
    using Interfaces;
    using Application.DTOs.Photo;
    using Persistence.Repositories;

    using static Common.SuccessMessages.Photo;
    using static Common.ExceptionMessages.Photo;

    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary cloudinary;
        private readonly IRepository repository;

        public PhotoService(Cloudinary cloudinary,
                            IRepository repository)
        {
            this.cloudinary = cloudinary;
            this.repository = repository;
        }

        public async Task<Result<Unit>> AddAnimalPhotosAsync(
            IFormFile[] files, Animal animal)
        {
            try
            {
                foreach (var file in files)
                {
                    if (animal.Photos.Count == 6)
                    {
                        return Result<Unit>.Failure(FullCapacityImage);
                    }

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
                            return Result<Unit>.Failure(ErrorUploadPhoto);
                        }
                    }

                    Photo photo = new Photo()
                    {
                        Id = imageUploadResult.PublicId,
                        IsMain = false,
                        Url = imageUploadResult.Url.AbsoluteUri,
                        AnimalId = animal.AnimalId
                    };

                    await repository.AddAsync(photo);
                    await repository.SaveChangesAsync();
                }

                return 
                    Result<Unit>.Success(Unit.Value, SuccessfullyUploadPhoto);
            }
            catch
            {
                return Result<Unit>.Failure(ErrorUploadPhoto);
            }
        }

        public async Task<Result<Unit>> AddUserPhotoAsync(IFormFile file, string userId)
        {
            try
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
                        return Result<Unit>.
                            Failure("Error occurred during image upload");                       
                    }
                }

                Photo photo = new Photo()
                {
                    Id = imageUploadResult.PublicId,
                    IsMain = true,
                    Url = imageUploadResult.Url.AbsoluteUri
                };

                User? user = await repository.
                    FirstOrDefaultAsync<User>(u => u.Id.ToString() == userId);
                user!.PhotoId = photo.Id;

                await repository.AddAsync(photo);
                await repository.SaveChangesAsync();

                return Result<Unit>.Success(Unit.Value, SuccessfullyUploadPhoto);
            }
            catch (Exception)
            {
                return Result<Unit>.Failure(ErrorUploadPhoto);
            }
        }

        public async Task<Result<Unit>> DeleteAnimalPhotoAsync(Photo photo)
        {
            try
            {
                var deleteParams = new DeletionParams(photo.Id);

                try
                {
                    await cloudinary.DestroyAsync(deleteParams);
                }
                catch (Exception)
                {

                    return Result<Unit>.Failure(FailedToDeletePhoto);
                }

                repository.Delete(photo);

                try
                {
                    await repository.SaveChangesAsync();

                    return Result<Unit>.Success(Unit.Value, SuccessfullyDeletedPhoto);
                }
                catch
                {
  
                    return Result<Unit>.Failure(ErrorDeletingPhoto);
                }
            }
            catch (Exception)
            {

                return Result<Unit>.Failure(ErrorDeletingPhoto);
            }
        }

        public async Task<Result<Unit>> DeleteUserPhotoAsync(Photo photo, string userId)
        {
            try
            {
                var deleteParams = new DeletionParams(photo.Id);

                try
                {
                    await cloudinary.DestroyAsync(deleteParams);
                }
                catch (Exception)
                {

                    return Result<Unit>.Failure(FailedToDeletePhoto);
                }

                User? user = await
                    repository.GetById<User>(Guid.Parse(userId));
                user!.PhotoId = null;

                repository.Delete(photo);

                try
                {
                    await repository.SaveChangesAsync();

                    return Result<Unit>.Success(Unit.Value, SuccessfullyDeletedPhoto);
                }
                catch
                {

                    return Result<Unit>.Failure(FailedToDeletePhoto);
                }
            }
            catch (Exception)
            {

                return Result<Unit>.Failure(ErrorDeletingPhoto);
            }
        }

        public async Task<Result<Unit>> SetAnimalMainPhotoAsync(Photo photo)
        {
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

                return Result<Unit>.Success(Unit.Value, SuccessfullySetMainPhoto);
            }
            catch (Exception)
            {

                return Result<Unit>.Failure(ErrorSetMain);
            }
        }
        public async Task<Result<string>> AddAnimalPhotosWithMainAsync(MainPhotoDto[] photos, Animal animal)
        {
            bool hasMain = photos.Any(p => p.IsMain);

            try
            {
                for (int i = 0; i < photos.Length; i++)
                {
                    MainPhotoDto photo = photos[i];
                    IFormFile file = photo.File;


                    if (animal.Photos.Count == 6)
                    {

                        return Result<string>.Failure(FullCapacityImage);
                    }

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

                            return Result<string>.Failure(ErrorUploadPhoto);
                        }
                    }

                    Photo photoToAdd = new Photo()
                    {
                        Id = imageUploadResult.PublicId,
                        IsMain = false,
                        Url = imageUploadResult.Url.AbsoluteUri,
                        AnimalId = animal.AnimalId
                    };

                    if (photo.IsMain)
                    {
                        photoToAdd.IsMain = true;
                    }

                    if (!hasMain && i == 0)
                    {
                        photoToAdd.IsMain = true;
                    }

                    await repository.AddAsync(photoToAdd);
                    await repository.SaveChangesAsync();
                }


                return Result<string>.Success(SuccessfullyUploadPhoto);
            }
            catch
            {

                return Result<string>.Failure(ErrorUploadPhoto);
            }
        }
    }
}
