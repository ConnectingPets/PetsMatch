namespace Application.Service.Interfaces
{
    using MediatR;
    using Microsoft.AspNetCore.Http;

    using Response;
    using Domain;

    public interface IPhotoService
    {
        Task<Result<Unit>> AddUserPhotoAsync(IFormFile file, string userId);
        Task<Result<Unit>> AddAnimalPhotoAsync(IFormFile file, string animalId, Animal animal);
        Task<Result<Unit>> DeletePhotoAsync(string photoId, Photo photo);
        Task<Result<Unit>> SetUserMainPhotoAsync(string photoId, Photo photo);
        Task<Result<Unit>> SetAnimalMainPhotoAsync(string photoId, Photo photo);
    }
}
