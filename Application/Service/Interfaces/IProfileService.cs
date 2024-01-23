namespace Application.Service.Interfaces
{
    using MediatR;
    using Response;
    using DTOs.User;
    using Domain;

    public interface IProfileService
    {
        Task<Result<UserProfileDto>> GetProfile(string userId);

        Task<Result<Unit>> EditUser(string userId, EditUserDto editUserDto);

        Task<Result<Unit>> DeleteUser(string userId);

        Task<Result<Unit>> DeleteRole(string userId, string roleName);

        Task<Result<Unit>> ChangePassword(ChangePasswordDto dto, User user);
    }
}
