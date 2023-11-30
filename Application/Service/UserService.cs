namespace Application.Service
{
    using System.Threading.Tasks;
    
    using MediatR;
    
    using Domain;
    using Application.Service.Interfaces;
    using static Application.User.LoginUser;
    using static Application.User.LogoutUser;
    using static Application.User.RegisterUser;

    public class UserService : IUserService
    {
        private readonly IMediator mediator;

        public UserService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<User> LoginAsync(string email, string password, bool rememberMe)
            => await this.mediator.Send(new LoginUserCommand
            {
                Email = email,
                Password = password,
                RememberMe = rememberMe
            });

        public async Task LogoutAsync(string userId)
            => await this.mediator.Send(new LogoutUserCommand
            {
                UserId = userId
            });

        public async Task<User> RegisterAsync(string email, string password, string name)
            => await this.mediator.Send(new RegisterUserCommand
            {
                Email = email,
                Password = password,
                Name = name
            });
    }
}
