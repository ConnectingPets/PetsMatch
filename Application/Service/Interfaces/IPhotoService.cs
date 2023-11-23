namespace Application.Service.Interfaces
{
    using Microsoft.AspNetCore.Http;

    public interface IPhotoService
    {
        Task AddUserPhotoAsync(IFormFile file);
        Task DeleteUserPhotoAsync(string photoId, string userId);
        Task SetMainUserPhotoAsync(string photoId, string userId);
        Task AddAnimalPhotoAsync(IFormFile file, string animalId);
        Task DeleteAnimalPhotoAsync(string photoId, string animalId);
        Task SetMainAnimalPhotoAsync(string photoId, string animalId);
    }
}
