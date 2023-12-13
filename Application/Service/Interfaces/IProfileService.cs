namespace Application.Service.Interfaces
{
    using Application.DTOs.User;
    using Application.Response;
    using MediatR;

    public interface IProfileService
    {
        Task<Result<UserProfileDto>> GetProfile(string userId);

        Task<Result<Unit>> EditUser(string userId, EditUserDto editUserDto);

        Task<Result<Unit>> DeleteUser(string userId);
    }
}
