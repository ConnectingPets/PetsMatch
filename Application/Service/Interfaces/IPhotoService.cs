namespace Application.Service.Interfaces
{
    using MediatR;
    using Microsoft.AspNetCore.Http;

    using Domain;
    using Response;

    public interface IPhotoService
    {
        Task<Result<Unit>> AddUserPhotoAsync(IFormFile file, string userId);
        Task<Result<string>> AddAnimalPhotoAsync(IFormFile file, Animal animal);
        Task<Result<Unit>> DeleteAnimalPhotoAsync(Photo photo);
        Task<Result<Unit>> DeleteUserPhotoAsync(Photo photo, string userId);
        Task<Result<Unit>> SetAnimalMainPhotoAsync(Photo photo);
    }
}
