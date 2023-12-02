namespace Application.Service.Interfaces
{
    using MediatR;
    using Microsoft.AspNetCore.Http;

    using Domain;
    using Response;

    public interface IPhotoService
    {
        Task<Result<Unit>> AddUserPhotoAsync(IFormFile file, string userId);
        Task<Result<string>> AddAnimalPhotoAsync(IFormFile file, string animalId, Animal animal);
        Task<Result<Unit>> DeleteAnimalPhotoAsync(string photoId, Photo photo);
        Task<Result<Unit>> DeleteUserPhotoAsync(Photo photo, string userId, string photoId);
        Task<Result<Unit>> SetAnimalMainPhotoAsync(string photoId, Photo photo);
    }
}
