namespace Application.User
{
    using Application.Exceptions;
    using Domain;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using System.Threading;
    using System.Threading.Tasks;
    using static Common.ExceptionMessages.User;

    public class LogoutUser
    {
        public class LogoutUserCommand : IRequest<Unit>
        {
            public string UserId { get; set; } = null!;
        }

        public class LogoutHandler : IRequestHandler<LogoutUserCommand, Unit>
        {
            private readonly SignInManager<User> signInManager;
            private readonly UserManager<User> userManager;

            public LogoutHandler(
                SignInManager<User> signInManager,
                UserManager<User> userManager)
            {
                this.signInManager = signInManager;
                this.userManager = userManager;
            }

            public async Task<Unit> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
            {
                User? user = await this.userManager.FindByIdAsync(request.UserId);

                if (user == null)
                {
                    throw new UserNotFoundException(NotAuthenticated);
                }

                await this.signInManager.SignOutAsync();

                return Unit.Value;
            }
        }
    }
}
