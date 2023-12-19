namespace Application.User
{
    using System.Threading;
    using System.Threading.Tasks;
    
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    
    using Domain;
    using Application.Response;

    using static Common.ExceptionMessages.User;
    using static Common.FailMessages.User;

    public class LogoutUser
    {
        public class LogoutUserCommand : IRequest<Result<Unit>>
        {
            public string UserId { get; set; } = null!;
        }

        public class LogoutHandler : IRequestHandler<LogoutUserCommand, Result<Unit>>
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

            public async Task<Result<Unit>> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
            {
                User? user = await this.userManager.FindByIdAsync(request.UserId);

                if (user == null)
                {
                    return Result<Unit>.Failure(NotAuthenticated);
                }

                try
                {
                    await this.signInManager.SignOutAsync();
                    return Result<Unit>.Success(Unit.Value);
                }
                catch (Exception)
                {
                    return Result<Unit>.Failure(FailedLogout);
                }
            }
        }
    }
}
