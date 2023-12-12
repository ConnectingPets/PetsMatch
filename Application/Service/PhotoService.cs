namespace Application.Service
{
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;
    using MediatR;

    using Domain;
    using Interfaces;
    using Response;
    using Persistence.Repositories;
    using Application.DTOs.Photo;
    using System.Net;

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

        public async Task<Result<string>> AddAnimalPhotosAsync(IFormFile[] files, Animal animal)
        {
            try
            {
                foreach (var file in files)
                {
                    if (animal.Photos.Count == 6)
                    {
                        return Result<string>.Failure("You already have 6 photos of this animal. You cannot add more");
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
                            return Result<string>.Failure("Error occurred during images upload");
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

                return Result<string>.Success("Successfully upload images");
            }
            catch
            {
                return Result<string>.Failure("Error occurred during uploading photo");
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
                        return Result<Unit>.Failure("Error occurred during image upload");
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

                return Result<Unit>.Success(Unit.Value, "Successfully upload image");
            }
            catch (Exception)
            {
                return Result<Unit>.Failure("Error occurred during uploading photo");
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
            catch (Exception)
            {
                return Result<Unit>.Failure("Error occurred during deleting photo");
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
                    return Result<Unit>.Failure("Failed to delete photo");
                }

                User? user = await
                    repository.GetById<User>(Guid.Parse(userId));
                user!.PhotoId = null;

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
            catch (Exception)
            {
                return Result<Unit>.Failure("Error occurred during deleting photo");
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
                return Result<Unit>.Success(Unit.Value, "You successfully set main photo");
            }
            catch (Exception)
            {
                return Result<Unit>.Failure("Error occurred during saving changes");
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
                        return Result<string>.Failure("You already have 6 photos of this animal. You cannot add more");
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
                            return Result<string>.Failure("Error occurred during images upload");
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

                return Result<string>.Success("Successfully upload images");
            }
            catch
            {
                return Result<string>.Failure("Error occurred during uploading photo");
            }
        }
    }
}
