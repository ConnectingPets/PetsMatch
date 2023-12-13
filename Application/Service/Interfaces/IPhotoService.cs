namespace Application.Service.Interfaces
{
    using MediatR;
    using Microsoft.AspNetCore.Http;

    using Domain;
    using Response;
    using Application.DTOs.Photo;

    public interface IPhotoService
    {
        Task<Result<Unit>> AddUserPhotoAsync(IFormFile file, string userId);
        Task<Result<string>> AddAnimalPhotosAsync(IFormFile[] file, Animal animal);
        Task<Result<Unit>> DeleteAnimalPhotoAsync(Photo photo);
        Task<Result<Unit>> DeleteUserPhotoAsync(Photo photo, string userId);
        Task<Result<Unit>> SetAnimalMainPhotoAsync(Photo photo);
        Task<Result<string>> AddAnimalPhotosWithMainAsync(MainPhotoDto[] photos, Animal animal);
    }
}
