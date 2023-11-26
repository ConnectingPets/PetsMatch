namespace Application.Service.Interfaces
{
    using Domain;

    public interface IUserService
    {
        Task<User> LoginAsync(string email, string password, bool rememberMe);

        Task<User> RegisterAsync(string email, string password, string name);

        Task LogoutAsync(string email);
    }
}
