namespace Application.Service.Interfaces
{
    using MediatR;

    using Application.DTOs.User;
    using Application.Response;

    public interface IUserService
    {
        Task<Result<UserDto>> LoginAsync(string email, string password, bool rememberMe);

        Task<Result<UserDto>> RegisterAsync(string email, string password, string name, string[] roles);

        Task<Result<Unit>> LogoutAsync(string email);

        Task<Result<IEnumerable<string>>> GetAllTownsAsync();
    }
}
