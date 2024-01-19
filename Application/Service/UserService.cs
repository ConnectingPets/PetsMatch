namespace Application.Service
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using MediatR;

    using Application.Response;
    using Application.Service.Interfaces;
    using Application.DTOs.User;

    using static Application.User.LoginUser;
    using static Application.User.LogoutUser;
    using static Application.User.RegisterUser;
    using static Application.User.GetAllTowns;

    public class UserService : IUserService
    {
        private readonly IMediator mediator;

        public UserService(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<Result<UserDto>> LoginAsync(string email, string password, bool rememberMe)
            => await this.mediator.Send(new LoginUserCommand
            {
                Email = email,
                Password = password,
                RememberMe = rememberMe
            });

        public async Task<Result<Unit>> LogoutAsync(string userId)
            => await this.mediator.Send(new LogoutUserCommand
            {
                UserId = userId
            });

        public async Task<Result<UserDto>> RegisterAsync(string email, string password, string name, string[] roles)
            => await this.mediator.Send(new RegisterUserCommand
            {
                Email = email,
                Password = password,
                Name = name,
                Roles = roles
            });

        public async Task<Result<IEnumerable<string>>> GetAllTownsAsync()
        => await this.mediator.Send(new GetAllTownsQuery());
    }
}
