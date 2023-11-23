namespace Application.Service
{
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Http;

    using Interfaces;
    using Persistence.Repositories;

    public class PhotoAccessor : IPhotoService
    {
        private readonly Cloudinary cloudinary;
        private readonly IRepository repository;

        public PhotoAccessor(Cloudinary cloudinary, IRepository repository)
        {
            this.cloudinary = cloudinary;
            this.repository = repository;
        }

        public Task AddAnimalPhotoAsync(IFormFile file, string animalId)
        {
            throw new NotImplementedException();
        }

        public Task AddUserPhotoAsync(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAnimalPhotoAsync(string photoId, string animalId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserPhotoAsync(string photoId, string userId)
        {
            throw new NotImplementedException();
        }

        public Task SetMainAnimalPhotoAsync(string photoId, string animalId)
        {
            throw new NotImplementedException();
        }

        public Task SetMainUserPhotoAsync(string photoId, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
