namespace Application.User
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Microsoft.AspNetCore.Identity;

    using Domain;
    using Response;
    using DTOs.User;

    using static Common.ExceptionMessages.User;
    using static Common.SuccessMessages.User;

    public class ChangePassword
    {
        public class ChangePasswordCommand : IRequest<Result<Unit>>
        {
            public ChangePasswordDto Dto { get; set; } = null!;
            public User User { get; set; } = null!;
        }

        public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result<Unit>>
        {
            private readonly UserManager<User> userManager;

            public ChangePasswordCommandHandler(UserManager<User> userManager)
            {
                this.userManager = userManager;
            }

            public async Task<Result<Unit>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
            {
                var user = request.User;
                var dto = request.Dto;

                var isOldPasswordCorrect = 
                    await userManager.CheckPasswordAsync(user, dto.OldPassword);

                if (!isOldPasswordCorrect)
                {
                    return Result<Unit>.Failure(InvalidOldPassword);
                }

                var changePasswordResult = 
                    await userManager.
                    ChangePasswordAsync(user, dto.OldPassword, dto.NewPassword);

                if (!changePasswordResult.Succeeded)
                {
                    return Result<Unit>.Failure(ChangingPasswordFailed);
                }

                return Result<Unit>.Success(Unit.Value, SuccessChangePassword);
            }
        }
    }
}
