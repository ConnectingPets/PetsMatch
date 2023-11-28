namespace Application.Service.Interfaces
{
    using Application.DTOs.User;

    public interface IProfileService
    {
        Task<UserProfileDto> GetProfile(string userId);

        Task EditUser(string userId, EditUserDto editUserDto);

        Task DeleteUser(string userId);
    }
}
