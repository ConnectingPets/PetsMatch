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
    using Persistence;

    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary cloudinary;
        private readonly IRepository repository;
        private readonly DataContext dataContext;

        public PhotoService(Cloudinary cloudinary,
                            IRepository repository,
                            DataContext dataContext)
        {
            this.cloudinary = cloudinary;
            this.repository = repository;
            this.dataContext = dataContext;
        }

        public async Task<Result<string>> AddAnimalPhotoAsync(IFormFile file, string animalId, Animal animal)
        {
            using var transaction =
                await dataContext.Database.BeginTransactionAsync();
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
                        return Result<string>.Failure("Error occurred during image upload");
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
                await repository.SaveChangesAsync();
                await transaction.CommitAsync();

                return Result<string>.Success(photo.Id, "Successfully upload image");
            }
            catch
            {
                await transaction.RollbackAsync();
                return Result<string>.Failure("Error occurred during uploading photo");
            }

        }

        public async Task<Result<Unit>> AddUserPhotoAsync(IFormFile file, string userId)
        {
            using var transaction =
                await dataContext.Database.BeginTransactionAsync();
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
                await transaction.CommitAsync();

                return Result<Unit>.Success(Unit.Value,"Successfully upload image");
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return Result<Unit>.Failure("Error occurred during uploading photo");
            }
        }

        public async Task<Result<Unit>> DeleteAnimalPhotoAsync(string photoId, Photo photo)
        {
            using var transaction =
                await dataContext.Database.BeginTransactionAsync();
            try
            {
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
                    await transaction.CommitAsync();
                    return Result<Unit>.Success(Unit.Value, "Successfully delete photo");
                }
                catch
                {
                    await transaction.RollbackAsync();
                    return Result<Unit>.Failure("Error occurred during saving changes");
                }
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return Result<Unit>.Failure("Error occurred during deleting photo");
            }
        }

        public async Task<Result<Unit>> DeleteUserPhotoAsync(string photoId, Photo photo, string userId)
        {
            using var transaction =
                await dataContext.Database.BeginTransactionAsync();
            try
            {
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
                User? user = await repository.FirstOrDefaultAsync<User>(u => u.Id.ToString() == userId);
                user!.PhotoId = null;

                try
                {
                    await repository.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return Result<Unit>.Success(Unit.Value, "Successfully delete photo");
                }
                catch
                {
                    await transaction.RollbackAsync();
                    return Result<Unit>.Failure("Error occurred during saving changes");
                }
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return Result<Unit>.Failure("Error occurred during deleting photo");
            }
        }

        public async Task<Result<Unit>> SetAnimalMainPhotoAsync(string photoId, Photo photo)
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
    }
}
