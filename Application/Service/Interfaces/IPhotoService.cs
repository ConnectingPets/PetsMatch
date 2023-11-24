namespace Application.Service.Interfaces
{
    using MediatR;
    using Microsoft.AspNetCore.Http;

    using Response;

    public interface IPhotoService
    {
        Task<Result<Unit>> AddUserPhotoAsync(IFormFile file, string userId);
        Task<Result<Unit>> AddAnimalPhotoAsync(IFormFile file, string animalId);
        Task<Result<Unit>> DeletePhotoAsync(string photoId);
        Task<Result<Unit>> SetUserMainPhotoAsync(string photoId);
        Task<Result<Unit>> SetAnimalMainPhotoAsync(string photoId);
    }
}
