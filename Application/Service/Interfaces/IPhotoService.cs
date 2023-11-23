namespace Application.Service.Interfaces
{
    using MediatR;
    using Microsoft.AspNetCore.Http;

    using Response;

    public interface IPhotoService
    {
        Task<Result<Unit>> AddUserPhotoAsync(IFormFile file, string userId);
        Task<Result<Unit>> DeleteUserPhotoAsync(string photoId, string userId);
        Task<Result<Unit>> SetMainUserPhotoAsync(string photoId, string userId);
        Task<Result<Unit>> AddAnimalPhotoAsync(IFormFile file, string animalId);
        Task<Result<Unit>> DeleteAnimalPhotoAsync(string photoId, string animalId);
        Task<Result<Unit>> SetMainAnimalPhotoAsync(string photoId, string animalId);
    }
}
