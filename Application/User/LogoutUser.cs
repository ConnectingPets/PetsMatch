namespace Application.User
{
    using Domain;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class LogoutUser
    {
        public class LogoutUserCommand : IRequest<Unit>
            { }

        public class LogoutHandler : IRequestHandler<LogoutUserCommand, Unit>
        {
            private readonly SignInManager<User> signInManager;

            public LogoutHandler(SignInManager<User> signInManager)
            {
                this.signInManager = signInManager;
            }

            public async Task<Unit> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
            {
                return await this.signInManager.SignOutAsync();
            }
        }
    }
}
